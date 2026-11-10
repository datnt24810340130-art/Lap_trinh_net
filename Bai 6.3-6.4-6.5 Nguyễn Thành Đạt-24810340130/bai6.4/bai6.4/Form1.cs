using System.Drawing;
using System.Windows.Forms;
using bai6._4.BUS;
using bai6._4.DAL;
using bai6._4.DTO;

namespace bai6._4
{
    public partial class Form1 : Form
    {
        private readonly Color _navy = Color.FromArgb(25, 45, 75);
        private readonly Color _blue = Color.FromArgb(42, 105, 180);
        private readonly Color _background = Color.FromArgb(243, 246, 250);
        private readonly GarageService _service;
        private readonly List<RepairDetailDto> _repairDetails = [];

        private DataGridView _recentRepairsGrid = null!;
        private DataGridView _partsGrid = null!;
        private DataGridView _repairDetailsGrid = null!;
        private Label _carsTodayValue = null!;
        private Label _repairsInProgressValue = null!;
        private Label _lowStockValue = null!;
        private TextBox _searchPlate = null!;
        private TextBox _ownerName = null!;
        private TextBox _ownerPhone = null!;
        private TextBox _ownerAddress = null!;
        private TextBox _intakePlate = null!;
        private TextBox _vehicleMake = null!;
        private NumericUpDown _vehicleYear = null!;
        private TextBox _partName = null!;
        private NumericUpDown _partPrice = null!;
        private NumericUpDown _partStock = null!;
        private TextBox _repairPlate = null!;
        private TextBox _repairOwner = null!;
        private TextBox _repairDescription = null!;
        private DateTimePicker _repairDate = null!;
        private ComboBox _repairPartCombo = null!;
        private NumericUpDown _repairQuantity = null!;
        private NumericUpDown _repairLabor = null!;
        private TextBox _repairTotal = null!;
        private int _currentCustomerId;
        private int? _activeTicketId;
        private decimal? _activeTicketTotal;
        private string? _activeTicketPlate;

        public Form1()
        {
            InitializeComponent();
            _service = new GarageService(new GarageRepository(new SqlConnectionFactory()));
            BuildInterface();
            Load += Form1_Load;
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
            RunSafely(() =>
            {
                _service.TestConnection();
                RefreshAllData();
            });
        }

        private void BuildInterface()
        {
            Text = "Garage Manager | Quản lý garage ô tô";
            StartPosition = FormStartPosition.CenterScreen;
            MinimumSize = new Size(1000, 700);
            Size = new Size(1280, 820);
            Font = new Font("Segoe UI", 9F);
            BackColor = _background;

            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 82,
                BackColor = _navy,
                Padding = new Padding(24, 12, 24, 10)
            };
            var title = new Label
            {
                Text = "GARAGE MANAGER",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(22, 10)
            };
            var subtitle = new Label
            {
                Text = "Hệ thống quản lý tiếp nhận, sửa chữa và phụ tùng",
                ForeColor = Color.FromArgb(205, 217, 232),
                AutoSize = true,
                Location = new Point(24, 48)
            };
            header.Controls.Add(title);
            header.Controls.Add(subtitle);

            var tabs = new TabControl
            {
                Dock = DockStyle.Fill,
                Padding = new Point(18, 8),
                Font = new Font("Segoe UI", 10F),
                BackColor = _background
            };
            tabs.TabPages.Add(CreateDashboardPage());
            tabs.TabPages.Add(CreateIntakePage());
            tabs.TabPages.Add(CreatePartsPage());
            tabs.TabPages.Add(CreateRepairPage());

            Controls.Add(tabs);
            Controls.Add(header);
        }

