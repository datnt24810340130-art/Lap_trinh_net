namespace bai4._11
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private MenuStrip menuStripMain;
        private ToolStripMenuItem fileMenuItem;
        private ToolStripMenuItem exportCsvMenuItem;
        private ToolStripMenuItem exitMenuItem;
        private StatusStrip statusStripMain;
        private ToolStripStatusLabel filteredCountStatusLabel;
        private ToolStripStatusLabel statusSpacerLabel;
        private ToolStripStatusLabel estimatedTotalStatusLabel;
        private SplitContainer mainSplitContainer;
        private GroupBox orderGroupBox;
        private TableLayoutPanel orderTableLayoutPanel;
        private Label orderIdLabel;
        private TextBox orderIdTextBox;
        private Label customerNameLabel;
        private TextBox customerNameTextBox;
        private Label phoneNumberLabel;
        private TextBox phoneNumberTextBox;
        private Label deviceTypeLabel;
        private ComboBox deviceTypeComboBox;
        private Label problemDescriptionLabel;
        private TextBox problemDescriptionTextBox;
        private Label receivedDateLabel;
        private DateTimePicker receivedDatePicker;
        private Label estimatedCostLabel;
        private NumericUpDown estimatedCostNumericUpDown;
        private Label orderStatusLabel;
        private ComboBox orderStatusComboBox;
        private Label deviceImageLabel;
        private PictureBox deviceImagePictureBox;
        private Button chooseImageButton;
        private FlowLayoutPanel orderButtonsFlowLayoutPanel;
        private Button addButton;
        private Button updateButton;
        private Button deleteButton;
        private Button clearButton;
        private GroupBox ordersGroupBox;
        private TableLayoutPanel ordersTableLayoutPanel;
        private FlowLayoutPanel filterFlowLayoutPanel;
        private Label searchLabel;
        private TextBox searchTextBox;
        private Label filterStatusLabel;
        private ComboBox filterStatusComboBox;
        private DataGridView ordersDataGridView;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            menuStripMain = new MenuStrip();
            fileMenuItem = new ToolStripMenuItem();
            exportCsvMenuItem = new ToolStripMenuItem();
            exitMenuItem = new ToolStripMenuItem();
            statusStripMain = new StatusStrip();
            filteredCountStatusLabel = new ToolStripStatusLabel();
            statusSpacerLabel = new ToolStripStatusLabel();
            estimatedTotalStatusLabel = new ToolStripStatusLabel();
            mainSplitContainer = new SplitContainer();
            orderGroupBox = new GroupBox();
            orderTableLayoutPanel = new TableLayoutPanel();
            orderIdLabel = new Label();
            orderIdTextBox = new TextBox();
            customerNameLabel = new Label();
            customerNameTextBox = new TextBox();
            phoneNumberLabel = new Label();
            phoneNumberTextBox = new TextBox();
            deviceTypeLabel = new Label();
            deviceTypeComboBox = new ComboBox();
            problemDescriptionLabel = new Label();
            problemDescriptionTextBox = new TextBox();
            receivedDateLabel = new Label();
            receivedDatePicker = new DateTimePicker();
            estimatedCostLabel = new Label();
            estimatedCostNumericUpDown = new NumericUpDown();
            orderStatusLabel = new Label();
            orderStatusComboBox = new ComboBox();
            deviceImageLabel = new Label();
            deviceImagePictureBox = new PictureBox();
            chooseImageButton = new Button();
            orderButtonsFlowLayoutPanel = new FlowLayoutPanel();
            addButton = new Button();
            updateButton = new Button();
            deleteButton = new Button();
            clearButton = new Button();
            ordersGroupBox = new GroupBox();
            ordersTableLayoutPanel = new TableLayoutPanel();
            filterFlowLayoutPanel = new FlowLayoutPanel();
            searchLabel = new Label();
            searchTextBox = new TextBox();
            filterStatusLabel = new Label();
            filterStatusComboBox = new ComboBox();
            ordersDataGridView = new DataGridView();
            menuStripMain.SuspendLayout();
            statusStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).BeginInit();
            mainSplitContainer.Panel1.SuspendLayout();
            mainSplitContainer.Panel2.SuspendLayout();
            mainSplitContainer.SuspendLayout();
            orderGroupBox.SuspendLayout();
            orderTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)estimatedCostNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)deviceImagePictureBox).BeginInit();
            orderButtonsFlowLayoutPanel.SuspendLayout();
            ordersGroupBox.SuspendLayout();
            ordersTableLayoutPanel.SuspendLayout();
            filterFlowLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ordersDataGridView).BeginInit();
            SuspendLayout();
            // 
            // menuStripMain
            // 
            menuStripMain.ImageScalingSize = new Size(20, 20);
            menuStripMain.Items.AddRange(new ToolStripItem[] { fileMenuItem });
            menuStripMain.Location = new Point(0, 0);
            menuStripMain.Name = "menuStripMain";
            menuStripMain.Padding = new Padding(7, 3, 0, 3);
            menuStripMain.Size = new Size(1566, 30);
            menuStripMain.TabIndex = 0;
            // 
            // fileMenuItem
            // 
            fileMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exportCsvMenuItem, exitMenuItem });
            fileMenuItem.Name = "fileMenuItem";
            fileMenuItem.Size = new Size(48, 24);
            fileMenuItem.Text = "Tệp";
            // 
            // exportCsvMenuItem
            // 
            exportCsvMenuItem.Name = "exportCsvMenuItem";
            exportCsvMenuItem.Size = new Size(248, 26);
            exportCsvMenuItem.Text = "Xuất danh sách ra CSV...";
            exportCsvMenuItem.Click += exportCsvMenuItem_Click;
            // 
            // exitMenuItem
            // 
            exitMenuItem.Name = "exitMenuItem";
            exitMenuItem.Size = new Size(248, 26);
            exitMenuItem.Text = "Thoát";
            exitMenuItem.Click += exitMenuItem_Click;
            // 
            // statusStripMain
            // 
            statusStripMain.ImageScalingSize = new Size(20, 20);
            statusStripMain.Items.AddRange(new ToolStripItem[] { filteredCountStatusLabel, statusSpacerLabel, estimatedTotalStatusLabel });
            statusStripMain.Location = new Point(0, 1015);
            statusStripMain.Name = "statusStripMain";
            statusStripMain.Padding = new Padding(1, 0, 16, 0);
            statusStripMain.Size = new Size(1566, 26);
            statusStripMain.TabIndex = 2;
            // 
            // filteredCountStatusLabel
            // 
            filteredCountStatusLabel.Name = "filteredCountStatusLabel";
            filteredCountStatusLabel.Size = new Size(162, 20);
            filteredCountStatusLabel.Text = "Số đơn đang hiển thị: 0";
            // 
            // statusSpacerLabel
            // 
            statusSpacerLabel.Name = "statusSpacerLabel";
            statusSpacerLabel.Size = new Size(1186, 20);
            statusSpacerLabel.Spring = true;
            // 
            // estimatedTotalStatusLabel
            // 
            estimatedTotalStatusLabel.Name = "estimatedTotalStatusLabel";
            estimatedTotalStatusLabel.Size = new Size(201, 20);
            estimatedTotalStatusLabel.Text = "Tổng chi phí chưa hoàn tất: 0";
            // 
            // mainSplitContainer
            // 
            mainSplitContainer.Dock = DockStyle.Fill;
            mainSplitContainer.Location = new Point(0, 30);
            mainSplitContainer.Margin = new Padding(3, 4, 3, 4);
            mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            mainSplitContainer.Panel1.Controls.Add(orderGroupBox);
            mainSplitContainer.Panel1MinSize = 390;
            // 
            // mainSplitContainer.Panel2
            // 
            mainSplitContainer.Panel2.Controls.Add(ordersGroupBox);
            mainSplitContainer.Panel2MinSize = 450;
            mainSplitContainer.Size = new Size(1566, 985);
            mainSplitContainer.SplitterDistance = 491;
            mainSplitContainer.SplitterWidth = 5;
            mainSplitContainer.TabIndex = 1;
            // 
            // orderGroupBox
            // 
            orderGroupBox.Controls.Add(orderTableLayoutPanel);
            orderGroupBox.Dock = DockStyle.Fill;
            orderGroupBox.Location = new Point(0, 0);
            orderGroupBox.Margin = new Padding(3, 4, 3, 4);
            orderGroupBox.Name = "orderGroupBox";
            orderGroupBox.Padding = new Padding(11, 13, 11, 13);
            orderGroupBox.Size = new Size(491, 985);
            orderGroupBox.TabIndex = 0;
            orderGroupBox.TabStop = false;
            orderGroupBox.Text = "Thông tin đơn sửa chữa";
            // 
            // orderTableLayoutPanel
            // 
            orderTableLayoutPanel.ColumnCount = 2;
            orderTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 143F));
            orderTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            orderTableLayoutPanel.Controls.Add(orderIdLabel, 0, 0);
            orderTableLayoutPanel.Controls.Add(orderIdTextBox, 1, 0);
            orderTableLayoutPanel.Controls.Add(customerNameLabel, 0, 1);
            orderTableLayoutPanel.Controls.Add(customerNameTextBox, 1, 1);
            orderTableLayoutPanel.Controls.Add(phoneNumberLabel, 0, 2);
            orderTableLayoutPanel.Controls.Add(phoneNumberTextBox, 1, 2);
            orderTableLayoutPanel.Controls.Add(deviceTypeLabel, 0, 3);
            orderTableLayoutPanel.Controls.Add(deviceTypeComboBox, 1, 3);
            orderTableLayoutPanel.Controls.Add(problemDescriptionLabel, 0, 4);
            orderTableLayoutPanel.Controls.Add(problemDescriptionTextBox, 1, 4);
            orderTableLayoutPanel.Controls.Add(receivedDateLabel, 0, 5);
            orderTableLayoutPanel.Controls.Add(receivedDatePicker, 1, 5);
            orderTableLayoutPanel.Controls.Add(estimatedCostLabel, 0, 6);
            orderTableLayoutPanel.Controls.Add(orderStatusLabel, 0, 7);
            orderTableLayoutPanel.Controls.Add(orderStatusComboBox, 1, 7);
            orderTableLayoutPanel.Controls.Add(deviceImageLabel, 0, 8);
            orderTableLayoutPanel.Controls.Add(deviceImagePictureBox, 1, 8);
            orderTableLayoutPanel.Controls.Add(chooseImageButton, 1, 9);
            orderTableLayoutPanel.Controls.Add(orderButtonsFlowLayoutPanel, 0, 10);
            orderTableLayoutPanel.Controls.Add(estimatedCostNumericUpDown, 1, 6);
            orderTableLayoutPanel.Dock = DockStyle.Fill;
            orderTableLayoutPanel.Location = new Point(11, 33);
            orderTableLayoutPanel.Margin = new Padding(3, 4, 3, 4);
            orderTableLayoutPanel.Name = "orderTableLayoutPanel";
            orderTableLayoutPanel.RowCount = 11;
            orderTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            orderTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            orderTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            orderTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            orderTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 140F));
            orderTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            orderTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            orderTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            orderTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            orderTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            orderTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));
            orderTableLayoutPanel.Size = new Size(469, 939);
            orderTableLayoutPanel.TabIndex = 0;
            orderTableLayoutPanel.Paint += orderTableLayoutPanel_Paint;
            // 
            // orderIdLabel
            // 
            orderIdLabel.Dock = DockStyle.Fill;
            orderIdLabel.Location = new Point(3, 0);
            orderIdLabel.Name = "orderIdLabel";
            orderIdLabel.Size = new Size(137, 56);
            orderIdLabel.TabIndex = 0;
            orderIdLabel.Text = "Mã đơn:";
            orderIdLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // orderIdTextBox
            // 
            orderIdTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            orderIdTextBox.Location = new Point(146, 14);
            orderIdTextBox.Margin = new Padding(3, 4, 24, 4);
            orderIdTextBox.Name = "orderIdTextBox";
            orderIdTextBox.PlaceholderText = "Nhập mã đơn";
            orderIdTextBox.Size = new Size(320, 27);
            orderIdTextBox.TabIndex = 0;
            // 
            // customerNameLabel
            // 
            customerNameLabel.Dock = DockStyle.Fill;
            customerNameLabel.Location = new Point(3, 56);
            customerNameLabel.Name = "customerNameLabel";
            customerNameLabel.Size = new Size(137, 56);
            customerNameLabel.TabIndex = 1;
            customerNameLabel.Text = "Tên khách hàng:";
            customerNameLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // customerNameTextBox
            // 
            customerNameTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            customerNameTextBox.Location = new Point(146, 70);
            customerNameTextBox.Margin = new Padding(3, 4, 24, 4);
            customerNameTextBox.Name = "customerNameTextBox";
            customerNameTextBox.Size = new Size(320, 27);
            customerNameTextBox.TabIndex = 1;
            // 
            // phoneNumberLabel
            // 
            phoneNumberLabel.Dock = DockStyle.Fill;
            phoneNumberLabel.Location = new Point(3, 112);
            phoneNumberLabel.Name = "phoneNumberLabel";
            phoneNumberLabel.Size = new Size(137, 56);
            phoneNumberLabel.TabIndex = 2;
            phoneNumberLabel.Text = "Số điện thoại:";
            phoneNumberLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // phoneNumberTextBox
            // 
            phoneNumberTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            phoneNumberTextBox.Location = new Point(146, 126);
            phoneNumberTextBox.Margin = new Padding(3, 4, 24, 4);
            phoneNumberTextBox.Name = "phoneNumberTextBox";
            phoneNumberTextBox.Size = new Size(320, 27);
            phoneNumberTextBox.TabIndex = 2;
            // 
            // deviceTypeLabel
            // 
            deviceTypeLabel.Dock = DockStyle.Fill;
            deviceTypeLabel.Location = new Point(3, 168);
            deviceTypeLabel.Name = "deviceTypeLabel";
            deviceTypeLabel.Size = new Size(137, 56);
            deviceTypeLabel.TabIndex = 3;
            deviceTypeLabel.Text = "Loại thiết bị:";
            deviceTypeLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // deviceTypeComboBox
            // 
            deviceTypeComboBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            deviceTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            deviceTypeComboBox.FormattingEnabled = true;
            deviceTypeComboBox.Location = new Point(146, 182);
            deviceTypeComboBox.Margin = new Padding(3, 4, 24, 4);
            deviceTypeComboBox.Name = "deviceTypeComboBox";
            deviceTypeComboBox.Size = new Size(320, 28);
            deviceTypeComboBox.TabIndex = 3;
            // 
            // problemDescriptionLabel
            // 
            problemDescriptionLabel.Dock = DockStyle.Fill;
            problemDescriptionLabel.Location = new Point(3, 224);
            problemDescriptionLabel.Name = "problemDescriptionLabel";
            problemDescriptionLabel.Padding = new Padding(0, 11, 0, 0);
            problemDescriptionLabel.Size = new Size(137, 140);
            problemDescriptionLabel.TabIndex = 4;
            problemDescriptionLabel.Text = "Mô tả lỗi:";
            // 
            // problemDescriptionTextBox
            // 
            problemDescriptionTextBox.Location = new Point(146, 228);
            problemDescriptionTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            problemDescriptionTextBox.Margin = new Padding(3, 4, 24, 4);
            problemDescriptionTextBox.Multiline = true;
            problemDescriptionTextBox.Name = "problemDescriptionTextBox";
            problemDescriptionTextBox.ScrollBars = ScrollBars.Vertical;
            problemDescriptionTextBox.Size = new Size(269, 131);
            problemDescriptionTextBox.TabIndex = 4;
            // 
            // receivedDateLabel
            // 
            receivedDateLabel.Dock = DockStyle.Fill;
            receivedDateLabel.Location = new Point(3, 364);
            receivedDateLabel.Name = "receivedDateLabel";
            receivedDateLabel.Size = new Size(137, 56);
            receivedDateLabel.TabIndex = 5;
            receivedDateLabel.Text = "Ngày tiếp nhận:";
            receivedDateLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // receivedDatePicker
            // 
            receivedDatePicker.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            receivedDatePicker.Format = DateTimePickerFormat.Short;
            receivedDatePicker.Location = new Point(146, 378);
            receivedDatePicker.Margin = new Padding(3, 4, 3, 4);
            receivedDatePicker.Name = "receivedDatePicker";
            receivedDatePicker.Size = new Size(320, 27);
            receivedDatePicker.TabIndex = 5;
            // 
            // estimatedCostLabel
            // 
            estimatedCostLabel.Dock = DockStyle.Fill;
            estimatedCostLabel.Location = new Point(3, 420);
            estimatedCostLabel.Name = "estimatedCostLabel";
            estimatedCostLabel.Size = new Size(137, 56);
            estimatedCostLabel.TabIndex = 6;
            estimatedCostLabel.Text = "Chi phí dự kiến:";
            estimatedCostLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // estimatedCostNumericUpDown
            // 
            estimatedCostNumericUpDown.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            estimatedCostNumericUpDown.Location = new Point(146, 434);
            estimatedCostNumericUpDown.Margin = new Padding(3, 4, 24, 4);
            estimatedCostNumericUpDown.Maximum = new decimal(new int[] { -727379968, 232, 0, 0 });
            estimatedCostNumericUpDown.Minimum = new decimal(new int[] { -727379968, 232, 0, int.MinValue });
            estimatedCostNumericUpDown.Name = "estimatedCostNumericUpDown";
            estimatedCostNumericUpDown.Size = new Size(320, 27);
            estimatedCostNumericUpDown.TabIndex = 6;
            estimatedCostNumericUpDown.ThousandsSeparator = true;
            // 
            // orderStatusLabel
            // 
            orderStatusLabel.Dock = DockStyle.Fill;
            orderStatusLabel.Location = new Point(3, 476);
            orderStatusLabel.Name = "orderStatusLabel";
            orderStatusLabel.Size = new Size(137, 56);
            orderStatusLabel.TabIndex = 7;
            orderStatusLabel.Text = "Trạng thái:";
            orderStatusLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // orderStatusComboBox
            // 
            orderStatusComboBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            orderStatusComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            orderStatusComboBox.FormattingEnabled = true;
            orderStatusComboBox.Location = new Point(146, 490);
            orderStatusComboBox.Margin = new Padding(3, 4, 24, 4);
            orderStatusComboBox.Name = "orderStatusComboBox";
            orderStatusComboBox.Size = new Size(320, 28);
            orderStatusComboBox.TabIndex = 7;
            // 
            // deviceImageLabel
            // 
            deviceImageLabel.Dock = DockStyle.Fill;
            deviceImageLabel.Location = new Point(3, 532);
            deviceImageLabel.Name = "deviceImageLabel";
            deviceImageLabel.Padding = new Padding(0, 11, 0, 0);
            deviceImageLabel.Size = new Size(137, 287);
            deviceImageLabel.TabIndex = 8;
            deviceImageLabel.Text = "Ảnh thiết bị:";
            // 
            // deviceImagePictureBox
            // 
            deviceImagePictureBox.BorderStyle = BorderStyle.FixedSingle;
            deviceImagePictureBox.Dock = DockStyle.Fill;
            deviceImagePictureBox.Location = new Point(146, 536);
            deviceImagePictureBox.Margin = new Padding(3, 4, 3, 4);
            deviceImagePictureBox.Name = "deviceImagePictureBox";
            deviceImagePictureBox.Size = new Size(320, 279);
            deviceImagePictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            deviceImagePictureBox.TabIndex = 8;
            deviceImagePictureBox.TabStop = false;
            // 
            // chooseImageButton
            // 
            chooseImageButton.Anchor = AnchorStyles.Left;
            chooseImageButton.Location = new Point(146, 827);
            chooseImageButton.Margin = new Padding(3, 4, 3, 4);
            chooseImageButton.Name = "chooseImageButton";
            chooseImageButton.Size = new Size(154, 40);
            chooseImageButton.TabIndex = 9;
            chooseImageButton.Text = "Chọn ảnh...";
            chooseImageButton.UseVisualStyleBackColor = true;
            chooseImageButton.Click += chooseImageButton_Click;
            // 
            // orderButtonsFlowLayoutPanel
            // 
            orderTableLayoutPanel.SetColumnSpan(orderButtonsFlowLayoutPanel, 2);
            orderButtonsFlowLayoutPanel.Controls.Add(addButton);
            orderButtonsFlowLayoutPanel.Controls.Add(updateButton);
            orderButtonsFlowLayoutPanel.Controls.Add(deleteButton);
            orderButtonsFlowLayoutPanel.Controls.Add(clearButton);
            orderButtonsFlowLayoutPanel.Dock = DockStyle.Fill;
            orderButtonsFlowLayoutPanel.Location = new Point(0, 875);
            orderButtonsFlowLayoutPanel.Margin = new Padding(0);
            orderButtonsFlowLayoutPanel.Name = "orderButtonsFlowLayoutPanel";
            orderButtonsFlowLayoutPanel.Size = new Size(469, 64);
            orderButtonsFlowLayoutPanel.TabIndex = 10;
            orderButtonsFlowLayoutPanel.WrapContents = false;
            // 
            // addButton
            // 
            addButton.Location = new Point(3, 4);
            addButton.Margin = new Padding(3, 4, 3, 4);
            addButton.Name = "addButton";
            addButton.Size = new Size(94, 45);
            addButton.TabIndex = 0;
            addButton.Text = "Thêm";
            addButton.UseVisualStyleBackColor = true;
            addButton.Click += addButton_Click;
            // 
            // updateButton
            // 
            updateButton.Location = new Point(103, 4);
            updateButton.Margin = new Padding(3, 4, 3, 4);
            updateButton.Name = "updateButton";
            updateButton.Size = new Size(94, 45);
            updateButton.TabIndex = 1;
            updateButton.Text = "Cập nhật";
            updateButton.UseVisualStyleBackColor = true;
            updateButton.Click += updateButton_Click;
            // 
            // deleteButton
            // 
            deleteButton.Location = new Point(203, 4);
            deleteButton.Margin = new Padding(3, 4, 3, 4);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(82, 45);
            deleteButton.TabIndex = 2;
            deleteButton.Text = "Xóa";
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Click += deleteButton_Click;
            // 
            // clearButton
            // 
            clearButton.Location = new Point(291, 4);
            clearButton.Margin = new Padding(3, 4, 3, 4);
            clearButton.Name = "clearButton";
            clearButton.Size = new Size(94, 45);
            clearButton.TabIndex = 3;
            clearButton.Text = "Làm mới";
            clearButton.UseVisualStyleBackColor = true;
            clearButton.Click += clearButton_Click;
            // 
            // ordersGroupBox
            // 
            ordersGroupBox.Controls.Add(ordersTableLayoutPanel);
            ordersGroupBox.Dock = DockStyle.Fill;
            ordersGroupBox.Location = new Point(0, 0);
            ordersGroupBox.Margin = new Padding(3, 4, 3, 4);
            ordersGroupBox.Name = "ordersGroupBox";
            ordersGroupBox.Padding = new Padding(11, 13, 11, 13);
            ordersGroupBox.Size = new Size(1070, 985);
            ordersGroupBox.TabIndex = 0;
            ordersGroupBox.TabStop = false;
            ordersGroupBox.Text = "Danh sách đơn sửa chữa";
            // 
            // ordersTableLayoutPanel
            // 
            ordersTableLayoutPanel.ColumnCount = 1;
            ordersTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            ordersTableLayoutPanel.Controls.Add(filterFlowLayoutPanel, 0, 0);
            ordersTableLayoutPanel.Controls.Add(ordersDataGridView, 0, 1);
            ordersTableLayoutPanel.Dock = DockStyle.Fill;
            ordersTableLayoutPanel.Location = new Point(11, 33);
            ordersTableLayoutPanel.Margin = new Padding(3, 4, 3, 4);
            ordersTableLayoutPanel.Name = "ordersTableLayoutPanel";
            ordersTableLayoutPanel.RowCount = 2;
            ordersTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            ordersTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            ordersTableLayoutPanel.Size = new Size(1048, 939);
            ordersTableLayoutPanel.TabIndex = 0;
            // 
            // filterFlowLayoutPanel
            // 
            filterFlowLayoutPanel.Controls.Add(searchLabel);
            filterFlowLayoutPanel.Controls.Add(searchTextBox);
            filterFlowLayoutPanel.Controls.Add(filterStatusLabel);
            filterFlowLayoutPanel.Controls.Add(filterStatusComboBox);
            filterFlowLayoutPanel.Dock = DockStyle.Fill;
            filterFlowLayoutPanel.Location = new Point(3, 4);
            filterFlowLayoutPanel.Margin = new Padding(3, 4, 3, 4);
            filterFlowLayoutPanel.Name = "filterFlowLayoutPanel";
            filterFlowLayoutPanel.Size = new Size(1042, 52);
            filterFlowLayoutPanel.TabIndex = 0;
            filterFlowLayoutPanel.WrapContents = false;
            // 
            // searchLabel
            // 
            searchLabel.AutoSize = true;
            searchLabel.Location = new Point(3, 0);
            searchLabel.Name = "searchLabel";
            searchLabel.Padding = new Padding(0, 11, 0, 0);
            searchLabel.Size = new Size(73, 31);
            searchLabel.TabIndex = 0;
            searchLabel.Text = "Tìm kiếm:";
            // 
            // searchTextBox
            // 
            searchTextBox.Location = new Point(88, 8);
            searchTextBox.Margin = new Padding(9, 8, 23, 4);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.PlaceholderText = "Mã đơn, tên khách hàng hoặc số điện thoại";
            searchTextBox.Size = new Size(331, 27);
            searchTextBox.TabIndex = 0;
            searchTextBox.TextChanged += searchTextBox_TextChanged;
            // 
            // filterStatusLabel
            // 
            filterStatusLabel.AutoSize = true;
            filterStatusLabel.Location = new Point(445, 0);
            filterStatusLabel.Name = "filterStatusLabel";
            filterStatusLabel.Padding = new Padding(0, 11, 0, 0);
            filterStatusLabel.Size = new Size(103, 31);
            filterStatusLabel.TabIndex = 1;
            filterStatusLabel.Text = "Lọc trạng thái:";
            // 
            // filterStatusComboBox
            // 
            filterStatusComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            filterStatusComboBox.Location = new Point(560, 7);
            filterStatusComboBox.Margin = new Padding(9, 7, 3, 4);
            filterStatusComboBox.Name = "filterStatusComboBox";
            filterStatusComboBox.Size = new Size(194, 28);
            filterStatusComboBox.TabIndex = 1;
            filterStatusComboBox.SelectedIndexChanged += filterStatusComboBox_SelectedIndexChanged;
            // 
            // ordersDataGridView
            // 
            ordersDataGridView.AllowUserToAddRows = false;
            ordersDataGridView.AllowUserToDeleteRows = false;
            ordersDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ordersDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ordersDataGridView.Dock = DockStyle.Fill;
            ordersDataGridView.Location = new Point(3, 64);
            ordersDataGridView.Margin = new Padding(3, 4, 3, 4);
            ordersDataGridView.MultiSelect = false;
            ordersDataGridView.Name = "ordersDataGridView";
            ordersDataGridView.ReadOnly = true;
            ordersDataGridView.RowHeadersVisible = false;
            ordersDataGridView.RowHeadersWidth = 51;
            ordersDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ordersDataGridView.Size = new Size(1042, 871);
            ordersDataGridView.TabIndex = 1;
            ordersDataGridView.SelectionChanged += ordersDataGridView_SelectionChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1566, 1041);
            Controls.Add(mainSplitContainer);
            Controls.Add(statusStripMain);
            Controls.Add(menuStripMain);
            MainMenuStrip = menuStripMain;
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(1197, 851);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý đơn sửa chữa";
            menuStripMain.ResumeLayout(false);
            menuStripMain.PerformLayout();
            statusStripMain.ResumeLayout(false);
            statusStripMain.PerformLayout();
            mainSplitContainer.Panel1.ResumeLayout(false);
            mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).EndInit();
            mainSplitContainer.ResumeLayout(false);
            orderGroupBox.ResumeLayout(false);
            orderTableLayoutPanel.ResumeLayout(false);
            orderTableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)estimatedCostNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)deviceImagePictureBox).EndInit();
            orderButtonsFlowLayoutPanel.ResumeLayout(false);
            ordersGroupBox.ResumeLayout(false);
            ordersTableLayoutPanel.ResumeLayout(false);
            filterFlowLayoutPanel.ResumeLayout(false);
            filterFlowLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ordersDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}