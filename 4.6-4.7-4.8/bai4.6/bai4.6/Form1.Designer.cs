namespace bai4._6
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private DataGridView dgvStudents;
        private TextBox txtStudentId;
        private TextBox txtFullName;
        private TextBox txtAge;
        private ComboBox cboGender;
        private PictureBox picAvatar;
        private Button btnChooseAvatar;
        private Button btnAdd;
        private Button btnDelete;
        private Button btnUpdate;
        private Button btnExportCsv;
        private Label lblStudentId;
        private Label lblFullName;
        private Label lblAge;
        private Label lblGender;
        private Label lblAvatar;

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
            components = new System.ComponentModel.Container();
            dgvStudents = new DataGridView();
            txtStudentId = new TextBox();
            txtFullName = new TextBox();
            txtAge = new TextBox();
            cboGender = new ComboBox();
            picAvatar = new PictureBox();
            btnChooseAvatar = new Button();
            btnAdd = new Button();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnExportCsv = new Button();
            lblStudentId = new Label();
            lblFullName = new Label();
            lblAge = new Label();
            lblGender = new Label();
            lblAvatar = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvStudents).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picAvatar).BeginInit();
            SuspendLayout();

            dgvStudents.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvStudents.AutoGenerateColumns = true;
            dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStudents.Location = new Point(12, 12);
            dgvStudents.MultiSelect = false;
            dgvStudents.Name = "dgvStudents";
            dgvStudents.ReadOnly = true;
            dgvStudents.RowHeadersVisible = false;
            dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStudents.Size = new Size(776, 220);
            dgvStudents.TabIndex = 0;

            lblStudentId.AutoSize = true;
            lblStudentId.Location = new Point(12, 250);
            lblStudentId.Text = "Mã SV:";
            txtStudentId.Location = new Point(95, 247);
            txtStudentId.Size = new Size(220, 23);

            lblFullName.AutoSize = true;
            lblFullName.Location = new Point(12, 289);
            lblFullName.Text = "Họ tên:";
            txtFullName.Location = new Point(95, 286);
            txtFullName.Size = new Size(220, 23);

            lblAge.AutoSize = true;
            lblAge.Location = new Point(12, 328);
            lblAge.Text = "Tuổi:";
            txtAge.Location = new Point(95, 325);
            txtAge.Size = new Size(220, 23);

            lblGender.AutoSize = true;
            lblGender.Location = new Point(12, 367);
            lblGender.Text = "Giới tính:";
            cboGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGender.FormattingEnabled = true;
            cboGender.Location = new Point(95, 364);
            cboGender.Size = new Size(220, 23);

            lblAvatar.AutoSize = true;
            lblAvatar.Location = new Point(360, 250);
            lblAvatar.Text = "Ảnh đại diện:";
            picAvatar.BorderStyle = BorderStyle.FixedSingle;
            picAvatar.Location = new Point(360, 275);
            picAvatar.Size = new Size(150, 150);
            picAvatar.SizeMode = PictureBoxSizeMode.Zoom;
            btnChooseAvatar.Location = new Point(525, 320);
            btnChooseAvatar.Size = new Size(110, 30);
            btnChooseAvatar.Text = "Chọn ảnh";
            btnChooseAvatar.UseVisualStyleBackColor = true;

            btnAdd.Location = new Point(12, 405);
            btnAdd.Size = new Size(95, 32);
            btnAdd.Text = "Thêm";
            btnAdd.UseVisualStyleBackColor = true;
            btnDelete.Location = new Point(115, 405);
            btnDelete.Size = new Size(95, 32);
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = true;
            btnUpdate.Location = new Point(218, 405);
            btnUpdate.Size = new Size(97, 32);
            btnUpdate.Text = "Cập nhật";
            btnUpdate.UseVisualStyleBackColor = true;
            btnExportCsv.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnExportCsv.Location = new Point(665, 405);
            btnExportCsv.Size = new Size(123, 32);
            btnExportCsv.Text = "Xuất CSV";
            btnExportCsv.UseVisualStyleBackColor = true;

            ClientSize = new Size(800, 455);
            Controls.Add(dgvStudents);
            Controls.Add(lblStudentId);
            Controls.Add(txtStudentId);
            Controls.Add(lblFullName);
            Controls.Add(txtFullName);
            Controls.Add(lblAge);
            Controls.Add(txtAge);
            Controls.Add(lblGender);
            Controls.Add(cboGender);
            Controls.Add(lblAvatar);
            Controls.Add(picAvatar);
            Controls.Add(btnChooseAvatar);
            Controls.Add(btnAdd);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnExportCsv);
            MinimumSize = new Size(816, 494);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý sinh viên";
            ((System.ComponentModel.ISupportInitialize)dgvStudents).EndInit();
            ((System.ComponentModel.ISupportInitialize)picAvatar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
