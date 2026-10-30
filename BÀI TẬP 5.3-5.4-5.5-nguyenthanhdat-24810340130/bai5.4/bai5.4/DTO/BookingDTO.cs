namespace bai5._4.DTO;

public sealed class BookingDTO
{
    public int MaPhieu { get; set; }
    public string SoPhong { get; set; } = string.Empty;
    public string HoTen { get; set; } = string.Empty;
    public DateTime NgayNhanPhong { get; set; }

    public override string ToString() =>
        $"Phiếu {MaPhieu} - Phòng {SoPhong} - {HoTen} ({NgayNhanPhong:dd/MM/yyyy HH:mm})";
}