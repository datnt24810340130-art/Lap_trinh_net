namespace Bai6._1.DTO;

public sealed class HoaDonDto
{
    public int MaNV { get; set; }
    public int? MaKH { get; set; }
    public List<ChiTietHoaDonDto> ChiTiet { get; set; } = [];
    public decimal TongTien => ChiTiet.Sum(item => item.ThanhTien);
}

public sealed record KetQuaThanhToanDto(int MaHD, decimal TongTien, int DiemCongThem);
