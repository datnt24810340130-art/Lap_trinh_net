namespace bai6._7;

partial class FormQuanLyThuoc
{
    private System.ComponentModel.IContainer? components = null;
    private Label lblTieuDe = null!;
    private Label lblTimKiem = null!;
    private TextBox txtTimKiem = null!;
    private Button btnTimKiem = null!;
    private Button btnLamMoi = null!;
    private DataGridView dgvThuoc = null!;
    private GroupBox grpThongTin = null!;
    private Label lblMaThuoc = null!;
    private Label lblTenThuoc = null!;
    private TextBox txtTenThuoc = null!;
    private Label lblDonViTinh = null!;
    private TextBox txtDonViTinh = null!;
    private Label lblDonGia = null!;
    private NumericUpDown nudDonGia = null!;
    private Label lblSoLuongTon = null!;
    private NumericUpDown nudSoLuongTon = null!;
    private Button btnThem = null!;
    private Button btnCapNhat = null!;
    private Button btnXoa = null!;
    private Button btnXoaTrang = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lblTieuDe = new Label();
        lblTimKiem = new Label();
        txtTimKiem = new TextBox();
        btnTimKiem = new Button();
        btnLamMoi = new Button();
        dgvThuoc = new DataGridView();
        grpThongTin = new GroupBox();
        lblMaThuoc = new Label();
        lblTenThuoc = new Label();
        txtTenThuoc = new TextBox();
        lblDonViTinh = new Label();
        txtDonViTinh = new TextBox();
        lblDonGia = new Label();
        nudDonGia = new NumericUpDown();
        lblSoLuongTon = new Label();
        nudSoLuongTon = new NumericUpDown();
        btnThem = new Button();
        btnCapNhat = new Button();
        btnXoa = new Button();
        btnXoaTrang = new Button();
        ((System.ComponentModel.ISupportInitialize)dgvThuoc).BeginInit();
        grpThongTin.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudDonGia).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudSoLuongTon).BeginInit();
        SuspendLayout();
        // 
        // lblTieuDe
        // 
        lblTieuDe.AutoSize = true;
        lblTieuDe.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblTieuDe.ForeColor = Color.FromArgb(21, 101, 92);
        lblTieuDe.Location = new Point(20, 16);
        lblTieuDe.Name = "lblTieuDe";
        lblTieuDe.Size = new Size(300, 32);
        lblTieuDe.Text = "QUẢN LÝ DANH MỤC THUỐC";
        // 
        // lblTimKiem
        // 
        lblTimKiem.AutoSize = true;
        lblTimKiem.Location = new Point(22, 68);
        lblTimKiem.Name = "lblTimKiem";
        lblTimKiem.Size = new Size(55, 15);
        lblTimKiem.Text = "Tìm kiếm";
        // 
        // txtTimKiem
        // 
        txtTimKiem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtTimKiem.Location = new Point(22, 88);
        txtTimKiem.MaxLength = 300;
        txtTimKiem.Name = "txtTimKiem";
        txtTimKiem.PlaceholderText = "Nhập tên thuốc hoặc đơn vị tính";
        txtTimKiem.Size = new Size(670, 23);
        // 
        // btnTimKiem
        // 
        btnTimKiem.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnTimKiem.BackColor = Color.FromArgb(230, 245, 242);
        btnTimKiem.FlatStyle = FlatStyle.Flat;
        btnTimKiem.Location = new Point(710, 86);
        btnTimKiem.Name = "btnTimKiem";
        btnTimKiem.Size = new Size(115, 28);
        btnTimKiem.Text = "Tìm kiếm";
        btnTimKiem.UseVisualStyleBackColor = false;
        // 
        // btnLamMoi
        // 
        btnLamMoi.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnLamMoi.FlatStyle = FlatStyle.Flat;
        btnLamMoi.Location = new Point(840, 86);
        btnLamMoi.Name = "btnLamMoi";
        btnLamMoi.Size = new Size(135, 28);
        btnLamMoi.Text = "Tải lại danh sách";
        // 
        // dgvThuoc
        // 
        dgvThuoc.AllowUserToAddRows = false;
        dgvThuoc.AllowUserToDeleteRows = false;
        dgvThuoc.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvThuoc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvThuoc.BackgroundColor = Color.White;
        dgvThuoc.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvThuoc.Location = new Point(22, 132);
        dgvThuoc.MultiSelect = false;
        dgvThuoc.Name = "dgvThuoc";
        dgvThuoc.ReadOnly = true;
        dgvThuoc.RowHeadersVisible = false;
        dgvThuoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvThuoc.Size = new Size(953, 295);
        // 
        // grpThongTin
        // 
        grpThongTin.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        grpThongTin.Controls.Add(lblMaThuoc);
        grpThongTin.Controls.Add(lblTenThuoc);
        grpThongTin.Controls.Add(txtTenThuoc);
        grpThongTin.Controls.Add(lblDonViTinh);
        grpThongTin.Controls.Add(txtDonViTinh);
        grpThongTin.Controls.Add(lblDonGia);
        grpThongTin.Controls.Add(nudDonGia);
        grpThongTin.Controls.Add(lblSoLuongTon);
        grpThongTin.Controls.Add(nudSoLuongTon);
        grpThongTin.Controls.Add(btnThem);
        grpThongTin.Controls.Add(btnCapNhat);
        grpThongTin.Controls.Add(btnXoa);
        grpThongTin.Controls.Add(btnXoaTrang);
        grpThongTin.Location = new Point(22, 441);
        grpThongTin.Name = "grpThongTin";
        grpThongTin.Size = new Size(953, 235);
        grpThongTin.TabStop = false;
        grpThongTin.Text = "Thông tin thuốc và tồn kho";
        // 
        // lblMaThuoc
        // 
        lblMaThuoc.AutoSize = true;
        lblMaThuoc.ForeColor = Color.FromArgb(90, 100, 100);
        lblMaThuoc.Location = new Point(18, 25);
        lblMaThuoc.Name = "lblMaThuoc";
        lblMaThuoc.Size = new Size(90, 15);
        lblMaThuoc.Text = "Chưa chọn thuốc";
        // 
        // lblTenThuoc
        // 
        lblTenThuoc.AutoSize = true;
        lblTenThuoc.Location = new Point(18, 54);
        lblTenThuoc.Name = "lblTenThuoc";
        lblTenThuoc.Size = new Size(57, 15);
        lblTenThuoc.Text = "Tên thuốc";
        // 
        // txtTenThuoc
        // 
        txtTenThuoc.Location = new Point(18, 74);
        txtTenThuoc.MaxLength = 150;
        txtTenThuoc.Name = "txtTenThuoc";
        txtTenThuoc.Size = new Size(330, 23);
        // 
        // lblDonViTinh
        // 
        lblDonViTinh.AutoSize = true;
        lblDonViTinh.Location = new Point(375, 54);
        lblDonViTinh.Name = "lblDonViTinh";
        lblDonViTinh.Size = new Size(65, 15);
        lblDonViTinh.Text = "Đơn vị tính";
        // 
        // txtDonViTinh
        // 
        txtDonViTinh.Location = new Point(375, 74);
        txtDonViTinh.MaxLength = 30;
        txtDonViTinh.Name = "txtDonViTinh";
        txtDonViTinh.Size = new Size(170, 23);
        // 
        // lblDonGia
        // 
        lblDonGia.AutoSize = true;
        lblDonGia.Location = new Point(572, 54);
        lblDonGia.Name = "lblDonGia";
        lblDonGia.Size = new Size(47, 15);
        lblDonGia.Text = "Đơn giá";
        // 
        // nudDonGia
        // 
        nudDonGia.DecimalPlaces = 2;
        nudDonGia.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
        nudDonGia.Location = new Point(572, 74);
        nudDonGia.Name = "nudDonGia";
        nudDonGia.Size = new Size(160, 23);
        nudDonGia.ThousandsSeparator = true;
        // 
        // lblSoLuongTon
        // 
        lblSoLuongTon.AutoSize = true;
        lblSoLuongTon.Location = new Point(760, 54);
        lblSoLuongTon.Name = "lblSoLuongTon";
        lblSoLuongTon.Size = new Size(66, 15);
        lblSoLuongTon.Text = "Tồn kho";
        // 
        // nudSoLuongTon
        // 
        nudSoLuongTon.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
        nudSoLuongTon.Location = new Point(760, 74);
        nudSoLuongTon.Name = "nudSoLuongTon";
        nudSoLuongTon.Size = new Size(175, 23);
        nudSoLuongTon.ThousandsSeparator = true;
        // 
        // btnThem
        // 
        btnThem.BackColor = Color.FromArgb(21, 101, 92);
        btnThem.FlatStyle = FlatStyle.Flat;
        btnThem.ForeColor = Color.White;
        btnThem.Location = new Point(450, 173);
        btnThem.Name = "btnThem";
        btnThem.Size = new Size(120, 35);
        btnThem.Text = "Thêm mới";
        btnThem.UseVisualStyleBackColor = false;
        // 
        // btnCapNhat
        // 
        btnCapNhat.BackColor = Color.FromArgb(230, 245, 242);
        btnCapNhat.FlatStyle = FlatStyle.Flat;
        btnCapNhat.Location = new Point(585, 173);
        btnCapNhat.Name = "btnCapNhat";
        btnCapNhat.Size = new Size(120, 35);
        btnCapNhat.Text = "Cập nhật";
        btnCapNhat.UseVisualStyleBackColor = false;
        // 
        // btnXoa
        // 
        btnXoa.BackColor = Color.FromArgb(255, 242, 242);
        btnXoa.FlatStyle = FlatStyle.Flat;
        btnXoa.ForeColor = Color.DarkRed;
        btnXoa.Location = new Point(720, 173);
        btnXoa.Name = "btnXoa";
        btnXoa.Size = new Size(100, 35);
        btnXoa.Text = "Xóa";
        btnXoa.UseVisualStyleBackColor = false;
        // 
        // btnXoaTrang
        // 
        btnXoaTrang.FlatStyle = FlatStyle.Flat;
        btnXoaTrang.Location = new Point(835, 173);
        btnXoaTrang.Name = "btnXoaTrang";
        btnXoaTrang.Size = new Size(100, 35);
        btnXoaTrang.Text = "Xóa trắng";
        // 
        // FormQuanLyThuoc
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(245, 248, 248);
        ClientSize = new Size(1000, 700);
        Controls.Add(grpThongTin);
        Controls.Add(dgvThuoc);
        Controls.Add(btnLamMoi);
        Controls.Add(btnTimKiem);
        Controls.Add(txtTimKiem);
        Controls.Add(lblTimKiem);
        Controls.Add(lblTieuDe);
        MinimumSize = new Size(850, 650);
        Name = "FormQuanLyThuoc";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Quản lý thuốc và tồn kho";
        ((System.ComponentModel.ISupportInitialize)dgvThuoc).EndInit();
        grpThongTin.ResumeLayout(false);
        grpThongTin.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudDonGia).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudSoLuongTon).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
