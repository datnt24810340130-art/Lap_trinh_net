using System.Configuration;
using Microsoft.Data.SqlClient;

namespace bai6._2.DAL;

internal static class DbConnectionFactory
{
    public static SqlConnection CreateConnection()
    {
        string? connectionString = ConfigurationManager.ConnectionStrings["HRMDB"]?.ConnectionString;
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Missing the HRMDB connection string in App.config.");
        }

        return new SqlConnection(connectionString);
    }
}
