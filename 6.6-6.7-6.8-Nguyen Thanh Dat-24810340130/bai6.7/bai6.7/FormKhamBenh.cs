using bai6._7.BUS;
using bai6._7.DTO;
using System.Globalization;

namespace bai6._7;

public partial class FormKhamBenh : Form
{
    private readonly BenhNhanBUS _benhNhanBUS = new();
    private readonly DonThuocBUS _donThuocBUS = new();
    private readonly ThanhToanBUS _thanhToanBUS = new();
    private readonly List<ChiTietDonThuocDTO> _chiTietDonThuoc = [];
    private int? _maBN;
    private int? _maHoSo;
    private bool _donDaLuu;
    private bool _daThanhToan;

    public FormKhamBenh()
    {
        InitializeComponent();
        btnTimKiem.Click += BtnTimKiem_Click;
        btnQuanLyBenhNhan.Click += (_, _) =>
        {
            using var form = new FormQuanLyBenhNhan();
            form.ShowDialog(this);
        };
        btnQuanLyThuoc.Click += (_, _) =>
        {
            using var form = new FormQuanLyThuoc();
            form.ShowDialog(this);
            RefreshMedicineList();
        };
        btnLichSuKham.Click += (_, _) =>
        {
            using var form = new FormLichSuKham();
            form.ShowDialog(this);
        };
        btnTaoHoSo.Click += BtnTaoHoSo_Click;
        btnThemThuoc.Click += BtnThemThuoc_Click;
        btnXoaThuoc.Click += BtnXoaThuoc_Click;
        btnLuuXuatDon.Click += BtnLuuXuatDon_Click;
        btnThanhToan.Click += BtnThanhToan_Click;
        nudTienKham.ValueChanged += (_, _) => RefreshPrescriptionGrid();
        Load += FormKhamBenh_Load;
        UpdateControls();
    }

    private void FormKhamBenh_Load(object? sender, EventArgs e)
    {
        RefreshMedicineList();
    }

