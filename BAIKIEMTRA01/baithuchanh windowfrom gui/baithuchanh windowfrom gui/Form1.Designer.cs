namespace baithuchanh_windowfrom_gui
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            menuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            exportCSVToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            tableLayoutPanel = new TableLayoutPanel();
            panelLeft = new Panel();
            btnChooseImage = new Button();
            picAvatar = new PictureBox();
            cboCategory = new ComboBox();
            txtQuantity = new TextBox();
            txtUnitPrice = new TextBox();
            txtProductName = new TextBox();
            txtProductId = new TextBox();
            lblQuantity = new Label();
            lblUnitPrice = new Label();
            lblCategory = new Label();
            lblProductName = new Label();
            lblProductId = new Label();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            panelRight = new Panel();
            txtSearch = new TextBox();
            dgvProducts = new DataGridView();
            statusStrip = new StatusStrip();
            toolStripStatusLabel = new ToolStripStatusLabel();
            errorProvider = new ErrorProvider(components);
            menuStrip.SuspendLayout();
            tableLayoutPanel.SuspendLayout();
            panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picAvatar).BeginInit();
            panelRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.ImageScalingSize = new Size(20, 20);
            menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(1000, 28);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exportCSVToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(46, 24);
            fileToolStripMenuItem.Text = "File";
            // 
            // exportCSVToolStripMenuItem
            // 
            exportCSVToolStripMenuItem.Name = "exportCSVToolStripMenuItem";
            exportCSVToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.E;
            exportCSVToolStripMenuItem.Size = new Size(215, 26);
            exportCSVToolStripMenuItem.Text = "Export CSV";
            exportCSVToolStripMenuItem.Click += exportCSVToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.X;
            exitToolStripMenuItem.Size = new Size(215, 26);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tableLayoutPanel.Controls.Add(panelLeft, 0, 0);
            tableLayoutPanel.Controls.Add(panelRight, 1, 0);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 28);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 1;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Size = new Size(1000, 396);
            tableLayoutPanel.TabIndex = 1;
            // 
            // panelLeft
            // 
            panelLeft.Controls.Add(btnChooseImage);
            panelLeft.Controls.Add(picAvatar);
            panelLeft.Controls.Add(cboCategory);
            panelLeft.Controls.Add(txtQuantity);
            panelLeft.Controls.Add(txtUnitPrice);
            panelLeft.Controls.Add(txtProductName);
            panelLeft.Controls.Add(txtProductId);
            panelLeft.Controls.Add(lblQuantity);
            panelLeft.Controls.Add(lblUnitPrice);
            panelLeft.Controls.Add(lblCategory);
            panelLeft.Controls.Add(lblProductName);
            panelLeft.Controls.Add(lblProductId);
            panelLeft.Controls.Add(btnAdd);
            panelLeft.Controls.Add(btnUpdate);
            panelLeft.Controls.Add(btnDelete);
            panelLeft.Dock = DockStyle.Fill;
            panelLeft.Location = new Point(3, 3);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new Size(344, 390);
            panelLeft.TabIndex = 0;
            // 
            // btnChooseImage
            // 
            btnChooseImage.Location = new Point(200, 199);
            btnChooseImage.Name = "btnChooseImage";
            btnChooseImage.Size = new Size(130, 33);
            btnChooseImage.TabIndex = 0;
            btnChooseImage.Text = "Chọn ảnh";
            btnChooseImage.Click += btnChooseImage_Click;
            // 
            // picAvatar
            // 
            picAvatar.BorderStyle = BorderStyle.FixedSingle;
            picAvatar.Location = new Point(12, 199);
            picAvatar.Name = "picAvatar";
            picAvatar.Size = new Size(164, 140);
            picAvatar.SizeMode = PictureBoxSizeMode.Zoom;
            picAvatar.TabIndex = 1;
            picAvatar.TabStop = false;
            // 
            // cboCategory
            // 
            cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCategory.Location = new Point(110, 82);
            cboCategory.Name = "cboCategory";
            cboCategory.Size = new Size(200, 28);
            cboCategory.TabIndex = 2;
            // 
            // txtQuantity
            // 
            txtQuantity.Location = new Point(110, 152);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(200, 27);
            txtQuantity.TabIndex = 3;
            // 
            // txtUnitPrice
            // 
            txtUnitPrice.Location = new Point(110, 117);
            txtUnitPrice.Name = "txtUnitPrice";
            txtUnitPrice.Size = new Size(200, 27);
            txtUnitPrice.TabIndex = 4;
            // 
            // txtProductName
            // 
            txtProductName.Location = new Point(110, 47);
            txtProductName.Name = "txtProductName";
            txtProductName.Size = new Size(200, 27);
            txtProductName.TabIndex = 5;
            // 
            // txtProductId
            // 
            txtProductId.Location = new Point(110, 12);
            txtProductId.Name = "txtProductId";
            txtProductId.Size = new Size(200, 27);
            txtProductId.TabIndex = 6;
            // 
            // lblQuantity
            // 
            lblQuantity.AutoSize = true;
            lblQuantity.Location = new Point(12, 155);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(69, 20);
            lblQuantity.TabIndex = 7;
            lblQuantity.Text = "Số lượng";
            // 
            // lblUnitPrice
            // 
            lblUnitPrice.AutoSize = true;
            lblUnitPrice.Location = new Point(12, 120);
            lblUnitPrice.Name = "lblUnitPrice";
            lblUnitPrice.Size = new Size(62, 20);
            lblUnitPrice.TabIndex = 8;
            lblUnitPrice.Text = "Đơn giá";
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Location = new Point(12, 85);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(76, 20);
            lblCategory.TabIndex = 9;
            lblCategory.Text = "Danh mục";
            // 
            // lblProductName
            // 
            lblProductName.AutoSize = true;
            lblProductName.Location = new Point(12, 50);
            lblProductName.Name = "lblProductName";
            lblProductName.Size = new Size(52, 20);
            lblProductName.TabIndex = 10;
            lblProductName.Text = "Tên SP";
            // 
            // lblProductId
            // 
            lblProductId.AutoSize = true;
            lblProductId.Location = new Point(12, 15);
            lblProductId.Name = "lblProductId";
            lblProductId.Size = new Size(50, 20);
            lblProductId.TabIndex = 11;
            lblProductId.Text = "Mã SP";
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(9, 357);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(89, 30);
            btnAdd.TabIndex = 12;
            btnAdd.Text = "Add";
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(221, 357);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(89, 30);
            btnUpdate.TabIndex = 13;
            btnUpdate.Text = "Update";
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(110, 357);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(89, 30);
            btnDelete.TabIndex = 14;
            btnDelete.Text = "Delete";
            btnDelete.Click += btnDelete_Click;
            // 
            // panelRight
            // 
            panelRight.Controls.Add(txtSearch);
            panelRight.Controls.Add(dgvProducts);
            panelRight.Dock = DockStyle.Fill;
            panelRight.Location = new Point(353, 3);
            panelRight.Name = "panelRight";
            panelRight.Size = new Size(644, 390);
            panelRight.TabIndex = 1;
            // 
            // txtSearch
            // 
            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSearch.Location = new Point(10, 10);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Tìm kiếm sản phẩm";
            txtSearch.Size = new Size(620, 27);
            txtSearch.TabIndex = 0;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // dgvProducts
            // 
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AllowUserToDeleteRows = false;
            dgvProducts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvProducts.ColumnHeadersHeight = 29;
            dgvProducts.Location = new Point(10, 40);
            dgvProducts.MultiSelect = false;
            dgvProducts.Name = "dgvProducts";
            dgvProducts.ReadOnly = true;
            dgvProducts.RowHeadersWidth = 51;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.Size = new Size(620, 337);
            dgvProducts.TabIndex = 1;
            dgvProducts.CellDoubleClick += dgvProducts_CellDoubleClick;
            dgvProducts.SelectionChanged += dgvProducts_SelectionChanged;
            // 
            // statusStrip
            // 
            statusStrip.ImageScalingSize = new Size(20, 20);
            statusStrip.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel });
            statusStrip.Location = new Point(0, 424);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(1000, 26);
            statusStrip.TabIndex = 2;
            // 
            // toolStripStatusLabel
            // 
            toolStripStatusLabel.Name = "toolStripStatusLabel";
            toolStripStatusLabel.Size = new Size(119, 20);
            toolStripStatusLabel.Text = "Total products: 0";
            // 
            // errorProvider
            // 
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider.ContainerControl = this;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 450);
            Controls.Add(tableLayoutPanel);
            Controls.Add(menuStrip);
            Controls.Add(statusStrip);
            MainMenuStrip = menuStrip;
            Name = "Form1";
            Text = "TechMart Product Manager";
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            tableLayoutPanel.ResumeLayout(false);
            panelLeft.ResumeLayout(false);
            panelLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picAvatar).EndInit();
            panelRight.ResumeLayout(false);
            panelRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.PictureBox picAvatar;
        private System.Windows.Forms.Button btnChooseImage;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.TextBox txtUnitPrice;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.TextBox txtProductId;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.Label lblUnitPrice;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.Label lblProductId;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridView dgvProducts;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ErrorProvider errorProvider;

        #endregion
    }
}
