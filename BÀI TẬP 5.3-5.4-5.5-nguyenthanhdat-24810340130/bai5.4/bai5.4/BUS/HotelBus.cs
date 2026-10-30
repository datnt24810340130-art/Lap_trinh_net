using bai5._4.DAL;
using bai5._4.DTO;

namespace bai5._4.BUS;

public sealed class HotelBus
{
    private readonly HotelDal _dal;

    public HotelBus(HotelDal dal) => _dal = dal;

    public System.Data.DataTable GetRooms() => _dal.GetRooms();

    public List<string> GetAvailableRooms() => _dal.GetAvailableRooms();

    public List<ServiceDTO> GetServices() => _dal.GetServices();

    public List<BookingDTO> GetActiveBookings() => _dal.GetActiveBookings();

    public int CheckIn(CustomerDTO customer, string roomNumber)
    {
        ArgumentNullException.ThrowIfNull(customer);
        if (string.IsNullOrWhiteSpace(customer.HoTen))
        {
            throw new ArgumentException("Vui lòng nhập họ tên khách hàng.");
        }
        if (string.IsNullOrWhiteSpace(customer.CCCD))
        {
            throw new ArgumentException("Vui lòng nhập CCCD.");
        }
        if (customer.CCCD.Length > 20)
        {
            throw new ArgumentException("CCCD không được vượt quá 20 ký tự.");
        }
        if (customer.HoTen.Length > 100)
        {
            throw new ArgumentException("Họ tên không được vượt quá 100 ký tự.");
        }
        if (string.IsNullOrWhiteSpace(roomNumber))
        {
            throw new ArgumentException("Vui lòng chọn phòng trống.");
        }
        if (customer.SoDienThoai?.Length > 15)
        {
            throw new ArgumentException("Số điện thoại không được vượt quá 15 ký tự.");
        }
        return _dal.CheckIn(customer, roomNumber);
    }

    public void AddService(int bookingId, int serviceId, int quantity)
    {
        if (bookingId <= 0 || serviceId <= 0)
        {
            throw new ArgumentException("Vui lòng chọn phiếu đang lưu trú và dịch vụ.");
        }
        if (quantity <= 0)
        {
            throw new ArgumentException("Số lượng dịch vụ phải lớn hơn 0.");
        }
        _dal.AddService(bookingId, serviceId, quantity);
    }

    public decimal CheckOut(int bookingId)
    {
        if (bookingId <= 0)
        {
            throw new ArgumentException("Vui lòng chọn phiếu cần thanh toán.");
        }
        return _dal.CheckOut(bookingId);
    }
}