        private TabPage CreateDashboardPage()
        {
            var page = CreatePage("Tổng quan");
            var layout = CreatePageLayout(3, 125, 45, 55);

            var cards = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                WrapContents = false,
                Padding = new Padding(2),
                BackColor = _background
            };
            cards.Controls.Add(CreateStatCard("PHIẾU NHẬN HÔM NAY", "0", "Tổng phiếu tiếp nhận", _blue, out _carsTodayValue));
            cards.Controls.Add(CreateStatCard("PHIẾU ĐANG SỬA", "0", "Cần theo dõi tiến độ", Color.FromArgb(230, 146, 45), out _repairsInProgressValue));
            cards.Controls.Add(CreateStatCard("PHỤ TÙNG SẮP HẾT", "0", "Tồn kho không quá 5", Color.FromArgb(190, 75, 75), out _lowStockValue));

            _recentRepairsGrid = CreateGrid(
                ("MaPhieu", "Mã phiếu", 90),
                ("BienSo", "Biển số", 120),
                ("NgayNhan", "Ngày nhận", 140),
                ("NoiDungSua", "Nội dung sửa", 300),
                ("TongTien", "Tổng tiền", 140),
                ("TrangThai", "Trạng thái", 150));
            var recentGroup = CreateGroup("Phiếu sửa chữa gần đây", _recentRepairsGrid);

            var note = new Label
            {
                Text = "Chọn một chức năng ở các tab để bắt đầu quản lý garage.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.FromArgb(90, 103, 120),
                Padding = new Padding(8, 0, 0, 0)
            };

