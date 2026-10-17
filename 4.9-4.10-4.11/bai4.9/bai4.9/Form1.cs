using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace bai4._9
{
    public partial class Form1 : Form
    {
        private static readonly string[] AppointmentStatuses = { "Chờ khám", "Đang khám", "Hoàn tất", "Đã hủy" };
        private static readonly string[] DoctorNames = { "Nguyễn Thành Đạt", "Trần Thanh Bình", "Lê Thảo Chi" };

        private readonly BindingList<Appointment> appointments = new();
        private readonly BindingList<Appointment> filteredAppointments = new();
        private readonly BindingSource appointmentSource = new();
        private readonly ErrorProvider errorProvider = new();
        private Appointment? selectedAppointment;
        private bool isRefreshing;
        private bool isLoadingForm;

        private readonly TextBox appointmentIdTextBox = new();
        private readonly TextBox patientNameTextBox = new();
        private readonly TextBox phoneNumberTextBox = new();
        private readonly DateTimePicker appointmentDatePicker = new();
        private readonly ComboBox doctorComboBox = new();
        private readonly TextBox reasonTextBox = new();
        private readonly ComboBox statusComboBox = new();
        private readonly TextBox searchTextBox = new();
        private readonly CheckBox filterDateCheckBox = new();
        private readonly DateTimePicker filterDatePicker = new();
        private readonly ComboBox filterStatusComboBox = new();
        private readonly DataGridView appointmentsGrid = new();
        private readonly ToolStripStatusLabel countStatusLabel = new();

        public Form1()
        {
            InitializeComponent();
            components.Add(appointmentSource);
            components.Add(errorProvider);
            InitializeUserInterface();
            ConfigureBindings();
            WireEvents();
        }

        private void InitializeUserInterface()
        {
            Text = "Quản lý lịch hẹn phòng khám";
            StartPosition = FormStartPosition.CenterScreen;
            MinimumSize = new Size(1000, 650);
            ClientSize = new Size(1250, 780);

            var menuStrip = new MenuStrip { Dock = DockStyle.Fill };
            var fileMenu = new ToolStripMenuItem("Tệp");
            var exportMenuItem = new ToolStripMenuItem("Xuất danh sách đang hiển thị ra CSV...");
            exportMenuItem.Click += ExportMenuItem_Click;
            fileMenu.DropDownItems.Add(exportMenuItem);
            menuStrip.Items.Add(fileMenu);
            MainMenuStrip = menuStrip;

            var statusStrip = new StatusStrip { Dock = DockStyle.Fill, SizingGrip = false };
            countStatusLabel.Text = "Số lịch hẹn: 0";
            statusStrip.Items.Add(countStatusLabel);

            var pageLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                Padding = new Padding(8)
            };
            pageLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            pageLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
            pageLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            pageLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 26));
            pageLayout.Controls.Add(menuStrip, 0, 0);
            pageLayout.Controls.Add(CreateMainLayout(), 0, 1);
            pageLayout.Controls.Add(statusStrip, 0, 2);
            Controls.Add(pageLayout);

            errorProvider.ContainerControl = this;
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        }

        private Control CreateMainLayout()
        {
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(0, 8, 0, 0)
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 62));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout.Controls.Add(CreateEntryPanel(), 0, 0);
            layout.Controls.Add(CreateAppointmentsPanel(), 1, 0);
            return layout;
        }

        private Control CreateEntryPanel()
        {
            var panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 9,
                Padding = new Padding(8)
            };
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 125));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 38));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 82));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var heading = new Label
            {
                Text = "Thông tin lịch hẹn",
                Dock = DockStyle.Fill,
                Font = new Font(Font, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };
            panel.Controls.Add(heading, 0, 0);
            panel.SetColumnSpan(heading, 2);

            ConfigureTextBox(appointmentIdTextBox);
            ConfigureTextBox(patientNameTextBox);
            ConfigureTextBox(phoneNumberTextBox);
            ConfigureTextBox(reasonTextBox);
            reasonTextBox.Multiline = true;
            reasonTextBox.ScrollBars = ScrollBars.Vertical;

            appointmentDatePicker.Dock = DockStyle.Fill;
            appointmentDatePicker.Format = DateTimePickerFormat.Custom;
            appointmentDatePicker.CustomFormat = "dd/MM/yyyy HH:mm";
            appointmentDatePicker.ShowUpDown = true;
            appointmentDatePicker.Value = DateTime.Now.AddMinutes(5);

            ConfigureComboBox(doctorComboBox, DoctorNames);
            ConfigureComboBox(statusComboBox, AppointmentStatuses);
            statusComboBox.SelectedIndex = 0;

            AddLabeledControl(panel, "Mã lịch hẹn:", appointmentIdTextBox, 1);
            AddLabeledControl(panel, "Họ tên bệnh nhân:", patientNameTextBox, 2);
            AddLabeledControl(panel, "Số điện thoại:", phoneNumberTextBox, 3);
            AddLabeledControl(panel, "Ngày giờ hẹn:", appointmentDatePicker, 4);
            AddLabeledControl(panel, "Bác sĩ:", doctorComboBox, 5);
            AddLabeledControl(panel, "Lý do khám:", reasonTextBox, 6);
            AddLabeledControl(panel, "Trạng thái:", statusComboBox, 7);

            var buttons = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                WrapContents = true,
                Padding = new Padding(0, 12, 0, 0)
            };
            var addButton = new Button { Text = "Thêm", AutoSize = true };
            var updateButton = new Button { Text = "Cập nhật", AutoSize = true };
            var deleteButton = new Button { Text = "Xóa", AutoSize = true };
            var clearButton = new Button { Text = "Làm mới", AutoSize = true };
            addButton.Click += AddButton_Click;
            updateButton.Click += UpdateButton_Click;
            deleteButton.Click += DeleteButton_Click;
            clearButton.Click += ClearButton_Click;
            buttons.Controls.AddRange(new Control[] { addButton, updateButton, deleteButton, clearButton });
            panel.Controls.Add(buttons, 0, 8);
            panel.SetColumnSpan(buttons, 2);
            return panel;
        }

        private Control CreateAppointmentsPanel()
        {
            var panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Padding = new Padding(8, 0, 0, 0)
            };
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 62));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var filters = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                WrapContents = true,
                AutoScroll = true,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(0, 6, 0, 0)
            };
            searchTextBox.Width = 190;
            searchTextBox.PlaceholderText = "Tên bệnh nhân hoặc điện thoại";
            filterDateCheckBox.Text = "Ngày:";
            filterDateCheckBox.AutoSize = true;
            filterDatePicker.Width = 125;
            filterDatePicker.Format = DateTimePickerFormat.Short;
            filterDatePicker.Enabled = false;
            ConfigureComboBox(filterStatusComboBox, new[] { "Tất cả trạng thái" }.Concat(AppointmentStatuses));
            filterStatusComboBox.Width = 145;
            filterStatusComboBox.SelectedIndex = 0;

            filters.Controls.AddRange(new Control[]
            {
                new Label { Text = "Tìm kiếm:", AutoSize = true, Padding = new Padding(0, 7, 0, 0) },
                searchTextBox,
                filterDateCheckBox,
                filterDatePicker,
                new Label { Text = "Trạng thái:", AutoSize = true, Padding = new Padding(0, 7, 0, 0) },
                filterStatusComboBox
            });
            panel.Controls.Add(filters, 0, 0);
            panel.Controls.Add(CreateAppointmentsGrid(), 0, 1);
            return panel;
        }

        private Control CreateAppointmentsGrid()
        {
            appointmentsGrid.Dock = DockStyle.Fill;
            appointmentsGrid.AutoGenerateColumns = false;
            appointmentsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            appointmentsGrid.MultiSelect = false;
            appointmentsGrid.ReadOnly = true;
            appointmentsGrid.AllowUserToAddRows = false;
            appointmentsGrid.AllowUserToDeleteRows = false;
            appointmentsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            appointmentsGrid.RowHeadersVisible = false;
            appointmentsGrid.Columns.Add(CreateTextColumn("AppointmentId", "Mã lịch hẹn", 85));
            appointmentsGrid.Columns.Add(CreateTextColumn("PatientName", "Bệnh nhân", 130));
            appointmentsGrid.Columns.Add(CreateTextColumn("PhoneNumber", "Điện thoại", 105));
            appointmentsGrid.Columns.Add(CreateTextColumn("AppointmentDate", "Ngày giờ hẹn", 125, "dd/MM/yyyy HH:mm"));
            appointmentsGrid.Columns.Add(CreateTextColumn("Doctor", "Bác sĩ", 120));
            appointmentsGrid.Columns.Add(CreateTextColumn("Reason", "Lý do khám", 140));
            appointmentsGrid.Columns.Add(CreateTextColumn("Status", "Trạng thái", 95));
            return appointmentsGrid;
        }

        private void ConfigureBindings()
        {
            appointmentSource.DataSource = filteredAppointments;
            appointmentsGrid.DataSource = appointmentSource;
        }

        private void WireEvents()
        {
            appointmentsGrid.SelectionChanged += AppointmentsGrid_SelectionChanged;
            searchTextBox.TextChanged += Filters_Changed;
            filterDateCheckBox.CheckedChanged += FilterDateCheckBox_CheckedChanged;
            filterDatePicker.ValueChanged += Filters_Changed;
            filterStatusComboBox.SelectedIndexChanged += Filters_Changed;
        }

        private static DataGridViewTextBoxColumn CreateTextColumn(string propertyName, string header, int minimumWidth, string? format = null)
        {
            var column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = propertyName,
                HeaderText = header,
                MinimumWidth = minimumWidth,
                FillWeight = minimumWidth,
                SortMode = DataGridViewColumnSortMode.Automatic
            };
            if (format is not null)
            {
                column.DefaultCellStyle.Format = format;
            }
            return column;
        }

        private static void ConfigureTextBox(TextBox textBox) => textBox.Dock = DockStyle.Fill;

        private static void ConfigureComboBox(ComboBox comboBox, IEnumerable<string> items)
        {
            comboBox.Dock = DockStyle.Fill;
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.Items.AddRange(items.Cast<object>().ToArray());
        }

        private static void AddLabeledControl(TableLayoutPanel panel, string text, Control control, int row)
        {
            panel.Controls.Add(new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                AutoEllipsis = true
            }, 0, row);
            panel.Controls.Add(control, 1, row);
        }

        private void AddButton_Click(object? sender, EventArgs e)
        {
            if (!ValidateAppointment())
            {
                return;
            }

            var appointment = ReadAppointment();
            if (appointments.Any(item => string.Equals(item.AppointmentId, appointment.AppointmentId, StringComparison.OrdinalIgnoreCase)))
            {
                errorProvider.SetError(appointmentIdTextBox, "Mã lịch hẹn đã tồn tại.");
                return;
            }

            if (MessageBox.Show(this, "Bạn có chắc muốn thêm lịch hẹn này?", "Xác nhận thêm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            appointments.Add(appointment);
            RefreshFilteredAppointments();
            ClearForm();
        }

        private void UpdateButton_Click(object? sender, EventArgs e)
        {
            if (selectedAppointment is null)
            {
                MessageBox.Show(this, "Vui lòng chọn lịch hẹn cần cập nhật.", "Chưa chọn lịch hẹn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!ValidateAppointment())
            {
                return;
            }

            var updatedAppointment = ReadAppointment();
            if (appointments.Any(item => !ReferenceEquals(item, selectedAppointment)
                && string.Equals(item.AppointmentId, updatedAppointment.AppointmentId, StringComparison.OrdinalIgnoreCase)))
            {
                errorProvider.SetError(appointmentIdTextBox, "Mã lịch hẹn đã tồn tại.");
                return;
            }

            if (MessageBox.Show(this, "Bạn có chắc muốn cập nhật lịch hẹn này?", "Xác nhận cập nhật", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            selectedAppointment.AppointmentId = updatedAppointment.AppointmentId;
            selectedAppointment.PatientName = updatedAppointment.PatientName;
            selectedAppointment.PhoneNumber = updatedAppointment.PhoneNumber;
            selectedAppointment.AppointmentDate = updatedAppointment.AppointmentDate;
            selectedAppointment.Doctor = updatedAppointment.Doctor;
            selectedAppointment.Reason = updatedAppointment.Reason;
            selectedAppointment.Status = updatedAppointment.Status;
            appointmentSource.ResetBindings(false);
            RefreshFilteredAppointments();
        }

        private void DeleteButton_Click(object? sender, EventArgs e)
        {
            if (selectedAppointment is null)
            {
                MessageBox.Show(this, "Vui lòng chọn lịch hẹn cần xóa.", "Chưa chọn lịch hẹn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show(this, $"Bạn có chắc muốn xóa lịch hẹn {selectedAppointment.AppointmentId}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            appointments.Remove(selectedAppointment);
            selectedAppointment = null;
            RefreshFilteredAppointments();
            ClearForm();
        }

        private void ClearButton_Click(object? sender, EventArgs e) => ClearForm();

        private void AppointmentsGrid_SelectionChanged(object? sender, EventArgs e)
        {
            if (isRefreshing || isLoadingForm || appointmentsGrid.SelectedRows.Count == 0
                || appointmentsGrid.SelectedRows[0].DataBoundItem is not Appointment appointment)
            {
                return;
            }

            selectedAppointment = appointment;
            isLoadingForm = true;
            try
            {
                appointmentIdTextBox.Text = appointment.AppointmentId;
                patientNameTextBox.Text = appointment.PatientName;
                phoneNumberTextBox.Text = appointment.PhoneNumber;
                appointmentDatePicker.Value = appointment.AppointmentDate;
                doctorComboBox.SelectedItem = appointment.Doctor;
                reasonTextBox.Text = appointment.Reason;
                statusComboBox.SelectedItem = appointment.Status;
                ClearValidationErrors();
            }
            finally
            {
                isLoadingForm = false;
            }
        }

        private void Filters_Changed(object? sender, EventArgs e) => RefreshFilteredAppointments();

        private void FilterDateCheckBox_CheckedChanged(object? sender, EventArgs e)
        {
            filterDatePicker.Enabled = filterDateCheckBox.Checked;
            RefreshFilteredAppointments();
        }

        private void RefreshFilteredAppointments()
        {
            var searchText = searchTextBox.Text.Trim();
            var selectedStatus = filterStatusComboBox.SelectedIndex > 0
                ? filterStatusComboBox.SelectedItem?.ToString()
                : null;

            var matches = appointments.Where(appointment =>
                (searchText.Length == 0
                    || appointment.PatientName.Contains(searchText, StringComparison.CurrentCultureIgnoreCase)
                    || appointment.PhoneNumber.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                && (!filterDateCheckBox.Checked || appointment.AppointmentDate.Date == filterDatePicker.Value.Date)
                && (selectedStatus is null || appointment.Status == selectedStatus))
                .ToList();

            isRefreshing = true;
            try
            {
                filteredAppointments.Clear();
                foreach (var appointment in matches)
                {
                    filteredAppointments.Add(appointment);
                }
                appointmentSource.ResetBindings(false);

                var selectedRow = appointmentsGrid.Rows
                    .Cast<DataGridViewRow>()
                    .FirstOrDefault(row => ReferenceEquals(row.DataBoundItem, selectedAppointment));
                if (selectedRow is not null)
                {
                    selectedRow.Selected = true;
                    appointmentsGrid.CurrentCell = selectedRow.Cells[0];
                }
                else
                {
                    if (selectedAppointment is not null && !matches.Contains(selectedAppointment))
                    {
                        selectedAppointment = null;
                    }
                    appointmentsGrid.ClearSelection();
                }

                countStatusLabel.Text = $"Số lịch hẹn: {matches.Count}";
            }
            finally
            {
                isRefreshing = false;
            }
        }

        private bool ValidateAppointment()
        {
            ClearValidationErrors();
            var isValid = true;

            if (string.IsNullOrWhiteSpace(appointmentIdTextBox.Text))
            {
                errorProvider.SetError(appointmentIdTextBox, "Vui lòng nhập mã lịch hẹn.");
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(patientNameTextBox.Text))
            {
                errorProvider.SetError(patientNameTextBox, "Vui lòng nhập họ tên bệnh nhân.");
                isValid = false;
            }

            var phone = phoneNumberTextBox.Text.Trim();
            var digits = Regex.Replace(phone, @"\D", string.Empty);
            if (!Regex.IsMatch(phone, @"^\+?[0-9\s().-]+$") || digits.Length is < 9 or > 15)
            {
                errorProvider.SetError(phoneNumberTextBox, "Số điện thoại phải có từ 9 đến 15 chữ số hợp lệ.");
                isValid = false;
            }
            if (appointmentDatePicker.Value < DateTime.Now)
            {
                errorProvider.SetError(appointmentDatePicker, "Ngày giờ hẹn không được nằm trong quá khứ.");
                isValid = false;
            }
            if (doctorComboBox.SelectedItem is null)
            {
                errorProvider.SetError(doctorComboBox, "Vui lòng chọn bác sĩ.");
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(reasonTextBox.Text))
            {
                errorProvider.SetError(reasonTextBox, "Vui lòng nhập lý do khám.");
                isValid = false;
            }
            if (statusComboBox.SelectedItem is null)
            {
                errorProvider.SetError(statusComboBox, "Vui lòng chọn trạng thái.");
                isValid = false;
            }

            return isValid;
        }

        private Appointment ReadAppointment() => new()
        {
            AppointmentId = appointmentIdTextBox.Text.Trim(),
            PatientName = patientNameTextBox.Text.Trim(),
            PhoneNumber = phoneNumberTextBox.Text.Trim(),
            AppointmentDate = appointmentDatePicker.Value,
            Doctor = doctorComboBox.SelectedItem!.ToString()!,
            Reason = reasonTextBox.Text.Trim(),
            Status = statusComboBox.SelectedItem!.ToString()!
        };

        private void ClearForm()
        {
            selectedAppointment = null;
            isLoadingForm = true;
            try
            {
                appointmentIdTextBox.Clear();
                patientNameTextBox.Clear();
                phoneNumberTextBox.Clear();
                appointmentDatePicker.Value = DateTime.Now.AddMinutes(5);
                doctorComboBox.SelectedIndex = -1;
                reasonTextBox.Clear();
                statusComboBox.SelectedIndex = 0;
                appointmentsGrid.ClearSelection();
                ClearValidationErrors();
            }
            finally
            {
                isLoadingForm = false;
            }
        }

        private void ClearValidationErrors()
        {
            errorProvider.SetError(appointmentIdTextBox, string.Empty);
            errorProvider.SetError(patientNameTextBox, string.Empty);
            errorProvider.SetError(phoneNumberTextBox, string.Empty);
            errorProvider.SetError(appointmentDatePicker, string.Empty);
            errorProvider.SetError(doctorComboBox, string.Empty);
            errorProvider.SetError(reasonTextBox, string.Empty);
            errorProvider.SetError(statusComboBox, string.Empty);
        }

        private void ExportMenuItem_Click(object? sender, EventArgs e)
        {
            using var dialog = new SaveFileDialog
            {
                Title = "Xuất danh sách lịch hẹn",
                Filter = "CSV (*.csv)|*.csv",
                DefaultExt = "csv",
                AddExtension = true,
                FileName = "lich-hen.csv"
            };
            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            var csv = new StringBuilder();
            csv.AppendLine("Mã lịch hẹn,Họ tên bệnh nhân,Số điện thoại,Ngày giờ hẹn,Bác sĩ,Lý do khám,Trạng thái");
            foreach (var appointment in filteredAppointments)
            {
                csv.AppendLine(string.Join(",", new[]
                {
                    appointment.AppointmentId,
                    appointment.PatientName,
                    appointment.PhoneNumber,
                    appointment.AppointmentDate.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                    appointment.Doctor,
                    appointment.Reason,
                    appointment.Status
                }.Select(EscapeCsv)));
            }

            try
            {
                File.WriteAllText(dialog.FileName, csv.ToString(), new UTF8Encoding(true));
            }
            catch (IOException exception)
            {
                MessageBox.Show(this, $"Không thể ghi tệp CSV: {exception.Message}", "Lỗi xuất tệp", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (UnauthorizedAccessException exception)
            {
                MessageBox.Show(this, $"Không có quyền ghi tệp: {exception.Message}", "Lỗi xuất tệp", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string EscapeCsv(string value) => $"\"{value.Replace("\"", "\"\"")}\"";
    }
}
