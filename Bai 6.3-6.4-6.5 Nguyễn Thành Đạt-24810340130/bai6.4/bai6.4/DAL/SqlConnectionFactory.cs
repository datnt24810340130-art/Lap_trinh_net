using System.Text.Json;
using Microsoft.Data.SqlClient;

namespace bai6._4.DAL;

public sealed class SqlConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory(string? configPath = null)
    {
        var path = configPath ?? Path.Combine(AppContext.BaseDirectory, "appsettings.json");
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"Không tìm thấy file cấu hình kết nối: {path}", path);
        }

        using var document = JsonDocument.Parse(File.ReadAllText(path));
        _connectionString = document.RootElement
            .GetProperty("ConnectionStrings")
            .GetProperty("GarageDB")
            .GetString() ?? throw new InvalidOperationException("ConnectionStrings:GarageDB chưa được cấu hình.");
    }

    public SqlConnection CreateConnection() => new(_connectionString);
}
