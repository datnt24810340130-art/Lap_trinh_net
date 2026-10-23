using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace bai3
{
    public class frmDoctorSchedule : Form
    {
        public frmDoctorSchedule()
        {
            Text = "Lịch trực của Bác sĩ";
            Width = 820;
            Height = 540;
            StartPosition = FormStartPosition.CenterParent;
            WindowState = FormWindowState.Normal;
            MdiParent = null;

            var schedule = new List<object>
            {
                new { BacSi = "BS. Nguyễn Thành Đạt", Khoa = "Nội tổng hợp", Ca = "Sáng", Thu = "Thứ Hai" },
                new { BacSi = "BS. Trần Văn Long", Khoa = "Tim mạch", Ca = "Chiều", Thu = "Thứ Ba" },
                new { BacSi = "BS. Lê Thị Mai", Khoa = "Sản", Ca = "Sáng", Thu = "Thứ Tư" },
                new { BacSi = "BS. Hoàng Đức Tâm", Khoa = "Nhi", Ca = "Tối", Thu = "Thứ Năm" }
            };

            var grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AutoGenerateColumns = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                DataSource = schedule
            };

            Controls.Add(grid);
        }
    }
}
