using bai6._7.Database;
using bai6._7.DTO;
using Microsoft.Data.SqlClient;
using System.Data;

namespace bai6._7.DAL;

public sealed class LichSuKhamDAL
{
    public List<LichSuKhamDTO> TimKiem(string? tuKhoa = null)
    {
        List<LichSuKhamDTO> history = [];
        using SqlConnection connection = DatabaseHelper.GetConnection();
        using SqlCommand command = new(
            "SELECT h.MaHoSo, h.MaBN, b.HoTen, b.SoDienThoai, h.NgayKham, h.ChanDoan, h.TienKham, d.SoDonThuoc, d.TienThuoc, h.TrangThai FROM dbo.HoSoKham h INNER JOIN dbo.BenhNhan b ON b.MaBN = h.MaBN OUTER APPLY (SELECT COUNT(DISTINCT don.MaDon) AS SoDonThuoc, COALESCE(SUM(ct.SoLuong * ct.DonGia), 0) AS TienThuoc FROM dbo.DonThuoc don LEFT JOIN dbo.ChiTietDonThuoc ct ON ct.MaDon = don.MaDon WHERE don.MaHoSo = h.MaHoSo) d WHERE @TuKhoa = N'' OR b.HoTen LIKE @Mau OR b.SoDienThoai LIKE @Mau OR h.ChanDoan LIKE @Mau ORDER BY h.NgayKham DESC, h.MaHoSo DESC;",
            connection);
        string keyword = tuKhoa?.Trim() ?? string.Empty;
        command.Parameters.Add("@TuKhoa", SqlDbType.NVarChar, 300).Value = keyword;
        command.Parameters.Add("@Mau", SqlDbType.NVarChar, 302).Value = $"%{keyword}%";
        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            history.Add(new LichSuKhamDTO
            {
                MaHoSo = reader.GetInt32(0),
                MaBN = reader.GetInt32(1),
                HoTen = reader.GetString(2),
                SoDienThoai = reader.GetString(3),
                NgayKham = reader.GetDateTime(4),
                ChanDoan = reader.GetString(5),
                TienKham = reader.GetDecimal(6),
                SoDonThuoc = reader.GetInt32(7),
                TienThuoc = reader.GetDecimal(8),
                TrangThai = reader.GetString(9)
            });
        }

        return history;
    }

    public List<ChiTietDonThuocDTO> LayChiTietDonThuoc(int maHoSo)
    {
        List<ChiTietDonThuocDTO> details = [];
        using SqlConnection connection = DatabaseHelper.GetConnection();
        using SqlCommand command = new(
            "SELECT d.MaDon, ct.MaThuoc, t.TenThuoc, t.DonViTinh, ct.SoLuong, ct.DonGia FROM dbo.DonThuoc d INNER JOIN dbo.ChiTietDonThuoc ct ON ct.MaDon = d.MaDon INNER JOIN dbo.Thuoc t ON t.MaThuoc = ct.MaThuoc WHERE d.MaHoSo = @MaHoSo ORDER BY d.MaDon, t.TenThuoc;",
            connection);
        command.Parameters.Add("@MaHoSo", SqlDbType.Int).Value = maHoSo;
        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            details.Add(new ChiTietDonThuocDTO
            {
                MaDon = reader.GetInt32(0),
                MaThuoc = reader.GetInt32(1),
                TenThuoc = reader.GetString(2),
                DonViTinh = reader.GetString(3),
                SoLuong = reader.GetInt32(4),
                DonGia = reader.GetDecimal(5)
            });
        }

        return details;
    }
}
