using System.Configuration;
using Microsoft.Data.SqlClient;

namespace bai5._5.DAL;

public sealed class DbConnectionFactory
{
    private readonly string _connectionString;

    public DbConnectionFactory()
    {
        _connectionString = ConfigurationManager.ConnectionStrings["InventoryDB"]?.ConnectionString
            ?? throw new InvalidOperationException("Thiếu connection string InventoryDB trong App.config.");
    }

    public SqlConnection CreateConnection() => new(_connectionString);
}
