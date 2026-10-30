namespace bai5._5.DTO;

public sealed class MaterialDto
{
    public string MaVatTu { get; set; } = string.Empty;
    public string TenVatTu { get; set; } = string.Empty;
    public string DVT { get; set; } = string.Empty;
    public decimal SoLuongTon { get; set; }
    public decimal SoLuongToiThieu { get; set; }
}
