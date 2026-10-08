namespace bai1ontapwindownform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            // Validate unit price
            if (!decimal.TryParse(txtUnitPrice.Text.Trim(), out var unitPrice) || unitPrice < 0)
            {
                MessageBox.Show("Vui lòng nhập Đơn giá hợp lệ (số không âm).", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUnitPrice.Focus();
                return;
            }

            // Validate quantity
            if (!int.TryParse(txtQuantity.Text.Trim(), out var quantity) || quantity < 0)
            {
                MessageBox.Show("Vui lòng nhập Số lượng khách hợp lệ (số nguyên không âm).", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuantity.Focus();
                return;
            }

            // Validate discount
            if (!decimal.TryParse(txtDiscount.Text.Trim(), out var discount) || discount < 0 || discount > 100)
            {
                MessageBox.Show("Vui lòng nhập Mã giảm giá hợp lệ (phần trăm từ 0 đến 100).", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiscount.Focus();
                return;
            }

            var total = (unitPrice * quantity) * (100 - discount) / 100m;
            lblTotal.Text = $"Tổng tiền: {total:C2}";
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtUnitPrice.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtDiscount.Text = string.Empty;
            lblTotal.Text = "Tổng tiền: 0.00";
            txtUnitPrice.Focus();
        }
    }
}
