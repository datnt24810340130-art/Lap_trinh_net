using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace bai4ontapwindownform
{
    public partial class Form1 : Form
    {
        private enum SeatState
        {
            Empty,
            Selected,
            Reserved
        }

        private TableLayoutPanel tblSeats;
        private ComboBox cboTimeSlot;
        private Label lblSelectedCount;
        private Label lblSubtotal;
        private Button btnConfirm;
        private Button btnClear;

        private readonly Dictionary<Button, SeatState> seatStates
            = new Dictionary<Button, SeatState>();

        private readonly CultureInfo viVN =
            CultureInfo.GetCultureInfo("vi-VN");

        private decimal seatPrice = 100000m;

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Tránh khởi tạo giao diện nhiều lần
            if (tblSeats != null)
                return;

            CreateInterface();
            CreateSeats();
            UpdateStatistics();
        }

        private void CreateInterface()
        {
            this.Text = "Sơ đồ chọn chỗ ngồi";
            this.Size = new Size(750, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Bố cục chính: thông tin, sơ đồ, nút chức năng
            TableLayoutPanel root = new TableLayoutPanel();
            root.Dock = DockStyle.Fill;
            root.ColumnCount = 1;
            root.RowCount = 3;
            root.Padding = new Padding(15);
            root.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 110));
            root.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));
            root.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 65));

            this.Controls.Add(root);

            // Khu vực thông tin
            FlowLayoutPanel pnlInfo = new FlowLayoutPanel();
            pnlInfo.Dock = DockStyle.Fill;
            pnlInfo.FlowDirection = FlowDirection.LeftToRight;
            pnlInfo.WrapContents = true;

            Label lblTime = new Label();
            lblTime.Text = "Khung giờ:";
            lblTime.AutoSize = true;
            lblTime.Margin = new Padding(5, 12, 5, 5);

            cboTimeSlot = new ComboBox();
            cboTimeSlot.DropDownStyle =
                ComboBoxStyle.DropDownList;
            cboTimeSlot.Width = 150;
            cboTimeSlot.Items.AddRange(new object[]
            {
                "Sáng - 100.000 đ",
                "Tối - 150.000 đ"
            });
            cboTimeSlot.SelectedIndex = 0;
            cboTimeSlot.SelectedIndexChanged +=
                CboTimeSlot_SelectedIndexChanged;

            lblSelectedCount = new Label();
            lblSelectedCount.AutoSize = true;
            lblSelectedCount.Margin =
                new Padding(15, 12, 5, 5);

            lblSubtotal = new Label();
            lblSubtotal.AutoSize = true;
            lblSubtotal.Margin =
                new Padding(15, 12, 5, 5);

            pnlInfo.Controls.Add(lblTime);
            pnlInfo.Controls.Add(cboTimeSlot);
            pnlInfo.Controls.Add(lblSelectedCount);
            pnlInfo.Controls.Add(lblSubtotal);

            // Bảng 4 hàng x 5 cột
            tblSeats = new TableLayoutPanel();
            tblSeats.Dock = DockStyle.Fill;
            tblSeats.RowCount = 4;
            tblSeats.ColumnCount = 5;
            tblSeats.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            tblSeats.CellBorderStyle =
                TableLayoutPanelCellBorderStyle.Single;

            for (int i = 0; i < 5; i++)
            {
                tblSeats.ColumnStyles.Add(
                    new ColumnStyle(SizeType.Percent, 20));
            }

            for (int i = 0; i < 4; i++)
            {
                tblSeats.RowStyles.Add(
                    new RowStyle(SizeType.Percent, 25));
            }

            // Khu vực nút chức năng
            FlowLayoutPanel pnlButtons = new FlowLayoutPanel();
            pnlButtons.Dock = DockStyle.Fill;
            pnlButtons.FlowDirection =
                FlowDirection.RightToLeft;

            btnConfirm = new Button();
            btnConfirm.Text = "Xác nhận đặt";
            btnConfirm.Size = new Size(140, 38);
            btnConfirm.Click += BtnConfirm_Click;

            btnClear = new Button();
            btnClear.Text = "Hủy chọn tất cả";
            btnClear.Size = new Size(150, 38);
            btnClear.Click += BtnClear_Click;

            pnlButtons.Controls.Add(btnConfirm);
            pnlButtons.Controls.Add(btnClear);

            root.Controls.Add(pnlInfo, 0, 0);
            root.Controls.Add(tblSeats, 0, 1);
            root.Controls.Add(pnlButtons, 0, 2);
        }

        private void CreateSeats()
        {
            // Tạo 20 Button bằng vòng lặp for
            for (int i = 0; i < 20; i++)
            {
                Button btn = new Button();

                btn.Text = $"Vị trí {i + 1}";
                btn.Dock = DockStyle.Fill;
                btn.Margin = new Padding(5);
                btn.Font = new Font("Segoe UI", 10);
                btn.BackColor = Color.WhiteSmoke;
                btn.UseVisualStyleBackColor = false;

                seatStates.Add(btn, SeatState.Empty);

                // Dùng chung một hàm Click cho tất cả Button
                btn.Click += Seat_Click;

                int row = i / 5;
                int col = i % 5;

                tblSeats.Controls.Add(btn, col, row);
            }
        }

        private void Seat_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn == null)
                return;

            SeatState state = seatStates[btn];

            // Không cho chọn vị trí đã đặt
            if (state == SeatState.Reserved)
                return;

            // Chuyển đổi giữa trống và đang chọn
            if (state == SeatState.Empty)
            {
                seatStates[btn] = SeatState.Selected;
            }
            else
            {
                seatStates[btn] = SeatState.Empty;
            }

            UpdateSeatColor(btn);
            UpdateStatistics();
        }

        private void UpdateSeatColor(Button btn)
        {
            switch (seatStates[btn])
            {
                case SeatState.Empty:
                    btn.BackColor = Color.WhiteSmoke;
                    break;

                case SeatState.Selected:
                    btn.BackColor = Color.LightGreen;
                    break;

                case SeatState.Reserved:
                    btn.BackColor = Color.IndianRed;
                    break;
            }
        }

        private void CboTimeSlot_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            if (cboTimeSlot.SelectedIndex == 0)
                seatPrice = 100000m;
            else
                seatPrice = 150000m;

            UpdateStatistics();
        }

        private void UpdateStatistics()
        {
            int count = 0;

            foreach (SeatState state in seatStates.Values)
            {
                if (state == SeatState.Selected)
                    count++;
            }

            decimal subtotal = count * seatPrice;

            lblSelectedCount.Text =
                $"Số vị trí đang chọn: {count}";

            lblSubtotal.Text =
                $"Tạm tính: {subtotal.ToString("N0", viVN)} đ";
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            int count = 0;

            foreach (SeatState state in seatStates.Values)
            {
                if (state == SeatState.Selected)
                    count++;
            }

            if (count == 0)
            {
                MessageBox.Show(
                    "Bạn chưa chọn vị trí nào.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            DialogResult result = MessageBox.Show(
                $"Xác nhận đặt {count} vị trí?\n" +
                $"Tổng tiền: {(count * seatPrice).ToString("N0", viVN)} đ",
                "Xác nhận đặt chỗ",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            // Chuyển các vị trí đang chọn thành đã đặt
            foreach (Button btn in seatStates.Keys)
            {
                if (seatStates[btn] == SeatState.Selected)
                {
                    seatStates[btn] = SeatState.Reserved;
                    UpdateSeatColor(btn);
                }
            }

            UpdateStatistics();

            MessageBox.Show(
                "Đặt chỗ thành công!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            // Chỉ hủy chọn, không hủy chỗ đã đặt
            foreach (Button btn in seatStates.Keys)
            {
                if (seatStates[btn] == SeatState.Selected)
                {
                    seatStates[btn] = SeatState.Empty;
                    UpdateSeatColor(btn);
                }
            }

            UpdateStatistics();
        }
    }
}