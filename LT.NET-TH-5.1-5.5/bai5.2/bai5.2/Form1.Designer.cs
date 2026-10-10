namespace bai5._2
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.lblAvailableServices = new System.Windows.Forms.Label();
            this.lstAvailableServices = new System.Windows.Forms.ListBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.lblSelectedServices = new System.Windows.Forms.Label();
            this.lstSelectedServices = new System.Windows.Forms.ListBox();
            this.pnlSeparator = new System.Windows.Forms.Panel();
            this.lblTotalBeforeDiscount = new System.Windows.Forms.Label();
            this.txtTotalBeforeDiscount = new System.Windows.Forms.TextBox();
            this.lblDiscountPercent = new System.Windows.Forms.Label();
            this.txtDiscountPercent = new System.Windows.Forms.TextBox();
            this.lblFinalCost = new System.Windows.Forms.Label();
            this.txtFinalCost = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTitle.Location = new System.Drawing.Point(20, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(900, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "BẢNG TÍNH TIỀN DỊCH VỤ VÀ CHIẾT KHẤU ĐƠN HÀNG";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategory.Location = new System.Drawing.Point(20, 50);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(120, 19);
            this.lblCategory.TabIndex = 1;
            this.lblCategory.Text = "Chọn loại dịch vụ:";
            // 
            // cboCategory
            // 
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Location = new System.Drawing.Point(180, 50);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(300, 25);
            this.cboCategory.TabIndex = 2;
            this.cboCategory.SelectedIndexChanged += new System.EventHandler(this.cboCategory_SelectedIndexChanged);
            // 
            // lblAvailableServices
            // 
            this.lblAvailableServices.AutoSize = true;
            this.lblAvailableServices.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvailableServices.Location = new System.Drawing.Point(20, 90);
            this.lblAvailableServices.Name = "lblAvailableServices";
            this.lblAvailableServices.Size = new System.Drawing.Size(198, 19);
            this.lblAvailableServices.TabIndex = 3;
            this.lblAvailableServices.Text = "Danh sách dịch vụ có sẵn:";
            // 
            // lstAvailableServices
            // 
            this.lstAvailableServices.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstAvailableServices.FormattingEnabled = true;
            this.lstAvailableServices.ItemHeight = 17;
            this.lstAvailableServices.Location = new System.Drawing.Point(20, 120);
            this.lstAvailableServices.Name = "lstAvailableServices";
            this.lstAvailableServices.SelectionMode = System.Windows.Forms.SelectionMode.One;
            this.lstAvailableServices.Size = new System.Drawing.Size(350, 280);
            this.lstAvailableServices.TabIndex = 4;
            this.lstAvailableServices.DoubleClick += new System.EventHandler(this.lstAvailableServices_DoubleClick);
            // 
            // btnSelect
            // 
            this.btnSelect.BackColor = System.Drawing.SystemColors.Control;
            this.btnSelect.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelect.Location = new System.Drawing.Point(390, 180);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(50, 40);
            this.btnSelect.TabIndex = 5;
            this.btnSelect.Text = ">";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.SystemColors.Control;
            this.btnRemove.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.Location = new System.Drawing.Point(390, 240);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(50, 40);
            this.btnRemove.TabIndex = 6;
            this.btnRemove.Text = "<";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnClearAll
            // 
            this.btnClearAll.BackColor = System.Drawing.SystemColors.Control;
            this.btnClearAll.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearAll.Location = new System.Drawing.Point(390, 300);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(50, 40);
            this.btnClearAll.TabIndex = 7;
            this.btnClearAll.Text = "<<";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // lblSelectedServices
            // 
            this.lblSelectedServices.AutoSize = true;
            this.lblSelectedServices.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedServices.Location = new System.Drawing.Point(470, 90);
            this.lblSelectedServices.Name = "lblSelectedServices";
            this.lblSelectedServices.Size = new System.Drawing.Size(212, 19);
            this.lblSelectedServices.TabIndex = 8;
            this.lblSelectedServices.Text = "Danh sách dịch vụ đã chọn:";
            // 
            // lstSelectedServices
            // 
            this.lstSelectedServices.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstSelectedServices.FormattingEnabled = true;
            this.lstSelectedServices.ItemHeight = 17;
            this.lstSelectedServices.Location = new System.Drawing.Point(470, 120);
            this.lstSelectedServices.Name = "lstSelectedServices";
            this.lstSelectedServices.SelectionMode = System.Windows.Forms.SelectionMode.One;
            this.lstSelectedServices.Size = new System.Drawing.Size(350, 280);
            this.lstSelectedServices.TabIndex = 9;
            // 
            // pnlSeparator
            // 
            this.pnlSeparator.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlSeparator.Location = new System.Drawing.Point(20, 410);
            this.pnlSeparator.Name = "pnlSeparator";
            this.pnlSeparator.Size = new System.Drawing.Size(800, 2);
            this.pnlSeparator.TabIndex = 10;
            // 
            // lblTotalBeforeDiscount
            // 
            this.lblTotalBeforeDiscount.AutoSize = true;
            this.lblTotalBeforeDiscount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalBeforeDiscount.Location = new System.Drawing.Point(20, 430);
            this.lblTotalBeforeDiscount.Name = "lblTotalBeforeDiscount";
            this.lblTotalBeforeDiscount.Size = new System.Drawing.Size(153, 19);
            this.lblTotalBeforeDiscount.TabIndex = 11;
            this.lblTotalBeforeDiscount.Text = "Tổng tiền chưa giảm:";
            // 
            // txtTotalBeforeDiscount
            // 
            this.txtTotalBeforeDiscount.BackColor = System.Drawing.SystemColors.Window;
            this.txtTotalBeforeDiscount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalBeforeDiscount.Location = new System.Drawing.Point(200, 430);
            this.txtTotalBeforeDiscount.Name = "txtTotalBeforeDiscount";
            this.txtTotalBeforeDiscount.ReadOnly = true;
            this.txtTotalBeforeDiscount.Size = new System.Drawing.Size(300, 25);
            this.txtTotalBeforeDiscount.TabIndex = 12;
            this.txtTotalBeforeDiscount.Text = "0 VNĐ";
            // 
            // lblDiscountPercent
            // 
            this.lblDiscountPercent.AutoSize = true;
            this.lblDiscountPercent.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiscountPercent.Location = new System.Drawing.Point(20, 475);
            this.lblDiscountPercent.Name = "lblDiscountPercent";
            this.lblDiscountPercent.Size = new System.Drawing.Size(167, 19);
            this.lblDiscountPercent.TabIndex = 13;
            this.lblDiscountPercent.Text = "Tỷ lệ chiết khấu (%):";
            // 
            // txtDiscountPercent
            // 
            this.txtDiscountPercent.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscountPercent.Location = new System.Drawing.Point(200, 475);
            this.txtDiscountPercent.Name = "txtDiscountPercent";
            this.txtDiscountPercent.Size = new System.Drawing.Size(300, 25);
            this.txtDiscountPercent.TabIndex = 14;
            this.txtDiscountPercent.Text = "0";
            this.txtDiscountPercent.TextChanged += new System.EventHandler(this.txtDiscountPercent_TextChanged);
            // 
            // lblFinalCost
            // 
            this.lblFinalCost.AutoSize = true;
            this.lblFinalCost.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFinalCost.Location = new System.Drawing.Point(20, 520);
            this.lblFinalCost.Name = "lblFinalCost";
            this.lblFinalCost.Size = new System.Drawing.Size(166, 19);
            this.lblFinalCost.TabIndex = 15;
            this.lblFinalCost.Text = "Thành tiền thanh toán:";
            // 
            // txtFinalCost
            // 
            this.txtFinalCost.BackColor = System.Drawing.Color.LightYellow;
            this.txtFinalCost.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFinalCost.Location = new System.Drawing.Point(200, 520);
            this.txtFinalCost.Name = "txtFinalCost";
            this.txtFinalCost.ReadOnly = true;
            this.txtFinalCost.Size = new System.Drawing.Size(300, 25);
            this.txtFinalCost.TabIndex = 16;
            this.txtFinalCost.Text = "0 VNĐ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(950, 750);
            this.Controls.Add(this.txtFinalCost);
            this.Controls.Add(this.lblFinalCost);
            this.Controls.Add(this.txtDiscountPercent);
            this.Controls.Add(this.lblDiscountPercent);
            this.Controls.Add(this.txtTotalBeforeDiscount);
            this.Controls.Add(this.lblTotalBeforeDiscount);
            this.Controls.Add(this.pnlSeparator);
            this.Controls.Add(this.lstSelectedServices);
            this.Controls.Add(this.lblSelectedServices);
            this.Controls.Add(this.btnClearAll);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.lstAvailableServices);
            this.Controls.Add(this.lblAvailableServices);
            this.Controls.Add(this.cboCategory);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bảng Tính Tiền Dịch Vụ Và Chiết Khấu Đơn Hàng";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.Label lblAvailableServices;
        private System.Windows.Forms.ListBox lstAvailableServices;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.Label lblSelectedServices;
        private System.Windows.Forms.ListBox lstSelectedServices;
        private System.Windows.Forms.Panel pnlSeparator;
        private System.Windows.Forms.Label lblTotalBeforeDiscount;
        private System.Windows.Forms.TextBox txtTotalBeforeDiscount;
        private System.Windows.Forms.Label lblDiscountPercent;
        private System.Windows.Forms.TextBox txtDiscountPercent;
        private System.Windows.Forms.Label lblFinalCost;
        private System.Windows.Forms.TextBox txtFinalCost;
    }
}
