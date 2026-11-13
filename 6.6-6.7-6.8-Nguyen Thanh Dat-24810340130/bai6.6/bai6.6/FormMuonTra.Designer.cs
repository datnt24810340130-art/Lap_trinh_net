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
    private ComboBox cboMaSach = null!;
    private ComboBox cboTrangThai = null!;
    private NumericUpDown nudSoLuong = null!;
    private DateTimePicker dtpHanTra = null!;
    private DataGridView dgvPhieuMuon = null!;
    private Button btnLoc = null!;
    private Button btnTaiLai = null!;
    private Button btnLapPhieu = null!;
    private Button btnTraSach = null!;
    private Button btnCanhBao = null!;
    private Button btnQuanLyDocGia = null!;
    private Button btnQuanLySach = null!;

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
        cboMaSach = new ComboBox();
        cboTrangThai = new ComboBox();
        nudSoLuong = new NumericUpDown();
        dtpHanTra = new DateTimePicker();
        dgvPhieuMuon = new DataGridView();
        btnLoc = new Button();
        btnTaiLai = new Button();
        btnLapPhieu = new Button();
        btnTraSach = new Button();
        btnCanhBao = new Button();
        btnQuanLyDocGia = new Button();
        btnQuanLySach = new Button();
        ((System.ComponentModel.ISupportInitialize)nudSoLuong).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvPhieuMuon).BeginInit();
        SuspendLayout();
        // 
        // lblTitle
        // 
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblTitle.Location = new Point(27, 24);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(582, 41);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "QUẢN LÝ MƯỢN - TRẢ SÁCH THƯ VIỆN";
        // 
        // lblHoTen
        // 
        lblHoTen.AutoSize = true;
        lblHoTen.Location = new Point(30, 101);
        lblHoTen.Name = "lblHoTen";
        lblHoTen.Size = new Size(137, 20);
        lblHoTen.TabIndex = 1;
        lblHoTen.Text = "Tên độc giả cần lọc";
        // 
        // lblTrangThai
        // 
        lblTrangThai.AutoSize = true;
        lblTrangThai.Location = new Point(306, 101);
        lblTrangThai.Name = "lblTrangThai";
        lblTrangThai.Size = new Size(75, 20);
        lblTrangThai.TabIndex = 3;
        lblTrangThai.Text = "Trạng thái";
        // 
        // lblMaDocGia
        // 
        lblMaDocGia.AutoSize = true;
        lblMaDocGia.Location = new Point(33, 615);
        lblMaDocGia.Name = "lblMaDocGia";
        lblMaDocGia.Size = new Size(84, 20);
        lblMaDocGia.TabIndex = 8;
        lblMaDocGia.Text = "Mã độc giả";
        // 
        // lblMaSach
        // 
        lblMaSach.AutoSize = true;
        lblMaSach.Location = new Point(182, 615);
        lblMaSach.Name = "lblMaSach";
        lblMaSach.Size = new Size(63, 20);
        lblMaSach.TabIndex = 10;
        lblMaSach.Text = "Mã sách";
        // 
        // lblSoLuong
        // 
        lblSoLuong.AutoSize = true;
        lblSoLuong.Location = new Point(458, 615);
        lblSoLuong.Name = "lblSoLuong";
        lblSoLuong.Size = new Size(69, 20);
        lblSoLuong.TabIndex = 12;
        lblSoLuong.Text = "Số lượng";
        // 
        // lblHanTra
        // 
        lblHanTra.AutoSize = true;
        lblHanTra.Location = new Point(570, 615);
        lblHanTra.Name = "lblHanTra";
        lblHanTra.Size = new Size(58, 20);
        lblHanTra.TabIndex = 14;
        lblHanTra.Text = "Hạn trả";
        // 
        // txtHoTen
        // 
        txtHoTen.Location = new Point(33, 128);
        txtHoTen.Margin = new Padding(3, 4, 3, 4);
        txtHoTen.Name = "txtHoTen";
        txtHoTen.Size = new Size(251, 27);
        txtHoTen.TabIndex = 2;
        // 
        // txtMaDocGia
        // 
        txtMaDocGia.Location = new Point(37, 641);
        txtMaDocGia.Margin = new Padding(3, 4, 3, 4);
        txtMaDocGia.Name = "txtMaDocGia";
        txtMaDocGia.Size = new Size(125, 27);
        txtMaDocGia.TabIndex = 9;
        // 
        // cboMaSach
        // 
        cboMaSach.DropDownStyle = ComboBoxStyle.DropDownList;
        cboMaSach.FormattingEnabled = true;
        cboMaSach.Location = new Point(185, 641);
        cboMaSach.Margin = new Padding(3, 4, 3, 4);
        cboMaSach.Name = "cboMaSach";
        cboMaSach.Size = new Size(250, 28);
        cboMaSach.TabIndex = 11;
        cboMaSach.SelectedIndexChanged += cboMaSach_SelectedIndexChanged;
        // 
        // cboTrangThai
        // 
        cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
        cboTrangThai.FormattingEnabled = true;
        cboTrangThai.Location = new Point(310, 128);
        cboTrangThai.Margin = new Padding(3, 4, 3, 4);
        cboTrangThai.Name = "cboTrangThai";
        cboTrangThai.Size = new Size(171, 28);
        cboTrangThai.TabIndex = 4;
        cboTrangThai.SelectedIndexChanged += cboTrangThai_SelectedIndexChanged;
        // 
        // nudSoLuong
        // 
        nudSoLuong.Location = new Point(458, 641);
        nudSoLuong.Margin = new Padding(3, 4, 3, 4);
        nudSoLuong.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        nudSoLuong.Name = "nudSoLuong";
        nudSoLuong.Size = new Size(88, 27);
        nudSoLuong.TabIndex = 13;
        nudSoLuong.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // dtpHanTra
        // 
        dtpHanTra.Format = DateTimePickerFormat.Short;
        dtpHanTra.Location = new Point(570, 641);
        dtpHanTra.Margin = new Padding(3, 4, 3, 4);
        dtpHanTra.Name = "dtpHanTra";
        dtpHanTra.Size = new Size(127, 27);
        dtpHanTra.TabIndex = 15;
        // 
        // dgvPhieuMuon
        // 
        dgvPhieuMuon.AllowUserToAddRows = false;
        dgvPhieuMuon.AllowUserToDeleteRows = false;
        dgvPhieuMuon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvPhieuMuon.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvPhieuMuon.Location = new Point(32, 183);
        dgvPhieuMuon.Margin = new Padding(3, 4, 3, 4);
        dgvPhieuMuon.MultiSelect = false;
        dgvPhieuMuon.Name = "dgvPhieuMuon";
        dgvPhieuMuon.ReadOnly = true;
        dgvPhieuMuon.RowHeadersVisible = false;
        dgvPhieuMuon.RowHeadersWidth = 51;
        dgvPhieuMuon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvPhieuMuon.Size = new Size(1080, 400);
        dgvPhieuMuon.TabIndex = 7;
        // 
        // btnLoc
        // 
        btnLoc.Location = new Point(501, 125);
        btnLoc.Margin = new Padding(3, 4, 3, 4);
        btnLoc.Name = "btnLoc";
        btnLoc.Size = new Size(101, 36);
        btnLoc.TabIndex = 5;
        btnLoc.Text = "Lọc phiếu";
        btnLoc.UseVisualStyleBackColor = true;
        btnLoc.Click += btnLoc_Click;
        // 
        // btnTaiLai
        // 
        btnTaiLai.Location = new Point(608, 125);
        btnTaiLai.Margin = new Padding(3, 4, 3, 4);
        btnTaiLai.Name = "btnTaiLai";
        btnTaiLai.Size = new Size(101, 36);
        btnTaiLai.TabIndex = 6;
        btnTaiLai.Text = "Tải lại";
        btnTaiLai.UseVisualStyleBackColor = true;
        btnTaiLai.Click += btnTaiLai_Click;
        // 
        // btnLapPhieu
        // 
        btnLapPhieu.Location = new Point(717, 637);
        btnLapPhieu.Margin = new Padding(3, 4, 3, 4);
        btnLapPhieu.Name = "btnLapPhieu";
        btnLapPhieu.Size = new Size(128, 39);
        btnLapPhieu.TabIndex = 16;
        btnLapPhieu.Text = "Lập phiếu mượn";
        btnLapPhieu.UseVisualStyleBackColor = true;
        btnLapPhieu.Click += btnLapPhieu_Click;
        // 
        // btnTraSach
        // 
        btnTraSach.Location = new Point(32, 707);
        btnTraSach.Margin = new Padding(3, 4, 3, 4);
        btnTraSach.Name = "btnTraSach";
        btnTraSach.Size = new Size(149, 45);
        btnTraSach.TabIndex = 17;
        btnTraSach.Text = "Trả phiếu đã chọn";
        btnTraSach.UseVisualStyleBackColor = true;
        btnTraSach.Click += btnTraSach_Click;
        // 
        // btnCanhBao
        // 
        btnCanhBao.Location = new Point(221, 707);
        btnCanhBao.Margin = new Padding(3, 4, 3, 4);
        btnCanhBao.Name = "btnCanhBao";
        btnCanhBao.Size = new Size(189, 45);
        btnCanhBao.TabIndex = 18;
        btnCanhBao.Text = "Cảnh báo quá hạn";
        btnCanhBao.UseVisualStyleBackColor = true;
        btnCanhBao.Click += btnCanhBao_Click;
        // 
        // btnQuanLyDocGia
        // 
        btnQuanLyDocGia.Location = new Point(760, 31);
        btnQuanLyDocGia.Margin = new Padding(3, 4, 3, 4);
        btnQuanLyDocGia.Name = "btnQuanLyDocGia";
        btnQuanLyDocGia.Size = new Size(165, 40);
        btnQuanLyDocGia.TabIndex = 19;
        btnQuanLyDocGia.Text = "Quản lý độc giả";
        btnQuanLyDocGia.UseVisualStyleBackColor = true;
        btnQuanLyDocGia.Click += btnQuanLyDocGia_Click;
        // 
        // btnQuanLySach
        // 
        btnQuanLySach.Location = new Point(940, 31);
        btnQuanLySach.Margin = new Padding(3, 4, 3, 4);
        btnQuanLySach.Name = "btnQuanLySach";
        btnQuanLySach.Size = new Size(165, 40);
        btnQuanLySach.TabIndex = 20;
        btnQuanLySach.Text = "Quản lý đầu sách";
        btnQuanLySach.UseVisualStyleBackColor = true;
        btnQuanLySach.Click += btnQuanLySach_Click;
        // 
        // FormMuonTra
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1173, 787);
        Controls.Add(btnQuanLySach);
        Controls.Add(btnQuanLyDocGia);
        Controls.Add(btnCanhBao);
        Controls.Add(btnTraSach);
        Controls.Add(btnLapPhieu);
        Controls.Add(dtpHanTra);
        Controls.Add(lblHanTra);
        Controls.Add(nudSoLuong);
        Controls.Add(lblSoLuong);
        Controls.Add(cboMaSach);
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
        Margin = new Padding(3, 4, 3, 4);
        MinimumSize = new Size(969, 651);
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
