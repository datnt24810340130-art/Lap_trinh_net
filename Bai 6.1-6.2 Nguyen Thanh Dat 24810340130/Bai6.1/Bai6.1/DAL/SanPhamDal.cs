using Bai6._1.DTO;
using Microsoft.Data.SqlClient;

namespace Bai6._1.DAL;

public sealed class SanPhamDal
{
    public List<SanPhamDto> GetAll()
    {
        const string sql = "SELECT MaSP, TenSP, GiaBan, SoLuongTon FROM dbo.SanPham ORDER BY TenSP";
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand(sql, connection);
        connection.Open();
        using var reader = command.ExecuteReader();
        var products = new List<SanPhamDto>();
        while (reader.Read())
            products.Add(ReadProduct(reader));
        return products;
    }

    public List<SanPhamDto> Search(string keyword)
    {
        const string sql = "SELECT MaSP, TenSP, GiaBan, SoLuongTon FROM dbo.SanPham WHERE MaSP LIKE @keyword OR TenSP LIKE @keyword ORDER BY TenSP";
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@keyword", System.Data.SqlDbType.NVarChar, 172).Value = $"%{keyword}%";
        connection.Open();
        using var reader = command.ExecuteReader();
        var products = new List<SanPhamDto>();
        while (reader.Read())
            products.Add(ReadProduct(reader));
        return products;
    }

    public void Add(SanPhamDto product)
    {
        const string sql = "INSERT INTO dbo.SanPham (MaSP, TenSP, GiaBan, SoLuongTon) VALUES (@ma, @ten, @gia, @ton)";
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand(sql, connection);
        AddProductParameters(command, product);
        connection.Open();
        command.ExecuteNonQuery();
    }

    public bool Update(SanPhamDto product)
    {
        const string sql = "UPDATE dbo.SanPham SET TenSP=@ten, GiaBan=@gia, SoLuongTon=@ton WHERE MaSP=@ma";
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand(sql, connection);
        AddProductParameters(command, product);
        connection.Open();
        return command.ExecuteNonQuery() == 1;
    }

    public bool Delete(string maSP)
    {
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand("DELETE FROM dbo.SanPham WHERE MaSP=@ma", connection);
        command.Parameters.Add("@ma", System.Data.SqlDbType.VarChar, 20).Value = maSP;
        connection.Open();
        return command.ExecuteNonQuery() == 1;
    }

    private static void AddProductParameters(SqlCommand command, SanPhamDto product)
    {
        command.Parameters.Add("@ma", System.Data.SqlDbType.VarChar, 20).Value = product.MaSP;
        command.Parameters.Add("@ten", System.Data.SqlDbType.NVarChar, 150).Value = product.TenSP;
        command.Parameters.Add("@gia", System.Data.SqlDbType.Decimal).Value = product.GiaBan;
        command.Parameters["@gia"].Precision = 18;
        command.Parameters["@gia"].Scale = 2;
        command.Parameters.Add("@ton", System.Data.SqlDbType.Int).Value = product.SoLuongTon;
    }

    private static SanPhamDto ReadProduct(SqlDataReader reader) => new()
    {
        MaSP = reader.GetString(0),
        TenSP = reader.GetString(1),
        GiaBan = reader.GetDecimal(2),
        SoLuongTon = reader.GetInt32(3)
    };
}
