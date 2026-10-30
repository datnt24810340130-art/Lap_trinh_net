using System.Data;
using bai5._5.DTO;
using Microsoft.Data.SqlClient;

namespace bai5._5.DAL;

public sealed class SupplierDal(DbConnectionFactory connectionFactory)
{
    public List<SupplierDto> GetAll(string? keyword = null)
    {
        const string sql = """
            SELECT MaNCC, TenNCC, DienThoai, DiaChi
            FROM dbo.NhaCungCap
            WHERE @Keyword IS NULL OR TenNCC LIKE @Keyword OR DienThoai LIKE @Keyword
            ORDER BY TenNCC;
            """;
        using var connection = connectionFactory.CreateConnection();
        using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@Keyword", SqlDbType.NVarChar, 150).Value =
            string.IsNullOrWhiteSpace(keyword) ? DBNull.Value : $"%{keyword.Trim()}%";
        connection.Open();
        using var reader = command.ExecuteReader();
        var suppliers = new List<SupplierDto>();
        while (reader.Read())
        {
            suppliers.Add(new SupplierDto
            {
                MaNCC = reader.GetInt32(0),
                TenNCC = reader.GetString(1),
                DienThoai = reader.IsDBNull(2) ? null : reader.GetString(2),
                DiaChi = reader.IsDBNull(3) ? null : reader.GetString(3)
            });
        }
        return suppliers;
    }

    public int Add(SupplierDto supplier)
    {
        const string sql = """
            INSERT INTO dbo.NhaCungCap (TenNCC, DienThoai, DiaChi)
            OUTPUT INSERTED.MaNCC
            VALUES (@TenNCC, @DienThoai, @DiaChi);
            """;
        using var connection = connectionFactory.CreateConnection();
        using var command = new SqlCommand(sql, connection);
        AddSupplierParameters(command, supplier);
        connection.Open();
        return Convert.ToInt32(command.ExecuteScalar());
    }

    public bool Update(SupplierDto supplier)
    {
        const string sql = """
            UPDATE dbo.NhaCungCap SET TenNCC = @TenNCC, DienThoai = @DienThoai, DiaChi = @DiaChi
            WHERE MaNCC = @MaNCC;
            """;
        using var connection = connectionFactory.CreateConnection();
        using var command = new SqlCommand(sql, connection);
        AddSupplierParameters(command, supplier);
        command.Parameters.Add("@MaNCC", SqlDbType.Int).Value = supplier.MaNCC;
        connection.Open();
        return command.ExecuteNonQuery() > 0;
    }

    public bool Delete(int maNcc)
    {
        const string sql = "DELETE FROM dbo.NhaCungCap WHERE MaNCC = @MaNCC;";
        using var connection = connectionFactory.CreateConnection();
        using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@MaNCC", SqlDbType.Int).Value = maNcc;
        connection.Open();
        return command.ExecuteNonQuery() > 0;
    }

    private static void AddSupplierParameters(SqlCommand command, SupplierDto supplier)
    {
        command.Parameters.Add("@TenNCC", SqlDbType.NVarChar, 150).Value = supplier.TenNCC;
        command.Parameters.Add("@DienThoai", SqlDbType.VarChar, 15).Value =
            string.IsNullOrWhiteSpace(supplier.DienThoai) ? DBNull.Value : supplier.DienThoai;
        command.Parameters.Add("@DiaChi", SqlDbType.NVarChar, 255).Value =
            string.IsNullOrWhiteSpace(supplier.DiaChi) ? DBNull.Value : supplier.DiaChi;
    }
}
