namespace Bai4._7;

public sealed class StatusMessageEventArgs(string message) : EventArgs
{
    public string Message { get; } = message;
}

public partial class FormStudent : Form
{
    private readonly DataGridView studentGrid = new();
    private readonly ContextMenuStrip gridContextMenu = new();

    public event EventHandler<StatusMessageEventArgs>? StatusChanged;

    public FormStudent()
    {
        InitializeComponent();
        LoadSampleStudents();
    }

    private void InitializeComponent()
    {
        Text = "Quản lý Sinh viên";
        Width = 900;
        Height = 500;
        StartPosition = FormStartPosition.CenterParent;

        studentGrid.Dock = DockStyle.Fill;
        studentGrid.AllowUserToAddRows = false;
        studentGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        studentGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        studentGrid.MultiSelect = false;
        studentGrid.ReadOnly = true;
        studentGrid.Columns.Add("Id", "Mã SV");
        studentGrid.Columns.Add("Name", "Họ và tên");
        studentGrid.Columns.Add("Class", "Lớp");
        studentGrid.Columns.Add("Major", "Chuyên ngành");

        gridContextMenu.Items.Add("Xóa dòng", null, DeleteSelectedStudent);
        gridContextMenu.Items.Add("Làm mới dữ liệu", null, RefreshStudents);
        gridContextMenu.Items.Add("Xem chi tiết", null, ShowStudentDetails);
        studentGrid.ContextMenuStrip = gridContextMenu;
        Controls.Add(studentGrid);
    }

    private void LoadSampleStudents()
    {
        studentGrid.Rows.Clear();
        studentGrid.Rows.Add("SV001", "Nguyễn Văn Anh", "D18CNPM1", "Công nghệ thông tin");
        studentGrid.Rows.Add("SV002", "Trần Thị Thư", "D18CNPM2", "Công nghệ thông tin");
        studentGrid.Rows.Add("SV003", "Lê Minh Ngọc", "D18KT1", "Kế toán");
    }

    private void DeleteSelectedStudent(object? sender, EventArgs e)
    {
        if (studentGrid.CurrentRow is null || studentGrid.CurrentRow.IsNewRow)
        {
            PublishStatus("Chưa chọn sinh viên để xóa.");
            return;
        }

        var studentName = studentGrid.CurrentRow.Cells[1].Value?.ToString() ?? "sinh viên";
        studentGrid.Rows.RemoveAt(studentGrid.CurrentRow.Index);
        PublishStatus($"Đã xóa {studentName} thành công.");
    }

    private void RefreshStudents(object? sender, EventArgs e)
    {
        LoadSampleStudents();
        PublishStatus("Đã làm mới danh sách sinh viên.");
    }

    private void ShowStudentDetails(object? sender, EventArgs e)
    {
        if (studentGrid.CurrentRow is null || studentGrid.CurrentRow.IsNewRow)
        {
            PublishStatus("Chưa chọn sinh viên để xem chi tiết.");
            return;
        }

        var row = studentGrid.CurrentRow;
        MessageBox.Show($"Mã: {row.Cells[0].Value}\nHọ tên: {row.Cells[1].Value}\nLớp: {row.Cells[2].Value}\nChuyên ngành: {row.Cells[3].Value}", "Chi tiết sinh viên", MessageBoxButtons.OK, MessageBoxIcon.Information);
        PublishStatus("Đã xem chi tiết sinh viên.");
    }

    private void PublishStatus(string message)
    {
        StatusChanged?.Invoke(this, new StatusMessageEventArgs(message));
    }
}
