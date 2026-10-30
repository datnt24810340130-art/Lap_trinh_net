using System.Data;
using bai5._3.BUS;
using bai5._3.DTO;

namespace bai5._3.GUI;

public sealed class MenuManagementForm : Form
{
    private readonly CafeService _service = new();
    private readonly ListBox _categoryList = new();
    private readonly TextBox _categoryName = new();
    private readonly ComboBox _categoryCombo = new();
    private readonly TextBox _menuName = new();
    private readonly NumericUpDown _price = new();
    private readonly DataGridView _menuGrid = new();
    private int? _editingMenuItemId;
    private bool _hasChanges;

    public MenuManagementForm()
    {
        Text = "Quản lý danh mục và thực đơn";
        StartPosition = FormStartPosition.CenterParent;
        MinimumSize = new Size(900, 600);
        Size = new Size(1050, 700);
        BuildLayout();
        Load += (_, _) => RunUiAction(RefreshAll);
        FormClosing += (_, _) => { if (_hasChanges) DialogResult = DialogResult.OK; };
    }

    private void BuildLayout()
    {
        Font = new Font("Segoe UI", 10F);
        TableLayoutPanel root = new() { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1, Padding = new Padding(12) };
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250));
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        Controls.Add(root);

        GroupBox categories = new() { Text = "Danh mục", Dock = DockStyle.Fill, Padding = new Padding(8) };
        TableLayoutPanel categoryLayout = new() { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3 };
        categoryLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        categoryLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
        categoryLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 84));
        _categoryList.Dock = DockStyle.Fill;
        _categoryList.DisplayMember = nameof(MenuCategoryDto.Name);
        _categoryList.SelectedIndexChanged += (_, _) => RunUiAction(RefreshMenuGrid);
        categoryLayout.Controls.Add(_categoryList, 0, 0);
        _categoryName.Dock = DockStyle.Fill;
        _categoryName.PlaceholderText = "Tên danh mục";
        categoryLayout.Controls.Add(_categoryName, 0, 1);
        FlowLayoutPanel categoryButtons = new() { Dock = DockStyle.Fill, WrapContents = false };
        Button addCategory = new() { Text = "Thêm", AutoSize = true };
        addCategory.Click += (_, _) => RunUiAction(AddCategory);
        Button deleteCategory = new() { Text = "Xóa", AutoSize = true };
        deleteCategory.Click += (_, _) => RunUiAction(DeleteCategory);
        categoryButtons.Controls.Add(addCategory);
        categoryButtons.Controls.Add(deleteCategory);
        categoryLayout.Controls.Add(categoryButtons, 0, 2);
        categories.Controls.Add(categoryLayout);
        root.Controls.Add(categories, 0, 0);

        TableLayoutPanel right = new() { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, Padding = new Padding(12, 0, 0, 0) };
        right.RowStyles.Add(new RowStyle(SizeType.Absolute, 150));
        right.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        root.Controls.Add(right, 1, 0);

        GroupBox editor = new() { Text = "Thông tin món ăn", Dock = DockStyle.Fill, Padding = new Padding(8) };
        TableLayoutPanel editorLayout = new() { Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 3 };
        editorLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90));
        editorLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        editorLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90));
        editorLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        editorLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38));
        editorLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38));
        editorLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        editor.Controls.Add(editorLayout);
        _menuName.Dock = DockStyle.Fill;
        _categoryCombo.Dock = DockStyle.Fill;
        _categoryCombo.DropDownStyle = ComboBoxStyle.DropDownList;
        _categoryCombo.DisplayMember = nameof(MenuCategoryDto.Name);
        _categoryCombo.ValueMember = nameof(MenuCategoryDto.Id);
        _price.Dock = DockStyle.Left;
        _price.Minimum = 0;
        _price.Maximum = 1000000000;
        _price.DecimalPlaces = 2;
        _price.ThousandsSeparator = true;
        editorLayout.Controls.Add(MakeLabel("Tên món"), 0, 0);
        editorLayout.Controls.Add(_menuName, 1, 0);
        editorLayout.Controls.Add(MakeLabel("Danh mục"), 2, 0);
        editorLayout.Controls.Add(_categoryCombo, 3, 0);
        editorLayout.Controls.Add(MakeLabel("Đơn giá"), 0, 1);
        editorLayout.Controls.Add(_price, 1, 1);

        FlowLayoutPanel buttons = new() { Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight };
        Button saveButton = new() { Text = "Lưu món", AutoSize = true };
        saveButton.Click += (_, _) => RunUiAction(SaveMenuItem);
        Button deleteButton = new() { Text = "Xóa món", AutoSize = true };
        deleteButton.Click += (_, _) => RunUiAction(DeleteMenuItem);
        Button clearButton = new() { Text = "Mới / Làm trống", AutoSize = true };
        clearButton.Click += (_, _) => ClearEditor();
        buttons.Controls.Add(saveButton);
        buttons.Controls.Add(deleteButton);
        buttons.Controls.Add(clearButton);
        editorLayout.Controls.Add(buttons, 0, 2);
        editorLayout.SetColumnSpan(buttons, 4);
        right.Controls.Add(editor, 0, 0);

        GroupBox menuItems = new() { Text = "Thực đơn", Dock = DockStyle.Fill, Padding = new Padding(8) };
        _menuGrid.Dock = DockStyle.Fill;
        _menuGrid.ReadOnly = true;
        _menuGrid.AllowUserToAddRows = false;
        _menuGrid.AllowUserToDeleteRows = false;
        _menuGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        _menuGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _menuGrid.RowHeadersVisible = false;
        _menuGrid.SelectionChanged += (_, _) => LoadSelectedMenuItem();
        menuItems.Controls.Add(_menuGrid);
        right.Controls.Add(menuItems, 0, 1);
    }

    private static Label MakeLabel(string text) => new() { Text = text, AutoSize = true, Anchor = AnchorStyles.Left, TextAlign = ContentAlignment.MiddleLeft };

    private void RefreshAll()
    {
        List<MenuCategoryDto> categories = _service.GetCategories();
        int? selectedCategoryId = (_categoryList.SelectedItem as MenuCategoryDto)?.Id;
        _categoryList.DataSource = null;
        _categoryList.DataSource = categories;
        if (selectedCategoryId.HasValue)
        {
            _categoryList.SelectedItem = categories.FirstOrDefault(category => category.Id == selectedCategoryId.Value);
        }

        _categoryCombo.DataSource = null;
        _categoryCombo.DataSource = categories;
        _categoryCombo.DisplayMember = nameof(MenuCategoryDto.Name);
        _categoryCombo.ValueMember = nameof(MenuCategoryDto.Id);
        RefreshMenuGrid();
    }

    private void RefreshMenuGrid()
    {
        int? categoryId = (_categoryList.SelectedItem as MenuCategoryDto)?.Id;
        _menuGrid.DataSource = _service.GetMenuItems(categoryId);
        if (_menuGrid.Columns.Contains("MaMon"))
        {
            _menuGrid.Columns["MaMon"].Visible = false;
            _menuGrid.Columns["MaDanhMuc"].Visible = false;
            _menuGrid.Columns["TenMon"].HeaderText = "Tên món";
            _menuGrid.Columns["TenDanhMuc"].HeaderText = "Danh mục";
            _menuGrid.Columns["DonGia"].HeaderText = "Đơn giá";
            _menuGrid.Columns["DonGia"].DefaultCellStyle.Format = "N0";
        }
    }

    private void AddCategory()
    {
        _service.AddCategory(_categoryName.Text);
        _categoryName.Clear();
        _hasChanges = true;
        RefreshAll();
    }

    private void DeleteCategory()
    {
        if (_categoryList.SelectedItem is not MenuCategoryDto category)
        {
            throw new InvalidOperationException("Vui lòng chọn danh mục cần xóa.");
        }
        if (MessageBox.Show(this, $"Xóa danh mục '{category.Name}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        {
            return;
        }

        _service.DeleteCategory(category.Id);
        _hasChanges = true;
        ClearEditor();
        RefreshAll();
    }

    private void SaveMenuItem()
    {
        if (_categoryCombo.SelectedValue is not int categoryId)
        {
            throw new InvalidOperationException("Vui lòng tạo và chọn danh mục trước.");
        }

        _service.SaveMenuItem(_editingMenuItemId, _menuName.Text, categoryId, _price.Value);
        _hasChanges = true;
        RefreshAll();
        ClearEditor();
    }

    private void DeleteMenuItem()
    {
        if (!_editingMenuItemId.HasValue)
        {
            throw new InvalidOperationException("Vui lòng chọn món cần xóa.");
        }
        if (MessageBox.Show(this, "Xóa món ăn đã chọn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        {
            return;
        }

        _service.DeleteMenuItem(_editingMenuItemId.Value);
        _hasChanges = true;
        RefreshAll();
        ClearEditor();
    }

    private void LoadSelectedMenuItem()
    {
        if (_menuGrid.CurrentRow?.DataBoundItem is not DataRowView row)
        {
            return;
        }

        _editingMenuItemId = row.Row.Field<int>("MaMon");
        _menuName.Text = row.Row.Field<string>("TenMon");
        _categoryCombo.SelectedValue = row.Row.Field<int>("MaDanhMuc");
        _price.Value = row.Row.Field<decimal>("DonGia");
    }

    private void ClearEditor()
    {
        _editingMenuItemId = null;
        _menuName.Clear();
        _price.Value = 0;
        if (_categoryCombo.Items.Count > 0)
        {
            _categoryCombo.SelectedIndex = 0;
        }
        _menuGrid.ClearSelection();
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