using bai6._6.BUS;
using bai6._6.DTO;

namespace bai6._6;

public partial class FormMuonTra : Form
{
    private readonly PhieuMuonBUS _phieuMuonBus = new();
    private readonly DauSachBUS _dauSachBus = new();
    private bool _initializing = true;
    private bool _loadingBooks;

    public FormMuonTra()
    {
        InitializeComponent();
    }

    private void FormMuonTra_Load(object? sender, EventArgs e)
    {
        dtpHanTra.Value = DateTime.Today.AddDays(14);
        cboTrangThai.Items.AddRange(["Tất cả", "Đang mượn", "Đã trả"]);
        cboTrangThai.SelectedIndex = 0;
        _initializing = false;
        LoadBooks();
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

    private void btnQuanLyDocGia_Click(object? sender, EventArgs e)
    {
        using var form = new FormDocGia();
        form.ShowDialog(this);
        LoadLoans();
    }

    private void btnQuanLySach_Click(object? sender, EventArgs e)
    {
        using var form = new FormDauSach();
        form.ShowDialog(this);
        LoadBooks();
    }

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
        if (!int.TryParse(txtMaDocGia.Text.Trim(), out int maDocGia))
        {
            MessageBox.Show("Mã độc giả phải là số nguyên hợp lệ.", "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (cboMaSach.SelectedItem is not DauSachDTO selectedBook)
        {
            MessageBox.Show("Vui lòng chọn sách còn bản để mượn.", "Chưa chọn sách", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            int maPhieu = _phieuMuonBus.CreateLoan(maDocGia, dtpHanTra.Value.Date,
            [new ChiTietMuonDTO { MaSach = selectedBook.MaSach, SoLuong = (int)nudSoLuong.Value }]);
            LoadLoans();
            MessageBox.Show($"Đã lập phiếu mượn số {maPhieu}.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            ShowError(ex);
        }
    }

    private void LoadBooks()
    {
        try
        {
            _loadingBooks = true;
            List<DauSachDTO> availableBooks = _dauSachBus.GetAll()
                .Where(book => book.SoLuongCon > 0)
                .ToList();

            cboMaSach.DataSource = null;
            cboMaSach.DisplayMember = nameof(DauSachDTO.HienThi);
            cboMaSach.ValueMember = nameof(DauSachDTO.MaSach);
            cboMaSach.DataSource = availableBooks;
            cboMaSach.SelectedIndex = availableBooks.Count > 0 ? 0 : -1;
            cboMaSach.Enabled = availableBooks.Count > 0;
            btnLapPhieu.Enabled = availableBooks.Count > 0;
            UpdateMaximumLoanQuantity();
        }
        catch (Exception ex)
        {
            cboMaSach.DataSource = null;
            cboMaSach.Enabled = false;
            btnLapPhieu.Enabled = false;
            ShowError(ex);
        }
        finally
        {
            _loadingBooks = false;
        }
    }

    private void cboMaSach_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (!_loadingBooks)
        {
            UpdateMaximumLoanQuantity();
        }
    }

    private void UpdateMaximumLoanQuantity()
    {
        if (cboMaSach.SelectedItem is DauSachDTO book)
        {
            nudSoLuong.Maximum = Math.Max(1, book.SoLuongCon);
            nudSoLuong.Enabled = true;
        }
        else
        {
            nudSoLuong.Maximum = 1;
            nudSoLuong.Value = 1;
            nudSoLuong.Enabled = false;
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
