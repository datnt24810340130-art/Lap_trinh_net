namespace bai6._7
{
    partial class FormKhamBenh
    {
        private System.ComponentModel.IContainer? components = null;
        private Panel pnlHeader = null!;
        private Label lblTitle = null!;
        private Label lblSubtitle = null!;
        private Button btnQuanLyBenhNhan = null!;
        private Button btnQuanLyThuoc = null!;
        private Button btnLichSuKham = null!;
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
            btnQuanLyBenhNhan = new Button();
            btnQuanLyThuoc = new Button();
            btnLichSuKham = new Button();
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
            pnlHeader.Controls.Add(btnQuanLyBenhNhan);
            pnlHeader.Controls.Add(btnQuanLyThuoc);
            pnlHeader.Controls.Add(btnLichSuKham);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Margin = new Padding(3, 4, 3, 4);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1257, 109);
            pnlHeader.TabIndex = 5;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(27, 13);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(338, 41);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "QUẢN LÝ KHÁM BỆNH";
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 9F);
            lblSubtitle.ForeColor = Color.FromArgb(220, 245, 240);
            lblSubtitle.Location = new Point(31, 65);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(349, 20);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Tiếp nhận bệnh nhân  |  Kê đơn thuốc  |  Thanh toán";
            // 
            // btnQuanLyBenhNhan
            // 
            btnQuanLyBenhNhan.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnQuanLyBenhNhan.BackColor = Color.White;
            btnQuanLyBenhNhan.FlatStyle = FlatStyle.Flat;
            btnQuanLyBenhNhan.ForeColor = Color.FromArgb(21, 101, 92);
            btnQuanLyBenhNhan.Location = new Point(1045, 34);
            btnQuanLyBenhNhan.Name = "btnQuanLyBenhNhan";
            btnQuanLyBenhNhan.Size = new Size(190, 40);
            btnQuanLyBenhNhan.TabIndex = 2;
            btnQuanLyBenhNhan.Text = "Quản lý bệnh nhân";
            btnQuanLyBenhNhan.UseVisualStyleBackColor = false;
            // 
            // btnQuanLyThuoc
            // 
            btnQuanLyThuoc.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnQuanLyThuoc.BackColor = Color.White;
            btnQuanLyThuoc.FlatStyle = FlatStyle.Flat;
            btnQuanLyThuoc.ForeColor = Color.FromArgb(21, 101, 92);
            btnQuanLyThuoc.Location = new Point(850, 34);
            btnQuanLyThuoc.Name = "btnQuanLyThuoc";
            btnQuanLyThuoc.Size = new Size(180, 40);
            btnQuanLyThuoc.TabIndex = 3;
            btnQuanLyThuoc.Text = "Quản lý thuốc";
            btnQuanLyThuoc.UseVisualStyleBackColor = false;
            // 
            // btnLichSuKham
            // 
            btnLichSuKham.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLichSuKham.BackColor = Color.White;
            btnLichSuKham.FlatStyle = FlatStyle.Flat;
            btnLichSuKham.ForeColor = Color.FromArgb(21, 101, 92);
            btnLichSuKham.Location = new Point(660, 34);
            btnLichSuKham.Name = "btnLichSuKham";
            btnLichSuKham.Size = new Size(175, 40);
            btnLichSuKham.TabIndex = 4;
            btnLichSuKham.Text = "Lịch sử khám";
            btnLichSuKham.UseVisualStyleBackColor = false;
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
            grpTiepNhan.Location = new Point(23, 131);
            grpTiepNhan.Margin = new Padding(3, 4, 3, 4);
            grpTiepNhan.Name = "grpTiepNhan";
            grpTiepNhan.Padding = new Padding(3, 4, 3, 4);
            grpTiepNhan.Size = new Size(1211, 245);
            grpTiepNhan.TabIndex = 4;
            grpTiepNhan.TabStop = false;
            grpTiepNhan.Text = "1. Tiếp nhận và thông tin khám";
            // 
            // lblSoDienThoai
            // 
            lblSoDienThoai.AutoSize = true;
            lblSoDienThoai.Location = new Point(23, 45);
            lblSoDienThoai.Name = "lblSoDienThoai";
            lblSoDienThoai.Size = new Size(97, 20);
            lblSoDienThoai.TabIndex = 0;
            lblSoDienThoai.Text = "Số điện thoại";
            // 
            // txtSoDienThoai
            // 
            txtSoDienThoai.Location = new Point(23, 72);
            txtSoDienThoai.Margin = new Padding(3, 4, 3, 4);
            txtSoDienThoai.MaxLength = 20;
            txtSoDienThoai.Name = "txtSoDienThoai";
            txtSoDienThoai.PlaceholderText = "Nhập số điện thoại";
            txtSoDienThoai.Size = new Size(268, 27);
            txtSoDienThoai.TabIndex = 1;
            // 
            // btnTimKiem
            // 
            btnTimKiem.BackColor = Color.FromArgb(230, 245, 242);
            btnTimKiem.FlatStyle = FlatStyle.Flat;
            btnTimKiem.Location = new Point(309, 67);
            btnTimKiem.Margin = new Padding(3, 4, 3, 4);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new Size(126, 36);
            btnTimKiem.TabIndex = 2;
            btnTimKiem.Text = "Tìm bệnh nhân";
            btnTimKiem.UseVisualStyleBackColor = false;
            // 
            // lblHoTen
            // 
            lblHoTen.AutoSize = true;
            lblHoTen.Location = new Point(451, 45);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Size = new Size(54, 20);
            lblHoTen.TabIndex = 3;
            lblHoTen.Text = "Họ tên";
            // 
            // txtHoTen
            // 
            txtHoTen.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtHoTen.Location = new Point(451, 72);
            txtHoTen.Margin = new Padding(3, 4, 3, 4);
            txtHoTen.MaxLength = 150;
            txtHoTen.Name = "txtHoTen";
            txtHoTen.PlaceholderText = "Nhập họ tên nếu là bệnh nhân mới";
            txtHoTen.Size = new Size(731, 27);
            txtHoTen.TabIndex = 4;
            // 
            // lblChanDoan
            // 
            lblChanDoan.AutoSize = true;
            lblChanDoan.Location = new Point(23, 125);
            lblChanDoan.Name = "lblChanDoan";
            lblChanDoan.Size = new Size(80, 20);
            lblChanDoan.TabIndex = 5;
            lblChanDoan.Text = "Chẩn đoán";
            // 
            // txtChanDoan
            // 
            txtChanDoan.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtChanDoan.Location = new Point(23, 152);
            txtChanDoan.Margin = new Padding(3, 4, 3, 4);
            txtChanDoan.MaxLength = 500;
            txtChanDoan.Name = "txtChanDoan";
            txtChanDoan.PlaceholderText = "Nhập chẩn đoán";
            txtChanDoan.Size = new Size(697, 27);
            txtChanDoan.TabIndex = 6;
            // 
            // lblTienKham
            // 
            lblTienKham.AutoSize = true;
            lblTienKham.Location = new Point(743, 125);
            lblTienKham.Name = "lblTienKham";
            lblTienKham.Size = new Size(77, 20);
            lblTienKham.TabIndex = 7;
            lblTienKham.Text = "Tiền khám";
            // 
            // nudTienKham
            // 
            nudTienKham.Location = new Point(743, 152);
            nudTienKham.Margin = new Padding(3, 4, 3, 4);
            nudTienKham.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            nudTienKham.MaximumSize = new Size(171, 0);
            nudTienKham.Name = "nudTienKham";
            nudTienKham.Size = new Size(171, 27);
            nudTienKham.TabIndex = 8;
            nudTienKham.ThousandsSeparator = true;
            // 
            // btnTaoHoSo
            // 
            btnTaoHoSo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnTaoHoSo.BackColor = Color.FromArgb(21, 101, 92);
            btnTaoHoSo.FlatStyle = FlatStyle.Flat;
            btnTaoHoSo.ForeColor = Color.White;
            btnTaoHoSo.Location = new Point(943, 147);
            btnTaoHoSo.Margin = new Padding(3, 4, 3, 4);
            btnTaoHoSo.Name = "btnTaoHoSo";
            btnTaoHoSo.Size = new Size(240, 41);
            btnTaoHoSo.TabIndex = 9;
            btnTaoHoSo.Text = "Tạo hồ sơ khám";
            btnTaoHoSo.UseVisualStyleBackColor = false;
            // 
            // lblMaHoSo
            // 
            lblMaHoSo.AutoSize = true;
            lblMaHoSo.ForeColor = Color.FromArgb(21, 101, 92);
            lblMaHoSo.Location = new Point(23, 201);
            lblMaHoSo.Name = "lblMaHoSo";
            lblMaHoSo.Size = new Size(149, 20);
            lblMaHoSo.TabIndex = 10;
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
            grpKeDon.Location = new Point(23, 392);
            grpKeDon.Margin = new Padding(3, 4, 3, 4);
            grpKeDon.Name = "grpKeDon";
            grpKeDon.Padding = new Padding(3, 4, 3, 4);
            grpKeDon.Size = new Size(1211, 467);
            grpKeDon.TabIndex = 3;
            grpKeDon.TabStop = false;
            grpKeDon.Text = "2. Kê đơn thuốc";
            // 
            // lblThuoc
            // 
            lblThuoc.AutoSize = true;
            lblThuoc.Location = new Point(23, 41);
            lblThuoc.Name = "lblThuoc";
            lblThuoc.Size = new Size(49, 20);
            lblThuoc.TabIndex = 0;
            lblThuoc.Text = "Thuốc";
            // 
            // cboThuoc
            // 
            cboThuoc.DropDownStyle = ComboBoxStyle.DropDownList;
            cboThuoc.FormattingEnabled = true;
            cboThuoc.Location = new Point(23, 68);
            cboThuoc.Margin = new Padding(3, 4, 3, 4);
            cboThuoc.Name = "cboThuoc";
            cboThuoc.Size = new Size(537, 28);
            cboThuoc.TabIndex = 1;
            // 
            // lblSoLuong
            // 
            lblSoLuong.AutoSize = true;
            lblSoLuong.Location = new Point(589, 41);
            lblSoLuong.Name = "lblSoLuong";
            lblSoLuong.Size = new Size(69, 20);
            lblSoLuong.TabIndex = 2;
            lblSoLuong.Text = "Số lượng";
            // 
            // txtSoLuong
            // 
            txtSoLuong.Location = new Point(589, 68);
            txtSoLuong.Margin = new Padding(3, 4, 3, 4);
            txtSoLuong.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            txtSoLuong.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            txtSoLuong.Name = "txtSoLuong";
            txtSoLuong.Size = new Size(114, 27);
            txtSoLuong.TabIndex = 3;
            txtSoLuong.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnThemThuoc
            // 
            btnThemThuoc.BackColor = Color.FromArgb(230, 245, 242);
            btnThemThuoc.FlatStyle = FlatStyle.Flat;
            btnThemThuoc.Location = new Point(726, 65);
            btnThemThuoc.Margin = new Padding(3, 4, 3, 4);
            btnThemThuoc.Name = "btnThemThuoc";
            btnThemThuoc.Size = new Size(149, 36);
            btnThemThuoc.TabIndex = 4;
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
            dgvDonThuoc.Location = new Point(23, 121);
            dgvDonThuoc.Margin = new Padding(3, 4, 3, 4);
            dgvDonThuoc.MultiSelect = false;
            dgvDonThuoc.Name = "dgvDonThuoc";
            dgvDonThuoc.ReadOnly = true;
            dgvDonThuoc.RowHeadersVisible = false;
            dgvDonThuoc.RowHeadersWidth = 51;
            dgvDonThuoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDonThuoc.Size = new Size(1160, 280);
            dgvDonThuoc.TabIndex = 5;
            // 
            // colMaThuoc
            // 
            colMaThuoc.FillWeight = 65F;
            colMaThuoc.HeaderText = "Mã thuốc";
            colMaThuoc.MinimumWidth = 6;
            colMaThuoc.Name = "colMaThuoc";
            colMaThuoc.ReadOnly = true;
            // 
            // colTenThuoc
            // 
            colTenThuoc.FillWeight = 170F;
            colTenThuoc.HeaderText = "Tên thuốc";
            colTenThuoc.MinimumWidth = 6;
            colTenThuoc.Name = "colTenThuoc";
            colTenThuoc.ReadOnly = true;
            // 
            // colDonViTinh
            // 
            colDonViTinh.FillWeight = 65F;
            colDonViTinh.HeaderText = "Đơn vị";
            colDonViTinh.MinimumWidth = 6;
            colDonViTinh.Name = "colDonViTinh";
            colDonViTinh.ReadOnly = true;
            // 
            // colSoLuong
            // 
            colSoLuong.FillWeight = 65F;
            colSoLuong.HeaderText = "Số lượng";
            colSoLuong.MinimumWidth = 6;
            colSoLuong.Name = "colSoLuong";
            colSoLuong.ReadOnly = true;
            // 
            // colDonGia
            // 
            colDonGia.FillWeight = 90F;
            colDonGia.HeaderText = "Đơn giá";
            colDonGia.MinimumWidth = 6;
            colDonGia.Name = "colDonGia";
            colDonGia.ReadOnly = true;
            // 
            // colThanhTien
            // 
            colThanhTien.HeaderText = "Thành tiền";
            colThanhTien.MinimumWidth = 6;
            colThanhTien.Name = "colThanhTien";
            colThanhTien.ReadOnly = true;
            // 
            // btnXoaThuoc
            // 
            btnXoaThuoc.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnXoaThuoc.BackColor = Color.FromArgb(255, 242, 242);
            btnXoaThuoc.FlatStyle = FlatStyle.Flat;
            btnXoaThuoc.Location = new Point(1029, 412);
            btnXoaThuoc.Margin = new Padding(3, 4, 3, 4);
            btnXoaThuoc.Name = "btnXoaThuoc";
            btnXoaThuoc.Size = new Size(154, 36);
            btnXoaThuoc.TabIndex = 6;
            btnXoaThuoc.Text = "Xóa thuốc chọn";
            btnXoaThuoc.UseVisualStyleBackColor = false;
            // 
            // lblTongTien
            // 
            lblTongTien.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblTongTien.AutoSize = true;
            lblTongTien.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTongTien.ForeColor = Color.FromArgb(21, 101, 92);
            lblTongTien.Location = new Point(27, 901);
            lblTongTien.Name = "lblTongTien";
            lblTongTien.Size = new Size(175, 32);
            lblTongTien.TabIndex = 2;
            lblTongTien.Text = "Tổng tiền: 0 ₫";
            // 
            // btnLuuXuatDon
            // 
            btnLuuXuatDon.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnLuuXuatDon.BackColor = Color.FromArgb(21, 101, 92);
            btnLuuXuatDon.FlatStyle = FlatStyle.Flat;
            btnLuuXuatDon.ForeColor = Color.White;
            btnLuuXuatDon.Location = new Point(789, 895);
            btnLuuXuatDon.Margin = new Padding(3, 4, 3, 4);
            btnLuuXuatDon.Name = "btnLuuXuatDon";
            btnLuuXuatDon.Size = new Size(211, 51);
            btnLuuXuatDon.TabIndex = 1;
            btnLuuXuatDon.Text = "Lưu và xuất đơn";
            btnLuuXuatDon.UseVisualStyleBackColor = false;
            // 
            // btnThanhToan
            // 
            btnThanhToan.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnThanhToan.BackColor = Color.FromArgb(30, 136, 100);
            btnThanhToan.FlatStyle = FlatStyle.Flat;
            btnThanhToan.ForeColor = Color.White;
            btnThanhToan.Location = new Point(1017, 895);
            btnThanhToan.Margin = new Padding(3, 4, 3, 4);
            btnThanhToan.Name = "btnThanhToan";
            btnThanhToan.Size = new Size(217, 51);
            btnThanhToan.TabIndex = 0;
            btnThanhToan.Text = "Thanh toán";
            btnThanhToan.UseVisualStyleBackColor = false;
            // 
            // FormKhamBenh
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 248, 248);
            ClientSize = new Size(1257, 980);
            Controls.Add(btnThanhToan);
            Controls.Add(btnLuuXuatDon);
            Controls.Add(lblTongTien);
            Controls.Add(grpKeDon);
            Controls.Add(grpTiepNhan);
            Controls.Add(pnlHeader);
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(1026, 918);
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
