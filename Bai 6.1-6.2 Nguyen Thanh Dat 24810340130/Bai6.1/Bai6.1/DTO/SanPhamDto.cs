namespace Bai6._1.DTO;

public sealed class SanPhamDto
{
    public string MaSP { get; set; } = string.Empty;
    public string TenSP { get; set; } = string.Empty;
    public decimal GiaBan { get; set; }
    public int SoLuongTon { get; set; }

    public override string ToString() => $"{MaSP} - {TenSP} ({GiaBan:N0} đ, tồn {SoLuongTon})";
}
