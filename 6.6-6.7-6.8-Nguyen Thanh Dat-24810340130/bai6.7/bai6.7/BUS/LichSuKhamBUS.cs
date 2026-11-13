using bai6._7.DAL;
using bai6._7.DTO;

namespace bai6._7.BUS;

public sealed class LichSuKhamBUS
{
    private readonly LichSuKhamDAL _lichSuKhamDAL = new();

    public IReadOnlyList<LichSuKhamDTO> TimKiem(string? tuKhoa = null)
    {
        return _lichSuKhamDAL.TimKiem(tuKhoa);
    }

    public IReadOnlyList<ChiTietDonThuocDTO> LayChiTietDonThuoc(int maHoSo)
    {
        if (maHoSo <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maHoSo), "Mã hồ sơ khám không hợp lệ.");
        }

        return _lichSuKhamDAL.LayChiTietDonThuoc(maHoSo);
    }
}
