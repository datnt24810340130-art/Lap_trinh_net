using bai6._6.BUS;
using bai6._6.DTO;

namespace bai6._6;

public partial class FormDocGia : Form
{
    private readonly DocGiaBUS _docGiaBus = new();
    private int? _selectedId;
    private bool _loading;

    public FormDocGia()
    {
        InitializeComponent();
    }

    private void FormDocGia_Load(object? sender, EventArgs e)
    {
        cboGioiTinh.Items.AddRange(["Nam", "Nữ", "Khác"]);
        cboGioiTinh.SelectedIndex = 0;
        LoadReaders();
    }

    private void LoadReaders()
    {
        try
        {
            _loading = true;
            dgvDocGia.DataSource = _docGiaBus.GetAll();
            if (dgvDocGia.Columns[nameof(DocGiaDTO.MaDocGia)] is not null)
            {
                dgvDocGia.Columns[nameof(DocGiaDTO.MaDocGia)]!.HeaderText = "Mã độc giả";
            }
            SetReaderHeaders();
            ClearEditor();
        }
        catch (Exception ex)
        {
            ShowError(ex);
        }
        finally
        {
            _loading = false;
        }
    }

    private void SetReaderHeaders()
    {
        SetHeader(nameof(DocGiaDTO.HoTen), "Họ tên");
        SetHeader(nameof(DocGiaDTO.NgaySinh), "Ngày sinh");
        SetHeader(nameof(DocGiaDTO.GioiTinh), "Giới tính");
        SetHeader(nameof(DocGiaDTO.DiaChi), "Địa chỉ");
        SetHeader(nameof(DocGiaDTO.SoDienThoai), "Số điện thoại");
        SetHeader(nameof(DocGiaDTO.Email), "Email");
        SetHeader(nameof(DocGiaDTO.NgayHetHanThe), "Hạn thẻ");
    }

    private void SetHeader(string columnName, string headerText)
    {
        if (dgvDocGia.Columns[columnName] is not null)
        {
            dgvDocGia.Columns[columnName]!.HeaderText = headerText;
        }
    }

    private void dgvDocGia_SelectionChanged(object? sender, EventArgs e)
    {
        if (_loading || dgvDocGia.CurrentRow?.DataBoundItem is not DocGiaDTO reader)
        {
            return;
        }

        _selectedId = reader.MaDocGia;
        txtHoTen.Text = reader.HoTen;
        chkCoNgaySinh.Checked = reader.NgaySinh.HasValue;
        dtpNgaySinh.Value = reader.NgaySinh ?? DateTime.Today;
        cboGioiTinh.SelectedItem = reader.GioiTinh;
        if (cboGioiTinh.SelectedIndex < 0)
        {
            cboGioiTinh.SelectedIndex = 0;
        }
        txtDiaChi.Text = reader.DiaChi ?? string.Empty;
        txtSoDienThoai.Text = reader.SoDienThoai ?? string.Empty;
        txtEmail.Text = reader.Email ?? string.Empty;
        dtpNgayHetHan.Value = reader.NgayHetHanThe;
    }

    private void btnThem_Click(object? sender, EventArgs e)
    {
        try
        {
            DocGiaDTO reader = ReadEditor();
            int id = _docGiaBus.Insert(reader);
            LoadReaders();
            MessageBox.Show($"Đã thêm độc giả mã {id}.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            ShowError(ex);
        }
    }

    private void btnSua_Click(object? sender, EventArgs e)
    {
        if (!_selectedId.HasValue)
        {
            ShowSelectMessage();
            return;
        }

        try
        {
            DocGiaDTO reader = ReadEditor();
            reader.MaDocGia = _selectedId.Value;
            _docGiaBus.Update(reader);
            LoadReaders();
            MessageBox.Show("Đã cập nhật thông tin độc giả.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            ShowError(ex);
        }
    }

    private void btnXoa_Click(object? sender, EventArgs e)
    {
        if (!_selectedId.HasValue)
        {
            ShowSelectMessage();
            return;
        }
        if (MessageBox.Show("Bạn có chắc muốn xóa độc giả đang chọn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        {
            return;
        }

        try
        {
            _docGiaBus.Delete(_selectedId.Value);
            LoadReaders();
            MessageBox.Show("Đã xóa độc giả.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            ShowError(ex);
        }
    }

    private void btnLamMoi_Click(object? sender, EventArgs e) => LoadReaders();

    private DocGiaDTO ReadEditor()
    {
        if (chkCoNgaySinh.Checked && dtpNgaySinh.Value.Date > DateTime.Today)
        {
            throw new ArgumentException("Ngày sinh không được ở tương lai.");
        }

        return new DocGiaDTO
        {
            HoTen = txtHoTen.Text.Trim(),
            NgaySinh = chkCoNgaySinh.Checked ? dtpNgaySinh.Value.Date : null,
            GioiTinh = cboGioiTinh.SelectedItem?.ToString(),
            DiaChi = NullIfEmpty(txtDiaChi.Text),
            SoDienThoai = NullIfEmpty(txtSoDienThoai.Text),
            Email = NullIfEmpty(txtEmail.Text),
            NgayHetHanThe = dtpNgayHetHan.Value.Date
        };
    }

    private void ClearEditor()
    {
        _selectedId = null;
        txtHoTen.Clear();
        chkCoNgaySinh.Checked = false;
        dtpNgaySinh.Value = DateTime.Today;
        cboGioiTinh.SelectedIndex = 0;
        txtDiaChi.Clear();
        txtSoDienThoai.Clear();
        txtEmail.Clear();
        dtpNgayHetHan.Value = DateTime.Today.AddYears(1);
        dgvDocGia.ClearSelection();
    }

    private void ShowSelectMessage() =>
        MessageBox.Show("Vui lòng chọn độc giả trong danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

    private static string? NullIfEmpty(string value) => string.IsNullOrWhiteSpace(value) ? null : value.Trim();

    private static void ShowError(Exception ex) =>
        MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
}
