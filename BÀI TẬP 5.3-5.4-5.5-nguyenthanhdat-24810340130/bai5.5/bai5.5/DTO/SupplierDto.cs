namespace bai5._5.DTO;

public sealed class SupplierDto
{
    public int MaNCC { get; set; }
    public string TenNCC { get; set; } = string.Empty;
    public string? DienThoai { get; set; }
    public string? DiaChi { get; set; }
}
