namespace bai5._3
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private GroupBox grpProductInfo;
        private Label lblProductId;
        private TextBox txtProductId;
        private Label lblProductName;
        private TextBox txtProductName;
        private Label lblUnitPrice;
        private TextBox txtUnitPrice;
        private Label lblQuantity;
        private TextBox txtQuantity;
        private Label lblCategory;
        private TextBox txtCategory;

        private GroupBox grpFunctions;
        private Label lblSearch;
        private TextBox txtSearchName;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnSearch;

        private DataGridView dgvProducts;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            grpProductInfo = new GroupBox();
            lblProductId = new Label();
            txtProductId = new TextBox();
            lblProductName = new Label();
            txtProductName = new TextBox();
            lblUnitPrice = new Label();
            txtUnitPrice = new TextBox();
            lblQuantity = new Label();
            txtQuantity = new TextBox();
            lblCategory = new Label();
            txtCategory = new TextBox();
            grpFunctions = new GroupBox();
            lblSearch = new Label();
            txtSearchName = new TextBox();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnSearch = new Button();
            dgvProducts = new DataGridView();
            grpProductInfo.SuspendLayout();
            grpFunctions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            SuspendLayout();
            // 
            // grpProductInfo
            // 
            grpProductInfo.Controls.Add(lblProductId);
            grpProductInfo.Controls.Add(txtProductId);
            grpProductInfo.Controls.Add(lblProductName);
            grpProductInfo.Controls.Add(txtProductName);
            grpProductInfo.Controls.Add(lblUnitPrice);
            grpProductInfo.Controls.Add(txtUnitPrice);
            grpProductInfo.Controls.Add(lblQuantity);
            grpProductInfo.Controls.Add(txtQuantity);
            grpProductInfo.Controls.Add(lblCategory);
            grpProductInfo.Controls.Add(txtCategory);
            grpProductInfo.Location = new Point(12, 12);
            grpProductInfo.Name = "grpProductInfo";
            grpProductInfo.Size = new Size(486, 264);
            grpProductInfo.TabIndex = 0;
            grpProductInfo.TabStop = false;
            grpProductInfo.Text = "Thông tin sản phẩm";
            // 
            // lblProductId
            // 
            lblProductId.AutoSize = true;
            lblProductId.Location = new Point(6, 29);
            lblProductId.Name = "lblProductId";
            lblProductId.Size = new Size(53, 20);
            lblProductId.TabIndex = 0;
            lblProductId.Text = "Mã SP:";
            // 
            // txtProductId
            // 
            txtProductId.Location = new Point(100, 26);
            txtProductId.Name = "txtProductId";
            txtProductId.Size = new Size(300, 27);
            txtProductId.TabIndex = 1;
            // 
            // lblProductName
            // 
            lblProductName.AutoSize = true;
            lblProductName.Location = new Point(4, 73);
            lblProductName.Name = "lblProductName";
            lblProductName.Size = new Size(55, 20);
            lblProductName.TabIndex = 2;
            lblProductName.Text = "Tên SP:";
            lblProductName.Click += lblProductName_Click;
            // 
            // txtProductName
            // 
            txtProductName.Location = new Point(100, 73);
            txtProductName.Name = "txtProductName";
            txtProductName.Size = new Size(300, 27);
            txtProductName.TabIndex = 3;
            // 
            // lblUnitPrice
            // 
            lblUnitPrice.AutoSize = true;
            lblUnitPrice.Location = new Point(6, 122);
            lblUnitPrice.Name = "lblUnitPrice";
            lblUnitPrice.Size = new Size(65, 20);
            lblUnitPrice.TabIndex = 4;
            lblUnitPrice.Text = "Đơn giá:";
            // 
            // txtUnitPrice
            // 
            txtUnitPrice.Location = new Point(100, 122);
            txtUnitPrice.Name = "txtUnitPrice";
            txtUnitPrice.Size = new Size(300, 27);
            txtUnitPrice.TabIndex = 5;
            // 
            // lblQuantity
            // 
            lblQuantity.AutoSize = true;
            lblQuantity.Location = new Point(6, 162);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(72, 20);
            lblQuantity.TabIndex = 6;
            lblQuantity.Text = "Số lượng:";
            lblQuantity.Click += lblQuantity_Click;
            // 
            // txtQuantity
            // 
            txtQuantity.Location = new Point(100, 162);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(300, 27);
            txtQuantity.TabIndex = 7;
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Location = new Point(6, 207);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(79, 20);
            lblCategory.TabIndex = 8;
            lblCategory.Text = "Danh mục:";
            lblCategory.Click += lblCategory_Click;
            // 
            // txtCategory
            // 
            txtCategory.Location = new Point(100, 207);
            txtCategory.Name = "txtCategory";
            txtCategory.Size = new Size(300, 27);
            txtCategory.TabIndex = 9;
            // 
            // grpFunctions
            // 
            grpFunctions.Controls.Add(lblSearch);
            grpFunctions.Controls.Add(txtSearchName);
            grpFunctions.Controls.Add(btnAdd);
            grpFunctions.Controls.Add(btnEdit);
            grpFunctions.Controls.Add(btnDelete);
            grpFunctions.Controls.Add(btnSearch);
            grpFunctions.Location = new Point(504, 29);
            grpFunctions.Name = "grpFunctions";
            grpFunctions.Size = new Size(482, 165);
            grpFunctions.TabIndex = 1;
            grpFunctions.TabStop = false;
            grpFunctions.Text = "Chức năng";
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(15, 30);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(74, 20);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "Tìm (Tên):";
            lblSearch.Click += lblSearch_Click;
            // 
            // txtSearchName
            // 
            txtSearchName.Location = new Point(95, 26);
            txtSearchName.Name = "txtSearchName";
            txtSearchName.Size = new Size(133, 27);
            txtSearchName.TabIndex = 1;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(5, 80);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(75, 35);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "Thêm";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += BtnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(104, 80);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(75, 35);
            btnEdit.TabIndex = 3;
            btnEdit.Text = "Sửa";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += BtnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(205, 80);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 35);
            btnDelete.TabIndex = 4;
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += BtnDelete_Click;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(270, 22);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(92, 41);
            btnSearch.TabIndex = 5;
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += BtnSearch_Click;
            // 
            // dgvProducts
            // 
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProducts.Location = new Point(12, 340);
            dgvProducts.Name = "dgvProducts";
            dgvProducts.RowHeadersWidth = 51;
            dgvProducts.Size = new Size(720, 202);
            dgvProducts.TabIndex = 2;
            dgvProducts.CellClick += DgvProducts_CellClick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1146, 570);
            Controls.Add(grpProductInfo);
            Controls.Add(grpFunctions);
            Controls.Add(dgvProducts);
            Name = "Form1";
            Text = "Quản Lý Danh Sách Sản Phẩm";
            Load += Form1_Load;
            grpProductInfo.ResumeLayout(false);
            grpProductInfo.PerformLayout();
            grpFunctions.ResumeLayout(false);
            grpFunctions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            ResumeLayout(false);
        }

        #endregion
    }
}
