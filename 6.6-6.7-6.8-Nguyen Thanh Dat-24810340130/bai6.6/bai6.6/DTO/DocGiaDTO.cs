namespace bai6._6.DTO;

public sealed class DocGiaDTO
{
    public int MaDocGia { get; set; }
    public string HoTen { get; set; } = string.Empty;
    public DateTime? NgaySinh { get; set; }
    public string? GioiTinh { get; set; }
    public string? DiaChi { get; set; }
    public string? SoDienThoai { get; set; }
    public string? Email { get; set; }
    public DateTime NgayHetHanThe { get; set; }
}
