namespace bai6._8.DTO;

public sealed class HoaDonDTO
{
    public int MaHD { get; set; }
    public int MaBan { get; set; }
    public DateTime GioBatDau { get; set; }
    public DateTime? GioKetThuc { get; set; }
    public decimal GiaTheoGio { get; set; }
    public decimal? SoGio { get; set; }
    public decimal? TienGioChoi { get; set; }
    public decimal? TienDichVu { get; set; }
    public decimal? TongTien { get; set; }
    public string TrangThai { get; set; } = string.Empty;
}
