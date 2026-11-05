using Bai6._1.DAL;
using Bai6._1.DTO;

namespace Bai6._1.BUS;

public sealed class KhachHangBus
{
    private readonly KhachHangDal _dal = new();

    public List<KhachHangDto> GetAll() => _dal.GetAll();

    public void Add(KhachHangDto customer)
    {
        Validate(customer);
        _dal.Add(customer);
    }

    public bool Update(KhachHangDto customer)
    {
        Validate(customer);
        if (customer.MaKH <= 0)
            throw new ArgumentException("Mã khách hàng không hợp lệ.");
        return _dal.Update(customer);
    }

    public bool Delete(int maKH)
    {
        if (maKH <= 0)
            throw new ArgumentException("Mã khách hàng không hợp lệ.");
        return _dal.Delete(maKH);
    }

    private static void Validate(KhachHangDto customer)
    {
        if (string.IsNullOrWhiteSpace(customer.TenKH) || customer.TenKH.Trim().Length > 100)
            throw new ArgumentException("Tên khách hàng bắt buộc và tối đa 100 ký tự.");
        if (customer.DienThoai?.Length > 15 || customer.DiemTichLuy < 0)
            throw new ArgumentException("Số điện thoại tối đa 15 ký tự; điểm tích lũy không được âm.");
        customer.TenKH = customer.TenKH.Trim();
    }
}
