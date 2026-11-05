using Bai6._1.DTO;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Bai6._1.DAL;

public sealed class NhanVienDal
{
    public List<NhanVienDto> GetAll()
    {
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand("SELECT MaNV, TenNV, SoDienThoai, ChucVu FROM dbo.NhanVien ORDER BY TenNV", connection);
        connection.Open();
        using var reader = command.ExecuteReader();
        var employees = new List<NhanVienDto>();
        while (reader.Read())
            employees.Add(ReadEmployee(reader));
        return employees;
    }

    public void Add(NhanVienDto employee)
    {
        const string sql = "INSERT INTO dbo.NhanVien (TenNV, SoDienThoai, ChucVu) VALUES (@ten, @dienThoai, @chucVu)";
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand(sql, connection);
        AddEmployeeParameters(command, employee);
        connection.Open();
        command.ExecuteNonQuery();
    }

    public bool Update(NhanVienDto employee)
    {
        const string sql = "UPDATE dbo.NhanVien SET TenNV=@ten, SoDienThoai=@dienThoai, ChucVu=@chucVu WHERE MaNV=@ma";
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand(sql, connection);
        AddEmployeeParameters(command, employee);
        command.Parameters.Add("@ma", SqlDbType.Int).Value = employee.MaNV;
        connection.Open();
        return command.ExecuteNonQuery() == 1;
    }

    public bool Delete(int maNV)
    {
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand("DELETE FROM dbo.NhanVien WHERE MaNV=@ma", connection);
        command.Parameters.Add("@ma", SqlDbType.Int).Value = maNV;
        connection.Open();
        return command.ExecuteNonQuery() == 1;
    }

    private static void AddEmployeeParameters(SqlCommand command, NhanVienDto employee)
    {
        command.Parameters.Add("@ten", SqlDbType.NVarChar, 100).Value = employee.TenNV;
        command.Parameters.Add("@dienThoai", SqlDbType.VarChar, 15).Value = (object?)employee.SoDienThoai ?? DBNull.Value;
        command.Parameters.Add("@chucVu", SqlDbType.NVarChar, 50).Value = (object?)employee.ChucVu ?? DBNull.Value;
    }

    private static NhanVienDto ReadEmployee(SqlDataReader reader) => new()
    {
        MaNV = reader.GetInt32(0),
        TenNV = reader.GetString(1),
        SoDienThoai = reader.IsDBNull(2) ? null : reader.GetString(2),
        ChucVu = reader.IsDBNull(3) ? null : reader.GetString(3)
    };
}
