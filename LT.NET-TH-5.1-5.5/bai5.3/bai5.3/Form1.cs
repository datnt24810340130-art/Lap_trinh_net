
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace bai5._3
{
    public partial class Form1 : Form
    {
        private List<Product> products;
        private BindingSource bindingSource;
        private int selectedIndex = -1;

        public Form1()
        {
            InitializeComponent();

            products = new List<Product>();
            bindingSource = new BindingSource();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            RefreshDataGridView();
        }

        // Cấu hình bảng sản phẩm
        private void SetupDataGridView()
        {
            dgvProducts.AutoGenerateColumns = false;
            dgvProducts.Columns.Clear();

            dgvProducts.Columns.Add("ProductId", "Mã SP");
            dgvProducts.Columns.Add("ProductName", "Tên SP");
            dgvProducts.Columns.Add("UnitPrice", "Đơn giá");
            dgvProducts.Columns.Add("Quantity", "Số lượng");
            dgvProducts.Columns.Add("Category", "Danh mục");

            dgvProducts.Columns["ProductId"].DataPropertyName = "ProductId";
            dgvProducts.Columns["ProductName"].DataPropertyName = "ProductName";
            dgvProducts.Columns["UnitPrice"].DataPropertyName = "UnitPrice";
            dgvProducts.Columns["Quantity"].DataPropertyName = "Quantity";
            dgvProducts.Columns["Category"].DataPropertyName = "Category";

            foreach (DataGridViewColumn column in dgvProducts.Columns)
            {
                column.Width = 140;
            }

            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.MultiSelect = false;
            dgvProducts.ReadOnly = true;
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
        }

        // Cập nhật dữ liệu lên bảng
        private void RefreshDataGridView()
        {
            bindingSource.DataSource = null;
            bindingSource.DataSource = products.ToList();
            dgvProducts.DataSource = bindingSource;
        }

        // Kiểm tra dữ liệu đầu vào
        private bool ValidateProductInput(
            out string productId,
            out string productName,
            out decimal unitPrice,
            out int quantity,
            out string category)
        {
            productId = txtProductId.Text.Trim();
            productName = txtProductName.Text.Trim();
            category = txtCategory.Text.Trim();
            unitPrice = 0;
            quantity = 0;

            if (string.IsNullOrWhiteSpace(productId) ||
                string.IsNullOrWhiteSpace(productName) ||
                string.IsNullOrWhiteSpace(txtUnitPrice.Text) ||
                string.IsNullOrWhiteSpace(txtQuantity.Text) ||
                string.IsNullOrWhiteSpace(category))
            {
                MessageBox.Show(
                    "Vui lòng điền đầy đủ thông tin sản phẩm!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(txtUnitPrice.Text.Trim(), out unitPrice)
                || unitPrice < 0)
            {
                MessageBox.Show(
                    "Đơn giá phải là số hợp lệ lớn hơn hoặc bằng 0!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtUnitPrice.Focus();
                return false;
            }

            if (!int.TryParse(txtQuantity.Text.Trim(), out quantity)
                || quantity < 0)
            {
                MessageBox.Show(
                    "Số lượng phải là số nguyên lớn hơn hoặc bằng 0!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtQuantity.Focus();
                return false;
            }

            // Danh mục là văn bản; chỉ từ chối khi toàn bộ nội dung
            // là một số âm, ví dụ -1 hoặc -2.5.
            if (decimal.TryParse(category, out decimal categoryNumber)
                && categoryNumber < 0)
            {
                MessageBox.Show(
                    "Danh mục không được là số âm!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtCategory.Focus();
                return false;
            }

            return true;
        }

        // Kiểm tra mã sản phẩm trùng
        private bool IsDuplicateProductId(string productId, int ignoreIndex = -1)
        {
            for (int i = 0; i < products.Count; i++)
            {
                if (i != ignoreIndex &&
                    string.Equals(
                        products[i].ProductId.Trim(),
                        productId.Trim(),
                        StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        // Thêm sản phẩm
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateProductInput(
                    out string productId,
                    out string productName,
                    out decimal unitPrice,
                    out int quantity,
                    out string category))
                {
                    return;
                }

                if (IsDuplicateProductId(productId))
                {
                    MessageBox.Show(
                        "Mã sản phẩm đã tồn tại! Vui lòng nhập mã khác.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    txtProductId.Focus();
                    return;
                }

                Product product = new Product
                {
                    ProductId = productId,
                    ProductName = productName,
                    UnitPrice = unitPrice,
                    Quantity = quantity,
                    Category = category
                };

                products.Add(product);

                RefreshDataGridView();
                ClearInputFields();

                MessageBox.Show(
                    "Đã thêm sản phẩm thành công!",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi");
            }
        }

        // Sửa sản phẩm
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedIndex < 0 || selectedIndex >= products.Count)
                {
                    MessageBox.Show(
                        "Vui lòng chọn sản phẩm cần sửa!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidateProductInput(
                    out string productId,
                    out string productName,
                    out decimal unitPrice,
                    out int quantity,
                    out string category))
                {
                    return;
                }

                if (IsDuplicateProductId(productId, selectedIndex))
                {
                    MessageBox.Show(
                        "Mã sản phẩm đã tồn tại ở sản phẩm khác!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    txtProductId.Focus();
                    return;
                }

                products[selectedIndex].ProductId = productId;
                products[selectedIndex].ProductName = productName;
                products[selectedIndex].UnitPrice = unitPrice;
                products[selectedIndex].Quantity = quantity;
                products[selectedIndex].Category = category;

                RefreshDataGridView();
                ClearInputFields();
                selectedIndex = -1;

                MessageBox.Show(
                    "Đã cập nhật sản phẩm thành công!",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi");
            }
        }

        // Xóa sản phẩm
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedIndex < 0 || selectedIndex >= products.Count)
                {
                    MessageBox.Show(
                        "Vui lòng chọn sản phẩm cần xóa!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show(
                    "Bạn có chắc muốn xóa sản phẩm '" +
                    products[selectedIndex].ProductName + "'?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    products.RemoveAt(selectedIndex);

                    RefreshDataGridView();
                    ClearInputFields();
                    selectedIndex = -1;

                    MessageBox.Show(
                        "Đã xóa sản phẩm thành công!",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi");
            }
        }

        // Tìm kiếm sản phẩm theo tên
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string searchName = txtSearchName.Text.Trim();

                if (string.IsNullOrWhiteSpace(searchName))
                {
                    RefreshDataGridView();
                    return;
                }

                List<Product> searchResults = products
                    .Where(p => p.ProductName != null &&
                        p.ProductName.Contains(
                            searchName,
                            StringComparison.OrdinalIgnoreCase))
                    .ToList();

                bindingSource.DataSource = null;
                bindingSource.DataSource = searchResults;
                dgvProducts.DataSource = bindingSource;

                if (searchResults.Count == 0)
                {
                    MessageBox.Show(
                        "Không tìm thấy sản phẩm nào!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi");
            }
        }

        // Chọn dòng để sửa hoặc xóa
        private void DgvProducts_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            Product selectedProduct =
                dgvProducts.Rows[e.RowIndex].DataBoundItem as Product;

            if (selectedProduct == null)
                return;

            selectedIndex = products.IndexOf(selectedProduct);

            txtProductId.Text = selectedProduct.ProductId;
            txtProductName.Text = selectedProduct.ProductName;
            txtUnitPrice.Text = selectedProduct.UnitPrice.ToString();
            txtQuantity.Text = selectedProduct.Quantity.ToString();
            txtCategory.Text = selectedProduct.Category;
        }

        // Xóa nội dung các ô nhập
        private void ClearInputFields()
        {
            txtProductId.Clear();
            txtProductName.Clear();
            txtUnitPrice.Clear();
            txtQuantity.Clear();
            txtCategory.Clear();

            selectedIndex = -1;
            dgvProducts.ClearSelection();
            txtProductId.Focus();
        }

        private void lblCategory_Click(object sender, EventArgs e)
        {
        }

        private void lblSearch_Click(object sender, EventArgs e)
        {
        }

        private void lblQuantity_Click(object sender, EventArgs e)
        {
        }

        private void lblProductName_Click(object sender, EventArgs e)
        {
        }
    }
}
