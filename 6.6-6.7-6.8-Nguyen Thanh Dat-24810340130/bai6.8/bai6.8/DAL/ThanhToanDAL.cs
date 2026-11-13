using Microsoft.Data.SqlClient;

namespace bai6._8.DAL;

public sealed record ThanhToanResult(decimal SoGio, decimal TienGioChoi, decimal TienDichVu, decimal TongTien);

public sealed class ThanhToanDAL
{
    public ThanhToanResult ThanhToan(int maHD, DateTime gioKetThuc, decimal soGio, decimal tienGioChoi)
    {
        using var connection = DatabaseHelper.CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
        try
        {
            int maBan;
            using (var billCommand = new SqlCommand(
                "SELECT MaBan FROM dbo.HoaDon WITH (UPDLOCK, HOLDLOCK) WHERE MaHD = @MaHD AND TrangThai = N'Đang mở';",
                connection, transaction))
            {
                billCommand.Parameters.AddWithValue("@MaHD", maHD);
                var value = billCommand.ExecuteScalar();
                if (value is null)
                    throw new InvalidOperationException("Hóa đơn không tồn tại hoặc đã được thanh toán.");
                maBan = Convert.ToInt32(value);
            }

            decimal tienDichVu;
            using (var totalCommand = new SqlCommand(
                "SELECT COALESCE(SUM(ThanhTien), 0) FROM dbo.ChiTietHoaDon WHERE MaHD = @MaHD;", connection, transaction))
            {
                totalCommand.Parameters.AddWithValue("@MaHD", maHD);
                tienDichVu = Convert.ToDecimal(totalCommand.ExecuteScalar());
            }

            var tongTien = tienGioChoi + tienDichVu;
            using (var updateBill = new SqlCommand(
                "UPDATE dbo.HoaDon SET GioKetThuc = @GioKetThuc, SoGio = @SoGio, TienGioChoi = @TienGioChoi, " +
                "TienDichVu = @TienDichVu, TongTien = @TongTien, TrangThai = N'Đã thanh toán' " +
                "WHERE MaHD = @MaHD AND TrangThai = N'Đang mở';", connection, transaction))
            {
                updateBill.Parameters.AddWithValue("@GioKetThuc", gioKetThuc);
                updateBill.Parameters.AddWithValue("@SoGio", soGio);
                updateBill.Parameters.AddWithValue("@TienGioChoi", tienGioChoi);
                updateBill.Parameters.AddWithValue("@TienDichVu", tienDichVu);
                updateBill.Parameters.AddWithValue("@TongTien", tongTien);
                updateBill.Parameters.AddWithValue("@MaHD", maHD);
                if (updateBill.ExecuteNonQuery() != 1)
                    throw new InvalidOperationException("Không thể cập nhật hóa đơn.");
            }

            int detailCount;
            using (var countCommand = new SqlCommand("SELECT COUNT(*) FROM dbo.ChiTietHoaDon WHERE MaHD = @MaHD;", connection, transaction))
            {
                countCommand.Parameters.AddWithValue("@MaHD", maHD);
                detailCount = Convert.ToInt32(countCommand.ExecuteScalar());
            }

            using (var stockCommand = new SqlCommand(
                "UPDATE dv SET dv.TonKho = dv.TonKho - ct.SoLuong FROM dbo.MenuDichVu dv " +
                "INNER JOIN dbo.ChiTietHoaDon ct ON ct.MaDV = dv.MaDV " +
                "WHERE ct.MaHD = @MaHD AND dv.TonKho >= ct.SoLuong;", connection, transaction))
            {
                stockCommand.Parameters.AddWithValue("@MaHD", maHD);
                if (stockCommand.ExecuteNonQuery() != detailCount)
                    throw new InvalidOperationException("Tồn kho không đủ để thanh toán. Giao dịch đã được hủy.");
            }

            using (var updateTable = new SqlCommand("UPDATE dbo.BanBida SET TrangThai = N'Trống' WHERE MaBan = @MaBan AND TrangThai = N'Đang chơi';", connection, transaction))
            {
                updateTable.Parameters.AddWithValue("@MaBan", maBan);
                if (updateTable.ExecuteNonQuery() != 1)
                    throw new InvalidOperationException("Không thể cập nhật trạng thái bàn.");
            }

            transaction.Commit();
            return new ThanhToanResult(soGio, tienGioChoi, tienDichVu, tongTien);
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
}
