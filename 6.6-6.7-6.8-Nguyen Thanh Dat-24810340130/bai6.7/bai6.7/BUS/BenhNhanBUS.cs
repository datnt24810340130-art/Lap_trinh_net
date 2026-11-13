using bai6._7.DAL;
using bai6._7.DTO;
using System.Text.RegularExpressions;

namespace bai6._7.BUS;

public sealed class BenhNhanBUS
{
    private readonly BenhNhanDAL _benhNhanDAL = new();

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
        if (string.IsNullOrWhiteSpace(benhNhan.HoTen) || benhNhan.HoTen.Trim().Length > 150)
        {
            throw new ArgumentException("Họ tên bệnh nhân là bắt buộc và tối đa 150 ký tự.", nameof(benhNhan));
        }

        ValidatePhone(benhNhan.SoDienThoai);
    }

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
