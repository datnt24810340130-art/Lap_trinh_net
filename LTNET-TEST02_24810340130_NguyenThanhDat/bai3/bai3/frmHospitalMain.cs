using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace bai3
{
    public class frmHospitalMain : Form
    {
        private MenuStrip menuStrip;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblDoctorName;
        private ToolStripProgressBar progressBar;

        public frmHospitalMain()
        {
            Text = "BỆNH VIỆN - HỆ THỐNG QUẢN LÝ BỆNH ÁN";
            IsMdiContainer = true;
            WindowState = FormWindowState.Maximized;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = SystemColors.Control;

            menuStrip = CreateMenuStrip();
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;

            statusStrip = CreateStatusStrip();
            Controls.Add(statusStrip);

            Load += FrmHospitalMain_Load;
        }

        private void FrmHospitalMain_Load(object? sender, EventArgs e)
        {
            lblDoctorName.Text = "Bác sĩ trực: BS. Nguyễn Thành Đạt";
            progressBar.Value = 78;
        }

        private MenuStrip CreateMenuStrip()
        {
            var menu = new MenuStrip();
            menu.Dock = DockStyle.Top;

            var systemMenu = new ToolStripMenuItem("Hệ thống");
            systemMenu.DropDownItems.Add(new ToolStripMenuItem("Đăng xuất", null, OnLogoutClick));
            systemMenu.DropDownItems.Add(new ToolStripMenuItem("Nạp lại dữ liệu", null, OnRefreshDataClick));
            systemMenu.DropDownItems.Add(new ToolStripSeparator());
            systemMenu.DropDownItems.Add(new ToolStripMenuItem("Thoát", null, OnExitClick));

            var manageMenu = new ToolStripMenuItem("Quản lý");
            manageMenu.DropDownItems.Add(new ToolStripMenuItem("Hồ sơ Bệnh nhân", null, OnOpenPatientProfiles));
            manageMenu.DropDownItems.Add(new ToolStripMenuItem("Danh mục Bác sĩ", null, OnOpenDoctorSchedule));
            manageMenu.DropDownItems.Add(new ToolStripMenuItem("Danh mục Khoa/Phòng", null, OnOpenDepartmentCatalog));

            var windowMenu = new ToolStripMenuItem("Cửa sổ");
            windowMenu.DropDownItems.Add(new ToolStripMenuItem("Sắp xếp Xếp tầng", null, OnCascadeLayout));
            windowMenu.DropDownItems.Add(new ToolStripMenuItem("Ngang", null, OnTileHorizontalLayout));
            windowMenu.DropDownItems.Add(new ToolStripMenuItem("Dọc", null, OnTileVerticalLayout));
            windowMenu.DropDownItems.Add(new ToolStripSeparator());
            windowMenu.DropDownItems.Add(new ToolStripMenuItem("Đóng tất cả", null, OnCloseAllChildren));

            menu.Items.Add(systemMenu);
            menu.Items.Add(manageMenu);
            menu.Items.Add(windowMenu);
            return menu;
        }

        private StatusStrip CreateStatusStrip()
        {
            var strip = new StatusStrip();
            strip.Dock = DockStyle.Bottom;

            lblDoctorName = new ToolStripStatusLabel
            {
                Text = "Bác sĩ trực: BS. Nguyễn Thành Đạt",
                BorderSides = ToolStripStatusLabelBorderSides.All,
                BorderStyle = Border3DStyle.Sunken
            };

            progressBar = new ToolStripProgressBar
            {
                Style = ProgressBarStyle.Continuous,
                Value = 50,
                Width = 180
            };

            strip.Items.Add(lblDoctorName);
            strip.Items.Add(new ToolStripStatusLabel(" "));
            strip.Items.Add(progressBar);
            return strip;
        }

        private void OnOpenPatientProfiles(object? sender, EventArgs e)
        {
            OpenMdiChild<frmPatientProfiles>();
        }

        private void OnOpenDoctorSchedule(object? sender, EventArgs e)
        {
            OpenMdiChild<frmDoctorSchedule>();
        }

        private void OnOpenDepartmentCatalog(object? sender, EventArgs e)
        {
            MessageBox.Show("Danh mục Khoa/Phòng đang được cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OnRefreshDataClick(object? sender, EventArgs e)
        {
            lblDoctorName.Text = "Đang nạp lại dữ liệu ...";
            progressBar.Value = 0;

            for (int i = 10; i <= 100; i += 10)
            {
                progressBar.Value = i;
                Application.DoEvents();
                System.Threading.Thread.Sleep(80);
            }

            lblDoctorName.Text = "Bác sĩ trực: BS. Nguyễn Thành Đạt";
            progressBar.Value = 78;
        }

        private void OnLogoutClick(object? sender, EventArgs e)
        {
            MessageBox.Show("Đăng xuất thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OnExitClick(object? sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OnCascadeLayout(object? sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void OnTileHorizontalLayout(object? sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void OnTileVerticalLayout(object? sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void OnCloseAllChildren(object? sender, EventArgs e)
        {
            foreach (Form child in MdiChildren.OfType<Form>().ToList())
            {
                child.Close();
            }
        }

        private void OpenMdiChild<T>() where T : Form, new()
        {
            foreach (Form child in MdiChildren)
            {
                if (child is T)
                {
                    child.Activate();
                    return;
                }
            }

            var form = new T
            {
                MdiParent = this
            };
            form.Show();
        }
    }
}
