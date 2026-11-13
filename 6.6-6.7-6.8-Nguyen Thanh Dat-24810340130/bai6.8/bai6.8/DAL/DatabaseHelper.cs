using System.Configuration;
using Microsoft.Data.SqlClient;

namespace bai6._8.DAL;

public static class DatabaseHelper
{
    public static string ConnectionString => ConfigurationManager.ConnectionStrings["BilliardsDB"]?.ConnectionString
        ?? throw new InvalidOperationException("Không tìm thấy chuỗi kết nối BilliardsDB trong App.config.");

    public static SqlConnection CreateConnection() => new(ConnectionString);
}
