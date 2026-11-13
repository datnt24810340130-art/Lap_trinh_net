using System.Configuration;
using Microsoft.Data.SqlClient;

namespace bai6._6.DAL;

public static class DatabaseHelper
{
    public static SqlConnection GetConnection()
    {
        string? connectionString = ConfigurationManager.ConnectionStrings["LibraryDB"]?.ConnectionString;
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Connection string 'LibraryDB' is missing from App.config.");
        }

        return new SqlConnection(connectionString);
    }
}
