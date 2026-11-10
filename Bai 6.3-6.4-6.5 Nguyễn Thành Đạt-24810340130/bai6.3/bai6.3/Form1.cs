using bai6._3.BUS;
using bai6._3.DTO;

namespace bai6._3
{
    public partial class Form1 : Form
    {
        private readonly ProjectTaskBus _bus;
        private int _selectedProjectId;
        private int _selectedTaskId;
        private bool _refreshingProjects;
        private bool _refreshingTasks;

        public Form1(ProjectTaskBus bus)
        {
            _bus = bus;
            InitializeComponent();
            BuildInterface();
            WireEvents();
            Load += (_, _) => RunUiAction(RefreshAll);
        }

        private void WireEvents()
        {
            _saveProjectButton.Click += (_, _) => RunUiAction(SaveProject);
            _deleteProjectButton.Click += (_, _) => RunUiAction(DeleteProject);
            _projectsGrid.SelectionChanged += (_, _) => LoadSelectedProject();
            _newTaskButton.Click += (_, _) => ClearTaskEditor();
            _saveTaskButton.Click += (_, _) => RunUiAction(SaveTask);
            _deleteTaskButton.Click += (_, _) => RunUiAction(DeleteTask);
            _tasksGrid.SelectionChanged += (_, _) => LoadSelectedTask();
            _refreshTasksButton.Click += (_, _) => RunUiAction(RefreshTasks);
            _refreshOverdueButton.Click += (_, _) => RunUiAction(RefreshOverdue);
            _refreshReportsButton.Click += (_, _) => RunUiAction(RefreshReports);
        }

        private void RefreshAll()
        {
            RefreshProjects();
            RefreshMembers();
            RefreshTasks();
            RefreshOverdue();
            RefreshReports();
        }

        private void RefreshProjects()
        {
            var projects = _bus.GetProjects();
            _refreshingProjects = true;
            try
            {
                _projectsGrid.DataSource = projects;
                _projectsGrid.ClearSelection();
                _projectsGrid.CurrentCell = null;
            }
            finally
            {
                _refreshingProjects = false;
            }
            _selectedProjectId = 0;
            _projectName.Clear();
            SetDate(_projectStart, null);
            SetDate(_projectEnd, null);
            SetHeaders(_projectsGrid, new Dictionary<string, string>
            {
                ["MaDuAn"] = "Mã dự án", ["TenDuAn"] = "Tên dự án", ["NgayBatDau"] = "Ngày bắt đầu",
                ["NgayKetThuc"] = "Ngày kết thúc", ["TrangThai"] = "Trạng thái"
            });
            _taskProject.DisplayMember = nameof(ProjectDto.TenDuAn);
            _taskProject.ValueMember = nameof(ProjectDto.MaDuAn);
            _taskProject.DataSource = projects.ToList();

            var filters = new List<ProjectFilterOption> { new() { Id = null, Name = "Tất cả dự án" } };
            filters.AddRange(projects.Select(p => new ProjectFilterOption { Id = p.MaDuAn, Name = p.TenDuAn }));
            _filterProject.DataSource = filters;
            _reportProject.DataSource = filters.ToList();
            _filterStatus.Items.Clear();
            _filterStatus.Items.AddRange(["Tất cả trạng thái", "To Do", "In Progress", "Review", "Done"]);
            _filterStatus.SelectedIndex = 0;
            _projectStatus.SelectedIndex = 0;
            _taskStatus.SelectedItem = "To Do";
            _taskPriority.SelectedItem = "Trung bình";
        }

        private void RefreshMembers()
        {
            _taskMembers.DataSource = _bus.GetMembers();
            _taskMembers.DisplayMember = nameof(MemberDto.HoTen);
        }

        private void RefreshTasks()
        {
            var projectFilter = _filterProject.SelectedItem as ProjectFilterOption;
            var status = _filterStatus.SelectedItem as string;
            _refreshingTasks = true;
            try
            {
                _tasksGrid.DataSource = _bus.GetTasks(projectFilter?.Id, status == "Tất cả trạng thái" ? null : status);
                _tasksGrid.ClearSelection();
                _tasksGrid.CurrentCell = null;
            }
            finally
            {
                _refreshingTasks = false;
            }
            SetHeaders(_tasksGrid, new Dictionary<string, string>
            {
                ["MaTask"] = "Mã task", ["MaDuAn"] = "Mã dự án", ["TenTask"] = "Tên công việc",
                ["MoTa"] = "Mô tả", ["HanNop"] = "Hạn nộp", ["DoUuTien"] = "Ưu tiên",
                ["TrangThai"] = "Trạng thái", ["TenDuAn"] = "Dự án"
            });
            _selectedTaskId = 0;
            ClearTaskEditor();
        }

        private void RefreshOverdue()
        {
            _overdueGrid.DataSource = _bus.GetOverdueTasks();
            SetHeaders(_overdueGrid, new Dictionary<string, string>
            {
                ["MaTask"] = "Mã task", ["TenTask"] = "Tên công việc", ["TenDuAn"] = "Dự án",
                ["HanNop"] = "Hạn nộp", ["DoUuTien"] = "Ưu tiên", ["TrangThai"] = "Trạng thái"
            });
        }

