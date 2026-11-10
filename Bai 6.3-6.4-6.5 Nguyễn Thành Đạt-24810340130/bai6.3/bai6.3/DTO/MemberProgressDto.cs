namespace bai6._3.DTO;

public sealed class MemberProgressDto
{
    public int MaThanhVien { get; set; }
    public string HoTen { get; set; } = string.Empty;
    public int TongCongViec { get; set; }
    public int CongViecHoanThanh { get; set; }
    public decimal TyLeHoanThanh { get; set; }
}
