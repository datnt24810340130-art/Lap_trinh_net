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
        components = new System.ComponentModel.Container();
        splitContainerMain = new SplitContainer();
        pnlDanhSachBan = new Panel();
        lblDanhSachBan = new Label();
        flpDanhSachBan = new FlowLayoutPanel();
        lblChuThichTrong = new Label();
        lblChuThichDangChoi = new Label();
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
        splitContainerMain.Name = "splitContainerMain";
        splitContainerMain.Panel1.Controls.Add(pnlDanhSachBan);
        splitContainerMain.Panel2.Controls.Add(pnlThongTinBan);
        splitContainerMain.SplitterDistance = 310;
        splitContainerMain.TabIndex = 0;
        // 
        // pnlDanhSachBan
        // 
        pnlDanhSachBan.Controls.Add(flpDanhSachBan);
        pnlDanhSachBan.Controls.Add(lblChuThichDangChoi);
        pnlDanhSachBan.Controls.Add(lblChuThichTrong);
        pnlDanhSachBan.Controls.Add(lblDanhSachBan);
        pnlDanhSachBan.Dock = DockStyle.Fill;
        pnlDanhSachBan.Padding = new Padding(12);
        pnlDanhSachBan.TabIndex = 0;
        // 
        // lblDanhSachBan
        // 
        lblDanhSachBan.Dock = DockStyle.Top;
        lblDanhSachBan.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
        lblDanhSachBan.Height = 45;
        lblDanhSachBan.Text = "DANH SÁCH BÀN";
        lblDanhSachBan.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // flpDanhSachBan
        // 
        flpDanhSachBan.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        flpDanhSachBan.AutoScroll = true;
        flpDanhSachBan.Location = new Point(12, 62);
        flpDanhSachBan.Name = "flpDanhSachBan";
        flpDanhSachBan.Padding = new Padding(4);
        flpDanhSachBan.Size = new Size(286, 535);
        flpDanhSachBan.TabIndex = 1;
        // 
        // legend
        // 
        lblChuThichTrong.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblChuThichTrong.AutoSize = true;
        lblChuThichTrong.ForeColor = Color.ForestGreen;
        lblChuThichTrong.Location = new Point(16, 605);
        lblChuThichTrong.Text = "● Trống";
        lblChuThichDangChoi.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblChuThichDangChoi.AutoSize = true;
        lblChuThichDangChoi.ForeColor = Color.Firebrick;
        lblChuThichDangChoi.Location = new Point(110, 605);
        lblChuThichDangChoi.Text = "● Đang chơi";
        // 
        // pnlThongTinBan
        // 
        pnlThongTinBan.Controls.Add(layoutThongTin);
        pnlThongTinBan.Dock = DockStyle.Fill;
        pnlThongTinBan.Padding = new Padding(16);
        // 
        // layoutThongTin
        // 
        layoutThongTin.ColumnCount = 4;
        layoutThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        layoutThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        layoutThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        layoutThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
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
        layoutThongTin.RowCount = 8;
        layoutThongTin.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        layoutThongTin.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        layoutThongTin.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
        layoutThongTin.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
        layoutThongTin.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        layoutThongTin.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
        layoutThongTin.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
        layoutThongTin.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
        layoutThongTin.TabIndex = 0;
        // 
        // header and info
        // 
        layoutThongTin.SetColumnSpan(lblTieuDe, 4);
        lblTieuDe.Dock = DockStyle.Fill;
        lblTieuDe.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTieuDe.Text = "QUẢN LÝ BÀN BIDA & DỊCH VỤ";
        lblTieuDe.TextAlign = ContentAlignment.MiddleLeft;
        layoutThongTin.SetColumnSpan(lblTenBan, 2);
        lblTenBan.Dock = DockStyle.Fill;
        lblTenBan.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblTenBan.Text = "Chưa chọn bàn";
        lblTenBan.TextAlign = ContentAlignment.MiddleLeft;
        layoutThongTin.SetColumnSpan(lblGioBatDau, 2);
        lblGioBatDau.Dock = DockStyle.Fill;
        lblGioBatDau.Text = "Giờ bắt đầu: --";
        lblGioBatDau.TextAlign = ContentAlignment.MiddleRight;
        layoutThongTin.SetColumnSpan(lblGiaTheoGio, 4);
        lblGiaTheoGio.Dock = DockStyle.Fill;
        lblGiaTheoGio.Text = "Giá giờ: 60.000 đ/giờ · tính theo block 15 phút";
        lblDichVu.Dock = DockStyle.Fill;
        lblDichVu.Text = "Dịch vụ:";
        lblDichVu.TextAlign = ContentAlignment.MiddleLeft;
        cboDichVu.Dock = DockStyle.Fill;
        cboDichVu.DropDownStyle = ComboBoxStyle.DropDownList;
        cboDichVu.Margin = new Padding(3, 7, 8, 3);
        nudSoLuong.Dock = DockStyle.Fill;
        nudSoLuong.Minimum = 1;
        nudSoLuong.Maximum = 999;
        nudSoLuong.Value = 1;
        nudSoLuong.Margin = new Padding(4, 7, 8, 3);
        btnThemMon.Dock = DockStyle.Fill;
        btnThemMon.Text = "Thêm món";
        btnThemMon.UseVisualStyleBackColor = true;
        btnThemMon.Click += btnThemMon_Click;
        // 
        // dgvChiTietHoaDon
        // 
        layoutThongTin.SetColumnSpan(dgvChiTietHoaDon, 4);
        dgvChiTietHoaDon.AllowUserToAddRows = false;
        dgvChiTietHoaDon.AllowUserToDeleteRows = false;
        dgvChiTietHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvChiTietHoaDon.BackgroundColor = SystemColors.Window;
        dgvChiTietHoaDon.Dock = DockStyle.Fill;
        dgvChiTietHoaDon.ReadOnly = true;
        dgvChiTietHoaDon.RowHeadersVisible = false;
        dgvChiTietHoaDon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvChiTietHoaDon.TabIndex = 8;
        // 
        // totals and actions
        // 
        layoutThongTin.SetColumnSpan(lblTienGio, 2);
        lblTienGio.Dock = DockStyle.Fill;
        lblTienGio.Text = "Tiền giờ: 0 đ";
        lblTienGio.TextAlign = ContentAlignment.MiddleLeft;
        layoutThongTin.SetColumnSpan(lblTienDichVu, 2);
        lblTienDichVu.Dock = DockStyle.Fill;
        lblTienDichVu.Text = "Tiền dịch vụ: 0 đ";
        lblTienDichVu.TextAlign = ContentAlignment.MiddleRight;
        layoutThongTin.SetColumnSpan(lblTongTien, 4);
        lblTongTien.Dock = DockStyle.Fill;
        lblTongTien.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        lblTongTien.ForeColor = Color.DarkBlue;
        lblTongTien.Text = "TỔNG TẠM TÍNH: 0 đ";
        lblTongTien.TextAlign = ContentAlignment.MiddleRight;
        btnMoBan.Dock = DockStyle.Fill;
        btnMoBan.BackColor = Color.ForestGreen;
        btnMoBan.ForeColor = Color.White;
        btnMoBan.Text = "Mở bàn";
        btnMoBan.UseVisualStyleBackColor = false;
        btnMoBan.Click += btnMoBan_Click;
        btnThanhToan.Dock = DockStyle.Fill;
        btnThanhToan.BackColor = Color.Firebrick;
        btnThanhToan.ForeColor = Color.White;
        btnThanhToan.Text = "Thanh toán";
        btnThanhToan.UseVisualStyleBackColor = false;
        btnThanhToan.Click += btnThanhToan_Click;
        // 
        // FormMainBilliards
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1100, 680);
        Controls.Add(splitContainerMain);
        MinimumSize = new Size(900, 600);
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
