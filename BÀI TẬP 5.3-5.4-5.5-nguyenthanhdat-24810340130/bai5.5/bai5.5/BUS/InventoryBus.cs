using bai5._5.DAL;
using bai5._5.DTO;

namespace bai5._5.BUS;

public sealed class InventoryBus(InventoryDal inventoryDal)
{
    public int Save(InventoryDocumentDto document)
    {
        if (document.LoaiPhieu is not ("NHAP" or "XUAT"))
            throw new ArgumentException("Loại phiếu chỉ nhận NHAP hoặc XUAT.");
        if (document.LoaiPhieu == "XUAT" && document.MaNCC.HasValue)
            throw new ArgumentException("Phiếu xuất không sử dụng nhà cung cấp.");
        if (document.ChiTiet.Count == 0)
            throw new ArgumentException("Phiếu kho phải có ít nhất một vật tư.");
        if (document.ChiTiet.Any(line => string.IsNullOrWhiteSpace(line.MaVatTu) || line.MaVatTu.Trim().Length > 20))
            throw new ArgumentException("Mã vật tư trong chi tiết không hợp lệ.");
        if (document.ChiTiet.Any(line => line.SoLuong <= 0 || line.DonGia < 0))
            throw new ArgumentException("Số lượng phải lớn hơn 0, đơn giá không được âm.");
        if (document.ChiTiet.GroupBy(line => line.MaVatTu, StringComparer.OrdinalIgnoreCase).Any(group => group.Count() > 1))
            throw new ArgumentException("Mỗi vật tư chỉ được xuất hiện một lần trong một phiếu.");

        foreach (var line in document.ChiTiet)
            line.MaVatTu = line.MaVatTu.Trim();

        return inventoryDal.Save(document);
    }
}
