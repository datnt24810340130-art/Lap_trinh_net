using System.Data;
using bai6._8.BUS;
using bai6._8.DAL;
using bai6._8.DTO;

namespace bai6._8;

public partial class FormMainBilliards : Form
{
    private const decimal GiaTheoGio = 60000m;
    private readonly BanBidaDAL banBidaDAL = new();
    private readonly MenuDichVuDAL menuDichVuDAL = new();
    private readonly HoaDonDAL hoaDonDAL = new();
    private readonly ChiTietHoaDonDAL chiTietHoaDonDAL = new();
    private readonly ThanhToanDAL thanhToanDAL = new();
    private readonly System.Windows.Forms.Timer timerCapNhat = new() { Interval = 30000 };
    private HoaDonDTO? hoaDonHienTai;
    private BanBidaDTO? banDangChon;

    public FormMainBilliards()
    {
        InitializeComponent();
        Load += FormMainBilliards_Load;
        timerCapNhat.Tick += (_, _) => CapNhatTamTinh();
    }

    private void FormMainBilliards_Load(object? sender, EventArgs e)
    {
        try
        {
            NapDanhSachBan();
            cboDichVu.DataSource = menuDichVuDAL.LayDanhSachDichVu();
            cboDichVu.DisplayMember = nameof(MenuDichVuDTO.TenDV);
            cboDichVu.ValueMember = nameof(MenuDichVuDTO.MaDV);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Không thể tải dữ liệu. Hãy chạy Database/ScriptDB.sql và kiểm tra kết nối.\n\n{ex.Message}",
                "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void NapDanhSachBan()
    {
        flpDanhSachBan.Controls.Clear();
        foreach (var ban in banBidaDAL.LayDanhSachBan())
        {
            var button = new Button
            {
                Text = ban.TenBan,
                Tag = ban,
                Width = 118,
                Height = 68,
                Margin = new Padding(6),
                BackColor = ban.TrangThai == "Trống" ? Color.LightGreen : Color.Salmon,
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                UseVisualStyleBackColor = false
            };
            button.Click += BanBida_Click;
            flpDanhSachBan.Controls.Add(button);
        }
    }

    private void BanBida_Click(object? sender, EventArgs e)
    {
        if (sender is not Button { Tag: BanBidaDTO ban })
            return;

        banDangChon = ban;
        hoaDonHienTai = hoaDonDAL.LayHoaDonDangMo(ban.MaBan);
        lblTenBan.Text = $"{ban.TenBan} · {ban.TrangThai}";
        lblGioBatDau.Text = hoaDonHienTai is null
            ? "Giờ bắt đầu: --"
            : $"Giờ bắt đầu: {hoaDonHienTai.GioBatDau:dd/MM/yyyy HH:mm:ss}";
        btnMoBan.Enabled = hoaDonHienTai is null && ban.TrangThai == "Trống";
        btnThemMon.Enabled = hoaDonHienTai is not null;
        btnThanhToan.Enabled = hoaDonHienTai is not null;
        timerCapNhat.Enabled = hoaDonHienTai is not null;
        NapChiTietHoaDon();
        CapNhatTamTinh();
    }

    private void btnMoBan_Click(object? sender, EventArgs e)
    {
        if (banDangChon is null || banDangChon.TrangThai != "Trống")
        {
            MessageBox.Show("Vui lòng chọn một bàn đang trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        try
        {
            hoaDonHienTai = hoaDonDAL.MoBan(banDangChon.MaBan, GiaTheoGio);
            banDangChon.TrangThai = "Đang chơi";
            lblGioBatDau.Text = $"Giờ bắt đầu: {hoaDonHienTai.GioBatDau:dd/MM/yyyy HH:mm:ss}";
            btnMoBan.Enabled = false;
            btnThemMon.Enabled = true;
            btnThanhToan.Enabled = true;
            timerCapNhat.Start();
            NapDanhSachBan();
            NapChiTietHoaDon();
            CapNhatTamTinh();
        }
        catch (Exception ex)
        {
            HienThiLoi(ex);
        }
    }

    private void btnThemMon_Click(object? sender, EventArgs e)
    {
        if (hoaDonHienTai is null || cboDichVu.SelectedItem is not MenuDichVuDTO dichVu)
        {
            MessageBox.Show("Vui lòng chọn bàn đang chơi và dịch vụ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        try
        {
            chiTietHoaDonDAL.ThemHoacCapNhatDichVu(hoaDonHienTai.MaHD, dichVu.MaDV, (int)nudSoLuong.Value);
            NapChiTietHoaDon();
            CapNhatTamTinh();
        }
        catch (Exception ex)
        {
            HienThiLoi(ex);
        }
    }

    private void btnThanhToan_Click(object? sender, EventArgs e)
    {
        if (hoaDonHienTai is null)
            return;

        var xacNhan = MessageBox.Show("Xác nhận thanh toán và đóng bàn?", "Thanh toán",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (xacNhan != DialogResult.Yes)
            return;

        try
        {
            var gioKetThuc = DateTime.Now;
            var tienGio = TinhTienBUS.TinhTienGioChoi(hoaDonHienTai.GioBatDau, gioKetThuc, hoaDonHienTai.GiaTheoGio);
            var ketQua = thanhToanDAL.ThanhToan(hoaDonHienTai.MaHD, gioKetThuc, tienGio.SoGio, tienGio.TienGioChoi);
            timerCapNhat.Stop();
            MessageBox.Show(
                $"Tiền giờ: {ketQua.TienGioChoi:N0} đ\nTiền dịch vụ: {ketQua.TienDichVu:N0} đ\nTổng thanh toán: {ketQua.TongTien:N0} đ",
                "Thanh toán thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            hoaDonHienTai = null;
            banDangChon = null;
            lblTenBan.Text = "Chưa chọn bàn";
            lblGioBatDau.Text = "Giờ bắt đầu: --";
            dgvChiTietHoaDon.DataSource = null;
            lblTienGio.Text = "Tiền giờ: 0 đ";
            lblTienDichVu.Text = "Tiền dịch vụ: 0 đ";
            lblTongTien.Text = "TỔNG TẠM TÍNH: 0 đ";
            btnMoBan.Enabled = false;
            btnThemMon.Enabled = false;
            btnThanhToan.Enabled = false;
            NapDanhSachBan();
        }
        catch (Exception ex)
        {
            HienThiLoi(ex);
        }
    }

    private void NapChiTietHoaDon()
    {
        DataTable? table = hoaDonHienTai is null ? null : chiTietHoaDonDAL.NapDichVuTheoHoaDon(hoaDonHienTai.MaHD);
        dgvChiTietHoaDon.DataSource = table;
        if (dgvChiTietHoaDon.Columns.Count == 0)
            return;

        dgvChiTietHoaDon.Columns["MaDV"].Visible = false;
        dgvChiTietHoaDon.Columns["TenDV"].HeaderText = "Tên dịch vụ";
        dgvChiTietHoaDon.Columns["SoLuong"].HeaderText = "Số lượng";
        dgvChiTietHoaDon.Columns["DonGia"].HeaderText = "Đơn giá";
        dgvChiTietHoaDon.Columns["ThanhTien"].HeaderText = "Thành tiền";
        dgvChiTietHoaDon.Columns["DonGia"].DefaultCellStyle.Format = "N0";
        dgvChiTietHoaDon.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
    }

    private void CapNhatTamTinh()
    {
        if (hoaDonHienTai is null)
        {
            lblTienGio.Text = "Tiền giờ: 0 đ";
            lblTienDichVu.Text = "Tiền dịch vụ: 0 đ";
            lblTongTien.Text = "TỔNG TẠM TÍNH: 0 đ";
            return;
        }

        var tinhTien = TinhTienBUS.TinhTienGioChoi(hoaDonHienTai.GioBatDau, DateTime.Now, hoaDonHienTai.GiaTheoGio);
        decimal tienDichVu = 0;
        if (dgvChiTietHoaDon.DataSource is DataTable table)
        {
            foreach (DataRow row in table.Rows)
                tienDichVu += Convert.ToDecimal(row["ThanhTien"]);
        }

        lblTienGio.Text = $"Tiền giờ: {tinhTien.TienGioChoi:N0} đ";
        lblTienDichVu.Text = $"Tiền dịch vụ: {tienDichVu:N0} đ";
        lblTongTien.Text = $"TỔNG TẠM TÍNH: {tinhTien.TienGioChoi + tienDichVu:N0} đ";
    }

    private static void HienThiLoi(Exception ex)
    {
        MessageBox.Show(ex.Message, "Không thể thực hiện", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    private void pnlThongTinBan_Paint(object sender, PaintEventArgs e)
    {

    }

    private void lblGioBatDau_Click(object sender, EventArgs e)
    {

    }

    private void lblDanhSachBan_Click(object sender, EventArgs e)
    {

    }
}
