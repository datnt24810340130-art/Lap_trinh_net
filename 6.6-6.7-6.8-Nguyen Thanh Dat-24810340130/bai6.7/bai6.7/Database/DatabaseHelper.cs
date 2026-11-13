using Microsoft.Data.SqlClient;
using System.Configuration;

namespace bai6._7.Database;

public static class DatabaseHelper
{
    public static SqlConnection GetConnection()
    {
        string? connectionString = ConfigurationManager.ConnectionStrings["ClinicDB"]?.ConnectionString;
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Không tìm thấy chuỗi kết nối ClinicDB trong App.config.");
        }

        return new SqlConnection(connectionString);
    }
}
