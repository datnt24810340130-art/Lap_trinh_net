using System.Data;
using bai5._5.DTO;
using Microsoft.Data.SqlClient;

namespace bai5._5.DAL;

public sealed class MaterialDal(DbConnectionFactory connectionFactory)
{
    public List<MaterialDto> GetAll(string? keyword = null)
    {
        const string sql = """
            SELECT v.MaVatTu, v.TenVatTu, v.DVT, v.SoLuongTon, d.SoLuongToiThieu
            FROM dbo.VatTu AS v
            INNER JOIN dbo.DinhMucTonKho AS d ON d.MaVatTu = v.MaVatTu
            WHERE @Keyword IS NULL OR v.MaVatTu LIKE @Keyword OR v.TenVatTu LIKE @Keyword
            ORDER BY v.MaVatTu;
            """;
        using var connection = connectionFactory.CreateConnection();
        using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@Keyword", SqlDbType.NVarChar, 150).Value =
            string.IsNullOrWhiteSpace(keyword) ? DBNull.Value : $"%{keyword.Trim()}%";
        connection.Open();
        using var reader = command.ExecuteReader();
        var materials = new List<MaterialDto>();
        while (reader.Read())
        {
            materials.Add(new MaterialDto
            {
                MaVatTu = reader.GetString(0),
                TenVatTu = reader.GetString(1),
                DVT = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                SoLuongTon = reader.GetDecimal(3),
                SoLuongToiThieu = reader.GetDecimal(4)
            });
        }
        return materials;
    }

    public void Add(MaterialDto material)
    {
        const string sql = """
            INSERT INTO dbo.VatTu (MaVatTu, TenVatTu, DVT, SoLuongTon)
            VALUES (@MaVatTu, @TenVatTu, @DVT, 0);
            INSERT INTO dbo.DinhMucTonKho (MaVatTu, SoLuongToiThieu)
            VALUES (@MaVatTu, @SoLuongToiThieu);
            """;
        using var connection = connectionFactory.CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction();
        try
        {
            using var command = new SqlCommand(sql, connection, transaction);
            AddMaterialParameters(command, material);
            command.Parameters.Add("@SoLuongToiThieu", SqlDbType.Decimal).Value = material.SoLuongToiThieu;
            command.Parameters["@SoLuongToiThieu"].Precision = 18;
            command.Parameters["@SoLuongToiThieu"].Scale = 2;
            command.ExecuteNonQuery();
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public bool Update(MaterialDto material)
    {
        const string sql = """
            UPDATE dbo.VatTu SET TenVatTu = @TenVatTu, DVT = @DVT WHERE MaVatTu = @MaVatTu;
            IF @@ROWCOUNT = 0 RETURN;
            UPDATE dbo.DinhMucTonKho SET SoLuongToiThieu = @SoLuongToiThieu WHERE MaVatTu = @MaVatTu;
            """;
        using var connection = connectionFactory.CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction();
        try
        {
            using var command = new SqlCommand(sql, connection, transaction);
            AddMaterialParameters(command, material);
            command.Parameters.Add("@SoLuongToiThieu", SqlDbType.Decimal).Value = material.SoLuongToiThieu;
            command.Parameters["@SoLuongToiThieu"].Precision = 18;
            command.Parameters["@SoLuongToiThieu"].Scale = 2;
            var updated = command.ExecuteNonQuery() > 0;
            transaction.Commit();
            return updated;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public bool Delete(string maVatTu)
    {
        const string sql = "DELETE FROM dbo.VatTu WHERE MaVatTu = @MaVatTu;";
        using var connection = connectionFactory.CreateConnection();
        using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@MaVatTu", SqlDbType.VarChar, 20).Value = maVatTu;
        connection.Open();
        return command.ExecuteNonQuery() > 0;
    }

    public List<MaterialDto> GetBelowMinimum()
    {
        const string sql = """
            SELECT v.MaVatTu, v.TenVatTu, v.DVT, v.SoLuongTon, d.SoLuongToiThieu
            FROM dbo.VatTu AS v
            INNER JOIN dbo.DinhMucTonKho AS d ON d.MaVatTu = v.MaVatTu
            WHERE v.SoLuongTon < d.SoLuongToiThieu
            ORDER BY v.SoLuongTon;
            """;
        using var connection = connectionFactory.CreateConnection();
        using var command = new SqlCommand(sql, connection);
        connection.Open();
        using var reader = command.ExecuteReader();
        var materials = new List<MaterialDto>();
        while (reader.Read())
        {
            materials.Add(new MaterialDto
            {
                MaVatTu = reader.GetString(0),
                TenVatTu = reader.GetString(1),
                DVT = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                SoLuongTon = reader.GetDecimal(3),
                SoLuongToiThieu = reader.GetDecimal(4)
            });
        }
        return materials;
    }

    public decimal GetInventoryValue()
    {
        const string sql = """
            SELECT COALESCE(SUM(v.SoLuongTon * ISNULL(g.DonGiaNhap, 0)), 0)
            FROM dbo.VatTu AS v
            OUTER APPLY (
                SELECT TOP (1) ct.DonGia AS DonGiaNhap
                FROM dbo.ChiTietPhieuKho AS ct
                INNER JOIN dbo.PhieuKho AS p ON p.MaPhieu = ct.MaPhieu
                WHERE ct.MaVatTu = v.MaVatTu AND p.LoaiPhieu = N'NHAP'
                ORDER BY p.NgayLap DESC, p.MaPhieu DESC
            ) AS g;
            """;
        using var connection = connectionFactory.CreateConnection();
        using var command = new SqlCommand(sql, connection);
        connection.Open();
        return Convert.ToDecimal(command.ExecuteScalar());
    }

    private static void AddMaterialParameters(SqlCommand command, MaterialDto material)
    {
        command.Parameters.Add("@MaVatTu", SqlDbType.VarChar, 20).Value = material.MaVatTu;
        command.Parameters.Add("@TenVatTu", SqlDbType.NVarChar, 150).Value = material.TenVatTu;
        command.Parameters.Add("@DVT", SqlDbType.NVarChar, 20).Value =
            string.IsNullOrWhiteSpace(material.DVT) ? DBNull.Value : material.DVT;
    }
}