    private void RefreshMedicineList()
    {
        try
        {
            cboThuoc.DataSource = _donThuocBUS.LayDanhSachThuoc().ToList();
            cboThuoc.DisplayMember = nameof(ThuocDTO.TenThuoc);
            cboThuoc.ValueMember = nameof(ThuocDTO.MaThuoc);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Không thể tải danh mục thuốc. Hãy kiểm tra kết nối CSDL.\n{ex.Message}",
                "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnTimKiem_Click(object? sender, EventArgs e)
    {
        try
        {
            BenhNhanDTO? patient = _benhNhanBUS.TimTheoSoDienThoai(txtSoDienThoai.Text);
            ResetVisit();
            _maBN = patient?.MaBN;
            txtHoTen.Text = patient?.HoTen ?? string.Empty;
            txtHoTen.ReadOnly = patient is not null;
            MessageBox.Show(patient is null
                    ? "Không tìm thấy bệnh nhân. Nhập họ tên để tạo bệnh nhân mới."
                    : $"Đã tìm thấy bệnh nhân: {patient.HoTen}.",
                "Tra cứu bệnh nhân", MessageBoxButtons.OK,
                patient is null ? MessageBoxIcon.Information : MessageBoxIcon.Information);
            UpdateControls();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Thông tin chưa hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void BtnTaoHoSo_Click(object? sender, EventArgs e)
    {
        try
        {
            string diagnosis = txtChanDoan.Text.Trim();
            decimal examinationFee = nudTienKham.Value;
            if (_maBN.HasValue)
            {
                _maHoSo = _benhNhanBUS.TaoHoSoKham(_maBN.Value, diagnosis, examinationFee);
            }
            else
            {
                var patient = new BenhNhanDTO
                {
                    HoTen = txtHoTen.Text.Trim(),
                    SoDienThoai = txtSoDienThoai.Text.Trim()
                };
                _maHoSo = _benhNhanBUS.TaoBenhNhanVaHoSo(patient, diagnosis, examinationFee);
                _maBN = null;
                txtHoTen.ReadOnly = true;
            }

            lblMaHoSo.Text = $"Mã hồ sơ: {_maHoSo.Value}";
            UpdateControls();
            MessageBox.Show("Đã tạo hồ sơ khám.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Không thể tạo hồ sơ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void BtnThemThuoc_Click(object? sender, EventArgs e)
    {
        if (_maHoSo is null)
        {
            MessageBox.Show("Vui lòng tạo hồ sơ khám trước khi kê đơn.", "Chưa có hồ sơ",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (cboThuoc.SelectedItem is not ThuocDTO medicine)
        {
            MessageBox.Show("Vui lòng chọn thuốc.", "Chưa chọn thuốc",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        int quantity = decimal.ToInt32(txtSoLuong.Value);
        ChiTietDonThuocDTO? currentLine = _chiTietDonThuoc.FirstOrDefault(item => item.MaThuoc == medicine.MaThuoc);
        int combinedQuantity = quantity + (currentLine?.SoLuong ?? 0);
        if (combinedQuantity > medicine.SoLuongTon)
        {
            MessageBox.Show($"Tồn kho hiện tại của {medicine.TenThuoc} là {medicine.SoLuongTon}.",
                "Không đủ tồn kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (currentLine is null)
        {
            _chiTietDonThuoc.Add(new ChiTietDonThuocDTO
            {
                MaThuoc = medicine.MaThuoc,
                TenThuoc = medicine.TenThuoc,
                DonViTinh = medicine.DonViTinh,
                SoLuong = quantity,
                DonGia = medicine.DonGia
            });
        }
        else
        {
            currentLine.SoLuong = combinedQuantity;
        }

        RefreshPrescriptionGrid();
    }

    private void BtnXoaThuoc_Click(object? sender, EventArgs e)
    {
        if (dgvDonThuoc.CurrentRow?.Cells[0].Value is int medicineId)
        {
            _chiTietDonThuoc.RemoveAll(item => item.MaThuoc == medicineId);
            RefreshPrescriptionGrid();
        }
    }

    private void BtnLuuXuatDon_Click(object? sender, EventArgs e)
    {
        if (_maHoSo is null)
        {
            MessageBox.Show("Vui lòng tạo hồ sơ khám trước.", "Chưa có hồ sơ",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            int prescriptionId = _donThuocBUS.LuuVaXuatDon(_maHoSo.Value, _chiTietDonThuoc);
            _donDaLuu = true;
            UpdateControls();
            MessageBox.Show($"Đã lưu đơn thuốc số {prescriptionId} và cập nhật tồn kho.",
                "Lưu đơn thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Không thể lưu đơn thuốc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void BtnThanhToan_Click(object? sender, EventArgs e)
    {
        if (_maHoSo is null)
        {
            MessageBox.Show("Vui lòng tạo hồ sơ khám trước khi thanh toán.", "Chưa có hồ sơ",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            decimal total = _thanhToanBUS.ThanhToanVaTinhTongTien(_maHoSo.Value);
            _daThanhToan = true;
            lblTongTien.Text = $"Tổng tiền: {total.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"))} ₫";
            UpdateControls();
            MessageBox.Show("Thanh toán thành công.", "Thanh toán", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Không thể thanh toán", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void RefreshPrescriptionGrid()
    {
        dgvDonThuoc.Rows.Clear();
        foreach (ChiTietDonThuocDTO item in _chiTietDonThuoc)
        {
            dgvDonThuoc.Rows.Add(item.MaThuoc, item.TenThuoc, item.DonViTinh, item.SoLuong,
                item.DonGia.ToString("N0", CultureInfo.GetCultureInfo("vi-VN")),
                item.ThanhTien.ToString("N0", CultureInfo.GetCultureInfo("vi-VN")));
        }

        decimal total = nudTienKham.Value + _chiTietDonThuoc.Sum(item => item.ThanhTien);
        lblTongTien.Text = $"Tổng tiền: {total.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"))} ₫";
    }

    private void ResetVisit()
    {
        _maHoSo = null;
        _chiTietDonThuoc.Clear();
        _donDaLuu = false;
        _daThanhToan = false;
        dgvDonThuoc.Rows.Clear();
        lblMaHoSo.Text = "Chưa tạo hồ sơ khám";
        lblTongTien.Text = "Tổng tiền: 0 ₫";
        txtChanDoan.Clear();
    }

    private void UpdateControls()
    {
        bool visitExists = _maHoSo.HasValue;
        bool editable = visitExists && !_donDaLuu && !_daThanhToan;
        btnTaoHoSo.Enabled = !visitExists;
        btnThemThuoc.Enabled = editable;
        txtSoLuong.Enabled = editable;
        cboThuoc.Enabled = editable;
        btnXoaThuoc.Enabled = editable;
        btnLuuXuatDon.Enabled = editable;
        btnThanhToan.Enabled = visitExists && !_daThanhToan;
    }
}
