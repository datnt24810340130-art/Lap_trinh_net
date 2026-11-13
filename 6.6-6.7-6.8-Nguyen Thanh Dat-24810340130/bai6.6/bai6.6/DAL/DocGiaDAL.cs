using bai6._6.DTO;
using Microsoft.Data.SqlClient;

namespace bai6._6.DAL;

public sealed class DocGiaDAL
{
    public List<DocGiaDTO> GetAll()
    {
        const string sql = "SELECT MaDocGia, HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai, Email, NgayHetHanThe FROM dbo.DocGia ORDER BY HoTen";
        using SqlConnection connection = DatabaseHelper.GetConnection();
        using SqlCommand command = new(sql, connection);
        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();
        var readers = new List<DocGiaDTO>();
        while (reader.Read())
        {
            readers.Add(Map(reader));
        }

        return readers;
    }

    public DocGiaDTO? GetById(int maDocGia)
    {
        const string sql = "SELECT MaDocGia, HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai, Email, NgayHetHanThe FROM dbo.DocGia WHERE MaDocGia = @MaDocGia";
        using SqlConnection connection = DatabaseHelper.GetConnection();
        using SqlCommand command = new(sql, connection);
        command.Parameters.AddWithValue("@MaDocGia", maDocGia);
        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();
        return reader.Read() ? Map(reader) : null;
    }

    public int Insert(DocGiaDTO reader)
    {
        const string sql = "INSERT dbo.DocGia (HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai, Email, NgayHetHanThe) VALUES (@HoTen, @NgaySinh, @GioiTinh, @DiaChi, @SoDienThoai, @Email, @NgayHetHanThe); SELECT CAST(SCOPE_IDENTITY() AS int);";
        using SqlConnection connection = DatabaseHelper.GetConnection();
        using SqlCommand command = new(sql, connection);
        AddReaderParameters(command, reader);
        connection.Open();
        return (int)command.ExecuteScalar()!;
    }

    public bool Update(DocGiaDTO reader)
    {
        const string sql = "UPDATE dbo.DocGia SET HoTen=@HoTen, NgaySinh=@NgaySinh, GioiTinh=@GioiTinh, DiaChi=@DiaChi, SoDienThoai=@SoDienThoai, Email=@Email, NgayHetHanThe=@NgayHetHanThe WHERE MaDocGia=@MaDocGia";
        using SqlConnection connection = DatabaseHelper.GetConnection();
        using SqlCommand command = new(sql, connection);
        AddReaderParameters(command, reader);
        command.Parameters.AddWithValue("@MaDocGia", reader.MaDocGia);
        connection.Open();
        return command.ExecuteNonQuery() == 1;
    }

    public bool Delete(int maDocGia)
    {
        using SqlConnection connection = DatabaseHelper.GetConnection();
        using SqlCommand command = new("DELETE dbo.DocGia WHERE MaDocGia=@MaDocGia", connection);
        command.Parameters.AddWithValue("@MaDocGia", maDocGia);
        connection.Open();
        return command.ExecuteNonQuery() == 1;
    }

    private static void AddReaderParameters(SqlCommand command, DocGiaDTO reader)
    {
        command.Parameters.AddWithValue("@HoTen", reader.HoTen);
        command.Parameters.AddWithValue("@NgaySinh", reader.NgaySinh.HasValue ? reader.NgaySinh.Value.Date : DBNull.Value);
        command.Parameters.AddWithValue("@GioiTinh", (object?)reader.GioiTinh ?? DBNull.Value);
        command.Parameters.AddWithValue("@DiaChi", (object?)reader.DiaChi ?? DBNull.Value);
        command.Parameters.AddWithValue("@SoDienThoai", (object?)reader.SoDienThoai ?? DBNull.Value);
        command.Parameters.AddWithValue("@Email", (object?)reader.Email ?? DBNull.Value);
        command.Parameters.AddWithValue("@NgayHetHanThe", reader.NgayHetHanThe.Date);
    }

    private static DocGiaDTO Map(SqlDataReader reader) => new()
    {
        MaDocGia = reader.GetInt32(0),
        HoTen = reader.GetString(1),
        NgaySinh = reader.IsDBNull(2) ? null : reader.GetDateTime(2),
        GioiTinh = reader.IsDBNull(3) ? null : reader.GetString(3),
        DiaChi = reader.IsDBNull(4) ? null : reader.GetString(4),
        SoDienThoai = reader.IsDBNull(5) ? null : reader.GetString(5),
        Email = reader.IsDBNull(6) ? null : reader.GetString(6),
        NgayHetHanThe = reader.GetDateTime(7)
    };
}
