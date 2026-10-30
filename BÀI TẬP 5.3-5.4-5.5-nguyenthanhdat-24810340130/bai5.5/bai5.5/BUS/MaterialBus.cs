using bai5._5.DAL;
using bai5._5.DTO;

namespace bai5._5.BUS;

public sealed class MaterialBus(MaterialDal materialDal)
{
    public List<MaterialDto> GetAll(string? keyword = null) => materialDal.GetAll(keyword);

    public void Add(MaterialDto material)
    {
        Validate(material);
        materialDal.Add(material);
    }

    public void Update(MaterialDto material)
    {
        Validate(material);
        if (!materialDal.Update(material))
            throw new InvalidOperationException("Không tìm thấy vật tư cần cập nhật.");
    }

    public void Delete(string maVatTu)
    {
        if (string.IsNullOrWhiteSpace(maVatTu))
            throw new ArgumentException("Mã vật tư không được để trống.");
        if (!materialDal.Delete(maVatTu.Trim()))
            throw new InvalidOperationException("Không tìm thấy vật tư cần xóa.");
    }

    public List<MaterialDto> GetBelowMinimum() => materialDal.GetBelowMinimum();

    public decimal GetInventoryValue() => materialDal.GetInventoryValue();

    private static void Validate(MaterialDto material)
    {
        if (string.IsNullOrWhiteSpace(material.MaVatTu) || material.MaVatTu.Trim().Length > 20)
            throw new ArgumentException("Mã vật tư bắt buộc và tối đa 20 ký tự.");
        if (string.IsNullOrWhiteSpace(material.TenVatTu) || material.TenVatTu.Trim().Length > 150)
            throw new ArgumentException("Tên vật tư bắt buộc và tối đa 150 ký tự.");
        if (material.DVT.Length > 20)
            throw new ArgumentException("Đơn vị tính tối đa 20 ký tự.");
        if (material.SoLuongToiThieu < 0)
            throw new ArgumentException("Định mức an toàn không được âm.");
        material.MaVatTu = material.MaVatTu.Trim();
        material.TenVatTu = material.TenVatTu.Trim();
        material.DVT = material.DVT.Trim();
    }
}
