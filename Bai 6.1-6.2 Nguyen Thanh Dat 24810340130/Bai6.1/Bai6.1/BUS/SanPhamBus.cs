using Bai6._1.DAL;
using Bai6._1.DTO;

namespace Bai6._1.BUS;

public sealed class SanPhamBus
{
    private readonly SanPhamDal _dal = new();

    public List<SanPhamDto> GetAll() => _dal.GetAll();
    public List<SanPhamDto> Search(string keyword) => string.IsNullOrWhiteSpace(keyword) ? GetAll() : _dal.Search(keyword.Trim());

    public void Add(SanPhamDto product)
    {
        Validate(product);
        _dal.Add(product);
    }

    public bool Update(SanPhamDto product)
    {
        Validate(product);
        return _dal.Update(product);
    }

    public bool Delete(string maSP)
    {
        if (string.IsNullOrWhiteSpace(maSP))
            throw new ArgumentException("Mã sản phẩm không được để trống.");
        return _dal.Delete(maSP.Trim());
    }

    private static void Validate(SanPhamDto product)
    {
        if (string.IsNullOrWhiteSpace(product.MaSP) || product.MaSP.Trim().Length > 20)
            throw new ArgumentException("Mã sản phẩm bắt buộc và tối đa 20 ký tự.");
        if (string.IsNullOrWhiteSpace(product.TenSP) || product.TenSP.Trim().Length > 150)
            throw new ArgumentException("Tên sản phẩm bắt buộc và tối đa 150 ký tự.");
        if (product.GiaBan <= 0 || product.SoLuongTon < 0)
            throw new ArgumentException("Giá bán phải lớn hơn 0 và tồn kho không được âm.");
        product.MaSP = product.MaSP.Trim();
        product.TenSP = product.TenSP.Trim();
    }
}
