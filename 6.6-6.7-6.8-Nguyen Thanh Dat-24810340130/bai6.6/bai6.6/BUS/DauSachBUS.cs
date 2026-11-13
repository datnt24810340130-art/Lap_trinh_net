using bai6._6.DAL;
using bai6._6.DTO;

namespace bai6._6.BUS;

public sealed class DauSachBUS
{
    private readonly DauSachDAL _dal = new();

    public List<DauSachDTO> GetAll() => _dal.GetAll();

    public int Insert(DauSachDTO book)
    {
        Validate(book);
        return _dal.Insert(book);
    }

    public bool Update(DauSachDTO book)
    {
        if (book.MaSach <= 0)
        {
            throw new ArgumentException("Mã sách không hợp lệ.");
        }
        Validate(book);
        return _dal.Update(book);
    }

    public bool Delete(int maSach)
    {
        if (maSach <= 0)
        {
            throw new ArgumentException("Mã sách không hợp lệ.");
        }
        return _dal.Delete(maSach);
    }

    public List<TheLoaiThongKeDTO> GetStatisticsByGenre() => _dal.GetStatisticsByGenre();

    private static void Validate(DauSachDTO book)
    {
        if (string.IsNullOrWhiteSpace(book.TenSach) || string.IsNullOrWhiteSpace(book.TheLoai))
        {
            throw new ArgumentException("Tên sách và thể loại không được để trống.");
        }
        if (book.SoLuong <= 0 || book.SoLuongCon < 0 || book.SoLuongCon > book.SoLuong)
        {
            throw new ArgumentException("Số lượng phải lớn hơn 0 và số lượng còn phải từ 0 đến tổng số lượng.");
        }
    }
}
