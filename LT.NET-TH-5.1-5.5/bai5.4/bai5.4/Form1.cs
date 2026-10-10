namespace bai5._4
{
    public partial class Form1 : Form
    {
        private readonly List<Employee> employees =
        [
            new("NV001", "Nguyễn Minh Anh", "Trưởng phòng", "12/03/2020", "Kinh doanh", "Bán hàng"),
            new("NV002", "Trần Quốc Bảo", "Nhân viên kinh doanh", "05/07/2021", "Kinh doanh", "Bán hàng"),
            new("NV003", "Lê Thu Hà", "Nhân viên kinh doanh", "18/01/2022", "Kinh doanh", "Bán hàng"),
            new("NV004", "Phạm Đức Long", "Trưởng nhóm", "23/09/2019", "Kinh doanh", "Chăm sóc khách hàng"),
            new("NV005", "Võ Ngọc Mai", "Chuyên viên CSKH", "14/02/2023", "Kinh doanh", "Chăm sóc khách hàng"),
            new("NV006", "Đặng Hoàng Nam", "Trưởng phòng", "08/06/2018", "Kỹ thuật", "Phát triển phần mềm"),
            new("NV007", "Bùi Thanh Tùng", "Lập trình viên", "11/11/2020", "Kỹ thuật", "Phát triển phần mềm"),
            new("NV008", "Ngô Hải Yến", "Lập trình viên", "17/04/2022", "Kỹ thuật", "Phát triển phần mềm"),
            new("NV009", "Đỗ Thành Trung", "Trưởng nhóm", "29/08/2021", "Kỹ thuật", "Hạ tầng hệ thống"),
            new("NV010", "Phan Gia Huy", "Quản trị hệ thống", "03/05/2023", "Kỹ thuật", "Hạ tầng hệ thống"),
            new("NV011", "Hoàng Thùy Linh", "Trưởng phòng", "16/01/2019", "Nhân sự", "Tuyển dụng"),
            new("NV012", "Dương Khánh Vy", "Chuyên viên tuyển dụng", "21/10/2021", "Nhân sự", "Tuyển dụng"),
            new("NV013", "Mai Đức Thịnh", "Chuyên viên đào tạo", "07/12/2022", "Nhân sự", "Đào tạo & Phát triển")
        ];

        private readonly TreeView tvDepartments = new();
        private readonly ListView lsvEmployees = new();
        private readonly ComboBox cboViewMode = new();

        public Form1()
        {
            InitializeComponent();
            InitializeInterface();
            BuildDepartmentTree();
        }

        private void InitializeInterface()
        {
            Text = "Trình quản lý tập tin chuyên nghiệp - TreeView & ListView";
            StartPosition = FormStartPosition.CenterScreen;
            MinimumSize = new Size(800, 500);
            Size = new Size(1100, 700);

            var splitContainer = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Size = new Size(1000, 600),
                FixedPanel = FixedPanel.Panel1,
                SplitterDistance = 280,
                Panel1MinSize = 180,
                Panel2MinSize = 350
            };

            var treeHeader = new Label
            {
                Text = "CƠ CẤU TỔ CHỨC",
                Dock = DockStyle.Top,
                Height = 38,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(240, 244, 248)
            };

            tvDepartments.Name = "tvDepartments";
            tvDepartments.Dock = DockStyle.Fill;
            tvDepartments.HideSelection = false;
            tvDepartments.AfterSelect += TvDepartments_AfterSelect;
            splitContainer.Panel1.Controls.Add(tvDepartments);
            splitContainer.Panel1.Controls.Add(treeHeader);

            var toolbar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 48,
                Padding = new Padding(10, 8, 10, 8),
                BackColor = Color.FromArgb(240, 244, 248)
            };
            var viewLabel = new Label
            {
                Text = "Chế độ xem:",
                Dock = DockStyle.Left,
                Width = 90,
                TextAlign = ContentAlignment.MiddleLeft
            };
            cboViewMode.Name = "cboViewMode";
            cboViewMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cboViewMode.Dock = DockStyle.Left;
            cboViewMode.Width = 150;
            cboViewMode.Items.AddRange(["Details", "SmallIcon", "LargeIcon", "Tile"]);
            cboViewMode.SelectedIndex = 0;
            cboViewMode.SelectedIndexChanged += CboViewMode_SelectedIndexChanged;
            toolbar.Controls.Add(cboViewMode);
            toolbar.Controls.Add(viewLabel);

            lsvEmployees.Name = "lsvEmployees";
            lsvEmployees.Dock = DockStyle.Fill;
            lsvEmployees.View = View.Details;
            lsvEmployees.FullRowSelect = true;
            lsvEmployees.GridLines = true;
            lsvEmployees.HideSelection = false;
            lsvEmployees.Columns.Add("Mã NV", 100);
            lsvEmployees.Columns.Add("Họ Tên", 220);
            lsvEmployees.Columns.Add("Chức vụ", 190);
            lsvEmployees.Columns.Add("Ngày vào làm", 130);

            splitContainer.Panel2.Controls.Add(lsvEmployees);
            splitContainer.Panel2.Controls.Add(toolbar);
            Controls.Add(splitContainer);
        }

        private void BuildDepartmentTree()
        {
            var company = new TreeNode("Công ty ABC")
            {
                Tag = new OrganizationUnit()
            };

            AddDepartment(company, "Kinh doanh", "Bán hàng", "Chăm sóc khách hàng");
            AddDepartment(company, "Kỹ thuật", "Phát triển phần mềm", "Hạ tầng hệ thống");
            AddDepartment(company, "Nhân sự", "Tuyển dụng", "Đào tạo & Phát triển");

            tvDepartments.Nodes.Add(company);
            company.Expand();
            tvDepartments.SelectedNode = company;
        }

        private static void AddDepartment(TreeNode company, string departmentName, params string[] groups)
        {
            var department = new TreeNode(departmentName)
            {
                Tag = new OrganizationUnit(departmentName)
            };

            foreach (var groupName in groups)
            {
                department.Nodes.Add(new TreeNode(groupName)
                {
                    Tag = new OrganizationUnit(departmentName, groupName)
                });
            }

            company.Nodes.Add(department);
        }

        private void TvDepartments_AfterSelect(object? sender, TreeViewEventArgs e)
        {
            var unit = (OrganizationUnit)e.Node.Tag!;
            var matchingEmployees = employees.Where(employee =>
                (unit.DepartmentName is null || employee.DepartmentName == unit.DepartmentName) &&
                (unit.GroupName is null || employee.GroupName == unit.GroupName));

            lsvEmployees.BeginUpdate();
            lsvEmployees.Items.Clear();
            foreach (var employee in matchingEmployees)
            {
                var item = new ListViewItem(employee.Id);
                item.SubItems.Add(employee.FullName);
                item.SubItems.Add(employee.Position);
                item.SubItems.Add(employee.StartDate);
                lsvEmployees.Items.Add(item);
            }
            lsvEmployees.EndUpdate();
        }

        private void CboViewMode_SelectedIndexChanged(object? sender, EventArgs e)
        {
            lsvEmployees.View = cboViewMode.SelectedIndex switch
            {
                1 => View.SmallIcon,
                2 => View.LargeIcon,
                3 => View.Tile,
                _ => View.Details
            };
        }

        private sealed record Employee(
            string Id,
            string FullName,
            string Position,
            string StartDate,
            string DepartmentName,
            string GroupName);

        private sealed record OrganizationUnit(string? DepartmentName = null, string? GroupName = null);
    }
}
