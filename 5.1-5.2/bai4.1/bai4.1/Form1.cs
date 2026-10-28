
using Microsoft.Data.SqlClient;
using System;
using System.Windows.Forms;

namespace bai4._1
{
    public partial class Form1 : Form
    {
        private readonly string connectionString =
            @"Server=LAPTOP-UNQQA3HP;Database=LoginDB1;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";

        public Form1()
        {
            InitializeComponent();

            txtPassword.UseSystemPasswordChar = true;
            AcceptButton = btnLogin;
            CancelButton = btnExit;
        }

        private void chkShowPassword_CheckedChanged(
            object? sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar =
                !chkShowPassword.Checked;
        }

        private void btnLogin_Click(object? sender, EventArgs e)
        {
            errorProvider.Clear();

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            bool isValid = true;

            if (string.IsNullOrWhiteSpace(username))
            {
                errorProvider.SetError(
                    txtUsername,
                    "Vui lòng nhập tên đăng nhập.");

                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                errorProvider.SetError(
                    txtPassword,
                    "Vui lòng nhập mật khẩu.");

                isValid = false;
            }

            if (!isValid)
                return;

            try
            {
                // Băm mật khẩu trước khi lưu
                string passwordHash =
                    BCrypt.Net.BCrypt.HashPassword(password);

                using (SqlConnection conn =
                       new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = @"
                        INSERT INTO dbo.Users
                            (Username, PasswordHash)
                        VALUES
                            (@Username, @PasswordHash)";

                    using (SqlCommand cmd =
                           new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue(
                            "@Username", username);

                        cmd.Parameters.AddWithValue(
                            "@PasswordHash", passwordHash);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show(
                    "Đăng ký tài khoản thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                txtUsername.Clear();
                txtPassword.Clear();
                chkShowPassword.Checked = false;
                txtUsername.Focus();
            }
            catch (SqlException ex) when (
                ex.Number == 2601 || ex.Number == 2627)
            {
                errorProvider.SetError(
                    txtUsername,
                    "Tên đăng nhập đã tồn tại.");

                txtUsername.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể lưu dữ liệu vào SQL Server.\n" +
                    "Chi tiết: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(
            object? sender, EventArgs e)
        {
            Close();
        }
    }
}
