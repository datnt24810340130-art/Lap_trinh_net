namespace bai6._7;

partial class FormQuanLyBenhNhan
{
    private System.ComponentModel.IContainer? components = null;
    private Label lblTieuDe = null!;
    private Label lblTimKiem = null!;
    private TextBox txtTimKiem = null!;
    private Button btnTimKiem = null!;
    private Button btnLamMoi = null!;
    private DataGridView dgvBenhNhan = null!;
    private GroupBox grpThongTin = null!;
    private Label lblMaBN = null!;
    private Label lblHoTen = null!;
    private TextBox txtHoTen = null!;
    private Label lblSoDienThoai = null!;
    private TextBox txtSoDienThoai = null!;
    private Label lblNgaySinh = null!;
    private DateTimePicker dtpNgaySinh = null!;
    private Label lblGioiTinh = null!;
    private ComboBox cboGioiTinh = null!;
    private Label lblDiaChi = null!;
    private TextBox txtDiaChi = null!;
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
        dgvBenhNhan = new DataGridView();
        grpThongTin = new GroupBox();
        lblMaBN = new Label();
        lblHoTen = new Label();
        txtHoTen = new TextBox();
        lblSoDienThoai = new Label();
        txtSoDienThoai = new TextBox();
        lblNgaySinh = new Label();
        dtpNgaySinh = new DateTimePicker();
        lblGioiTinh = new Label();
        cboGioiTinh = new ComboBox();
        lblDiaChi = new Label();
        txtDiaChi = new TextBox();
        btnThem = new Button();
        btnCapNhat = new Button();
        btnXoa = new Button();
        btnXoaTrang = new Button();
        ((System.ComponentModel.ISupportInitialize)dgvBenhNhan).BeginInit();
        grpThongTin.SuspendLayout();
        SuspendLayout();
        // 
        // lblTieuDe
        // 
        lblTieuDe.AutoSize = true;
        lblTieuDe.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblTieuDe.ForeColor = Color.FromArgb(21, 101, 92);
        lblTieuDe.Location = new Point(20, 16);
        lblTieuDe.Name = "lblTieuDe";
        lblTieuDe.Size = new Size(350, 32);
        lblTieuDe.Text = "QUẢN LÝ BỆNH NHÂN";
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
        txtTimKiem.PlaceholderText = "Nhập họ tên hoặc số điện thoại";
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
        // dgvBenhNhan
        // 
        dgvBenhNhan.AllowUserToAddRows = false;
        dgvBenhNhan.AllowUserToDeleteRows = false;
        dgvBenhNhan.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvBenhNhan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvBenhNhan.BackgroundColor = Color.White;
        dgvBenhNhan.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvBenhNhan.Location = new Point(22, 132);
        dgvBenhNhan.MultiSelect = false;
        dgvBenhNhan.Name = "dgvBenhNhan";
        dgvBenhNhan.ReadOnly = true;
        dgvBenhNhan.RowHeadersVisible = false;
        dgvBenhNhan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvBenhNhan.Size = new Size(953, 295);
        // 
        // grpThongTin
        // 
        grpThongTin.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        grpThongTin.Controls.Add(lblMaBN);
        grpThongTin.Controls.Add(lblHoTen);
        grpThongTin.Controls.Add(txtHoTen);
        grpThongTin.Controls.Add(lblSoDienThoai);
        grpThongTin.Controls.Add(txtSoDienThoai);
        grpThongTin.Controls.Add(lblNgaySinh);
        grpThongTin.Controls.Add(dtpNgaySinh);
        grpThongTin.Controls.Add(lblGioiTinh);
        grpThongTin.Controls.Add(cboGioiTinh);
        grpThongTin.Controls.Add(lblDiaChi);
        grpThongTin.Controls.Add(txtDiaChi);
        grpThongTin.Controls.Add(btnThem);
        grpThongTin.Controls.Add(btnCapNhat);
        grpThongTin.Controls.Add(btnXoa);
        grpThongTin.Controls.Add(btnXoaTrang);
        grpThongTin.Location = new Point(22, 441);
        grpThongTin.Name = "grpThongTin";
        grpThongTin.Size = new Size(953, 235);
        grpThongTin.TabStop = false;
        grpThongTin.Text = "Thông tin bệnh nhân";
        // 
        // lblMaBN
        // 
        lblMaBN.AutoSize = true;
        lblMaBN.ForeColor = Color.FromArgb(90, 100, 100);
        lblMaBN.Location = new Point(18, 25);
        lblMaBN.Name = "lblMaBN";
        lblMaBN.Size = new Size(105, 15);
        lblMaBN.Text = "Chưa chọn bệnh nhân";
        // 
        // lblHoTen
        // 
        lblHoTen.AutoSize = true;
        lblHoTen.Location = new Point(18, 54);
        lblHoTen.Name = "lblHoTen";
        lblHoTen.Size = new Size(43, 15);
        lblHoTen.Text = "Họ tên";
        // 
        // txtHoTen
        // 
        txtHoTen.Location = new Point(18, 74);
        txtHoTen.MaxLength = 150;
        txtHoTen.Name = "txtHoTen";
        txtHoTen.Size = new Size(360, 23);
        // 
        // lblSoDienThoai
        // 
        lblSoDienThoai.AutoSize = true;
        lblSoDienThoai.Location = new Point(405, 54);
        lblSoDienThoai.Name = "lblSoDienThoai";
        lblSoDienThoai.Size = new Size(79, 15);
        lblSoDienThoai.Text = "Số điện thoại";
        // 
        // txtSoDienThoai
        // 
        txtSoDienThoai.Location = new Point(405, 74);
        txtSoDienThoai.MaxLength = 20;
        txtSoDienThoai.Name = "txtSoDienThoai";
        txtSoDienThoai.Size = new Size(240, 23);
        // 
        // lblNgaySinh
        // 
        lblNgaySinh.AutoSize = true;
        lblNgaySinh.Location = new Point(18, 112);
        lblNgaySinh.Name = "lblNgaySinh";
        lblNgaySinh.Size = new Size(55, 15);
        lblNgaySinh.Text = "Ngày sinh";
        // 
        // dtpNgaySinh
        // 
        dtpNgaySinh.Format = DateTimePickerFormat.Short;
        dtpNgaySinh.Location = new Point(18, 132);
        dtpNgaySinh.Name = "dtpNgaySinh";
        dtpNgaySinh.ShowCheckBox = true;
        dtpNgaySinh.Size = new Size(200, 23);
        // 
        // lblGioiTinh
        // 
        lblGioiTinh.AutoSize = true;
        lblGioiTinh.Location = new Point(245, 112);
        lblGioiTinh.Name = "lblGioiTinh";
        lblGioiTinh.Size = new Size(49, 15);
        lblGioiTinh.Text = "Giới tính";
        // 
        // cboGioiTinh
        // 
        cboGioiTinh.DropDownStyle = ComboBoxStyle.DropDownList;
        cboGioiTinh.FormattingEnabled = true;
        cboGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });
        cboGioiTinh.Location = new Point(245, 132);
        cboGioiTinh.Name = "cboGioiTinh";
        cboGioiTinh.Size = new Size(160, 23);
        // 
        // lblDiaChi
        // 
        lblDiaChi.AutoSize = true;
        lblDiaChi.Location = new Point(670, 54);
        lblDiaChi.Name = "lblDiaChi";
        lblDiaChi.Size = new Size(42, 15);
        lblDiaChi.Text = "Địa chỉ";
        // 
        // txtDiaChi
        // 
        txtDiaChi.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtDiaChi.Location = new Point(670, 74);
        txtDiaChi.MaxLength = 300;
        txtDiaChi.Name = "txtDiaChi";
        txtDiaChi.Size = new Size(265, 23);
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
        // FormQuanLyBenhNhan
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(245, 248, 248);
        ClientSize = new Size(1000, 700);
        Controls.Add(grpThongTin);
        Controls.Add(dgvBenhNhan);
        Controls.Add(btnLamMoi);
        Controls.Add(btnTimKiem);
        Controls.Add(txtTimKiem);
        Controls.Add(lblTimKiem);
        Controls.Add(lblTieuDe);
        MinimumSize = new Size(850, 650);
        Name = "FormQuanLyBenhNhan";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Quản lý bệnh nhân";
        ((System.ComponentModel.ISupportInitialize)dgvBenhNhan).EndInit();
        grpThongTin.ResumeLayout(false);
        grpThongTin.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }
}
