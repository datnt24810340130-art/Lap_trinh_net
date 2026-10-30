using bai5._5.DAL;
using bai5._5.DTO;

namespace bai5._5.BUS;

public sealed class SupplierBus(SupplierDal supplierDal)
{
    public List<SupplierDto> GetAll(string? keyword = null) => supplierDal.GetAll(keyword);

    public int Add(SupplierDto supplier)
    {
        Validate(supplier);
        return supplierDal.Add(supplier);
    }

    public void Update(SupplierDto supplier)
    {
        Validate(supplier);
        if (supplier.MaNCC <= 0 || !supplierDal.Update(supplier))
            throw new InvalidOperationException("Không tìm thấy nhà cung cấp cần cập nhật.");
    }

    public void Delete(int maNcc)
    {
        if (maNcc <= 0 || !supplierDal.Delete(maNcc))
            throw new InvalidOperationException("Không tìm thấy nhà cung cấp cần xóa.");
    }

    private static void Validate(SupplierDto supplier)
    {
        if (string.IsNullOrWhiteSpace(supplier.TenNCC) || supplier.TenNCC.Trim().Length > 150)
            throw new ArgumentException("Tên nhà cung cấp bắt buộc và tối đa 150 ký tự.");
        if (supplier.DienThoai?.Length > 15)
            throw new ArgumentException("Số điện thoại tối đa 15 ký tự.");
        if (supplier.DiaChi?.Length > 255)
            throw new ArgumentException("Địa chỉ tối đa 255 ký tự.");
        supplier.TenNCC = supplier.TenNCC.Trim();
        supplier.DienThoai = string.IsNullOrWhiteSpace(supplier.DienThoai) ? null : supplier.DienThoai.Trim();
        supplier.DiaChi = string.IsNullOrWhiteSpace(supplier.DiaChi) ? null : supplier.DiaChi.Trim();
    }
}
