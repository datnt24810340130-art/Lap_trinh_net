namespace bai2
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuOpenCsv;
        private System.Windows.Forms.ToolStripMenuItem menuSaveCsv;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuAddBook;
        private System.Windows.Forms.ToolStripMenuItem menuDeleteBook;
        private System.Windows.Forms.ToolStripMenuItem menuSearchBook;

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolOpenCsv;
        private System.Windows.Forms.ToolStripButton toolSaveCsv;
        private System.Windows.Forms.ToolStripButton toolAddBook;
        private System.Windows.Forms.ToolStripButton toolSearchBook;

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelTotalBooks;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelTotalValue;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelCurrentTime;

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBoxDetails;
        private System.Windows.Forms.Label lblBookId;
        private System.Windows.Forms.Label lblBookTitle;
        private System.Windows.Forms.Label lblBookAuthor;
        private System.Windows.Forms.Label lblBookCategory;
        private System.Windows.Forms.Label lblBookPrice;
        private System.Windows.Forms.Label lblBookQuantity;
        private System.Windows.Forms.Label lblBookCover;
        private System.Windows.Forms.TextBox txtBookId;
        private System.Windows.Forms.TextBox txtBookTitle;
        private System.Windows.Forms.TextBox txtBookAuthor;
        private System.Windows.Forms.TextBox txtBookCategory;
        private System.Windows.Forms.TextBox txtBookPrice;
        private System.Windows.Forms.TextBox txtBookQuantity;
        private System.Windows.Forms.TextBox txtCoverImagePath;
        private System.Windows.Forms.PictureBox picBookCover;
        private System.Windows.Forms.Button btnBrowseCover;
        private System.Windows.Forms.Button btnAddBook;
        private System.Windows.Forms.Button btnDeleteBook;
        private System.Windows.Forms.Button btnClearForm;
        private System.Windows.Forms.Button btnOpenCsv;
        private System.Windows.Forms.Button btnSaveCsv;
        private System.Windows.Forms.DataGridView dgvBooks;
        private System.Windows.Forms.Timer formClosingTimer;

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
            components = new System.ComponentModel.Container();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            menuOpenCsv = new ToolStripMenuItem();
            menuSaveCsv = new ToolStripMenuItem();
            menuExit = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            menuAddBook = new ToolStripMenuItem();
            menuDeleteBook = new ToolStripMenuItem();
            menuSearchBook = new ToolStripMenuItem();
            toolStrip1 = new ToolStrip();
            toolOpenCsv = new ToolStripButton();
            toolSaveCsv = new ToolStripButton();
            toolAddBook = new ToolStripButton();
            toolSearchBook = new ToolStripButton();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabelTotalBooks = new ToolStripStatusLabel();
            toolStripStatusLabelTotalValue = new ToolStripStatusLabel();
            toolStripStatusLabelCurrentTime = new ToolStripStatusLabel();
            tableLayoutPanel1 = new TableLayoutPanel();
            groupBoxDetails = new GroupBox();
            lblBookId = new Label();
            txtBookId = new TextBox();
            lblBookTitle = new Label();
            txtBookTitle = new TextBox();
            lblBookAuthor = new Label();
            txtBookAuthor = new TextBox();
            lblBookCategory = new Label();
            txtBookCategory = new TextBox();
            lblBookPrice = new Label();
            txtBookPrice = new TextBox();
            lblBookQuantity = new Label();
            txtBookQuantity = new TextBox();
            lblBookCover = new Label();
            txtCoverImagePath = new TextBox();
            btnBrowseCover = new Button();
            picBookCover = new PictureBox();
            btnAddBook = new Button();
            btnDeleteBook = new Button();
            btnClearForm = new Button();
            btnOpenCsv = new Button();
            btnSaveCsv = new Button();
            dgvBooks = new DataGridView();
            formClosingTimer = new System.Windows.Forms.Timer(components);
            menuStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            groupBoxDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picBookCover).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvBooks).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem });
            menuStrip1.Location = new Point(0, 25);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(7, 3, 0, 3);
            menuStrip1.Size = new Size(1257, 30);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { menuOpenCsv, menuSaveCsv, menuExit });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(46, 24);
            fileToolStripMenuItem.Text = "File";
            // 
            // menuOpenCsv
            // 
            menuOpenCsv.Name = "menuOpenCsv";
            menuOpenCsv.Size = new Size(171, 26);
            menuOpenCsv.Text = "Mở file CSV";
            // 
            // menuSaveCsv
            // 
            menuSaveCsv.Name = "menuSaveCsv";
            menuSaveCsv.Size = new Size(171, 26);
            menuSaveCsv.Text = "Lưu file CSV";
            // 
            // menuExit
            // 
            menuExit.Name = "menuExit";
            menuExit.Size = new Size(171, 26);
            menuExit.Text = "Thoát";
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { menuAddBook, menuDeleteBook, menuSearchBook });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(49, 24);
            editToolStripMenuItem.Text = "Edit";
            // 
            // menuAddBook
            // 
            menuAddBook.Name = "menuAddBook";
            menuAddBook.Size = new Size(153, 26);
            menuAddBook.Text = "Thêm";
            // 
            // menuDeleteBook
            // 
            menuDeleteBook.Name = "menuDeleteBook";
            menuDeleteBook.Size = new Size(153, 26);
            menuDeleteBook.Text = "Xóa";
            // 
            // menuSearchBook
            // 
            menuSearchBook.Name = "menuSearchBook";
            menuSearchBook.Size = new Size(153, 26);
            menuSearchBook.Text = "Tìm kiếm";
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolOpenCsv, toolSaveCsv, toolAddBook, toolSearchBook });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1257, 25);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolOpenCsv
            // 
            toolOpenCsv.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolOpenCsv.Name = "toolOpenCsv";
            toolOpenCsv.Size = new Size(29, 22);
            toolOpenCsv.Text = "Mở CSV";
            // 
            // toolSaveCsv
            // 
            toolSaveCsv.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolSaveCsv.Name = "toolSaveCsv";
            toolSaveCsv.Size = new Size(29, 22);
            toolSaveCsv.Text = "Lưu CSV";
            // 
            // toolAddBook
            // 
            toolAddBook.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolAddBook.Name = "toolAddBook";
            toolAddBook.Size = new Size(29, 22);
            toolAddBook.Text = "Thêm sách";
            // 
            // toolSearchBook
            // 
            toolSearchBook.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolSearchBook.Name = "toolSearchBook";
            toolSearchBook.Size = new Size(29, 22);
            toolSearchBook.Text = "Tìm kiếm";
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabelTotalBooks, toolStripStatusLabelTotalValue, toolStripStatusLabelCurrentTime });
            statusStrip1.Location = new Point(0, 894);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 16, 0);
            statusStrip1.Size = new Size(1257, 26);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelTotalBooks
            // 
            toolStripStatusLabelTotalBooks.Name = "toolStripStatusLabelTotalBooks";
            toolStripStatusLabelTotalBooks.Size = new Size(139, 20);
            toolStripStatusLabelTotalBooks.Text = "Tổng số đầu sách: 0";
            // 
            // toolStripStatusLabelTotalValue
            // 
            toolStripStatusLabelTotalValue.Name = "toolStripStatusLabelTotalValue";
            toolStripStatusLabelTotalValue.Size = new Size(164, 20);
            toolStripStatusLabelTotalValue.Text = "Tổng giá trị kho: 0 VNĐ";
            // 
            // toolStripStatusLabelCurrentTime
            // 
            toolStripStatusLabelCurrentTime.Name = "toolStripStatusLabelCurrentTime";
            toolStripStatusLabelCurrentTime.Size = new Size(143, 20);
            toolStripStatusLabelCurrentTime.Text = "Thời gian hiện tại: --";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Controls.Add(groupBoxDetails, 0, 0);
            tableLayoutPanel1.Controls.Add(dgvBooks, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 521F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1257, 894);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // groupBoxDetails
            // 
            groupBoxDetails.Controls.Add(lblBookId);
            groupBoxDetails.Controls.Add(txtBookId);
            groupBoxDetails.Controls.Add(lblBookTitle);
            groupBoxDetails.Controls.Add(txtBookTitle);
            groupBoxDetails.Controls.Add(lblBookAuthor);
            groupBoxDetails.Controls.Add(txtBookAuthor);
            groupBoxDetails.Controls.Add(lblBookCategory);
            groupBoxDetails.Controls.Add(txtBookCategory);
            groupBoxDetails.Controls.Add(lblBookPrice);
            groupBoxDetails.Controls.Add(txtBookPrice);
            groupBoxDetails.Controls.Add(lblBookQuantity);
            groupBoxDetails.Controls.Add(txtBookQuantity);
            groupBoxDetails.Controls.Add(lblBookCover);
            groupBoxDetails.Controls.Add(txtCoverImagePath);
            groupBoxDetails.Controls.Add(btnBrowseCover);
            groupBoxDetails.Controls.Add(picBookCover);
            groupBoxDetails.Controls.Add(btnAddBook);
            groupBoxDetails.Controls.Add(btnDeleteBook);
            groupBoxDetails.Controls.Add(btnClearForm);
            groupBoxDetails.Controls.Add(btnOpenCsv);
            groupBoxDetails.Controls.Add(btnSaveCsv);
            groupBoxDetails.Dock = DockStyle.Fill;
            groupBoxDetails.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBoxDetails.Location = new Point(3, 4);
            groupBoxDetails.Margin = new Padding(3, 4, 3, 4);
            groupBoxDetails.Name = "groupBoxDetails";
            groupBoxDetails.Padding = new Padding(3, 4, 3, 4);
            groupBoxDetails.Size = new Size(1251, 513);
            groupBoxDetails.TabIndex = 0;
            groupBoxDetails.TabStop = false;
            groupBoxDetails.Text = "Thông tin sách";
            // 
            // lblBookId
            // 
            lblBookId.AutoSize = true;
            lblBookId.Location = new Point(18, 88);
            lblBookId.Name = "lblBookId";
            lblBookId.Size = new Size(66, 20);
            lblBookId.TabIndex = 0;
            lblBookId.Text = "Mã sách";
            // 
            // txtBookId
            // 
            txtBookId.Location = new Point(110, 88);
            txtBookId.Margin = new Padding(3, 4, 3, 4);
            txtBookId.Name = "txtBookId";
            txtBookId.ReadOnly = true;
            txtBookId.Size = new Size(137, 27);
            txtBookId.TabIndex = 1;
            // 
            // lblBookTitle
            // 
            lblBookTitle.AutoSize = true;
            lblBookTitle.Location = new Point(18, 146);
            lblBookTitle.Name = "lblBookTitle";
            lblBookTitle.Size = new Size(69, 20);
            lblBookTitle.TabIndex = 2;
            lblBookTitle.Text = "Tên sách";
            // 
            // txtBookTitle
            // 
            txtBookTitle.Location = new Point(110, 143);
            txtBookTitle.Margin = new Padding(3, 4, 3, 4);
            txtBookTitle.Name = "txtBookTitle";
            txtBookTitle.Size = new Size(228, 27);
            txtBookTitle.TabIndex = 3;
            // 
            // lblBookAuthor
            // 
            lblBookAuthor.AutoSize = true;
            lblBookAuthor.Location = new Point(21, 194);
            lblBookAuthor.Name = "lblBookAuthor";
            lblBookAuthor.Size = new Size(57, 20);
            lblBookAuthor.TabIndex = 4;
            lblBookAuthor.Text = "Tác giả";
            // 
            // txtBookAuthor
            // 
            txtBookAuthor.Location = new Point(110, 191);
            txtBookAuthor.Margin = new Padding(3, 4, 3, 4);
            txtBookAuthor.Name = "txtBookAuthor";
            txtBookAuthor.Size = new Size(228, 27);
            txtBookAuthor.TabIndex = 5;
            // 
            // lblBookCategory
            // 
            lblBookCategory.AutoSize = true;
            lblBookCategory.Location = new Point(19, 244);
            lblBookCategory.Name = "lblBookCategory";
            lblBookCategory.Size = new Size(64, 20);
            lblBookCategory.TabIndex = 6;
            lblBookCategory.Text = "Thể loại";
            // 
            // txtBookCategory
            // 
            txtBookCategory.Location = new Point(110, 241);
            txtBookCategory.Margin = new Padding(3, 4, 3, 4);
            txtBookCategory.Name = "txtBookCategory";
            txtBookCategory.Size = new Size(228, 27);
            txtBookCategory.TabIndex = 7;
            // 
            // lblBookPrice
            // 
            lblBookPrice.AutoSize = true;
            lblBookPrice.Location = new Point(21, 284);
            lblBookPrice.Name = "lblBookPrice";
            lblBookPrice.Size = new Size(62, 20);
            lblBookPrice.TabIndex = 8;
            lblBookPrice.Text = "Giá bán";
            // 
            // txtBookPrice
            // 
            txtBookPrice.Location = new Point(110, 284);
            txtBookPrice.Margin = new Padding(3, 4, 3, 4);
            txtBookPrice.Name = "txtBookPrice";
            txtBookPrice.Size = new Size(137, 27);
            txtBookPrice.TabIndex = 9;
            // 
            // lblBookQuantity
            // 
            lblBookQuantity.AutoSize = true;
            lblBookQuantity.Location = new Point(19, 335);
            lblBookQuantity.Name = "lblBookQuantity";
            lblBookQuantity.Size = new Size(71, 20);
            lblBookQuantity.TabIndex = 10;
            lblBookQuantity.Text = "Số lượng";
            // 
            // txtBookQuantity
            // 
            txtBookQuantity.Location = new Point(110, 332);
            txtBookQuantity.Margin = new Padding(3, 4, 3, 4);
            txtBookQuantity.Name = "txtBookQuantity";
            txtBookQuantity.Size = new Size(137, 27);
            txtBookQuantity.TabIndex = 11;
            // 
            // lblBookCover
            // 
            lblBookCover.AutoSize = true;
            lblBookCover.Location = new Point(400, 91);
            lblBookCover.Name = "lblBookCover";
            lblBookCover.Size = new Size(63, 20);
            lblBookCover.TabIndex = 12;
            lblBookCover.Text = "Ảnh bìa";
            lblBookCover.Click += lblBookCover_Click;
            // 
            // txtCoverImagePath
            // 
            txtCoverImagePath.Location = new Point(491, 81);
            txtCoverImagePath.Margin = new Padding(3, 4, 3, 4);
            txtCoverImagePath.Name = "txtCoverImagePath";
            txtCoverImagePath.ReadOnly = true;
            txtCoverImagePath.Size = new Size(345, 27);
            txtCoverImagePath.TabIndex = 13;
            // 
            // btnBrowseCover
            // 
            btnBrowseCover.Location = new Point(491, 136);
            btnBrowseCover.Margin = new Padding(3, 4, 3, 4);
            btnBrowseCover.Name = "btnBrowseCover";
            btnBrowseCover.Size = new Size(171, 40);
            btnBrowseCover.TabIndex = 14;
            btnBrowseCover.Text = "Tải ảnh bìa";
            // 
            // picBookCover
            // 
            picBookCover.BorderStyle = BorderStyle.FixedSingle;
            picBookCover.Location = new Point(491, 204);
            picBookCover.Margin = new Padding(3, 4, 3, 4);
            picBookCover.Name = "picBookCover";
            picBookCover.Size = new Size(171, 199);
            picBookCover.SizeMode = PictureBoxSizeMode.Zoom;
            picBookCover.TabIndex = 15;
            picBookCover.TabStop = false;
            // 
            // btnAddBook
            // 
            btnAddBook.Location = new Point(309, 387);
            btnAddBook.Margin = new Padding(3, 4, 3, 4);
            btnAddBook.Name = "btnAddBook";
            btnAddBook.Size = new Size(126, 37);
            btnAddBook.TabIndex = 16;
            btnAddBook.Text = "Thêm";
            // 
            // btnDeleteBook
            // 
            btnDeleteBook.Location = new Point(6, 432);
            btnDeleteBook.Margin = new Padding(3, 4, 3, 4);
            btnDeleteBook.Name = "btnDeleteBook";
            btnDeleteBook.Size = new Size(126, 37);
            btnDeleteBook.TabIndex = 17;
            btnDeleteBook.Text = "Xóa";
            // 
            // btnClearForm
            // 
            btnClearForm.Location = new Point(158, 432);
            btnClearForm.Margin = new Padding(3, 4, 3, 4);
            btnClearForm.Name = "btnClearForm";
            btnClearForm.Size = new Size(126, 37);
            btnClearForm.TabIndex = 18;
            btnClearForm.Text = "Xóa form";
            // 
            // btnOpenCsv
            // 
            btnOpenCsv.Location = new Point(6, 387);
            btnOpenCsv.Margin = new Padding(3, 4, 3, 4);
            btnOpenCsv.Name = "btnOpenCsv";
            btnOpenCsv.Size = new Size(126, 37);
            btnOpenCsv.TabIndex = 19;
            btnOpenCsv.Text = "Mở CSV";
            // 
            // btnSaveCsv
            // 
            btnSaveCsv.Location = new Point(158, 387);
            btnSaveCsv.Margin = new Padding(3, 4, 3, 4);
            btnSaveCsv.Name = "btnSaveCsv";
            btnSaveCsv.Size = new Size(126, 37);
            btnSaveCsv.TabIndex = 20;
            btnSaveCsv.Text = "Lưu CSV";
            // 
            // dgvBooks
            // 
            dgvBooks.AllowUserToAddRows = false;
            dgvBooks.AllowUserToDeleteRows = false;
            dgvBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBooks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBooks.Dock = DockStyle.Fill;
            dgvBooks.Location = new Point(3, 525);
            dgvBooks.Margin = new Padding(3, 4, 3, 4);
            dgvBooks.Name = "dgvBooks";
            dgvBooks.ReadOnly = true;
            dgvBooks.RowHeadersVisible = false;
            dgvBooks.RowHeadersWidth = 51;
            dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBooks.Size = new Size(1251, 365);
            dgvBooks.TabIndex = 1;
            // 
            // formClosingTimer
            // 
            formClosingTimer.Enabled = true;
            formClosingTimer.Interval = 1000;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1257, 920);
            Controls.Add(menuStrip1);
            Controls.Add(toolStrip1);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(statusStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý tài liệu sách";
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            groupBoxDetails.ResumeLayout(false);
            groupBoxDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picBookCover).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvBooks).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
