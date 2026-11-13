namespace bai6._8.BUS;

public sealed record KetQuaTinhTien(decimal SoGio, decimal TienGioChoi);

public static class TinhTienBUS
{
    public static KetQuaTinhTien TinhTienGioChoi(DateTime gioBatDau, DateTime gioKetThuc, decimal giaTheoGio)
    {
        if (gioKetThuc < gioBatDau)
            throw new ArgumentException("Giờ kết thúc không thể trước giờ bắt đầu.", nameof(gioKetThuc));
        if (giaTheoGio < 0)
            throw new ArgumentOutOfRangeException(nameof(giaTheoGio));

        var tongPhut = (gioKetThuc - gioBatDau).TotalMinutes;
        var soBlock = (long)Math.Ceiling(tongPhut / 15d);
        var soGio = soBlock * 0.25m;
        return new KetQuaTinhTien(soGio, decimal.Round(soGio * giaTheoGio, 0, MidpointRounding.AwayFromZero));
    }
}
