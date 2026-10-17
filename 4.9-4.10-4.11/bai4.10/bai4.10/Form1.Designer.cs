namespace bai4._10
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.SplitContainer _mainSplitContainer;
        private System.Windows.Forms.Panel _editorPanel;
        private System.Windows.Forms.DataGridView _recordsGrid;
        private System.Windows.Forms.BindingSource _bindingSource;
        private System.Windows.Forms.ErrorProvider _errorProvider;

        private System.Windows.Forms.TextBox _checkoutIdTextBox;
        private System.Windows.Forms.TextBox _studentIdTextBox;
        private System.Windows.Forms.TextBox _studentNameTextBox;
        private System.Windows.Forms.ComboBox _equipmentComboBox;
        private System.Windows.Forms.NumericUpDown _quantityNumeric;
        private System.Windows.Forms.DateTimePicker _borrowDatePicker;
        private System.Windows.Forms.DateTimePicker _dueDatePicker;
        private System.Windows.Forms.DateTimePicker _returnDatePicker;

        private System.Windows.Forms.TextBox _searchTextBox;
        private System.Windows.Forms.ComboBox _statusFilterComboBox;

        private System.Windows.Forms.Button _addButton;
        private System.Windows.Forms.Button _updateButton;
        private System.Windows.Forms.Button _deleteButton;
        private System.Windows.Forms.Button _clearButton;
        private System.Windows.Forms.Button _returnButton;
        private System.Windows.Forms.Button _exportButton;

        private System.Windows.Forms.Label _totalLabel;
        private System.Windows.Forms.Label _borrowedLabel;
        private System.Windows.Forms.Label _overdueLabel;
        private System.Windows.Forms.Label _returnedLabel;
        private System.Windows.Forms.Label _statusLabel;
        private System.Windows.Forms.StatusStrip _statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel _summaryStatusLabel;

        private System.Windows.Forms.DataGridViewTextBoxColumn _checkoutIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn _studentIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn _studentNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn _equipmentColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn _quantityColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn _borrowDateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn _dueDateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn _returnDateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn _statusColumn;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            _mainSplitContainer = new System.Windows.Forms.SplitContainer();
            _editorPanel = new System.Windows.Forms.Panel();
            _recordsGrid = new System.Windows.Forms.DataGridView();
            _bindingSource = new System.Windows.Forms.BindingSource(components);
            _errorProvider = new System.Windows.Forms.ErrorProvider(components);

            _checkoutIdTextBox = new System.Windows.Forms.TextBox();
            _studentIdTextBox = new System.Windows.Forms.TextBox();
            _studentNameTextBox = new System.Windows.Forms.TextBox();
            _equipmentComboBox = new System.Windows.Forms.ComboBox();
            _quantityNumeric = new System.Windows.Forms.NumericUpDown();
            _borrowDatePicker = new System.Windows.Forms.DateTimePicker();
            _dueDatePicker = new System.Windows.Forms.DateTimePicker();
            _returnDatePicker = new System.Windows.Forms.DateTimePicker();

            _searchTextBox = new System.Windows.Forms.TextBox();
            _statusFilterComboBox = new System.Windows.Forms.ComboBox();

            _addButton = new System.Windows.Forms.Button();
            _updateButton = new System.Windows.Forms.Button();
            _deleteButton = new System.Windows.Forms.Button();
            _clearButton = new System.Windows.Forms.Button();
            _returnButton = new System.Windows.Forms.Button();
            _exportButton = new System.Windows.Forms.Button();

            _totalLabel = new System.Windows.Forms.Label();
            _borrowedLabel = new System.Windows.Forms.Label();
            _overdueLabel = new System.Windows.Forms.Label();
            _returnedLabel = new System.Windows.Forms.Label();
            _statusLabel = new System.Windows.Forms.Label();
            _statusStrip = new System.Windows.Forms.StatusStrip();
            _summaryStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();

            _checkoutIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            _studentIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            _studentNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            _equipmentColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            _quantityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            _borrowDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            _dueDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            _returnDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            _statusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();

            ((System.ComponentModel.ISupportInitialize)_mainSplitContainer).BeginInit();
            _mainSplitContainer.Panel1.SuspendLayout();
            _mainSplitContainer.Panel2.SuspendLayout();
            _mainSplitContainer.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)_recordsGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_quantityNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_errorProvider).BeginInit();

            SuspendLayout();

            // Form
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1500, 850);
            MinimumSize = new System.Drawing.Size(1300, 700);
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Quản lý mượn thiết bị";

            _errorProvider.ContainerControl = this;
            _errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;

            // Main SplitContainer
            _mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            _mainSplitContainer.FixedPanel =
                System.Windows.Forms.FixedPanel.None;

            _mainSplitContainer.Orientation =
                System.Windows.Forms.Orientation.Vertical;

            _mainSplitContainer.Size = new System.Drawing.Size(1500, 850);
            _mainSplitContainer.Panel1MinSize = 330;
            _mainSplitContainer.Panel2MinSize = 450;
            _mainSplitContainer.SplitterWidth = 5;
            _mainSplitContainer.SplitterDistance = 400;

            // Editor panel
            _editorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            _editorPanel.Padding = new System.Windows.Forms.Padding(12);
            _editorPanel.AutoScroll = true;

            var titleLabel = new System.Windows.Forms.Label
            {
                Text = "THÔNG TIN PHIẾU MƯỢN",
                Font = new System.Drawing.Font(
                    "Segoe UI",
                    13F,
                    System.Drawing.FontStyle.Bold),
                AutoSize = true,
                Location = new System.Drawing.Point(12, 12)
            };

            // Mã phiếu
            AddLabel(_editorPanel, "Mã phiếu:", 20, 65);
            _checkoutIdTextBox.SetBounds(20, 90, 300, 30);
            _checkoutIdTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Left
                | System.Windows.Forms.AnchorStyles.Right;

            // Mã sinh viên
            AddLabel(_editorPanel, "Mã sinh viên:", 20, 135);
            _studentIdTextBox.SetBounds(20, 160, 300, 30);
            _studentIdTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Left
                | System.Windows.Forms.AnchorStyles.Right;

            // Họ tên
            AddLabel(_editorPanel, "Họ tên sinh viên:", 20, 205);
            _studentNameTextBox.SetBounds(20, 230, 300, 30);
            _studentNameTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Left
                | System.Windows.Forms.AnchorStyles.Right;

            // Thiết bị
            AddLabel(_editorPanel, "Thiết bị:", 20, 275);
            _equipmentComboBox.SetBounds(20, 300, 300, 30);
            _equipmentComboBox.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;
            _equipmentComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Left
                | System.Windows.Forms.AnchorStyles.Right;

            _equipmentComboBox.Items.AddRange(new object[]
            {
                "Laptop",
                "Máy chiếu",
                "Máy ảnh",
                "Micro",
                "Máy tính bảng",
                "Bàn phím",
                "Chuột máy tính"
            });

            // Số lượng
            AddLabel(_editorPanel, "Số lượng:", 20, 345);
            _quantityNumeric.SetBounds(20, 370, 300, 30);
            _quantityNumeric.Anchor = System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Left
                | System.Windows.Forms.AnchorStyles.Right;
            _quantityNumeric.Minimum = 1;
            _quantityNumeric.Maximum = 1000;
            _quantityNumeric.Value = 1;

            // Ngày mượn
            AddLabel(_editorPanel, "Ngày mượn:", 20, 415);
            _borrowDatePicker.SetBounds(20, 440, 300, 30);
            _borrowDatePicker.Anchor = System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Left
                | System.Windows.Forms.AnchorStyles.Right;
            _borrowDatePicker.Format =
                System.Windows.Forms.DateTimePickerFormat.Short;

            // Hạn trả
            AddLabel(_editorPanel, "Hạn trả:", 20, 485);
            _dueDatePicker.SetBounds(20, 510, 300, 30);
            _dueDatePicker.Anchor = System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Left
                | System.Windows.Forms.AnchorStyles.Right;
            _dueDatePicker.Format =
                System.Windows.Forms.DateTimePickerFormat.Short;
            _dueDatePicker.Value = System.DateTime.Today.AddDays(7);

            // Ngày trả thực tế
            AddLabel(_editorPanel, "Ngày trả thực tế:", 20, 555);
            _returnDatePicker.SetBounds(20, 580, 300, 30);
            _returnDatePicker.Anchor = System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Left
                | System.Windows.Forms.AnchorStyles.Right;
            _returnDatePicker.Format =
                System.Windows.Forms.DateTimePickerFormat.Short;
            _returnDatePicker.ShowCheckBox = true;
            _returnDatePicker.Checked = false;

            // Nút chức năng
            _addButton.Text = "Thêm";
            _addButton.SetBounds(20, 630, 90, 38);

            _updateButton.Text = "Sửa";
            _updateButton.SetBounds(120, 630, 90, 38);

            _deleteButton.Text = "Xóa";
            _deleteButton.SetBounds(220, 630, 90, 38);

            _clearButton.Text = "Làm mới";
            _clearButton.SetBounds(20, 680, 140, 38);

            _returnButton.Text = "Xác nhận trả";
            _returnButton.SetBounds(170, 680, 140, 38);

            _editorPanel.Controls.Add(titleLabel);

            _editorPanel.Controls.AddRange(
                new System.Windows.Forms.Control[]
                {
                    _checkoutIdTextBox,
                    _studentIdTextBox,
                    _studentNameTextBox,
                    _equipmentComboBox,
                    _quantityNumeric,
                    _borrowDatePicker,
                    _dueDatePicker,
                    _returnDatePicker,
                    _addButton,
                    _updateButton,
                    _deleteButton,
                    _clearButton,
                    _returnButton
                });

            // Thanh tìm kiếm
            _searchTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            _searchTextBox.PlaceholderText =
                "Tìm mã phiếu, mã SV, họ tên hoặc thiết bị...";

            // Bộ lọc tình trạng
            _statusFilterComboBox.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            _statusFilterComboBox.Items.AddRange(new object[]
            {
                "Tất cả",
                "Đang mượn",
                "Quá hạn",
                "Đã trả"
            });

            _statusFilterComboBox.SelectedIndex = 0;
            _statusFilterComboBox.Dock = System.Windows.Forms.DockStyle.Fill;

            _exportButton.Text = "Xuất CSV";
            _exportButton.Dock = System.Windows.Forms.DockStyle.Fill;
            _exportButton.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            _exportButton.Name = "_exportButton";

            var searchLabel = new System.Windows.Forms.Label
            {
                Text = "Tìm kiếm:",
                AutoSize = true,
                Anchor = System.Windows.Forms.AnchorStyles.Left
            };

            var filterLabel = new System.Windows.Forms.Label
            {
                Text = "Tình trạng:",
                AutoSize = true,
                Anchor = System.Windows.Forms.AnchorStyles.Left
            };

            var searchLayout = new System.Windows.Forms.TableLayoutPanel
            {
                Dock = System.Windows.Forms.DockStyle.Top,
                Height = 45,
                ColumnCount = 5,
                Padding = new System.Windows.Forms.Padding(5)
            };

            searchLayout.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Absolute, 85));

            searchLayout.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent, 60));

            searchLayout.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Absolute, 85));

            searchLayout.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent, 40));

            searchLayout.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Absolute, 110));

            searchLayout.Controls.Add(searchLabel, 0, 0);
            searchLayout.Controls.Add(_searchTextBox, 1, 0);
            searchLayout.Controls.Add(filterLabel, 2, 0);
            searchLayout.Controls.Add(_statusFilterComboBox, 3, 0);
            searchLayout.Controls.Add(_exportButton, 4, 0);

            // Thống kê
            var summaryLayout = new System.Windows.Forms.FlowLayoutPanel
            {
                Dock = System.Windows.Forms.DockStyle.Top,
                Height = 45,
                Padding = new System.Windows.Forms.Padding(5),
                WrapContents = false
            };

            _totalLabel.Text = "Tổng phiếu: 0";
            _totalLabel.AutoSize = true;
            _totalLabel.Margin = new System.Windows.Forms.Padding(10);

            _borrowedLabel.Text = "Đang mượn: 0";
            _borrowedLabel.AutoSize = true;
            _borrowedLabel.Margin = new System.Windows.Forms.Padding(10);

            _overdueLabel.Text = "Quá hạn: 0";
            _overdueLabel.AutoSize = true;
            _overdueLabel.Margin = new System.Windows.Forms.Padding(10);

            _returnedLabel.Text = "Đã trả: 0";
            _returnedLabel.AutoSize = true;
            _returnedLabel.Margin = new System.Windows.Forms.Padding(10);

            summaryLayout.Controls.AddRange(
                new System.Windows.Forms.Control[]
                {
                    _totalLabel,
                    _borrowedLabel,
                    _overdueLabel,
                    _returnedLabel
                });

            // Cột Mã phiếu
            _checkoutIdColumn.Name = "_checkoutIdColumn";
            _checkoutIdColumn.HeaderText = "Mã phiếu";
            _checkoutIdColumn.DataPropertyName =
                nameof(CheckoutRecord.CheckoutId);

            // Cột Mã sinh viên
            _studentIdColumn.Name = "_studentIdColumn";
            _studentIdColumn.HeaderText = "Mã SV";
            _studentIdColumn.DataPropertyName =
                nameof(CheckoutRecord.StudentId);

            // Cột Họ tên
            _studentNameColumn.Name = "_studentNameColumn";
            _studentNameColumn.HeaderText = "Họ tên";
            _studentNameColumn.DataPropertyName =
                nameof(CheckoutRecord.StudentName);

            // Cột Thiết bị
            _equipmentColumn.Name = "_equipmentColumn";
            _equipmentColumn.HeaderText = "Thiết bị";
            _equipmentColumn.DataPropertyName =
                nameof(CheckoutRecord.Equipment);

            // Cột Số lượng
            _quantityColumn.Name = "_quantityColumn";
            _quantityColumn.HeaderText = "Số lượng";
            _quantityColumn.DataPropertyName =
                nameof(CheckoutRecord.Quantity);

            // Cột Ngày mượn
            _borrowDateColumn.Name = "_borrowDateColumn";
            _borrowDateColumn.HeaderText = "Ngày mượn";
            _borrowDateColumn.DataPropertyName =
                nameof(CheckoutRecord.BorrowDate);
            _borrowDateColumn.DefaultCellStyle.Format = "dd/MM/yyyy";

            // Cột Hạn trả
            _dueDateColumn.Name = "_dueDateColumn";
            _dueDateColumn.HeaderText = "Hạn trả";
            _dueDateColumn.DataPropertyName =
                nameof(CheckoutRecord.DueDate);
            _dueDateColumn.DefaultCellStyle.Format = "dd/MM/yyyy";

            // Cột Ngày trả
            _returnDateColumn.Name = "_returnDateColumn";
            _returnDateColumn.HeaderText = "Ngày trả";
            _returnDateColumn.DataPropertyName =
                nameof(CheckoutRecord.ActualReturnDate);
            _returnDateColumn.DefaultCellStyle.Format = "dd/MM/yyyy";

            // Cột Tình trạng
            _statusColumn.Name = "_statusColumn";
            _statusColumn.HeaderText = "Tình trạng";
            _statusColumn.DataPropertyName =
                nameof(CheckoutRecord.Status);

            // Cấu hình DataGridView
            _recordsGrid.AutoGenerateColumns = false;

            _recordsGrid.Columns.AddRange(
                new System.Windows.Forms.DataGridViewColumn[]
                {
                    _checkoutIdColumn,
                    _studentIdColumn,
                    _studentNameColumn,
                    _equipmentColumn,
                    _quantityColumn,
                    _borrowDateColumn,
                    _dueDateColumn,
                    _returnDateColumn,
                    _statusColumn
                });

            _recordsGrid.DataSource = _bindingSource;
            _recordsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            _recordsGrid.AllowUserToAddRows = false;
            _recordsGrid.AllowUserToDeleteRows = false;
            _recordsGrid.ReadOnly = true;
            _recordsGrid.MultiSelect = false;

            _recordsGrid.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            _recordsGrid.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            _recordsGrid.RowHeadersVisible = false;
            _recordsGrid.BackgroundColor = System.Drawing.Color.White;

            _recordsGrid.BorderStyle =
                System.Windows.Forms.BorderStyle.FixedSingle;

            // Thanh trạng thái
            _statusLabel.Text = "Sẵn sàng";
            _statusLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            _statusLabel.Height = 25;

            _statusLabel.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;

            
            _mainSplitContainer.Panel1.Controls.Add(_editorPanel);

            _mainSplitContainer.Panel2.Controls.Add(_recordsGrid);
            _mainSplitContainer.Panel2.Controls.Add(summaryLayout);
            _mainSplitContainer.Panel2.Controls.Add(searchLayout);
            _mainSplitContainer.Panel2.Controls.Add(_statusLabel);

            Controls.Add(_mainSplitContainer);
            Controls.Add(_statusStrip);

            ((System.ComponentModel.ISupportInitialize)
                _mainSplitContainer).EndInit();

            _mainSplitContainer.Panel1.ResumeLayout(false);
            _mainSplitContainer.Panel2.ResumeLayout(false);
            _mainSplitContainer.ResumeLayout(false);

            ((System.ComponentModel.ISupportInitialize)
                _recordsGrid).EndInit();

            ((System.ComponentModel.ISupportInitialize)
                _quantityNumeric).EndInit();

            ((System.ComponentModel.ISupportInitialize)
                _errorProvider).EndInit();

            ResumeLayout(false);
        }

        private void AddLabel(
            System.Windows.Forms.Control parent,
            string text,
            int x,
            int y)
        {
            var label = new System.Windows.Forms.Label
            {
                Text = text,
                Location = new System.Drawing.Point(x, y),
                AutoSize = true
            };

            parent.Controls.Add(label);
        }
    }
}