using bai6._7.DAL;
using bai6._7.DTO;

namespace bai6._7.BUS;

public sealed class DonThuocBUS
{
    private readonly DonThuocDAL _donThuocDAL = new();

    public IReadOnlyList<ThuocDTO> LayDanhSachThuoc() => _donThuocDAL.LayDanhSachThuoc();

    public int LuuVaXuatDon(int maHoSo, IReadOnlyCollection<ChiTietDonThuocDTO> chiTiet)
    {
        if (maHoSo <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maHoSo), "Hồ sơ khám chưa hợp lệ.");
        }

        ArgumentNullException.ThrowIfNull(chiTiet);
        if (chiTiet.Count == 0)
        {
            throw new ArgumentException("Vui lòng thêm ít nhất một thuốc vào đơn.", nameof(chiTiet));
        }

        if (chiTiet.Any(item => item.SoLuong <= 0))
        {
            throw new ArgumentException("Số lượng mỗi thuốc phải lớn hơn 0.", nameof(chiTiet));
        }

        return _donThuocDAL.LuuVaXuatDon(maHoSo, chiTiet);
    }
}
