namespace bai6._4.DTO;

public sealed class VehicleDto
{
    public string BienSo { get; set; } = string.Empty;
    public int MaKH { get; set; }
    public string TenKH { get; set; } = string.Empty;
    public string? DienThoai { get; set; }
    public string? DiaChi { get; set; }
    public string? HieuXe { get; set; }
    public int? NamSanXuat { get; set; }
}
