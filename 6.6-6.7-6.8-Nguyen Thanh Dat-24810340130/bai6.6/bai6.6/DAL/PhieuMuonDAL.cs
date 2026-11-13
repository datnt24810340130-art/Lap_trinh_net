using bai6._6.DTO;
using Microsoft.Data.SqlClient;

namespace bai6._6.DAL;

public sealed class PhieuMuonDAL
{
    public List<PhieuMuonDTO> GetAll(int? maDocGia = null, string? trangThai = null, string? tenDocGia = null)
    {
        var sql = "SELECT p.MaPhieu, p.MaDocGia, d.HoTen, p.NgayMuon, p.HanTra, p.NgayTra, p.TrangThai FROM dbo.PhieuMuon p INNER JOIN dbo.DocGia d ON d.MaDocGia=p.MaDocGia WHERE 1=1";
        if (maDocGia.HasValue)
        {
            sql += " AND p.MaDocGia=@MaDocGia";
        }
        if (!string.IsNullOrWhiteSpace(trangThai) && trangThai != "Tất cả")
        {
            sql += " AND p.TrangThai=@TrangThai";
        }
        if (!string.IsNullOrWhiteSpace(tenDocGia))
        {
            sql += " AND d.HoTen LIKE @HoTen";
        }
        sql += " ORDER BY p.NgayMuon DESC, p.MaPhieu DESC";

        using SqlConnection connection = DatabaseHelper.GetConnection();
        using SqlCommand command = new(sql, connection);
        if (maDocGia.HasValue)
        {
            command.Parameters.AddWithValue("@MaDocGia", maDocGia.Value);
        }
        if (!string.IsNullOrWhiteSpace(trangThai) && trangThai != "Tất cả")
        {
            command.Parameters.AddWithValue("@TrangThai", trangThai);
        }
        if (!string.IsNullOrWhiteSpace(tenDocGia))
        {
            command.Parameters.AddWithValue("@HoTen", $"%{tenDocGia.Trim()}%");
        }

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();
        var loans = new List<PhieuMuonDTO>();
        while (reader.Read())
        {
            loans.Add(MapLoan(reader));
        }

        return loans;
    }

    public List<PhieuMuonDTO> GetOverdueLoans()
    {
        const string sql = "SELECT p.MaPhieu, p.MaDocGia, d.HoTen, p.NgayMuon, p.HanTra, p.NgayTra, p.TrangThai FROM dbo.PhieuMuon p INNER JOIN dbo.DocGia d ON d.MaDocGia=p.MaDocGia WHERE p.TrangThai=N'Đang mượn' AND p.HanTra < CONVERT(date, GETDATE()) ORDER BY p.HanTra";
        using SqlConnection connection = DatabaseHelper.GetConnection();
        using SqlCommand command = new(sql, connection);
        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();
        var overdueLoans = new List<PhieuMuonDTO>();
        while (reader.Read())
        {
            overdueLoans.Add(MapLoan(reader));
        }

        return overdueLoans;
    }

