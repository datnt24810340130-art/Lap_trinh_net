namespace baitap4._2
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
            lblFullName = new Label();
            label2 = new Label();
            lblBirthDate = new Label();
            lblCourse = new Label();
            lblGender = new Label();
            lblLanguage = new Label();
            txtFullName = new TextBox();
            mtxtPhone = new MaskedTextBox();
            dtpBirthDate = new DateTimePicker();
            cboCourse = new ComboBox();
            rdoMale = new RadioButton();
            rdoFemale = new RadioButton();
            chkCSharp = new CheckBox();
            chkJava = new CheckBox();
            chkPython = new CheckBox();
            btnRegister = new Button();
            SuspendLayout();
            // 
            // lblFullName
            // 
            lblFullName.AutoSize = true;
            lblFullName.Location = new Point(155, 69);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(57, 20);
            lblFullName.TabIndex = 0;
            lblFullName.Text = "Họ tên:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(155, 117);
            label2.Name = "label2";
            label2.Size = new Size(100, 20);
            label2.TabIndex = 0;
            label2.Text = "Số điện thoại:";
            // 
            // lblBirthDate
            // 
            lblBirthDate.AutoSize = true;
            lblBirthDate.Location = new Point(155, 164);
            lblBirthDate.Name = "lblBirthDate";
            lblBirthDate.Size = new Size(77, 20);
            lblBirthDate.TabIndex = 0;
            lblBirthDate.Text = "Ngày sinh:";
            // 
            // lblCourse
            // 
            lblCourse.AutoSize = true;
            lblCourse.Location = new Point(155, 202);
            lblCourse.Name = "lblCourse";
            lblCourse.Size = new Size(74, 20);
            lblCourse.TabIndex = 0;
            lblCourse.Text = "Khóa học:";
            // 
            // lblGender
            // 
            lblGender.AutoSize = true;
            lblGender.Location = new Point(155, 250);
            lblGender.Name = "lblGender";
            lblGender.Size = new Size(68, 20);
            lblGender.TabIndex = 0;
            lblGender.Text = "Giới tính:";
            // 
            // lblLanguage
            // 
            lblLanguage.AutoSize = true;
            lblLanguage.Location = new Point(155, 291);
            lblLanguage.Name = "lblLanguage";
            lblLanguage.Size = new Size(76, 20);
            lblLanguage.TabIndex = 0;
            lblLanguage.Text = "Ngôn ngữ";
            // 
            // txtFullName
            // 
            txtFullName.Location = new Point(266, 69);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(193, 27);
            txtFullName.TabIndex = 1;
            // 
            // mtxtPhone
            // 
            mtxtPhone.Location = new Point(266, 110);
            mtxtPhone.Mask = "(000) 000-0000";
            mtxtPhone.Name = "mtxtPhone";
            mtxtPhone.Size = new Size(97, 27);
            mtxtPhone.TabIndex = 2;
            // 
            // dtpBirthDate
            // 
            dtpBirthDate.CustomFormat = "dd/MM/yyyy";
            dtpBirthDate.Format = DateTimePickerFormat.Custom;
            dtpBirthDate.Location = new Point(255, 164);
            dtpBirthDate.Name = "dtpBirthDate";
            dtpBirthDate.Size = new Size(250, 27);
            dtpBirthDate.TabIndex = 3;
            dtpBirthDate.ValueChanged += dtpBirthDate_ValueChanged;
            // 
            // cboCourse
            // 
            cboCourse.FormattingEnabled = true;
            cboCourse.Location = new Point(255, 202);
            cboCourse.Name = "cboCourse";
            cboCourse.Size = new Size(169, 28);
            cboCourse.TabIndex = 4;
            // 
            // rdoMale
            // 
            rdoMale.AutoSize = true;
            rdoMale.Location = new Point(263, 248);
            rdoMale.Name = "rdoMale";
            rdoMale.Size = new Size(62, 24);
            rdoMale.TabIndex = 5;
            rdoMale.Text = "Nam";
            rdoMale.UseVisualStyleBackColor = true;
            // 
            // rdoFemale
            // 
            rdoFemale.AutoSize = true;
            rdoFemale.Location = new Point(340, 248);
            rdoFemale.Name = "rdoFemale";
            rdoFemale.Size = new Size(50, 24);
            rdoFemale.TabIndex = 5;
            rdoFemale.Text = "Nữ";
            rdoFemale.UseVisualStyleBackColor = true;
            // 
            // chkCSharp
            // 
            chkCSharp.AutoSize = true;
            chkCSharp.Location = new Point(255, 291);
            chkCSharp.Name = "chkCSharp";
            chkCSharp.Size = new Size(49, 24);
            chkCSharp.TabIndex = 6;
            chkCSharp.Text = "C#";
            chkCSharp.UseVisualStyleBackColor = true;
            // 
            // chkJava
            // 
            chkJava.AutoSize = true;
            chkJava.Location = new Point(304, 291);
            chkJava.Name = "chkJava";
            chkJava.Size = new Size(59, 24);
            chkJava.TabIndex = 6;
            chkJava.Text = "Java";
            chkJava.UseVisualStyleBackColor = true;
            // 
            // chkPython
            // 
            chkPython.AutoSize = true;
            chkPython.Location = new Point(375, 291);
            chkPython.Name = "chkPython";
            chkPython.Size = new Size(76, 24);
            chkPython.TabIndex = 6;
            chkPython.Text = "Python";
            chkPython.UseVisualStyleBackColor = true;
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(263, 332);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(114, 41);
            btnRegister.TabIndex = 7;
            btnRegister.Text = "Đăng ký";
            btnRegister.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnRegister);
            Controls.Add(chkPython);
            Controls.Add(chkJava);
            Controls.Add(chkCSharp);
            Controls.Add(rdoFemale);
            Controls.Add(rdoMale);
            Controls.Add(cboCourse);
            Controls.Add(dtpBirthDate);
            Controls.Add(mtxtPhone);
            Controls.Add(txtFullName);
            Controls.Add(lblLanguage);
            Controls.Add(lblGender);
            Controls.Add(lblCourse);
            Controls.Add(lblBirthDate);
            Controls.Add(label2);
            Controls.Add(lblFullName);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblFullName;
        private Label label2;
        private Label lblBirthDate;
        private Label lblCourse;
        private Label lblGender;
        private Label lblLanguage;
        private TextBox txtFullName;
        private TextBox lblPhone;
        private MaskedTextBox mtxtPhone;
        private DateTimePicker dtpBirthDate;
        private ComboBox cboCourse;
        private RadioButton rdoMale;
        private RadioButton rdoFemale;
        private CheckBox chkCSharp;
        private CheckBox chkJava;
        private CheckBox chkPython;
        private Button btnRegister;
    }
}
