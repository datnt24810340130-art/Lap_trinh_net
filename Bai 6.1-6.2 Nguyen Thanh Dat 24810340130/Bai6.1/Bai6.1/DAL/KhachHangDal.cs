using Bai6._1.DTO;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Bai6._1.DAL;

public sealed class KhachHangDal
{
    public List<KhachHangDto> GetAll()
    {
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand("SELECT MaKH, TenKH, DienThoai, DiemTichLuy FROM dbo.KhachHang ORDER BY TenKH", connection);
        connection.Open();
        using var reader = command.ExecuteReader();
        var customers = new List<KhachHangDto>();
        while (reader.Read())
            customers.Add(ReadCustomer(reader));
        return customers;
    }

    public void Add(KhachHangDto customer)
    {
        const string sql = "INSERT INTO dbo.KhachHang (TenKH, DienThoai, DiemTichLuy) VALUES (@ten, @dienThoai, @diem)";
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand(sql, connection);
        AddCustomerParameters(command, customer);
        connection.Open();
        command.ExecuteNonQuery();
    }

    public bool Update(KhachHangDto customer)
    {
        const string sql = "UPDATE dbo.KhachHang SET TenKH=@ten, DienThoai=@dienThoai, DiemTichLuy=@diem WHERE MaKH=@ma";
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand(sql, connection);
        AddCustomerParameters(command, customer);
        command.Parameters.Add("@ma", SqlDbType.Int).Value = customer.MaKH;
        connection.Open();
        return command.ExecuteNonQuery() == 1;
    }

    public bool Delete(int maKH)
    {
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand("DELETE FROM dbo.KhachHang WHERE MaKH=@ma", connection);
        command.Parameters.Add("@ma", SqlDbType.Int).Value = maKH;
        connection.Open();
        return command.ExecuteNonQuery() == 1;
    }

    private static void AddCustomerParameters(SqlCommand command, KhachHangDto customer)
    {
        command.Parameters.Add("@ten", SqlDbType.NVarChar, 100).Value = customer.TenKH;
        command.Parameters.Add("@dienThoai", SqlDbType.VarChar, 15).Value = (object?)customer.DienThoai ?? DBNull.Value;
        command.Parameters.Add("@diem", SqlDbType.Int).Value = customer.DiemTichLuy;
    }

    private static KhachHangDto ReadCustomer(SqlDataReader reader) => new()
    {
        MaKH = reader.GetInt32(0),
        TenKH = reader.GetString(1),
        DienThoai = reader.IsDBNull(2) ? null : reader.GetString(2),
        DiemTichLuy = reader.GetInt32(3)
    };
}
