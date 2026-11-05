using Bai6._1.DAL;
using Bai6._1.DTO;

namespace Bai6._1.BUS;

public sealed class NhanVienBus
{
    private readonly NhanVienDal _dal = new();

    public List<NhanVienDto> GetAll() => _dal.GetAll();

    public void Add(NhanVienDto employee)
    {
        Validate(employee);
        _dal.Add(employee);
    }

    public bool Update(NhanVienDto employee)
    {
        Validate(employee);
        if (employee.MaNV <= 0)
            throw new ArgumentException("Mã nhân viên không hợp lệ.");
        return _dal.Update(employee);
    }

    public bool Delete(int maNV)
    {
        if (maNV <= 0)
            throw new ArgumentException("Mã nhân viên không hợp lệ.");
        return _dal.Delete(maNV);
    }

    private static void Validate(NhanVienDto employee)
    {
        if (string.IsNullOrWhiteSpace(employee.TenNV) || employee.TenNV.Trim().Length > 100)
            throw new ArgumentException("Tên nhân viên bắt buộc và tối đa 100 ký tự.");
        if (employee.SoDienThoai?.Length > 15 || employee.ChucVu?.Length > 50)
            throw new ArgumentException("Số điện thoại tối đa 15 ký tự và chức vụ tối đa 50 ký tự.");
        employee.TenNV = employee.TenNV.Trim();
    }
}
