using Microsoft.Data.SqlClient;
using System.Configuration;

namespace Bai6._1.DAL;

internal static class DbConnectionFactory
{
    public static SqlConnection CreateConnection()
    {
        var connectionString = ConfigurationManager.ConnectionStrings["SalesDB"]?.ConnectionString;
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "The SalesDB connection string is missing. Configure it in the application configuration file.");
        }

        return new SqlConnection(connectionString);
    }
}
