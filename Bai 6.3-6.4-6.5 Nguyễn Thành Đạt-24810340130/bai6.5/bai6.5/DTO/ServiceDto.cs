namespace bai6._5.DTO;

public sealed class ServiceDto
{
    public int MaDV { get; set; }
    public string TenDV { get; set; } = string.Empty;
    public decimal DonGia { get; set; }
    public int TonKho { get; set; }

    public override string ToString() => $"{TenDV} - {DonGia:N0} đ (Tồn: {TonKho})";
}
