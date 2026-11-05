using Bai6._1.DTO;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Bai6._1.DAL;

public sealed class BanHangDal
{
    public KetQuaThanhToanDto ThanhToan(HoaDonDto invoice)
    {
        if (invoice.ChiTiet.Count == 0)
            throw new InvalidOperationException("Giỏ hàng đang trống.");

        using var connection = DbConnectionFactory.CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction(IsolationLevel.Serializable);

        try
        {
            var preparedLines = new List<ChiTietHoaDonDto>();
            foreach (var line in invoice.ChiTiet.OrderBy(item => item.MaSP, StringComparer.Ordinal))
            {
                if (line.SoLuong <= 0)
                    throw new InvalidOperationException("Số lượng bán phải lớn hơn 0.");

                using var productCommand = new SqlCommand(
                    "SELECT TenSP, GiaBan, SoLuongTon FROM dbo.SanPham WITH (UPDLOCK, HOLDLOCK) WHERE MaSP=@maSP",
                    connection, transaction);
                productCommand.Parameters.Add("@maSP", SqlDbType.VarChar, 20).Value = line.MaSP;
                using var reader = productCommand.ExecuteReader();
                if (!reader.Read())
                    throw new InvalidOperationException($"Không tìm thấy sản phẩm {line.MaSP}.");

                var name = reader.GetString(0);
                var price = reader.GetDecimal(1);
                var stock = reader.GetInt32(2);
                reader.Close();

                if (stock < line.SoLuong)
                    throw new InvalidOperationException($"Sản phẩm {name} không đủ tồn kho. Còn {stock}.");

                preparedLines.Add(new ChiTietHoaDonDto
                {
                    MaSP = line.MaSP,
                    TenSP = name,
                    SoLuong = line.SoLuong,
                    DonGia = price
                });
            }

            var total = preparedLines.Sum(line => line.ThanhTien);
            using var invoiceCommand = new SqlCommand(
                "INSERT INTO dbo.HoaDon (MaNV, MaKH, TongTien) OUTPUT INSERTED.MaHD VALUES (@maNV, @maKH, @tongTien)",
                connection, transaction);
            invoiceCommand.Parameters.Add("@maNV", SqlDbType.Int).Value = invoice.MaNV;
            invoiceCommand.Parameters.Add("@maKH", SqlDbType.Int).Value = (object?)invoice.MaKH ?? DBNull.Value;
            var totalParameter = invoiceCommand.Parameters.Add("@tongTien", SqlDbType.Decimal);
            totalParameter.Precision = 18;
            totalParameter.Scale = 2;
            totalParameter.Value = total;
            var invoiceId = (int)invoiceCommand.ExecuteScalar()!;

            foreach (var line in preparedLines)
            {
                using var detailCommand = new SqlCommand(
                    "INSERT INTO dbo.ChiTietHoaDon (MaHD, MaSP, SoLuong, DonGia) VALUES (@maHD, @maSP, @soLuong, @donGia)",
                    connection, transaction);
                detailCommand.Parameters.Add("@maHD", SqlDbType.Int).Value = invoiceId;
                detailCommand.Parameters.Add("@maSP", SqlDbType.VarChar, 20).Value = line.MaSP;
                detailCommand.Parameters.Add("@soLuong", SqlDbType.Int).Value = line.SoLuong;
                var priceParameter = detailCommand.Parameters.Add("@donGia", SqlDbType.Decimal);
                priceParameter.Precision = 18;
                priceParameter.Scale = 2;
                priceParameter.Value = line.DonGia;
                detailCommand.ExecuteNonQuery();

                using var stockCommand = new SqlCommand(
                    "UPDATE dbo.SanPham SET SoLuongTon=SoLuongTon-@soLuong WHERE MaSP=@maSP AND SoLuongTon>=@soLuong",
                    connection, transaction);
                stockCommand.Parameters.Add("@soLuong", SqlDbType.Int).Value = line.SoLuong;
                stockCommand.Parameters.Add("@maSP", SqlDbType.VarChar, 20).Value = line.MaSP;
                if (stockCommand.ExecuteNonQuery() != 1)
                    throw new InvalidOperationException($"Tồn kho sản phẩm {line.MaSP} đã thay đổi. Vui lòng thử lại.");
            }

            var points = (int)(total / 100000m);
            if (invoice.MaKH.HasValue && points > 0)
            {
                using var loyaltyCommand = new SqlCommand(
                    "UPDATE dbo.KhachHang SET DiemTichLuy=DiemTichLuy+@diem WHERE MaKH=@maKH",
                    connection, transaction);
                loyaltyCommand.Parameters.Add("@diem", SqlDbType.Int).Value = points;
                loyaltyCommand.Parameters.Add("@maKH", SqlDbType.Int).Value = invoice.MaKH.Value;
                if (loyaltyCommand.ExecuteNonQuery() != 1)
                    throw new InvalidOperationException("Không tìm thấy khách hàng để cộng điểm.");
            }

            transaction.Commit();
            return new KetQuaThanhToanDto(invoiceId, total, invoice.MaKH.HasValue ? points : 0);
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public decimal GetRevenue(DateTime fromInclusive, DateTime toExclusive)
    {
        const string sql = "SELECT COALESCE(SUM(TongTien), 0) FROM dbo.HoaDon WHERE NgayLap>=@from AND NgayLap<@to";
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@from", SqlDbType.DateTime).Value = fromInclusive;
        command.Parameters.Add("@to", SqlDbType.DateTime).Value = toExclusive;
        connection.Open();
        return Convert.ToDecimal(command.ExecuteScalar());
    }

    public List<SanPhamBanChayDto> GetTopSellingProducts()
    {
        const string sql = "SELECT TOP (5) sp.MaSP, sp.TenSP, SUM(ct.SoLuong) AS TongSoLuong FROM dbo.ChiTietHoaDon ct INNER JOIN dbo.SanPham sp ON sp.MaSP=ct.MaSP GROUP BY sp.MaSP, sp.TenSP ORDER BY SUM(ct.SoLuong) DESC, sp.TenSP";
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand(sql, connection);
        connection.Open();
        using var reader = command.ExecuteReader();
        var products = new List<SanPhamBanChayDto>();
        while (reader.Read())
        {
            products.Add(new SanPhamBanChayDto
            {
                MaSP = reader.GetString(0),
                TenSP = reader.GetString(1),
                TongSoLuong = reader.GetInt32(2)
            });
        }
        return products;
    }
}
