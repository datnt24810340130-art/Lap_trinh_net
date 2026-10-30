using bai5._4.BUS;
using bai5._4.DTO;
using System.Data;

namespace bai5._4.GUI;

public sealed class HotelDashboard : UserControl
{
    private readonly HotelBus _bus;
    private readonly TabControl _tabs = new() { Dock = DockStyle.Fill };
    private readonly DataGridView _roomGrid = CreateGrid();
    private readonly ComboBox _roomTypeFilter = CreateDropDown();
    private readonly ComboBox _roomStatusFilter = CreateDropDown();
    private readonly ComboBox _availableRoom = CreateDropDown();
    private readonly ComboBox _serviceBooking = CreateDropDown();
    private readonly ComboBox _serviceChoice = CreateDropDown();
    private readonly ComboBox _checkoutBooking = CreateDropDown();
    private readonly TextBox _customerName = new() { MaxLength = 100, Width = 190 };
    private readonly TextBox _customerId = new() { MaxLength = 20, Width = 160 };
    private readonly TextBox _phone = new() { MaxLength = 15, Width = 150 };
    private readonly NumericUpDown _quantity = new() { Minimum = 1, Maximum = 1000, Value = 1, Width = 100 };
    private readonly Label _checkoutTotal = new()
    {
        AutoSize = true,
        Font = new Font("Segoe UI", 14, FontStyle.Bold),
        Text = "Tổng thanh toán: --"
    };
    private DataTable? _rooms;

    public HotelDashboard(HotelBus bus)
    {
        _bus = bus;
        Dock = DockStyle.Fill;
        Padding = new Padding(12);
        BuildInterface();
        Load += (_, _) => RunSafely(LoadData);
    }

    private void BuildInterface()
    {
        var roomsTab = new TabPage("Phòng");
        var filters = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 52,
            Padding = new Padding(4),
            WrapContents = false
        };
        _roomTypeFilter.Width = 180;
        _roomStatusFilter.Width = 180;
        filters.Controls.Add(CreateInlineField("Loại phòng", _roomTypeFilter));
        filters.Controls.Add(CreateInlineField("Trạng thái", _roomStatusFilter));
        roomsTab.Controls.Add(_roomGrid);
        roomsTab.Controls.Add(filters);
        _roomTypeFilter.SelectedIndexChanged += (_, _) => ApplyRoomFilter();
        _roomStatusFilter.SelectedIndexChanged += (_, _) => ApplyRoomFilter();

