namespace Bai6._1.DTO;

public sealed class NhanVienDto
{
    public int MaNV { get; set; }
    public string TenNV { get; set; } = string.Empty;
    public string? SoDienThoai { get; set; }
    public string? ChucVu { get; set; }

    public override string ToString() => TenNV;
}
