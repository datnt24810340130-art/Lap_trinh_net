using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bai4._10
{
    public partial class Form1 : Form
    {
        private readonly BindingList<CheckoutRecord> _records = new();
        private readonly BindingList<CheckoutRecord> _visibleRecords = new();

        private CheckoutRecord? _selectedRecord;

        public Form1()
        {
            InitializeComponent();

            _bindingSource.DataSource = _visibleRecords;

            WireRecordActions();
            RefreshView();
        }

        private void WireRecordActions()
        {
            _addButton.Click += (_, _) => AddRecord();
            _updateButton.Click += (_, _) => UpdateRecord();
            _deleteButton.Click += (_, _) => DeleteRecord();
            _clearButton.Click += (_, _) => ClearForm();
            _returnButton.Click += (_, _) => ReturnRecord();
            _exportButton.Click += (_, _) => ExportFilteredRecords();

            _searchTextBox.TextChanged += (_, _) => RefreshView();

            _statusFilterComboBox.SelectedIndexChanged +=
                (_, _) => RefreshView();

            _recordsGrid.SelectionChanged += (_, _) =>
            {
                if (_recordsGrid.CurrentRow is { Selected: true, DataBoundItem: CheckoutRecord record })
                {
                    LoadRecordToForm(record);
                }
                else
                {
                    _selectedRecord = null;
                }
            };

            _borrowDatePicker.ValueChanged += (_, _) => ValidateDateInputs();
            _dueDatePicker.ValueChanged += (_, _) => ValidateDateInputs();
            _returnDatePicker.ValueChanged += (_, _) => ValidateDateInputs();
        }

        private void AddRecord()
        {
            if (!ValidateInput(isUpdate: false))
                return;

            string checkoutId = _checkoutIdTextBox.Text.Trim();

            var record = new CheckoutRecord
            {
                CheckoutId = checkoutId,
                StudentId = _studentIdTextBox.Text.Trim(),
                StudentName = _studentNameTextBox.Text.Trim(),
                Equipment = _equipmentComboBox.Text.Trim(),
                Quantity = (int)_quantityNumeric.Value,
                BorrowDate = _borrowDatePicker.Value.Date,
                DueDate = _dueDatePicker.Value.Date,
                ActualReturnDate = _returnDatePicker.Checked
                    ? _returnDatePicker.Value.Date
                    : null
            };

            _records.Add(record);

            RefreshView();
            ClearForm();

            MessageBox.Show(
                "Thêm phiếu mượn thành công!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void UpdateRecord()
        {
            if (_selectedRecord == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn phiếu cần sửa trong bảng.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (!ValidateInput(isUpdate: true))
                return;

            string checkoutId = _checkoutIdTextBox.Text.Trim();

            _selectedRecord.CheckoutId = checkoutId;
            _selectedRecord.StudentId = _studentIdTextBox.Text.Trim();
            _selectedRecord.StudentName = _studentNameTextBox.Text.Trim();
            _selectedRecord.Equipment = _equipmentComboBox.Text.Trim();
            _selectedRecord.Quantity = (int)_quantityNumeric.Value;
            _selectedRecord.BorrowDate = _borrowDatePicker.Value.Date;
            _selectedRecord.DueDate = _dueDatePicker.Value.Date;
            _selectedRecord.ActualReturnDate = _returnDatePicker.Checked
                ? _returnDatePicker.Value.Date
                : null;

            RefreshView();
            ClearForm();

            MessageBox.Show(
                "Cập nhật phiếu mượn thành công!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void DeleteRecord()
        {
            if (_selectedRecord == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn phiếu cần xóa trong bảng.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc muốn xóa phiếu {_selectedRecord.CheckoutId}?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            _records.Remove(_selectedRecord);
            _selectedRecord = null;

            RefreshView();
            ClearForm();

            MessageBox.Show(
                "Đã xóa phiếu mượn.",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void ReturnRecord()
        {
            if (_selectedRecord == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn phiếu cần trả trong bảng.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (_selectedRecord.ActualReturnDate.HasValue)
            {
                MessageBox.Show(
                    "Phiếu này đã được trả thiết bị.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            if (_selectedRecord.BorrowDate.Date > DateTime.Today)
            {
                const string message = "Không thể trả thiết bị trước ngày mượn.";
                _errorProvider.SetError(
                    _returnDatePicker,
                    message);
                MessageBox.Show(
                    this,
                    message,
                    "Ngày trả không hợp lệ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                _returnDatePicker.Focus();
                return;
            }

            var returnedRecord = _selectedRecord;
            returnedRecord.ActualReturnDate = DateTime.Today;
            _returnDatePicker.Value = DateTime.Today;
            _returnDatePicker.Checked = true;

            RefreshView();
            if (_visibleRecords.Contains(returnedRecord))
                LoadRecordToForm(returnedRecord);

            MessageBox.Show(
                "Đã xác nhận trả thiết bị.",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private bool ValidateInput(bool isUpdate)
        {
            _errorProvider.Clear();
            bool valid = true;

            if (string.IsNullOrWhiteSpace(_checkoutIdTextBox.Text))
            {
                SetValidationError(_checkoutIdTextBox, "Vui lòng nhập mã phiếu.", ref valid);
            }
            else if (_records.Any(record =>
                (!isUpdate || !ReferenceEquals(record, _selectedRecord)) &&
                string.Equals(record.CheckoutId, _checkoutIdTextBox.Text.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                SetValidationError(_checkoutIdTextBox, "Mã phiếu đã tồn tại.", ref valid);
            }

            if (string.IsNullOrWhiteSpace(_studentIdTextBox.Text))
                SetValidationError(_studentIdTextBox, "Vui lòng nhập mã sinh viên.", ref valid);
            if (string.IsNullOrWhiteSpace(_studentNameTextBox.Text))
                SetValidationError(_studentNameTextBox, "Vui lòng nhập họ tên sinh viên.", ref valid);

            if (string.IsNullOrWhiteSpace(_equipmentComboBox.Text))
                SetValidationError(_equipmentComboBox, "Vui lòng chọn thiết bị.", ref valid);

            if (_quantityNumeric.Value < 1)
                SetValidationError(_quantityNumeric, "Số lượng phải lớn hơn 0.", ref valid);

            if (_dueDatePicker.Value.Date < _borrowDatePicker.Value.Date)
                SetValidationError(_dueDatePicker, "Hạn trả không được trước ngày mượn.", ref valid);

            if (_returnDatePicker.Checked &&
                _returnDatePicker.Value.Date < _borrowDatePicker.Value.Date)
            {
                SetValidationError(_returnDatePicker, "Ngày trả thực tế không được trước ngày mượn.", ref valid);
            }

            if (!valid)
            {
                Control[] invalidControls = new Control[]
                {
                    _checkoutIdTextBox,
                    _studentIdTextBox,
                    _studentNameTextBox,
                    _equipmentComboBox,
                    _quantityNumeric,
                    _dueDatePicker,
                    _returnDatePicker
                }.Where(control => !string.IsNullOrEmpty(_errorProvider.GetError(control))).ToArray();

                string errorMessage = string.Join(
                    Environment.NewLine,
                    invalidControls.Select(control => _errorProvider.GetError(control)).Distinct());

                MessageBox.Show(
                    this,
                    errorMessage,
                    "Dữ liệu chưa hợp lệ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                Control? firstInvalid = invalidControls.FirstOrDefault();
                firstInvalid?.Focus();
            }

            return valid;
        }

        private void SetValidationError(Control control, string message, ref bool valid)
        {
            _errorProvider.SetError(control, message);
            valid = false;
        }

        private void ValidateDateInputs()
        {
            _errorProvider.SetError(
                _dueDatePicker,
                _dueDatePicker.Value.Date < _borrowDatePicker.Value.Date
                    ? "Hạn trả không được trước ngày mượn."
                    : string.Empty);

            _errorProvider.SetError(
                _returnDatePicker,
                _returnDatePicker.Checked && _returnDatePicker.Value.Date < _borrowDatePicker.Value.Date
                    ? "Ngày trả thực tế không được trước ngày mượn."
                    : string.Empty);
        }

        private void ClearForm()
        {
            _errorProvider.Clear();
            _checkoutIdTextBox.Clear();
            _studentIdTextBox.Clear();
            _studentNameTextBox.Clear();
            _equipmentComboBox.SelectedIndex = -1;
            _equipmentComboBox.Text = "";
            _quantityNumeric.Value = 1;

            _borrowDatePicker.Value = DateTime.Today;
            _dueDatePicker.Value = DateTime.Today.AddDays(7);

            _returnDatePicker.Value = DateTime.Today;
            _returnDatePicker.Checked = false;

            _selectedRecord = null;
            _recordsGrid.ClearSelection();

            _statusLabel.Text = "Đã làm mới biểu mẫu";

            _checkoutIdTextBox.Focus();
        }

        private void LoadRecordToForm(CheckoutRecord record)
        {
            _selectedRecord = record;
            _errorProvider.Clear();

            _checkoutIdTextBox.Text = record.CheckoutId;
            _studentIdTextBox.Text = record.StudentId;
            _studentNameTextBox.Text = record.StudentName;
            _equipmentComboBox.Text = record.Equipment;

            _quantityNumeric.Value = Math.Max(
                _quantityNumeric.Minimum,
                Math.Min(_quantityNumeric.Maximum, record.Quantity));

            _borrowDatePicker.Value = record.BorrowDate;
            _dueDatePicker.Value = record.DueDate;

            if (record.ActualReturnDate.HasValue)
            {
                _returnDatePicker.Value = record.ActualReturnDate.Value;
                _returnDatePicker.Checked = true;
            }
            else
            {
                _returnDatePicker.Value = DateTime.Today;
                _returnDatePicker.Checked = false;
            }

            _statusLabel.Text =
                $"Đang chỉnh sửa phiếu: {record.CheckoutId}";
        }

        private void RefreshView()
        {
            string keyword = _searchTextBox.Text.Trim();

            string selectedStatus =
                _statusFilterComboBox.SelectedItem?.ToString() ?? "Tất cả";

            var filteredRecords = _records.Where(record =>
            {
                bool matchesKeyword =
                    string.IsNullOrWhiteSpace(keyword) ||
                    record.StudentId.Contains(
                        keyword,
                        StringComparison.OrdinalIgnoreCase) ||
                    record.Equipment.Contains(
                        keyword,
                        StringComparison.OrdinalIgnoreCase) ||
                    record.CheckoutId.Contains(
                        keyword,
                        StringComparison.OrdinalIgnoreCase) ||
                    record.StudentName.Contains(
                        keyword,
                        StringComparison.OrdinalIgnoreCase);

                bool matchesStatus = selectedStatus == "Tất cả" ||
                    record.Status.Equals(
                        selectedStatus,
                        StringComparison.OrdinalIgnoreCase);

                return matchesKeyword && matchesStatus;
            }).ToList();

            _visibleRecords.RaiseListChangedEvents = false;
            _visibleRecords.Clear();

            foreach (CheckoutRecord record in filteredRecords)
            {
                _visibleRecords.Add(record);
            }

            _visibleRecords.RaiseListChangedEvents = true;
            _visibleRecords.ResetBindings();

            _summaryStatusLabel.Text =
                $"Tổng số phiếu: {_records.Count} | Quá hạn: {_records.Count(record => record.Status == "Quá hạn")}";

            _totalLabel.Text = $"Tổng phiếu: {_records.Count}";

            _borrowedLabel.Text =
                $"Đang mượn: {_records.Count(r => r.Status == "Đang mượn")}";

            _overdueLabel.Text =
                $"Quá hạn: {_records.Count(r => r.Status == "Quá hạn")}";

            _returnedLabel.Text =
                $"Đã trả: {_records.Count(r => r.Status == "Đã trả")}";

            _statusLabel.Text =
                $"Đang hiển thị {filteredRecords.Count} / {_records.Count} phiếu";
        }

        private void ExportFilteredRecords()
        {
            using var dialog = new SaveFileDialog
            {
                Title = "Xuất danh sách phiếu mượn",
                Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
                DefaultExt = "csv",
                AddExtension = true,
                FileName = "lab-equipment-checkouts.csv"
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
                return;

            try
            {
                using var writer = new StreamWriter(
                    dialog.FileName,
                    false,
                    new UTF8Encoding(encoderShouldEmitUTF8Identifier: true));

                writer.WriteLine(CsvLine(
                    "Mã phiếu", "Mã sinh viên", "Họ tên sinh viên", "Thiết bị",
                    "Số lượng", "Ngày mượn", "Hạn trả", "Ngày trả thực tế", "Tình trạng"));

                foreach (var record in _visibleRecords)
                {
                    writer.WriteLine(CsvLine(
                        record.CheckoutId,
                        record.StudentId,
                        record.StudentName,
                        record.Equipment,
                        record.Quantity.ToString(CultureInfo.InvariantCulture),
                        record.BorrowDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                        record.DueDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                        record.ActualReturnDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) ?? string.Empty,
                        record.Status));
                }

                MessageBox.Show(
                    this,
                    $"Đã xuất {_visibleRecords.Count} phiếu.",
                    "Xuất CSV thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception exception) when (exception is IOException or UnauthorizedAccessException)
            {
                MessageBox.Show(
                    this,
                    $"Không thể ghi tệp CSV: {exception.Message}",
                    "Lỗi xuất dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private static string CsvLine(params string[] values) =>
            string.Join(",", values.Select(value => $"\"{value.Replace("\"", "\"\"")}\""));
    }
}