        private void RefreshReports()
        {
            var project = _reportProject.SelectedItem as ProjectFilterOption;
            _reportsGrid.DataSource = _bus.GetMemberProgress(project?.Id);
            SetHeaders(_reportsGrid, new Dictionary<string, string>
            {
                ["MaThanhVien"] = "Mã thành viên", ["HoTen"] = "Thành viên", ["TongCongViec"] = "Tổng công việc",
                ["CongViecHoanThanh"] = "Đã hoàn thành", ["TyLeHoanThanh"] = "Tỷ lệ hoàn thành (%)"
            });
        }

        private void SaveProject()
        {
            var project = new ProjectDto
            {
                MaDuAn = _selectedProjectId,
                TenDuAn = _projectName.Text,
                NgayBatDau = _projectStart.Checked ? _projectStart.Value.Date : null,
                NgayKetThuc = _projectEnd.Checked ? _projectEnd.Value.Date : null,
                TrangThai = _projectStatus.SelectedItem as string
            };
            _bus.SaveProject(project);
            RefreshAll();
        }

        private void DeleteProject()
        {
            if (_selectedProjectId == 0)
                throw new InvalidOperationException("Vui lòng chọn dự án cần xóa.");
            if (MessageBox.Show("Xóa dự án sẽ xóa các công việc và phân công liên quan. Tiếp tục?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            _bus.DeleteProject(_selectedProjectId);
            _selectedProjectId = 0;
            _projectName.Clear();
            RefreshAll();
        }

        private void SaveTask()
        {
            var project = _taskProject.SelectedItem as ProjectDto
                ?? throw new InvalidOperationException("Vui lòng tạo và chọn dự án trước.");
            var task = new TaskDto
            {
                MaTask = _selectedTaskId,
                MaDuAn = project.MaDuAn,
                TenTask = _taskName.Text,
                MoTa = string.IsNullOrWhiteSpace(_taskDescription.Text) ? null : _taskDescription.Text.Trim(),
                HanNop = _taskDueDate.Checked ? _taskDueDate.Value : null,
                DoUuTien = _taskPriority.SelectedItem as string,
                TrangThai = _taskStatus.SelectedItem as string ?? "To Do"
            };
            var taskId = _bus.SaveTask(task);
            _bus.SetTaskAssignments(taskId, _taskMembers.CheckedItems.Cast<MemberDto>().Select(m => m.MaThanhVien));
            RefreshTasks();
            RefreshOverdue();
            RefreshReports();
        }

        private void DeleteTask()
        {
            if (_selectedTaskId == 0)
                throw new InvalidOperationException("Vui lòng chọn công việc cần xóa.");
            _bus.DeleteTask(_selectedTaskId);
            RefreshTasks();
            RefreshOverdue();
            RefreshReports();
        }

        private void LoadSelectedProject()
        {
            if (_refreshingProjects)
                return;
            if (_projectsGrid.CurrentRow?.DataBoundItem is not ProjectDto project)
                return;
            _selectedProjectId = project.MaDuAn;
            _projectName.Text = project.TenDuAn;
            SetDate(_projectStart, project.NgayBatDau);
            SetDate(_projectEnd, project.NgayKetThuc);
            _projectStatus.SelectedItem = project.TrangThai;
        }

        private void LoadSelectedTask()
        {
            if (_refreshingTasks)
                return;
            if (_tasksGrid.CurrentRow?.DataBoundItem is not TaskDto task)
                return;
            _selectedTaskId = task.MaTask;
            _taskProject.SelectedValue = task.MaDuAn;
            _taskName.Text = task.TenTask;
            _taskDescription.Text = task.MoTa ?? string.Empty;
            SetDate(_taskDueDate, task.HanNop);
            _taskPriority.SelectedItem = task.DoUuTien;
            _taskStatus.SelectedItem = task.TrangThai;
            var assignedIds = _bus.GetAssignedMemberIds(task.MaTask);
            for (var index = 0; index < _taskMembers.Items.Count; index++)
            {
                if (_taskMembers.Items[index] is MemberDto member)
                    _taskMembers.SetItemChecked(index, assignedIds.Contains(member.MaThanhVien));
            }
        }

        private void ClearTaskEditor()
        {
            _selectedTaskId = 0;
            _taskName.Clear();
            _taskDescription.Clear();
            SetDate(_taskDueDate, null);
            _taskPriority.SelectedItem = "Trung bình";
            _taskStatus.SelectedItem = "To Do";
            for (var index = 0; index < _taskMembers.Items.Count; index++)
                _taskMembers.SetItemChecked(index, false);
        }

        private static void SetDate(DateTimePicker picker, DateTime? value)
        {
            picker.Checked = value.HasValue;
            if (value.HasValue)
                picker.Value = value.Value;
        }

        private static void SetHeaders(DataGridView grid, IReadOnlyDictionary<string, string> headers)
        {
            foreach (DataGridViewColumn column in grid.Columns)
            {
                if (headers.TryGetValue(column.DataPropertyName, out var header))
                    column.HeaderText = header;
            }
        }

        private static void RunUiAction(Action action)
        {
            try
            {
                action();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Không thể thực hiện", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
