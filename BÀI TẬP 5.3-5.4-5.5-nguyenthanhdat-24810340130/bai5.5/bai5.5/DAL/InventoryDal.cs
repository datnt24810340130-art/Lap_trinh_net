using System.Data;
using bai5._5.DTO;
using Microsoft.Data.SqlClient;

namespace bai5._5.DAL;

public sealed class InventoryDal(DbConnectionFactory connectionFactory)
{
    public int Save(InventoryDocumentDto document)
    {
        const string insertHeader = """
            INSERT INTO dbo.PhieuKho (LoaiPhieu, MaNCC, TongTien)
            OUTPUT INSERTED.MaPhieu
            VALUES (@LoaiPhieu, @MaNCC, @TongTien);
            """;
        const string insertLine = """
            INSERT INTO dbo.ChiTietPhieuKho (MaPhieu, MaVatTu, SoLuong, DonGia)
            VALUES (@MaPhieu, @MaVatTu, @SoLuong, @DonGia);
            """;
        const string updateStock = """
            UPDATE dbo.VatTu
            SET SoLuongTon = SoLuongTon + CASE WHEN @LoaiPhieu = N'NHAP' THEN @SoLuong ELSE -@SoLuong END
            WHERE MaVatTu = @MaVatTu
              AND (@LoaiPhieu = N'NHAP' OR SoLuongTon >= @SoLuong);
            """;

        using var connection = connectionFactory.CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
        try
        {
            var total = document.ChiTiet.Sum(line => line.SoLuong * line.DonGia);
            int maPhieu;
            using (var command = new SqlCommand(insertHeader, connection, transaction))
            {
                command.Parameters.Add("@LoaiPhieu", SqlDbType.NVarChar, 10).Value = document.LoaiPhieu;
                command.Parameters.Add("@MaNCC", SqlDbType.Int).Value = document.MaNCC ?? (object)DBNull.Value;
                AddDecimal(command, "@TongTien", total);
                maPhieu = Convert.ToInt32(command.ExecuteScalar());
            }

            foreach (var line in document.ChiTiet)
            {
                using (var command = new SqlCommand(insertLine, connection, transaction))
                {
                    command.Parameters.Add("@MaPhieu", SqlDbType.Int).Value = maPhieu;
                    command.Parameters.Add("@MaVatTu", SqlDbType.VarChar, 20).Value = line.MaVatTu;
                    AddDecimal(command, "@SoLuong", line.SoLuong);
                    AddDecimal(command, "@DonGia", line.DonGia);
                    command.ExecuteNonQuery();
                }

                using (var command = new SqlCommand(updateStock, connection, transaction))
                {
                    command.Parameters.Add("@LoaiPhieu", SqlDbType.NVarChar, 10).Value = document.LoaiPhieu;
                    command.Parameters.Add("@MaVatTu", SqlDbType.VarChar, 20).Value = line.MaVatTu;
                    AddDecimal(command, "@SoLuong", line.SoLuong);
                    if (command.ExecuteNonQuery() == 0)
                    {
                        if (document.LoaiPhieu == "XUAT")
                            throw new InvalidOperationException("Không đủ hàng trong kho hoặc mã vật tư không tồn tại.");
                        throw new InvalidOperationException($"Không tìm thấy vật tư {line.MaVatTu}.");
                    }
                }
            }

            transaction.Commit();
            return maPhieu;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    private static void AddDecimal(SqlCommand command, string name, decimal value)
    {
        var parameter = command.Parameters.Add(name, SqlDbType.Decimal);
        parameter.Precision = 18;
        parameter.Scale = 2;
        parameter.Value = value;
    }
}
