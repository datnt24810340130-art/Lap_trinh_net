using bai6._7.DAL;
using bai6._7.DTO;
using System.Text.RegularExpressions;

namespace bai6._7.BUS;

public sealed class BenhNhanBUS
{
    private readonly BenhNhanDAL _benhNhanDAL = new();

    public IReadOnlyList<BenhNhanDTO> LayDanhSach(string? tuKhoa = null)
    {
        return _benhNhanDAL.LayDanhSach(tuKhoa);
    }

    public int ThemBenhNhan(BenhNhanDTO benhNhan)
    {
        ValidatePatient(benhNhan);
        return _benhNhanDAL.ThemBenhNhan(NormalizePatient(benhNhan));
    }

    public void CapNhatBenhNhan(BenhNhanDTO benhNhan)
    {
        ValidatePatient(benhNhan);
        if (benhNhan.MaBN <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(benhNhan), "Mã bệnh nhân không hợp lệ.");
        }

        _benhNhanDAL.CapNhatBenhNhan(NormalizePatient(benhNhan));
    }

    public void XoaBenhNhan(int maBN)
    {
        if (maBN <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maBN), "Mã bệnh nhân không hợp lệ.");
        }

        _benhNhanDAL.XoaBenhNhan(maBN);
    }

    public BenhNhanDTO? TimTheoSoDienThoai(string soDienThoai)
    {
        ValidatePhone(soDienThoai);
        return _benhNhanDAL.TimTheoSoDienThoai(soDienThoai.Trim());
    }

    public int TaoBenhNhanVaHoSo(BenhNhanDTO benhNhan, string chanDoan, decimal tienKham)
    {
        ArgumentNullException.ThrowIfNull(benhNhan);
        ValidatePatient(benhNhan);
        ValidateVisit(chanDoan, tienKham);
        return _benhNhanDAL.TaoBenhNhanVaHoSo(benhNhan, chanDoan.Trim(), tienKham);
    }

    public int TaoHoSoKham(int maBN, string chanDoan, decimal tienKham)
    {
        if (maBN <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maBN), "Bệnh nhân chưa hợp lệ.");
        }

        ValidateVisit(chanDoan, tienKham);
        return _benhNhanDAL.TaoHoSoKham(maBN, chanDoan.Trim(), tienKham);
    }

    private static void ValidatePatient(BenhNhanDTO benhNhan)
    {
        ArgumentNullException.ThrowIfNull(benhNhan);
        if (string.IsNullOrWhiteSpace(benhNhan.HoTen) || benhNhan.HoTen.Trim().Length > 150)
        {
            throw new ArgumentException("Họ tên bệnh nhân là bắt buộc và tối đa 150 ký tự.", nameof(benhNhan));
        }

        ValidatePhone(benhNhan.SoDienThoai);
        if (benhNhan.NgaySinh > DateTime.Today)
        {
            throw new ArgumentException("Ngày sinh không thể ở tương lai.", nameof(benhNhan));
        }

        if ((benhNhan.GioiTinh?.Trim().Length ?? 0) > 20 || (benhNhan.DiaChi?.Trim().Length ?? 0) > 300)
        {
            throw new ArgumentException("Giới tính tối đa 20 ký tự và địa chỉ tối đa 300 ký tự.", nameof(benhNhan));
        }
    }

    private static BenhNhanDTO NormalizePatient(BenhNhanDTO patient) => new()
    {
        MaBN = patient.MaBN,
        HoTen = patient.HoTen.Trim(),
        SoDienThoai = patient.SoDienThoai.Trim(),
        NgaySinh = patient.NgaySinh,
        GioiTinh = string.IsNullOrWhiteSpace(patient.GioiTinh) ? null : patient.GioiTinh.Trim(),
        DiaChi = string.IsNullOrWhiteSpace(patient.DiaChi) ? null : patient.DiaChi.Trim()
    };

    private static void ValidatePhone(string soDienThoai)
    {
        if (string.IsNullOrWhiteSpace(soDienThoai) || !Regex.IsMatch(soDienThoai.Trim(), @"^\d{9,11}$"))
        {
            throw new ArgumentException("Số điện thoại phải gồm 9 đến 11 chữ số.", nameof(soDienThoai));
        }
    }

    private static void ValidateVisit(string chanDoan, decimal tienKham)
    {
        if (string.IsNullOrWhiteSpace(chanDoan) || chanDoan.Trim().Length > 500)
        {
            throw new ArgumentException("Chẩn đoán là bắt buộc và tối đa 500 ký tự.", nameof(chanDoan));
        }

        if (tienKham < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(tienKham), "Tiền khám không thể âm.");
        }
    }
}
