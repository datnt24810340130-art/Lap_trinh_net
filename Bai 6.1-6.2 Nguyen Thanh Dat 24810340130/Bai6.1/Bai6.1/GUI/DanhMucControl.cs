using Bai6._1.BUS;
using Bai6._1.DTO;

namespace Bai6._1.GUI;

public sealed class DanhMucControl : UserControl
{
    private readonly SanPhamBus _sanPhamBus = new();
    private readonly KhachHangBus _khachHangBus = new();
    private readonly NhanVienBus _nhanVienBus = new();

    private readonly DataGridView _gridSanPham = CreateGrid();
    private readonly DataGridView _gridKhachHang = CreateGrid();
    private readonly DataGridView _gridNhanVien = CreateGrid();

    private readonly TextBox _maSP = new();
    private readonly TextBox _tenSP = new();
    private readonly TextBox _giaSP = new();
    private readonly TextBox _tonSP = new();
    private readonly TextBox _maKH = new();
    private readonly TextBox _tenKH = new();
    private readonly TextBox _dienThoaiKH = new();
    private readonly TextBox _diemKH = new();
    private readonly TextBox _maNV = new();
    private readonly TextBox _tenNV = new();
    private readonly TextBox _dienThoaiNV = new();
    private readonly TextBox _chucVuNV = new();

    public DanhMucControl()
    {
        Dock = DockStyle.Fill;
        var tabs = new TabControl { Dock = DockStyle.Fill };
        tabs.TabPages.Add(CreatePage("Sản phẩm", _gridSanPham, BuildProductEditor()));
        tabs.TabPages.Add(CreatePage("Khách hàng", _gridKhachHang, BuildCustomerEditor()));
        tabs.TabPages.Add(CreatePage("Nhân viên", _gridNhanVien, BuildEmployeeEditor()));
        Controls.Add(tabs);
        Load += (_, _) => ReloadAll();
    }

