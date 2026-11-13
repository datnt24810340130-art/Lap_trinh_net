using bai6._7.BUS;
using bai6._7.DTO;
using System.Data;

namespace bai6._7;

public partial class FormQuanLyBenhNhan : Form
{
    private readonly BenhNhanBUS _benhNhanBUS = new();
    private int? _selectedPatientId;
    private bool _loading;

    public FormQuanLyBenhNhan()
    {
        InitializeComponent();
        Load += (_, _) => LoadPatients();
        btnTimKiem.Click += (_, _) => LoadPatients(txtTimKiem.Text);
        btnLamMoi.Click += (_, _) =>
        {
            txtTimKiem.Clear();
            LoadPatients();
        };
        btnThem.Click += BtnThem_Click;
        btnCapNhat.Click += BtnCapNhat_Click;
        btnXoa.Click += BtnXoa_Click;
        btnXoaTrang.Click += (_, _) => ClearEditor();
        dgvBenhNhan.SelectionChanged += DgvBenhNhan_SelectionChanged;
        txtTimKiem.KeyDown += (_, e) =>
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                LoadPatients(txtTimKiem.Text);
            }
        };
        UpdateButtons();
    }

    private void LoadPatients(string? keyword = null)
    {
        try
        {
            _loading = true;
            dgvBenhNhan.DataSource = _benhNhanBUS.LayDanhSach(keyword).ToList();
            dgvBenhNhan.ClearSelection();
            dgvBenhNhan.CurrentCell = null;
            ConfigureGridColumns();
            _selectedPatientId = null;
            ClearEditor();
            UpdateButtons();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Không thể tải danh sách bệnh nhân.\n{ex.Message}",
                "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            _loading = false;
        }
    }

    private void ConfigureGridColumns()
    {
        SetColumn("MaBN", "Mã BN", 55);
        SetColumn("HoTen", "Họ tên", 150);
        SetColumn("SoDienThoai", "Số điện thoại", 100);
        SetColumn("NgaySinh", "Ngày sinh", 85);
        SetColumn("GioiTinh", "Giới tính", 70);
        SetColumn("DiaChi", "Địa chỉ", 150);
        if (dgvBenhNhan.Columns[nameof(BenhNhanDTO.NgaySinh)] is DataGridViewColumn birthDateColumn)
        {
            birthDateColumn.DefaultCellStyle.Format = "dd/MM/yyyy";
        }
    }

    private void SetColumn(string name, string header, int fillWeight)
    {
        if (dgvBenhNhan.Columns[name] is DataGridViewColumn column)
        {
            column.HeaderText = header;
            column.FillWeight = fillWeight;
        }
    }

    private void DgvBenhNhan_SelectionChanged(object? sender, EventArgs e)
    {
        if (_loading || dgvBenhNhan.CurrentRow?.DataBoundItem is not BenhNhanDTO patient)
        {
            return;
        }

        _selectedPatientId = patient.MaBN;
        lblMaBN.Text = $"Mã bệnh nhân: {patient.MaBN}";
        txtHoTen.Text = patient.HoTen;
        txtSoDienThoai.Text = patient.SoDienThoai;
        txtDiaChi.Text = patient.DiaChi ?? string.Empty;
        cboGioiTinh.SelectedItem = patient.GioiTinh;
        dtpNgaySinh.Checked = patient.NgaySinh.HasValue;
        if (patient.NgaySinh.HasValue)
        {
            dtpNgaySinh.Value = patient.NgaySinh.Value;
        }

        UpdateButtons();
    }

    private void BtnThem_Click(object? sender, EventArgs e)
    {
        try
        {
            BenhNhanDTO patient = ReadEditor();
            int patientId = _benhNhanBUS.ThemBenhNhan(patient);
            LoadPatients(txtTimKiem.Text);
            MessageBox.Show($"Đã thêm bệnh nhân mã {patientId}.", "Thành công",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Không thể thêm bệnh nhân", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void BtnCapNhat_Click(object? sender, EventArgs e)
    {
        if (!_selectedPatientId.HasValue)
        {
            MessageBox.Show("Hãy chọn bệnh nhân cần cập nhật.", "Chưa chọn bệnh nhân",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            BenhNhanDTO patient = ReadEditor();
            patient.MaBN = _selectedPatientId.Value;
            _benhNhanBUS.CapNhatBenhNhan(patient);
            LoadPatients(txtTimKiem.Text);
            MessageBox.Show("Đã cập nhật thông tin bệnh nhân.", "Thành công",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Không thể cập nhật bệnh nhân", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void BtnXoa_Click(object? sender, EventArgs e)
    {
        if (!_selectedPatientId.HasValue)
        {
            MessageBox.Show("Hãy chọn bệnh nhân cần xóa.", "Chưa chọn bệnh nhân",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        DialogResult answer = MessageBox.Show(
            $"Bạn có chắc muốn xóa bệnh nhân {txtHoTen.Text.Trim()}? Bệnh nhân có hồ sơ khám sẽ không thể xóa.",
            "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (answer != DialogResult.Yes)
        {
            return;
        }

        try
        {
            _benhNhanBUS.XoaBenhNhan(_selectedPatientId.Value);
            LoadPatients(txtTimKiem.Text);
            MessageBox.Show("Đã xóa bệnh nhân.", "Thành công",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Không thể xóa bệnh nhân", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private BenhNhanDTO ReadEditor() => new()
    {
        HoTen = txtHoTen.Text,
        SoDienThoai = txtSoDienThoai.Text,
        NgaySinh = dtpNgaySinh.Checked ? dtpNgaySinh.Value.Date : null,
        GioiTinh = cboGioiTinh.SelectedItem?.ToString(),
        DiaChi = txtDiaChi.Text
    };

    private void ClearEditor()
    {
        _selectedPatientId = null;
        lblMaBN.Text = "Chưa chọn bệnh nhân";
        txtHoTen.Clear();
        txtSoDienThoai.Clear();
        txtDiaChi.Clear();
        dtpNgaySinh.Checked = false;
        cboGioiTinh.SelectedIndex = -1;
        dgvBenhNhan.ClearSelection();
        UpdateButtons();
    }

    private void UpdateButtons()
    {
        btnCapNhat.Enabled = _selectedPatientId.HasValue;
        btnXoa.Enabled = _selectedPatientId.HasValue;
    }
}
