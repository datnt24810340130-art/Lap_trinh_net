namespace bai2ontapwindownform
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            lblTicketId = new Label();
            txtTicketId = new TextBox();
            lblRequester = new Label();
            txtRequester = new TextBox();
            lblDate = new Label();
            dtpDate = new DateTimePicker();
            grpPriority = new GroupBox();
            rbUrgent = new RadioButton();
            rbMedium = new RadioButton();
            rbLow = new RadioButton();
            lblType = new Label();
            cbType = new ComboBox();
            grpDevices = new GroupBox();
            chkPhone = new CheckBox();
            chkPrinter = new CheckBox();
            chkLaptop = new CheckBox();
            chkDesktop = new CheckBox();
            picError = new PictureBox();
            btnLoadImage = new Button();
            btnSubmit = new Button();
            btnReset = new Button();
            openFileDialog = new OpenFileDialog();
            grpPriority.SuspendLayout();
            grpDevices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picError).BeginInit();
            SuspendLayout();
            // 
            // lblTicketId
            // 
            lblTicketId.AutoSize = true;
            lblTicketId.Location = new Point(12, 15);
            lblTicketId.Name = "lblTicketId";
            lblTicketId.Size = new Size(74, 20);
            lblTicketId.TabIndex = 0;
            lblTicketId.Text = "Mã phiếu:";
            // 
            // txtTicketId
            // 
            txtTicketId.Location = new Point(126, 12);
            txtTicketId.Name = "txtTicketId";
            txtTicketId.Size = new Size(200, 27);
            txtTicketId.TabIndex = 1;
            // 
            // lblRequester
            // 
            lblRequester.AutoSize = true;
            lblRequester.Location = new Point(12, 50);
            lblRequester.Name = "lblRequester";
            lblRequester.Size = new Size(108, 20);
            lblRequester.TabIndex = 2;
            lblRequester.Text = "Người yêu cầu:";
            // 
            // txtRequester
            // 
            txtRequester.Location = new Point(126, 47);
            txtRequester.Name = "txtRequester";
            txtRequester.Size = new Size(200, 27);
            txtRequester.TabIndex = 3;
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Location = new Point(12, 85);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(108, 20);
            lblDate.TabIndex = 4;
            lblDate.Text = "Ngày ghi nhận:";
            // 
            // dtpDate
            // 
            dtpDate.Location = new Point(126, 87);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(200, 27);
            dtpDate.TabIndex = 5;
            // 
            // grpPriority
            // 
            grpPriority.Controls.Add(rbUrgent);
            grpPriority.Controls.Add(rbMedium);
            grpPriority.Controls.Add(rbLow);
            grpPriority.Location = new Point(12, 120);
            grpPriority.Name = "grpPriority";
            grpPriority.Size = new Size(298, 55);
            grpPriority.TabIndex = 6;
            grpPriority.TabStop = false;
            grpPriority.Text = "Mức độ ưu tiên";
            // 
            // rbUrgent
            // 
            rbUrgent.AutoSize = true;
            rbUrgent.Location = new Point(210, 22);
            rbUrgent.Name = "rbUrgent";
            rbUrgent.Size = new Size(91, 24);
            rbUrgent.TabIndex = 2;
            rbUrgent.Text = "Khẩn cấp";
            rbUrgent.UseVisualStyleBackColor = true;
            // 
            // rbMedium
            // 
            rbMedium.AutoSize = true;
            rbMedium.Location = new Point(105, 22);
            rbMedium.Name = "rbMedium";
            rbMedium.Size = new Size(100, 24);
            rbMedium.TabIndex = 1;
            rbMedium.Text = "Trung bình";
            rbMedium.UseVisualStyleBackColor = true;
            // 
            // rbLow
            // 
            rbLow.AutoSize = true;
            rbLow.Checked = true;
            rbLow.Location = new Point(15, 22);
            rbLow.Name = "rbLow";
            rbLow.Size = new Size(63, 24);
            rbLow.TabIndex = 0;
            rbLow.TabStop = true;
            rbLow.Text = "Thấp";
            rbLow.UseVisualStyleBackColor = true;
            // 
            // lblType
            // 
            lblType.AutoSize = true;
            lblType.Location = new Point(12, 190);
            lblType.Name = "lblType";
            lblType.Size = new Size(79, 20);
            lblType.TabIndex = 7;
            lblType.Text = "Loại sự cố:";
            // 
            // cbType
            // 
            cbType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbType.FormattingEnabled = true;
            cbType.Items.AddRange(new object[] { "Phần cứng", "Phần mềm", "Mạng", "Tài khoản" });
            cbType.Location = new Point(110, 187);
            cbType.Name = "cbType";
            cbType.Size = new Size(200, 28);
            cbType.TabIndex = 8;
            // 
            // grpDevices
            // 
            grpDevices.Controls.Add(chkPhone);
            grpDevices.Controls.Add(chkPrinter);
            grpDevices.Controls.Add(chkLaptop);
            grpDevices.Controls.Add(chkDesktop);
            grpDevices.Location = new Point(12, 225);
            grpDevices.Name = "grpDevices";
            grpDevices.Size = new Size(298, 100);
            grpDevices.TabIndex = 9;
            grpDevices.TabStop = false;
            grpDevices.Text = "Thiết bị ảnh hưởng";
            // 
            // chkPhone
            // 
            chkPhone.AutoSize = true;
            chkPhone.Location = new Point(15, 68);
            chkPhone.Name = "chkPhone";
            chkPhone.Size = new Size(100, 24);
            chkPhone.TabIndex = 3;
            chkPhone.Text = "Điện thoại";
            chkPhone.UseVisualStyleBackColor = true;
            // 
            // chkPrinter
            // 
            chkPrinter.AutoSize = true;
            chkPrinter.Location = new Point(150, 70);
            chkPrinter.Name = "chkPrinter";
            chkPrinter.Size = new Size(75, 24);
            chkPrinter.TabIndex = 2;
            chkPrinter.Text = "Máy in";
            chkPrinter.UseVisualStyleBackColor = true;
            // 
            // chkLaptop
            // 
            chkLaptop.AutoSize = true;
            chkLaptop.Location = new Point(15, 40);
            chkLaptop.Name = "chkLaptop";
            chkLaptop.Size = new Size(78, 24);
            chkLaptop.TabIndex = 1;
            chkLaptop.Text = "Laptop";
            chkLaptop.UseVisualStyleBackColor = true;
            chkLaptop.CheckedChanged += chkLaptop_CheckedChanged;
            // 
            // chkDesktop
            // 
            chkDesktop.AutoSize = true;
            chkDesktop.Location = new Point(150, 40);
            chkDesktop.Name = "chkDesktop";
            chkDesktop.Size = new Size(117, 24);
            chkDesktop.TabIndex = 0;
            chkDesktop.Text = "Máy tính bàn";
            chkDesktop.UseVisualStyleBackColor = true;
            chkDesktop.CheckedChanged += chkDesktop_CheckedChanged;
            // 
            // picError
            // 
            picError.BorderStyle = BorderStyle.FixedSingle;
            picError.Location = new Point(340, 12);
            picError.Name = "picError";
            picError.Size = new Size(430, 300);
            picError.SizeMode = PictureBoxSizeMode.StretchImage;
            picError.TabIndex = 10;
            picError.TabStop = false;
            // 
            // btnLoadImage
            // 
            btnLoadImage.Location = new Point(340, 318);
            btnLoadImage.Name = "btnLoadImage";
            btnLoadImage.Size = new Size(120, 25);
            btnLoadImage.TabIndex = 11;
            btnLoadImage.Text = "Tải ảnh lỗi";
            btnLoadImage.UseVisualStyleBackColor = true;
            btnLoadImage.Click += btnLoadImage_Click;
            // 
            // btnSubmit
            // 
            btnSubmit.Location = new Point(580, 350);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new Size(90, 30);
            btnSubmit.TabIndex = 12;
            btnSubmit.Text = "Gửi yêu cầu";
            btnSubmit.UseVisualStyleBackColor = true;
            btnSubmit.Click += btnSubmit_Click;
            // 
            // btnReset
            // 
            btnReset.Location = new Point(690, 350);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(90, 30);
            btnReset.TabIndex = 13;
            btnReset.Text = "Nhập lại";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += btnReset_Click;
            // 
            // openFileDialog
            // 
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 420);
            Controls.Add(btnReset);
            Controls.Add(btnSubmit);
            Controls.Add(btnLoadImage);
            Controls.Add(picError);
            Controls.Add(grpDevices);
            Controls.Add(cbType);
            Controls.Add(lblType);
            Controls.Add(grpPriority);
            Controls.Add(dtpDate);
            Controls.Add(lblDate);
            Controls.Add(txtRequester);
            Controls.Add(lblRequester);
            Controls.Add(txtTicketId);
            Controls.Add(lblTicketId);
            Name = "Form1";
            Text = "Form Tiếp nhận & Phân loại sự cố IT";
            grpPriority.ResumeLayout(false);
            grpPriority.PerformLayout();
            grpDevices.ResumeLayout(false);
            grpDevices.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picError).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label lblTicketId;
        private System.Windows.Forms.TextBox txtTicketId;
        private System.Windows.Forms.Label lblRequester;
        private System.Windows.Forms.TextBox txtRequester;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.GroupBox grpPriority;
        private System.Windows.Forms.RadioButton rbUrgent;
        private System.Windows.Forms.RadioButton rbMedium;
        private System.Windows.Forms.RadioButton rbLow;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.GroupBox grpDevices;
        private System.Windows.Forms.CheckBox chkPhone;
        private System.Windows.Forms.CheckBox chkPrinter;
        private System.Windows.Forms.CheckBox chkLaptop;
        private System.Windows.Forms.CheckBox chkDesktop;
        private System.Windows.Forms.PictureBox picError;
        private System.Windows.Forms.Button btnLoadImage;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.OpenFileDialog openFileDialog;

        #endregion
    }
}