    private static TabPage CreatePage(string title, DataGridView grid, Control editor)
    {
        var page = new TabPage(title);
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, Padding = new Padding(8) };
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 115));
        layout.Controls.Add(grid, 0, 0);
        layout.Controls.Add(editor, 0, 1);
        page.Controls.Add(layout);
        return page;
    }

    private Control BuildProductEditor()
    {
        var panel = CreateEditorPanel();
        AddField(panel, "Mã SP", _maSP);
        AddField(panel, "Tên sản phẩm", _tenSP);
        AddField(panel, "Giá bán", _giaSP);
        AddField(panel, "Tồn kho", _tonSP);
        AddActions(panel, "Thêm", AddProduct, "Cập nhật", UpdateProduct, "Xóa", DeleteProduct, ClearProduct);
        _gridSanPham.SelectionChanged += (_, _) => SelectProduct();
        return panel;
    }

    private Control BuildCustomerEditor()
    {
        var panel = CreateEditorPanel();
        AddField(panel, "Mã KH", _maKH);
        AddField(panel, "Tên khách hàng", _tenKH);
        AddField(panel, "Điện thoại", _dienThoaiKH);
        AddField(panel, "Điểm", _diemKH);
        _maKH.ReadOnly = true;
        AddActions(panel, "Thêm", AddCustomer, "Cập nhật", UpdateCustomer, "Xóa", DeleteCustomer, ClearCustomer);
        _gridKhachHang.SelectionChanged += (_, _) => SelectCustomer();
        return panel;
    }

    private Control BuildEmployeeEditor()
    {
        var panel = CreateEditorPanel();
        AddField(panel, "Mã NV", _maNV);
        AddField(panel, "Tên nhân viên", _tenNV);
        AddField(panel, "Điện thoại", _dienThoaiNV);
        AddField(panel, "Chức vụ", _chucVuNV);
        _maNV.ReadOnly = true;
        AddActions(panel, "Thêm", AddEmployee, "Cập nhật", UpdateEmployee, "Xóa", DeleteEmployee, ClearEmployee);
        _gridNhanVien.SelectionChanged += (_, _) => SelectEmployee();
        return panel;
    }

    private static FlowLayoutPanel CreateEditorPanel() => new()
    {
        Dock = DockStyle.Fill,
        WrapContents = true,
        AutoScroll = true,
        Padding = new Padding(4)
    };

    private static void AddField(FlowLayoutPanel panel, string label, TextBox textBox)
    {
        textBox.Width = 145;
        var group = new FlowLayoutPanel { AutoSize = true, WrapContents = false, Margin = new Padding(4) };
        group.Controls.Add(new Label { Text = label, AutoSize = true, Margin = new Padding(3, 7, 3, 3) });
        group.Controls.Add(textBox);
        panel.Controls.Add(group);
    }

    private static void AddActions(FlowLayoutPanel panel, string addText, Action add, string updateText, Action update, string deleteText, Action delete, Action clear)
    {
        var actions = new FlowLayoutPanel { AutoSize = true, WrapContents = false, Margin = new Padding(4, 25, 4, 4) };
        actions.Controls.Add(CreateButton(addText, add));
        actions.Controls.Add(CreateButton(updateText, update));
        actions.Controls.Add(CreateButton(deleteText, delete));
        actions.Controls.Add(CreateButton("Làm mới", clear));
        panel.Controls.Add(actions);
    }

    private static Button CreateButton(string text, Action action)
    {
        var button = new Button { Text = text, AutoSize = true, Margin = new Padding(3) };
        button.Click += (_, _) => RunAction(action);
        return button;
    }

    private static DataGridView CreateGrid() => new()
    {
        Dock = DockStyle.Fill,
        AutoGenerateColumns = true,
        ReadOnly = true,
        AllowUserToAddRows = false,
        SelectionMode = DataGridViewSelectionMode.FullRowSelect,
        MultiSelect = false,
        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    };

    private void ReloadAll()
    {
        RunAction(() =>
        {
            _gridSanPham.DataSource = _sanPhamBus.GetAll();
            _gridKhachHang.DataSource = _khachHangBus.GetAll();
            _gridNhanVien.DataSource = _nhanVienBus.GetAll();
        });
    }

    private void AddProduct()
    {
        _sanPhamBus.Add(new SanPhamDto
        {
            MaSP = _maSP.Text,
            TenSP = _tenSP.Text,
            GiaBan = ReadDecimal(_giaSP, "Giá bán"),
            SoLuongTon = ReadInt(_tonSP, "Tồn kho")
        });
        _gridSanPham.DataSource = _sanPhamBus.GetAll();
        ClearProduct();
    }

    private void UpdateProduct()
    {
        _sanPhamBus.Update(new SanPhamDto
        {
            MaSP = _maSP.Text,
            TenSP = _tenSP.Text,
            GiaBan = ReadDecimal(_giaSP, "Giá bán"),
            SoLuongTon = ReadInt(_tonSP, "Tồn kho")
        });
        _gridSanPham.DataSource = _sanPhamBus.GetAll();
        ClearProduct();
    }

    private void DeleteProduct()
    {
        if (string.IsNullOrWhiteSpace(_maSP.Text))
            throw new InvalidOperationException("Chọn sản phẩm cần xóa.");
        ConfirmDelete(() => _sanPhamBus.Delete(_maSP.Text));
        _gridSanPham.DataSource = _sanPhamBus.GetAll();
        ClearProduct();
    }

    private void AddCustomer()
    {
        _khachHangBus.Add(new KhachHangDto
        {
            TenKH = _tenKH.Text,
            DienThoai = EmptyToNull(_dienThoaiKH.Text),
            DiemTichLuy = ReadInt(_diemKH, "Điểm")
        });
        _gridKhachHang.DataSource = _khachHangBus.GetAll();
        ClearCustomer();
    }

    private void UpdateCustomer()
    {
        _khachHangBus.Update(new KhachHangDto
        {
            MaKH = ReadInt(_maKH, "Mã khách hàng"),
            TenKH = _tenKH.Text,
            DienThoai = EmptyToNull(_dienThoaiKH.Text),
            DiemTichLuy = ReadInt(_diemKH, "Điểm")
        });
        _gridKhachHang.DataSource = _khachHangBus.GetAll();
        ClearCustomer();
    }

    private void DeleteCustomer()
    {
        ConfirmDelete(() => _khachHangBus.Delete(ReadInt(_maKH, "Mã khách hàng")));
        _gridKhachHang.DataSource = _khachHangBus.GetAll();
        ClearCustomer();
    }

    private void AddEmployee()
    {
        _nhanVienBus.Add(new NhanVienDto
        {
            TenNV = _tenNV.Text,
            SoDienThoai = EmptyToNull(_dienThoaiNV.Text),
            ChucVu = EmptyToNull(_chucVuNV.Text)
        });
        _gridNhanVien.DataSource = _nhanVienBus.GetAll();
        ClearEmployee();
    }

    private void UpdateEmployee()
    {
        _nhanVienBus.Update(new NhanVienDto
        {
            MaNV = ReadInt(_maNV, "Mã nhân viên"),
            TenNV = _tenNV.Text,
            SoDienThoai = EmptyToNull(_dienThoaiNV.Text),
            ChucVu = EmptyToNull(_chucVuNV.Text)
        });
        _gridNhanVien.DataSource = _nhanVienBus.GetAll();
        ClearEmployee();
    }

    private void DeleteEmployee()
    {
        ConfirmDelete(() => _nhanVienBus.Delete(ReadInt(_maNV, "Mã nhân viên")));
        _gridNhanVien.DataSource = _nhanVienBus.GetAll();
        ClearEmployee();
    }

    private void SelectProduct()
    {
        if (_gridSanPham.CurrentRow?.DataBoundItem is not SanPhamDto product)
            return;
        _maSP.Text = product.MaSP;
        _tenSP.Text = product.TenSP;
        _giaSP.Text = product.GiaBan.ToString();
        _tonSP.Text = product.SoLuongTon.ToString();
    }

    private void SelectCustomer()
    {
        if (_gridKhachHang.CurrentRow?.DataBoundItem is not KhachHangDto customer)
            return;
        _maKH.Text = customer.MaKH.ToString();
        _tenKH.Text = customer.TenKH;
        _dienThoaiKH.Text = customer.DienThoai ?? string.Empty;
        _diemKH.Text = customer.DiemTichLuy.ToString();
    }

    private void SelectEmployee()
    {
        if (_gridNhanVien.CurrentRow?.DataBoundItem is not NhanVienDto employee)
            return;
        _maNV.Text = employee.MaNV.ToString();
        _tenNV.Text = employee.TenNV;
        _dienThoaiNV.Text = employee.SoDienThoai ?? string.Empty;
        _chucVuNV.Text = employee.ChucVu ?? string.Empty;
    }

    private void ClearProduct()
    {
        _gridSanPham.ClearSelection();
        _maSP.Clear();
        _tenSP.Clear();
        _giaSP.Clear();
        _tonSP.Clear();
    }

    private void ClearCustomer()
    {
        _gridKhachHang.ClearSelection();
        _maKH.Clear();
        _tenKH.Clear();
        _dienThoaiKH.Clear();
        _diemKH.Text = "0";
    }

    private void ClearEmployee()
    {
        _gridNhanVien.ClearSelection();
        _maNV.Clear();
        _tenNV.Clear();
        _dienThoaiNV.Clear();
        _chucVuNV.Clear();
    }

    private static void ConfirmDelete(Action delete)
    {
        if (MessageBox.Show("Bạn có chắc muốn xóa bản ghi đã chọn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            delete();
    }

    private static int ReadInt(TextBox textBox, string fieldName) =>
        int.TryParse(textBox.Text, out var value) ? value : throw new ArgumentException($"{fieldName} phải là số nguyên hợp lệ.");

    private static decimal ReadDecimal(TextBox textBox, string fieldName) =>
        decimal.TryParse(textBox.Text, out var value) ? value : throw new ArgumentException($"{fieldName} phải là số hợp lệ.");

    private static string? EmptyToNull(string value) => string.IsNullOrWhiteSpace(value) ? null : value.Trim();

    private static void RunAction(Action action)
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Thao tác thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
