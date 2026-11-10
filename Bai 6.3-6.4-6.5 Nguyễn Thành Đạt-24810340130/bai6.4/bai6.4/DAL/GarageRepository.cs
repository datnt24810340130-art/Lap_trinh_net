using System.Data;
using bai6._4.DTO;
using Microsoft.Data.SqlClient;

namespace bai6._4.DAL;

public sealed class GarageRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public GarageRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public void TestConnection()
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();
        using var command = new SqlCommand("SELECT DB_NAME();", connection);
        var databaseName = (string?)command.ExecuteScalar();
        if (!string.Equals(databaseName, "GarageDB", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Chuỗi kết nối chưa trỏ tới cơ sở dữ liệu GarageDB.");
        }
    }

    public VehicleDto? FindVehicle(string licensePlate)
    {
        const string sql = """
            SELECT x.BienSo, x.MaKH, kh.TenKH, kh.DienThoai, kh.DiaChi, x.HieuXe, x.NamSanXuat
            FROM dbo.Xe AS x
            INNER JOIN dbo.KhachHang AS kh ON kh.MaKH = x.MaKH
            WHERE x.BienSo = @BienSo;
            """;
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@BienSo", SqlDbType.VarChar, 20).Value = licensePlate.Trim();
        connection.Open();
        using var reader = command.ExecuteReader();
        if (!reader.Read())
        {
            return null;
        }

        return new VehicleDto
        {
            BienSo = reader.GetString(0),
            MaKH = reader.GetInt32(1),
            TenKH = reader.GetString(2),
            DienThoai = reader.IsDBNull(3) ? null : reader.GetString(3),
            DiaChi = reader.IsDBNull(4) ? null : reader.GetString(4),
            HieuXe = reader.IsDBNull(5) ? null : reader.GetString(5),
            NamSanXuat = reader.IsDBNull(6) ? null : reader.GetInt32(6)
        };
    }

    public void SaveVehicle(VehicleDto vehicle)
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction();
        try
        {
            if (vehicle.MaKH > 0)
            {
                using var updateCustomer = new SqlCommand("""
                    UPDATE dbo.KhachHang
                    SET TenKH = @TenKH, DienThoai = @DienThoai, DiaChi = @DiaChi
                    WHERE MaKH = @MaKH;
                    """, connection, transaction);
                AddCustomerParameters(updateCustomer, vehicle);
                updateCustomer.Parameters.Add("@MaKH", SqlDbType.Int).Value = vehicle.MaKH;
                if (updateCustomer.ExecuteNonQuery() == 0)
                {
                    throw new InvalidOperationException("Không tìm thấy chủ xe cần cập nhật.");
                }

                using var updateVehicle = new SqlCommand("""
                    UPDATE dbo.Xe
                    SET MaKH = @MaKH, HieuXe = @HieuXe, NamSanXuat = @NamSanXuat
                    WHERE BienSo = @BienSo;
                    """, connection, transaction);
                AddVehicleParameters(updateVehicle, vehicle);
                updateVehicle.Parameters.Add("@MaKH", SqlDbType.Int).Value = vehicle.MaKH;
                if (updateVehicle.ExecuteNonQuery() == 0)
                {
                    InsertVehicle(connection, transaction, vehicle, vehicle.MaKH);
                }
            }
            else
            {
                using var insertCustomer = new SqlCommand("""
                    INSERT INTO dbo.KhachHang (TenKH, DienThoai, DiaChi)
                    OUTPUT INSERTED.MaKH
                    VALUES (@TenKH, @DienThoai, @DiaChi);
                    """, connection, transaction);
                AddCustomerParameters(insertCustomer, vehicle);
                var customerId = (int)insertCustomer.ExecuteScalar()!;
                InsertVehicle(connection, transaction, vehicle, customerId);
            }

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public List<PartDto> GetParts()
    {
        const string sql = "SELECT MaPhuTung, TenPhuTung, DonGia, SoLuongTon FROM dbo.PhuTung ORDER BY TenPhuTung;";
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(sql, connection);
        connection.Open();
        using var reader = command.ExecuteReader();
        var parts = new List<PartDto>();
        while (reader.Read())
        {
            parts.Add(new PartDto
            {
                MaPhuTung = reader.GetInt32(0),
                TenPhuTung = reader.GetString(1),
                DonGia = reader.GetDecimal(2),
                SoLuongTon = reader.GetInt32(3)
            });
        }
        return parts;
    }

    public int AddPart(PartDto part)
    {
        const string sql = """
            INSERT INTO dbo.PhuTung (TenPhuTung, DonGia, SoLuongTon)
            OUTPUT INSERTED.MaPhuTung
            VALUES (@TenPhuTung, @DonGia, @SoLuongTon);
            """;
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(sql, connection);
        AddPartParameters(command, part);
        connection.Open();
        return (int)command.ExecuteScalar()!;
    }

    public void UpdatePart(PartDto part)
    {
        const string sql = """
            UPDATE dbo.PhuTung
            SET TenPhuTung = @TenPhuTung, DonGia = @DonGia, SoLuongTon = @SoLuongTon
            WHERE MaPhuTung = @MaPhuTung;
            """;
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(sql, connection);
        AddPartParameters(command, part);
        command.Parameters.Add("@MaPhuTung", SqlDbType.Int).Value = part.MaPhuTung;
        connection.Open();
        if (command.ExecuteNonQuery() == 0)
        {
            throw new InvalidOperationException("Không tìm thấy phụ tùng cần cập nhật.");
        }
    }

    public void DeletePart(int partId)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand("DELETE FROM dbo.PhuTung WHERE MaPhuTung = @MaPhuTung;", connection);
        command.Parameters.Add("@MaPhuTung", SqlDbType.Int).Value = partId;
        connection.Open();
        if (command.ExecuteNonQuery() == 0)
        {
            throw new InvalidOperationException("Không tìm thấy phụ tùng cần xóa.");
        }
    }

    public List<RepairTicketDto> GetRecentRepairs(int limit = 100)
    {
        const string sql = """
            SELECT TOP (@Limit) MaPhieu, BienSo, NgayNhan, NoiDungSua, TongTien, TrangThai
            FROM dbo.PhieuSuaChua
            ORDER BY NgayNhan DESC, MaPhieu DESC;
            """;
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@Limit", SqlDbType.Int).Value = limit;
        connection.Open();
        using var reader = command.ExecuteReader();
        var tickets = new List<RepairTicketDto>();
        while (reader.Read())
        {
            tickets.Add(new RepairTicketDto
            {
                MaPhieu = reader.GetInt32(0),
                BienSo = reader.GetString(1),
                NgayNhan = reader.GetDateTime(2),
                NoiDungSua = reader.IsDBNull(3) ? null : reader.GetString(3),
                TongTien = reader.GetDecimal(4),
                TrangThai = reader.GetString(5)
            });
        }
        return tickets;
    }

    public (int ReceiptsToday, int RepairsInProgress, int LowStockParts) GetDashboardCounts()
    {
        const string sql = """
            SELECT
                (SELECT COUNT(*) FROM dbo.PhieuSuaChua WHERE CAST(NgayNhan AS date) = CAST(GETDATE() AS date)),
                (SELECT COUNT(*) FROM dbo.PhieuSuaChua WHERE TrangThai = N'Đang sửa'),
                (SELECT COUNT(*) FROM dbo.PhuTung WHERE SoLuongTon <= 5);
            """;
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(sql, connection);
        connection.Open();
        using var reader = command.ExecuteReader();
        reader.Read();
        return (reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2));
    }

    public int CreateRepairTicket(RepairTicketDto ticket)
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
        try
        {
            using var insertTicket = new SqlCommand("""
                INSERT INTO dbo.PhieuSuaChua (BienSo, NgayNhan, NoiDungSua, TongTien, TrangThai)
                OUTPUT INSERTED.MaPhieu
                VALUES (@BienSo, @NgayNhan, @NoiDungSua, 0, N'Đã hoàn thành');
                """, connection, transaction);
            insertTicket.Parameters.Add("@BienSo", SqlDbType.VarChar, 20).Value = ticket.BienSo.Trim();
            insertTicket.Parameters.Add("@NgayNhan", SqlDbType.DateTime).Value = ticket.NgayNhan;
            insertTicket.Parameters.Add("@NoiDungSua", SqlDbType.NVarChar, -1).Value = (object?)ticket.NoiDungSua ?? DBNull.Value;
            var ticketId = (int)insertTicket.ExecuteScalar()!;
            decimal total = 0;

            foreach (var detail in ticket.ChiTiet)
            {
                using var deductStock = new SqlCommand("""
                    UPDATE dbo.PhuTung
                    SET SoLuongTon = SoLuongTon - @SoLuong
                    WHERE MaPhuTung = @MaPhuTung AND SoLuongTon >= @SoLuong;
                    """, connection, transaction);
                deductStock.Parameters.Add("@MaPhuTung", SqlDbType.Int).Value = detail.MaPhuTung;
                deductStock.Parameters.Add("@SoLuong", SqlDbType.Int).Value = detail.SoLuong;
                if (deductStock.ExecuteNonQuery() != 1)
                {
                    throw new InvalidOperationException($"Kho không đủ số lượng phụ tùng '{detail.TenPhuTung}'. Phiếu đã được hủy và tồn kho không thay đổi.");
                }

                using var getPrice = new SqlCommand("SELECT DonGia FROM dbo.PhuTung WHERE MaPhuTung = @MaPhuTung;", connection, transaction);
                getPrice.Parameters.Add("@MaPhuTung", SqlDbType.Int).Value = detail.MaPhuTung;
                var unitPrice = (decimal)getPrice.ExecuteScalar()!;
                using var insertDetail = new SqlCommand("""
                    INSERT INTO dbo.ChiTietSuaChua (MaPhieu, MaPhuTung, SoLuong, TienCong)
                    VALUES (@MaPhieu, @MaPhuTung, @SoLuong, @TienCong);
                    """, connection, transaction);
                insertDetail.Parameters.Add("@MaPhieu", SqlDbType.Int).Value = ticketId;
                insertDetail.Parameters.Add("@MaPhuTung", SqlDbType.Int).Value = detail.MaPhuTung;
                insertDetail.Parameters.Add("@SoLuong", SqlDbType.Int).Value = detail.SoLuong;
                var laborParameter = insertDetail.Parameters.Add("@TienCong", SqlDbType.Decimal);
                laborParameter.Precision = 18;
                laborParameter.Scale = 2;
                laborParameter.Value = detail.TienCong;
                total += unitPrice * detail.SoLuong + detail.TienCong;
            }

            using var updateTotal = new SqlCommand("UPDATE dbo.PhieuSuaChua SET TongTien = @TongTien WHERE MaPhieu = @MaPhieu;", connection, transaction);
            var totalParameter = updateTotal.Parameters.Add("@TongTien", SqlDbType.Decimal);
            totalParameter.Precision = 18;
            totalParameter.Scale = 2;
            totalParameter.Value = total;
            updateTotal.Parameters.Add("@MaPhieu", SqlDbType.Int).Value = ticketId;
            updateTotal.ExecuteNonQuery();
            transaction.Commit();
            return ticketId;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public void MarkRepairPaid(int ticketId)
    {
        const string sql = "UPDATE dbo.PhieuSuaChua SET TrangThai = N'Đã thanh toán' WHERE MaPhieu = @MaPhieu AND TrangThai <> N'Đã thanh toán';";
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@MaPhieu", SqlDbType.Int).Value = ticketId;
        connection.Open();
        if (command.ExecuteNonQuery() == 0)
        {
            throw new InvalidOperationException("Không tìm thấy phiếu hoặc phiếu đã được thanh toán.");
        }
    }

    private static void AddCustomerParameters(SqlCommand command, VehicleDto vehicle)
    {
        command.Parameters.Add("@TenKH", SqlDbType.NVarChar, 100).Value = vehicle.TenKH.Trim();
        command.Parameters.Add("@DienThoai", SqlDbType.VarChar, 15).Value = (object?)vehicle.DienThoai ?? DBNull.Value;
        command.Parameters.Add("@DiaChi", SqlDbType.NVarChar, 200).Value = (object?)vehicle.DiaChi ?? DBNull.Value;
    }

    private static void AddVehicleParameters(SqlCommand command, VehicleDto vehicle)
    {
        command.Parameters.Add("@BienSo", SqlDbType.VarChar, 20).Value = vehicle.BienSo.Trim();
        command.Parameters.Add("@HieuXe", SqlDbType.NVarChar, 50).Value = (object?)vehicle.HieuXe ?? DBNull.Value;
        command.Parameters.Add("@NamSanXuat", SqlDbType.Int).Value = (object?)vehicle.NamSanXuat ?? DBNull.Value;
    }

    private static void InsertVehicle(SqlConnection connection, SqlTransaction transaction, VehicleDto vehicle, int customerId)
    {
        using var command = new SqlCommand("""
            INSERT INTO dbo.Xe (BienSo, MaKH, HieuXe, NamSanXuat)
            VALUES (@BienSo, @MaKH, @HieuXe, @NamSanXuat);
            """, connection, transaction);
        AddVehicleParameters(command, vehicle);
        command.Parameters.Add("@MaKH", SqlDbType.Int).Value = customerId;
        command.ExecuteNonQuery();
    }

    private static void AddPartParameters(SqlCommand command, PartDto part)
    {
        command.Parameters.Add("@TenPhuTung", SqlDbType.NVarChar, 150).Value = part.TenPhuTung.Trim();
        var priceParameter = command.Parameters.Add("@DonGia", SqlDbType.Decimal);
        priceParameter.Precision = 18;
        priceParameter.Scale = 2;
        priceParameter.Value = part.DonGia;
        command.Parameters.Add("@SoLuongTon", SqlDbType.Int).Value = part.SoLuongTon;
    }
}
