
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace bai4._2
{
    public partial class Form1 : Form
    {
        // Sửa Server nếu tên SQL Server trên máy bạn khác.
        private readonly string connectionString =
            @"Server=LAPTOP-UNQQA3HP;Database=LoginDB2;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";

        private readonly TextBox txtFullName = new();
        private readonly RadioButton rdoMale = new();
        private readonly RadioButton rdoFemale = new();
        private readonly DateTimePicker dtpBirthDate = new();
        private readonly MaskedTextBox mtxtPhone = new();
        private readonly ComboBox cboCourse = new();
        private readonly CheckBox chkConsultation = new();
        private readonly CheckBox chkPromotion = new();

        private readonly ErrorProvider errorProvider = new();

        public Form1()
        {
            InitializeComponent();
            InitializeRegistrationForm();
        }

        private void InitializeRegistrationForm()
        {
            Text = "Đăng ký khóa học";
            ClientSize = new Size(650, 500);
            MinimumSize = new Size(650, 500);
            StartPosition = FormStartPosition.CenterScreen;

            errorProvider.ContainerControl = this;

            var title = new Label
            {
                Text = "PHIẾU ĐĂNG KÝ KHÓA HỌC",
                AutoSize = true,
                Font = new Font("Arial", 16, FontStyle.Bold),
                Anchor = AnchorStyles.None,
                Margin = new Padding(0, 0, 0, 12)
            };

            var details = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 5,
                Padding = new Padding(0, 8, 0, 8)
            };

            details.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 145));
            details.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100));

            for (int i = 0; i < 5; i++)
            {
                details.RowStyles.Add(
                    new RowStyle(SizeType.Percent, 20));
            }

            // Họ và tên
            txtFullName.Name = "txtFullName";
            txtFullName.Dock = DockStyle.Fill;

            // Giới tính
            var genderPanel = new FlowLayoutPanel
            {
                AutoSize = true,
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };

            rdoMale.Name = "rdoMale";
            rdoMale.Text = "Nam";
            rdoMale.AutoSize = true;

            rdoFemale.Name = "rdoFemale";
            rdoFemale.Text = "Nữ";
            rdoFemale.AutoSize = true;

            genderPanel.Controls.AddRange(
                new Control[] { rdoMale, rdoFemale });

            // Ngày sinh
            dtpBirthDate.Name = "dtpBirthDate";
            dtpBirthDate.Format = DateTimePickerFormat.Short;
            dtpBirthDate.Dock = DockStyle.Left;
            dtpBirthDate.MaxDate = DateTime.Today;
            dtpBirthDate.Value = DateTime.Today;

            // Số điện thoại
            mtxtPhone.Name = "mtxtPhone";
            mtxtPhone.Mask = "(000) 000-0000";
            mtxtPhone.Dock = DockStyle.Left;

            // Khóa học
            cboCourse.Name = "cboCourse";
            cboCourse.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCourse.Dock = DockStyle.Fill;
            cboCourse.DisplayMember = nameof(Course.Name);
            cboCourse.ValueMember = nameof(Course.Code);

            cboCourse.DataSource = new List<Course>
            {
                new Course("NET01", "Lập trình C# và .NET"),
                new Course("WEB01", "Thiết kế và lập trình Web"),
                new Course("DB01", "Cơ sở dữ liệu SQL Server")
            };

            cboCourse.SelectedIndex = -1;

            AddField(details, "Họ và tên:", txtFullName, 0);
            AddField(details, "Giới tính:", genderPanel, 1);
            AddField(details, "Ngày sinh:", dtpBirthDate, 2);
            AddField(details, "Số điện thoại:", mtxtPhone, 3);
            AddField(details, "Khóa học:", cboCourse, 4);

            // Tùy chọn
            chkConsultation.Name = "chkConsultation";
            chkConsultation.Text = "Cần tư vấn thêm";
            chkConsultation.AutoSize = true;

            chkPromotion.Name = "chkPromotion";
            chkPromotion.Text = "Nhận thông tin ưu đãi";
            chkPromotion.AutoSize = true;

            var options = new FlowLayoutPanel
            {
                AutoSize = true,
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Margin = new Padding(145, 8, 0, 8)
            };

            options.Controls.AddRange(
                new Control[] { chkConsultation, chkPromotion });

            // Nút đăng ký
            var btnRegister = new Button
            {
                Name = "btnRegister",
                Text = "Đăng ký",
                AutoSize = true,
                Padding = new Padding(12, 4, 12, 4)
            };

            btnRegister.Click += RegisterButton_Click;
            AcceptButton = btnRegister;

            // Nút làm mới
            var btnReset = new Button
            {
                Name = "btnReset",
                Text = "Làm mới",
                AutoSize = true,
                Padding = new Padding(12, 4, 12, 4)
            };

            btnReset.Click += (sender, e) => ResetForm();

            var buttonPanel = new FlowLayoutPanel
            {
                AutoSize = true,
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft
            };

            buttonPanel.Controls.Add(btnRegister);
            buttonPanel.Controls.Add(btnReset);

            // Bố cục chính
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                Padding = new Padding(24)
            };

            layout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 48));
            layout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 48));
            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 48));

            layout.Controls.Add(title, 0, 0);
            layout.Controls.Add(details, 0, 1);
            layout.Controls.Add(options, 0, 2);
            layout.Controls.Add(buttonPanel, 0, 3);

            Controls.Add(layout);
        }

        private static void AddField(
            TableLayoutPanel layout,
            string labelText,
            Control control,
            int row)
        {
            var label = new Label
            {
                Text = labelText,
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                Margin = new Padding(3, 8, 3, 3)
            };

            control.Margin = new Padding(3, 5, 3, 5);

            layout.Controls.Add(label, 0, row);
            layout.Controls.Add(control, 1, row);
        }

        private void RegisterButton_Click(
            object? sender, EventArgs e)
        {
            errorProvider.Clear();

            string fullName = txtFullName.Text.Trim();

            // Kiểm tra họ tên
            if (string.IsNullOrWhiteSpace(fullName))
            {
                errorProvider.SetError(
                    txtFullName, "Vui lòng nhập họ và tên.");

                MessageBox.Show("Vui lòng nhập họ và tên.");
                txtFullName.Focus();
                return;
            }

            // Kiểm tra giới tính
            if (!rdoMale.Checked && !rdoFemale.Checked)
            {
                errorProvider.SetError(
                    rdoMale, "Vui lòng chọn giới tính.");

                MessageBox.Show("Vui lòng chọn giới tính.");
                return;
            }

            // Kiểm tra số điện thoại
            if (!mtxtPhone.MaskCompleted)
            {
                errorProvider.SetError(
                    mtxtPhone, "Vui lòng nhập đầy đủ số điện thoại.");

                MessageBox.Show("Vui lòng nhập đầy đủ số điện thoại.");
                mtxtPhone.Focus();
                return;
            }

            // Kiểm tra khóa học
            if (cboCourse.SelectedItem is not Course course)
            {
                errorProvider.SetError(
                    cboCourse, "Vui lòng chọn khóa học.");

                MessageBox.Show("Vui lòng chọn khóa học.");
                cboCourse.Focus();
                return;
            }

            string gender = rdoMale.Checked ? "Nam" : "Nữ";
            DateTime birthDate = dtpBirthDate.Value.Date;
            string phone = mtxtPhone.Text;

            bool needsConsultation = chkConsultation.Checked;
            bool receivePromotions = chkPromotion.Checked;

            try
            {
                int rows;

                using (SqlConnection conn =
                    new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = @"
                        INSERT INTO dbo.CourseRegistrations
                        (
                            FullName,
                            Gender,
                            BirthDate,
                            Phone,
                            CourseCode,
                            CourseName,
                            NeedsConsultation,
                            ReceivePromotions
                        )
                        VALUES
                        (
                            @FullName,
                            @Gender,
                            @BirthDate,
                            @Phone,
                            @CourseCode,
                            @CourseName,
                            @NeedsConsultation,
                            @ReceivePromotions
                        );";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add(
                            "@FullName", SqlDbType.NVarChar, 100)
                            .Value = fullName;

                        cmd.Parameters.Add(
                            "@Gender", SqlDbType.NVarChar, 10)
                            .Value = gender;

                        cmd.Parameters.Add(
                            "@BirthDate", SqlDbType.Date)
                            .Value = birthDate;

                        cmd.Parameters.Add(
                            "@Phone", SqlDbType.NVarChar, 20)
                            .Value = phone;

                        cmd.Parameters.Add(
                            "@CourseCode", SqlDbType.NVarChar, 20)
                            .Value = course.Code;

                        cmd.Parameters.Add(
                            "@CourseName", SqlDbType.NVarChar, 100)
                            .Value = course.Name;

                        cmd.Parameters.Add(
                            "@NeedsConsultation", SqlDbType.Bit)
                            .Value = needsConsultation;

                        cmd.Parameters.Add(
                            "@ReceivePromotions", SqlDbType.Bit)
                            .Value = receivePromotions;

                        // Chỉ thực thi INSERT một lần
                        rows = cmd.ExecuteNonQuery();
                    }

                    // Kiểm tra ngay trong kết nối hiện tại
                    using SqlCommand checkCmd = new SqlCommand(
                        "SELECT COUNT(*) FROM dbo.CourseRegistrations;",
                        conn);

                    int totalRows = Convert.ToInt32(
                        checkCmd.ExecuteScalar());

                    if (rows != 1)
                    {
                        MessageBox.Show(
                            $"SQL Server không xác nhận thêm được bản ghi.\n" +
                            $"Server: {conn.DataSource}\n" +
                            $"Database: {conn.Database}\n" +
                            $"Số dòng được thêm: {rows}",
                            "Chưa lưu dữ liệu",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return;
                    }

                    MessageBox.Show(
                        $"Đăng ký và lưu dữ liệu thành công!\n\n" +
                        $"Server: {conn.DataSource}\n" +
                        $"Database: {conn.Database}\n" +
                        $"Số dòng vừa thêm: {rows}\n" +
                        $"Tổng số đăng ký: {totalRows}",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    "Lỗi SQL Server:\n" + ex.Message,
                    "Lỗi lưu dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Đã xảy ra lỗi:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ResetForm()
        {
            txtFullName.Clear();
            rdoMale.Checked = false;
            rdoFemale.Checked = false;
            dtpBirthDate.Value = DateTime.Today;
            mtxtPhone.Clear();
            cboCourse.SelectedIndex = -1;
            chkConsultation.Checked = false;
            chkPromotion.Checked = false;
            errorProvider.Clear();
            txtFullName.Focus();
        }

        private sealed record Course(string Code, string Name);
    }
}
