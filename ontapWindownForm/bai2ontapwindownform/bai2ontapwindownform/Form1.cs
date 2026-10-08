namespace bai2ontapwindownform
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    
                    if (picError.Image != null)
                    {
                        var old = picError.Image;
                        picError.Image = null;
                        old.Dispose();
                    }
                    picError.Image = Image.FromFile(openFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể tải ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            var id = txtTicketId.Text.Trim();
            var requester = txtRequester.Text.Trim();
            var date = dtpDate.Value.ToString("g");

            string priority = rbLow.Checked ? "Thấp" : rbMedium.Checked ? "Trung bình" : "Khẩn cấp";

            var type = cbType.SelectedItem != null ? cbType.SelectedItem.ToString() : "(Không chọn)";

            var devices = new System.Collections.Generic.List<string>();
            if (chkDesktop.Checked) devices.Add("Máy tính bàn");
            if (chkLaptop.Checked) devices.Add("Laptop");
            if (chkPrinter.Checked) devices.Add("Máy in");
            if (chkPhone.Checked) devices.Add("Điện thoại");
            var devicesText = devices.Count > 0 ? string.Join(", ", devices) : "(Không chọn)";

            var hasImage = picError.Image != null ? "Có" : "Không";

            var summary =
                $"Mã phiếu: {id}\n" +
                $"Người yêu cầu: {requester}\n" +
                $"Ngày ghi nhận: {date}\n" +
                $"Mức độ ưu tiên: {priority}\n" +
                $"Loại sự cố: {type}\n" +
                $"Thiết bị ảnh hưởng: {devicesText}\n" +
                $"Ảnh lỗi: {hasImage}";

            MessageBox.Show(summary, "Tóm tắt phiếu yêu cầu", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtTicketId.Clear();
            txtRequester.Clear();
            dtpDate.Value = DateTime.Now;
            rbLow.Checked = true;
            cbType.SelectedIndex = -1;
            chkDesktop.Checked = chkLaptop.Checked = chkPrinter.Checked = chkPhone.Checked = false;
            if (picError.Image != null)
            {
                var old = picError.Image;
                picError.Image = null;
                old.Dispose();
            }
        }

        private void chkDesktop_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkLaptop_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
