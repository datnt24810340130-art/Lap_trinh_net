using bai6._6.DAL;
using bai6._6.DTO;

namespace bai6._6.BUS;

public sealed class DocGiaBUS
{
    private readonly DocGiaDAL _dal = new();

    public List<DocGiaDTO> GetAll() => _dal.GetAll();

    public int Insert(DocGiaDTO reader)
    {
        Validate(reader);
        return _dal.Insert(reader);
    }

    public bool Update(DocGiaDTO reader)
    {
        if (reader.MaDocGia <= 0)
        {
            throw new ArgumentException("Mã độc giả không hợp lệ.");
        }
        Validate(reader);
        return _dal.Update(reader);
    }

    public bool Delete(int maDocGia)
    {
        if (maDocGia <= 0)
        {
            throw new ArgumentException("Mã độc giả không hợp lệ.");
        }
        return _dal.Delete(maDocGia);
    }

    private static void Validate(DocGiaDTO reader)
    {
        if (string.IsNullOrWhiteSpace(reader.HoTen))
        {
            throw new ArgumentException("Họ tên độc giả không được để trống.");
        }
        if (reader.NgayHetHanThe.Date < DateTime.Today)
        {
            throw new ArgumentException("Ngày hết hạn thẻ phải từ hôm nay trở đi.");
        }
    }
}
