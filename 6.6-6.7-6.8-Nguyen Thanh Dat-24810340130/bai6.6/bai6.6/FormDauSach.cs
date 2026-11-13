using bai6._6.BUS;
using bai6._6.DTO;

namespace bai6._6;

public partial class FormDauSach : Form
{
    private readonly DauSachBUS _dauSachBus = new();
    private int? _selectedId;
    private bool _loading;

    public FormDauSach()
    {
        InitializeComponent();
    }

    private void FormDauSach_Load(object? sender, EventArgs e) => LoadBooks();

    private void LoadBooks()
    {
        try
        {
            _loading = true;
            dgvDauSach.DataSource = _dauSachBus.GetAll();
            SetHeader(nameof(DauSachDTO.MaSach), "Mã sách");
            SetHeader(nameof(DauSachDTO.TenSach), "Tên sách");
            SetHeader(nameof(DauSachDTO.TacGia), "Tác giả");
            SetHeader(nameof(DauSachDTO.TheLoai), "Thể loại");
            SetHeader(nameof(DauSachDTO.NhaXuatBan), "Nhà xuất bản");
            SetHeader(nameof(DauSachDTO.NamXuatBan), "Năm xuất bản");
            SetHeader(nameof(DauSachDTO.SoLuong), "Tổng số lượng");
            SetHeader(nameof(DauSachDTO.SoLuongCon), "Số lượng còn");
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

    private void SetHeader(string columnName, string headerText)
    {
        if (dgvDauSach.Columns[columnName] is not null)
        {
            dgvDauSach.Columns[columnName]!.HeaderText = headerText;
        }
    }

    private void dgvDauSach_SelectionChanged(object? sender, EventArgs e)
    {
        if (_loading || dgvDauSach.CurrentRow?.DataBoundItem is not DauSachDTO book)
        {
            return;
        }

        _selectedId = book.MaSach;
        txtTenSach.Text = book.TenSach;
        txtTacGia.Text = book.TacGia ?? string.Empty;
        txtTheLoai.Text = book.TheLoai;
        txtNhaXuatBan.Text = book.NhaXuatBan ?? string.Empty;
        nudNamXuatBan.Value = book.NamXuatBan.HasValue
            ? Math.Clamp(book.NamXuatBan.Value, (int)nudNamXuatBan.Minimum, (int)nudNamXuatBan.Maximum)
            : 0;
        nudSoLuong.Value = book.SoLuong;
        nudSoLuongCon.Value = book.SoLuongCon;
    }

    private void btnThem_Click(object? sender, EventArgs e)
    {
        try
        {
            int id = _dauSachBus.Insert(ReadEditor());
            LoadBooks();
            MessageBox.Show($"Đã thêm đầu sách mã {id}.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            DauSachDTO book = ReadEditor();
            book.MaSach = _selectedId.Value;
            _dauSachBus.Update(book);
            LoadBooks();
            MessageBox.Show("Đã cập nhật đầu sách.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        if (MessageBox.Show("Bạn có chắc muốn xóa đầu sách đang chọn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        {
            return;
        }

        try
        {
            _dauSachBus.Delete(_selectedId.Value);
            LoadBooks();
            MessageBox.Show("Đã xóa đầu sách.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            ShowError(ex);
        }
    }

    private void btnLamMoi_Click(object? sender, EventArgs e) => LoadBooks();

    private DauSachDTO ReadEditor() => new()
    {
        TenSach = txtTenSach.Text.Trim(),
        TacGia = NullIfEmpty(txtTacGia.Text),
        TheLoai = txtTheLoai.Text.Trim(),
        NhaXuatBan = NullIfEmpty(txtNhaXuatBan.Text),
        NamXuatBan = nudNamXuatBan.Value == 0 ? null : (int)nudNamXuatBan.Value,
        SoLuong = (int)nudSoLuong.Value,
        SoLuongCon = (int)nudSoLuongCon.Value
    };

    private void ClearEditor()
    {
        _selectedId = null;
        txtTenSach.Clear();
        txtTacGia.Clear();
        txtTheLoai.Clear();
        txtNhaXuatBan.Clear();
        nudNamXuatBan.Value = 0;
        nudSoLuong.Value = 1;
        nudSoLuongCon.Value = 1;
        dgvDauSach.ClearSelection();
    }

    private void ShowSelectMessage() =>
        MessageBox.Show("Vui lòng chọn đầu sách trong danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

    private static string? NullIfEmpty(string value) => string.IsNullOrWhiteSpace(value) ? null : value.Trim();

    private static void ShowError(Exception ex) =>
        MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
}
