using bai6._3.DTO;

namespace bai6._3
{
    public partial class Form1
    {
        private readonly TabControl _tabs = new();
        private readonly DataGridView _projectsGrid = CreateGrid();
        private readonly DataGridView _tasksGrid = CreateGrid();
        private readonly DataGridView _overdueGrid = CreateGrid();
        private readonly DataGridView _reportsGrid = CreateGrid();

        private readonly TextBox _projectName = new();
        private readonly DateTimePicker _projectStart = CreateDatePicker();
        private readonly DateTimePicker _projectEnd = CreateDatePicker();
        private readonly ComboBox _projectStatus = CreateCombo();

        private readonly ComboBox _taskProject = CreateCombo();
        private readonly ComboBox _filterProject = CreateCombo();
        private readonly ComboBox _filterStatus = CreateCombo();
        private readonly TextBox _taskName = new();
        private readonly TextBox _taskDescription = new();
        private readonly DateTimePicker _taskDueDate = CreateDatePicker();
        private readonly ComboBox _taskPriority = CreateCombo();
        private readonly ComboBox _taskStatus = CreateCombo();
        private readonly CheckedListBox _taskMembers = new();
        private readonly ComboBox _reportProject = CreateCombo();

        private Button _saveProjectButton = new();
        private Button _deleteProjectButton = new();
        private Button _newTaskButton = new();
        private Button _saveTaskButton = new();
        private Button _deleteTaskButton = new();
        private Button _refreshTasksButton = new();
        private Button _refreshOverdueButton = new();
        private Button _refreshReportsButton = new();

        private void BuildInterface()
        {
            Text = "Quản lý công việc và tiến độ dự án";
            StartPosition = FormStartPosition.CenterScreen;
            MinimumSize = new Size(1000, 680);
            ClientSize = new Size(1200, 800);

            _tabs.Dock = DockStyle.Fill;
            Controls.Add(_tabs);
            BuildProjectsTab();
            BuildTasksTab();
            BuildOverdueTab();
            BuildReportsTab();
        }

        private void BuildProjectsTab()
        {
            var page = new TabPage("Dự án");
            var editor = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 145, Padding = new Padding(12), WrapContents = true };
            editor.Controls.Add(CreateField("Tên dự án", _projectName, 260));
            editor.Controls.Add(CreateField("Ngày bắt đầu", _projectStart, 190));
            editor.Controls.Add(CreateField("Ngày kết thúc", _projectEnd, 190));
            _projectStatus.Items.AddRange(["Đang chạy", "Hoàn thành", "Tạm dừng"]);
            _projectStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            editor.Controls.Add(CreateField("Trạng thái", _projectStatus, 180));

            _saveProjectButton = CreateButton("Lưu dự án");
            _deleteProjectButton = CreateButton("Xóa dự án");
            var buttons = new FlowLayoutPanel { AutoSize = true, Margin = new Padding(8, 24, 0, 0) };
            buttons.Controls.Add(_saveProjectButton);
            buttons.Controls.Add(_deleteProjectButton);
            editor.Controls.Add(buttons);
            _projectsGrid.Dock = DockStyle.Fill;
            page.Controls.Add(_projectsGrid);
            page.Controls.Add(editor);
            _tabs.TabPages.Add(page);
        }

        private void BuildTasksTab()
        {
            var page = new TabPage("Công việc");
            var top = new Panel { Dock = DockStyle.Top, Height = 335 };
            var filters = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 55, Padding = new Padding(12, 8, 12, 4), WrapContents = false };
            filters.Controls.Add(new Label { Text = "Dự án lọc", AutoSize = true, Margin = new Padding(0, 9, 8, 0) });
            _filterProject.Width = 240;
            filters.Controls.Add(_filterProject);
            filters.Controls.Add(new Label { Text = "Trạng thái", AutoSize = true, Margin = new Padding(12, 9, 8, 0) });
            _filterStatus.Width = 160;
            filters.Controls.Add(_filterStatus);
            _refreshTasksButton = CreateButton("Lọc / Tải lại");
            filters.Controls.Add(_refreshTasksButton);

