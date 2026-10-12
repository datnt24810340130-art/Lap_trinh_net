namespace Bai4._8;

public partial class StudentForm : Form
{
    private readonly Action<int> reportToParent;

    public StudentForm(Action<int> reportToParent)
    {
        InitializeComponent();
        this.reportToParent = reportToParent;
    }

    private async void loadButton_Click(object? sender, EventArgs e)
    {
        loadButton.Enabled = false;
        studentGridView.DataSource = null;
        progressBar.Value = 0;
        progressLabel.Text = "0%";
        reportToParent(0);

        var progress = new Progress<int>(value =>
        {
            progressBar.Value = value;
            progressLabel.Text = $"{value}%";
            reportToParent(value);
        });

        try
        {
            var students = await LoadStudentsAsync(progress);
            studentGridView.DataSource = students;
            MessageBox.Show(this, "Nạp dữ liệu hoàn tất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception exception)
        {
            MessageBox.Show(this, $"Không thể nạp dữ liệu: {exception.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            loadButton.Enabled = true;
        }
    }

    private static Task<List<Student>> LoadStudentsAsync(IProgress<int> progress)
    {
        return Task.Run(async () =>
        {
            var students = new List<Student>();
            const int total = 100;

            for (var index = 1; index <= total; index++)
            {
                await Task.Delay(35);
                students.Add(new Student
                {
                    Id = index,
                    Name = index switch
                    {
                        1 => "Nguyễn Văn An",
                        2 => "Trần Thị Bình",
                        3 => "Lê Hoàng Nam",
                        4 => "Phạm Thu Hà",
                        5 => "Đặng Minh Tuấn",
                        _ => $"Sinh viên {index}"
                    },
                    ClassName = index % 2 == 0 ? "D16CNPM1" : "D16CNPM2",
                    AverageScore = Math.Round(6 + (index % 40) / 10.0, 1)
                });
                progress.Report(index);
            }

            return students;
        });
    }
}