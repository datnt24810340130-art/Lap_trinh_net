using bai6._7.Database;
using bai6._7.DTO;
using Microsoft.Data.SqlClient;
using System.Data;

namespace bai6._7.DAL;

public sealed class ThuocDAL
{
    public List<ThuocDTO> LayDanhSach(string? tuKhoa = null)
    {
        List<ThuocDTO> medicines = [];
        using SqlConnection connection = DatabaseHelper.GetConnection();
        using SqlCommand command = new(
            "SELECT MaThuoc, TenThuoc, DonViTinh, DonGia, SoLuongTon FROM dbo.Thuoc WHERE @TuKhoa = N'' OR TenThuoc LIKE @Mau OR DonViTinh LIKE @Mau ORDER BY TenThuoc;",
            connection);
        string keyword = tuKhoa?.Trim() ?? string.Empty;
        command.Parameters.Add("@TuKhoa", SqlDbType.NVarChar, 300).Value = keyword;
        command.Parameters.Add("@Mau", SqlDbType.NVarChar, 302).Value = $"%{keyword}%";
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

    public int Them(ThuocDTO thuoc)
    {
        using SqlConnection connection = DatabaseHelper.GetConnection();
        using SqlCommand command = new(
            "INSERT dbo.Thuoc (TenThuoc, DonViTinh, DonGia, SoLuongTon) VALUES (@TenThuoc, @DonViTinh, @DonGia, @SoLuongTon); SELECT CONVERT(int, SCOPE_IDENTITY());",
            connection);
        AddParameters(command, thuoc);
        connection.Open();
        return (int)command.ExecuteScalar()!;
    }

    public void CapNhat(ThuocDTO thuoc)
    {
        using SqlConnection connection = DatabaseHelper.GetConnection();
        using SqlCommand command = new(
            "UPDATE dbo.Thuoc SET TenThuoc = @TenThuoc, DonViTinh = @DonViTinh, DonGia = @DonGia, SoLuongTon = @SoLuongTon WHERE MaThuoc = @MaThuoc;",
            connection);
        AddParameters(command, thuoc);
        command.Parameters.Add("@MaThuoc", SqlDbType.Int).Value = thuoc.MaThuoc;
        connection.Open();
        if (command.ExecuteNonQuery() == 0)
        {
            throw new InvalidOperationException("Không tìm thấy thuốc cần cập nhật.");
        }
    }

    public void Xoa(int maThuoc)
    {
        using SqlConnection connection = DatabaseHelper.GetConnection();
        using SqlCommand command = new("DELETE dbo.Thuoc WHERE MaThuoc = @MaThuoc;", connection);
        command.Parameters.Add("@MaThuoc", SqlDbType.Int).Value = maThuoc;
        connection.Open();
        try
        {
            if (command.ExecuteNonQuery() == 0)
            {
                throw new InvalidOperationException("Không tìm thấy thuốc cần xóa.");
            }
        }
        catch (SqlException ex) when (ex.Number == 547)
        {
            throw new InvalidOperationException("Không thể xóa thuốc đã xuất hiện trong chi tiết đơn thuốc.", ex);
        }
    }

    private static void AddParameters(SqlCommand command, ThuocDTO thuoc)
    {
        command.Parameters.Add("@TenThuoc", SqlDbType.NVarChar, 150).Value = thuoc.TenThuoc;
        command.Parameters.Add("@DonViTinh", SqlDbType.NVarChar, 30).Value = thuoc.DonViTinh;
        SqlParameter price = command.Parameters.Add("@DonGia", SqlDbType.Decimal);
        price.Precision = 18;
        price.Scale = 2;
        price.Value = thuoc.DonGia;
        command.Parameters.Add("@SoLuongTon", SqlDbType.Int).Value = thuoc.SoLuongTon;
    }
}
