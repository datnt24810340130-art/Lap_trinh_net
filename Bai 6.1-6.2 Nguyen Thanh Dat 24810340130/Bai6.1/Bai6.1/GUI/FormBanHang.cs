using Bai6._1.BUS;
using Bai6._1.DTO;
using System.ComponentModel;

namespace Bai6._1.GUI;

public sealed class FormBanHang : Form
{
    private readonly BanHangBus _banHangBus = new();
    private readonly BindingList<ChiTietHoaDonDto> _gioHang = [];
    private readonly TextBox _txtTimKiem = new();
    private readonly ComboBox _cboSanPham = new();
    private readonly ComboBox _cboNhanVien = new();
    private readonly ComboBox _cboKhachHang = new();
    private readonly NumericUpDown _numSoLuong = new();
    private readonly DataGridView _gridGioHang = new();
    private readonly Label _lblTongTien = new();
    private readonly DateTimePicker _dateNgay = new();
    private readonly DateTimePicker _dateThang = new();
    private readonly Label _lblDoanhThu = new();
    private readonly DataGridView _gridTopSanPham = new();

    public FormBanHang()
    {
        Text = "Hệ thống quản lý bán hàng - Siêu thị mini";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(900, 650);
        ClientSize = new Size(1150, 760);
        BuildUi();
        Load += (_, _) => LoadCatalogs();
    }

