using System.ComponentModel;
using bai5._5.BUS;
using bai5._5.DTO;
using Microsoft.Data.SqlClient;

namespace bai5._5.GUI;

public sealed class MainForm : Form
{
    private readonly MaterialBus _materialBus;
    private readonly SupplierBus _supplierBus;
    private readonly InventoryBus _inventoryBus;
    private readonly DataGridView _materialGrid = CreateGrid();
    private readonly DataGridView _supplierGrid = CreateGrid();
    private readonly DataGridView _detailGrid = CreateGrid();
    private readonly DataGridView _lowStockGrid = CreateGrid();
    private readonly TextBox _materialSearch = new() { Width = 260 };
    private readonly TextBox _materialCode = new() { Width = 160 };
    private readonly TextBox _materialName = new() { Width = 220 };
    private readonly TextBox _materialUnit = new() { Width = 130 };
    private readonly NumericUpDown _minimumStock = CreateNumberInput();
    private readonly TextBox _supplierSearch = new() { Width = 260 };
    private readonly TextBox _supplierId = new() { Width = 100, ReadOnly = true };
    private readonly TextBox _supplierName = new() { Width = 220 };
    private readonly TextBox _supplierPhone = new() { Width = 160 };
    private readonly TextBox _supplierAddress = new() { Width = 220 };
    private readonly ComboBox _documentType = new() { DropDownStyle = ComboBoxStyle.DropDownList, Width = 130 };
    private readonly ComboBox _supplierPicker = new() { DropDownStyle = ComboBoxStyle.DropDownList, Width = 220 };
    private readonly ComboBox _materialPicker = new() { DropDownStyle = ComboBoxStyle.DropDownList, Width = 220, DisplayMember = nameof(MaterialDto.TenVatTu) };
    private readonly NumericUpDown _quantity = CreateNumberInput();
    private readonly NumericUpDown _unitPrice = CreateNumberInput();
    private readonly BindingList<InventoryDocumentLineDto> _documentLines = [];
    private readonly Label _inventoryValue = new() { AutoSize = true, Font = new Font("Segoe UI", 14, FontStyle.Bold) };

    public MainForm(MaterialBus materialBus, SupplierBus supplierBus, InventoryBus inventoryBus)
    {
        _materialBus = materialBus;
        _supplierBus = supplierBus;
        _inventoryBus = inventoryBus;

        Text = "Hệ thống quản lý nhập - xuất kho";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(1000, 680);
        Size = new Size(1280, 800);
        Font = new Font("Segoe UI", 9);

        BuildInterface();
        Shown += (_, _) => RunSafely(RefreshAll);
    }

    private void BuildInterface()
    {
        var tabs = new TabControl { Dock = DockStyle.Fill };
        tabs.TabPages.Add(BuildMaterialsPage());
        tabs.TabPages.Add(BuildSuppliersPage());
        tabs.TabPages.Add(BuildInventoryPage());
        tabs.TabPages.Add(BuildReportPage());
        Controls.Add(tabs);
    }

    private TabPage BuildMaterialsPage()
    {
        var page = new TabPage("Vật tư");
        var top = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 190,
            Padding = new Padding(10),
            AutoScroll = true,
            WrapContents = true
        };
        top.Controls.Add(CreateField("Tìm theo mã hoặc tên", _materialSearch));
        top.Controls.Add(CreateButton("Tìm kiếm", () => RunSafely(() => LoadMaterials(_materialSearch.Text))));
        top.Controls.Add(CreateButton("Làm mới", () => RunSafely(() => LoadMaterials())));
        top.Controls.Add(CreateField("Mã vật tư", _materialCode));
        top.Controls.Add(CreateField("Tên vật tư", _materialName));
        top.Controls.Add(CreateField("Đơn vị tính", _materialUnit));
        top.Controls.Add(CreateField("Định mức an toàn", _minimumStock));
        top.Controls.Add(CreateButton("Thêm", () => RunSafely(AddMaterial)));
        top.Controls.Add(CreateButton("Cập nhật", () => RunSafely(UpdateMaterial)));
        top.Controls.Add(CreateButton("Xóa", DeleteMaterial));
        top.Controls.Add(CreateButton("Xóa trắng", ClearMaterialFields));

