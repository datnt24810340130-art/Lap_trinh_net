namespace bai6._7.DTO;

public sealed class ThuocDTO
{
    public int MaThuoc { get; set; }
    public string TenThuoc { get; set; } = string.Empty;
    public string DonViTinh { get; set; } = string.Empty;
    public decimal DonGia { get; set; }
    public int SoLuongTon { get; set; }

    public override string ToString() => $"{TenThuoc} ({DonViTinh}) - tồn: {SoLuongTon}";
}
