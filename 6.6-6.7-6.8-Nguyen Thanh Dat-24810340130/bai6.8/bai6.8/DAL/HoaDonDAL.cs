using bai6._8.DTO;
using Microsoft.Data.SqlClient;

namespace bai6._8.DAL;

public sealed class HoaDonDAL
{
    public HoaDonDTO? LayHoaDonDangMo(int maBan)
    {
        using var connection = DatabaseHelper.CreateConnection();
        using var command = new SqlCommand("SELECT TOP (1) MaHD, MaBan, GioBatDau, GiaTheoGio, TrangThai FROM dbo.HoaDon WHERE MaBan = @MaBan AND TrangThai = N'Đang mở' ORDER BY MaHD DESC;", connection);
        command.Parameters.AddWithValue("@MaBan", maBan);
        connection.Open();
        using var reader = command.ExecuteReader();
        if (!reader.Read())
            return null;

        return new HoaDonDTO
        {
            MaHD = reader.GetInt32(0),
            MaBan = reader.GetInt32(1),
            GioBatDau = reader.GetDateTime(2),
            GiaTheoGio = reader.GetDecimal(3),
            TrangThai = reader.GetString(4)
        };
    }

    public HoaDonDTO MoBan(int maBan, decimal giaTheoGio)
    {
        using var connection = DatabaseHelper.CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction();
        try
        {
            using (var updateTable = new SqlCommand("UPDATE dbo.BanBida SET TrangThai = N'Đang chơi' WHERE MaBan = @MaBan AND TrangThai = N'Trống';", connection, transaction))
            {
                updateTable.Parameters.AddWithValue("@MaBan", maBan);
                if (updateTable.ExecuteNonQuery() != 1)
                    throw new InvalidOperationException("Bàn không còn trống hoặc không tồn tại.");
            }

            using var insertBill = new SqlCommand("INSERT INTO dbo.HoaDon (MaBan, GioBatDau, GiaTheoGio) OUTPUT INSERTED.MaHD, INSERTED.GioBatDau VALUES (@MaBan, SYSDATETIME(), @GiaTheoGio);", connection, transaction);
            insertBill.Parameters.AddWithValue("@MaBan", maBan);
            insertBill.Parameters.AddWithValue("@GiaTheoGio", giaTheoGio);
            using var reader = insertBill.ExecuteReader();
            reader.Read();
            var bill = new HoaDonDTO
            {
                MaHD = reader.GetInt32(0),
                MaBan = maBan,
                GioBatDau = reader.GetDateTime(1),
                GiaTheoGio = giaTheoGio,
                TrangThai = "Đang mở"
            };
            reader.Close();
            transaction.Commit();
            return bill;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
}