    public int CreateLoan(int maDocGia, DateTime hanTra, IReadOnlyCollection<ChiTietMuonDTO> details)
    {
        using SqlConnection connection = DatabaseHelper.GetConnection();
        connection.Open();
        using SqlTransaction transaction = connection.BeginTransaction();

        try
        {
            foreach (ChiTietMuonDTO detail in details)
            {
                using SqlCommand stockCommand = new("UPDATE dbo.DauSach SET SoLuongCon=SoLuongCon-@SoLuong WHERE MaSach=@MaSach AND SoLuongCon>=@SoLuong", connection, transaction);
                stockCommand.Parameters.AddWithValue("@SoLuong", detail.SoLuong);
                stockCommand.Parameters.AddWithValue("@MaSach", detail.MaSach);
                if (stockCommand.ExecuteNonQuery() != 1)
                {
                    throw new InvalidOperationException($"Sách mã {detail.MaSach} không tồn tại hoặc không đủ số lượng còn.");
                }
            }

            int maPhieu;
            using (SqlCommand loanCommand = new("INSERT dbo.PhieuMuon (MaDocGia, NgayMuon, HanTra, TrangThai) VALUES (@MaDocGia, @NgayMuon, @HanTra, N'Đang mượn'); SELECT CAST(SCOPE_IDENTITY() AS int);", connection, transaction))
            {
                loanCommand.Parameters.AddWithValue("@MaDocGia", maDocGia);
                loanCommand.Parameters.AddWithValue("@NgayMuon", DateTime.Today);
                loanCommand.Parameters.AddWithValue("@HanTra", hanTra.Date);
                maPhieu = (int)loanCommand.ExecuteScalar()!;
            }

            foreach (ChiTietMuonDTO detail in details)
            {
                using SqlCommand detailCommand = new("INSERT dbo.ChiTietMuon (MaPhieu, MaSach, SoLuong) VALUES (@MaPhieu, @MaSach, @SoLuong)", connection, transaction);
                detailCommand.Parameters.AddWithValue("@MaPhieu", maPhieu);
                detailCommand.Parameters.AddWithValue("@MaSach", detail.MaSach);
                detailCommand.Parameters.AddWithValue("@SoLuong", detail.SoLuong);
                detailCommand.ExecuteNonQuery();
            }

            transaction.Commit();
            return maPhieu;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public void ReturnLoan(int maPhieu)
    {
        using SqlConnection connection = DatabaseHelper.GetConnection();
        connection.Open();
        using SqlTransaction transaction = connection.BeginTransaction();

        try
        {
            using (SqlCommand updateLoan = new("UPDATE dbo.PhieuMuon SET TrangThai=N'Đã trả', NgayTra=CONVERT(date, GETDATE()) WHERE MaPhieu=@MaPhieu AND TrangThai=N'Đang mượn'", connection, transaction))
            {
                updateLoan.Parameters.AddWithValue("@MaPhieu", maPhieu);
                if (updateLoan.ExecuteNonQuery() != 1)
                {
                    throw new InvalidOperationException("Phiếu mượn không tồn tại hoặc đã được trả.");
                }
            }

            var details = new List<(int MaSach, int SoLuong)>();
            using (SqlCommand detailCommand = new("SELECT MaSach, SoLuong FROM dbo.ChiTietMuon WHERE MaPhieu=@MaPhieu", connection, transaction))
            {
                detailCommand.Parameters.AddWithValue("@MaPhieu", maPhieu);
                using SqlDataReader reader = detailCommand.ExecuteReader();
                while (reader.Read())
                {
                    details.Add((reader.GetInt32(0), reader.GetInt32(1)));
                }
            }

            foreach ((int maSach, int soLuong) in details)
            {
                using SqlCommand stockCommand = new("UPDATE dbo.DauSach SET SoLuongCon=SoLuongCon+@SoLuong WHERE MaSach=@MaSach AND SoLuongCon+@SoLuong<=SoLuong", connection, transaction);
                stockCommand.Parameters.AddWithValue("@SoLuong", soLuong);
                stockCommand.Parameters.AddWithValue("@MaSach", maSach);
                if (stockCommand.ExecuteNonQuery() != 1)
                {
                    throw new InvalidOperationException($"Không thể cập nhật tồn kho cho sách mã {maSach}.");
                }
            }

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    private static PhieuMuonDTO MapLoan(SqlDataReader reader) => new()
    {
        MaPhieu = reader.GetInt32(0),
        MaDocGia = reader.GetInt32(1),
        HoTen = reader.GetString(2),
        NgayMuon = reader.GetDateTime(3),
        HanTra = reader.GetDateTime(4),
        NgayTra = reader.IsDBNull(5) ? null : reader.GetDateTime(5),
        TrangThai = reader.GetString(6)
    };
}
