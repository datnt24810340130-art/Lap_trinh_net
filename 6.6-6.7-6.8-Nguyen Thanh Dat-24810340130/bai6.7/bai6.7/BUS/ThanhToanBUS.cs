using bai6._7.DAL;

namespace bai6._7.BUS;

public sealed class ThanhToanBUS
{
    private readonly ThanhToanDAL _thanhToanDAL = new();

    public decimal ThanhToanVaTinhTongTien(int maHoSo)
    {
        if (maHoSo <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maHoSo), "Hồ sơ khám chưa hợp lệ.");
        }

        return _thanhToanDAL.ThanhToanVaTinhTongTien(maHoSo);
    }
}
