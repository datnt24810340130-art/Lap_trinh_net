using bai6._2.DTO;
using Microsoft.Data.SqlClient;

namespace bai6._2.DAL;

public sealed class EmployeeRepository
{
    public List<DepartmentDto> GetDepartments()
    {
        const string sql = "SELECT MaPB, TenPB, Mota FROM dbo.PhongBan ORDER BY TenPB;";
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand(sql, connection);
        connection.Open();
        using var reader = command.ExecuteReader();

        var departments = new List<DepartmentDto>();
        while (reader.Read())
        {
            departments.Add(new DepartmentDto
            {
                MaPB = reader.GetInt32(0),
                TenPB = reader.GetString(1),
                Mota = reader.IsDBNull(2) ? null : reader.GetString(2)
            });
        }

        return departments;
    }

    public List<EmployeeDto> SearchEmployees(string? name, int? departmentId, string? nationalId)
    {
        const string sql = """
            SELECT MaNV, HoTen, NgaySinh, CCCD, MaPB, LuongCoBan, HeSoLuong
            FROM dbo.NhanVien
            WHERE (@Name IS NULL OR HoTen LIKE @Name)
              AND (@DepartmentId IS NULL OR MaPB = @DepartmentId)
              AND (@NationalId IS NULL OR CCCD = @NationalId)
            ORDER BY HoTen;
            """;

        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar, 102)
            .Value = string.IsNullOrWhiteSpace(name) ? DBNull.Value : $"%{name.Trim()}%";
        command.Parameters.Add("@DepartmentId", System.Data.SqlDbType.Int)
            .Value = departmentId.HasValue ? departmentId.Value : DBNull.Value;
        command.Parameters.Add("@NationalId", System.Data.SqlDbType.VarChar, 20)
            .Value = string.IsNullOrWhiteSpace(nationalId) ? DBNull.Value : nationalId.Trim();

        connection.Open();
        using var reader = command.ExecuteReader();
        var employees = new List<EmployeeDto>();
        while (reader.Read())
        {
            employees.Add(ReadEmployee(reader));
        }

        return employees;
    }

    public int Insert(EmployeeDto employee)
    {
        const string sql = """
            INSERT INTO dbo.NhanVien (HoTen, NgaySinh, CCCD, MaPB, LuongCoBan, HeSoLuong)
            VALUES (@HoTen, @NgaySinh, @CCCD, @MaPB, @LuongCoBan, @HeSoLuong);
            SELECT CAST(SCOPE_IDENTITY() AS int);
            """;
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand(sql, connection);
        AddEmployeeParameters(command, employee);
        connection.Open();
        return (int)command.ExecuteScalar()!;
    }

    public void Update(EmployeeDto employee)
    {
        const string sql = """
            UPDATE dbo.NhanVien
            SET HoTen = @HoTen, NgaySinh = @NgaySinh, CCCD = @CCCD,
                MaPB = @MaPB, LuongCoBan = @LuongCoBan, HeSoLuong = @HeSoLuong
            WHERE MaNV = @MaNV;
            """;
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand(sql, connection);
        AddEmployeeParameters(command, employee);
        command.Parameters.Add("@MaNV", System.Data.SqlDbType.Int).Value = employee.MaNV;
        connection.Open();
        if (command.ExecuteNonQuery() == 0)
        {
            throw new InvalidOperationException("The employee no longer exists.");
        }
    }

    public void Delete(int employeeId)
    {
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand("DELETE FROM dbo.NhanVien WHERE MaNV = @MaNV;", connection);
        command.Parameters.Add("@MaNV", System.Data.SqlDbType.Int).Value = employeeId;
        connection.Open();
        if (command.ExecuteNonQuery() == 0)
        {
            throw new InvalidOperationException("The employee no longer exists.");
        }
    }

    private static void AddEmployeeParameters(SqlCommand command, EmployeeDto employee)
    {
        command.Parameters.Add("@HoTen", System.Data.SqlDbType.NVarChar, 100).Value = employee.HoTen.Trim();
        command.Parameters.Add("@NgaySinh", System.Data.SqlDbType.DateTime).Value = (object?)employee.NgaySinh ?? DBNull.Value;
        command.Parameters.Add("@CCCD", System.Data.SqlDbType.VarChar, 20).Value = (object?)employee.CCCD ?? DBNull.Value;
        command.Parameters.Add("@MaPB", System.Data.SqlDbType.Int).Value = (object?)employee.MaPB ?? DBNull.Value;
        command.Parameters.Add("@LuongCoBan", System.Data.SqlDbType.Decimal).Value = employee.LuongCoBan;
        command.Parameters["@LuongCoBan"].Precision = 18;
        command.Parameters["@LuongCoBan"].Scale = 2;
        command.Parameters.Add("@HeSoLuong", System.Data.SqlDbType.Float).Value = employee.HeSoLuong;
    }

    private static EmployeeDto ReadEmployee(SqlDataReader reader) => new()
    {
        MaNV = reader.GetInt32(0),
        HoTen = reader.GetString(1),
        NgaySinh = reader.IsDBNull(2) ? null : reader.GetDateTime(2),
        CCCD = reader.IsDBNull(3) ? null : reader.GetString(3),
        MaPB = reader.IsDBNull(4) ? null : reader.GetInt32(4),
        LuongCoBan = reader.GetDecimal(5),
        HeSoLuong = reader.GetDouble(6)
    };
}
