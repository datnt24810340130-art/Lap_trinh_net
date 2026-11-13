using bai6._7.BUS;
using bai6._7.DTO;

namespace bai6._7;

public partial class FormLichSuKham : Form
{
    private readonly LichSuKhamBUS _lichSuKhamBUS = new();
    private bool _loading;

    public FormLichSuKham()
    {
        InitializeComponent();
        Load += (_, _) => LoadHistory();
        btnTimKiem.Click += (_, _) => LoadHistory(txtTimKiem.Text);
        btnLamMoi.Click += (_, _) =>
        {
            txtTimKiem.Clear();
            LoadHistory();
        };
        dgvLichSu.SelectionChanged += DgvLichSu_SelectionChanged;
        txtTimKiem.KeyDown += (_, e) =>
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                LoadHistory(txtTimKiem.Text);
            }
        };
    }

    private void LoadHistory(string? keyword = null)
    {
        try
        {
            _loading = true;
            dgvLichSu.DataSource = _lichSuKhamBUS.TimKiem(keyword).ToList();
            dgvLichSu.ClearSelection();
            dgvLichSu.CurrentCell = null;
            ConfigureHistoryColumns();
            dgvChiTietDon.DataSource = null;
            lblChiTiet.Text = "Chi tiết đơn thuốc của hồ sơ đã chọn";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Không thể tải lịch sử khám.\n{ex.Message}",
                "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            _loading = false;
        }
    }

    private void DgvLichSu_SelectionChanged(object? sender, EventArgs e)
    {
        if (_loading || dgvLichSu.CurrentRow?.DataBoundItem is not LichSuKhamDTO visit)
        {
            return;
        }

        try
        {
            IReadOnlyList<ChiTietDonThuocDTO> details = _lichSuKhamBUS.LayChiTietDonThuoc(visit.MaHoSo);
            dgvChiTietDon.DataSource = details.ToList();
            ConfigureDetailColumns();
            lblChiTiet.Text = details.Count == 0
                ? $"Hồ sơ {visit.MaHoSo} chưa có đơn thuốc."
                : $"Chi tiết thuốc kê cho hồ sơ {visit.MaHoSo} ({details.Count} dòng)";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Không thể tải chi tiết đơn thuốc.\n{ex.Message}",
                "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ConfigureHistoryColumns()
    {
        SetColumn(dgvLichSu, nameof(LichSuKhamDTO.MaHoSo), "Mã hồ sơ", 55);
        SetColumn(dgvLichSu, nameof(LichSuKhamDTO.MaBN), "Mã BN", 45);
        SetColumn(dgvLichSu, nameof(LichSuKhamDTO.HoTen), "Bệnh nhân", 110);
        SetColumn(dgvLichSu, nameof(LichSuKhamDTO.SoDienThoai), "Số điện thoại", 90);
        SetColumn(dgvLichSu, nameof(LichSuKhamDTO.NgayKham), "Ngày khám", 95);
        SetColumn(dgvLichSu, nameof(LichSuKhamDTO.ChanDoan), "Chẩn đoán", 150);
        SetColumn(dgvLichSu, nameof(LichSuKhamDTO.TienKham), "Tiền khám", 75);
        SetColumn(dgvLichSu, nameof(LichSuKhamDTO.SoDonThuoc), "Số đơn", 50);
        SetColumn(dgvLichSu, nameof(LichSuKhamDTO.TienThuoc), "Tiền thuốc", 75);
        SetColumn(dgvLichSu, nameof(LichSuKhamDTO.TongTien), "Tổng tiền", 80);
        SetColumn(dgvLichSu, nameof(LichSuKhamDTO.TrangThai), "Thanh toán", 85);
        if (dgvLichSu.Columns[nameof(LichSuKhamDTO.NgayKham)] is DataGridViewColumn visitDateColumn)
        {
            visitDateColumn.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
        }

        foreach (string columnName in new[]
                 {
                     nameof(LichSuKhamDTO.TienKham), nameof(LichSuKhamDTO.TienThuoc), nameof(LichSuKhamDTO.TongTien)
                 })
        {
            if (dgvLichSu.Columns[columnName] is DataGridViewColumn moneyColumn)
            {
                moneyColumn.DefaultCellStyle.Format = "N0";
            }
        }
    }

    private void ConfigureDetailColumns()
    {
        SetColumn(dgvChiTietDon, nameof(ChiTietDonThuocDTO.MaDon), "Mã đơn", 55);
        SetColumn(dgvChiTietDon, nameof(ChiTietDonThuocDTO.MaThuoc), "Mã thuốc", 55);
        SetColumn(dgvChiTietDon, nameof(ChiTietDonThuocDTO.TenThuoc), "Tên thuốc", 150);
        SetColumn(dgvChiTietDon, nameof(ChiTietDonThuocDTO.DonViTinh), "Đơn vị", 70);
        SetColumn(dgvChiTietDon, nameof(ChiTietDonThuocDTO.SoLuong), "Số lượng", 65);
        SetColumn(dgvChiTietDon, nameof(ChiTietDonThuocDTO.DonGia), "Đơn giá", 80);
        SetColumn(dgvChiTietDon, nameof(ChiTietDonThuocDTO.ThanhTien), "Thành tiền", 85);
        foreach (string columnName in new[]
                 {
                     nameof(ChiTietDonThuocDTO.DonGia), nameof(ChiTietDonThuocDTO.ThanhTien)
                 })
        {
            if (dgvChiTietDon.Columns[columnName] is DataGridViewColumn moneyColumn)
            {
                moneyColumn.DefaultCellStyle.Format = "N0";
            }
        }
    }

    private static void SetColumn(DataGridView grid, string name, string header, int fillWeight)
    {
        if (grid.Columns[name] is DataGridViewColumn column)
        {
            column.HeaderText = header;
            column.FillWeight = fillWeight;
        }
    }
}
