namespace bai6._6.DTO;

public sealed class PhieuMuonDTO
{
    public int MaPhieu { get; set; }
    public int MaDocGia { get; set; }
    public string HoTen { get; set; } = string.Empty;
    public DateTime NgayMuon { get; set; }
    public DateTime HanTra { get; set; }
    public DateTime? NgayTra { get; set; }
    public string TrangThai { get; set; } = string.Empty;
}