            layout.Controls.Add(cards, 0, 0);
            layout.Controls.Add(recentGroup, 0, 1);
            layout.Controls.Add(note, 0, 2);
            page.Controls.Add(layout);
            return page;
        }

        private TabPage CreateIntakePage()
        {
            var page = CreatePage("Tiếp nhận xe");
            var layout = CreatePageLayout(3, 105, 100, 100);

            var searchFlow = CreateFlowPanel();
            _searchPlate = new TextBox();
            searchFlow.Controls.Add(CreateField("Biển số cần tra cứu", _searchPlate, 230));
            var searchButton = CreateButton("Tra cứu xe", _blue);
            searchButton.Click += SearchVehicle_Click;
            searchFlow.Controls.Add(searchButton);
            var clearIntakeButton = CreateButton("Làm mới", Color.FromArgb(105, 118, 135));
            clearIntakeButton.Click += ClearIntake_Click;
            searchFlow.Controls.Add(clearIntakeButton);
            layout.Controls.Add(CreateGroup("Tra cứu thông tin xe", searchFlow), 0, 0);

            var ownerFlow = CreateFlowPanel();
            _ownerName = new TextBox();
            _ownerPhone = new TextBox();
            _ownerAddress = new TextBox();
            ownerFlow.Controls.Add(CreateField("Họ tên chủ xe *", _ownerName, 300));
            ownerFlow.Controls.Add(CreateField("Điện thoại", _ownerPhone, 220));
            ownerFlow.Controls.Add(CreateField("Địa chỉ", _ownerAddress, 420));
            layout.Controls.Add(CreateGroup("Thông tin chủ xe", ownerFlow), 0, 1);

            var vehicleFlow = CreateFlowPanel();
            _intakePlate = new TextBox();
            _vehicleMake = new TextBox();
            _vehicleYear = new NumericUpDown { Minimum = 1900, Maximum = 2100, Value = DateTime.Now.Year };
            vehicleFlow.Controls.Add(CreateField("Biển số xe *", _intakePlate, 220));
            vehicleFlow.Controls.Add(CreateField("Hãng xe", _vehicleMake, 220));
            vehicleFlow.Controls.Add(CreateField("Năm sản xuất", _vehicleYear, 160));
            var saveVehicleButton = CreateButton("Lưu tiếp nhận", _blue);
            saveVehicleButton.Click += SaveVehicle_Click;
            vehicleFlow.Controls.Add(saveVehicleButton);
            var clearVehicleButton = CreateButton("Xóa thông tin", Color.FromArgb(105, 118, 135));
            clearVehicleButton.Click += ClearIntake_Click;
            vehicleFlow.Controls.Add(clearVehicleButton);
            layout.Controls.Add(CreateGroup("Thông tin xe", vehicleFlow), 0, 2);

            page.Controls.Add(layout);
            return page;
        }

        private TabPage CreatePartsPage()
        {
            var page = CreatePage("Phụ tùng");
            var layout = CreatePageLayout(2, 155, 100);

            var editor = CreateFlowPanel();
            _partName = new TextBox();
            _partPrice = new NumericUpDown { Maximum = 1000000000, ThousandsSeparator = true, DecimalPlaces = 2, Width = 180 };
            _partStock = new NumericUpDown { Maximum = 1000000, Width = 140 };
            editor.Controls.Add(CreateField("Tên phụ tùng *", _partName, 300));
            editor.Controls.Add(CreateField("Đơn giá", _partPrice, 200));
            editor.Controls.Add(CreateField("Số lượng tồn", _partStock, 160));
            var addPartButton = CreateButton("Thêm phụ tùng", _blue);
            addPartButton.Click += AddPart_Click;
            editor.Controls.Add(addPartButton);
            var updatePartButton = CreateButton("Cập nhật", Color.FromArgb(57, 143, 105));
            updatePartButton.Click += UpdatePart_Click;
            editor.Controls.Add(updatePartButton);
            var deletePartButton = CreateButton("Xóa", Color.FromArgb(190, 75, 75));
            deletePartButton.Click += DeletePart_Click;
            editor.Controls.Add(deletePartButton);
            layout.Controls.Add(CreateGroup("Danh mục phụ tùng | Thêm và cập nhật tồn kho", editor), 0, 0);

            _partsGrid = CreateGrid(
                ("MaPhuTung", "Mã phụ tùng", 120),
                ("TenPhuTung", "Tên phụ tùng", 300),
                ("DonGia", "Đơn giá", 160),
                ("SoLuongTon", "Số lượng tồn", 140));
            _partsGrid.SelectionChanged += PartsGrid_SelectionChanged;
            layout.Controls.Add(CreateGroup("Danh sách phụ tùng", _partsGrid), 0, 1);

            page.Controls.Add(layout);
            return page;
        }

        private TabPage CreateRepairPage()
        {
            var page = CreatePage("Sửa chữa & thanh toán");
            var layout = CreatePageLayout(4, 105, 90, 100, 55);

            var vehicleFlow = CreateFlowPanel();
            _repairPlate = new TextBox();
            _repairOwner = new TextBox { ReadOnly = true };
            _repairDate = new DateTimePicker { Format = DateTimePickerFormat.Short };
            vehicleFlow.Controls.Add(CreateField("Biển số xe", _repairPlate, 220));
            var findRepairVehicleButton = CreateButton("Tìm xe", _blue);
            findRepairVehicleButton.Click += FindRepairVehicle_Click;
            vehicleFlow.Controls.Add(findRepairVehicleButton);
            vehicleFlow.Controls.Add(CreateField("Chủ xe", _repairOwner, 250));
            vehicleFlow.Controls.Add(CreateField("Ngày nhận", _repairDate, 180));
            layout.Controls.Add(CreateGroup("Thông tin tiếp nhận", vehicleFlow), 0, 0);

            var descriptionFlow = CreateFlowPanel();
            _repairDescription = new TextBox();
            descriptionFlow.Controls.Add(CreateField("Nội dung sửa chữa", _repairDescription, 600));
            descriptionFlow.Controls.Add(CreateField("Trạng thái", new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Items = { "Đang sửa", "Đã hoàn thành", "Đã thanh toán" } }, 200));
            layout.Controls.Add(CreateGroup("Mô tả dịch vụ", descriptionFlow), 0, 1);

            var partsFlow = CreateFlowPanel();
            _repairPartCombo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            _repairQuantity = new NumericUpDown { Minimum = 1, Maximum = 1000, Value = 1 };
            _repairLabor = new NumericUpDown { Maximum = 1000000000, ThousandsSeparator = true, DecimalPlaces = 2 };
            partsFlow.Controls.Add(CreateField("Phụ tùng", _repairPartCombo, 300));
            partsFlow.Controls.Add(CreateField("Số lượng", _repairQuantity, 140));
            partsFlow.Controls.Add(CreateField("Tiền công", _repairLabor, 200));
            var addRepairDetailButton = CreateButton("Thêm vào phiếu", _blue);
            addRepairDetailButton.Click += AddRepairDetail_Click;
            partsFlow.Controls.Add(addRepairDetailButton);
            _repairDetailsGrid = CreateGrid(
                ("TenPhuTung", "Phụ tùng / dịch vụ", 320),
                ("SoLuong", "Số lượng", 100),
                ("DonGia", "Đơn giá", 150),
                ("TienCong", "Tiền công", 150),
                ("ThanhTien", "Thành tiền", 170));
            var detailsLayout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, Padding = new Padding(8) };
            detailsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 72));
            detailsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            partsFlow.Dock = DockStyle.Fill;
            detailsLayout.Controls.Add(partsFlow, 0, 0);
            detailsLayout.Controls.Add(_repairDetailsGrid, 0, 1);
            layout.Controls.Add(CreateGroup("Phụ tùng thay thế và tiền công", detailsLayout), 0, 2);

            var paymentFlow = CreateFlowPanel();
            var saveRepairButton = CreateButton("Lưu phiếu sửa chữa", _blue);
            saveRepairButton.Click += SaveRepair_Click;
            paymentFlow.Controls.Add(saveRepairButton);
            var payRepairButton = CreateButton("Thanh toán / xuất hóa đơn", Color.FromArgb(57, 143, 105));
            payRepairButton.Click += PayRepair_Click;
            paymentFlow.Controls.Add(payRepairButton);
            _repairTotal = new TextBox { ReadOnly = true, Text = "0" };
            paymentFlow.Controls.Add(CreateField("Tổng tiền", _repairTotal, 220));
            layout.Controls.Add(paymentFlow, 0, 3);

            page.Controls.Add(layout);
            return page;
        }

        private void RefreshAllData()
        {
            RefreshParts();
            RefreshRepairs();
            var counts = _service.GetDashboardCounts();
            _carsTodayValue.Text = counts.ReceiptsToday.ToString();
            _repairsInProgressValue.Text = counts.RepairsInProgress.ToString();
            _lowStockValue.Text = counts.LowStockParts.ToString();
        }

        private void RefreshParts()
        {
            var parts = _service.GetParts();
            _partsGrid.DataSource = null;
            _partsGrid.DataSource = parts;
            _partsGrid.ClearSelection();
            _repairPartCombo.DataSource = null;
            _repairPartCombo.DisplayMember = nameof(PartDto.TenPhuTung);
            _repairPartCombo.ValueMember = nameof(PartDto.MaPhuTung);
            _repairPartCombo.DataSource = parts;
        }

        private void RefreshRepairs()
        {
            _recentRepairsGrid.DataSource = null;
            _recentRepairsGrid.DataSource = _service.GetRecentRepairs();
            _recentRepairsGrid.ClearSelection();
        }

        private void RunSafely(Action action)
        {
            try
            {
                action();
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, exception.Message, "Không thể hoàn tất thao tác", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchVehicle_Click(object? sender, EventArgs e)
        {
            RunSafely(() =>
            {
                var vehicle = _service.FindVehicle(_searchPlate.Text);
                if (vehicle is null)
                {
                    _currentCustomerId = 0;
                    _intakePlate.Text = _searchPlate.Text.Trim();
                    _ownerName.Clear();
                    _ownerPhone.Clear();
                    _ownerAddress.Clear();
                    _vehicleMake.Clear();
                    _vehicleYear.Value = DateTime.Now.Year;
                    MessageBox.Show(this, "Chưa có xe này. Nhập thông tin chủ xe và xe rồi chọn Lưu tiếp nhận.", "Tra cứu xe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                _currentCustomerId = vehicle.MaKH;
                _intakePlate.Text = vehicle.BienSo;
                _ownerName.Text = vehicle.TenKH;
                _ownerPhone.Text = vehicle.DienThoai ?? string.Empty;
                _ownerAddress.Text = vehicle.DiaChi ?? string.Empty;
                _vehicleMake.Text = vehicle.HieuXe ?? string.Empty;
                _vehicleYear.Value = vehicle.NamSanXuat is >= 1900 and <= 2100 ? vehicle.NamSanXuat.Value : DateTime.Now.Year;
            });
        }

        private void SaveVehicle_Click(object? sender, EventArgs e)
        {
            RunSafely(() =>
            {
                _service.SaveVehicle(new VehicleDto
                {
                    MaKH = _currentCustomerId,
                    BienSo = _intakePlate.Text,
                    TenKH = _ownerName.Text,
                    DienThoai = OptionalText(_ownerPhone),
                    DiaChi = OptionalText(_ownerAddress),
                    HieuXe = OptionalText(_vehicleMake),
                    NamSanXuat = (int)_vehicleYear.Value
                });
                _searchPlate.Text = _intakePlate.Text;
                MessageBox.Show(this, "Đã lưu thông tin tiếp nhận xe.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshAllData();
            });
        }

        private void ClearIntake_Click(object? sender, EventArgs e)
        {
            _currentCustomerId = 0;
            _searchPlate.Clear();
            _intakePlate.Clear();
            _ownerName.Clear();
            _ownerPhone.Clear();
            _ownerAddress.Clear();
            _vehicleMake.Clear();
            _vehicleYear.Value = DateTime.Now.Year;
        }

        private void AddPart_Click(object? sender, EventArgs e)
        {
            RunSafely(() =>
            {
                _service.AddPart(ReadPartEditor());
                ClearPartEditor();
                RefreshAllData();
            });
        }

        private void UpdatePart_Click(object? sender, EventArgs e)
        {
            RunSafely(() =>
            {
                var part = ReadPartEditor();
                part.MaPhuTung = SelectedPartId();
                _service.UpdatePart(part);
                ClearPartEditor();
                RefreshAllData();
            });
        }

        private void DeletePart_Click(object? sender, EventArgs e)
        {
            var selectedId = SelectedPartId();
            if (selectedId <= 0)
            {
                MessageBox.Show(this, "Chọn phụ tùng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show(this, "Bạn có chắc muốn xóa phụ tùng đã chọn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }
            RunSafely(() =>
            {
                _service.DeletePart(selectedId);
                ClearPartEditor();
                RefreshAllData();
            });
        }

        private void PartsGrid_SelectionChanged(object? sender, EventArgs e)
        {
            if (_partsGrid.SelectedRows.Count == 0 || _partsGrid.SelectedRows[0].DataBoundItem is not PartDto part)
            {
                return;
            }
            _partName.Text = part.TenPhuTung;
            _partPrice.Value = Math.Min(_partPrice.Maximum, Math.Max(_partPrice.Minimum, part.DonGia));
            _partStock.Value = Math.Min(_partStock.Maximum, Math.Max(_partStock.Minimum, part.SoLuongTon));
        }

        private void FindRepairVehicle_Click(object? sender, EventArgs e)
        {
            RunSafely(() =>
            {
                var vehicle = _service.FindVehicle(_repairPlate.Text);
                _repairOwner.Text = vehicle?.TenKH ?? string.Empty;
                if (vehicle is null)
                {
                    MessageBox.Show(this, "Không tìm thấy biển số xe trong danh sách tiếp nhận.", "Tra cứu xe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            });
        }

        private void AddRepairDetail_Click(object? sender, EventArgs e)
        {
            if (_repairPartCombo.SelectedItem is not PartDto part)
            {
                MessageBox.Show(this, "Hãy thêm/chọn phụ tùng trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var quantity = (int)_repairQuantity.Value;
            var existing = _repairDetails.FirstOrDefault(detail => detail.MaPhuTung == part.MaPhuTung);
            if (quantity + (existing?.SoLuong ?? 0) > part.SoLuongTon)
            {
                MessageBox.Show(this, $"Tồn kho '{part.TenPhuTung}' hiện có {part.SoLuongTon}.", "Không đủ tồn kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (existing is null)
            {
                _repairDetails.Add(new RepairDetailDto
                {
                    MaPhuTung = part.MaPhuTung,
                    TenPhuTung = part.TenPhuTung,
                    SoLuong = quantity,
                    DonGia = part.DonGia,
                    TienCong = _repairLabor.Value
                });
            }
            else
            {
                existing.SoLuong += quantity;
                existing.TienCong += _repairLabor.Value;
            }
            RefreshRepairDetails();
        }

        private void SaveRepair_Click(object? sender, EventArgs e)
        {
            RunSafely(() =>
            {
                var ticketId = _service.CreateRepairTicket(new RepairTicketDto
                {
                    BienSo = _repairPlate.Text,
                    NgayNhan = _repairDate.Value,
                    NoiDungSua = OptionalText(_repairDescription),
                    ChiTiet = _repairDetails.ToList()
                });
                _activeTicketId = ticketId;
                _activeTicketTotal = _repairDetails.Sum(detail => detail.ThanhTien);
                _activeTicketPlate = _repairPlate.Text.Trim();
                _repairDetails.Clear();
                RefreshRepairDetails();
                RefreshAllData();
                MessageBox.Show(this, $"Đã lưu phiếu #{ticketId}, cập nhật tồn kho và ghi nhận doanh thu.", "Lưu phiếu thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });
        }

        private void PayRepair_Click(object? sender, EventArgs e)
        {
            var ticketId = _activeTicketId;
            RepairTicketDto? selectedTicket = null;
            if (_recentRepairsGrid.SelectedRows.Count > 0)
            {
                selectedTicket = _recentRepairsGrid.SelectedRows[0].DataBoundItem as RepairTicketDto;
                ticketId = selectedTicket?.MaPhieu;
            }
            if (ticketId is null)
            {
                MessageBox.Show(this, "Chọn phiếu trên tab Tổng quan hoặc lưu phiếu sửa chữa trước khi thanh toán.", "Chưa chọn phiếu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            RunSafely(() =>
            {
                _service.MarkRepairPaid(ticketId.Value);
                var total = selectedTicket?.TongTien ?? _activeTicketTotal ?? 0;
                var plate = selectedTicket?.BienSo ?? _activeTicketPlate ?? _repairPlate.Text;
                RefreshAllData();
                MessageBox.Show(this,
                    $"HÓA ĐƠN DỊCH VỤ GARAGE\nMã phiếu: {ticketId}\nBiển số: {plate}\nTổng thanh toán: {total:N0} VNĐ\nTrạng thái: Đã thanh toán",
                    "Hóa đơn thanh toán", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _activeTicketId = null;
                _activeTicketTotal = null;
                _activeTicketPlate = null;
            });
        }

        private void RefreshRepairDetails()
        {
            _repairDetailsGrid.DataSource = null;
            _repairDetailsGrid.DataSource = _repairDetails.ToList();
            _repairTotal.Text = _repairDetails.Sum(detail => detail.ThanhTien).ToString("N0");
        }

        private PartDto ReadPartEditor() => new()
        {
            TenPhuTung = _partName.Text,
            DonGia = _partPrice.Value,
            SoLuongTon = (int)_partStock.Value
        };

        private int SelectedPartId()
            => _partsGrid.SelectedRows.Count > 0 && _partsGrid.SelectedRows[0].DataBoundItem is PartDto part ? part.MaPhuTung : 0;

        private void ClearPartEditor()
        {
            _partName.Clear();
            _partPrice.Value = 0;
            _partStock.Value = 0;
            _partsGrid.ClearSelection();
        }

        private static string? OptionalText(TextBox textBox)
            => string.IsNullOrWhiteSpace(textBox.Text) ? null : textBox.Text.Trim();

        private TabPage CreatePage(string title)
        {
            return new TabPage(title) { BackColor = _background, Padding = new Padding(8) };
        }

        private TableLayoutPanel CreatePageLayout(int rows, params int[] rowHeights)
        {
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = rows,
                Padding = new Padding(14),
                BackColor = _background
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            for (var i = 0; i < rows; i++)
            {
                var height = rowHeights[i];
                layout.RowStyles.Add(height <= 100
                    ? new RowStyle(SizeType.Percent, height)
                    : new RowStyle(SizeType.Absolute, height));
            }
            return layout;
        }

        private GroupBox CreateGroup(string title, Control content)
        {
            var group = new GroupBox
            {
                Text = title,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = _navy,
                Padding = new Padding(10)
            };
            content.Dock = DockStyle.Fill;
            content.Margin = new Padding(4);
            group.Controls.Add(content);
            return group;
        }

        private FlowLayoutPanel CreateFlowPanel()
        {
            return new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                WrapContents = true,
                Padding = new Padding(8),
                BackColor = Color.White
            };
        }

        private Control CreateField(string labelText, Control input, int width)
        {
            var field = new Panel { Width = width, Height = 64, Margin = new Padding(8, 4, 8, 4) };
            var label = new Label
            {
                Text = labelText,
                AutoSize = false,
                Height = 22,
                Dock = DockStyle.Top,
                ForeColor = Color.FromArgb(70, 82, 98),
                Font = new Font("Segoe UI", 9F)
            };
            input.Dock = DockStyle.Bottom;
            input.Width = width;
            input.Height = 30;
            input.Font = new Font("Segoe UI", 9F);
            field.Controls.Add(input);
            field.Controls.Add(label);
            return field;
        }

        private Button CreateButton(string text, Color color)
        {
            return new Button
            {
                Text = text,
                Width = 165,
                Height = 36,
                Margin = new Padding(10, 24, 8, 4),
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
        }

        private Panel CreateStatCard(string title, string value, string detail, Color accent, out Label valueLabel)
        {
            var card = new Panel
            {
                Width = 270,
                Height = 100,
                Margin = new Padding(8),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            card.Controls.Add(new Panel { Dock = DockStyle.Left, Width = 5, BackColor = accent });
            card.Controls.Add(new Label
            {
                Text = title,
                Location = new Point(18, 12),
                AutoSize = true,
                ForeColor = Color.FromArgb(100, 112, 128),
                Font = new Font("Segoe UI", 8F, FontStyle.Bold)
            });
            valueLabel = new Label
            {
                Text = value,
                Location = new Point(18, 32),
                AutoSize = true,
                ForeColor = _navy,
                Font = new Font("Segoe UI", 22F, FontStyle.Bold)
            };
            card.Controls.Add(valueLabel);
            card.Controls.Add(new Label
            {
                Text = detail,
                Location = new Point(88, 52),
                AutoSize = true,
                ForeColor = Color.FromArgb(120, 130, 143)
            });
            return card;
        }

        private DataGridView CreateGrid(params (string Name, string Header, int Width)[] columns)
        {
            var grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                EnableHeadersVisualStyles = false,
                GridColor = Color.FromArgb(230, 234, 239)
            };
            grid.ColumnHeadersDefaultCellStyle.BackColor = _navy;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grid.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 234, 250);
            grid.DefaultCellStyle.SelectionForeColor = _navy;
            grid.RowTemplate.Height = 30;
            foreach (var column in columns)
            {
                grid.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = column.Name,
                    DataPropertyName = column.Name,
                    HeaderText = column.Header,
                    MinimumWidth = 80,
                    FillWeight = column.Width
                });
            }
            return grid;
        }
    }
}
