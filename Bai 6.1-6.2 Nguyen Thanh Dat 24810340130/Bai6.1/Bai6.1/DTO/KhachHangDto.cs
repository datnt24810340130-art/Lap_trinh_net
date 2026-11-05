namespace Bai6._1.DTO;

public sealed class KhachHangDto
{
    public int MaKH { get; set; }
    public string TenKH { get; set; } = string.Empty;
    public string? DienThoai { get; set; }
    public int DiemTichLuy { get; set; }

    public override string ToString() => $"{TenKH} (điểm: {DiemTichLuy})";
}
