namespace bai6._7;

partial class FormLichSuKham
{
    private System.ComponentModel.IContainer? components = null;
    private Label lblTieuDe = null!;
    private Label lblTimKiem = null!;
    private TextBox txtTimKiem = null!;
    private Button btnTimKiem = null!;
    private Button btnLamMoi = null!;
    private DataGridView dgvLichSu = null!;
    private Label lblChiTiet = null!;
    private DataGridView dgvChiTietDon = null!;
    private Label lblGhiChu = null!;

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
        dgvLichSu = new DataGridView();
        lblChiTiet = new Label();
        dgvChiTietDon = new DataGridView();
        lblGhiChu = new Label();
        ((System.ComponentModel.ISupportInitialize)dgvLichSu).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvChiTietDon).BeginInit();
        SuspendLayout();
        // 
        // lblTieuDe
        // 
        lblTieuDe.AutoSize = true;
        lblTieuDe.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblTieuDe.ForeColor = Color.FromArgb(21, 101, 92);
        lblTieuDe.Location = new Point(20, 16);
        lblTieuDe.Name = "lblTieuDe";
        lblTieuDe.Size = new Size(280, 32);
        lblTieuDe.Text = "LỊCH SỬ KHÁM BỆNH";
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
        txtTimKiem.PlaceholderText = "Tên bệnh nhân, số điện thoại hoặc chẩn đoán";
        txtTimKiem.Size = new Size(770, 23);
        // 
        // btnTimKiem
        // 
        btnTimKiem.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnTimKiem.BackColor = Color.FromArgb(230, 245, 242);
        btnTimKiem.FlatStyle = FlatStyle.Flat;
        btnTimKiem.Location = new Point(810, 86);
        btnTimKiem.Name = "btnTimKiem";
        btnTimKiem.Size = new Size(115, 28);
        btnTimKiem.Text = "Tìm kiếm";
        btnTimKiem.UseVisualStyleBackColor = false;
        // 
        // btnLamMoi
        // 
        btnLamMoi.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnLamMoi.FlatStyle = FlatStyle.Flat;
        btnLamMoi.Location = new Point(940, 86);
        btnLamMoi.Name = "btnLamMoi";
        btnLamMoi.Size = new Size(135, 28);
        btnLamMoi.Text = "Tải lại danh sách";
        // 
        // dgvLichSu
        // 
        dgvLichSu.AllowUserToAddRows = false;
        dgvLichSu.AllowUserToDeleteRows = false;
        dgvLichSu.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        dgvLichSu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvLichSu.BackgroundColor = Color.White;
        dgvLichSu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvLichSu.Location = new Point(22, 132);
        dgvLichSu.MultiSelect = false;
        dgvLichSu.Name = "dgvLichSu";
        dgvLichSu.ReadOnly = true;
        dgvLichSu.RowHeadersVisible = false;
        dgvLichSu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvLichSu.Size = new Size(1053, 300);
        // 
        // lblChiTiet
        // 
        lblChiTiet.AutoSize = true;
        lblChiTiet.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblChiTiet.ForeColor = Color.FromArgb(21, 101, 92);
        lblChiTiet.Location = new Point(22, 449);
        lblChiTiet.Name = "lblChiTiet";
        lblChiTiet.Size = new Size(260, 20);
        lblChiTiet.Text = "Chi tiết đơn thuốc của hồ sơ đã chọn";
        // 
        // dgvChiTietDon
        // 
        dgvChiTietDon.AllowUserToAddRows = false;
        dgvChiTietDon.AllowUserToDeleteRows = false;
        dgvChiTietDon.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvChiTietDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvChiTietDon.BackgroundColor = Color.White;
        dgvChiTietDon.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvChiTietDon.Location = new Point(22, 479);
        dgvChiTietDon.MultiSelect = false;
        dgvChiTietDon.Name = "dgvChiTietDon";
        dgvChiTietDon.ReadOnly = true;
        dgvChiTietDon.RowHeadersVisible = false;
        dgvChiTietDon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvChiTietDon.Size = new Size(1053, 155);
        // 
        // lblGhiChu
        // 
        lblGhiChu.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblGhiChu.AutoSize = true;
        lblGhiChu.ForeColor = Color.FromArgb(90, 100, 100);
        lblGhiChu.Location = new Point(22, 648);
        lblGhiChu.Name = "lblGhiChu";
        lblGhiChu.Size = new Size(605, 15);
        lblGhiChu.Text = "Lịch sử chỉ đọc để bảo toàn số lượng tồn kho và thông tin thanh toán đã ghi nhận.";
        // 
        // FormLichSuKham
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(245, 248, 248);
        ClientSize = new Size(1100, 690);
        Controls.Add(lblGhiChu);
        Controls.Add(dgvChiTietDon);
        Controls.Add(lblChiTiet);
        Controls.Add(dgvLichSu);
        Controls.Add(btnLamMoi);
        Controls.Add(btnTimKiem);
        Controls.Add(txtTimKiem);
        Controls.Add(lblTimKiem);
        Controls.Add(lblTieuDe);
        MinimumSize = new Size(900, 600);
        Name = "FormLichSuKham";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Lịch sử khám bệnh và kê đơn";
        ((System.ComponentModel.ISupportInitialize)dgvLichSu).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvChiTietDon).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
