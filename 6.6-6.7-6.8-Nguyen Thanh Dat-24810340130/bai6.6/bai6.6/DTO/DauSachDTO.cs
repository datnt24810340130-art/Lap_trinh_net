namespace bai6._6.DTO;

public sealed class DauSachDTO
{
    public int MaSach { get; set; }
    public string TenSach { get; set; } = string.Empty;
    public string? TacGia { get; set; }
    public string TheLoai { get; set; } = string.Empty;
    public string? NhaXuatBan { get; set; }
    public int? NamXuatBan { get; set; }
    public int SoLuong { get; set; }
    public int SoLuongCon { get; set; }
}
