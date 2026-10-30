using System.Configuration;
using System.Data;
using bai5._4.DTO;
using Microsoft.Data.SqlClient;

namespace bai5._4.DAL;

public sealed class HotelDal
{
    private readonly string _connectionString;

    public HotelDal()
    {
        _connectionString = ConfigurationManager.ConnectionStrings["HotelDB"]?.ConnectionString
            ?? throw new InvalidOperationException("Chưa cấu hình connection string HotelDB trong App.config.");

    }

    private SqlConnection CreateConnection()
    {
        if (_connectionString.Contains("YOUR_SQL_SERVER", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException(
                "Hãy thay YOUR_SQL_SERVER trong App.config bằng tên SQL Server/instance của bạn rồi chạy Database/ScriptDB.sql.");
        }
        return new SqlConnection(_connectionString);
    }

    public DataTable GetRooms()
    {
        using var connection = CreateConnection();
        using var command = new SqlCommand(
            "SELECT SoPhong, LoaiPhong, GiaTheoGio, TrangThai FROM dbo.Phong ORDER BY SoPhong;", connection);
        using var adapter = new SqlDataAdapter(command);
        var table = new DataTable();
        adapter.Fill(table);
        return table;
    }

    public List<string> GetAvailableRooms()
    {
        using var connection = CreateConnection();
        using var command = new SqlCommand(
            "SELECT SoPhong FROM dbo.Phong WHERE TrangThai = N'Trống' ORDER BY SoPhong;", connection);
        connection.Open();
        using var reader = command.ExecuteReader();
        var rooms = new List<string>();
        while (reader.Read())
        {
            rooms.Add(reader.GetString(0));
        }
        return rooms;
    }

    public int CheckIn(CustomerDTO customer, string roomNumber)
    {
        using var connection = CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction(IsolationLevel.Serializable);
        try
        {
            int customerId;
            using (var findCustomer = new SqlCommand(
                "SELECT MaKH FROM dbo.KhachHang WITH (UPDLOCK, HOLDLOCK) WHERE CCCD = @CCCD;",
                connection, transaction))
            {
                findCustomer.Parameters.Add("@CCCD", SqlDbType.VarChar, 20).Value = customer.CCCD;
                var existingId = findCustomer.ExecuteScalar();
                if (existingId is not null)
                {
                    customerId = Convert.ToInt32(existingId);
                }
                else
                {
                    using var addCustomer = new SqlCommand(
                        "INSERT INTO dbo.KhachHang (HoTen, CCCD, SoDienThoai) " +
                        "OUTPUT INSERTED.MaKH VALUES (@HoTen, @CCCD, @SoDienThoai);",
                        connection, transaction);
                    addCustomer.Parameters.Add("@HoTen", SqlDbType.NVarChar, 100).Value = customer.HoTen;
                    addCustomer.Parameters.Add("@CCCD", SqlDbType.VarChar, 20).Value = customer.CCCD;
                    addCustomer.Parameters.Add("@SoDienThoai", SqlDbType.VarChar, 15).Value =
                        (object?)customer.SoDienThoai ?? DBNull.Value;
                    customerId = Convert.ToInt32(addCustomer.ExecuteScalar());
                }
            }

            using (var updateRoom = new SqlCommand(
                "UPDATE dbo.Phong SET TrangThai = N'Đang ở' WHERE SoPhong = @SoPhong AND TrangThai = N'Trống';",
                connection, transaction))
            {
                updateRoom.Parameters.Add("@SoPhong", SqlDbType.VarChar, 10).Value = roomNumber;
                if (updateRoom.ExecuteNonQuery() != 1)
                {
                    throw new InvalidOperationException("Phòng không còn trống hoặc không tồn tại.");
                }
            }

            int bookingId;
            using (var addBooking = new SqlCommand(
                "INSERT INTO dbo.PhieuDatPhong (SoPhong, MaKH, NgayNhanPhong) " +
                "OUTPUT INSERTED.MaPhieu VALUES (@SoPhong, @MaKH, @NgayNhanPhong);",
                connection, transaction))
            {
                addBooking.Parameters.Add("@SoPhong", SqlDbType.VarChar, 10).Value = roomNumber;
                addBooking.Parameters.Add("@MaKH", SqlDbType.Int).Value = customerId;
                addBooking.Parameters.Add("@NgayNhanPhong", SqlDbType.DateTime).Value = DateTime.Now;
                bookingId = Convert.ToInt32(addBooking.ExecuteScalar());
            }

            transaction.Commit();
            return bookingId;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public List<ServiceDTO> GetServices()
    {
        using var connection = CreateConnection();
        using var command = new SqlCommand(
            "SELECT MaDV, TenDV, DonGia FROM dbo.DichVu ORDER BY TenDV;", connection);
        connection.Open();
        using var reader = command.ExecuteReader();
        var services = new List<ServiceDTO>();
        while (reader.Read())
        {
            services.Add(new ServiceDTO
            {
                MaDV = reader.GetInt32(0),
                TenDV = reader.GetString(1),
                DonGia = reader.GetDecimal(2)
            });
        }
        return services;
    }

    public List<BookingDTO> GetActiveBookings()
    {
        using var connection = CreateConnection();
        using var command = new SqlCommand(
            "SELECT pdp.MaPhieu, pdp.SoPhong, kh.HoTen, pdp.NgayNhanPhong " +
            "FROM dbo.PhieuDatPhong AS pdp " +
            "INNER JOIN dbo.KhachHang AS kh ON kh.MaKH = pdp.MaKH " +
            "WHERE pdp.NgayTraPhong IS NULL ORDER BY pdp.SoPhong;", connection);
        connection.Open();
        using var reader = command.ExecuteReader();
        var bookings = new List<BookingDTO>();
        while (reader.Read())
        {
            bookings.Add(new BookingDTO
            {
                MaPhieu = reader.GetInt32(0),
                SoPhong = reader.GetString(1),
                HoTen = reader.GetString(2),
                NgayNhanPhong = reader.GetDateTime(3)
            });
        }
        return bookings;
    }

    public void AddService(int bookingId, int serviceId, int quantity)
    {
        using var connection = CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction(IsolationLevel.Serializable);
        try
        {
            using var command = new SqlCommand(
                "INSERT INTO dbo.SuDungDichVu (MaPhieu, MaDV, SoLuong, ThoiGian) " +
                "SELECT @MaPhieu, @MaDV, @SoLuong, @ThoiGian " +
                "WHERE EXISTS (SELECT 1 FROM dbo.PhieuDatPhong WITH (UPDLOCK, HOLDLOCK) " +
                "WHERE MaPhieu = @MaPhieu AND NgayTraPhong IS NULL);",
                connection, transaction);
            command.Parameters.Add("@MaPhieu", SqlDbType.Int).Value = bookingId;
            command.Parameters.Add("@MaDV", SqlDbType.Int).Value = serviceId;
            command.Parameters.Add("@SoLuong", SqlDbType.Int).Value = quantity;
            command.Parameters.Add("@ThoiGian", SqlDbType.DateTime).Value = DateTime.Now;
            if (command.ExecuteNonQuery() != 1)
            {
                throw new InvalidOperationException("Phiếu đã trả phòng hoặc không tồn tại.");
            }
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public decimal CheckOut(int bookingId)
    {
        using var connection = CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction(IsolationLevel.Serializable);
        try
        {
            string roomNumber;
            DateTime checkIn;
            decimal hourlyRate;
            using (var command = new SqlCommand(
                "SELECT pdp.SoPhong, pdp.NgayNhanPhong, p.GiaTheoGio " +
                "FROM dbo.PhieuDatPhong AS pdp WITH (UPDLOCK, HOLDLOCK) " +
                "INNER JOIN dbo.Phong AS p ON p.SoPhong = pdp.SoPhong " +
                "WHERE pdp.MaPhieu = @MaPhieu AND pdp.NgayTraPhong IS NULL;",
                connection, transaction))
            {
                command.Parameters.Add("@MaPhieu", SqlDbType.Int).Value = bookingId;
                using var reader = command.ExecuteReader();
                if (!reader.Read())
                {
                    throw new InvalidOperationException("Phiếu không tồn tại hoặc đã được thanh toán.");
                }
                roomNumber = reader.GetString(0);
                checkIn = reader.GetDateTime(1);
                hourlyRate = reader.GetDecimal(2);
            }

            var checkOut = DateTime.Now;
            var hours = (decimal)(checkOut - checkIn).TotalHours;
            using var serviceTotalCommand = new SqlCommand(
                "SELECT COALESCE(SUM(CAST(sddv.SoLuong AS decimal(18,2)) * dv.DonGia), 0) " +
                "FROM dbo.SuDungDichVu AS sddv " +
                "INNER JOIN dbo.DichVu AS dv ON dv.MaDV = sddv.MaDV " +
                "WHERE sddv.MaPhieu = @MaPhieu;", connection, transaction);
            serviceTotalCommand.Parameters.Add("@MaPhieu", SqlDbType.Int).Value = bookingId;
            var serviceTotal = Convert.ToDecimal(serviceTotalCommand.ExecuteScalar());
            var total = (hours * hourlyRate) + serviceTotal;

            using (var updateBooking = new SqlCommand(
                "UPDATE dbo.PhieuDatPhong SET NgayTraPhong = @NgayTraPhong, " +
                "TongTienThanhToan = @TongTien WHERE MaPhieu = @MaPhieu AND NgayTraPhong IS NULL;",
                connection, transaction))
            {
                updateBooking.Parameters.Add("@NgayTraPhong", SqlDbType.DateTime).Value = checkOut;
                var amount = updateBooking.Parameters.Add("@TongTien", SqlDbType.Decimal);
                amount.Precision = 18;
                amount.Scale = 2;
                amount.Value = decimal.Round(total, 2, MidpointRounding.AwayFromZero);
                updateBooking.Parameters.Add("@MaPhieu", SqlDbType.Int).Value = bookingId;
                if (updateBooking.ExecuteNonQuery() != 1)
                {
                    throw new InvalidOperationException("Phiếu đã được thanh toán bởi thao tác khác.");
                }
            }

            using (var updateRoom = new SqlCommand(
                "UPDATE dbo.Phong SET TrangThai = N'Đang dọn dẹp' " +
                "WHERE SoPhong = @SoPhong AND TrangThai = N'Đang ở';", connection, transaction))
            {
                updateRoom.Parameters.Add("@SoPhong", SqlDbType.VarChar, 10).Value = roomNumber;
                if (updateRoom.ExecuteNonQuery() != 1)
                {
                    throw new InvalidOperationException("Không thể chuyển trạng thái phòng sang đang dọn dẹp.");
                }
            }

            transaction.Commit();
            return decimal.Round(total, 2, MidpointRounding.AwayFromZero);
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
}