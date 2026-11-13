using bai6._7.Database;
using bai6._7.DTO;
using Microsoft.Data.SqlClient;
using System.Data;

namespace bai6._7.DAL;

public sealed class BenhNhanDAL
{
    public List<BenhNhanDTO> LayDanhSach(string? tuKhoa = null)
    {
        List<BenhNhanDTO> patients = [];
        using SqlConnection connection = DatabaseHelper.GetConnection();
        using SqlCommand command = new(
            "SELECT MaBN, HoTen, SoDienThoai, NgaySinh, GioiTinh, DiaChi FROM dbo.BenhNhan WHERE @TuKhoa = N'' OR HoTen LIKE @Mau OR SoDienThoai LIKE @Mau ORDER BY MaBN DESC;",
            connection);
        string keyword = tuKhoa?.Trim() ?? string.Empty;
        command.Parameters.Add("@TuKhoa", SqlDbType.NVarChar, 300).Value = keyword;
        command.Parameters.Add("@Mau", SqlDbType.NVarChar, 302).Value = $"%{keyword}%";
        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            patients.Add(ReadPatient(reader));
        }

        return patients;
    }

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

        return ReadPatient(reader);
    }

    public int ThemBenhNhan(BenhNhanDTO benhNhan)
    {
        using SqlConnection connection = DatabaseHelper.GetConnection();
        using SqlCommand command = new(
            "INSERT dbo.BenhNhan (HoTen, SoDienThoai, NgaySinh, GioiTinh, DiaChi) VALUES (@HoTen, @SoDienThoai, @NgaySinh, @GioiTinh, @DiaChi); SELECT CONVERT(int, SCOPE_IDENTITY());",
            connection);
        AddPatientParameters(command, benhNhan);
        connection.Open();
        return (int)command.ExecuteScalar()!;
    }

    public void CapNhatBenhNhan(BenhNhanDTO benhNhan)
    {
        using SqlConnection connection = DatabaseHelper.GetConnection();
        using SqlCommand command = new(
            "UPDATE dbo.BenhNhan SET HoTen = @HoTen, SoDienThoai = @SoDienThoai, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, DiaChi = @DiaChi WHERE MaBN = @MaBN;",
            connection);
        AddPatientParameters(command, benhNhan);
        command.Parameters.Add("@MaBN", SqlDbType.Int).Value = benhNhan.MaBN;
        connection.Open();
        if (command.ExecuteNonQuery() == 0)
        {
            throw new InvalidOperationException("Không tìm thấy bệnh nhân cần cập nhật.");
        }
    }

    public void XoaBenhNhan(int maBN)
    {
        using SqlConnection connection = DatabaseHelper.GetConnection();
        using SqlCommand command = new("DELETE dbo.BenhNhan WHERE MaBN = @MaBN;", connection);
        command.Parameters.Add("@MaBN", SqlDbType.Int).Value = maBN;
        connection.Open();
        try
        {
            if (command.ExecuteNonQuery() == 0)
            {
                throw new InvalidOperationException("Không tìm thấy bệnh nhân cần xóa.");
            }
        }
        catch (SqlException ex) when (ex.Number == 547)
        {
            throw new InvalidOperationException("Không thể xóa bệnh nhân đã có hồ sơ khám.", ex);
        }
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

    private static void AddPatientParameters(SqlCommand command, BenhNhanDTO benhNhan)
    {
        command.Parameters.Add("@HoTen", SqlDbType.NVarChar, 150).Value = benhNhan.HoTen;
        command.Parameters.Add("@SoDienThoai", SqlDbType.NVarChar, 20).Value = benhNhan.SoDienThoai;
        command.Parameters.Add("@NgaySinh", SqlDbType.Date).Value = (object?)benhNhan.NgaySinh ?? DBNull.Value;
        command.Parameters.Add("@GioiTinh", SqlDbType.NVarChar, 20).Value = (object?)benhNhan.GioiTinh ?? DBNull.Value;
        command.Parameters.Add("@DiaChi", SqlDbType.NVarChar, 300).Value = (object?)benhNhan.DiaChi ?? DBNull.Value;
    }

    private static BenhNhanDTO ReadPatient(SqlDataReader reader) => new()
    {
        MaBN = reader.GetInt32(0),
        HoTen = reader.GetString(1),
        SoDienThoai = reader.GetString(2),
        NgaySinh = reader.IsDBNull(3) ? null : reader.GetDateTime(3),
        GioiTinh = reader.IsDBNull(4) ? null : reader.GetString(4),
        DiaChi = reader.IsDBNull(5) ? null : reader.GetString(5)
    };
}
