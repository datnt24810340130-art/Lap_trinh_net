namespace bai6._4.DTO;

public sealed class CustomerDto
{
    public int MaKH { get; set; }
    public string TenKH { get; set; } = string.Empty;
    public string? DienThoai { get; set; }
    public string? DiaChi { get; set; }
}
