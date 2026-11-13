using bai6._8.DTO;
using Microsoft.Data.SqlClient;

namespace bai6._8.DAL;

public sealed class MenuDichVuDAL
{
    public List<MenuDichVuDTO> LayDanhSachDichVu()
    {
        var result = new List<MenuDichVuDTO>();
        using var connection = DatabaseHelper.CreateConnection();
        using var command = new SqlCommand("SELECT MaDV, TenDV, DonGia, TonKho FROM dbo.MenuDichVu ORDER BY TenDV;", connection);
        connection.Open();
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            result.Add(new MenuDichVuDTO
            {
                MaDV = reader.GetInt32(0),
                TenDV = reader.GetString(1),
                DonGia = reader.GetDecimal(2),
                TonKho = reader.GetInt32(3)
            });
        }
        return result;
    }
}
