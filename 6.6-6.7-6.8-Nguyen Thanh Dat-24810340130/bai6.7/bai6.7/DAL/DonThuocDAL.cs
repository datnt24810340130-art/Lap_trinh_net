using bai6._7.Database;
using bai6._7.DTO;
using Microsoft.Data.SqlClient;
using System.Data;

namespace bai6._7.DAL;

public sealed class DonThuocDAL
{
    public List<ThuocDTO> LayDanhSachThuoc()
    {
        List<ThuocDTO> medicines = [];
        using SqlConnection connection = DatabaseHelper.GetConnection();
        using SqlCommand command = new(
            "SELECT MaThuoc, TenThuoc, DonViTinh, DonGia, SoLuongTon FROM dbo.Thuoc ORDER BY TenThuoc;",
            connection);
        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            medicines.Add(new ThuocDTO
            {
                MaThuoc = reader.GetInt32(0),
                TenThuoc = reader.GetString(1),
                DonViTinh = reader.GetString(2),
                DonGia = reader.GetDecimal(3),
                SoLuongTon = reader.GetInt32(4)
            });
        }

        return medicines;
    }

    public int LuuVaXuatDon(int maHoSo, IReadOnlyCollection<ChiTietDonThuocDTO> chiTiet)
    {
        if (chiTiet.Count == 0)
        {
            throw new ArgumentException("Đơn thuốc phải có ít nhất một loại thuốc.", nameof(chiTiet));
        }

        if (chiTiet.Any(item => item.MaThuoc <= 0 || item.SoLuong <= 0))
        {
            throw new ArgumentException("Mã thuốc và số lượng kê phải hợp lệ.", nameof(chiTiet));
        }

        var quantities = chiTiet
            .GroupBy(item => item.MaThuoc)
            .ToDictionary(group => group.Key, group => group.Sum(item => item.SoLuong));

        using SqlConnection connection = DatabaseHelper.GetConnection();
        connection.Open();
        using SqlTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
        bool rolledBack = false;

        try
        {
            using SqlCommand prescriptionCommand = new(
                "INSERT dbo.DonThuoc (MaHoSo) VALUES (@MaHoSo); SELECT CONVERT(int, SCOPE_IDENTITY());",
                connection, transaction);
            prescriptionCommand.Parameters.Add("@MaHoSo", SqlDbType.Int).Value = maHoSo;
            int maDon = (int)prescriptionCommand.ExecuteScalar()!;

            foreach ((int maThuoc, int soLuong) in quantities)
            {
                using SqlCommand stockCommand = new(
                    "SELECT TenThuoc, DonGia, SoLuongTon FROM dbo.Thuoc WITH (UPDLOCK, HOLDLOCK) WHERE MaThuoc = @MaThuoc;",
                    connection, transaction);
                stockCommand.Parameters.Add("@MaThuoc", SqlDbType.Int).Value = maThuoc;

                string tenThuoc;
                decimal donGia;
                int soLuongTon;
                using (SqlDataReader reader = stockCommand.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        transaction.Rollback();
                        rolledBack = true;
                        throw new InvalidOperationException($"Không tìm thấy thuốc có mã {maThuoc}.");
                    }

                    tenThuoc = reader.GetString(0);
                    donGia = reader.GetDecimal(1);
                    soLuongTon = reader.GetInt32(2);
                }

                if (soLuongTon < soLuong)
                {
                    transaction.Rollback();
                    rolledBack = true;
                    throw new InvalidOperationException($"Thuốc {tenThuoc} không đủ tồn kho. Còn {soLuongTon}, cần {soLuong}.");
                }

                using SqlCommand updateStockCommand = new(
                    "UPDATE dbo.Thuoc SET SoLuongTon = SoLuongTon - @SoLuong WHERE MaThuoc = @MaThuoc;",
                    connection, transaction);
                updateStockCommand.Parameters.Add("@SoLuong", SqlDbType.Int).Value = soLuong;
                updateStockCommand.Parameters.Add("@MaThuoc", SqlDbType.Int).Value = maThuoc;
                updateStockCommand.ExecuteNonQuery();

                using SqlCommand detailCommand = new(
                    "INSERT dbo.ChiTietDonThuoc (MaDon, MaThuoc, SoLuong, DonGia) VALUES (@MaDon, @MaThuoc, @SoLuong, @DonGia);",
                    connection, transaction);
                detailCommand.Parameters.Add("@MaDon", SqlDbType.Int).Value = maDon;
                detailCommand.Parameters.Add("@MaThuoc", SqlDbType.Int).Value = maThuoc;
                detailCommand.Parameters.Add("@SoLuong", SqlDbType.Int).Value = soLuong;
                SqlParameter priceParameter = detailCommand.Parameters.Add("@DonGia", SqlDbType.Decimal);
                priceParameter.Precision = 18;
                priceParameter.Scale = 2;
                priceParameter.Value = donGia;
                detailCommand.ExecuteNonQuery();
            }

            transaction.Commit();
            return maDon;
        }
        catch
        {
            if (!rolledBack && transaction.Connection is not null)
            {
                transaction.Rollback();
            }

            throw;
        }
    }
}
