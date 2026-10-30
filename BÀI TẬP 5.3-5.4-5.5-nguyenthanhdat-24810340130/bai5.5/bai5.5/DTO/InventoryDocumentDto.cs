namespace bai5._5.DTO;

public sealed class InventoryDocumentDto
{
    public string LoaiPhieu { get; set; } = string.Empty;
    public int? MaNCC { get; set; }
    public List<InventoryDocumentLineDto> ChiTiet { get; set; } = [];
}

public sealed class InventoryDocumentLineDto
{
    public string MaVatTu { get; set; } = string.Empty;
    public decimal SoLuong { get; set; }
    public decimal DonGia { get; set; }
}
