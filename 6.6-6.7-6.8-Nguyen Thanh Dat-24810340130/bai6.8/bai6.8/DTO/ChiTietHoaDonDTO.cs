namespace bai6._8.DTO;

public sealed class ChiTietHoaDonDTO
{
    public int MaHD { get; set; }
    public int MaDV { get; set; }
    public string TenDV { get; set; } = string.Empty;
    public int SoLuong { get; set; }
    public decimal DonGia { get; set; }
    public decimal ThanhTien => SoLuong * DonGia;
}
