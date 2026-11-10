using bai6._4.DAL;
using bai6._4.DTO;

namespace bai6._4.BUS;

public sealed class GarageService
{
    private readonly GarageRepository _repository;

    public GarageService(GarageRepository repository)
    {
        _repository = repository;
    }

    public void TestConnection() => _repository.TestConnection();

    public VehicleDto? FindVehicle(string licensePlate)
    {
        if (string.IsNullOrWhiteSpace(licensePlate))
        {
            throw new ArgumentException("Vui lòng nhập biển số xe.");
        }
        return _repository.FindVehicle(licensePlate.Trim());
    }

    public void SaveVehicle(VehicleDto vehicle)
    {
        vehicle.BienSo = vehicle.BienSo.Trim();
        vehicle.TenKH = vehicle.TenKH.Trim();
        if (vehicle.BienSo.Length == 0 || vehicle.BienSo.Length > 20)
        {
            throw new ArgumentException("Biển số xe là bắt buộc và không vượt quá 20 ký tự.");
        }
        if (vehicle.TenKH.Length == 0 || vehicle.TenKH.Length > 100)
        {
            throw new ArgumentException("Tên chủ xe là bắt buộc và không vượt quá 100 ký tự.");
        }
        if (vehicle.DienThoai?.Length > 15)
        {
            throw new ArgumentException("Số điện thoại không được vượt quá 15 ký tự.");
        }
        if (vehicle.HieuXe?.Length > 50)
        {
            throw new ArgumentException("Hãng xe không được vượt quá 50 ký tự.");
        }
        if (vehicle.NamSanXuat is < 1900 or > 2100)
        {
            throw new ArgumentException("Năm sản xuất không hợp lệ.");
        }
        _repository.SaveVehicle(vehicle);
    }

    public List<PartDto> GetParts() => _repository.GetParts();

    public int AddPart(PartDto part)
    {
        ValidatePart(part);
        return _repository.AddPart(part);
    }

    public void UpdatePart(PartDto part)
    {
        if (part.MaPhuTung <= 0)
        {
            throw new ArgumentException("Hãy chọn phụ tùng cần cập nhật.");
        }
        ValidatePart(part);
        _repository.UpdatePart(part);
    }

    public void DeletePart(int partId)
    {
        if (partId <= 0)
        {
            throw new ArgumentException("Hãy chọn phụ tùng cần xóa.");
        }
        _repository.DeletePart(partId);
    }

    public List<RepairTicketDto> GetRecentRepairs() => _repository.GetRecentRepairs();

    public (int ReceiptsToday, int RepairsInProgress, int LowStockParts) GetDashboardCounts()
        => _repository.GetDashboardCounts();

    public int CreateRepairTicket(RepairTicketDto ticket)
    {
        ticket.BienSo = ticket.BienSo.Trim();
        if (ticket.BienSo.Length == 0)
        {
            throw new ArgumentException("Vui lòng nhập biển số xe cho phiếu sửa chữa.");
        }
        if (ticket.ChiTiet.Count == 0)
        {
            throw new ArgumentException("Phiếu sửa chữa cần có ít nhất một phụ tùng hoặc dịch vụ.");
        }
        if (ticket.ChiTiet.Any(item => item.MaPhuTung <= 0 || item.SoLuong <= 0 || item.TienCong < 0))
        {
            throw new ArgumentException("Thông tin phụ tùng, số lượng hoặc tiền công không hợp lệ.");
        }
        if (ticket.ChiTiet.GroupBy(item => item.MaPhuTung).Any(group => group.Count() > 1))
        {
            throw new ArgumentException("Mỗi phụ tùng chỉ được xuất hiện một lần trong một phiếu.");
        }
        return _repository.CreateRepairTicket(ticket);
    }

    public void MarkRepairPaid(int ticketId)
    {
        if (ticketId <= 0)
        {
            throw new ArgumentException("Mã phiếu sửa chữa không hợp lệ.");
        }
        _repository.MarkRepairPaid(ticketId);
    }

    private static void ValidatePart(PartDto part)
    {
        part.TenPhuTung = part.TenPhuTung.Trim();
        if (part.TenPhuTung.Length == 0 || part.TenPhuTung.Length > 150)
        {
            throw new ArgumentException("Tên phụ tùng là bắt buộc và không vượt quá 150 ký tự.");
        }
        if (part.DonGia < 0)
        {
            throw new ArgumentException("Đơn giá không được âm.");
        }
        if (part.SoLuongTon < 0)
        {
            throw new ArgumentException("Số lượng tồn không được âm.");
        }
    }
}
