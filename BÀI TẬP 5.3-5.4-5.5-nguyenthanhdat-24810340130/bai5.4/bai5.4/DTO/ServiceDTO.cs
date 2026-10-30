namespace bai5._4.DTO;

public sealed class ServiceDTO
{
    public int MaDV { get; set; }
    public string TenDV { get; set; } = string.Empty;
    public decimal DonGia { get; set; }

    public override string ToString() => $"{TenDV} - {DonGia:N0} đ";
}