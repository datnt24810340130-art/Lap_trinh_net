using bai6._5.DAL;
using bai6._5.DTO;
using System.Data;

namespace bai6._5.BUS;

public sealed class KaraokeBus
{
    private readonly KaraokeDal _dal = new();

    public List<RoomDto> GetRooms() => _dal.GetRooms();

    public List<ServiceDto> GetServices() => _dal.GetServices();

    public DataTable GetBillServices(int billId)
    {
        if (billId <= 0)
            throw new ArgumentOutOfRangeException(nameof(billId));
        return _dal.GetBillServices(billId);
    }

    public int OpenRoom(RoomDto room)
    {
        ArgumentNullException.ThrowIfNull(room);
        if (room.MaPhong <= 0 || room.TrangThai != "Trống")
            throw new InvalidOperationException("Chỉ có thể mở phòng đang trống.");
        return _dal.OpenRoom(room.MaPhong);
    }

    public void AddService(int billId, ServiceDto? service, int quantity)
    {
        if (billId <= 0)
            throw new ArgumentOutOfRangeException(nameof(billId));
        ArgumentNullException.ThrowIfNull(service);
        if (quantity <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantity), "Số lượng phải lớn hơn 0.");
        if (service.TonKho < quantity)
            throw new InvalidOperationException("Số lượng yêu cầu vượt quá tồn kho hiện tại.");
        _dal.AddService(billId, service, quantity);
    }

    public CheckoutResultDto Checkout(RoomDto room)
    {
        ArgumentNullException.ThrowIfNull(room);
        if (room.MaPhong <= 0 || room.MaHD is null || room.TrangThai != "Đang hát")
            throw new InvalidOperationException("Chọn phòng đang hát để thanh toán.");
        return _dal.Checkout(room.MaPhong, room.MaHD.Value);
    }
}
