using bai6._5.BUS;
using bai6._5.DTO;
using System.Data;
using System.Drawing;

namespace bai6._5.GUI;

public sealed class KaraokeForm : Form
{
    private readonly KaraokeBus _bus = new();
    private readonly FlowLayoutPanel _roomPanel = new();
    private readonly ComboBox _serviceComboBox = new();
    private readonly NumericUpDown _quantityInput = new();
    private readonly DataGridView _serviceGrid = new();
    private readonly Button _openButton = new();
    private readonly Button _addServiceButton = new();
    private readonly Button _checkoutButton = new();
    private readonly Label _selectedRoomLabel = new();
    private readonly Label _elapsedLabel = new();
    private readonly Label _serviceTotalLabel = new();
    private readonly System.Windows.Forms.Timer _timer = new() { Interval = 1000 };
    private List<RoomDto> _rooms = [];
    private RoomDto? _selectedRoom;

    public KaraokeForm()
    {
        Text = "Hệ thống quản lý Karaoke";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(1000, 650);
        Size = new Size(1180, 760);
        Font = new Font("Segoe UI", 10F);
        BackColor = Color.FromArgb(245, 247, 250);
        BuildLayout();
        WireEvents();
        LoadServices();
        RefreshRooms();
        _timer.Start();
    }

