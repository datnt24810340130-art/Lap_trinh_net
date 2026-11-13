namespace bai6._7.DTO;

public sealed class LichSuKhamDTO
{
    public int MaHoSo { get; set; }
    public int MaBN { get; set; }
    public string HoTen { get; set; } = string.Empty;
    public string SoDienThoai { get; set; } = string.Empty;
    public DateTime NgayKham { get; set; }
    public string ChanDoan { get; set; } = string.Empty;
    public decimal TienKham { get; set; }
    public int SoDonThuoc { get; set; }
    public decimal TienThuoc { get; set; }
    public decimal TongTien => TienKham + TienThuoc;
    public string TrangThai { get; set; } = string.Empty;
}
