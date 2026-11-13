namespace bai6._8;

partial class FormMainBilliards
{
    private System.ComponentModel.IContainer? components = null;
    private SplitContainer splitContainerMain = null!;
    private Panel pnlDanhSachBan = null!;
    private Label lblDanhSachBan = null!;
    private FlowLayoutPanel flpDanhSachBan = null!;
    private Label lblChuThichTrong = null!;
    private Label lblChuThichDangChoi = null!;
    private Panel pnlThongTinBan = null!;
    private TableLayoutPanel layoutThongTin = null!;
    private Label lblTieuDe = null!;
    private Label lblTenBan = null!;
    private Label lblGioBatDau = null!;
    private Label lblGiaTheoGio = null!;
    private Label lblDichVu = null!;
    private ComboBox cboDichVu = null!;
    private NumericUpDown nudSoLuong = null!;
    private Button btnThemMon = null!;
    private DataGridView dgvChiTietHoaDon = null!;
    private Label lblTienGio = null!;
    private Label lblTienDichVu = null!;
    private Label lblTongTien = null!;
    private Button btnMoBan = null!;
    private Button btnThanhToan = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            components?.Dispose();
            timerCapNhat?.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        splitContainerMain = new SplitContainer();
        pnlDanhSachBan = new Panel();
        flpDanhSachBan = new FlowLayoutPanel();
        lblChuThichDangChoi = new Label();
        lblChuThichTrong = new Label();
        lblDanhSachBan = new Label();
        pnlThongTinBan = new Panel();
        layoutThongTin = new TableLayoutPanel();
        lblTieuDe = new Label();
        lblTenBan = new Label();
        lblGioBatDau = new Label();
        lblGiaTheoGio = new Label();
        lblDichVu = new Label();
        cboDichVu = new ComboBox();
        nudSoLuong = new NumericUpDown();
        btnThemMon = new Button();
        dgvChiTietHoaDon = new DataGridView();
        lblTienGio = new Label();
        lblTienDichVu = new Label();
        lblTongTien = new Label();
        btnMoBan = new Button();
        btnThanhToan = new Button();
        ((System.ComponentModel.ISupportInitialize)splitContainerMain).BeginInit();
        splitContainerMain.Panel1.SuspendLayout();
        splitContainerMain.Panel2.SuspendLayout();
        splitContainerMain.SuspendLayout();
        pnlDanhSachBan.SuspendLayout();
        pnlThongTinBan.SuspendLayout();
        layoutThongTin.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudSoLuong).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvChiTietHoaDon).BeginInit();
        SuspendLayout();
        // 
        // splitContainerMain
        // 
        splitContainerMain.Dock = DockStyle.Fill;
        splitContainerMain.FixedPanel = FixedPanel.Panel1;
        splitContainerMain.Location = new Point(0, 0);
        splitContainerMain.Margin = new Padding(3, 4, 3, 4);
        splitContainerMain.Name = "splitContainerMain";
        // 
        // splitContainerMain.Panel1
        // 
        splitContainerMain.Panel1.Controls.Add(pnlDanhSachBan);
        // 
        // splitContainerMain.Panel2
        // 
        splitContainerMain.Panel2.Controls.Add(pnlThongTinBan);
        splitContainerMain.Size = new Size(1545, 907);
        splitContainerMain.SplitterDistance = 197;
        splitContainerMain.SplitterWidth = 5;
        splitContainerMain.TabIndex = 0;
        // 
        // pnlDanhSachBan
        // 
        pnlDanhSachBan.Controls.Add(flpDanhSachBan);
        pnlDanhSachBan.Controls.Add(lblChuThichDangChoi);
        pnlDanhSachBan.Controls.Add(lblChuThichTrong);
        pnlDanhSachBan.Controls.Add(lblDanhSachBan);
        pnlDanhSachBan.Location = new Point(0, 0);
        pnlDanhSachBan.Margin = new Padding(3, 4, 3, 4);
        pnlDanhSachBan.Name = "pnlDanhSachBan";
        pnlDanhSachBan.Padding = new Padding(14, 16, 14, 16);
        pnlDanhSachBan.Size = new Size(197, 907);
        pnlDanhSachBan.TabIndex = 0;
        // 
        // flpDanhSachBan
        // 
        flpDanhSachBan.AutoScroll = true;
        flpDanhSachBan.Location = new Point(14, 124);
        flpDanhSachBan.Margin = new Padding(3, 4, 3, 4);
        flpDanhSachBan.Name = "flpDanhSachBan";
        flpDanhSachBan.Padding = new Padding(5);
        flpDanhSachBan.Size = new Size(169, 767);
        flpDanhSachBan.TabIndex = 1;
        // 
        // lblChuThichDangChoi
        // 
        lblChuThichDangChoi.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblChuThichDangChoi.AutoSize = true;
        lblChuThichDangChoi.ForeColor = Color.Firebrick;
        lblChuThichDangChoi.Location = new Point(126, 1581);
        lblChuThichDangChoi.Name = "lblChuThichDangChoi";
        lblChuThichDangChoi.Size = new Size(90, 20);
        lblChuThichDangChoi.TabIndex = 2;
        lblChuThichDangChoi.Text = "● Đang chơi";
        // 
        // lblChuThichTrong
        // 
        lblChuThichTrong.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblChuThichTrong.AutoSize = true;
        lblChuThichTrong.ForeColor = Color.ForestGreen;
        lblChuThichTrong.Location = new Point(18, 1581);
        lblChuThichTrong.Name = "lblChuThichTrong";
        lblChuThichTrong.Size = new Size(60, 20);
        lblChuThichTrong.TabIndex = 3;
        lblChuThichTrong.Text = "● Trống";
        // 
        // lblDanhSachBan
        // 
        lblDanhSachBan.Dock = DockStyle.Top;
        lblDanhSachBan.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
        lblDanhSachBan.Location = new Point(14, 16);
        lblDanhSachBan.Name = "lblDanhSachBan";
        lblDanhSachBan.Size = new Size(169, 125);
        lblDanhSachBan.TabIndex = 4;
        lblDanhSachBan.Text = "DANH SÁCH BÀN";
        lblDanhSachBan.TextAlign = ContentAlignment.MiddleCenter;
        lblDanhSachBan.Click += lblDanhSachBan_Click;
        // 
        // pnlThongTinBan
        // 
        pnlThongTinBan.Controls.Add(layoutThongTin);
        pnlThongTinBan.Dock = DockStyle.Fill;
        pnlThongTinBan.Location = new Point(0, 0);
        pnlThongTinBan.Margin = new Padding(3, 4, 3, 4);
        pnlThongTinBan.Name = "pnlThongTinBan";
        pnlThongTinBan.Padding = new Padding(18, 21, 18, 21);
        pnlThongTinBan.Size = new Size(1343, 907);
        pnlThongTinBan.TabIndex = 0;
        pnlThongTinBan.Paint += pnlThongTinBan_Paint;
        // 
        // layoutThongTin
        // 
        layoutThongTin.ColumnCount = 4;
        layoutThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        layoutThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 21.7068653F));
        layoutThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.76809F));
        layoutThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28.6641922F));
        layoutThongTin.Controls.Add(lblTieuDe, 0, 0);
        layoutThongTin.Controls.Add(lblTenBan, 0, 1);
        layoutThongTin.Controls.Add(lblGioBatDau, 2, 1);
        layoutThongTin.Controls.Add(lblGiaTheoGio, 0, 2);
        layoutThongTin.Controls.Add(lblDichVu, 0, 3);
        layoutThongTin.Controls.Add(cboDichVu, 1, 3);
        layoutThongTin.Controls.Add(nudSoLuong, 2, 3);
        layoutThongTin.Controls.Add(btnThemMon, 3, 3);
        layoutThongTin.Controls.Add(dgvChiTietHoaDon, 0, 4);
        layoutThongTin.Controls.Add(lblTienGio, 0, 5);
        layoutThongTin.Controls.Add(lblTienDichVu, 2, 5);
        layoutThongTin.Controls.Add(lblTongTien, 0, 6);
        layoutThongTin.Controls.Add(btnMoBan, 2, 7);
        layoutThongTin.Controls.Add(btnThanhToan, 3, 7);
        layoutThongTin.Dock = DockStyle.Fill;
        layoutThongTin.Location = new Point(18, 21);
        layoutThongTin.Margin = new Padding(3, 4, 3, 4);
        layoutThongTin.Name = "layoutThongTin";
        layoutThongTin.RowCount = 8;
        layoutThongTin.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));
        layoutThongTin.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        layoutThongTin.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        layoutThongTin.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
        layoutThongTin.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        layoutThongTin.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        layoutThongTin.RowStyles.Add(new RowStyle(SizeType.Absolute, 59F));
        layoutThongTin.RowStyles.Add(new RowStyle(SizeType.Absolute, 69F));
        layoutThongTin.Size = new Size(1307, 865);
        layoutThongTin.TabIndex = 0;
        // 
        // lblTieuDe
        // 
        layoutThongTin.SetColumnSpan(lblTieuDe, 4);
        lblTieuDe.Dock = DockStyle.Fill;
        lblTieuDe.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTieuDe.Location = new Point(3, 0);
        lblTieuDe.Name = "lblTieuDe";
        lblTieuDe.Size = new Size(1301, 64);
        lblTieuDe.TabIndex = 0;
        lblTieuDe.Text = "QUẢN LÝ BÀN BIDA & DỊCH VỤ";
        lblTieuDe.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblTenBan
        // 
        layoutThongTin.SetColumnSpan(lblTenBan, 2);
        lblTenBan.Dock = DockStyle.Fill;
        lblTenBan.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblTenBan.Location = new Point(3, 64);
        lblTenBan.Name = "lblTenBan";
        lblTenBan.Size = new Size(603, 56);
        lblTenBan.TabIndex = 1;
        lblTenBan.Text = "Chưa chọn bàn";
        lblTenBan.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblGioBatDau
        // 
        layoutThongTin.SetColumnSpan(lblGioBatDau, 2);
        lblGioBatDau.Dock = DockStyle.Fill;
        lblGioBatDau.Location = new Point(612, 64);
        lblGioBatDau.Name = "lblGioBatDau";
        lblGioBatDau.Size = new Size(692, 56);
        lblGioBatDau.TabIndex = 2;
        lblGioBatDau.Text = "Giờ bắt đầu: --";
        lblGioBatDau.TextAlign = ContentAlignment.MiddleRight;
        lblGioBatDau.Click += lblGioBatDau_Click;
        // 
        // lblGiaTheoGio
        // 
        layoutThongTin.SetColumnSpan(lblGiaTheoGio, 4);
        lblGiaTheoGio.Dock = DockStyle.Fill;
        lblGiaTheoGio.Location = new Point(3, 120);
        lblGiaTheoGio.Name = "lblGiaTheoGio";
        lblGiaTheoGio.Size = new Size(1301, 48);
        lblGiaTheoGio.TabIndex = 3;
        lblGiaTheoGio.Text = "Giá giờ: 60.000 đ/giờ · tính theo block 15 phút";
        // 
        // lblDichVu
        // 
        lblDichVu.Dock = DockStyle.Fill;
        lblDichVu.Location = new Point(3, 168);
        lblDichVu.Name = "lblDichVu";
        lblDichVu.Size = new Size(320, 44);
        lblDichVu.TabIndex = 4;
        lblDichVu.Text = "Dịch vụ:";
        lblDichVu.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // cboDichVu
        // 
        cboDichVu.Dock = DockStyle.Fill;
        cboDichVu.DropDownStyle = ComboBoxStyle.DropDownList;
        cboDichVu.Location = new Point(329, 177);
        cboDichVu.Margin = new Padding(3, 9, 9, 4);
        cboDichVu.Name = "cboDichVu";
        cboDichVu.Size = new Size(271, 28);
        cboDichVu.TabIndex = 5;
        // 
        // nudSoLuong
        // 
        nudSoLuong.Dock = DockStyle.Fill;
        nudSoLuong.Location = new Point(614, 177);
        nudSoLuong.Margin = new Padding(5, 9, 9, 4);
        nudSoLuong.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
        nudSoLuong.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        nudSoLuong.Name = "nudSoLuong";
        nudSoLuong.Size = new Size(309, 27);
        nudSoLuong.TabIndex = 6;
        nudSoLuong.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // btnThemMon
        // 
        btnThemMon.Dock = DockStyle.Fill;
        btnThemMon.Location = new Point(935, 172);
        btnThemMon.Margin = new Padding(3, 4, 3, 4);
        btnThemMon.Name = "btnThemMon";
        btnThemMon.Size = new Size(369, 36);
        btnThemMon.TabIndex = 7;
        btnThemMon.Text = "Thêm món";
        btnThemMon.UseVisualStyleBackColor = true;
        btnThemMon.Click += btnThemMon_Click;
        // 
        // dgvChiTietHoaDon
        // 
        dgvChiTietHoaDon.AllowUserToAddRows = false;
        dgvChiTietHoaDon.AllowUserToDeleteRows = false;
        dgvChiTietHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvChiTietHoaDon.BackgroundColor = SystemColors.Window;
        dgvChiTietHoaDon.ColumnHeadersHeight = 29;
        layoutThongTin.SetColumnSpan(dgvChiTietHoaDon, 4);
        dgvChiTietHoaDon.Dock = DockStyle.Fill;
        dgvChiTietHoaDon.Location = new Point(3, 216);
        dgvChiTietHoaDon.Margin = new Padding(3, 4, 3, 4);
        dgvChiTietHoaDon.Name = "dgvChiTietHoaDon";
        dgvChiTietHoaDon.ReadOnly = true;
        dgvChiTietHoaDon.RowHeadersVisible = false;
        dgvChiTietHoaDon.RowHeadersWidth = 51;
        dgvChiTietHoaDon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvChiTietHoaDon.Size = new Size(1301, 469);
        dgvChiTietHoaDon.TabIndex = 8;
        // 
        // lblTienGio
        // 
        layoutThongTin.SetColumnSpan(lblTienGio, 2);
        lblTienGio.Dock = DockStyle.Fill;
        lblTienGio.Location = new Point(3, 689);
        lblTienGio.Name = "lblTienGio";
        lblTienGio.Size = new Size(603, 48);
        lblTienGio.TabIndex = 9;
        lblTienGio.Text = "Tiền giờ: 0 đ";
        lblTienGio.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblTienDichVu
        // 
        layoutThongTin.SetColumnSpan(lblTienDichVu, 2);
        lblTienDichVu.Dock = DockStyle.Fill;
        lblTienDichVu.Location = new Point(612, 689);
        lblTienDichVu.Name = "lblTienDichVu";
        lblTienDichVu.Size = new Size(692, 48);
        lblTienDichVu.TabIndex = 10;
        lblTienDichVu.Text = "Tiền dịch vụ: 0 đ";
        lblTienDichVu.TextAlign = ContentAlignment.MiddleRight;
        // 
        // lblTongTien
        // 
        layoutThongTin.SetColumnSpan(lblTongTien, 4);
        lblTongTien.Dock = DockStyle.Fill;
        lblTongTien.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        lblTongTien.ForeColor = Color.DarkBlue;
        lblTongTien.Location = new Point(3, 737);
        lblTongTien.Name = "lblTongTien";
        lblTongTien.Size = new Size(1301, 59);
        lblTongTien.TabIndex = 11;
        lblTongTien.Text = "TỔNG TẠM TÍNH: 0 đ";
        lblTongTien.TextAlign = ContentAlignment.MiddleRight;
        // 
        // btnMoBan
        // 
        btnMoBan.BackColor = Color.ForestGreen;
        btnMoBan.Dock = DockStyle.Fill;
        btnMoBan.ForeColor = Color.White;
        btnMoBan.Location = new Point(612, 800);
        btnMoBan.Margin = new Padding(3, 4, 3, 4);
        btnMoBan.Name = "btnMoBan";
        btnMoBan.Size = new Size(317, 61);
        btnMoBan.TabIndex = 12;
        btnMoBan.Text = "Mở bàn";
        btnMoBan.UseVisualStyleBackColor = false;
        btnMoBan.Click += btnMoBan_Click;
        // 
        // btnThanhToan
        // 
        btnThanhToan.BackColor = Color.Firebrick;
        btnThanhToan.Dock = DockStyle.Fill;
        btnThanhToan.ForeColor = Color.White;
        btnThanhToan.Location = new Point(935, 800);
        btnThanhToan.Margin = new Padding(3, 4, 3, 4);
        btnThanhToan.Name = "btnThanhToan";
        btnThanhToan.Size = new Size(369, 61);
        btnThanhToan.TabIndex = 13;
        btnThanhToan.Text = "Thanh toán";
        btnThanhToan.UseVisualStyleBackColor = false;
        btnThanhToan.Click += btnThanhToan_Click;
        // 
        // FormMainBilliards
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1545, 907);
        Controls.Add(splitContainerMain);
        Margin = new Padding(3, 4, 3, 4);
        MinimumSize = new Size(1026, 784);
        Name = "FormMainBilliards";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Hệ thống quản lý bàn bida & dịch vụ giải trí";
        splitContainerMain.Panel1.ResumeLayout(false);
        splitContainerMain.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)splitContainerMain).EndInit();
        splitContainerMain.ResumeLayout(false);
        pnlDanhSachBan.ResumeLayout(false);
        pnlDanhSachBan.PerformLayout();
        pnlThongTinBan.ResumeLayout(false);
        layoutThongTin.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)nudSoLuong).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvChiTietHoaDon).EndInit();
        ResumeLayout(false);
    }
}
