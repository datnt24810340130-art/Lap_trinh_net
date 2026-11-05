using System.Data;
using bai6._2.DAL;

namespace bai6._2.BUS;

public sealed class AttendanceService
{
    private readonly AttendanceRepository _repository = new();

    public DataTable LoadMonth(string month) => _repository.LoadMonthlyAttendance(MonthValidator.Normalize(month));

    public int SaveMonth(DataTable attendance)
    {
        ArgumentNullException.ThrowIfNull(attendance);
        if (!attendance.Columns.Contains("MaNV") || !attendance.Columns.Contains("ThangNam") ||
            !attendance.Columns.Contains("SoNgayCong") || !attendance.Columns.Contains("SoGioOT"))
        {
            throw new ArgumentException("The attendance table is missing required columns.", nameof(attendance));
        }

        var employeeIds = new HashSet<int>();
        string? submittedMonth = null;
        foreach (DataRow row in attendance.Rows)
        {
            if (row.RowState == DataRowState.Deleted)
            {
                continue;
            }

            int employeeId = Convert.ToInt32(row["MaNV"]);
            string month = MonthValidator.Normalize(Convert.ToString(row["ThangNam"]) ?? string.Empty);
            if (submittedMonth is not null && submittedMonth != month)
            {
                throw new ArgumentException("An attendance save can only contain one month.");
            }

            submittedMonth = month;
            double workDays = Convert.ToDouble(row["SoNgayCong"]);
            double overtimeHours = Convert.ToDouble(row["SoGioOT"]);

            if (!employeeIds.Add(employeeId))
            {
                throw new ArgumentException("Each employee can only have one attendance row per month.");
            }

            if (!double.IsFinite(workDays) || workDays < 0 || workDays > 31)
            {
                throw new ArgumentException("Work days must be between 0 and 31.");
            }

            if (!double.IsFinite(overtimeHours) || overtimeHours < 0)
            {
                throw new ArgumentException("Overtime hours must be a non-negative number.");
            }

            row["ThangNam"] = month;
        }

        return _repository.SaveMonthlyAttendance(attendance);
    }
}
