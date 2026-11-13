using bai6._7.Database;
using Microsoft.Data.SqlClient;
using System.Data;

namespace bai6._7.DAL;

public sealed class ThanhToanDAL
{
    public decimal ThanhToanVaTinhTongTien(int maHoSo)
    {
        using SqlConnection connection = DatabaseHelper.GetConnection();
        connection.Open();
        using SqlTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);

        try
        {
            using SqlCommand totalCommand = new(
                "SELECT h.TienKham + COALESCE(SUM(ct.SoLuong * ct.DonGia), 0) FROM dbo.HoSoKham h LEFT JOIN dbo.DonThuoc d ON d.MaHoSo = h.MaHoSo LEFT JOIN dbo.ChiTietDonThuoc ct ON ct.MaDon = d.MaDon WHERE h.MaHoSo = @MaHoSo GROUP BY h.TienKham;",
                connection, transaction);
            totalCommand.Parameters.Add("@MaHoSo", SqlDbType.Int).Value = maHoSo;
            object? result = totalCommand.ExecuteScalar();
            if (result is null || result is DBNull)
            {
                throw new InvalidOperationException("Không tìm thấy hồ sơ khám cần thanh toán.");
            }

            decimal total = Convert.ToDecimal(result);
            using SqlCommand paymentCommand = new(
                "UPDATE dbo.HoSoKham SET TrangThai = @TrangThai WHERE MaHoSo = @MaHoSo;",
                connection, transaction);
            paymentCommand.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 30).Value = "Đã thanh toán";
            paymentCommand.Parameters.Add("@MaHoSo", SqlDbType.Int).Value = maHoSo;
            paymentCommand.ExecuteNonQuery();
            transaction.Commit();
            return total;
        }
        catch
        {
            if (transaction.Connection is not null)
            {
                transaction.Rollback();
            }

            throw;
        }
    }
}
