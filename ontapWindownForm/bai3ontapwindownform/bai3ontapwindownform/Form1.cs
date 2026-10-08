using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace bai3ontapwindownform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeCustom();
        }

        private void InitializeCustom()
        {
            // Populate unit combobox
            cboUnit.Items.Clear();
            cboUnit.Items.AddRange(new object[] { "Cái", "Bộ", "Kg", "Mét" });
            cboUnit.SelectedIndex = 0;

            // Configure ListView
            lvItems.View = View.Details;
            lvItems.FullRowSelect = true;
            lvItems.GridLines = true;
            lvItems.Columns.Clear();
            lvItems.Columns.Add("Mã VT", 100);
            lvItems.Columns.Add("Tên VT", 200);
            lvItems.Columns.Add("Đơn vị tính", 80);
            lvItems.Columns.Add("Đơn giá", 100, HorizontalAlignment.Right);

            // Wire events
            btnAdd.Click += BtnAdd_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDeleteRow.Click += BtnDeleteRow_Click;
            btnDeleteAll.Click += BtnDeleteAll_Click;
            lvItems.SelectedIndexChanged += LvItems_SelectedIndexChanged;
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            var code = txtCode.Text.Trim();
            var name = txtName.Text.Trim();
            var unit = cboUnit.SelectedItem?.ToString() ?? string.Empty;
            if (string.IsNullOrEmpty(code))
            {
                MessageBox.Show("Mã vật tư không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCode.Focus();
                return;
            }

            // Check duplicate code
            foreach (ListViewItem it in lvItems.Items)
            {
                if (string.Equals(it.Text, code, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Mã vật tư đã tồn tại trong danh sách.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCode.Focus();
                    return;
                }
            }

            if (!decimal.TryParse(txtPrice.Text.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out var price))
            {
                MessageBox.Show("Đơn giá không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrice.Focus();
                return;
            }

            var lvi = new ListViewItem(code);
            lvi.SubItems.Add(name);
            lvi.SubItems.Add(unit);
            lvi.SubItems.Add(price.ToString("N2"));
            lvItems.Items.Add(lvi);

            ClearInput();
        }

        private void BtnUpdate_Click(object? sender, EventArgs e)
        {
            if (lvItems.SelectedItems.Count == 0)
            {
                MessageBox.Show("Hãy chọn một dòng để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selected = lvItems.SelectedItems[0];
            var code = txtCode.Text.Trim();
            var name = txtName.Text.Trim();
            var unit = cboUnit.SelectedItem?.ToString() ?? string.Empty;
            if (string.IsNullOrEmpty(code))
            {
                MessageBox.Show("Mã vật tư không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCode.Focus();
                return;
            }

            // If code changed, ensure no duplicate among other items
            foreach (ListViewItem it in lvItems.Items)
            {
                if (it != selected && string.Equals(it.Text, code, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Mã vật tư đã tồn tại trong danh sách.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCode.Focus();
                    return;
                }
            }

            if (!decimal.TryParse(txtPrice.Text.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out var price))
            {
                MessageBox.Show("Đơn giá không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrice.Focus();
                return;
            }

            selected.Text = code;
            selected.SubItems[1].Text = name;
            selected.SubItems[2].Text = unit;
            selected.SubItems[3].Text = price.ToString("N0",CultureInfo.GetCultureInfo("vi-VN"));
            ClearInput();
        }

        private void BtnDeleteRow_Click(object? sender, EventArgs e)
        {
            if (lvItems.SelectedItems.Count == 0)
            {
                MessageBox.Show("Hãy chọn một dòng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show("Bạn có chắc muốn xóa dòng đã chọn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                lvItems.Items.Remove(lvItems.SelectedItems[0]);
                ClearInput();
            }
        }

        private void BtnDeleteAll_Click(object? sender, EventArgs e)
        {
            if (lvItems.Items.Count == 0)
                return;

            var result = MessageBox.Show("Bạn có chắc muốn xóa toàn bộ danh sách?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                lvItems.Items.Clear();
                ClearInput();
            }
        }

        private void LvItems_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (lvItems.SelectedItems.Count == 0)
                return;

            var sel = lvItems.SelectedItems[0];
            txtCode.Text = sel.Text;
            txtName.Text = sel.SubItems.Count > 1 ? sel.SubItems[1].Text : string.Empty;
            var unit = sel.SubItems.Count > 2 ? sel.SubItems[2].Text : string.Empty;
            cboUnit.SelectedItem = unit;
            txtPrice.Text = sel.SubItems.Count > 3 ? sel.SubItems[3].Text : string.Empty;
        }

        private void ClearInput()
        {
            txtCode.Clear();
            txtName.Clear();
            txtPrice.Clear();
            if (cboUnit.Items.Count > 0) cboUnit.SelectedIndex = 0;
            txtCode.Focus();
            lvItems.SelectedItems.Clear();
        }

        private void lblCode_Click(object sender, EventArgs e)
        {

        }

        private void groupBoxRight_Enter(object sender, EventArgs e)
        {

        }

        private void lblPrice_Click(object sender, EventArgs e)
        {

        }
    }
}
