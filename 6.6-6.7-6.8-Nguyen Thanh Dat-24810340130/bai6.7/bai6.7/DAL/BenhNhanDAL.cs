using bai6._7.Database;
using bai6._7.DTO;
using Microsoft.Data.SqlClient;
using System.Data;

namespace bai6._7.DAL;

public sealed class BenhNhanDAL
{
    public BenhNhanDTO? TimTheoSoDienThoai(string soDienThoai)
    {
        using SqlConnection connection = DatabaseHelper.GetConnection();
        using SqlCommand command = new(
            "SELECT MaBN, HoTen, SoDienThoai, NgaySinh, GioiTinh, DiaChi FROM dbo.BenhNhan WHERE SoDienThoai = @SoDienThoai;",
            connection);
        command.Parameters.Add("@SoDienThoai", SqlDbType.NVarChar, 20).Value = soDienThoai;
        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();
        if (!reader.Read())
        {
            return null;
        }

        return new BenhNhanDTO
        {
            MaBN = reader.GetInt32(0),
            HoTen = reader.GetString(1),
            SoDienThoai = reader.GetString(2),
            NgaySinh = reader.IsDBNull(3) ? null : reader.GetDateTime(3),
            GioiTinh = reader.IsDBNull(4) ? null : reader.GetString(4),
            DiaChi = reader.IsDBNull(5) ? null : reader.GetString(5)
        };
    }

    public int TaoBenhNhanVaHoSo(BenhNhanDTO benhNhan, string chanDoan, decimal tienKham)
    {
        using SqlConnection connection = DatabaseHelper.GetConnection();
        connection.Open();
        using SqlTransaction transaction = connection.BeginTransaction();

        try
        {
            using SqlCommand patientCommand = new(
                "INSERT dbo.BenhNhan (HoTen, SoDienThoai, NgaySinh, GioiTinh, DiaChi) VALUES (@HoTen, @SoDienThoai, @NgaySinh, @GioiTinh, @DiaChi); SELECT CONVERT(int, SCOPE_IDENTITY());",
                connection, transaction);
            patientCommand.Parameters.Add("@HoTen", SqlDbType.NVarChar, 150).Value = benhNhan.HoTen;
            patientCommand.Parameters.Add("@SoDienThoai", SqlDbType.NVarChar, 20).Value = benhNhan.SoDienThoai;
            patientCommand.Parameters.Add("@NgaySinh", SqlDbType.Date).Value = (object?)benhNhan.NgaySinh ?? DBNull.Value;
            patientCommand.Parameters.Add("@GioiTinh", SqlDbType.NVarChar, 20).Value = (object?)benhNhan.GioiTinh ?? DBNull.Value;
            patientCommand.Parameters.Add("@DiaChi", SqlDbType.NVarChar, 300).Value = (object?)benhNhan.DiaChi ?? DBNull.Value;
            int maBN = (int)patientCommand.ExecuteScalar()!;

            int maHoSo = TaoHoSo(connection, transaction, maBN, chanDoan, tienKham);
            transaction.Commit();
            return maHoSo;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public int TaoHoSoKham(int maBN, string chanDoan, decimal tienKham)
    {
        using SqlConnection connection = DatabaseHelper.GetConnection();
        connection.Open();
        using SqlTransaction transaction = connection.BeginTransaction();

        try
        {
            int maHoSo = TaoHoSo(connection, transaction, maBN, chanDoan, tienKham);
            transaction.Commit();
            return maHoSo;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    private static int TaoHoSo(SqlConnection connection, SqlTransaction transaction, int maBN, string chanDoan, decimal tienKham)
    {
        using SqlCommand command = new(
            "INSERT dbo.HoSoKham (MaBN, ChanDoan, TienKham) VALUES (@MaBN, @ChanDoan, @TienKham); SELECT CONVERT(int, SCOPE_IDENTITY());",
            connection, transaction);
        command.Parameters.Add("@MaBN", SqlDbType.Int).Value = maBN;
        command.Parameters.Add("@ChanDoan", SqlDbType.NVarChar, 500).Value = chanDoan;
        command.Parameters.Add("@TienKham", SqlDbType.Decimal).Value = tienKham;
        command.Parameters["@TienKham"].Precision = 18;
        command.Parameters["@TienKham"].Scale = 2;
        return (int)command.ExecuteScalar()!;
    }
}