            var editor = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, Padding = new Padding(12, 4, 12, 8) };
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32));
            var fields = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = true, AutoScroll = true };
            fields.Controls.Add(CreateField("Dự án", _taskProject, 255));
            fields.Controls.Add(CreateField("Tên công việc", _taskName, 280));
            _taskDescription.Multiline = true;
            _taskDescription.ScrollBars = ScrollBars.Vertical;
            fields.Controls.Add(CreateField("Mô tả", _taskDescription, 280, 76));
            fields.Controls.Add(CreateField("Hạn nộp", _taskDueDate, 190));
            _taskPriority.Items.AddRange(["Cao", "Trung bình", "Thấp"]);
            _taskPriority.DropDownStyle = ComboBoxStyle.DropDownList;
            fields.Controls.Add(CreateField("Độ ưu tiên", _taskPriority, 150));
            _taskStatus.Items.AddRange(["To Do", "In Progress", "Review", "Done"]);
            _taskStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            fields.Controls.Add(CreateField("Trạng thái", _taskStatus, 150));
            editor.Controls.Add(fields, 0, 0);

            var memberPanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(8, 0, 0, 0) };
            memberPanel.Controls.Add(new Label { Text = "Thành viên được giao", Dock = DockStyle.Top, Height = 24 });
            _taskMembers.Dock = DockStyle.Fill;
            memberPanel.Controls.Add(_taskMembers);
            editor.Controls.Add(memberPanel, 1, 0);
            top.Controls.Add(editor);
            top.Controls.Add(filters);

            var actions = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 48, Padding = new Padding(12, 4, 12, 4) };
            _newTaskButton = CreateButton("Tạo mới");
            _saveTaskButton = CreateButton("Lưu công việc");
            _deleteTaskButton = CreateButton("Xóa công việc");
            actions.Controls.AddRange([_newTaskButton, _saveTaskButton, _deleteTaskButton]);
            _tasksGrid.Dock = DockStyle.Fill;
            page.Controls.Add(_tasksGrid);
            page.Controls.Add(actions);
            page.Controls.Add(top);
            _tabs.TabPages.Add(page);
        }

        private void BuildOverdueTab()
        {
            var page = new TabPage("Cảnh báo trễ hạn");
            var toolbar = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 48, Padding = new Padding(12, 6, 12, 4) };
            _refreshOverdueButton = CreateButton("Tải công việc trễ hạn");
            toolbar.Controls.Add(_refreshOverdueButton);
            _overdueGrid.Dock = DockStyle.Fill;
            page.Controls.Add(_overdueGrid);
            page.Controls.Add(toolbar);
            _tabs.TabPages.Add(page);
        }

        private void BuildReportsTab()
        {
            var page = new TabPage("Báo cáo thành viên");
            var toolbar = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 52, Padding = new Padding(12, 8, 12, 4) };
            toolbar.Controls.Add(new Label { Text = "Dự án", AutoSize = true, Margin = new Padding(0, 8, 8, 0) });
            _reportProject.Width = 300;
            toolbar.Controls.Add(_reportProject);
            _refreshReportsButton = CreateButton("Thống kê");
            toolbar.Controls.Add(_refreshReportsButton);
            _reportsGrid.Dock = DockStyle.Fill;
            page.Controls.Add(_reportsGrid);
            page.Controls.Add(toolbar);
            _tabs.TabPages.Add(page);
        }

        private static Panel CreateField(string label, Control control, int width, int height = 58)
        {
            var panel = new Panel { Width = width, Height = height, Margin = new Padding(4) };
            panel.Controls.Add(new Label { Text = label, Dock = DockStyle.Top, Height = 22 });
            control.Dock = DockStyle.Top;
            panel.Controls.Add(control);
            return panel;
        }

        private static Button CreateButton(string text) => new() { Text = text, AutoSize = true, Height = 32, Margin = new Padding(4) };

        private static ComboBox CreateCombo() => new() { DropDownStyle = ComboBoxStyle.DropDownList };

        private static DateTimePicker CreateDatePicker() => new() { Format = DateTimePickerFormat.Short, ShowCheckBox = true, Checked = false };

        private static DataGridView CreateGrid() => new()
        {
            ReadOnly = true,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = false,
            RowHeadersVisible = false
        };

        private sealed class ProjectFilterOption
        {
            public int? Id { get; init; }
            public string Name { get; init; } = string.Empty;
            public override string ToString() => Name;
        }
    }
}
