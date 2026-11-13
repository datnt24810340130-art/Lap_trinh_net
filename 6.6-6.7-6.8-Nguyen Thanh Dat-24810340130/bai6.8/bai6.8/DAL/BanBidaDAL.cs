using bai6._8.DTO;
using Microsoft.Data.SqlClient;

namespace bai6._8.DAL;

public sealed class BanBidaDAL
{
    public List<BanBidaDTO> LayDanhSachBan()
    {
        var result = new List<BanBidaDTO>();
        using var connection = DatabaseHelper.CreateConnection();
        using var command = new SqlCommand("SELECT MaBan, TenBan, TrangThai FROM dbo.BanBida ORDER BY MaBan;", connection);
        connection.Open();
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            result.Add(new BanBidaDTO
            {
                MaBan = reader.GetInt32(0),
                TenBan = reader.GetString(1),
                TrangThai = reader.GetString(2)
            });
        }
        return result;
    }

    public bool CapNhatTrangThai(int maBan, string trangThai)
    {
        if (trangThai is not ("Trống" or "Đang chơi"))
            throw new ArgumentException("Trạng thái bàn không hợp lệ.", nameof(trangThai));

        using var connection = DatabaseHelper.CreateConnection();
        using var command = new SqlCommand("UPDATE dbo.BanBida SET TrangThai = @TrangThai WHERE MaBan = @MaBan;", connection);
        command.Parameters.AddWithValue("@TrangThai", trangThai);
        command.Parameters.AddWithValue("@MaBan", maBan);
        connection.Open();
        return command.ExecuteNonQuery() == 1;
    }
}
