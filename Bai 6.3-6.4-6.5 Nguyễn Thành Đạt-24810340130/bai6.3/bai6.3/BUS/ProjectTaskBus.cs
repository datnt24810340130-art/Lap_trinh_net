using bai6._3.DAL;
using bai6._3.DTO;

namespace bai6._3.BUS;

public sealed class ProjectTaskBus
{
    private static readonly string[] TaskStatuses = ["To Do", "In Progress", "Review", "Done"];
    private static readonly string[] Priorities = ["Cao", "Trung bình", "Thấp"];
    private readonly ProjectTaskDal _dal;

    public ProjectTaskBus(ProjectTaskDal dal)
    {
        _dal = dal;
    }

    public List<ProjectDto> GetProjects() => _dal.GetProjects();
    public List<MemberDto> GetMembers() => _dal.GetMembers();
    public List<TaskDto> GetTasks(int? projectId = null, string? status = null) => _dal.GetTasks(projectId, status);
    public List<TaskDto> GetOverdueTasks() => _dal.GetOverdueTasks();
    public List<MemberProgressDto> GetMemberProgress(int? projectId) => _dal.GetMemberProgress(projectId);
    public List<int> GetAssignedMemberIds(int taskId) => _dal.GetAssignedMemberIds(taskId);

    public int SaveProject(ProjectDto project)
    {
        ArgumentNullException.ThrowIfNull(project);
        project.TenDuAn = project.TenDuAn.Trim();
        if (project.TenDuAn.Length == 0)
            throw new ArgumentException("Tên dự án không được để trống.");
        if (project.TenDuAn.Length > 150)
            throw new ArgumentException("Tên dự án tối đa 150 ký tự.");
        if (project.NgayBatDau.HasValue && project.NgayKetThuc.HasValue && project.NgayKetThuc < project.NgayBatDau)
            throw new ArgumentException("Ngày kết thúc phải bằng hoặc sau ngày bắt đầu.");
        return _dal.SaveProject(project);
    }

    public void DeleteProject(int projectId) => _dal.DeleteProject(projectId);

    public int SaveTask(TaskDto task)
    {
        ArgumentNullException.ThrowIfNull(task);
        task.TenTask = task.TenTask.Trim();
        if (task.MaDuAn <= 0)
            throw new ArgumentException("Vui lòng chọn dự án cho công việc.");
        if (task.TenTask.Length == 0)
            throw new ArgumentException("Tên công việc không được để trống.");
        if (task.TenTask.Length > 200)
            throw new ArgumentException("Tên công việc tối đa 200 ký tự.");
        if (!TaskStatuses.Contains(task.TrangThai, StringComparer.Ordinal))
            throw new ArgumentException("Trạng thái công việc không hợp lệ.");
        if (!string.IsNullOrEmpty(task.DoUuTien) && !Priorities.Contains(task.DoUuTien, StringComparer.Ordinal))
            throw new ArgumentException("Độ ưu tiên không hợp lệ.");
        return _dal.SaveTask(task);
    }

    public void DeleteTask(int taskId) => _dal.DeleteTask(taskId);

    public void UpdateTaskStatus(int taskId, string status)
    {
        if (!TaskStatuses.Contains(status, StringComparer.Ordinal))
            throw new ArgumentException("Trạng thái công việc không hợp lệ.");
        _dal.UpdateTaskStatus(taskId, status);
    }

    public void SetTaskAssignments(int taskId, IEnumerable<int> memberIds)
    {
        ArgumentNullException.ThrowIfNull(memberIds);
        _dal.SetTaskAssignments(taskId, memberIds);
    }
}
