using bai6._3.DTO;
using Microsoft.Data.SqlClient;
using System.Data;

namespace bai6._3.DAL;

public sealed class ProjectTaskDal
{
    private readonly string _connectionString;

    public ProjectTaskDal(string connectionString)
    {
        _connectionString = connectionString;
    }

    private SqlConnection CreateConnection() => new(_connectionString);

    public List<ProjectDto> GetProjects()
    {
        const string sql = "SELECT MaDuAn, TenDuAn, NgayBatDau, NgayKetThuc, TrangThai FROM dbo.DuAn ORDER BY TenDuAn";
        using var connection = CreateConnection();
        using var command = new SqlCommand(sql, connection);
        connection.Open();
        using var reader = command.ExecuteReader();
        var projects = new List<ProjectDto>();
        while (reader.Read())
        {
            projects.Add(new ProjectDto
            {
                MaDuAn = reader.GetInt32(0),
                TenDuAn = reader.GetString(1),
                NgayBatDau = reader.IsDBNull(2) ? null : reader.GetDateTime(2),
                NgayKetThuc = reader.IsDBNull(3) ? null : reader.GetDateTime(3),
                TrangThai = reader.IsDBNull(4) ? null : reader.GetString(4)
            });
        }
        return projects;
    }

    public int SaveProject(ProjectDto project)
    {
        const string insertSql = "INSERT INTO dbo.DuAn (TenDuAn, NgayBatDau, NgayKetThuc, TrangThai) OUTPUT INSERTED.MaDuAn VALUES (@name, @start, @end, @status)";
        const string updateSql = "UPDATE dbo.DuAn SET TenDuAn = @name, NgayBatDau = @start, NgayKetThuc = @end, TrangThai = @status WHERE MaDuAn = @id";
        using var connection = CreateConnection();
        using var command = new SqlCommand(project.MaDuAn == 0 ? insertSql : updateSql, connection);
        command.Parameters.Add("@name", SqlDbType.NVarChar, 150).Value = project.TenDuAn;
        command.Parameters.Add("@start", SqlDbType.DateTime).Value = (object?)project.NgayBatDau ?? DBNull.Value;
        command.Parameters.Add("@end", SqlDbType.DateTime).Value = (object?)project.NgayKetThuc ?? DBNull.Value;
        command.Parameters.Add("@status", SqlDbType.NVarChar, 50).Value = (object?)project.TrangThai ?? DBNull.Value;
        if (project.MaDuAn == 0)
        {
            connection.Open();
            return Convert.ToInt32(command.ExecuteScalar());
        }

        command.Parameters.Add("@id", SqlDbType.Int).Value = project.MaDuAn;
        connection.Open();
        if (command.ExecuteNonQuery() == 0)
            throw new InvalidOperationException("Không tìm thấy dự án cần cập nhật.");
        return project.MaDuAn;
    }

    public void DeleteProject(int projectId)
    {
        using var connection = CreateConnection();
        using var command = new SqlCommand("DELETE FROM dbo.DuAn WHERE MaDuAn = @id", connection);
        command.Parameters.Add("@id", SqlDbType.Int).Value = projectId;
        connection.Open();
        if (command.ExecuteNonQuery() == 0)
            throw new InvalidOperationException("Không tìm thấy dự án cần xóa.");
    }

    public List<TaskDto> GetTasks(int? projectId = null, string? status = null)
    {
        const string sql = "SELECT t.MaTask, t.MaDuAn, t.TenTask, t.MoTa, t.HanNop, t.DoUuTien, t.TrangThai, p.TenDuAn FROM dbo.CongViec t INNER JOIN dbo.DuAn p ON p.MaDuAn = t.MaDuAn WHERE (@projectId IS NULL OR t.MaDuAn = @projectId) AND (@status IS NULL OR t.TrangThai = @status) ORDER BY t.HanNop, t.MaTask";
        using var connection = CreateConnection();
        using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@projectId", SqlDbType.Int).Value = (object?)projectId ?? DBNull.Value;
        command.Parameters.Add("@status", SqlDbType.NVarChar, 50).Value = (object?)status ?? DBNull.Value;
        connection.Open();
        using var reader = command.ExecuteReader();
        return ReadTasks(reader);
    }

