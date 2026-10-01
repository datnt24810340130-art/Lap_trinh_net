using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace baithuchanh_windowfrom_gui
{
    public partial class Form1 : Form
    {
        private System.ComponentModel.BindingList<Product> products;
        private System.Windows.Forms.BindingSource bindingSource;

        public Form1()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
           
            var categories = new[] {
                new { Id = 1, Name = "Điện thoại" },
                new { Id = 2, Name = "Laptop" },
                new { Id = 3, Name = "Phụ kiện" }
            };
            cboCategory.DisplayMember = "Name";
            cboCategory.ValueMember = "Id";
            cboCategory.DataSource = categories;

            products = new System.ComponentModel.BindingList<Product>();
            bindingSource = new System.Windows.Forms.BindingSource();
            bindingSource.DataSource = products;

            dgvProducts.Columns.Clear();
            dgvProducts.AutoGenerateColumns = false;

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã SP", DataPropertyName = "ProductId" });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tên SP", DataPropertyName = "ProductName", Width = 200 });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Danh Mục", DataPropertyName = "CategoryName" });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Đơn Giá", DataPropertyName = "UnitPrice", DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Số Lượng", DataPropertyName = "Quantity" });

            dgvProducts.DataSource = bindingSource;

            UpdateStatus();
        }

        private void UpdateStatus()
        {
            toolStripStatusLabel.Text = $"Tổng số sản phẩm: {products.Count}";
        }

        private bool ValidateInputs()
        {
            errorProvider.Clear();
            bool ok = true;
            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                errorProvider.SetError(txtProductName, "Product name is required");
                ok = false;
            }
            if (!decimal.TryParse(txtUnitPrice.Text, out var price) || price <= 0)
            {
                errorProvider.SetError(txtUnitPrice, "Unit price must be a number > 0");
                ok = false;
            }
            if (!int.TryParse(txtQuantity.Text, out var qty) || qty < 0)
            {
                errorProvider.SetError(txtQuantity, "Quantity must be an integer >= 0");
                ok = false;
            }
            return ok;
        }

        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp|All files|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    picAvatar.Image?.Dispose();
                    picAvatar.Image = Image.FromFile(ofd.FileName);
                }
                catch
                {
                    MessageBox.Show("Cannot load image.");
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;
            var p = new Product
            {
                ProductId = txtProductId.Text.Trim(),
                ProductName = txtProductName.Text.Trim(),
                CategoryId = (int)cboCategory.SelectedValue,
                CategoryName = cboCategory.Text,
                UnitPrice = decimal.Parse(txtUnitPrice.Text),
                Quantity = int.Parse(txtQuantity.Text)
            };
       
            if (picAvatar.Image != null)
            {
                using var ms = new System.IO.MemoryStream();
                picAvatar.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                p.Avatar = ms.ToArray();
            }
            products.Add(p);
            bindingSource.ResetBindings(false);
            UpdateStatus();
        }

        private void dgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null) return;
            if (dgvProducts.CurrentRow.DataBoundItem is Product p)
            {
                txtProductId.Text = p.ProductId;
                txtProductName.Text = p.ProductName;
                txtUnitPrice.Text = p.UnitPrice.ToString();
                txtQuantity.Text = p.Quantity.ToString();
           
                cboCategory.SelectedIndex = cboCategory.FindStringExact(p.CategoryName);
                if (p.Avatar != null)
                {
                    using var ms = new System.IO.MemoryStream(p.Avatar);
                    picAvatar.Image = Image.FromStream(ms);
                }
                else
                {
                    picAvatar.Image = null;
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null) return;
            if (!ValidateInputs()) return;
            if (dgvProducts.CurrentRow.DataBoundItem is Product p)
            {
                p.ProductId = txtProductId.Text.Trim();
                p.ProductName = txtProductName.Text.Trim();
                p.CategoryId = (int)cboCategory.SelectedValue;
                p.CategoryName = cboCategory.Text;
                p.UnitPrice = decimal.Parse(txtUnitPrice.Text);
                p.Quantity = int.Parse(txtQuantity.Text);
                if (picAvatar.Image != null)
                {
                    using var ms = new System.IO.MemoryStream();
                    picAvatar.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    p.Avatar = ms.ToArray();
                }
                bindingSource.ResetBindings(false);
                UpdateStatus();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null) return;
            if (dgvProducts.CurrentRow.DataBoundItem is Product p)
            {
                var res = MessageBox.Show($"Delete product {p.ProductName}?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    products.Remove(p);
                    bindingSource.ResetBindings(false);
                    UpdateStatus();
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var q = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(q))
            {
                bindingSource.DataSource = products;
            }
            else
            {
                var filtered = products.Where(x => x.ProductName.Contains(q, StringComparison.OrdinalIgnoreCase)).ToList();
                bindingSource.DataSource = new System.ComponentModel.BindingList<Product>(filtered);
            }
            dgvProducts.Refresh();
        }

        private void exportCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var sfd = new SaveFileDialog();
            sfd.Filter = "CSV files|*.csv|All files|*.*";
            sfd.FileName = "products.csv";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using var sw = new System.IO.StreamWriter(sfd.FileName, false, System.Text.Encoding.UTF8);
                    sw.WriteLine("ProductId,ProductName,Category,UnitPrice,Quantity");
                    foreach (var p in products)
                    {
                        var safeName = p.ProductName?.Replace("\"", "\"\"") ?? string.Empty;
                        var line = $"{p.ProductId},\"{safeName}\",{p.CategoryName},{p.UnitPrice},{p.Quantity}";
                        sw.WriteLine(line);
                    }
                    MessageBox.Show("Exported successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Export failed: " + ex.Message);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvProducts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
            dgvProducts_SelectionChanged(sender, EventArgs.Empty);
        }
    }
}
