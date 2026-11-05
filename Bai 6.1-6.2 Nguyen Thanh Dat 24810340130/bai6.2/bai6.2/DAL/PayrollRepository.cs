using System.Data;
using Microsoft.Data.SqlClient;

namespace bai6._2.DAL;

public sealed class PayrollRepository
{
    public int CloseMonth(string month)
    {
        using var connection = DbConnectionFactory.CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction(IsolationLevel.Serializable);

        try
        {
            using (var existingSalaryCommand = new SqlCommand(
                "SELECT COUNT(*) FROM dbo.BangLuong WITH (UPDLOCK, HOLDLOCK) WHERE ThangNam = @ThangNam;",
                connection, transaction))
            {
                existingSalaryCommand.Parameters.Add("@ThangNam", SqlDbType.VarChar, 7).Value = month;
                if ((int)existingSalaryCommand.ExecuteScalar()! > 0)
                {
                    throw new InvalidOperationException("Payroll for this month has already been closed.");
                }
            }

            const string createMissingAttendanceSql = """
                INSERT INTO dbo.BangChamCong (MaNV, ThangNam, SoNgayCong, SoGioOT)
                SELECT nv.MaNV, @ThangNam, 0, 0
                FROM dbo.NhanVien AS nv
                WHERE NOT EXISTS
                (
                    SELECT 1 FROM dbo.BangChamCong AS cc WITH (UPDLOCK, HOLDLOCK)
                    WHERE cc.MaNV = nv.MaNV AND cc.ThangNam = @ThangNam
                );
                """;
            using (var attendanceCommand = new SqlCommand(createMissingAttendanceSql, connection, transaction))
            {
                attendanceCommand.Parameters.Add("@ThangNam", SqlDbType.VarChar, 7).Value = month;
                attendanceCommand.ExecuteNonQuery();
            }

            const string insertSalarySql = """
                INSERT INTO dbo.BangLuong (MaNV, ThangNam, ThucLinh)
                SELECT nv.MaNV, cc.ThangNam,
                       CONVERT(decimal(18,2), ((nv.LuongCoBan * nv.HeSoLuong / 22.0) * cc.SoNgayCong)
                           + (cc.SoGioOT * 50000.0))
                FROM dbo.BangChamCong AS cc WITH (UPDLOCK, HOLDLOCK)
                INNER JOIN dbo.NhanVien AS nv ON nv.MaNV = cc.MaNV
                WHERE cc.ThangNam = @ThangNam;
                """;
            using (var salaryCommand = new SqlCommand(insertSalarySql, connection, transaction))
            {
                salaryCommand.Parameters.Add("@ThangNam", SqlDbType.VarChar, 7).Value = month;
                int count = salaryCommand.ExecuteNonQuery();
                if (count == 0)
                {
                    throw new InvalidOperationException("There are no employees to calculate payroll for.");
                }

                using var lockAttendanceCommand = new SqlCommand(
                    "UPDATE dbo.BangChamCong SET IsLocked = 1 WHERE ThangNam = @ThangNam AND IsLocked = 0;",
                    connection, transaction);
                lockAttendanceCommand.Parameters.Add("@ThangNam", SqlDbType.VarChar, 7).Value = month;
                lockAttendanceCommand.ExecuteNonQuery();

                transaction.Commit();
                return count;
            }
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
}
