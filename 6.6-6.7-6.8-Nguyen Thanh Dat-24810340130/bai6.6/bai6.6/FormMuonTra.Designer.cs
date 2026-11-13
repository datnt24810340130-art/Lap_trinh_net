namespace bai6._6;

partial class FormMuonTra
{
    private System.ComponentModel.IContainer? components = null;
    private Label lblTitle = null!;
    private Label lblHoTen = null!;
    private Label lblTrangThai = null!;
    private Label lblMaDocGia = null!;
    private Label lblMaSach = null!;
    private Label lblSoLuong = null!;
    private Label lblHanTra = null!;
    private TextBox txtHoTen = null!;
    private TextBox txtMaDocGia = null!;
    private TextBox txtMaSach = null!;
    private ComboBox cboTrangThai = null!;
    private NumericUpDown nudSoLuong = null!;
    private DateTimePicker dtpHanTra = null!;
    private DataGridView dgvPhieuMuon = null!;
    private Button btnLoc = null!;
    private Button btnTaiLai = null!;
    private Button btnLapPhieu = null!;
    private Button btnTraSach = null!;
    private Button btnCanhBao = null!;

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
        lblTitle = new Label();
        lblHoTen = new Label();
        lblTrangThai = new Label();
        lblMaDocGia = new Label();
        lblMaSach = new Label();
        lblSoLuong = new Label();
        lblHanTra = new Label();
        txtHoTen = new TextBox();
        txtMaDocGia = new TextBox();
        txtMaSach = new TextBox();
        cboTrangThai = new ComboBox();
        nudSoLuong = new NumericUpDown();
        dtpHanTra = new DateTimePicker();
        dgvPhieuMuon = new DataGridView();
        btnLoc = new Button();
        btnTaiLai = new Button();
        btnLapPhieu = new Button();
        btnTraSach = new Button();
        btnCanhBao = new Button();
        ((System.ComponentModel.ISupportInitialize)nudSoLuong).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvPhieuMuon).BeginInit();
        SuspendLayout();
        // 
        // lblTitle
        // 
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
        lblTitle.Location = new Point(24, 18);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(438, 32);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "QUẢN LÝ MƯỢN - TRẢ SÁCH THƯ VIỆN";
        // 
        // lblHoTen
        // 
        lblHoTen.AutoSize = true;
        lblHoTen.Location = new Point(26, 76);
        lblHoTen.Name = "lblHoTen";
        lblHoTen.Size = new Size(104, 15);
        lblHoTen.TabIndex = 1;
        lblHoTen.Text = "Tên độc giả cần lọc";
        // 
        // txtHoTen
        // 
        txtHoTen.Location = new Point(29, 96);
        txtHoTen.Name = "txtHoTen";
        txtHoTen.Size = new Size(220, 23);
        txtHoTen.TabIndex = 2;
        // 
        // lblTrangThai
        // 
        lblTrangThai.AutoSize = true;
        lblTrangThai.Location = new Point(268, 76);
        lblTrangThai.Name = "lblTrangThai";
        lblTrangThai.Size = new Size(59, 15);
        lblTrangThai.TabIndex = 3;
        lblTrangThai.Text = "Trạng thái";
        // 
        // cboTrangThai
        // 
        cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
        cboTrangThai.FormattingEnabled = true;
        cboTrangThai.Location = new Point(271, 96);
        cboTrangThai.Name = "cboTrangThai";
        cboTrangThai.Size = new Size(150, 23);
        cboTrangThai.TabIndex = 4;
        cboTrangThai.SelectedIndexChanged += cboTrangThai_SelectedIndexChanged;
        // 
        // btnLoc
        // 
        btnLoc.Location = new Point(438, 94);
        btnLoc.Name = "btnLoc";
        btnLoc.Size = new Size(88, 27);
        btnLoc.TabIndex = 5;
        btnLoc.Text = "Lọc phiếu";
        btnLoc.UseVisualStyleBackColor = true;
        btnLoc.Click += btnLoc_Click;
        // 
        // btnTaiLai
        // 
        btnTaiLai.Location = new Point(532, 94);
        btnTaiLai.Name = "btnTaiLai";
        btnTaiLai.Size = new Size(88, 27);
        btnTaiLai.TabIndex = 6;
        btnTaiLai.Text = "Tải lại";
        btnTaiLai.UseVisualStyleBackColor = true;
        btnTaiLai.Click += btnTaiLai_Click;
        // 
        // dgvPhieuMuon
        // 
        dgvPhieuMuon.AllowUserToAddRows = false;
        dgvPhieuMuon.AllowUserToDeleteRows = false;
        dgvPhieuMuon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvPhieuMuon.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvPhieuMuon.Location = new Point(28, 137);
        dgvPhieuMuon.MultiSelect = false;
        dgvPhieuMuon.Name = "dgvPhieuMuon";
        dgvPhieuMuon.ReadOnly = true;
        dgvPhieuMuon.RowHeadersVisible = false;
        dgvPhieuMuon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvPhieuMuon.Size = new Size(945, 300);
        dgvPhieuMuon.TabIndex = 7;
        // 
        // lblMaDocGia
        // 
        lblMaDocGia.AutoSize = true;
        lblMaDocGia.Location = new Point(29, 461);
        lblMaDocGia.Name = "lblMaDocGia";
        lblMaDocGia.Size = new Size(67, 15);
        lblMaDocGia.TabIndex = 8;
        lblMaDocGia.Text = "Mã độc giả";
        // 
        // txtMaDocGia
        // 
        txtMaDocGia.Location = new Point(32, 481);
        txtMaDocGia.Name = "txtMaDocGia";
        txtMaDocGia.Size = new Size(110, 23);
        txtMaDocGia.TabIndex = 9;
        // 
        // lblMaSach
        // 
        lblMaSach.AutoSize = true;
        lblMaSach.Location = new Point(159, 461);
        lblMaSach.Name = "lblMaSach";
        lblMaSach.Size = new Size(48, 15);
        lblMaSach.TabIndex = 10;
        lblMaSach.Text = "Mã sách";
        // 
        // txtMaSach
        // 
        txtMaSach.Location = new Point(162, 481);
        txtMaSach.Name = "txtMaSach";
        txtMaSach.Size = new Size(100, 23);
        txtMaSach.TabIndex = 11;
        // 
        // lblSoLuong
        // 
        lblSoLuong.AutoSize = true;
        lblSoLuong.Location = new Point(279, 461);
        lblSoLuong.Name = "lblSoLuong";
        lblSoLuong.Size = new Size(52, 15);
        lblSoLuong.TabIndex = 12;
        lblSoLuong.Text = "Số lượng";
        // 
        // nudSoLuong
        // 
        nudSoLuong.Location = new Point(282, 481);
        nudSoLuong.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        nudSoLuong.Name = "nudSoLuong";
        nudSoLuong.Size = new Size(77, 23);
        nudSoLuong.TabIndex = 13;
        nudSoLuong.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // lblHanTra
        // 
        lblHanTra.AutoSize = true;
        lblHanTra.Location = new Point(377, 461);
        lblHanTra.Name = "lblHanTra";
        lblHanTra.Size = new Size(47, 15);
        lblHanTra.TabIndex = 14;
        lblHanTra.Text = "Hạn trả";
        // 
        // dtpHanTra
        // 
        dtpHanTra.Format = DateTimePickerFormat.Short;
        dtpHanTra.Location = new Point(380, 481);
        dtpHanTra.Name = "dtpHanTra";
        dtpHanTra.Size = new Size(112, 23);
        dtpHanTra.TabIndex = 15;
        dtpHanTra.Value = DateTime.Today.AddDays(14);
        // 
        // btnLapPhieu
        // 
        btnLapPhieu.Location = new Point(508, 478);
        btnLapPhieu.Name = "btnLapPhieu";
        btnLapPhieu.Size = new Size(112, 29);
        btnLapPhieu.TabIndex = 16;
        btnLapPhieu.Text = "Lập phiếu mượn";
        btnLapPhieu.UseVisualStyleBackColor = true;
        btnLapPhieu.Click += btnLapPhieu_Click;
        // 
        // btnTraSach
        // 
        btnTraSach.Location = new Point(28, 530);
        btnTraSach.Name = "btnTraSach";
        btnTraSach.Size = new Size(130, 34);
        btnTraSach.TabIndex = 17;
        btnTraSach.Text = "Trả phiếu đã chọn";
        btnTraSach.UseVisualStyleBackColor = true;
        btnTraSach.Click += btnTraSach_Click;
        // 
        // btnCanhBao
        // 
        btnCanhBao.Location = new Point(172, 530);
        btnCanhBao.Name = "btnCanhBao";
        btnCanhBao.Size = new Size(165, 34);
        btnCanhBao.TabIndex = 18;
        btnCanhBao.Text = "Cảnh báo quá hạn";
        btnCanhBao.UseVisualStyleBackColor = true;
        btnCanhBao.Click += btnCanhBao_Click;
        // 
        // FormMuonTra
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1000, 590);
        Controls.Add(btnCanhBao);
        Controls.Add(btnTraSach);
        Controls.Add(btnLapPhieu);
        Controls.Add(dtpHanTra);
        Controls.Add(lblHanTra);
        Controls.Add(nudSoLuong);
        Controls.Add(lblSoLuong);
        Controls.Add(txtMaSach);
        Controls.Add(lblMaSach);
        Controls.Add(txtMaDocGia);
        Controls.Add(lblMaDocGia);
        Controls.Add(dgvPhieuMuon);
        Controls.Add(btnTaiLai);
        Controls.Add(btnLoc);
        Controls.Add(cboTrangThai);
        Controls.Add(lblTrangThai);
        Controls.Add(txtHoTen);
        Controls.Add(lblHoTen);
        Controls.Add(lblTitle);
        MinimumSize = new Size(850, 500);
        Name = "FormMuonTra";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Quản lý mượn trả sách thư viện";
        Load += FormMuonTra_Load;
        ((System.ComponentModel.ISupportInitialize)nudSoLuong).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvPhieuMuon).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
