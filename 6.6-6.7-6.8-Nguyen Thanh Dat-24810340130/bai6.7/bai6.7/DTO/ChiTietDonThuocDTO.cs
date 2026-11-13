namespace bai6._7.DTO;

public sealed class ChiTietDonThuocDTO
{
    public int MaDon { get; set; }
    public int MaThuoc { get; set; }
    public string TenThuoc { get; set; } = string.Empty;
    public string DonViTinh { get; set; } = string.Empty;
    public int SoLuong { get; set; }
    public decimal DonGia { get; set; }
    public decimal ThanhTien => SoLuong * DonGia;
}
