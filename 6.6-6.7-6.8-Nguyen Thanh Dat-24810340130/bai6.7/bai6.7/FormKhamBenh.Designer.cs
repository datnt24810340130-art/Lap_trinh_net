namespace bai6._7
{
    partial class FormKhamBenh
    {
        private System.ComponentModel.IContainer? components = null;
        private Panel pnlHeader = null!;
        private Label lblTitle = null!;
        private Label lblSubtitle = null!;
        private GroupBox grpTiepNhan = null!;
        private Label lblSoDienThoai = null!;
        private TextBox txtSoDienThoai = null!;
        private Button btnTimKiem = null!;
        private Label lblHoTen = null!;
        private TextBox txtHoTen = null!;
        private Label lblChanDoan = null!;
        private TextBox txtChanDoan = null!;
        private Label lblTienKham = null!;
        private NumericUpDown nudTienKham = null!;
        private Button btnTaoHoSo = null!;
        private Label lblMaHoSo = null!;
        private GroupBox grpKeDon = null!;
        private Label lblThuoc = null!;
        private ComboBox cboThuoc = null!;
        private Label lblSoLuong = null!;
        private NumericUpDown txtSoLuong = null!;
        private Button btnThemThuoc = null!;
        private DataGridView dgvDonThuoc = null!;
        private DataGridViewTextBoxColumn colMaThuoc = null!;
        private DataGridViewTextBoxColumn colTenThuoc = null!;
        private DataGridViewTextBoxColumn colDonViTinh = null!;
        private DataGridViewTextBoxColumn colSoLuong = null!;
        private DataGridViewTextBoxColumn colDonGia = null!;
        private DataGridViewTextBoxColumn colThanhTien = null!;
        private Button btnXoaThuoc = null!;
        private Label lblTongTien = null!;
        private Button btnLuuXuatDon = null!;
        private Button btnThanhToan = null!;

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
            pnlHeader = new Panel();
            lblTitle = new Label();
            lblSubtitle = new Label();
            grpTiepNhan = new GroupBox();
            lblSoDienThoai = new Label();
            txtSoDienThoai = new TextBox();
            btnTimKiem = new Button();
            lblHoTen = new Label();
            txtHoTen = new TextBox();
            lblChanDoan = new Label();
            txtChanDoan = new TextBox();
            lblTienKham = new Label();
            nudTienKham = new NumericUpDown();
            btnTaoHoSo = new Button();
            lblMaHoSo = new Label();
            grpKeDon = new GroupBox();
            lblThuoc = new Label();
            cboThuoc = new ComboBox();
            lblSoLuong = new Label();
            txtSoLuong = new NumericUpDown();
            btnThemThuoc = new Button();
            dgvDonThuoc = new DataGridView();
            colMaThuoc = new DataGridViewTextBoxColumn();
            colTenThuoc = new DataGridViewTextBoxColumn();
            colDonViTinh = new DataGridViewTextBoxColumn();
            colSoLuong = new DataGridViewTextBoxColumn();
            colDonGia = new DataGridViewTextBoxColumn();
            colThanhTien = new DataGridViewTextBoxColumn();
            btnXoaThuoc = new Button();
            lblTongTien = new Label();
            btnLuuXuatDon = new Button();
            btnThanhToan = new Button();
            pnlHeader.SuspendLayout();
            grpTiepNhan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudTienKham).BeginInit();
            grpKeDon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtSoLuong).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDonThuoc).BeginInit();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(21, 101, 92);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblSubtitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1100, 82);
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(24, 10);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(340, 32);
            lblTitle.Text = "QUẢN LÝ KHÁM BỆNH";
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 9F);
            lblSubtitle.ForeColor = Color.FromArgb(220, 245, 240);
            lblSubtitle.Location = new Point(27, 49);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(335, 15);
            lblSubtitle.Text = "Tiếp nhận bệnh nhân  |  Kê đơn thuốc  |  Thanh toán";
            // 
            // grpTiepNhan
            // 
            grpTiepNhan.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpTiepNhan.Controls.Add(lblSoDienThoai);
            grpTiepNhan.Controls.Add(txtSoDienThoai);
            grpTiepNhan.Controls.Add(btnTimKiem);
            grpTiepNhan.Controls.Add(lblHoTen);
            grpTiepNhan.Controls.Add(txtHoTen);
            grpTiepNhan.Controls.Add(lblChanDoan);
            grpTiepNhan.Controls.Add(txtChanDoan);
            grpTiepNhan.Controls.Add(lblTienKham);
            grpTiepNhan.Controls.Add(nudTienKham);
            grpTiepNhan.Controls.Add(btnTaoHoSo);
            grpTiepNhan.Controls.Add(lblMaHoSo);
            grpTiepNhan.Location = new Point(20, 98);
            grpTiepNhan.Name = "grpTiepNhan";
            grpTiepNhan.Size = new Size(1060, 184);
            grpTiepNhan.TabStop = false;
            grpTiepNhan.Text = "1. Tiếp nhận và thông tin khám";
            // 
            // lblSoDienThoai
            // 
            lblSoDienThoai.AutoSize = true;
            lblSoDienThoai.Location = new Point(20, 34);
            lblSoDienThoai.Name = "lblSoDienThoai";
            lblSoDienThoai.Size = new Size(79, 15);
            lblSoDienThoai.Text = "Số điện thoại";
            // 
            // txtSoDienThoai
            // 
            txtSoDienThoai.Location = new Point(20, 54);
            txtSoDienThoai.MaxLength = 20;
            txtSoDienThoai.Name = "txtSoDienThoai";
            txtSoDienThoai.PlaceholderText = "Nhập số điện thoại";
            txtSoDienThoai.Size = new Size(235, 23);
            // 
            // btnTimKiem
            // 
            btnTimKiem.BackColor = Color.FromArgb(230, 245, 242);
            btnTimKiem.FlatStyle = FlatStyle.Flat;
            btnTimKiem.Location = new Point(265, 53);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new Size(110, 27);
            btnTimKiem.Text = "Tìm bệnh nhân";
            btnTimKiem.UseVisualStyleBackColor = false;
            // 
            // lblHoTen
            // 
            lblHoTen.AutoSize = true;
            lblHoTen.Location = new Point(395, 34);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Size = new Size(43, 15);
            lblHoTen.Text = "Họ tên";
            // 
            // txtHoTen
            // 
            txtHoTen.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtHoTen.Location = new Point(395, 54);
            txtHoTen.MaxLength = 150;
            txtHoTen.Name = "txtHoTen";
            txtHoTen.PlaceholderText = "Nhập họ tên nếu là bệnh nhân mới";
            txtHoTen.Size = new Size(640, 23);
            // 
            // lblChanDoan
            // 
            lblChanDoan.AutoSize = true;
            lblChanDoan.Location = new Point(20, 94);
            lblChanDoan.Name = "lblChanDoan";
            lblChanDoan.Size = new Size(60, 15);
            lblChanDoan.Text = "Chẩn đoán";
            // 
            // txtChanDoan
            // 
            txtChanDoan.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtChanDoan.Location = new Point(20, 114);
            txtChanDoan.MaxLength = 500;
            txtChanDoan.Name = "txtChanDoan";
            txtChanDoan.PlaceholderText = "Nhập chẩn đoán";
            txtChanDoan.Size = new Size(610, 23);
            // 
            // lblTienKham
            // 
            lblTienKham.AutoSize = true;
            lblTienKham.Location = new Point(650, 94);
            lblTienKham.Name = "lblTienKham";
            lblTienKham.Size = new Size(59, 15);
            lblTienKham.Text = "Tiền khám";
            // 
            // nudTienKham
            // 
            nudTienKham.DecimalPlaces = 0;
            nudTienKham.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            nudTienKham.Location = new Point(650, 114);
            nudTienKham.MaximumSize = new Size(150, 0);
            nudTienKham.Name = "nudTienKham";
            nudTienKham.Size = new Size(150, 23);
            nudTienKham.ThousandsSeparator = true;
            // 
            // btnTaoHoSo
            // 
            btnTaoHoSo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnTaoHoSo.BackColor = Color.FromArgb(21, 101, 92);
            btnTaoHoSo.FlatStyle = FlatStyle.Flat;
            btnTaoHoSo.ForeColor = Color.White;
            btnTaoHoSo.Location = new Point(825, 110);
            btnTaoHoSo.Name = "btnTaoHoSo";
            btnTaoHoSo.Size = new Size(210, 31);
            btnTaoHoSo.Text = "Tạo hồ sơ khám";
            btnTaoHoSo.UseVisualStyleBackColor = false;
            // 
            // lblMaHoSo
            // 
            lblMaHoSo.AutoSize = true;
            lblMaHoSo.ForeColor = Color.FromArgb(21, 101, 92);
            lblMaHoSo.Location = new Point(20, 151);
            lblMaHoSo.Name = "lblMaHoSo";
            lblMaHoSo.Size = new Size(120, 15);
            lblMaHoSo.Text = "Chưa tạo hồ sơ khám";
            // 
            // grpKeDon
            // 
            grpKeDon.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpKeDon.Controls.Add(lblThuoc);
            grpKeDon.Controls.Add(cboThuoc);
            grpKeDon.Controls.Add(lblSoLuong);
            grpKeDon.Controls.Add(txtSoLuong);
            grpKeDon.Controls.Add(btnThemThuoc);
            grpKeDon.Controls.Add(dgvDonThuoc);
            grpKeDon.Controls.Add(btnXoaThuoc);
            grpKeDon.Location = new Point(20, 294);
            grpKeDon.Name = "grpKeDon";
            grpKeDon.Size = new Size(1060, 350);
            grpKeDon.TabStop = false;
            grpKeDon.Text = "2. Kê đơn thuốc";
            // 
            // lblThuoc
            // 
            lblThuoc.AutoSize = true;
            lblThuoc.Location = new Point(20, 31);
            lblThuoc.Name = "lblThuoc";
            lblThuoc.Size = new Size(37, 15);
            lblThuoc.Text = "Thuốc";
            // 
            // cboThuoc
            // 
            cboThuoc.DropDownStyle = ComboBoxStyle.DropDownList;
            cboThuoc.FormattingEnabled = true;
            cboThuoc.Location = new Point(20, 51);
            cboThuoc.Name = "cboThuoc";
            cboThuoc.Size = new Size(470, 23);
            // 
            // lblSoLuong
            // 
            lblSoLuong.AutoSize = true;
            lblSoLuong.Location = new Point(515, 31);
            lblSoLuong.Name = "lblSoLuong";
            lblSoLuong.Size = new Size(52, 15);
            lblSoLuong.Text = "Số lượng";
            // 
            // txtSoLuong
            // 
            txtSoLuong.Location = new Point(515, 51);
            txtSoLuong.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            txtSoLuong.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            txtSoLuong.Name = "txtSoLuong";
            txtSoLuong.Size = new Size(100, 23);
            txtSoLuong.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnThemThuoc
            // 
            btnThemThuoc.BackColor = Color.FromArgb(230, 245, 242);
            btnThemThuoc.FlatStyle = FlatStyle.Flat;
            btnThemThuoc.Location = new Point(635, 49);
            btnThemThuoc.Name = "btnThemThuoc";
            btnThemThuoc.Size = new Size(130, 27);
            btnThemThuoc.Text = "Thêm vào đơn";
            btnThemThuoc.UseVisualStyleBackColor = false;
            // 
            // dgvDonThuoc
            // 
            dgvDonThuoc.AllowUserToAddRows = false;
            dgvDonThuoc.AllowUserToDeleteRows = false;
            dgvDonThuoc.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvDonThuoc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDonThuoc.BackgroundColor = Color.White;
            dgvDonThuoc.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDonThuoc.Columns.AddRange(new DataGridViewColumn[] { colMaThuoc, colTenThuoc, colDonViTinh, colSoLuong, colDonGia, colThanhTien });
            dgvDonThuoc.Location = new Point(20, 91);
            dgvDonThuoc.MultiSelect = false;
            dgvDonThuoc.Name = "dgvDonThuoc";
            dgvDonThuoc.ReadOnly = true;
            dgvDonThuoc.RowHeadersVisible = false;
            dgvDonThuoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDonThuoc.Size = new Size(1015, 210);
            // 
            // colMaThuoc
            // 
            colMaThuoc.HeaderText = "Mã thuốc";
            colMaThuoc.Name = "colMaThuoc";
            colMaThuoc.ReadOnly = true;
            colMaThuoc.FillWeight = 65F;
            // 
            // colTenThuoc
            // 
            colTenThuoc.HeaderText = "Tên thuốc";
            colTenThuoc.Name = "colTenThuoc";
            colTenThuoc.ReadOnly = true;
            colTenThuoc.FillWeight = 170F;
            // 
            // colDonViTinh
            // 
            colDonViTinh.HeaderText = "Đơn vị";
            colDonViTinh.Name = "colDonViTinh";
            colDonViTinh.ReadOnly = true;
            colDonViTinh.FillWeight = 65F;
            // 
            // colSoLuong
            // 
            colSoLuong.HeaderText = "Số lượng";
            colSoLuong.Name = "colSoLuong";
            colSoLuong.ReadOnly = true;
            colSoLuong.FillWeight = 65F;
            // 
            // colDonGia
            // 
            colDonGia.HeaderText = "Đơn giá";
            colDonGia.Name = "colDonGia";
            colDonGia.ReadOnly = true;
            colDonGia.FillWeight = 90F;
            // 
            // colThanhTien
            // 
            colThanhTien.HeaderText = "Thành tiền";
            colThanhTien.Name = "colThanhTien";
            colThanhTien.ReadOnly = true;
            colThanhTien.FillWeight = 100F;
            // 
            // btnXoaThuoc
            // 
            btnXoaThuoc.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnXoaThuoc.BackColor = Color.FromArgb(255, 242, 242);
            btnXoaThuoc.FlatStyle = FlatStyle.Flat;
            btnXoaThuoc.Location = new Point(900, 309);
            btnXoaThuoc.Name = "btnXoaThuoc";
            btnXoaThuoc.Size = new Size(135, 27);
            btnXoaThuoc.Text = "Xóa thuốc chọn";
            btnXoaThuoc.UseVisualStyleBackColor = false;
            // 
            // lblTongTien
            // 
            lblTongTien.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblTongTien.AutoSize = true;
            lblTongTien.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTongTien.ForeColor = Color.FromArgb(21, 101, 92);
            lblTongTien.Location = new Point(24, 676);
            lblTongTien.Name = "lblTongTien";
            lblTongTien.Size = new Size(214, 25);
            lblTongTien.Text = "Tổng tiền: 0 ₫";
            // 
            // btnLuuXuatDon
            // 
            btnLuuXuatDon.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnLuuXuatDon.BackColor = Color.FromArgb(21, 101, 92);
            btnLuuXuatDon.FlatStyle = FlatStyle.Flat;
            btnLuuXuatDon.ForeColor = Color.White;
            btnLuuXuatDon.Location = new Point(690, 671);
            btnLuuXuatDon.Name = "btnLuuXuatDon";
            btnLuuXuatDon.Size = new Size(185, 38);
            btnLuuXuatDon.Text = "Lưu và xuất đơn";
            btnLuuXuatDon.UseVisualStyleBackColor = false;
            // 
            // btnThanhToan
            // 
            btnThanhToan.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnThanhToan.BackColor = Color.FromArgb(30, 136, 100);
            btnThanhToan.FlatStyle = FlatStyle.Flat;
            btnThanhToan.ForeColor = Color.White;
            btnThanhToan.Location = new Point(890, 671);
            btnThanhToan.Name = "btnThanhToan";
            btnThanhToan.Size = new Size(190, 38);
            btnThanhToan.Text = "Thanh toán";
            btnThanhToan.UseVisualStyleBackColor = false;
            // 
            // FormKhamBenh
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 248, 248);
            ClientSize = new Size(1100, 735);
            Controls.Add(btnThanhToan);
            Controls.Add(btnLuuXuatDon);
            Controls.Add(lblTongTien);
            Controls.Add(grpKeDon);
            Controls.Add(grpTiepNhan);
            Controls.Add(pnlHeader);
            MinimumSize = new Size(900, 700);
            Name = "FormKhamBenh";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hệ thống quản lý phòng khám";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            grpTiepNhan.ResumeLayout(false);
            grpTiepNhan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudTienKham).EndInit();
            grpKeDon.ResumeLayout(false);
            grpKeDon.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)txtSoLuong).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDonThuoc).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
