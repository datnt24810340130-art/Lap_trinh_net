using System.Configuration;
using Microsoft.Data.SqlClient;

namespace bai5._3.DAL;

internal static class Database
{
    public static SqlConnection CreateConnection()
    {
        string? connectionString = ConfigurationManager.ConnectionStrings["CafePOSDB"]?.ConnectionString;
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Không tìm thấy chuỗi kết nối CafePOSDB trong App.config.");
        }

        return new SqlConnection(connectionString);
    }
}