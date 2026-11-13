using bai6._6.BUS;
using bai6._6.DTO;

namespace bai6._6;

public partial class FormMuonTra : Form
{
    private readonly PhieuMuonBUS _phieuMuonBus = new();
    private bool _initializing = true;

    public FormMuonTra()
    {
        InitializeComponent();
    }

    private void FormMuonTra_Load(object? sender, EventArgs e)
    {
        cboTrangThai.Items.AddRange(["Tất cả", "Đang mượn", "Đã trả"]);
        cboTrangThai.SelectedIndex = 0;
        _initializing = false;
        LoadLoans();
    }

    private void cboTrangThai_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (!_initializing)
        {
            LoadLoans();
        }
    }

    private void btnLoc_Click(object? sender, EventArgs e) => LoadLoans();

    private void btnTaiLai_Click(object? sender, EventArgs e)
    {
        txtHoTen.Clear();
        cboTrangThai.SelectedIndex = 0;
        LoadLoans();
    }

    private void btnTraSach_Click(object? sender, EventArgs e)
    {
        if (dgvPhieuMuon.CurrentRow?.DataBoundItem is not PhieuMuonDTO loan)
        {
            MessageBox.Show("Vui lòng chọn phiếu mượn cần trả.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (!string.Equals(loan.TrangThai, "Đang mượn", StringComparison.Ordinal))
        {
            MessageBox.Show("Phiếu này đã được trả.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        DialogResult confirmation = MessageBox.Show($"Xác nhận trả sách cho phiếu {loan.MaPhieu}?", "Xác nhận trả sách", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (confirmation != DialogResult.Yes)
        {
            return;
        }

        try
        {
            _phieuMuonBus.ReturnLoan(loan.MaPhieu);
            LoadLoans();
            MessageBox.Show("Trả sách thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            ShowError(ex);
        }
    }

    private void btnCanhBao_Click(object? sender, EventArgs e)
    {
        try
        {
            List<PhieuMuonDTO> overdueLoans = _phieuMuonBus.GetOverdueLoans();
            if (overdueLoans.Count == 0)
            {
                MessageBox.Show("Hiện không có phiếu mượn quá hạn.", "Cảnh báo quá hạn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string message = string.Join(Environment.NewLine, overdueLoans.Select(loan =>
                $"Phiếu {loan.MaPhieu} - {loan.HoTen} - Hạn trả: {loan.HanTra:dd/MM/yyyy}"));
            MessageBox.Show(message, $"Có {overdueLoans.Count} phiếu quá hạn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (Exception ex)
        {
            ShowError(ex);
        }
    }

    private void btnLapPhieu_Click(object? sender, EventArgs e)
    {
        if (!int.TryParse(txtMaDocGia.Text.Trim(), out int maDocGia) ||
            !int.TryParse(txtMaSach.Text.Trim(), out int maSach))
        {
            MessageBox.Show("Mã độc giả và mã sách phải là số nguyên hợp lệ.", "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            int maPhieu = _phieuMuonBus.CreateLoan(maDocGia, dtpHanTra.Value.Date,
            [new ChiTietMuonDTO { MaSach = maSach, SoLuong = (int)nudSoLuong.Value }]);
            LoadLoans();
            MessageBox.Show($"Đã lập phiếu mượn số {maPhieu}.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            ShowError(ex);
        }
    }

    private void LoadLoans()
    {
        try
        {
            string? status = cboTrangThai.SelectedItem?.ToString();
            dgvPhieuMuon.DataSource = _phieuMuonBus.GetAll(trangThai: status, tenDocGia: txtHoTen.Text);
        }
        catch (Exception ex)
        {
            ShowError(ex);
        }
    }

    private static void ShowError(Exception ex) =>
        MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
}
