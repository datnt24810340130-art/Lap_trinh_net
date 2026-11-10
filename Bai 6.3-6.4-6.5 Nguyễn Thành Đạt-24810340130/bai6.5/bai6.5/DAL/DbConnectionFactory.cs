using Microsoft.Data.SqlClient;
using System.Text.Json;

namespace bai6._5.DAL;

internal static class DbConnectionFactory
{
    public static SqlConnection CreateConnection()
    {
        var configuredConnectionString = Environment.GetEnvironmentVariable("KARAOKE_DB_CONNECTION");
        if (string.IsNullOrWhiteSpace(configuredConnectionString))
        {
            var settingsPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
            if (File.Exists(settingsPath))
            {
                using var document = JsonDocument.Parse(File.ReadAllText(settingsPath));
                if (document.RootElement.TryGetProperty("ConnectionStrings", out var connections)
                    && connections.TryGetProperty("KaraokeDB", out var connectionStringElement))
                {
                    configuredConnectionString = connectionStringElement.GetString();
                }
            }
        }

        configuredConnectionString ??= "Server=LAPTOP-UNQQA3HP;Database=KaraokeDB;Integrated Security=True;TrustServerCertificate=True";
        return new SqlConnection(configuredConnectionString);
    }
}
