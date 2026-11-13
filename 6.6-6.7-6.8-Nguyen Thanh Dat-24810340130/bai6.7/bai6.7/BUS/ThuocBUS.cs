using bai6._7.DAL;
using bai6._7.DTO;

namespace bai6._7.BUS;

public sealed class ThuocBUS
{
    private readonly ThuocDAL _thuocDAL = new();

    public IReadOnlyList<ThuocDTO> LayDanhSach(string? tuKhoa = null) => _thuocDAL.LayDanhSach(tuKhoa);

    public int Them(ThuocDTO thuoc)
    {
        Validate(thuoc);
        return _thuocDAL.Them(Normalize(thuoc));
    }

    public void CapNhat(ThuocDTO thuoc)
    {
        Validate(thuoc);
        if (thuoc.MaThuoc <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(thuoc), "Mã thuốc không hợp lệ.");
        }

        _thuocDAL.CapNhat(Normalize(thuoc));
    }

    public void Xoa(int maThuoc)
    {
        if (maThuoc <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maThuoc), "Mã thuốc không hợp lệ.");
        }

        _thuocDAL.Xoa(maThuoc);
    }

    private static void Validate(ThuocDTO thuoc)
    {
        ArgumentNullException.ThrowIfNull(thuoc);
        if (string.IsNullOrWhiteSpace(thuoc.TenThuoc) || thuoc.TenThuoc.Trim().Length > 150)
        {
            throw new ArgumentException("Tên thuốc là bắt buộc và tối đa 150 ký tự.", nameof(thuoc));
        }

        if (string.IsNullOrWhiteSpace(thuoc.DonViTinh) || thuoc.DonViTinh.Trim().Length > 30)
        {
            throw new ArgumentException("Đơn vị tính là bắt buộc và tối đa 30 ký tự.", nameof(thuoc));
        }

        if (thuoc.DonGia < 0 || thuoc.SoLuongTon < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(thuoc), "Đơn giá và số lượng tồn phải lớn hơn hoặc bằng 0.");
        }
    }

    private static ThuocDTO Normalize(ThuocDTO thuoc) => new()
    {
        MaThuoc = thuoc.MaThuoc,
        TenThuoc = thuoc.TenThuoc.Trim(),
        DonViTinh = thuoc.DonViTinh.Trim(),
        DonGia = thuoc.DonGia,
        SoLuongTon = thuoc.SoLuongTon
    };
}
