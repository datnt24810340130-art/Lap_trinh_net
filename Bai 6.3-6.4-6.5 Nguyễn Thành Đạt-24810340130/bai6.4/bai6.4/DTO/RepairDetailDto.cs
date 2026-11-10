namespace bai6._4.DTO;

public sealed class RepairDetailDto
{
    public int MaPhuTung { get; set; }
    public string TenPhuTung { get; set; } = string.Empty;
    public int SoLuong { get; set; }
    public decimal DonGia { get; set; }
    public decimal TienCong { get; set; }
    public decimal ThanhTien => SoLuong * DonGia + TienCong;
}
