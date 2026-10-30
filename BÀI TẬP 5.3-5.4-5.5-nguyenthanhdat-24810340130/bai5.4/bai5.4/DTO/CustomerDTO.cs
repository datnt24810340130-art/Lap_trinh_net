namespace bai5._4.DTO;

public sealed class CustomerDTO
{
    public int MaKH { get; set; }
    public string HoTen { get; set; } = string.Empty;
    public string CCCD { get; set; } = string.Empty;
    public string? SoDienThoai { get; set; }
}