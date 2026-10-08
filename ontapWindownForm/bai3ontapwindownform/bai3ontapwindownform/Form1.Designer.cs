namespace bai3ontapwindownform
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox groupBoxLeft;
        private System.Windows.Forms.GroupBox groupBoxRight;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox cboUnit;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDeleteRow;
        private System.Windows.Forms.Button btnDeleteAll;
        private System.Windows.Forms.ListView lvItems;

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
            groupBoxLeft = new GroupBox();
            lblCode = new Label();
            txtCode = new TextBox();
            lblName = new Label();
            txtName = new TextBox();
            lblUnit = new Label();
            cboUnit = new ComboBox();
            lblPrice = new Label();
            txtPrice = new TextBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDeleteRow = new Button();
            btnDeleteAll = new Button();
            groupBoxRight = new GroupBox();
            lvItems = new ListView();
            groupBoxLeft.SuspendLayout();
            groupBoxRight.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxLeft
            // 
            groupBoxLeft.Controls.Add(lblCode);
            groupBoxLeft.Controls.Add(txtCode);
            groupBoxLeft.Controls.Add(lblName);
            groupBoxLeft.Controls.Add(txtName);
            groupBoxLeft.Controls.Add(lblUnit);
            groupBoxLeft.Controls.Add(cboUnit);
            groupBoxLeft.Controls.Add(lblPrice);
            groupBoxLeft.Controls.Add(txtPrice);
            groupBoxLeft.Controls.Add(btnAdd);
            groupBoxLeft.Controls.Add(btnUpdate);
            groupBoxLeft.Controls.Add(btnDeleteRow);
            groupBoxLeft.Controls.Add(btnDeleteAll);
            groupBoxLeft.Location = new Point(12, 12);
            groupBoxLeft.Name = "groupBoxLeft";
            groupBoxLeft.Size = new Size(350, 426);
            groupBoxLeft.TabIndex = 0;
            groupBoxLeft.TabStop = false;
            groupBoxLeft.Text = "Nhập liệu";
            // 
            // lblCode
            // 
            lblCode.AutoSize = true;
            lblCode.Location = new Point(6, 49);
            lblCode.Name = "lblCode";
            lblCode.Size = new Size(72, 20);
            lblCode.TabIndex = 0;
            lblCode.Text = "Mã vật tư";
            lblCode.Click += lblCode_Click;
            // 
            // txtCode
            // 
            txtCode.Location = new Point(100, 46);
            txtCode.Name = "txtCode";
            txtCode.Size = new Size(220, 27);
            txtCode.TabIndex = 1;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(6, 101);
            lblName.Name = "lblName";
            lblName.Size = new Size(74, 20);
            lblName.TabIndex = 2;
            lblName.Text = "Tên vật tư";
            // 
            // txtName
            // 
            txtName.Location = new Point(100, 101);
            txtName.Name = "txtName";
            txtName.Size = new Size(220, 27);
            txtName.TabIndex = 3;
            // 
            // lblUnit
            // 
            lblUnit.AutoSize = true;
            lblUnit.Location = new Point(6, 160);
            lblUnit.Name = "lblUnit";
            lblUnit.Size = new Size(81, 20);
            lblUnit.TabIndex = 4;
            lblUnit.Text = "Đơn vị tính";
            // 
            // cboUnit
            // 
            cboUnit.Location = new Point(100, 157);
            cboUnit.Name = "cboUnit";
            cboUnit.Size = new Size(120, 28);
            cboUnit.TabIndex = 5;
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Location = new Point(6, 222);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(62, 20);
            lblPrice.TabIndex = 6;
            lblPrice.Text = "Đơn giá";
            lblPrice.Click += lblPrice_Click;
            // 
            // txtPrice
            // 
            txtPrice.Location = new Point(100, 215);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(120, 27);
            txtPrice.TabIndex = 7;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(12, 287);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(90, 30);
            btnAdd.TabIndex = 8;
            btnAdd.Text = "Thêm mới";
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(108, 287);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(90, 30);
            btnUpdate.TabIndex = 9;
            btnUpdate.Text = "Cập nhật";
            // 
            // btnDeleteRow
            // 
            btnDeleteRow.Location = new Point(212, 287);
            btnDeleteRow.Name = "btnDeleteRow";
            btnDeleteRow.Size = new Size(90, 30);
            btnDeleteRow.TabIndex = 10;
            btnDeleteRow.Text = "Xóa dòng";
            // 
            // btnDeleteAll
            // 
            btnDeleteAll.Location = new Point(12, 348);
            btnDeleteAll.Name = "btnDeleteAll";
            btnDeleteAll.Size = new Size(290, 30);
            btnDeleteAll.TabIndex = 11;
            btnDeleteAll.Text = "Xóa toàn bộ";
            // 
            // groupBoxRight
            // 
            groupBoxRight.Controls.Add(lvItems);
            groupBoxRight.Location = new Point(368, 12);
            groupBoxRight.Name = "groupBoxRight";
            groupBoxRight.Size = new Size(679, 426);
            groupBoxRight.TabIndex = 1;
            groupBoxRight.TabStop = false;
            groupBoxRight.Text = "Danh sách";
            groupBoxRight.Enter += groupBoxRight_Enter;
            // 
            // lvItems
            // 
            lvItems.FullRowSelect = true;
            lvItems.Location = new Point(6, 49);
            lvItems.Name = "lvItems";
            lvItems.Size = new Size(921, 418);
            lvItems.TabIndex = 0;
            lvItems.UseCompatibleStateImageBehavior = false;
            lvItems.View = View.Details;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1325, 596);
            Controls.Add(groupBoxLeft);
            Controls.Add(groupBoxRight);
            Name = "Form1";
            Text = "Quản lý danh mục Vật tư / Linh kiện";
            groupBoxLeft.ResumeLayout(false);
            groupBoxLeft.PerformLayout();
            groupBoxRight.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
    }
}
