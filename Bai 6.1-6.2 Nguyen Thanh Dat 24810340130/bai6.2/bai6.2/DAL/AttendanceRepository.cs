using System.Data;
using Microsoft.Data.SqlClient;

namespace bai6._2.DAL;

public sealed class AttendanceRepository
{
    public DataTable LoadMonthlyAttendance(string month)
    {
        const string attendanceSql = """
            SELECT MaCong, MaNV, ThangNam, SoNgayCong, SoGioOT, IsLocked
            FROM dbo.BangChamCong
            WHERE ThangNam = @ThangNam
            ORDER BY MaNV;
            """;
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand(attendanceSql, connection);
        command.Parameters.Add("@ThangNam", SqlDbType.VarChar, 7).Value = month;
        using var adapter = new SqlDataAdapter(command)
        {
            MissingSchemaAction = MissingSchemaAction.AddWithKey
        };

        var table = new DataTable("BangChamCong");
        connection.Open();
        adapter.Fill(table);

        if (!table.Columns.Contains("HoTen"))
        {
            table.Columns.Add("HoTen", typeof(string));
        }

        var employeeNames = new Dictionary<int, string>();
        using (var employeeCommand = new SqlCommand("SELECT MaNV, HoTen FROM dbo.NhanVien ORDER BY HoTen;", connection))
        using (var reader = employeeCommand.ExecuteReader())
        {
            while (reader.Read())
            {
                employeeNames.Add(reader.GetInt32(0), reader.GetString(1));
            }
        }

        var existingEmployeeIds = new HashSet<int>();
        foreach (DataRow row in table.Rows)
        {
            int employeeId = (int)row["MaNV"];
            existingEmployeeIds.Add(employeeId);
            row["HoTen"] = employeeNames.GetValueOrDefault(employeeId, string.Empty);
        }

        foreach ((int employeeId, string name) in employeeNames)
        {
            if (existingEmployeeIds.Contains(employeeId))
            {
                continue;
            }

            DataRow row = table.NewRow();
            row["MaNV"] = employeeId;
            row["ThangNam"] = month;
            row["SoNgayCong"] = 0d;
            row["SoGioOT"] = 0d;
            row["IsLocked"] = false;
            row["HoTen"] = name;
            table.Rows.Add(row);
        }

        return table;
    }

    public int SaveMonthlyAttendance(DataTable attendance)
    {
        if (attendance.Rows.Count == 0)
        {
            return 0;
        }

        string month = Convert.ToString(attendance.Rows[0]["ThangNam"])!;
        using var connection = DbConnectionFactory.CreateConnection();
        connection.Open();

        using (var lockCheck = new SqlCommand(
            "SELECT COUNT(*) FROM dbo.BangChamCong WHERE ThangNam = @ThangNam AND IsLocked = 1;", connection))
        {
            lockCheck.Parameters.Add("@ThangNam", SqlDbType.VarChar, 7).Value = month;
            if ((int)lockCheck.ExecuteScalar()! > 0)
            {
                throw new InvalidOperationException("Attendance for this month has already been closed.");
            }
        }

        const string selectSql = """
            SELECT MaCong, MaNV, ThangNam, SoNgayCong, SoGioOT, IsLocked
            FROM dbo.BangChamCong
            WHERE ThangNam = @ThangNam;
            """;
        using var selectCommand = new SqlCommand(selectSql, connection);
        selectCommand.Parameters.Add("@ThangNam", SqlDbType.VarChar, 7).Value = month;
        using var adapter = new SqlDataAdapter(selectCommand);
        using var builder = new SqlCommandBuilder(adapter);
        adapter.UpdateCommand = builder.GetUpdateCommand();
        adapter.InsertCommand = builder.GetInsertCommand();
        adapter.DeleteCommand = builder.GetDeleteCommand();

        return adapter.Update(attendance);
    }
}
