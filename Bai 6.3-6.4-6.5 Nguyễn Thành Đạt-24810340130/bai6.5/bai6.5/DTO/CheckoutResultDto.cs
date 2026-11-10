namespace bai6._5.DTO;

public sealed class CheckoutResultDto
{
    public int MaHD { get; set; }
    public decimal TongTienPhong { get; set; }
    public decimal TongTienDichVu { get; set; }
    public decimal TongThanhToan => TongTienPhong + TongTienDichVu;
}
