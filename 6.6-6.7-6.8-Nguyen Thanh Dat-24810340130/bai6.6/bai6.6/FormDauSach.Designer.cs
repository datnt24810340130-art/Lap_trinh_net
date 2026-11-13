namespace bai6._6;

partial class FormDauSach
{
    private System.ComponentModel.IContainer? components = null;
    private DataGridView dgvDauSach = null!;
    private Label lblTitle = null!;
    private Label lblTenSach = null!;
    private Label lblTacGia = null!;
    private Label lblTheLoai = null!;
    private Label lblNhaXuatBan = null!;
    private Label lblNamXuatBan = null!;
    private Label lblSoLuong = null!;
    private Label lblSoLuongCon = null!;
    private TextBox txtTenSach = null!;
    private TextBox txtTacGia = null!;
    private TextBox txtTheLoai = null!;
    private TextBox txtNhaXuatBan = null!;
    private NumericUpDown nudNamXuatBan = null!;
    private NumericUpDown nudSoLuong = null!;
    private NumericUpDown nudSoLuongCon = null!;
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
        dgvDauSach = new DataGridView();
        lblTitle = new Label();
        lblTenSach = new Label();
        lblTacGia = new Label();
        lblTheLoai = new Label();
        lblNhaXuatBan = new Label();
        lblNamXuatBan = new Label();
        lblSoLuong = new Label();
        lblSoLuongCon = new Label();
        txtTenSach = new TextBox();
        txtTacGia = new TextBox();
        txtTheLoai = new TextBox();
        txtNhaXuatBan = new TextBox();
        nudNamXuatBan = new NumericUpDown();
        nudSoLuong = new NumericUpDown();
        nudSoLuongCon = new NumericUpDown();
        btnThem = new Button();
        btnSua = new Button();
        btnXoa = new Button();
        btnLamMoi = new Button();
        ((System.ComponentModel.ISupportInitialize)dgvDauSach).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudNamXuatBan).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudSoLuong).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudSoLuongCon).BeginInit();
        SuspendLayout();
        // 
        // dgvDauSach
        // 
        dgvDauSach.AllowUserToAddRows = false;
        dgvDauSach.AllowUserToDeleteRows = false;
        dgvDauSach.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvDauSach.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvDauSach.Location = new Point(20, 58);
        dgvDauSach.MultiSelect = false;
        dgvDauSach.Name = "dgvDauSach";
        dgvDauSach.ReadOnly = true;
        dgvDauSach.RowHeadersVisible = false;
        dgvDauSach.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvDauSach.Size = new Size(1040, 300);
        dgvDauSach.TabIndex = 0;
        dgvDauSach.SelectionChanged += dgvDauSach_SelectionChanged;
        // 
        // lblTitle
        // 
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTitle.Location = new Point(20, 15);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(260, 30);
        lblTitle.TabIndex = 1;
        lblTitle.Text = "QUẢN LÝ ĐẦU SÁCH";
        // 
        // lblTenSach
        // 
        lblTenSach.AutoSize = true;
        lblTenSach.Location = new Point(22, 380);
        lblTenSach.Name = "lblTenSach";
        lblTenSach.Size = new Size(59, 15);
        lblTenSach.TabIndex = 2;
        lblTenSach.Text = "Tên sách*";
        // 
        // txtTenSach
        // 
        txtTenSach.Location = new Point(25, 400);
        txtTenSach.Name = "txtTenSach";
        txtTenSach.Size = new Size(300, 23);
        txtTenSach.TabIndex = 3;
        // 
        // lblTacGia
        // 
        lblTacGia.AutoSize = true;
        lblTacGia.Location = new Point(350, 380);
        lblTacGia.Name = "lblTacGia";
        lblTacGia.Size = new Size(43, 15);
        lblTacGia.TabIndex = 4;
        lblTacGia.Text = "Tác giả";
        // 
        // txtTacGia
        // 
        txtTacGia.Location = new Point(353, 400);
        txtTacGia.Name = "txtTacGia";
        txtTacGia.Size = new Size(220, 23);
        txtTacGia.TabIndex = 5;
        // 
        // lblTheLoai
        // 
        lblTheLoai.AutoSize = true;
        lblTheLoai.Location = new Point(600, 380);
        lblTheLoai.Name = "lblTheLoai";
        lblTheLoai.Size = new Size(47, 15);
        lblTheLoai.TabIndex = 6;
        lblTheLoai.Text = "Thể loại*";
        // 
        // txtTheLoai
        // 
        txtTheLoai.Location = new Point(603, 400);
        txtTheLoai.Name = "txtTheLoai";
        txtTheLoai.Size = new Size(200, 23);
        txtTheLoai.TabIndex = 7;
        // 
        // lblNhaXuatBan
        // 
        lblNhaXuatBan.AutoSize = true;
        lblNhaXuatBan.Location = new Point(830, 380);
        lblNhaXuatBan.Name = "lblNhaXuatBan";
        lblNhaXuatBan.Size = new Size(77, 15);
        lblNhaXuatBan.TabIndex = 8;
        lblNhaXuatBan.Text = "Nhà xuất bản";
        // 
        // txtNhaXuatBan
        // 
        txtNhaXuatBan.Location = new Point(833, 400);
        txtNhaXuatBan.Name = "txtNhaXuatBan";
        txtNhaXuatBan.Size = new Size(210, 23);
        txtNhaXuatBan.TabIndex = 9;
        // 
        // lblNamXuatBan
        // 
        lblNamXuatBan.AutoSize = true;
        lblNamXuatBan.Location = new Point(22, 440);
        lblNamXuatBan.Name = "lblNamXuatBan";
        lblNamXuatBan.Size = new Size(81, 15);
        lblNamXuatBan.TabIndex = 10;
        lblNamXuatBan.Text = "Năm xuất bản";
        // 
        // nudNamXuatBan
        // 
        nudNamXuatBan.Location = new Point(25, 460);
        nudNamXuatBan.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
        nudNamXuatBan.Name = "nudNamXuatBan";
        nudNamXuatBan.Size = new Size(120, 23);
        nudNamXuatBan.TabIndex = 11;
        nudNamXuatBan.Value = new decimal(new int[] { 2026, 0, 0, 0 });
        // 
        // lblSoLuong
        // 
        lblSoLuong.AutoSize = true;
        lblSoLuong.Location = new Point(180, 440);
        lblSoLuong.Name = "lblSoLuong";
        lblSoLuong.Size = new Size(101, 15);
        lblSoLuong.TabIndex = 12;
        lblSoLuong.Text = "Tổng số lượng*";
        // 
        // nudSoLuong
        // 
        nudSoLuong.Location = new Point(183, 460);
        nudSoLuong.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
        nudSoLuong.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        nudSoLuong.Name = "nudSoLuong";
        nudSoLuong.Size = new Size(120, 23);
        nudSoLuong.TabIndex = 13;
        nudSoLuong.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // lblSoLuongCon
        // 
        lblSoLuongCon.AutoSize = true;
        lblSoLuongCon.Location = new Point(340, 440);
        lblSoLuongCon.Name = "lblSoLuongCon";
        lblSoLuongCon.Size = new Size(83, 15);
        lblSoLuongCon.TabIndex = 14;
        lblSoLuongCon.Text = "Số lượng còn*";
        // 
        // nudSoLuongCon
        // 
        nudSoLuongCon.Location = new Point(343, 460);
        nudSoLuongCon.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
        nudSoLuongCon.Name = "nudSoLuongCon";
        nudSoLuongCon.Size = new Size(120, 23);
        nudSoLuongCon.TabIndex = 15;
        // 
        // btnThem
        // 
        btnThem.Location = new Point(25, 510);
        btnThem.Name = "btnThem";
        btnThem.Size = new Size(100, 32);
        btnThem.TabIndex = 16;
        btnThem.Text = "Thêm";
        btnThem.UseVisualStyleBackColor = true;
        btnThem.Click += btnThem_Click;
        // 
        // btnSua
        // 
        btnSua.Location = new Point(140, 510);
        btnSua.Name = "btnSua";
        btnSua.Size = new Size(100, 32);
        btnSua.TabIndex = 17;
        btnSua.Text = "Sửa";
        btnSua.UseVisualStyleBackColor = true;
        btnSua.Click += btnSua_Click;
        // 
        // btnXoa
        // 
        btnXoa.Location = new Point(255, 510);
        btnXoa.Name = "btnXoa";
        btnXoa.Size = new Size(100, 32);
        btnXoa.TabIndex = 18;
        btnXoa.Text = "Xóa";
        btnXoa.UseVisualStyleBackColor = true;
        btnXoa.Click += btnXoa_Click;
        // 
        // btnLamMoi
        // 
        btnLamMoi.Location = new Point(370, 510);
        btnLamMoi.Name = "btnLamMoi";
        btnLamMoi.Size = new Size(100, 32);
        btnLamMoi.TabIndex = 19;
        btnLamMoi.Text = "Làm mới";
        btnLamMoi.UseVisualStyleBackColor = true;
        btnLamMoi.Click += btnLamMoi_Click;
        // 
        // FormDauSach
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1080, 570);
        Controls.Add(btnLamMoi);
        Controls.Add(btnXoa);
        Controls.Add(btnSua);
        Controls.Add(btnThem);
        Controls.Add(nudSoLuongCon);
        Controls.Add(lblSoLuongCon);
        Controls.Add(nudSoLuong);
        Controls.Add(lblSoLuong);
        Controls.Add(nudNamXuatBan);
        Controls.Add(lblNamXuatBan);
        Controls.Add(txtNhaXuatBan);
        Controls.Add(lblNhaXuatBan);
        Controls.Add(txtTheLoai);
        Controls.Add(lblTheLoai);
        Controls.Add(txtTacGia);
        Controls.Add(lblTacGia);
        Controls.Add(txtTenSach);
        Controls.Add(lblTenSach);
        Controls.Add(lblTitle);
        Controls.Add(dgvDauSach);
        MinimumSize = new Size(950, 500);
        Name = "FormDauSach";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Quản lý đầu sách";
        Load += FormDauSach_Load;
        ((System.ComponentModel.ISupportInitialize)dgvDauSach).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudNamXuatBan).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudSoLuong).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudSoLuongCon).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
