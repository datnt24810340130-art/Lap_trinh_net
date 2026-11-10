namespace bai6._5.DTO;

public sealed class RoomDto
{
    public int MaPhong { get; set; }
    public string TenPhong { get; set; } = string.Empty;
    public string LoaiPhong { get; set; } = string.Empty;
    public decimal GiaTheoGio { get; set; }
    public string TrangThai { get; set; } = string.Empty;
    public int? MaHD { get; set; }
    public DateTime? GioVao { get; set; }
}
