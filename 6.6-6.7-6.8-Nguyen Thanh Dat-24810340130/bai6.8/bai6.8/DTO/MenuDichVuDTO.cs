namespace bai6._8.DTO;

public sealed class MenuDichVuDTO
{
    public int MaDV { get; set; }
    public string TenDV { get; set; } = string.Empty;
    public decimal DonGia { get; set; }
    public int TonKho { get; set; }

    public override string ToString() => TenDV;
}
