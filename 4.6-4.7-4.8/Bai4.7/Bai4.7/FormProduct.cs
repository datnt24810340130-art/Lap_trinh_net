namespace Bai4._7;

public partial class FormProduct : Form
{
    private readonly DataGridView productGrid = new();

    public FormProduct()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        Text = "Quản lý Sản phẩm";
        Width = 800;
        Height = 450;
        StartPosition = FormStartPosition.CenterParent;

        productGrid.Dock = DockStyle.Fill;
        productGrid.AllowUserToAddRows = false;
        productGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        productGrid.ReadOnly = true;
        productGrid.Columns.Add("Code", "Mã sản phẩm");
        productGrid.Columns.Add("Name", "Tên sản phẩm");
        productGrid.Columns.Add("Category", "Danh mục");
        productGrid.Columns.Add("Price", "Đơn giá");
        productGrid.Rows.Add("SP001", "Laptop văn phòng", "Thiết bị", "15.000.000");
        productGrid.Rows.Add("SP002", "Bàn phím cơ", "Phụ kiện", "850.000");
        Controls.Add(productGrid);
    }
}
