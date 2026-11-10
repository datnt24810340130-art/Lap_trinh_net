using bai6._5.DTO;
using Microsoft.Data.SqlClient;
using System.Data;

namespace bai6._5.DAL;

public sealed class KaraokeDal
{
    public List<RoomDto> GetRooms()
    {
        const string sql = """
            SELECT p.MaPhong, p.TenPhong, p.LoaiPhong, p.GiaTheoGio, p.TrangThai,
                   h.MaHD, h.GioVao
            FROM dbo.PhongHat AS p
            OUTER APPLY
            (
                SELECT TOP (1) hd.MaHD, hd.GioVao
                FROM dbo.HoaDon AS hd
                WHERE hd.MaPhong = p.MaPhong AND hd.TrangThai = 0
                ORDER BY hd.MaHD DESC
            ) AS h
            ORDER BY p.MaPhong;
            """;
        var rooms = new List<RoomDto>();
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand(sql, connection);
        connection.Open();
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            rooms.Add(new RoomDto
            {
                MaPhong = reader.GetInt32(0),
                TenPhong = reader.GetString(1),
                LoaiPhong = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                GiaTheoGio = reader.GetDecimal(3),
                TrangThai = reader.GetString(4),
                MaHD = reader.IsDBNull(5) ? null : reader.GetInt32(5),
                GioVao = reader.IsDBNull(6) ? null : reader.GetDateTime(6)
            });
        }
        return rooms;
    }

    public List<ServiceDto> GetServices()
    {
        const string sql = "SELECT MaDV, TenDV, DonGia, TonKho FROM dbo.DichVu ORDER BY TenDV;";
        var services = new List<ServiceDto>();
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand(sql, connection);
        connection.Open();
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            services.Add(new ServiceDto
            {
                MaDV = reader.GetInt32(0),
                TenDV = reader.GetString(1),
                DonGia = reader.GetDecimal(2),
                TonKho = reader.GetInt32(3)
            });
        }
        return services;
    }

    public DataTable GetBillServices(int billId)
    {
        const string sql = """
            SELECT ct.MaDV, dv.TenDV, ct.SoLuong, ct.DonGia,
                   CAST(ct.SoLuong * ct.DonGia AS decimal(18,2)) AS ThanhTien
            FROM dbo.ChiTietDichVu AS ct
            INNER JOIN dbo.DichVu AS dv ON dv.MaDV = ct.MaDV
            WHERE ct.MaHD = @MaHD
            ORDER BY dv.TenDV;
            """;
        using var connection = DbConnectionFactory.CreateConnection();
        using var adapter = new SqlDataAdapter(sql, connection);
        adapter.SelectCommand!.Parameters.Add("@MaHD", SqlDbType.Int).Value = billId;
        var table = new DataTable();
        adapter.Fill(table);
        return table;
    }

    public int OpenRoom(int roomId)
    {
        using var connection = DbConnectionFactory.CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction(IsolationLevel.Serializable);
        try
        {
            using (var updateRoom = new SqlCommand(
                "UPDATE dbo.PhongHat SET TrangThai = N'Đang hát' WHERE MaPhong = @MaPhong AND TrangThai = N'Trống';",
                connection, transaction))
            {
                updateRoom.Parameters.Add("@MaPhong", SqlDbType.Int).Value = roomId;
                if (updateRoom.ExecuteNonQuery() != 1)
                    throw new InvalidOperationException("Phòng không còn trống hoặc không tồn tại.");
            }

            int billId;
            using (var insertBill = new SqlCommand(
                "INSERT INTO dbo.HoaDon (MaPhong, GioVao) VALUES (@MaPhong, GETDATE()); SELECT CONVERT(int, SCOPE_IDENTITY());",
                connection, transaction))
            {
                insertBill.Parameters.Add("@MaPhong", SqlDbType.Int).Value = roomId;
                billId = (int)insertBill.ExecuteScalar()!;
            }

            transaction.Commit();
            return billId;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public void AddService(int billId, ServiceDto service, int quantity)
    {
        using var connection = DbConnectionFactory.CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction(IsolationLevel.Serializable);
        try
        {
            using (var checkBill = new SqlCommand(
                "SELECT COUNT(1) FROM dbo.HoaDon WITH (UPDLOCK, HOLDLOCK) WHERE MaHD = @MaHD AND TrangThai = 0;",
                connection, transaction))
            {
                checkBill.Parameters.Add("@MaHD", SqlDbType.Int).Value = billId;
                if ((int)checkBill.ExecuteScalar()! != 1)
                    throw new InvalidOperationException("Hóa đơn không còn mở.");
            }

            int currentQuantity;
            using (var checkLine = new SqlCommand(
                "SELECT SoLuong FROM dbo.ChiTietDichVu WITH (UPDLOCK, HOLDLOCK) WHERE MaHD = @MaHD AND MaDV = @MaDV;",
                connection, transaction))
            {
                checkLine.Parameters.Add("@MaHD", SqlDbType.Int).Value = billId;
                checkLine.Parameters.Add("@MaDV", SqlDbType.Int).Value = service.MaDV;
                var result = checkLine.ExecuteScalar();
                currentQuantity = result is null ? 0 : (int)result;
            }

            if (service.TonKho < currentQuantity + quantity)
                throw new InvalidOperationException($"Không đủ tồn kho cho dịch vụ '{service.TenDV}'.");

            using var upsert = new SqlCommand("""
                IF EXISTS (SELECT 1 FROM dbo.ChiTietDichVu WHERE MaHD = @MaHD AND MaDV = @MaDV)
                    UPDATE dbo.ChiTietDichVu SET SoLuong = SoLuong + @SoLuong WHERE MaHD = @MaHD AND MaDV = @MaDV;
                ELSE
                    INSERT INTO dbo.ChiTietDichVu (MaHD, MaDV, SoLuong, DonGia) VALUES (@MaHD, @MaDV, @SoLuong, @DonGia);
                """, connection, transaction);
            upsert.Parameters.Add("@MaHD", SqlDbType.Int).Value = billId;
            upsert.Parameters.Add("@MaDV", SqlDbType.Int).Value = service.MaDV;
            upsert.Parameters.Add("@SoLuong", SqlDbType.Int).Value = quantity;
            upsert.Parameters.Add("@DonGia", SqlDbType.Decimal).Value = service.DonGia;
            upsert.ExecuteNonQuery();
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public CheckoutResultDto Checkout(int roomId, int billId)
    {
        using var connection = DbConnectionFactory.CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction(IsolationLevel.Serializable);
        try
        {
            DateTime startedAt;
            DateTime endedAt;
            decimal hourlyRate;
            using (var billCommand = new SqlCommand("""
                SELECT h.GioVao, p.GiaTheoGio, GETDATE()
                FROM dbo.HoaDon AS h WITH (UPDLOCK, HOLDLOCK)
                INNER JOIN dbo.PhongHat AS p WITH (UPDLOCK, HOLDLOCK) ON p.MaPhong = h.MaPhong
                WHERE h.MaHD = @MaHD AND h.MaPhong = @MaPhong AND h.TrangThai = 0;
                """, connection, transaction))
            {
                billCommand.Parameters.Add("@MaHD", SqlDbType.Int).Value = billId;
                billCommand.Parameters.Add("@MaPhong", SqlDbType.Int).Value = roomId;
                using var reader = billCommand.ExecuteReader();
                if (!reader.Read())
                    throw new InvalidOperationException("Không tìm thấy hóa đơn đang mở của phòng.");
                startedAt = reader.GetDateTime(0);
                hourlyRate = reader.GetDecimal(1);
                endedAt = reader.GetDateTime(2);
            }

            var roomTotal = decimal.Round((decimal)(endedAt - startedAt).TotalHours * hourlyRate, 2, MidpointRounding.AwayFromZero);
            var serviceTotal = 0m;
            var orderLines = new List<(int ServiceId, int Quantity)>();
            using (var linesCommand = new SqlCommand(
                "SELECT MaDV, SoLuong, DonGia FROM dbo.ChiTietDichVu WITH (UPDLOCK, HOLDLOCK) WHERE MaHD = @MaHD;",
                connection, transaction))
            {
                linesCommand.Parameters.Add("@MaHD", SqlDbType.Int).Value = billId;
                using var reader = linesCommand.ExecuteReader();
                while (reader.Read())
                {
                    var serviceId = reader.GetInt32(0);
                    var quantity = reader.GetInt32(1);
                    serviceTotal += quantity * reader.GetDecimal(2);
                    orderLines.Add((serviceId, quantity));
                }
            }

            foreach (var line in orderLines)
            {
                using var stockCommand = new SqlCommand(
                    "UPDATE dbo.DichVu SET TonKho = TonKho - @SoLuong WHERE MaDV = @MaDV AND TonKho >= @SoLuong;",
                    connection, transaction);
                stockCommand.Parameters.Add("@SoLuong", SqlDbType.Int).Value = line.Quantity;
                stockCommand.Parameters.Add("@MaDV", SqlDbType.Int).Value = line.ServiceId;
                if (stockCommand.ExecuteNonQuery() != 1)
                    throw new InvalidOperationException("Tồn kho không đủ để thanh toán. Hóa đơn chưa được thanh toán.");
            }

            using (var updateBill = new SqlCommand("""
                UPDATE dbo.HoaDon
                SET GioRa = @GioRa, TongTienPhong = @TongTienPhong,
                    TongTienDichVu = @TongTienDichVu, TrangThai = 1
                WHERE MaHD = @MaHD AND TrangThai = 0;
                """, connection, transaction))
            {
                updateBill.Parameters.Add("@GioRa", SqlDbType.DateTime).Value = endedAt;
                updateBill.Parameters.Add("@TongTienPhong", SqlDbType.Decimal).Value = roomTotal;
                updateBill.Parameters.Add("@TongTienDichVu", SqlDbType.Decimal).Value = serviceTotal;
                updateBill.Parameters.Add("@MaHD", SqlDbType.Int).Value = billId;
                if (updateBill.ExecuteNonQuery() != 1)
                    throw new InvalidOperationException("Không thể cập nhật hóa đơn.");
            }

            using (var updateRoom = new SqlCommand(
                "UPDATE dbo.PhongHat SET TrangThai = N'Trống' WHERE MaPhong = @MaPhong AND TrangThai = N'Đang hát';",
                connection, transaction))
            {
                updateRoom.Parameters.Add("@MaPhong", SqlDbType.Int).Value = roomId;
                if (updateRoom.ExecuteNonQuery() != 1)
                    throw new InvalidOperationException("Không thể cập nhật trạng thái phòng.");
            }

            transaction.Commit();
            return new CheckoutResultDto
            {
                MaHD = billId,
                TongTienPhong = roomTotal,
                TongTienDichVu = serviceTotal
            };
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
}
