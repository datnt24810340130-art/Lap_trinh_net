using System.Data;
using Microsoft.Data.SqlClient;

namespace bai6._8.DAL;

public sealed class ChiTietHoaDonDAL
{
    public DataTable NapDichVuTheoHoaDon(int maHD)
    {
        using var connection = DatabaseHelper.CreateConnection();
        using var adapter = new SqlDataAdapter(
            "SELECT ct.MaDV, dv.TenDV, ct.SoLuong, ct.DonGia, ct.ThanhTien " +
            "FROM dbo.ChiTietHoaDon ct INNER JOIN dbo.MenuDichVu dv ON dv.MaDV = ct.MaDV " +
            "WHERE ct.MaHD = @MaHD ORDER BY dv.TenDV;", connection);
        adapter.SelectCommand!.Parameters.AddWithValue("@MaHD", maHD);
        var table = new DataTable();
        adapter.Fill(table);
        return table;
    }

    public void ThemHoacCapNhatDichVu(int maHD, int maDV, int soLuong)
    {
        if (soLuong <= 0)
            throw new ArgumentOutOfRangeException(nameof(soLuong), "Số lượng phải lớn hơn 0.");

        using var connection = DatabaseHelper.CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction();
        try
        {
            using var command = new SqlCommand(
                "DECLARE @TonKho INT, @SoLuongHienTai INT; " +
                "SELECT @TonKho = TonKho FROM dbo.MenuDichVu WITH (UPDLOCK, HOLDLOCK) WHERE MaDV = @MaDV; " +
                "IF @TonKho IS NULL THROW 50001, N'Dịch vụ không tồn tại.', 1; " +
                "SELECT @SoLuongHienTai = SoLuong FROM dbo.ChiTietHoaDon WHERE MaHD = @MaHD AND MaDV = @MaDV; " +
                "IF ISNULL(@SoLuongHienTai, 0) + @SoLuong > @TonKho THROW 50002, N'Số lượng vượt quá tồn kho.', 1; " +
                "UPDATE dbo.ChiTietHoaDon SET SoLuong = SoLuong + @SoLuong WHERE MaHD = @MaHD AND MaDV = @MaDV; " +
                "IF @@ROWCOUNT = 0 INSERT INTO dbo.ChiTietHoaDon (MaHD, MaDV, SoLuong, DonGia) " +
                "SELECT @MaHD, MaDV, @SoLuong, DonGia FROM dbo.MenuDichVu WHERE MaDV = @MaDV;",
                connection, transaction);
            command.Parameters.AddWithValue("@MaHD", maHD);
            command.Parameters.AddWithValue("@MaDV", maDV);
            command.Parameters.AddWithValue("@SoLuong", soLuong);
            command.ExecuteNonQuery();
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
}
