using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace bai4._11
{
    public partial class Form1 : Form
    {
        private readonly BindingList<RepairOrder> _orders = [];
        private readonly BindingSource _bindingSource;
        private readonly ErrorProvider _errorProvider;
        private List<RepairOrder> _visibleOrders = [];
        private RepairOrder? _selectedOrder;
        private string? _selectedImagePath;
        private bool _refreshingGrid;

        public Form1()
        {
            InitializeComponent();
            components ??= new System.ComponentModel.Container();
            ConfigureOrdersGrid();

            _bindingSource = new BindingSource(components);
            _errorProvider = new ErrorProvider(components)
            {
                ContainerControl = this,
                BlinkStyle = ErrorBlinkStyle.NeverBlink
            };
            estimatedCostNumericUpDown.ValueChanged += estimatedCostNumericUpDown_ValueChanged;

            deviceTypeComboBox.Items.AddRange(["Điện thoại", "Máy tính bảng", "Laptop", "Máy tính để bàn", "Khác"]);
            orderStatusComboBox.Items.AddRange(RepairStatuses.All);
            filterStatusComboBox.Items.Add("Tất cả trạng thái");
            filterStatusComboBox.Items.AddRange(RepairStatuses.All);

            deviceTypeComboBox.SelectedIndex = 0;
            orderStatusComboBox.SelectedItem = RepairStatuses.New;
            filterStatusComboBox.SelectedIndex = 0;

            _bindingSource.DataSource = _orders;
            ordersDataGridView.DataSource = _bindingSource;
            _orders.ListChanged += (_, _) => UpdateStatusStrip();
            FormClosed += (_, _) => deviceImagePictureBox.Image?.Dispose();
            ApplyFilters();
        }

        private void ConfigureOrdersGrid()
        {
            ordersDataGridView.AutoGenerateColumns = false;
            ordersDataGridView.Columns.Clear();
            ordersDataGridView.Columns.AddRange(
            [
                CreateTextColumn("OrderId", "Mã đơn"),
                CreateTextColumn("CustomerName", "Tên khách hàng"),
                CreateTextColumn("PhoneNumber", "Số điện thoại"),
                CreateTextColumn("DeviceType", "Loại thiết bị"),
                CreateTextColumn("ProblemDescription", "Mô tả lỗi"),
                CreateTextColumn("ReceivedDate", "Ngày tiếp nhận", "d"),
                CreateTextColumn("EstimatedCost", "Chi phí dự kiến", "N0"),
                CreateTextColumn("Status", "Trạng thái")
            ]);
        }

        private static DataGridViewTextBoxColumn CreateTextColumn(string propertyName, string headerText, string? format = null)
        {
            var column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = propertyName,
                HeaderText = headerText,
                Name = $"{propertyName}Column",
                ReadOnly = true,
                SortMode = DataGridViewColumnSortMode.Automatic
            };

            if (format is not null)
            {
                column.DefaultCellStyle.Format = format;
            }

            return column;
        }

        private void addButton_Click(object? sender, EventArgs e)
        {
            SaveOrder(update: false);
        }

        private void updateButton_Click(object? sender, EventArgs e)
        {
            SaveOrder(update: true);
        }

        private void SaveOrder(bool update)
        {
            if (update && _selectedOrder is null)
            {
                MessageBox.Show("Vui lòng chọn đơn hàng cần cập nhật trong danh sách.", "Chưa chọn đơn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!ValidateOrder(update))
            {
                MessageBox.Show(
                    "Vui lòng sửa các trường đang được đánh dấu lỗi trước khi lưu.",
                    "Dữ liệu chưa hợp lệ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var id = orderIdTextBox.Text.Trim();
            var order = update ? _selectedOrder! : new RepairOrder();
            order.OrderId = id;
            order.CustomerName = customerNameTextBox.Text.Trim();
            order.PhoneNumber = phoneNumberTextBox.Text.Trim();
            order.DeviceType = deviceTypeComboBox.SelectedItem!.ToString()!;
            order.ProblemDescription = problemDescriptionTextBox.Text.Trim();
            order.ReceivedDate = receivedDatePicker.Value.Date;
            order.EstimatedCost = estimatedCostNumericUpDown.Value;
            order.Status = orderStatusComboBox.SelectedItem!.ToString()!;
            order.ImagePath = _selectedImagePath;

            if (!update)
            {
                _orders.Add(order);
            }

            ApplyFilters();
            ClearForm();
            MessageBox.Show(
                update ? $"Đã cập nhật đơn hàng '{id}'." : $"Đã thêm đơn hàng '{id}'.",
                update ? "Cập nhật thành công" : "Thêm thành công",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private bool ValidateOrder(bool update)
        {
            _errorProvider.Clear();
            var isValid = true;
            var id = orderIdTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(id))
            {
                _errorProvider.SetError(orderIdTextBox, "Mã đơn không được để trống.");
                isValid = false;
            }
            else if (_orders.Any(order => !ReferenceEquals(order, update ? _selectedOrder : null)
                                         && string.Equals(order.OrderId, id, StringComparison.OrdinalIgnoreCase)))
            {
                _errorProvider.SetError(orderIdTextBox, "Mã đơn đã tồn tại.");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(customerNameTextBox.Text))
            {
                _errorProvider.SetError(customerNameTextBox, "Tên khách hàng không được để trống.");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(phoneNumberTextBox.Text))
            {
                _errorProvider.SetError(phoneNumberTextBox, "Số điện thoại không được để trống.");
                isValid = false;
            }

            if (deviceTypeComboBox.SelectedIndex < 0)
            {
                _errorProvider.SetError(deviceTypeComboBox, "Vui lòng chọn loại thiết bị.");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(problemDescriptionTextBox.Text))
            {
                _errorProvider.SetError(problemDescriptionTextBox, "Mô tả lỗi không được để trống.");
                isValid = false;
            }

            if (estimatedCostNumericUpDown.Value < 0)
            {
                _errorProvider.SetError(estimatedCostNumericUpDown, "Chi phí dự kiến phải là số không âm.");
                isValid = false;
            }

            if (orderStatusComboBox.SelectedIndex < 0)
            {
                _errorProvider.SetError(orderStatusComboBox, "Vui lòng chọn trạng thái.");
                isValid = false;
            }

            return isValid;
        }

        private void deleteButton_Click(object? sender, EventArgs e)
        {
            if (_selectedOrder is null)
            {
                MessageBox.Show("Vui lòng chọn đơn hàng cần xóa trong danh sách.", "Chưa chọn đơn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var deletedOrderId = _selectedOrder.OrderId;
            var answer = MessageBox.Show(
                $"Bạn có chắc muốn xóa đơn hàng '{deletedOrderId}' không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (answer != DialogResult.Yes)
            {
                return;
            }

            _orders.Remove(_selectedOrder);
            ApplyFilters();
            ClearForm();
            MessageBox.Show(
                $"Đã xóa đơn hàng '{deletedOrderId}'.",
                "Xóa thành công",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void clearButton_Click(object? sender, EventArgs e)
        {
            ClearForm();
        }

        private void estimatedCostNumericUpDown_ValueChanged(object? sender, EventArgs e)
        {
            _errorProvider.SetError(
                estimatedCostNumericUpDown,
                estimatedCostNumericUpDown.Value < 0 ? "Chi phí dự kiến không được là số âm." : string.Empty);
        }

        private void ClearForm()
        {
            _selectedOrder = null;
            _errorProvider.Clear();
            orderIdTextBox.Clear();
            customerNameTextBox.Clear();
            phoneNumberTextBox.Clear();
            deviceTypeComboBox.SelectedIndex = 0;
            problemDescriptionTextBox.Clear();
            receivedDatePicker.Value = DateTime.Today;
            estimatedCostNumericUpDown.Value = 0;
            orderStatusComboBox.SelectedItem = RepairStatuses.New;
            _selectedImagePath = null;
            SetDeviceImage(null);

            _refreshingGrid = true;
            ordersDataGridView.ClearSelection();
            ordersDataGridView.CurrentCell = null;
            _refreshingGrid = false;
            orderIdTextBox.Focus();
        }

        private void ordersDataGridView_SelectionChanged(object? sender, EventArgs e)
        {
            if (_refreshingGrid || ordersDataGridView.SelectedRows.Count == 0)
            {
                return;
            }

            if (ordersDataGridView.SelectedRows[0].DataBoundItem is not RepairOrder order)
            {
                return;
            }

            _selectedOrder = order;
            _errorProvider.Clear();
            orderIdTextBox.Text = order.OrderId;
            customerNameTextBox.Text = order.CustomerName;
            phoneNumberTextBox.Text = order.PhoneNumber;
            deviceTypeComboBox.SelectedItem = order.DeviceType;
            problemDescriptionTextBox.Text = order.ProblemDescription;
            receivedDatePicker.Value = order.ReceivedDate;
            estimatedCostNumericUpDown.Value = Math.Clamp(order.EstimatedCost, estimatedCostNumericUpDown.Minimum, estimatedCostNumericUpDown.Maximum);
            orderStatusComboBox.SelectedItem = order.Status;
            _selectedImagePath = order.ImagePath;
            SetDeviceImage(order.ImagePath);
        }

        private void searchTextBox_TextChanged(object? sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void filterStatusComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            var search = searchTextBox.Text.Trim();
            var selectedStatus = filterStatusComboBox.SelectedItem?.ToString();

            _visibleOrders = _orders.Where(order =>
                    (string.IsNullOrEmpty(search)
                     || order.OrderId.Contains(search, StringComparison.OrdinalIgnoreCase)
                     || order.CustomerName.Contains(search, StringComparison.OrdinalIgnoreCase)
                     || order.PhoneNumber.Contains(search, StringComparison.OrdinalIgnoreCase))
                    && (string.IsNullOrEmpty(selectedStatus)
                        || selectedStatus == "Tất cả trạng thái"
                        || order.Status == selectedStatus))
                .ToList();

            _refreshingGrid = true;
            _selectedOrder = null;
            _bindingSource.DataSource = new BindingList<RepairOrder>(_visibleOrders);
            ordersDataGridView.ClearSelection();
            ordersDataGridView.CurrentCell = null;
            _refreshingGrid = false;
            UpdateStatusStrip();
        }

        private void UpdateStatusStrip()
        {
            filteredCountStatusLabel.Text = $"Số đơn đang hiển thị: {_visibleOrders.Count}";
            var total = _orders.Where(order => order.Status != RepairStatuses.Completed).Sum(order => order.EstimatedCost);
            estimatedTotalStatusLabel.Text = $"Tổng chi phí chưa hoàn tất: {total.ToString("N0", CultureInfo.CurrentCulture)} ₫";
        }

        private void SetDeviceImage(string? imagePath)
        {
            var previousImage = deviceImagePictureBox.Image;
            deviceImagePictureBox.Image = null;
            previousImage?.Dispose();

            if (string.IsNullOrWhiteSpace(imagePath) || !File.Exists(imagePath))
            {
                return;
            }

            try
            {
                using var source = Image.FromFile(imagePath);
                deviceImagePictureBox.Image = new Bitmap(source);
            }
            catch (Exception exception) when (exception is IOException or ArgumentException or OutOfMemoryException)
            {
                MessageBox.Show("Không thể mở ảnh đã chọn.", "Lỗi ảnh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void chooseImageButton_Click(object? sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog
            {
                Title = "Chọn ảnh thiết bị",
                Filter = "Tệp ảnh|*.jpg;*.jpeg;*.png;*.bmp;*.gif|Tất cả tệp|*.*",
                CheckFileExists = true,
                Multiselect = false
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            _selectedImagePath = dialog.FileName;
            SetDeviceImage(_selectedImagePath);
        }

        private void exportCsvMenuItem_Click(object? sender, EventArgs e)
        {
            using var dialog = new SaveFileDialog
            {
                Title = "Xuất danh sách đơn sửa chữa",
                Filter = "CSV (*.csv)|*.csv",
                DefaultExt = "csv",
                AddExtension = true,
                FileName = "DanhSachDonSuaChua.csv"
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            var csv = new StringBuilder();
            csv.AppendLine("Mã đơn,Tên khách hàng,Số điện thoại,Loại thiết bị,Mô tả lỗi,Ngày tiếp nhận,Chi phí dự kiến,Trạng thái");
            foreach (var order in _visibleOrders)
            {
                csv.AppendLine(string.Join(",", new[]
                {
                    CsvField(order.OrderId),
                    CsvField(order.CustomerName),
                    CsvField(order.PhoneNumber),
                    CsvField(order.DeviceType),
                    CsvField(order.ProblemDescription),
                    CsvField(order.ReceivedDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)),
                    CsvField(order.EstimatedCost.ToString(CultureInfo.InvariantCulture)),
                    CsvField(order.Status)
                }));
            }

            try
            {
                File.WriteAllText(dialog.FileName, csv.ToString(), new UTF8Encoding(true));
                MessageBox.Show("Đã xuất danh sách đang hiển thị ra CSV.", "Xuất thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (IOException exception)
            {
                MessageBox.Show($"Không thể ghi tệp CSV: {exception.Message}", "Lỗi xuất tệp", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (UnauthorizedAccessException exception)
            {
                MessageBox.Show($"Không có quyền ghi tệp CSV: {exception.Message}", "Lỗi xuất tệp", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string CsvField(string value)
        {
            return $"\"{value.Replace("\"", "\"\"", StringComparison.Ordinal)}\"";
        }

        private void exitMenuItem_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private void orderTableLayoutPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}