        page.Controls.Add(_materialGrid);
        page.Controls.Add(top);
        _materialGrid.SelectionChanged += (_, _) =>
        {
            if (_materialGrid.CurrentRow?.DataBoundItem is not MaterialDto material) return;
            _materialCode.Text = material.MaVatTu;
            _materialCode.ReadOnly = true;
            _materialName.Text = material.TenVatTu;
            _materialUnit.Text = material.DVT;
            _minimumStock.Value = Math.Clamp(material.SoLuongToiThieu, _minimumStock.Minimum, _minimumStock.Maximum);
        };
        return page;
    }

    private TabPage BuildSuppliersPage()
    {
        var page = new TabPage("Nhà cung cấp");
        var top = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 190,
            Padding = new Padding(10),
            AutoScroll = true,
            WrapContents = true
        };
        top.Controls.Add(CreateField("Tìm tên hoặc điện thoại", _supplierSearch));
        top.Controls.Add(CreateButton("Tìm kiếm", () => RunSafely(() => LoadSuppliers(_supplierSearch.Text))));
        top.Controls.Add(CreateButton("Làm mới", () => RunSafely(() => LoadSuppliers())));
        top.Controls.Add(CreateField("Mã nhà cung cấp", _supplierId));
        top.Controls.Add(CreateField("Tên nhà cung cấp", _supplierName));
        top.Controls.Add(CreateField("Điện thoại", _supplierPhone));
        top.Controls.Add(CreateField("Địa chỉ", _supplierAddress));
        top.Controls.Add(CreateButton("Thêm", () => RunSafely(AddSupplier)));
        top.Controls.Add(CreateButton("Cập nhật", () => RunSafely(UpdateSupplier)));
        top.Controls.Add(CreateButton("Xóa", DeleteSupplier));
        top.Controls.Add(CreateButton("Xóa trắng", ClearSupplierFields));

        page.Controls.Add(_supplierGrid);
        page.Controls.Add(top);
        _supplierGrid.SelectionChanged += (_, _) =>
        {
            if (_supplierGrid.CurrentRow?.DataBoundItem is not SupplierDto supplier) return;
            _supplierId.Text = supplier.MaNCC.ToString();
            _supplierName.Text = supplier.TenNCC;
            _supplierPhone.Text = supplier.DienThoai;
            _supplierAddress.Text = supplier.DiaChi;
        };
        return page;
    }

    private TabPage BuildInventoryPage()
    {
        var page = new TabPage("Lập phiếu kho");
        _documentType.Items.AddRange(["NHAP", "XUAT"]);
        _documentType.SelectedIndex = 0;
        _documentType.SelectedIndexChanged += (_, _) => _supplierPicker.Enabled = _documentType.SelectedItem?.ToString() == "NHAP";

        var top = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 170,
            Padding = new Padding(10),
            AutoScroll = true,
            WrapContents = true
        };
        top.Controls.Add(CreateField("Loại phiếu", _documentType));
        top.Controls.Add(CreateField("Nhà cung cấp (phiếu nhập)", _supplierPicker));
        top.Controls.Add(CreateField("Vật tư", _materialPicker));
        top.Controls.Add(CreateField("Số lượng", _quantity));
        top.Controls.Add(CreateField("Đơn giá", _unitPrice));
        top.Controls.Add(CreateButton("Thêm vào phiếu", () => RunSafely(AddDocumentLine)));
        top.Controls.Add(CreateButton("Xóa dòng chọn", RemoveDocumentLine));

        _detailGrid.DataBindingComplete += (_, _) =>
        {
            _detailGrid.Columns[nameof(InventoryDocumentLineDto.MaVatTu)]!.HeaderText = "Mã vật tư";
            _detailGrid.Columns[nameof(InventoryDocumentLineDto.SoLuong)]!.HeaderText = "Số lượng";
            _detailGrid.Columns[nameof(InventoryDocumentLineDto.DonGia)]!.HeaderText = "Đơn giá";
        };
        _detailGrid.DataSource = _documentLines;

        var bottom = new FlowLayoutPanel
        {
            Dock = DockStyle.Bottom,
            Height = 60,
            Padding = new Padding(10),
            FlowDirection = FlowDirection.RightToLeft
        };
        bottom.Controls.Add(CreateButton("Lưu phiếu kho", () => RunSafely(SaveDocument), 150));

        page.Controls.Add(_detailGrid);
        page.Controls.Add(bottom);
        page.Controls.Add(top);
        return page;
    }

    private TabPage BuildReportPage()
    {
        var page = new TabPage("Báo cáo tồn kho");
        var top = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 65,
            Padding = new Padding(12),
            WrapContents = false
        };
        top.Controls.Add(CreateButton("Cập nhật báo cáo", () => RunSafely(LoadReport)));
        top.Controls.Add(new Label { Text = "Tổng giá trị tồn kho:", AutoSize = true, Padding = new Padding(10, 12, 0, 0) });
        top.Controls.Add(_inventoryValue);
        top.Controls.Add(new Label { Text = "Vật tư dưới định mức an toàn", AutoSize = true, Padding = new Padding(25, 12, 0, 0) });
        page.Controls.Add(_lowStockGrid);
        page.Controls.Add(top);
        return page;
    }

    private void LoadMaterials(string? keyword = null)
    {
        var materials = _materialBus.GetAll(keyword);
        _materialGrid.DataSource = materials;
        _materialPicker.DataSource = materials.ToList();
        _materialPicker.DisplayMember = nameof(MaterialDto.TenVatTu);
        _materialPicker.ValueMember = nameof(MaterialDto.MaVatTu);
    }

    private void LoadSuppliers(string? keyword = null)
    {
        var suppliers = _supplierBus.GetAll(keyword);
        _supplierGrid.DataSource = suppliers;
        _supplierPicker.DataSource = suppliers.ToList();
        _supplierPicker.DisplayMember = nameof(SupplierDto.TenNCC);
        _supplierPicker.ValueMember = nameof(SupplierDto.MaNCC);
    }

    private void LoadReport()
    {
        _inventoryValue.Text = $"{_materialBus.GetInventoryValue():N2} VNĐ";
        _lowStockGrid.DataSource = _materialBus.GetBelowMinimum();
    }

    private void RefreshAll()
    {
        LoadMaterials();
        LoadSuppliers();
        LoadReport();
    }

    private void AddMaterial()
    {
        _materialBus.Add(ReadMaterial());
        LoadMaterials();
        LoadReport();
        ClearMaterialFields();
        MessageBox.Show(this, "Đã thêm vật tư.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void UpdateMaterial()
    {
        _materialBus.Update(ReadMaterial());
        LoadMaterials(_materialSearch.Text);
        LoadReport();
        MessageBox.Show(this, "Đã cập nhật vật tư.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void DeleteMaterial()
    {
        if (string.IsNullOrWhiteSpace(_materialCode.Text))
        {
            ShowError("Chọn hoặc nhập mã vật tư cần xóa.");
            return;
        }
        if (MessageBox.Show(this, "Bạn có chắc muốn xóa vật tư này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            return;
        RunSafely(() =>
        {
            _materialBus.Delete(_materialCode.Text);
            LoadMaterials();
            LoadReport();
            ClearMaterialFields();
        });
    }

    private void AddSupplier()
    {
        var supplier = ReadSupplier();
        var id = _supplierBus.Add(supplier);
        LoadSuppliers();
        ClearSupplierFields();
        MessageBox.Show(this, $"Đã thêm nhà cung cấp, mã: {id}.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void UpdateSupplier()
    {
        if (!int.TryParse(_supplierId.Text, out var id))
            throw new ArgumentException("Chọn nhà cung cấp cần cập nhật.");
        var supplier = ReadSupplier();
        supplier.MaNCC = id;
        _supplierBus.Update(supplier);
        LoadSuppliers();
        MessageBox.Show(this, "Đã cập nhật nhà cung cấp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void DeleteSupplier()
    {
        if (!int.TryParse(_supplierId.Text, out var id))
        {
            ShowError("Chọn nhà cung cấp cần xóa.");
            return;
        }
        if (MessageBox.Show(this, "Bạn có chắc muốn xóa nhà cung cấp này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            return;
        RunSafely(() =>
        {
            _supplierBus.Delete(id);
            LoadSuppliers();
            ClearSupplierFields();
        });
    }

    private void AddDocumentLine()
    {
        if (_materialPicker.SelectedItem is not MaterialDto material)
            throw new ArgumentException("Hãy chọn vật tư.");
        if (_documentLines.Any(line => line.MaVatTu == material.MaVatTu))
            throw new ArgumentException("Vật tư này đã có trong phiếu.");

        _documentLines.Add(new InventoryDocumentLineDto
        {
            MaVatTu = material.MaVatTu,
            SoLuong = _quantity.Value,
            DonGia = _unitPrice.Value
        });
        _quantity.Value = 1;
        _unitPrice.Value = 0;
    }

    private void RemoveDocumentLine()
    {
        if (_detailGrid.CurrentRow?.DataBoundItem is InventoryDocumentLineDto line)
            _documentLines.Remove(line);
    }

    private void SaveDocument()
    {
        var type = _documentType.SelectedItem?.ToString() ?? "NHAP";
        var document = new InventoryDocumentDto { LoaiPhieu = type };
        if (type == "NHAP" && _supplierPicker.SelectedValue is int supplierId)
            document.MaNCC = supplierId;
        document.ChiTiet.AddRange(_documentLines.Select(line => new InventoryDocumentLineDto
        {
            MaVatTu = line.MaVatTu,
            SoLuong = line.SoLuong,
            DonGia = line.DonGia
        }));

        var documentId = _inventoryBus.Save(document);
        _documentLines.Clear();
        LoadMaterials();
        LoadReport();
        MessageBox.Show(this, $"Đã lưu phiếu {type}, mã phiếu: {documentId}.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private MaterialDto ReadMaterial() => new()
    {
        MaVatTu = _materialCode.Text,
        TenVatTu = _materialName.Text,
        DVT = _materialUnit.Text,
        SoLuongToiThieu = _minimumStock.Value
    };

    private SupplierDto ReadSupplier() => new()
    {
        TenNCC = _supplierName.Text,
        DienThoai = _supplierPhone.Text,
        DiaChi = _supplierAddress.Text
    };

    private void ClearMaterialFields()
    {
        _materialCode.Clear();
        _materialCode.ReadOnly = false;
        _materialName.Clear();
        _materialUnit.Clear();
        _minimumStock.Value = 0;
        _materialGrid.ClearSelection();
    }

    private void ClearSupplierFields()
    {
        _supplierId.Clear();
        _supplierName.Clear();
        _supplierPhone.Clear();
        _supplierAddress.Clear();
        _supplierGrid.ClearSelection();
    }

    private void RunSafely(Action action)
    {
        try
        {
            action();
        }
        catch (ArgumentException exception)
        {
            ShowError(exception.Message);
        }
        catch (InvalidOperationException exception)
        {
            ShowError(exception.Message);
        }
        catch (SqlException exception)
        {
            var message = exception.Number == 4060
                ? $"SQL Server đã nhận kết nối nhưng không mở được cơ sở dữ liệu InventoryDB cho tài khoản Windows đang chạy ứng dụng. Hãy chạy Database/ScriptDB.sql trên đúng instance SQL Server trong App.config; nếu InventoryDB đã tồn tại, hãy nhờ quản trị viên tạo user cho login Windows này trong InventoryDB và cấp quyền đọc/ghi cần thiết.\n\nChi tiết SQL Server: {exception.Message}"
                : $"Lỗi cơ sở dữ liệu: {exception.Message}";
            ShowError(message);
        }
        catch (FormatException exception)
        {
            ShowError($"Dữ liệu không đúng định dạng: {exception.Message}");
        }
        catch (OverflowException exception)
        {
            ShowError($"Giá trị nhập vượt giới hạn: {exception.Message}");
        }
    }

    private void ShowError(string message) =>
        MessageBox.Show(this, message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

    private static Control CreateField(string label, Control input)
    {
        var container = new Panel { Width = Math.Max(input.Width + 12, 170), Height = 62, Margin = new Padding(5) };
        container.Controls.Add(new Label { Text = label, AutoSize = true, Location = new Point(0, 0) });
        input.Location = new Point(0, 24);
        container.Controls.Add(input);
        return container;
    }

    private static Button CreateButton(string text, Action action, int width = 105)
    {
        var button = new Button { Text = text, Width = width, Height = 34, Margin = new Padding(5, 23, 5, 5) };
        button.Click += (_, _) => action();
        return button;
    }

    private static DataGridView CreateGrid() => new()
    {
        Dock = DockStyle.Fill,
        ReadOnly = true,
        AllowUserToAddRows = false,
        AllowUserToDeleteRows = false,
        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
        SelectionMode = DataGridViewSelectionMode.FullRowSelect,
        MultiSelect = false,
        RowHeadersVisible = false,
        BackgroundColor = SystemColors.Window
    };

    private static NumericUpDown CreateNumberInput() => new()
    {
        Width = 150,
        DecimalPlaces = 2,
        Minimum = 0,
        Maximum = 9999999999999999m,
        Increment = 0.1m,
        ThousandsSeparator = true
    };
}
