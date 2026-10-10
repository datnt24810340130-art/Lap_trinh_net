namespace bai5._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnSignUp_Click(object sender, EventArgs e)
        {
            // Clear all previous errors
            epCheck.Clear();

            // Validate all fields
            bool isValid = true;

            // Validate username
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                epCheck.SetError(txtUsername, "Tên đăng nhập không được để trống!");
                isValid = false;
            }

            // Validate password
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                epCheck.SetError(txtPassword, "Mật khẩu không được để trống!");
                isValid = false;
            }

            // Validate confirm password
            if (string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                epCheck.SetError(txtConfirmPassword, "Xác nhận mật khẩu không được để trống!");
                isValid = false;
            }

            // Validate password match
            if (!string.IsNullOrWhiteSpace(txtPassword.Text) && !string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    epCheck.SetError(txtConfirmPassword, "Mật khẩu xác nhận không khớp!");
                    isValid = false;
                }
            }

            // Validate age
            int age = CalculateAge(dtpDateOfBirth.Value);
            if (age < 18)
            {
                epCheck.SetError(dtpDateOfBirth, "Phải từ 18 tuổi trở lên!");
                isValid = false;
            }

            // Validate terms acceptance
            if (!chkTerms.Checked)
            {
                epCheck.SetError(chkTerms, "Bạn phải đồng ý với các điều khoản dịch vụ!");
                isValid = false;
            }

            // If all validations pass
            if (isValid)
            {
                MessageBox.Show(
                    "Đăng ký tài khoản thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                ResetForm();
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void ResetForm()
        {
            epCheck.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtConfirmPassword.Clear();
            dtpDateOfBirth.Value = DateTime.Now;
            rbMale.Checked = false;
            rbFemale.Checked = false;
            chkTerms.Checked = false;
            txtUsername.Focus();
        }

        private int CalculateAge(DateTime dateOfBirth)
        {
            int age = DateTime.Now.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > DateTime.Now.AddYears(-age))
            {
                age--;
            }
            return age;
        }

        private void txtConfirmPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