    private void BuildUi()
    {
        var tabs = new TabControl { Dock = DockStyle.Fill };
        var checkoutTab = new TabPage("Bán hàng");
        var reportTab = new TabPage("Báo cáo - Thống kê");
        var catalogTab = new TabPage("Quản lý danh mục");
        tabs.TabPages.Add(checkoutTab);
        tabs.TabPages.Add(reportTab);
        tabs.TabPages.Add(catalogTab);
        catalogTab.Controls.Add(new DanhMucControl());
        Controls.Add(tabs);

        var checkoutLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 3,
            Padding = new Padding(10)
        };
        checkoutLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 145));
        checkoutLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        checkoutLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 65));
        checkoutTab.Controls.Add(checkoutLayout);

        var inputPanel = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2 };
        inputPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        inputPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        checkoutLayout.Controls.Add(inputPanel, 0, 0);

        var productPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false, AutoScroll = true, Padding = new Padding(0, 8, 0, 0) };
        productPanel.Controls.Add(new Label { Text = "Tìm mã/tên:", AutoSize = true, Margin = new Padding(3, 10, 3, 3) });
        _txtTimKiem.Width = 180;
        productPanel.Controls.Add(_txtTimKiem);
        var btnTimKiem = new Button { Text = "Tìm sản phẩm", AutoSize = true };
        btnTimKiem.Click += (_, _) => SearchProducts();
        productPanel.Controls.Add(btnTimKiem);
        _cboSanPham.Width = 310;
        _cboSanPham.DropDownStyle = ComboBoxStyle.DropDownList;
        productPanel.Controls.Add(_cboSanPham);
        productPanel.Controls.Add(new Label { Text = "Số lượng:", AutoSize = true, Margin = new Padding(3, 10, 3, 3) });
        _numSoLuong.Minimum = 1;
        _numSoLuong.Maximum = 100000;
        _numSoLuong.Value = 1;
        _numSoLuong.Width = 75;
        productPanel.Controls.Add(_numSoLuong);
        var btnThem = new Button { Text = "Thêm vào giỏ", AutoSize = true };
        btnThem.Click += (_, _) => AddToCart();
        productPanel.Controls.Add(btnThem);
        inputPanel.Controls.Add(productPanel, 0, 0);

        var customerPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false, AutoScroll = true, Padding = new Padding(0, 8, 0, 0) };
        customerPanel.Controls.Add(new Label { Text = "Nhân viên:", AutoSize = true, Margin = new Padding(3, 10, 3, 3) });
        _cboNhanVien.Width = 280;
        _cboNhanVien.DropDownStyle = ComboBoxStyle.DropDownList;
        customerPanel.Controls.Add(_cboNhanVien);
        customerPanel.Controls.Add(new Label { Text = "Khách hàng:", AutoSize = true, Margin = new Padding(15, 10, 3, 3) });
        _cboKhachHang.Width = 300;
        _cboKhachHang.DropDownStyle = ComboBoxStyle.DropDownList;
        customerPanel.Controls.Add(_cboKhachHang);
        inputPanel.Controls.Add(customerPanel, 0, 1);

        _gridGioHang.Dock = DockStyle.Fill;
        _gridGioHang.AutoGenerateColumns = true;
        _gridGioHang.AllowUserToAddRows = false;
        _gridGioHang.ReadOnly = true;
        _gridGioHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _gridGioHang.DataSource = _gioHang;
        _gridGioHang.DataBindingComplete += (_, _) => ConfigureCartColumns();
        checkoutLayout.Controls.Add(_gridGioHang, 0, 1);

        var checkoutPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft, WrapContents = false, Padding = new Padding(5, 12, 5, 0) };
        var btnThanhToan = new Button { Text = "Thanh toán", AutoSize = true };
        btnThanhToan.Click += (_, _) => Checkout();
        _lblTongTien.AutoSize = true;
        _lblTongTien.Font = new Font(Font, FontStyle.Bold);
        _lblTongTien.Margin = new Padding(15, 8, 15, 0);
        checkoutPanel.Controls.Add(btnThanhToan);
        checkoutPanel.Controls.Add(_lblTongTien);
        var btnXoaDong = new Button { Text = "Xóa dòng chọn", AutoSize = true };
        btnXoaDong.Click += (_, _) => RemoveSelectedCartLine();
        checkoutPanel.Controls.Add(btnXoaDong);
        checkoutLayout.Controls.Add(checkoutPanel, 0, 2);

        BuildReportUi(reportTab);
        UpdateTotal();
    }

    private void BuildReportUi(Control parent)
    {
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3, Padding = new Padding(12) };
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 75));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 55));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        parent.Controls.Add(layout);

        var revenuePanel = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false, Padding = new Padding(0, 12, 0, 0) };
        _dateNgay.Format = DateTimePickerFormat.Short;
        revenuePanel.Controls.Add(new Label { Text = "Doanh thu ngày:", AutoSize = true, Margin = new Padding(3, 8, 3, 3) });
        revenuePanel.Controls.Add(_dateNgay);
        var btnDoanhThuNgay = new Button { Text = "Xem ngày", AutoSize = true };
        btnDoanhThuNgay.Click += (_, _) => ShowDailyRevenue();
        revenuePanel.Controls.Add(btnDoanhThuNgay);
        _dateThang.Format = DateTimePickerFormat.Custom;
        _dateThang.CustomFormat = "MM/yyyy";
        _dateThang.ShowUpDown = true;
        revenuePanel.Controls.Add(new Label { Text = "Doanh thu tháng:", AutoSize = true, Margin = new Padding(20, 8, 3, 3) });
        revenuePanel.Controls.Add(_dateThang);
        var btnDoanhThuThang = new Button { Text = "Xem tháng", AutoSize = true };
        btnDoanhThuThang.Click += (_, _) => ShowMonthlyRevenue();
        revenuePanel.Controls.Add(btnDoanhThuThang);
        _lblDoanhThu.AutoSize = true;
        _lblDoanhThu.Margin = new Padding(15, 8, 3, 3);
        revenuePanel.Controls.Add(_lblDoanhThu);
        layout.Controls.Add(revenuePanel, 0, 0);

        var topPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(0, 8, 0, 0) };
        var btnTop = new Button { Text = "Tải Top 5 sản phẩm bán chạy", AutoSize = true };
        btnTop.Click += (_, _) => LoadTopProducts();
        topPanel.Controls.Add(btnTop);
        layout.Controls.Add(topPanel, 0, 1);

        _gridTopSanPham.Dock = DockStyle.Fill;
        _gridTopSanPham.ReadOnly = true;
        _gridTopSanPham.AllowUserToAddRows = false;
        _gridTopSanPham.AutoGenerateColumns = true;
        layout.Controls.Add(_gridTopSanPham, 0, 2);
    }

    private void LoadCatalogs()
    {
        try
        {
            _cboNhanVien.DataSource = _banHangBus.GetEmployees();
            _cboNhanVien.DisplayMember = nameof(NhanVienDto.TenNV);
            var customers = new List<KhachHangLuaChon> { new(null, "Khách lẻ") };
            customers.AddRange(_banHangBus.GetCustomers().Select(customer => new KhachHangLuaChon(customer.MaKH, customer.ToString())));
            _cboKhachHang.DataSource = customers;
            _cboKhachHang.DisplayMember = nameof(KhachHangLuaChon.TenHienThi);
            LoadProducts(_banHangBus.GetProducts());
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Không thể tải dữ liệu. Kiểm tra SQL Server và connection string.\n{ex.Message}", "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void SearchProducts()
    {
        try
        {
            LoadProducts(_banHangBus.GetProducts(_txtTimKiem.Text));
        }
        catch (Exception ex)
        {
            ShowError(ex);
        }
    }

    private void LoadProducts(List<SanPhamDto> products)
    {
        _cboSanPham.DataSource = products;
    }

    private void AddToCart()
    {
        if (_cboSanPham.SelectedItem is not SanPhamDto product)
        {
            MessageBox.Show("Vui lòng chọn sản phẩm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var quantity = (int)_numSoLuong.Value;
        var existing = _gioHang.FirstOrDefault(item => item.MaSP == product.MaSP);
        if (quantity + (existing?.SoLuong ?? 0) > product.SoLuongTon)
        {
            MessageBox.Show($"Số lượng vượt tồn kho hiện tại ({product.SoLuongTon}).", "Không đủ hàng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (existing is null)
        {
            _gioHang.Add(new ChiTietHoaDonDto { MaSP = product.MaSP, TenSP = product.TenSP, DonGia = product.GiaBan, SoLuong = quantity });
        }
        else
        {
            var index = _gioHang.IndexOf(existing);
            _gioHang[index] = new ChiTietHoaDonDto
            {
                MaSP = existing.MaSP,
                TenSP = existing.TenSP,
                DonGia = existing.DonGia,
                SoLuong = existing.SoLuong + quantity
            };
        }
        UpdateTotal();
    }

    private void RemoveSelectedCartLine()
    {
        if (_gridGioHang.CurrentRow?.DataBoundItem is ChiTietHoaDonDto line)
            _gioHang.Remove(line);
        UpdateTotal();
    }

    private void Checkout()
    {
        if (_cboNhanVien.SelectedItem is not NhanVienDto employee)
        {
            MessageBox.Show("Cần có nhân viên bán hàng trong cơ sở dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var customer = _cboKhachHang.SelectedItem as KhachHangLuaChon;
        var invoice = new HoaDonDto
        {
            MaNV = employee.MaNV,
            MaKH = customer?.MaKH,
            ChiTiet = _gioHang.Select(item => new ChiTietHoaDonDto
            {
                MaSP = item.MaSP,
                TenSP = item.TenSP,
                SoLuong = item.SoLuong,
                DonGia = item.DonGia
            }).ToList()
        };

        try
        {
            var result = _banHangBus.ThanhToan(invoice);
            _gioHang.Clear();
            UpdateTotal();
            MessageBox.Show($"Thanh toán thành công.\nMã hóa đơn: {result.MaHD}\nTổng tiền: {result.TongTien:N0} đ\nĐiểm cộng: {result.DiemCongThem}", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadProducts(_banHangBus.GetProducts(_txtTimKiem.Text));
        }
        catch (Exception ex)
        {
            ShowError(ex);
        }
    }

    private void ShowDailyRevenue()
    {
        try
        {
            var revenue = _banHangBus.GetDailyRevenue(_dateNgay.Value);
            _lblDoanhThu.Text = $"Doanh thu ngày: {revenue:N0} đ";
        }
        catch (Exception ex)
        {
            ShowError(ex);
        }
    }

    private void ShowMonthlyRevenue()
    {
        try
        {
            var revenue = _banHangBus.GetMonthlyRevenue(_dateThang.Value.Year, _dateThang.Value.Month);
            _lblDoanhThu.Text = $"Doanh thu tháng: {revenue:N0} đ";
        }
        catch (Exception ex)
        {
            ShowError(ex);
        }
    }

    private void LoadTopProducts()
    {
        try
        {
            _gridTopSanPham.DataSource = _banHangBus.GetTopSellingProducts();
        }
        catch (Exception ex)
        {
            ShowError(ex);
        }
    }

    private void ConfigureCartColumns()
    {
        SetColumnHeader(nameof(ChiTietHoaDonDto.MaSP), "Mã sản phẩm");
        SetColumnHeader(nameof(ChiTietHoaDonDto.TenSP), "Tên sản phẩm");
        SetColumnHeader(nameof(ChiTietHoaDonDto.SoLuong), "Số lượng");
        SetColumnHeader(nameof(ChiTietHoaDonDto.DonGia), "Đơn giá");
        SetColumnHeader(nameof(ChiTietHoaDonDto.ThanhTien), "Thành tiền");
    }

    private void SetColumnHeader(string property, string header)
    {
        if (_gridGioHang.Columns[property] is { } column)
            column.HeaderText = header;
    }

    private void UpdateTotal() => _lblTongTien.Text = $"Tổng tiền: {_gioHang.Sum(item => item.ThanhTien):N0} đ";

    private static void ShowError(Exception ex) =>
        MessageBox.Show(ex.Message, "Thao tác thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);

    private sealed record KhachHangLuaChon(int? MaKH, string TenHienThi);
}