    public int SaveTask(TaskDto task)
    {
        const string insertSql = "INSERT INTO dbo.CongViec (MaDuAn, TenTask, MoTa, HanNop, DoUuTien, TrangThai) OUTPUT INSERTED.MaTask VALUES (@projectId, @name, @description, @dueDate, @priority, @status)";
        const string updateSql = "UPDATE dbo.CongViec SET MaDuAn = @projectId, TenTask = @name, MoTa = @description, HanNop = @dueDate, DoUuTien = @priority, TrangThai = @status WHERE MaTask = @id";
        using var connection = CreateConnection();
        using var command = new SqlCommand(task.MaTask == 0 ? insertSql : updateSql, connection);
        AddTaskParameters(command, task);
        connection.Open();
        if (task.MaTask == 0)
            return Convert.ToInt32(command.ExecuteScalar());

        command.Parameters.Add("@id", SqlDbType.Int).Value = task.MaTask;
        if (command.ExecuteNonQuery() == 0)
            throw new InvalidOperationException("Không tìm thấy công việc cần cập nhật.");
        return task.MaTask;
    }

    public void DeleteTask(int taskId)
    {
        using var connection = CreateConnection();
        using var command = new SqlCommand("DELETE FROM dbo.CongViec WHERE MaTask = @id", connection);
        command.Parameters.Add("@id", SqlDbType.Int).Value = taskId;
        connection.Open();
        if (command.ExecuteNonQuery() == 0)
            throw new InvalidOperationException("Không tìm thấy công việc cần xóa.");
    }

    public void UpdateTaskStatus(int taskId, string status)
    {
        using var connection = CreateConnection();
        using var command = new SqlCommand("UPDATE dbo.CongViec SET TrangThai = @status WHERE MaTask = @id", connection);
        command.Parameters.Add("@status", SqlDbType.NVarChar, 50).Value = status;
        command.Parameters.Add("@id", SqlDbType.Int).Value = taskId;
        connection.Open();
        if (command.ExecuteNonQuery() == 0)
            throw new InvalidOperationException("Không tìm thấy công việc cần chuyển trạng thái.");
    }

    public List<MemberDto> GetMembers()
    {
        const string sql = "SELECT MaThanhVien, HoTen, Email, ChuyenMon FROM dbo.ThanhVien ORDER BY HoTen";
        using var connection = CreateConnection();
        using var command = new SqlCommand(sql, connection);
        connection.Open();
        using var reader = command.ExecuteReader();
        var members = new List<MemberDto>();
        while (reader.Read())
        {
            members.Add(new MemberDto
            {
                MaThanhVien = reader.GetInt32(0),
                HoTen = reader.GetString(1),
                Email = reader.GetString(2),
                ChuyenMon = reader.IsDBNull(3) ? null : reader.GetString(3)
            });
        }
        return members;
    }

