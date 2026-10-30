using System.Data;
using bai5._3.BUS;
using bai5._3.DTO;

namespace bai5._3.GUI;

public sealed class MainForm : Form
{
    private readonly CafeService _service = new();
    private readonly FlowLayoutPanel _tablePanel = new();
    private readonly ComboBox _categoryCombo = new();
    private readonly ComboBox _menuCombo = new();
    private readonly NumericUpDown _quantityInput = new();
    private readonly DataGridView _orderGrid = new();
    private readonly Label _selectedTableLabel = new();
    private readonly Label _totalLabel = new();
    private int? _selectedTableId;

    public MainForm()
    {
        Text = "Cafe POS - Quản lý quán cafe";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(1000, 650);
        Size = new Size(1200, 760);
        BuildLayout();
        Load += (_, _) => RunUiAction(RefreshAll);
    }

    private void BuildLayout()
    {
        Font = new Font("Segoe UI", 10F);
        TableLayoutPanel root = new() { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1, Padding = new Padding(12) };
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38));
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 62));
        Controls.Add(root);

        GroupBox tablesGroup = new() { Text = "Sơ đồ bàn", Dock = DockStyle.Fill, Padding = new Padding(10) };
        _tablePanel.Dock = DockStyle.Fill;
        _tablePanel.AutoScroll = true;
        _tablePanel.Padding = new Padding(4);
        tablesGroup.Controls.Add(_tablePanel);
        root.Controls.Add(tablesGroup, 0, 0);

        TableLayoutPanel right = new() { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3, Padding = new Padding(12, 0, 0, 0) };
        right.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
        right.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        right.RowStyles.Add(new RowStyle(SizeType.Absolute, 155));
        root.Controls.Add(right, 1, 0);

        Panel header = new() { Dock = DockStyle.Fill };
        _selectedTableLabel.Text = "Chưa chọn bàn";
        _selectedTableLabel.Font = new Font(Font, FontStyle.Bold);
        _selectedTableLabel.Dock = DockStyle.Left;
        _selectedTableLabel.TextAlign = ContentAlignment.MiddleLeft;
        Button menuManagementButton = new() { Text = "Quản lý thực đơn", AutoSize = true, Dock = DockStyle.Right };
        menuManagementButton.Click += (_, _) => OpenMenuManagement();
        header.Controls.Add(menuManagementButton);
        header.Controls.Add(_selectedTableLabel);
        right.Controls.Add(header, 0, 0);

        GroupBox orderGroup = new() { Text = "Hóa đơn tạm", Dock = DockStyle.Fill, Padding = new Padding(8) };
        ConfigureGrid(_orderGrid);
        orderGroup.Controls.Add(_orderGrid);
        right.Controls.Add(orderGroup, 0, 1);

        GroupBox orderControls = new() { Text = "Gọi món / Thanh toán", Dock = DockStyle.Fill, Padding = new Padding(10) };
        TableLayoutPanel controls = new() { Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 3 };
        controls.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 95));
        controls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        controls.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90));
        controls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        controls.RowStyles.Add(new RowStyle(SizeType.Absolute, 38));
        controls.RowStyles.Add(new RowStyle(SizeType.Absolute, 38));
        controls.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        orderControls.Controls.Add(controls);

        _categoryCombo.DropDownStyle = ComboBoxStyle.DropDownList;
        _categoryCombo.Dock = DockStyle.Fill;
        _categoryCombo.SelectedIndexChanged += CategoryChanged;
        _menuCombo.DropDownStyle = ComboBoxStyle.DropDownList;
        _menuCombo.Dock = DockStyle.Fill;
        _quantityInput.Minimum = 1;
        _quantityInput.Maximum = 1000;
        _quantityInput.Value = 1;
        _quantityInput.Dock = DockStyle.Left;
        controls.Controls.Add(MakeLabel("Danh mục"), 0, 0);
        controls.Controls.Add(_categoryCombo, 1, 0);
        controls.Controls.Add(MakeLabel("Món ăn"), 0, 1);
        controls.Controls.Add(_menuCombo, 1, 1);
        controls.Controls.Add(MakeLabel("Số lượng"), 2, 1);
        controls.Controls.Add(_quantityInput, 3, 1);

        Button addButton = new() { Text = "Thêm món", Width = 120, Height = 34 };
        addButton.Click += (_, _) => RunUiAction(AddOrderItem);
        Button moveButton = new() { Text = "Đổi bàn", Width = 100, Height = 34 };
        moveButton.Click += (_, _) => RunUiAction(MoveSelectedTable);
        _totalLabel.Text = "Tổng tiền: 0 đ";
        _totalLabel.Font = new Font(Font, FontStyle.Bold);
        _totalLabel.TextAlign = ContentAlignment.MiddleRight;
        Button payButton = new() { Text = "Thanh toán", Width = 130, Height = 34, BackColor = Color.LightGreen };
        payButton.Click += (_, _) => RunUiAction(PaySelectedTable);
        FlowLayoutPanel actions = new() { Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight, WrapContents = false };
        actions.Controls.Add(addButton);
        actions.Controls.Add(moveButton);
        actions.Controls.Add(_totalLabel);
        actions.Controls.Add(payButton);
        controls.Controls.Add(actions, 0, 2);
        controls.SetColumnSpan(actions, 4);
        right.Controls.Add(orderControls, 0, 2);
    }

    private static Label MakeLabel(string text) => new() { Text = text, AutoSize = true, Anchor = AnchorStyles.Left, TextAlign = ContentAlignment.MiddleLeft };

    private static void ConfigureGrid(DataGridView grid)
    {
        grid.Dock = DockStyle.Fill;
        grid.ReadOnly = true;
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.RowHeadersVisible = false;
    }

    private void RefreshAll()
    {
        List<DiningTableDto> tables = _service.GetTables();
        int? priorTableId = _selectedTableId;
        _tablePanel.SuspendLayout();
        _tablePanel.Controls.Clear();
        foreach (DiningTableDto table in tables)
        {
            Button button = new()
            {
                Text = table.Name + Environment.NewLine + table.Status,
                Tag = table.Id,
                Width = 125,
                Height = 82,
                Margin = new Padding(6),
                BackColor = table.Status == "Trống" ? Color.LightGreen : Color.LightCoral,
                FlatStyle = FlatStyle.Flat
            };
            button.Click += (_, _) => SelectTable(table);
            _tablePanel.Controls.Add(button);
        }
        _tablePanel.ResumeLayout();

        List<MenuCategoryDto> categories = _service.GetCategories();
        _categoryCombo.SelectedIndexChanged -= CategoryChanged;
        _categoryCombo.DataSource = categories;
        _categoryCombo.DisplayMember = nameof(MenuCategoryDto.Name);
        _categoryCombo.ValueMember = nameof(MenuCategoryDto.Id);
        _categoryCombo.SelectedIndexChanged += CategoryChanged;
        RefreshMenuItems();

        if (priorTableId.HasValue)
        {
            DiningTableDto? selected = tables.FirstOrDefault(table => table.Id == priorTableId.Value);
            if (selected is not null)
            {
                SelectTable(selected);
                return;
            }
        }
        _selectedTableId = null;
        _selectedTableLabel.Text = "Chưa chọn bàn";
        _orderGrid.DataSource = null;
        UpdateTotal();
    }

    private void CategoryChanged(object? sender, EventArgs e) => RunUiAction(RefreshMenuItems);

    private void RefreshMenuItems()
    {
        int? categoryId = _categoryCombo.SelectedValue is int value ? value : null;
        System.Data.DataTable menu = _service.GetMenuItems(categoryId);
        _menuCombo.DataSource = menu;
        _menuCombo.DisplayMember = "TenMon";
        _menuCombo.ValueMember = "MaMon";
    }

    private void SelectTable(DiningTableDto table)
    {
        _selectedTableId = table.Id;
        _selectedTableLabel.Text = $"{table.Name} - {table.Status}";
        _orderGrid.DataSource = _service.GetOrderItems(table.Id);
        if (_orderGrid.Columns.Contains("MaMon"))
        {
            _orderGrid.Columns["MaMon"].Visible = false;
            _orderGrid.Columns["TenMon"].HeaderText = "Tên món";
            _orderGrid.Columns["SoLuong"].HeaderText = "Số lượng";
            _orderGrid.Columns["DonGia"].HeaderText = "Đơn giá";
            _orderGrid.Columns["ThanhTien"].HeaderText = "Thành tiền";
            _orderGrid.Columns["DonGia"].DefaultCellStyle.Format = "N0";
            _orderGrid.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
        }
        UpdateTotal();
        foreach (Button button in _tablePanel.Controls.OfType<Button>())
        {
            bool selected = button.Tag is int id && id == table.Id;
            button.FlatAppearance.BorderSize = selected ? 3 : 1;
            button.FlatAppearance.BorderColor = selected ? Color.DodgerBlue : SystemColors.ControlDark;
        }
    }

    private void AddOrderItem()
    {
        if (!_selectedTableId.HasValue || _menuCombo.SelectedValue is not int menuItemId)
        {
            throw new InvalidOperationException("Vui lòng chọn bàn và món ăn trước khi thêm.");
        }

        _service.AddOrderItem(_selectedTableId.Value, menuItemId, Decimal.ToInt32(_quantityInput.Value));
        RefreshAll();
    }

    private void MoveSelectedTable()
    {
        if (!_selectedTableId.HasValue)
        {
            throw new InvalidOperationException("Vui lòng chọn bàn có hóa đơn cần đổi.");
        }

        List<DiningTableDto> availableTables = _service.GetTables()
            .Where(table => table.Id != _selectedTableId.Value && table.Status == "Trống")
            .ToList();
        if (availableTables.Count == 0)
        {
            throw new InvalidOperationException("Hiện không có bàn trống để chuyển hóa đơn.");
        }

        using Form dialog = new()
        {
            Text = "Chọn bàn đích",
            StartPosition = FormStartPosition.CenterParent,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            MinimizeBox = false,
            MaximizeBox = false,
            ClientSize = new Size(320, 125)
        };
        ComboBox targetCombo = new() { Left = 16, Top = 18, Width = 285, DropDownStyle = ComboBoxStyle.DropDownList };
        targetCombo.DataSource = availableTables;
        targetCombo.DisplayMember = nameof(DiningTableDto.Name);
        Button confirmButton = new() { Text = "Chuyển", Left = 116, Top = 70, Width = 85, DialogResult = DialogResult.OK };
        Button cancelButton = new() { Text = "Hủy", Left = 210, Top = 70, Width = 85, DialogResult = DialogResult.Cancel };
        dialog.Controls.Add(targetCombo);
        dialog.Controls.Add(confirmButton);
        dialog.Controls.Add(cancelButton);
        dialog.AcceptButton = confirmButton;
        dialog.CancelButton = cancelButton;

        if (dialog.ShowDialog(this) != DialogResult.OK || targetCombo.SelectedItem is not DiningTableDto targetTable)
        {
            return;
        }

        _service.MoveTable(_selectedTableId.Value, targetTable.Id);
        _selectedTableId = targetTable.Id;
        RefreshAll();
    }

    private void PaySelectedTable()
    {
        if (!_selectedTableId.HasValue)
        {
            throw new InvalidOperationException("Vui lòng chọn bàn cần thanh toán.");
        }

        decimal total = _service.PayTable(_selectedTableId.Value);
        MessageBox.Show(this, $"Thanh toán thành công: {total:N0} đ", "Thanh toán", MessageBoxButtons.OK, MessageBoxIcon.Information);
        _selectedTableId = null;
        RefreshAll();
    }

    private void UpdateTotal()
    {
        decimal total = 0;
        if (_orderGrid.DataSource is System.Data.DataTable table && table.Columns.Contains("ThanhTien"))
        {
            total = table.AsEnumerable().Sum(row => row.Field<decimal>("ThanhTien"));
        }
        _totalLabel.Text = $"Tổng tiền: {total:N0} đ";
    }

    private void OpenMenuManagement()
    {
        using MenuManagementForm form = new();
        if (form.ShowDialog(this) == DialogResult.OK)
        {
            RunUiAction(RefreshAll);
        }
    }

    private void RunUiAction(Action action)
    {
        try
        {
            action();
        }
        catch (Exception exception)
        {
            MessageBox.Show(this, exception.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}