    private void BuildLayout()
    {
        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 2,
            Padding = new Padding(18),
            BackColor = BackColor
        };
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 72));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        Controls.Add(root);

        var title = new Label
        {
            Text = "KARAOKE MANAGEMENT SYSTEM",
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 20F, FontStyle.Bold),
            ForeColor = Color.FromArgb(35, 54, 80),
            TextAlign = ContentAlignment.MiddleLeft
        };
        root.Controls.Add(title, 0, 0);

        var content = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 1,
            BackColor = Color.White,
            Padding = new Padding(14)
        };
        content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42));
        content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58));
        root.Controls.Add(content, 0, 1);

        var roomSection = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, Padding = new Padding(4) };
        roomSection.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
        roomSection.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        roomSection.Controls.Add(CreateSectionTitle("Sơ đồ phòng hát"), 0, 0);
        _roomPanel.Dock = DockStyle.Fill;
        _roomPanel.AutoScroll = true;
        _roomPanel.WrapContents = true;
        _roomPanel.Padding = new Padding(4);
        roomSection.Controls.Add(_roomPanel, 0, 1);
        content.Controls.Add(roomSection, 0, 0);

        var rightSection = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 5, Padding = new Padding(12, 4, 4, 4) };
        rightSection.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
        rightSection.RowStyles.Add(new RowStyle(SizeType.Absolute, 82));
        rightSection.RowStyles.Add(new RowStyle(SizeType.Absolute, 76));
        rightSection.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        rightSection.RowStyles.Add(new RowStyle(SizeType.Absolute, 54));
        rightSection.Controls.Add(CreateSectionTitle("Thông tin & dịch vụ"), 0, 0);

        var roomInfo = new Panel { Dock = DockStyle.Fill, Padding = new Padding(4) };
        _selectedRoomLabel.Dock = DockStyle.Top;
        _selectedRoomLabel.Height = 30;
        _selectedRoomLabel.Font = new Font(Font, FontStyle.Bold);
        _selectedRoomLabel.Text = "Chưa chọn phòng";
        _elapsedLabel.Dock = DockStyle.Top;
        _elapsedLabel.Height = 28;
        _elapsedLabel.ForeColor = Color.DimGray;
        roomInfo.Controls.Add(_elapsedLabel);
        roomInfo.Controls.Add(_selectedRoomLabel);
        rightSection.Controls.Add(roomInfo, 0, 1);

        var orderControls = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 2, Padding = new Padding(0, 4, 0, 4) };
        orderControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55));
        orderControls.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 85));
        orderControls.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 105));
        orderControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45));
        orderControls.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));
        orderControls.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        orderControls.Controls.Add(new Label { Text = "Dịch vụ", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft }, 0, 0);
        orderControls.Controls.Add(new Label { Text = "Số lượng", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft }, 1, 0);
        _serviceComboBox.Dock = DockStyle.Fill;
        _serviceComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        orderControls.Controls.Add(_serviceComboBox, 0, 1);
        _quantityInput.Dock = DockStyle.Fill;
        _quantityInput.Minimum = 1;
        _quantityInput.Maximum = 999;
        orderControls.Controls.Add(_quantityInput, 1, 1);
        _addServiceButton.Text = "Gọi dịch vụ";
        _addServiceButton.Dock = DockStyle.Fill;
        _addServiceButton.BackColor = Color.FromArgb(42, 121, 197);
        _addServiceButton.ForeColor = Color.White;
        _addServiceButton.FlatStyle = FlatStyle.Flat;
        orderControls.Controls.Add(_addServiceButton, 2, 1);
        orderControls.SetColumnSpan(_addServiceButton, 2);
        rightSection.Controls.Add(orderControls, 0, 2);

        _serviceGrid.Dock = DockStyle.Fill;
        _serviceGrid.ReadOnly = true;
        _serviceGrid.AllowUserToAddRows = false;
        _serviceGrid.AllowUserToDeleteRows = false;
        _serviceGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        _serviceGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _serviceGrid.MultiSelect = false;
        _serviceGrid.RowHeadersVisible = false;
        _serviceGrid.BackgroundColor = Color.White;
        rightSection.Controls.Add(_serviceGrid, 0, 3);

        var bottom = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 3, RowCount = 1, Padding = new Padding(0, 8, 0, 0) };
        bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 105));
        bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 125));
        _serviceTotalLabel.Dock = DockStyle.Fill;
        _serviceTotalLabel.Text = "Tiền dịch vụ: 0 đ";
        _serviceTotalLabel.TextAlign = ContentAlignment.MiddleLeft;
        bottom.Controls.Add(_serviceTotalLabel, 0, 0);
        _openButton.Text = "Mở phòng";
        _openButton.Dock = DockStyle.Fill;
        _openButton.BackColor = Color.FromArgb(43, 150, 87);
        _openButton.ForeColor = Color.White;
        _openButton.FlatStyle = FlatStyle.Flat;
        bottom.Controls.Add(_openButton, 1, 0);
        _checkoutButton.Text = "Thanh toán";
        _checkoutButton.Dock = DockStyle.Fill;
        _checkoutButton.BackColor = Color.FromArgb(213, 78, 70);
        _checkoutButton.ForeColor = Color.White;
        _checkoutButton.FlatStyle = FlatStyle.Flat;
        bottom.Controls.Add(_checkoutButton, 2, 0);
        rightSection.Controls.Add(bottom, 0, 4);
        content.Controls.Add(rightSection, 1, 0);
    }

    private static Label CreateSectionTitle(string text) => new()
    {
        Text = text,
        Dock = DockStyle.Fill,
        Font = new Font("Segoe UI", 13F, FontStyle.Bold),
        ForeColor = Color.FromArgb(35, 54, 80),
        TextAlign = ContentAlignment.MiddleLeft
    };

    private void WireEvents()
    {
        _openButton.Click += (_, _) => OpenSelectedRoom();
        _addServiceButton.Click += (_, _) => AddSelectedService();
        _checkoutButton.Click += (_, _) => CheckoutSelectedRoom();
        _timer.Tick += (_, _) => UpdateElapsedTime();
        FormClosed += (_, _) => _timer.Dispose();
    }

    private void LoadServices()
    {
        try
        {
            _serviceComboBox.DataSource = _bus.GetServices();
            _serviceComboBox.Enabled = _serviceComboBox.Items.Count > 0;
        }
        catch (Exception ex)
        {
            ShowError(ex);
        }
    }

    private void RefreshRooms(int? roomToSelect = null)
    {
        try
        {
            _rooms = _bus.GetRooms();
            _roomPanel.SuspendLayout();
            _roomPanel.Controls.Clear();
            foreach (var room in _rooms)
            {
                var isOccupied = room.TrangThai == "Đang hát";
                var button = new Button
                {
                    Text = $"{room.TenPhong}\n{room.LoaiPhong} · {room.GiaTheoGio:N0} đ/giờ\n{room.TrangThai}",
                    Tag = room,
                    Width = 185,
                    Height = 108,
                    Margin = new Padding(7),
                    BackColor = isOccupied ? Color.FromArgb(211, 76, 69) : Color.FromArgb(49, 157, 96),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                button.FlatAppearance.BorderSize = 0;
                button.Click += (_, _) => SelectRoom((RoomDto)button.Tag!);
                _roomPanel.Controls.Add(button);
            }
            _roomPanel.ResumeLayout();
            if (roomToSelect.HasValue)
                SelectRoom(_rooms.FirstOrDefault(room => room.MaPhong == roomToSelect.Value));
            else if (_selectedRoom is not null)
                SelectRoom(_rooms.FirstOrDefault(room => room.MaPhong == _selectedRoom.MaPhong));
            else
                SelectRoom(null);
        }
        catch (Exception ex)
        {
            ShowError(ex);
        }
    }

    private void SelectRoom(RoomDto? room)
    {
        _selectedRoom = room;
        if (room is null)
        {
            _selectedRoomLabel.Text = "Chưa chọn phòng";
            _elapsedLabel.Text = string.Empty;
            _serviceGrid.DataSource = null;
        }
        else
        {
            _selectedRoomLabel.Text = $"{room.TenPhong} — {room.TrangThai}";
            _serviceGrid.DataSource = room.MaHD.HasValue ? _bus.GetBillServices(room.MaHD.Value) : null;
        }

        _openButton.Enabled = room is not null && room.TrangThai == "Trống";
        _checkoutButton.Enabled = room is not null && room.TrangThai == "Đang hát";
        _addServiceButton.Enabled = room is not null && room.TrangThai == "Đang hát" && _serviceComboBox.Items.Count > 0;
        if (_serviceGrid.Columns.Contains("MaDV"))
            _serviceGrid.Columns["MaDV"].Visible = false;
        UpdateServiceTotal();
        UpdateElapsedTime();
    }

    private void OpenSelectedRoom()
    {
        if (_selectedRoom is null)
            return;
        try
        {
            var roomId = _selectedRoom.MaPhong;
            _bus.OpenRoom(_selectedRoom);
            RefreshRooms(roomId);
            LoadServices();
        }
        catch (Exception ex)
        {
            ShowError(ex);
            RefreshRooms(_selectedRoom.MaPhong);
        }
    }

    private void AddSelectedService()
    {
        if (_selectedRoom?.MaHD is not int billId || _serviceComboBox.SelectedItem is not ServiceDto service)
            return;
        try
        {
            _bus.AddService(billId, service, (int)_quantityInput.Value);
            SelectRoom(_selectedRoom);
            _quantityInput.Value = 1;
        }
        catch (Exception ex)
        {
            ShowError(ex);
            LoadServices();
        }
    }

    private void CheckoutSelectedRoom()
    {
        if (_selectedRoom is null)
            return;
        var roomId = _selectedRoom.MaPhong;
        try
        {
            var result = _bus.Checkout(_selectedRoom);
            MessageBox.Show(
                $"Hóa đơn #{result.MaHD}\nTiền phòng: {result.TongTienPhong:N0} đ\nTiền dịch vụ: {result.TongTienDichVu:N0} đ\nTổng thanh toán: {result.TongThanhToan:N0} đ",
                "Thanh toán thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshRooms(roomId);
            LoadServices();
        }
        catch (Exception ex)
        {
            ShowError(ex);
            RefreshRooms(roomId);
        }
    }

    private void UpdateElapsedTime()
    {
        if (_selectedRoom?.TrangThai == "Đang hát" && _selectedRoom.GioVao is DateTime startedAt)
        {
            var elapsed = DateTime.Now - startedAt;
            _elapsedLabel.Text = $"Giờ vào: {startedAt:dd/MM/yyyy HH:mm:ss} · Đã hát: {elapsed:hh\\:mm\\:ss}";
        }
        else
        {
            _elapsedLabel.Text = string.Empty;
        }
    }

    private void UpdateServiceTotal()
    {
        if (_serviceGrid.DataSource is not DataTable table || !table.Columns.Contains("ThanhTien"))
        {
            _serviceTotalLabel.Text = "Tiền dịch vụ: 0 đ";
            return;
        }

        var total = table.AsEnumerable().Sum(row => row.Field<decimal>("ThanhTien"));
        _serviceTotalLabel.Text = $"Tiền dịch vụ: {total:N0} đ";
    }

    private void ShowError(Exception exception) =>
        MessageBox.Show(exception.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
}
