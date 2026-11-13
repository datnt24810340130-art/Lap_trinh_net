using bai6._7.BUS;
using bai6._7.DTO;

namespace bai6._7;

public partial class FormQuanLyThuoc : Form
{
    private readonly ThuocBUS _thuocBUS = new();
    private int? _selectedMedicineId;
    private bool _loading;

    public FormQuanLyThuoc()
    {
        InitializeComponent();
        Load += (_, _) => LoadMedicines();
        btnTimKiem.Click += (_, _) => LoadMedicines(txtTimKiem.Text);
        btnLamMoi.Click += (_, _) =>
        {
            txtTimKiem.Clear();
            LoadMedicines();
        };
        btnThem.Click += BtnThem_Click;
        btnCapNhat.Click += BtnCapNhat_Click;
        btnXoa.Click += BtnXoa_Click;
        btnXoaTrang.Click += (_, _) => ClearEditor();
        dgvThuoc.SelectionChanged += DgvThuoc_SelectionChanged;
        txtTimKiem.KeyDown += (_, e) =>
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                LoadMedicines(txtTimKiem.Text);
            }
        };
        UpdateButtons();
    }

    private void LoadMedicines(string? keyword = null)
    {
        try
        {
            _loading = true;
            dgvThuoc.DataSource = _thuocBUS.LayDanhSach(keyword).ToList();
            dgvThuoc.ClearSelection();
            dgvThuoc.CurrentCell = null;
            ConfigureGridColumns();
            ClearEditor();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Không thể tải danh mục thuốc.\n{ex.Message}",
                "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            _loading = false;
        }
    }

    private void ConfigureGridColumns()
    {
        SetColumn(nameof(ThuocDTO.MaThuoc), "Mã thuốc", 55);
        SetColumn(nameof(ThuocDTO.TenThuoc), "Tên thuốc", 170);
        SetColumn(nameof(ThuocDTO.DonViTinh), "Đơn vị tính", 75);
        SetColumn(nameof(ThuocDTO.DonGia), "Đơn giá", 90);
        SetColumn(nameof(ThuocDTO.SoLuongTon), "Tồn kho", 75);
        if (dgvThuoc.Columns[nameof(ThuocDTO.DonGia)] is DataGridViewColumn priceColumn)
        {
            priceColumn.DefaultCellStyle.Format = "N0";
        }
    }

    private void SetColumn(string name, string header, int fillWeight)
    {
        if (dgvThuoc.Columns[name] is DataGridViewColumn column)
        {
            column.HeaderText = header;
            column.FillWeight = fillWeight;
        }
    }

    private void DgvThuoc_SelectionChanged(object? sender, EventArgs e)
    {
        if (_loading || dgvThuoc.CurrentRow?.DataBoundItem is not ThuocDTO medicine)
        {
            return;
        }

        _selectedMedicineId = medicine.MaThuoc;
        lblMaThuoc.Text = $"Mã thuốc: {medicine.MaThuoc}";
        txtTenThuoc.Text = medicine.TenThuoc;
        txtDonViTinh.Text = medicine.DonViTinh;
        nudDonGia.Value = medicine.DonGia;
        nudSoLuongTon.Value = medicine.SoLuongTon;
        UpdateButtons();
    }

    private void BtnThem_Click(object? sender, EventArgs e)
    {
        try
        {
            int medicineId = _thuocBUS.Them(ReadEditor());
            LoadMedicines(txtTimKiem.Text);
            MessageBox.Show($"Đã thêm thuốc mã {medicineId}.", "Thành công",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Không thể thêm thuốc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void BtnCapNhat_Click(object? sender, EventArgs e)
    {
        if (!_selectedMedicineId.HasValue)
        {
            MessageBox.Show("Hãy chọn thuốc cần cập nhật.", "Chưa chọn thuốc",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            ThuocDTO medicine = ReadEditor();
            medicine.MaThuoc = _selectedMedicineId.Value;
            _thuocBUS.CapNhat(medicine);
            LoadMedicines(txtTimKiem.Text);
            MessageBox.Show("Đã cập nhật thông tin thuốc và tồn kho.", "Thành công",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Không thể cập nhật thuốc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void BtnXoa_Click(object? sender, EventArgs e)
    {
        if (!_selectedMedicineId.HasValue)
        {
            MessageBox.Show("Hãy chọn thuốc cần xóa.", "Chưa chọn thuốc",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (MessageBox.Show($"Bạn có chắc muốn xóa thuốc {txtTenThuoc.Text.Trim()}?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        {
            return;
        }

        try
        {
            _thuocBUS.Xoa(_selectedMedicineId.Value);
            LoadMedicines(txtTimKiem.Text);
            MessageBox.Show("Đã xóa thuốc.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Không thể xóa thuốc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private ThuocDTO ReadEditor() => new()
    {
        TenThuoc = txtTenThuoc.Text,
        DonViTinh = txtDonViTinh.Text,
        DonGia = nudDonGia.Value,
        SoLuongTon = decimal.ToInt32(nudSoLuongTon.Value)
    };

    private void ClearEditor()
    {
        _selectedMedicineId = null;
        lblMaThuoc.Text = "Chưa chọn thuốc";
        txtTenThuoc.Clear();
        txtDonViTinh.Clear();
        nudDonGia.Value = 0;
        nudSoLuongTon.Value = 0;
        dgvThuoc.ClearSelection();
        UpdateButtons();
    }

    private void UpdateButtons()
    {
        btnCapNhat.Enabled = _selectedMedicineId.HasValue;
        btnXoa.Enabled = _selectedMedicineId.HasValue;
    }
}
