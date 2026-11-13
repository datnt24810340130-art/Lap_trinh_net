namespace bai6._7.DTO;

public sealed class HoSoKhamDTO
{
    public int MaHoSo { get; set; }
    public int MaBN { get; set; }
    public DateTime NgayKham { get; set; }
    public string ChanDoan { get; set; } = string.Empty;
    public decimal TienKham { get; set; }
    public string TrangThai { get; set; } = "Chưa thanh toán";
}
