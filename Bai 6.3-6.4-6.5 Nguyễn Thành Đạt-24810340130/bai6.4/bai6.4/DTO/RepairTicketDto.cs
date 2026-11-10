namespace bai6._4.DTO;

public sealed class RepairTicketDto
{
    public int MaPhieu { get; set; }
    public string BienSo { get; set; } = string.Empty;
    public DateTime NgayNhan { get; set; }
    public string? NoiDungSua { get; set; }
    public decimal TongTien { get; set; }
    public string TrangThai { get; set; } = "Đang sửa";
    public List<RepairDetailDto> ChiTiet { get; set; } = [];
}
