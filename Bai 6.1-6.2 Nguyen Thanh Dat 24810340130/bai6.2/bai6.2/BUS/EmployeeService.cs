using bai6._2.DAL;
using bai6._2.DTO;

namespace bai6._2.BUS;

public sealed class EmployeeService
{
    private readonly EmployeeRepository _repository = new();

    public List<DepartmentDto> GetDepartments() => _repository.GetDepartments();

    public List<EmployeeDto> SearchEmployees(string? name, int? departmentId, string? nationalId) =>
        _repository.SearchEmployees(name, departmentId, nationalId);

    public int AddEmployee(EmployeeDto employee)
    {
        Validate(employee);
        return _repository.Insert(employee);
    }

    public void UpdateEmployee(EmployeeDto employee)
    {
        if (employee.MaNV <= 0)
        {
            throw new ArgumentException("Select an employee to update.");
        }

        Validate(employee);
        _repository.Update(employee);
    }

    public void DeleteEmployee(int employeeId)
    {
        if (employeeId <= 0)
        {
            throw new ArgumentException("Select an employee to delete.");
        }

        _repository.Delete(employeeId);
    }

    private static void Validate(EmployeeDto employee)
    {
        if (string.IsNullOrWhiteSpace(employee.HoTen) || employee.HoTen.Trim().Length > 100)
        {
            throw new ArgumentException("Employee name is required and must be 100 characters or fewer.");
        }

        if (employee.CCCD?.Length > 20)
        {
            throw new ArgumentException("National ID must be 20 characters or fewer.");
        }

        if (employee.LuongCoBan <= 0)
        {
            throw new ArgumentException("Base salary must be greater than zero.");
        }

        if (!double.IsFinite(employee.HeSoLuong) || employee.HeSoLuong <= 0)
        {
            throw new ArgumentException("Salary coefficient must be a positive finite number.");
        }

        if (employee.NgaySinh > DateTime.Today)
        {
            throw new ArgumentException("Date of birth cannot be in the future.");
        }
    }
}
