using Bai6._1.DAL;
using Bai6._1.DTO;

namespace Bai6._1.BUS;

public sealed class BanHangBus
{
    private readonly BanHangDal _dal = new();
    private readonly SanPhamDal _sanPhamDal = new();
    private readonly KhachHangDal _khachHangDal = new();
    private readonly NhanVienDal _nhanVienDal = new();

    public List<SanPhamDto> GetProducts(string keyword = "") =>
        string.IsNullOrWhiteSpace(keyword) ? _sanPhamDal.GetAll() : _sanPhamDal.Search(keyword.Trim());

    public List<KhachHangDto> GetCustomers() => _khachHangDal.GetAll();
    public List<NhanVienDto> GetEmployees() => _nhanVienDal.GetAll();

    public KetQuaThanhToanDto ThanhToan(HoaDonDto invoice)
    {
        if (invoice.MaNV <= 0)
            throw new ArgumentException("Vui lòng chọn nhân viên bán hàng.");
        if (invoice.ChiTiet.Count == 0)
            throw new ArgumentException("Giỏ hàng đang trống.");
        if (invoice.ChiTiet.Any(item => string.IsNullOrWhiteSpace(item.MaSP) || item.SoLuong <= 0))
            throw new ArgumentException("Sản phẩm và số lượng trong giỏ hàng không hợp lệ.");
        if (invoice.ChiTiet.GroupBy(item => item.MaSP, StringComparer.OrdinalIgnoreCase).Any(group => group.Count() > 1))
            throw new ArgumentException("Mỗi sản phẩm chỉ được xuất hiện một lần trong giỏ hàng.");
        return _dal.ThanhToan(invoice);
    }

    public decimal GetDailyRevenue(DateTime date)
    {
        var start = date.Date;
        return _dal.GetRevenue(start, start.AddDays(1));
    }

    public decimal GetMonthlyRevenue(int year, int month)
    {
        if (year < 1 || month is < 1 or > 12)
            throw new ArgumentOutOfRangeException(nameof(month), "Tháng hoặc năm không hợp lệ.");
        var start = new DateTime(year, month, 1);
        return _dal.GetRevenue(start, start.AddMonths(1));
    }

    public List<SanPhamBanChayDto> GetTopSellingProducts() => _dal.GetTopSellingProducts();
}
