using System.Data;
using bai5._3.DTO;
using Microsoft.Data.SqlClient;

namespace bai5._3.DAL;

public sealed class CafeRepository
{
    public List<DiningTableDto> GetTables()
    {
        const string sql = "SELECT MaBan, TenBan, TrangThai FROM BanAn ORDER BY MaBan;";
        using SqlConnection connection = Database.CreateConnection();
        using SqlCommand command = new(sql, connection);
        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        List<DiningTableDto> tables = [];
        while (reader.Read())
        {
            tables.Add(new DiningTableDto
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Status = reader.IsDBNull(2) ? "Trống" : reader.GetString(2)
            });
        }

        return tables;
    }

    public List<MenuCategoryDto> GetCategories()
    {
        const string sql = "SELECT MaDanhMuc, TenDanhMuc FROM DanhMuc ORDER BY TenDanhMuc;";
        using SqlConnection connection = Database.CreateConnection();
        using SqlCommand command = new(sql, connection);
        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        List<MenuCategoryDto> categories = [];
        while (reader.Read())
        {
            categories.Add(new MenuCategoryDto { Id = reader.GetInt32(0), Name = reader.GetString(1) });
        }

        return categories;
    }

    public DataTable GetMenuItems(int? categoryId = null)
    {
        const string sql = "SELECT m.MaMon, m.TenMon, m.MaDanhMuc, d.TenDanhMuc, m.DonGia " +
                           "FROM MonAn AS m INNER JOIN DanhMuc AS d ON d.MaDanhMuc = m.MaDanhMuc " +
                           "WHERE (@MaDanhMuc IS NULL OR m.MaDanhMuc = @MaDanhMuc) ORDER BY d.TenDanhMuc, m.TenMon;";
        using SqlConnection connection = Database.CreateConnection();
        using SqlCommand command = new(sql, connection);
        command.Parameters.Add("@MaDanhMuc", SqlDbType.Int).Value = categoryId.HasValue ? categoryId.Value : DBNull.Value;
        using SqlDataAdapter adapter = new(command);
        DataTable table = new();
        adapter.Fill(table);
        return table;
    }

    public DataTable GetOrderItems(int tableId)
    {
        const string sql = "SELECT c.MaMon, m.TenMon, c.SoLuong, c.DonGia, " +
                           "CAST(c.SoLuong * c.DonGia AS decimal(18,2)) AS ThanhTien " +
                           "FROM ChiTietHoaDon AS c INNER JOIN MonAn AS m ON m.MaMon = c.MaMon " +
                           "INNER JOIN HoaDon AS h ON h.MaHoaDon = c.MaHoaDon " +
                           "WHERE h.MaBan = @MaBan AND h.TrangThai = 0 ORDER BY m.TenMon;";
        using SqlConnection connection = Database.CreateConnection();
        using SqlCommand command = new(sql, connection);
        command.Parameters.Add("@MaBan", SqlDbType.Int).Value = tableId;
        using SqlDataAdapter adapter = new(command);
        DataTable table = new();
        adapter.Fill(table);
        return table;
    }

    public void AddOrderItem(int tableId, int menuItemId, int quantity)
    {
        const string findBillSql = "SELECT MaHoaDon FROM HoaDon WHERE MaBan = @MaBan AND TrangThai = 0;";
        const string createBillSql = "INSERT INTO HoaDon (MaBan, GioVao, TrangThai) OUTPUT INSERTED.MaHoaDon " +
                                     "VALUES (@MaBan, GETDATE(), 0);";
        const string addItemSql = "IF EXISTS (SELECT 1 FROM ChiTietHoaDon WHERE MaHoaDon = @MaHoaDon AND MaMon = @MaMon) " +
                                  "UPDATE ChiTietHoaDon SET SoLuong = SoLuong + @SoLuong WHERE MaHoaDon = @MaHoaDon AND MaMon = @MaMon " +
                                  "ELSE INSERT INTO ChiTietHoaDon (MaHoaDon, MaMon, SoLuong, DonGia) " +
                                  "SELECT @MaHoaDon, MaMon, @SoLuong, DonGia FROM MonAn WHERE MaMon = @MaMon;";
        using SqlConnection connection = Database.CreateConnection();
        connection.Open();
        using SqlTransaction transaction = connection.BeginTransaction();
        try
        {
            int billId;
            using (SqlCommand findBill = new(findBillSql, connection, transaction))
            {
                findBill.Parameters.Add("@MaBan", SqlDbType.Int).Value = tableId;
                object? existingBill = findBill.ExecuteScalar();
                if (existingBill is null)
                {
                    using SqlCommand createBill = new(createBillSql, connection, transaction);
                    createBill.Parameters.Add("@MaBan", SqlDbType.Int).Value = tableId;
                    billId = Convert.ToInt32(createBill.ExecuteScalar());
                }
                else
                {
                    billId = Convert.ToInt32(existingBill);
                }
            }

            using (SqlCommand addItem = new(addItemSql, connection, transaction))
            {
                addItem.Parameters.Add("@MaHoaDon", SqlDbType.Int).Value = billId;
                addItem.Parameters.Add("@MaMon", SqlDbType.Int).Value = menuItemId;
                addItem.Parameters.Add("@SoLuong", SqlDbType.Int).Value = quantity;
                if (addItem.ExecuteNonQuery() == 0)
                {
                    throw new InvalidOperationException("Món ăn không tồn tại hoặc không thể thêm vào hóa đơn.");
                }
            }

            using (SqlCommand updateTable = new("UPDATE BanAn SET TrangThai = N'Có khách' WHERE MaBan = @MaBan;", connection, transaction))
            {
                updateTable.Parameters.Add("@MaBan", SqlDbType.Int).Value = tableId;
                updateTable.ExecuteNonQuery();
            }

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public decimal PayTable(int tableId)
    {
        const string findBillSql = "SELECT MaHoaDon FROM HoaDon WHERE MaBan = @MaBan AND TrangThai = 0;";
        using SqlConnection connection = Database.CreateConnection();
        connection.Open();
        using SqlTransaction transaction = connection.BeginTransaction();
        try
        {
            int billId;
            using (SqlCommand findBill = new(findBillSql, connection, transaction))
            {
                findBill.Parameters.Add("@MaBan", SqlDbType.Int).Value = tableId;
                object? result = findBill.ExecuteScalar();
                if (result is null)
                {
                    throw new InvalidOperationException("Bàn chưa có hóa đơn chưa thanh toán.");
                }
                billId = Convert.ToInt32(result);
            }

            decimal total;
            using (SqlCommand getTotal = new("SELECT COALESCE(SUM(SoLuong * DonGia), 0) FROM ChiTietHoaDon WHERE MaHoaDon = @MaHoaDon;", connection, transaction))
            {
                getTotal.Parameters.Add("@MaHoaDon", SqlDbType.Int).Value = billId;
                total = Convert.ToDecimal(getTotal.ExecuteScalar());
            }

            using (SqlCommand payBill = new("UPDATE HoaDon SET GioRa = GETDATE(), TongTien = @TongTien, TrangThai = 1 " +
                                            "WHERE MaHoaDon = @MaHoaDon AND TrangThai = 0;", connection, transaction))
            {
                payBill.Parameters.Add("@TongTien", SqlDbType.Decimal).Precision = 18;
                payBill.Parameters["@TongTien"].Scale = 2;
                payBill.Parameters["@TongTien"].Value = total;
                payBill.Parameters.Add("@MaHoaDon", SqlDbType.Int).Value = billId;
                if (payBill.ExecuteNonQuery() != 1)
                {
                    throw new InvalidOperationException("Hóa đơn đã được thanh toán hoặc không còn tồn tại.");
                }
            }

            using (SqlCommand clearTable = new("UPDATE BanAn SET TrangThai = N'Trống' WHERE MaBan = @MaBan;", connection, transaction))
            {
                clearTable.Parameters.Add("@MaBan", SqlDbType.Int).Value = tableId;
                clearTable.ExecuteNonQuery();
            }

            transaction.Commit();
            return total;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public void MoveTable(int sourceTableId, int targetTableId)
    {
        using SqlConnection connection = Database.CreateConnection();
        connection.Open();
        using SqlTransaction transaction = connection.BeginTransaction();
        try
        {
            Dictionary<int, string> tableStatuses = [];
            using (SqlCommand lockTables = new(
                "SELECT MaBan, TrangThai FROM BanAn WITH (UPDLOCK, HOLDLOCK) WHERE MaBan IN (@BanNguon, @BanDich);",
                connection, transaction))
            {
                lockTables.Parameters.Add("@BanNguon", SqlDbType.Int).Value = sourceTableId;
                lockTables.Parameters.Add("@BanDich", SqlDbType.Int).Value = targetTableId;
                using SqlDataReader reader = lockTables.ExecuteReader();
                while (reader.Read())
                {
                    tableStatuses.Add(reader.GetInt32(0), reader.GetString(1));
                }
            }

            if (!tableStatuses.ContainsKey(sourceTableId) || !tableStatuses.ContainsKey(targetTableId))
            {
                throw new InvalidOperationException("Bàn nguồn hoặc bàn đích không tồn tại.");
            }
            if (tableStatuses[targetTableId] != "Trống")
            {
                throw new InvalidOperationException("Chỉ có thể chuyển hóa đơn sang bàn đang trống.");
            }

            int billId;
            using (SqlCommand findBill = new(
                "SELECT MaHoaDon FROM HoaDon WHERE MaBan = @MaBan AND TrangThai = 0;", connection, transaction))
            {
                findBill.Parameters.Add("@MaBan", SqlDbType.Int).Value = sourceTableId;
                object? result = findBill.ExecuteScalar();
                if (result is null)
                {
                    throw new InvalidOperationException("Bàn nguồn chưa có hóa đơn chưa thanh toán.");
                }
                billId = Convert.ToInt32(result);
            }

            using (SqlCommand moveBill = new(
                "UPDATE HoaDon SET MaBan = @BanDich WHERE MaHoaDon = @MaHoaDon AND TrangThai = 0;", connection, transaction))
            {
                moveBill.Parameters.Add("@BanDich", SqlDbType.Int).Value = targetTableId;
                moveBill.Parameters.Add("@MaHoaDon", SqlDbType.Int).Value = billId;
                if (moveBill.ExecuteNonQuery() != 1)
                {
                    throw new InvalidOperationException("Không thể chuyển hóa đơn sang bàn đích.");
                }
            }

            using (SqlCommand updateStatuses = new(
                "UPDATE BanAn SET TrangThai = CASE WHEN MaBan = @BanNguon THEN N'Trống' ELSE N'Có khách' END " +
                "WHERE MaBan IN (@BanNguon, @BanDich);", connection, transaction))
            {
                updateStatuses.Parameters.Add("@BanNguon", SqlDbType.Int).Value = sourceTableId;
                updateStatuses.Parameters.Add("@BanDich", SqlDbType.Int).Value = targetTableId;
                updateStatuses.ExecuteNonQuery();
            }

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public void AddCategory(string name)
    {
        ExecuteNonQuery("INSERT INTO DanhMuc (TenDanhMuc) VALUES (@TenDanhMuc);", command =>
            command.Parameters.Add("@TenDanhMuc", SqlDbType.NVarChar, 100).Value = name);
    }

    public void DeleteCategory(int categoryId)
    {
        ExecuteNonQuery("DELETE FROM DanhMuc WHERE MaDanhMuc = @MaDanhMuc;", command =>
            command.Parameters.Add("@MaDanhMuc", SqlDbType.Int).Value = categoryId);
    }

    public void SaveMenuItem(int? menuItemId, string name, int categoryId, decimal price)
    {
        if (menuItemId.HasValue)
        {
            ExecuteNonQuery("UPDATE MonAn SET TenMon = @TenMon, MaDanhMuc = @MaDanhMuc, DonGia = @DonGia WHERE MaMon = @MaMon;", command =>
            {
                AddMenuItemParameters(command, name, categoryId, price);
                command.Parameters.Add("@MaMon", SqlDbType.Int).Value = menuItemId.Value;
            });
        }
        else
        {
            ExecuteNonQuery("INSERT INTO MonAn (TenMon, MaDanhMuc, DonGia) VALUES (@TenMon, @MaDanhMuc, @DonGia);", command =>
                AddMenuItemParameters(command, name, categoryId, price));
        }
    }

    public void DeleteMenuItem(int menuItemId)
    {
        ExecuteNonQuery("DELETE FROM MonAn WHERE MaMon = @MaMon;", command =>
            command.Parameters.Add("@MaMon", SqlDbType.Int).Value = menuItemId);
    }

    private static void AddMenuItemParameters(SqlCommand command, string name, int categoryId, decimal price)
    {
        command.Parameters.Add("@TenMon", SqlDbType.NVarChar, 100).Value = name;
        command.Parameters.Add("@MaDanhMuc", SqlDbType.Int).Value = categoryId;
        SqlParameter priceParameter = command.Parameters.Add("@DonGia", SqlDbType.Decimal);
        priceParameter.Precision = 18;
        priceParameter.Scale = 2;
        priceParameter.Value = price;
    }

    private static void ExecuteNonQuery(string sql, Action<SqlCommand> addParameters)
    {
        using SqlConnection connection = Database.CreateConnection();
        using SqlCommand command = new(sql, connection);
        addParameters(command);
        connection.Open();
        command.ExecuteNonQuery();
    }
}