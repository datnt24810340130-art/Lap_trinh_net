using bai6._6.DAL;
using bai6._6.DTO;

namespace bai6._6.BUS;

public sealed class PhieuMuonBUS
{
    private readonly PhieuMuonDAL _loanDal = new();
    private readonly DocGiaDAL _readerDal = new();

    public List<PhieuMuonDTO> GetAll(int? maDocGia = null, string? trangThai = null, string? tenDocGia = null) =>
        _loanDal.GetAll(maDocGia, trangThai, tenDocGia);

    public List<PhieuMuonDTO> GetOverdueLoans() => _loanDal.GetOverdueLoans();

    public int CreateLoan(int maDocGia, DateTime hanTra, IReadOnlyCollection<ChiTietMuonDTO> details)
    {
        if (maDocGia <= 0)
        {
            throw new ArgumentException("Vui lòng nhập mã độc giả hợp lệ.");
        }
        if (hanTra.Date < DateTime.Today)
        {
            throw new ArgumentException("Hạn trả phải từ hôm nay trở đi.");
        }
        if (details is null || details.Count == 0)
        {
            throw new ArgumentException("Phiếu mượn phải có ít nhất một đầu sách.");
        }
        if (details.Any(detail => detail.MaSach <= 0 || detail.SoLuong <= 0))
        {
            throw new ArgumentException("Mã sách và số lượng mượn phải lớn hơn 0.");
        }

        DocGiaDTO? reader = _readerDal.GetById(maDocGia);
        if (reader is null)
        {
            throw new InvalidOperationException("Không tìm thấy độc giả.");
        }
        if (reader.NgayHetHanThe.Date < DateTime.Now.Date)
        {
            throw new InvalidOperationException("Thẻ độc giả đã hết hạn; không thể lập phiếu mượn.");
        }

        List<ChiTietMuonDTO> normalizedDetails = details
            .GroupBy(detail => detail.MaSach)
            .Select(group => new ChiTietMuonDTO
            {
                MaSach = group.Key,
                SoLuong = group.Sum(detail => detail.SoLuong)
            })
            .ToList();

        return _loanDal.CreateLoan(maDocGia, hanTra, normalizedDetails);
    }

    public void ReturnLoan(int maPhieu)
    {
        if (maPhieu <= 0)
        {
            throw new ArgumentException("Mã phiếu mượn không hợp lệ.");
        }
        _loanDal.ReturnLoan(maPhieu);
    }
}