        var checkInTab = new TabPage("Nhận phòng");
        var checkInContent = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            Padding = new Padding(12),
            WrapContents = true
        };
        checkInContent.Controls.Add(CreateField("Họ tên khách hàng", _customerName));
        checkInContent.Controls.Add(CreateField("CCCD", _customerId));
        checkInContent.Controls.Add(CreateField("Số điện thoại", _phone));
        checkInContent.Controls.Add(CreateField("Phòng trống", _availableRoom));
        var checkInButton = new Button { Text = "Nhận phòng", AutoSize = true, Height = 36, Margin = new Padding(8, 24, 8, 8) };
        checkInButton.Click += (_, _) => RunSafely(CheckIn);
        checkInContent.Controls.Add(checkInButton);
        checkInTab.Controls.Add(new Label
        {
            Dock = DockStyle.Top,
            Height = 54,
            Padding = new Padding(14),
            Text = "Nhập CCCD để tra cứu khách hàng; nếu chưa có, hệ thống sẽ tạo khách hàng mới."
        });
        checkInTab.Controls.Add(checkInContent);

        var serviceTab = new TabPage("Gọi dịch vụ");
        var serviceContent = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            Padding = new Padding(12),
            WrapContents = true
        };
        _serviceBooking.Width = 420;
        _serviceChoice.Width = 260;
        serviceContent.Controls.Add(CreateField("Phiếu đang lưu trú", _serviceBooking));
        serviceContent.Controls.Add(CreateField("Dịch vụ", _serviceChoice));
        serviceContent.Controls.Add(CreateField("Số lượng", _quantity));
        var serviceButton = new Button { Text = "Gọi dịch vụ", AutoSize = true, Height = 36, Margin = new Padding(8, 24, 8, 8) };
        serviceButton.Click += (_, _) => RunSafely(AddService);
        serviceContent.Controls.Add(serviceButton);
        serviceTab.Controls.Add(new Label
        {
            Dock = DockStyle.Top,
            Height = 54,
            Padding = new Padding(14),
            Text = "Chi phí dịch vụ sẽ được cộng vào hóa đơn khi trả phòng."
        });
        serviceTab.Controls.Add(serviceContent);

        var checkoutTab = new TabPage("Trả phòng / hóa đơn");
        var checkoutContent = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            Padding = new Padding(12),
            WrapContents = true
        };
        _checkoutBooking.Width = 520;
        checkoutContent.Controls.Add(CreateField("Phiếu cần thanh toán", _checkoutBooking));
        var checkoutButton = new Button { Text = "Tính tiền và thanh toán", AutoSize = true, Height = 36, Margin = new Padding(8, 24, 8, 8) };
        checkoutButton.Click += (_, _) => RunSafely(CheckOut);
        checkoutContent.Controls.Add(checkoutButton);
        checkoutContent.Controls.Add(_checkoutTotal);
        checkoutTab.Controls.Add(checkoutContent);

        _tabs.TabPages.AddRange([roomsTab, checkInTab, serviceTab, checkoutTab]);
        Controls.Add(_tabs);
    }

    private void LoadData()
    {
        _rooms = _bus.GetRooms();
        _roomGrid.DataSource = _rooms;
        if (_roomGrid.Columns[nameof(RoomDTO.GiaTheoGio)] is { } rateColumn)
        {
            rateColumn.DefaultCellStyle.Format = "N0";
        }

        _roomTypeFilter.Items.Clear();
        _roomTypeFilter.Items.Add("Tất cả");
        foreach (var type in _rooms.AsEnumerable()
                     .Select(row => row.Field<string>(nameof(RoomDTO.LoaiPhong)))
                     .OfType<string>()
                     .Distinct()
                     .OrderBy(value => value))
        {
            _roomTypeFilter.Items.Add(type);
        }
        _roomTypeFilter.SelectedIndex = 0;

        _roomStatusFilter.Items.Clear();
        _roomStatusFilter.Items.AddRange(["Tất cả", "Trống", "Đang ở", "Đang dọn dẹp"]);
        _roomStatusFilter.SelectedIndex = 0;

        _availableRoom.DataSource = _bus.GetAvailableRooms();
        _serviceChoice.DataSource = _bus.GetServices();
        var activeBookings = _bus.GetActiveBookings();
        _serviceBooking.DataSource = activeBookings.ToList();
        _checkoutBooking.DataSource = activeBookings.ToList();
        _checkoutTotal.Text = "Tổng thanh toán: --";
        ApplyRoomFilter();
    }

    private void ApplyRoomFilter()
    {
        if (_rooms is null)
        {
            return;
        }

        var conditions = new List<string>();
        if (_roomTypeFilter.SelectedItem is string type && type != "Tất cả")
        {
            conditions.Add($"[{nameof(RoomDTO.LoaiPhong)}] = '{EscapeFilterValue(type)}'");
        }
        if (_roomStatusFilter.SelectedItem is string status && status != "Tất cả")
        {
            conditions.Add($"[{nameof(RoomDTO.TrangThai)}] = '{EscapeFilterValue(status)}'");
        }
        _rooms.DefaultView.RowFilter = string.Join(" AND ", conditions);
    }

    private void CheckIn()
    {
        var customer = new CustomerDTO
        {
            HoTen = _customerName.Text.Trim(),
            CCCD = _customerId.Text.Trim(),
            SoDienThoai = string.IsNullOrWhiteSpace(_phone.Text) ? null : _phone.Text.Trim()
        };
        var roomNumber = _availableRoom.SelectedItem as string;
        var bookingId = _bus.CheckIn(customer, roomNumber ?? string.Empty);
        MessageBox.Show(this, $"Nhận phòng thành công. Mã phiếu: {bookingId}", "Thông báo",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
        LoadData();
    }

    private void AddService()
    {
        var booking = _serviceBooking.SelectedItem as BookingDTO;
        var service = _serviceChoice.SelectedItem as ServiceDTO;
        _bus.AddService(booking?.MaPhieu ?? 0, service?.MaDV ?? 0, decimal.ToInt32(_quantity.Value));
        MessageBox.Show(this, "Đã ghi nhận dịch vụ.", "Thông báo",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
        _quantity.Value = 1;
    }

    private void CheckOut()
    {
        var booking = _checkoutBooking.SelectedItem as BookingDTO;
        if (booking is null)
        {
            throw new InvalidOperationException("Hiện không có phiếu lưu trú cần thanh toán.");
        }

        var confirm = MessageBox.Show(this,
            $"Xác nhận thanh toán và trả phòng {booking.SoPhong}? Tổng tiền sẽ được tính đến thời điểm hiện tại.",
            "Xác nhận trả phòng", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (confirm != DialogResult.Yes)
        {
            return;
        }

        var total = _bus.CheckOut(booking.MaPhieu);
        _checkoutTotal.Text = $"Tổng thanh toán: {total:N0} đ";
        MessageBox.Show(this, $"Thanh toán thành công. Tổng tiền: {total:N0} đ", "Hóa đơn",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
        LoadData();
    }

    private void RunSafely(Action action)
    {
        try
        {
            action();
        }
        catch (Exception exception)
        {
            MessageBox.Show(this, exception.Message, "Không thể thực hiện thao tác",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private static string EscapeFilterValue(string value) => value.Replace("'", "''", StringComparison.Ordinal);

    private static ComboBox CreateDropDown() => new() { DropDownStyle = ComboBoxStyle.DropDownList };

    private static DataGridView CreateGrid() => new()
    {
        Dock = DockStyle.Fill,
        ReadOnly = true,
        AllowUserToAddRows = false,
        AllowUserToDeleteRows = false,
        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
        SelectionMode = DataGridViewSelectionMode.FullRowSelect,
        MultiSelect = false,
        RowHeadersVisible = false
    };

    private static Control CreateField(string title, Control control)
    {
        var field = new FlowLayoutPanel
        {
            FlowDirection = FlowDirection.TopDown,
            AutoSize = true,
            WrapContents = false,
            Margin = new Padding(8)
        };
        field.Controls.Add(new Label { Text = title, AutoSize = true, Margin = new Padding(3, 3, 3, 6) });
        field.Controls.Add(control);
        return field;
    }

    private static Control CreateInlineField(string title, Control control)
    {
        var field = new FlowLayoutPanel
        {
            FlowDirection = FlowDirection.TopDown,
            AutoSize = true,
            WrapContents = false,
            Margin = new Padding(8, 0, 14, 0)
        };
        field.Controls.Add(new Label { Text = title, AutoSize = true });
        field.Controls.Add(control);
        return field;
    }
}
