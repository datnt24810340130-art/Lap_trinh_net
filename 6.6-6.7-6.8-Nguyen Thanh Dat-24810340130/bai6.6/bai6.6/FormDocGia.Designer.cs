namespace bai6._6;

partial class FormDocGia
{
    private System.ComponentModel.IContainer? components = null;
    private DataGridView dgvDocGia = null!;
    private Label lblTitle = null!;
    private Label lblHoTen = null!;
    private Label lblNgaySinh = null!;
    private Label lblGioiTinh = null!;
    private Label lblDiaChi = null!;
    private Label lblSoDienThoai = null!;
    private Label lblEmail = null!;
    private Label lblNgayHetHan = null!;
    private TextBox txtHoTen = null!;
    private TextBox txtDiaChi = null!;
    private TextBox txtSoDienThoai = null!;
    private TextBox txtEmail = null!;
    private DateTimePicker dtpNgaySinh = null!;
    private DateTimePicker dtpNgayHetHan = null!;
    private CheckBox chkCoNgaySinh = null!;
    private ComboBox cboGioiTinh = null!;
    private Button btnThem = null!;
    private Button btnSua = null!;
    private Button btnXoa = null!;
    private Button btnLamMoi = null!;

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
        dgvDocGia = new DataGridView();
        lblTitle = new Label();
        lblHoTen = new Label();
        lblNgaySinh = new Label();
        lblGioiTinh = new Label();
        lblDiaChi = new Label();
        lblSoDienThoai = new Label();
        lblEmail = new Label();
        lblNgayHetHan = new Label();
        txtHoTen = new TextBox();
        txtDiaChi = new TextBox();
        txtSoDienThoai = new TextBox();
        txtEmail = new TextBox();
        dtpNgaySinh = new DateTimePicker();
        dtpNgayHetHan = new DateTimePicker();
        chkCoNgaySinh = new CheckBox();
        cboGioiTinh = new ComboBox();
        btnThem = new Button();
        btnSua = new Button();
        btnXoa = new Button();
        btnLamMoi = new Button();
        ((System.ComponentModel.ISupportInitialize)dgvDocGia).BeginInit();
        SuspendLayout();
        // 
        // dgvDocGia
        // 
        dgvDocGia.AllowUserToAddRows = false;
        dgvDocGia.AllowUserToDeleteRows = false;
        dgvDocGia.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvDocGia.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvDocGia.Location = new Point(20, 58);
        dgvDocGia.MultiSelect = false;
        dgvDocGia.Name = "dgvDocGia";
        dgvDocGia.ReadOnly = true;
        dgvDocGia.RowHeadersVisible = false;
        dgvDocGia.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvDocGia.Size = new Size(1040, 300);
        dgvDocGia.TabIndex = 0;
        dgvDocGia.SelectionChanged += dgvDocGia_SelectionChanged;
        // 
        // lblTitle
        // 
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTitle.Location = new Point(20, 15);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(250, 30);
        lblTitle.TabIndex = 1;
        lblTitle.Text = "QUẢN LÝ ĐỘC GIẢ";
        // 
        // lblHoTen
        // 
        lblHoTen.AutoSize = true;
        lblHoTen.Location = new Point(22, 380);
        lblHoTen.Name = "lblHoTen";
        lblHoTen.Size = new Size(47, 15);
        lblHoTen.TabIndex = 2;
        lblHoTen.Text = "Họ tên*";
        // 
        // txtHoTen
        // 
        txtHoTen.Location = new Point(25, 400);
        txtHoTen.Name = "txtHoTen";
        txtHoTen.Size = new Size(300, 23);
        txtHoTen.TabIndex = 3;
        // 
        // lblNgaySinh
        // 
        lblNgaySinh.AutoSize = true;
        lblNgaySinh.Location = new Point(350, 380);
        lblNgaySinh.Name = "lblNgaySinh";
        lblNgaySinh.Size = new Size(56, 15);
        lblNgaySinh.TabIndex = 4;
        lblNgaySinh.Text = "Ngày sinh";
        // 
        // dtpNgaySinh
        // 
        dtpNgaySinh.Format = DateTimePickerFormat.Short;
        dtpNgaySinh.Location = new Point(353, 400);
        dtpNgaySinh.Name = "dtpNgaySinh";
        dtpNgaySinh.Size = new Size(130, 23);
        dtpNgaySinh.TabIndex = 5;
        // 
        // chkCoNgaySinh
        // 
        chkCoNgaySinh.AutoSize = true;
        chkCoNgaySinh.Location = new Point(490, 402);
        chkCoNgaySinh.Name = "chkCoNgaySinh";
        chkCoNgaySinh.Size = new Size(48, 19);
        chkCoNgaySinh.TabIndex = 6;
        chkCoNgaySinh.Text = "Có";
        chkCoNgaySinh.UseVisualStyleBackColor = true;
        // 
        // lblGioiTinh
        // 
        lblGioiTinh.AutoSize = true;
        lblGioiTinh.Location = new Point(570, 380);
        lblGioiTinh.Name = "lblGioiTinh";
        lblGioiTinh.Size = new Size(49, 15);
        lblGioiTinh.TabIndex = 7;
        lblGioiTinh.Text = "Giới tính";
        // 
        // cboGioiTinh
        // 
        cboGioiTinh.DropDownStyle = ComboBoxStyle.DropDownList;
        cboGioiTinh.FormattingEnabled = true;
        cboGioiTinh.Location = new Point(573, 400);
        cboGioiTinh.Name = "cboGioiTinh";
        cboGioiTinh.Size = new Size(140, 23);
        cboGioiTinh.TabIndex = 8;
        // 
        // lblDiaChi
        // 
        lblDiaChi.AutoSize = true;
        lblDiaChi.Location = new Point(22, 440);
        lblDiaChi.Name = "lblDiaChi";
        lblDiaChi.Size = new Size(41, 15);
        lblDiaChi.TabIndex = 9;
        lblDiaChi.Text = "Địa chỉ";
        // 
        // txtDiaChi
        // 
        txtDiaChi.Location = new Point(25, 460);
        txtDiaChi.Name = "txtDiaChi";
        txtDiaChi.Size = new Size(300, 23);
        txtDiaChi.TabIndex = 10;
        // 
        // lblSoDienThoai
        // 
        lblSoDienThoai.AutoSize = true;
        lblSoDienThoai.Location = new Point(350, 440);
        lblSoDienThoai.Name = "lblSoDienThoai";
        lblSoDienThoai.Size = new Size(70, 15);
        lblSoDienThoai.TabIndex = 11;
        lblSoDienThoai.Text = "Số điện thoại";
        // 
        // txtSoDienThoai
        // 
        txtSoDienThoai.Location = new Point(353, 460);
        txtSoDienThoai.Name = "txtSoDienThoai";
        txtSoDienThoai.Size = new Size(180, 23);
        txtSoDienThoai.TabIndex = 12;
        // 
        // lblEmail
        // 
        lblEmail.AutoSize = true;
        lblEmail.Location = new Point(570, 440);
        lblEmail.Name = "lblEmail";
        lblEmail.Size = new Size(35, 15);
        lblEmail.TabIndex = 13;
        lblEmail.Text = "Email";
        // 
        // txtEmail
        // 
        txtEmail.Location = new Point(573, 460);
        txtEmail.Name = "txtEmail";
        txtEmail.Size = new Size(250, 23);
        txtEmail.TabIndex = 14;
        // 
        // lblNgayHetHan
        // 
        lblNgayHetHan.AutoSize = true;
        lblNgayHetHan.Location = new Point(840, 380);
        lblNgayHetHan.Name = "lblNgayHetHan";
        lblNgayHetHan.Size = new Size(75, 15);
        lblNgayHetHan.TabIndex = 15;
        lblNgayHetHan.Text = "Hạn thẻ*";
        // 
        // dtpNgayHetHan
        // 
        dtpNgayHetHan.Format = DateTimePickerFormat.Short;
        dtpNgayHetHan.Location = new Point(843, 400);
        dtpNgayHetHan.Name = "dtpNgayHetHan";
        dtpNgayHetHan.Size = new Size(160, 23);
        dtpNgayHetHan.TabIndex = 16;
        dtpNgayHetHan.Value = DateTime.Today.AddYears(1);
        // 
        // btnThem
        // 
        btnThem.Location = new Point(25, 510);
        btnThem.Name = "btnThem";
        btnThem.Size = new Size(100, 32);
        btnThem.TabIndex = 17;
        btnThem.Text = "Thêm";
        btnThem.UseVisualStyleBackColor = true;
        btnThem.Click += btnThem_Click;
        // 
        // btnSua
        // 
        btnSua.Location = new Point(140, 510);
        btnSua.Name = "btnSua";
        btnSua.Size = new Size(100, 32);
        btnSua.TabIndex = 18;
        btnSua.Text = "Sửa";
        btnSua.UseVisualStyleBackColor = true;
        btnSua.Click += btnSua_Click;
        // 
        // btnXoa
        // 
        btnXoa.Location = new Point(255, 510);
        btnXoa.Name = "btnXoa";
        btnXoa.Size = new Size(100, 32);
        btnXoa.TabIndex = 19;
        btnXoa.Text = "Xóa";
        btnXoa.UseVisualStyleBackColor = true;
        btnXoa.Click += btnXoa_Click;
        // 
        // btnLamMoi
        // 
        btnLamMoi.Location = new Point(370, 510);
        btnLamMoi.Name = "btnLamMoi";
        btnLamMoi.Size = new Size(100, 32);
        btnLamMoi.TabIndex = 20;
        btnLamMoi.Text = "Làm mới";
        btnLamMoi.UseVisualStyleBackColor = true;
        btnLamMoi.Click += btnLamMoi_Click;
        // 
        // FormDocGia
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1080, 570);
        Controls.Add(btnLamMoi);
        Controls.Add(btnXoa);
        Controls.Add(btnSua);
        Controls.Add(btnThem);
        Controls.Add(dtpNgayHetHan);
        Controls.Add(lblNgayHetHan);
        Controls.Add(txtEmail);
        Controls.Add(lblEmail);
        Controls.Add(txtSoDienThoai);
        Controls.Add(lblSoDienThoai);
        Controls.Add(txtDiaChi);
        Controls.Add(lblDiaChi);
        Controls.Add(cboGioiTinh);
        Controls.Add(lblGioiTinh);
        Controls.Add(chkCoNgaySinh);
        Controls.Add(dtpNgaySinh);
        Controls.Add(lblNgaySinh);
        Controls.Add(txtHoTen);
        Controls.Add(lblHoTen);
        Controls.Add(lblTitle);
        Controls.Add(dgvDocGia);
        MinimumSize = new Size(950, 500);
        Name = "FormDocGia";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Quản lý độc giả";
        Load += FormDocGia_Load;
        ((System.ComponentModel.ISupportInitialize)dgvDocGia).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
