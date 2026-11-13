using bai6._6.DTO;
using Microsoft.Data.SqlClient;

namespace bai6._6.DAL;

public sealed class DauSachDAL
{
    public List<DauSachDTO> GetAll()
    {
        const string sql = "SELECT MaSach, TenSach, TacGia, TheLoai, NhaXuatBan, NamXuatBan, SoLuong, SoLuongCon FROM dbo.DauSach ORDER BY TenSach";
        using SqlConnection connection = DatabaseHelper.GetConnection();
        using SqlCommand command = new(sql, connection);
        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();
        var books = new List<DauSachDTO>();
        while (reader.Read())
        {
            books.Add(Map(reader));
        }

        return books;
    }

    public int Insert(DauSachDTO book)
    {
        const string sql = "INSERT dbo.DauSach (TenSach, TacGia, TheLoai, NhaXuatBan, NamXuatBan, SoLuong, SoLuongCon) VALUES (@TenSach, @TacGia, @TheLoai, @NhaXuatBan, @NamXuatBan, @SoLuong, @SoLuongCon); SELECT CAST(SCOPE_IDENTITY() AS int);";
        using SqlConnection connection = DatabaseHelper.GetConnection();
        using SqlCommand command = new(sql, connection);
        AddBookParameters(command, book);
        connection.Open();
        return (int)command.ExecuteScalar()!;
    }

    public bool Update(DauSachDTO book)
    {
        const string sql = "UPDATE dbo.DauSach SET TenSach=@TenSach, TacGia=@TacGia, TheLoai=@TheLoai, NhaXuatBan=@NhaXuatBan, NamXuatBan=@NamXuatBan, SoLuong=@SoLuong, SoLuongCon=@SoLuongCon WHERE MaSach=@MaSach";
        using SqlConnection connection = DatabaseHelper.GetConnection();
        using SqlCommand command = new(sql, connection);
        AddBookParameters(command, book);
        command.Parameters.AddWithValue("@MaSach", book.MaSach);
        connection.Open();
        return command.ExecuteNonQuery() == 1;
    }

    public bool Delete(int maSach)
    {
        using SqlConnection connection = DatabaseHelper.GetConnection();
        using SqlCommand command = new("DELETE dbo.DauSach WHERE MaSach=@MaSach", connection);
        command.Parameters.AddWithValue("@MaSach", maSach);
        connection.Open();
        return command.ExecuteNonQuery() == 1;
    }

    public List<TheLoaiThongKeDTO> GetStatisticsByGenre()
    {
        const string sql = "SELECT TheLoai, SUM(SoLuong) AS TongSoLuong, SUM(SoLuongCon) AS TongSoLuongCon FROM dbo.DauSach GROUP BY TheLoai ORDER BY TheLoai";
        using SqlConnection connection = DatabaseHelper.GetConnection();
        using SqlCommand command = new(sql, connection);
        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();
        var statistics = new List<TheLoaiThongKeDTO>();
        while (reader.Read())
        {
            statistics.Add(new TheLoaiThongKeDTO
            {
                TheLoai = reader.GetString(0),
                TongSoLuong = reader.GetInt32(1),
                TongSoLuongCon = reader.GetInt32(2)
            });
        }

        return statistics;
    }

    private static void AddBookParameters(SqlCommand command, DauSachDTO book)
    {
        command.Parameters.AddWithValue("@TenSach", book.TenSach);
        command.Parameters.AddWithValue("@TacGia", (object?)book.TacGia ?? DBNull.Value);
        command.Parameters.AddWithValue("@TheLoai", book.TheLoai);
        command.Parameters.AddWithValue("@NhaXuatBan", (object?)book.NhaXuatBan ?? DBNull.Value);
        command.Parameters.AddWithValue("@NamXuatBan", book.NamXuatBan.HasValue ? book.NamXuatBan.Value : DBNull.Value);
        command.Parameters.AddWithValue("@SoLuong", book.SoLuong);
        command.Parameters.AddWithValue("@SoLuongCon", book.SoLuongCon);
    }

    private static DauSachDTO Map(SqlDataReader reader) => new()
    {
        MaSach = reader.GetInt32(0),
        TenSach = reader.GetString(1),
        TacGia = reader.IsDBNull(2) ? null : reader.GetString(2),
        TheLoai = reader.GetString(3),
        NhaXuatBan = reader.IsDBNull(4) ? null : reader.GetString(4),
        NamXuatBan = reader.IsDBNull(5) ? null : reader.GetInt32(5),
        SoLuong = reader.GetInt32(6),
        SoLuongCon = reader.GetInt32(7)
    };
}
