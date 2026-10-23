using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace bai3
{
    public class frmPatientProfiles : Form
    {
        public frmPatientProfiles()
        {
            Text = "Hồ sơ Bệnh nhân";
            Width = 820;
            Height = 540;
            StartPosition = FormStartPosition.CenterParent;
            WindowState = FormWindowState.Normal;
            MdiParent = null;

            var data = new List<object>
            {
                new { MaBN = "BN001", HoTen = "Nguyễn Văn An", GioiTinh = "Nam", Tuoi = 42, Khoa = "Nội tổng hợp" },
                new { MaBN = "BN002", HoTen = "Trần Thị Bích", GioiTinh = "Nữ", Tuoi = 35, Khoa = "Sản" },
                new { MaBN = "BN003", HoTen = "Lê Hoàng Cường", GioiTinh = "Nam", Tuoi = 62, Khoa = "Tim mạch" },
                new { MaBN = "BN004", HoTen = "Phạm Thị Duyên", GioiTinh = "Nữ", Tuoi = 28, Khoa = "Nhi" }
            };

            var grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AutoGenerateColumns = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                DataSource = data
            };

            Controls.Add(grid);
        }
    }
}