    public void SetTaskAssignments(int taskId, IEnumerable<int> memberIds)
    {
        using var connection = CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction();
        try
        {
            using (var delete = new SqlCommand("DELETE FROM dbo.PhanCong WHERE MaTask = @taskId", connection, transaction))
            {
                delete.Parameters.Add("@taskId", SqlDbType.Int).Value = taskId;
                delete.ExecuteNonQuery();
            }

            foreach (var memberId in memberIds.Distinct())
            {
                using var insert = new SqlCommand("INSERT INTO dbo.PhanCong (MaTask, MaThanhVien) VALUES (@taskId, @memberId)", connection, transaction);
                insert.Parameters.Add("@taskId", SqlDbType.Int).Value = taskId;
                insert.Parameters.Add("@memberId", SqlDbType.Int).Value = memberId;
                insert.ExecuteNonQuery();
            }
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public List<int> GetAssignedMemberIds(int taskId)
    {
        using var connection = CreateConnection();
        using var command = new SqlCommand("SELECT MaThanhVien FROM dbo.PhanCong WHERE MaTask = @taskId", connection);
        command.Parameters.Add("@taskId", SqlDbType.Int).Value = taskId;
        connection.Open();
        using var reader = command.ExecuteReader();
        var ids = new List<int>();
        while (reader.Read())
            ids.Add(reader.GetInt32(0));
        return ids;
    }

    public List<TaskDto> GetOverdueTasks()
    {
        const string sql = "SELECT t.MaTask, t.MaDuAn, t.TenTask, t.MoTa, t.HanNop, t.DoUuTien, t.TrangThai, p.TenDuAn FROM dbo.CongViec t INNER JOIN dbo.DuAn p ON p.MaDuAn = t.MaDuAn WHERE t.HanNop < GETDATE() AND t.TrangThai <> N'Done' ORDER BY t.HanNop";
        using var connection = CreateConnection();
        using var command = new SqlCommand(sql, connection);
        connection.Open();
        using var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
        return ReadTasks(reader);
    }

    public List<MemberProgressDto> GetMemberProgress(int? projectId)
    {
        const string sql = "SELECT m.MaThanhVien, m.HoTen, COUNT(t.MaTask) AS TongCongViec, SUM(CASE WHEN t.TrangThai = N'Done' THEN 1 ELSE 0 END) AS CongViecHoanThanh, CAST(CASE WHEN COUNT(t.MaTask) = 0 THEN 0 ELSE 100.0 * SUM(CASE WHEN t.TrangThai = N'Done' THEN 1 ELSE 0 END) / COUNT(t.MaTask) END AS DECIMAL(5,2)) AS TyLeHoanThanh FROM dbo.ThanhVien m INNER JOIN dbo.PhanCong pc ON pc.MaThanhVien = m.MaThanhVien INNER JOIN dbo.CongViec t ON t.MaTask = pc.MaTask WHERE (@projectId IS NULL OR t.MaDuAn = @projectId) GROUP BY m.MaThanhVien, m.HoTen ORDER BY m.HoTen";
        using var connection = CreateConnection();
        using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@projectId", SqlDbType.Int).Value = (object?)projectId ?? DBNull.Value;
        connection.Open();
        using var reader = command.ExecuteReader();
        var progress = new List<MemberProgressDto>();
        while (reader.Read())
        {
            progress.Add(new MemberProgressDto
            {
                MaThanhVien = reader.GetInt32(0),
                HoTen = reader.GetString(1),
                TongCongViec = reader.GetInt32(2),
                CongViecHoanThanh = reader.GetInt32(3),
                TyLeHoanThanh = reader.GetDecimal(4)
            });
        }
        return progress;
    }

    private static void AddTaskParameters(SqlCommand command, TaskDto task)
    {
        command.Parameters.Add("@projectId", SqlDbType.Int).Value = task.MaDuAn;
        command.Parameters.Add("@name", SqlDbType.NVarChar, 200).Value = task.TenTask;
        command.Parameters.Add("@description", SqlDbType.NVarChar, -1).Value = (object?)task.MoTa ?? DBNull.Value;
        command.Parameters.Add("@dueDate", SqlDbType.DateTime).Value = (object?)task.HanNop ?? DBNull.Value;
        command.Parameters.Add("@priority", SqlDbType.NVarChar, 20).Value = (object?)task.DoUuTien ?? DBNull.Value;
        command.Parameters.Add("@status", SqlDbType.NVarChar, 50).Value = task.TrangThai;
    }

    private static List<TaskDto> ReadTasks(SqlDataReader reader)
    {
        var tasks = new List<TaskDto>();
        while (reader.Read())
        {
            tasks.Add(new TaskDto
            {
                MaTask = reader.GetInt32(0),
                MaDuAn = reader.GetInt32(1),
                TenTask = reader.GetString(2),
                MoTa = reader.IsDBNull(3) ? null : reader.GetString(3),
                HanNop = reader.IsDBNull(4) ? null : reader.GetDateTime(4),
                DoUuTien = reader.IsDBNull(5) ? null : reader.GetString(5),
                TrangThai = reader.GetString(6),
                TenDuAn = reader.GetString(7)
            });
        }
        return tasks;
    }
}
