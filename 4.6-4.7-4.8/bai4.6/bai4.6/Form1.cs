using System.ComponentModel;

namespace bai4._6
{
    public partial class Form1 : Form
    {
        private readonly BindingList<Student> _students = new();
        private readonly BindingSource _studentSource = new();

        public Form1()
        {
            InitializeComponent();
            ConfigureBindings();
            ConfigureEvents();
        }

        private void ConfigureBindings()
        {
            _studentSource.DataSource = _students;
            dgvStudents.DataSource = _studentSource;
            cboGender.DataSource = new[] { "Nam", "Nữ" };

            txtStudentId.DataBindings.Add(nameof(TextBox.Text), _studentSource, nameof(Student.StudentId), true, DataSourceUpdateMode.OnPropertyChanged);
            txtFullName.DataBindings.Add(nameof(TextBox.Text), _studentSource, nameof(Student.FullName), true, DataSourceUpdateMode.OnPropertyChanged);

            var ageBinding = txtAge.DataBindings.Add(nameof(TextBox.Text), _studentSource, nameof(Student.Age), true, DataSourceUpdateMode.OnPropertyChanged);
            ageBinding.Parse += (_, e) =>
            {
                e.Value = int.TryParse(e.Value?.ToString(), out var age) ? age : 0;
            };

            cboGender.DataBindings.Add(nameof(ComboBox.SelectedItem), _studentSource, nameof(Student.Gender), true, DataSourceUpdateMode.OnPropertyChanged);
            _studentSource.CurrentChanged += (_, _) => LoadCurrentAvatar();
        }

        private void ConfigureEvents()
        {
            btnAdd.Click += AddStudent;
            btnDelete.Click += DeleteStudent;
            btnUpdate.Click += UpdateStudent;
            btnChooseAvatar.Click += ChooseAvatar;
            btnExportCsv.Click += ExportCsv;
        }

        private void AddStudent(object? sender, EventArgs e)
        {
            if (!TryReadStudent(out var student))
            {
                return;
            }

            if (_students.Any(item => string.Equals(item.StudentId, student.StudentId, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Mã sinh viên đã tồn tại.", "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _students.Add(student);
            _studentSource.Position = _students.Count - 1;
        }

        private void DeleteStudent(object? sender, EventArgs e)
        {
            if (_studentSource.Current is not Student student)
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show($"Xóa sinh viên '{student.FullName}'?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                _students.Remove(student);
            }
        }

        private void UpdateStudent(object? sender, EventArgs e)
        {
            if (_studentSource.Current is not Student student)
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _studentSource.EndEdit();
            if (!ValidateStudent(student))
            {
                return;
            }

            if (_students.Any(item => !ReferenceEquals(item, student) && string.Equals(item.StudentId, student.StudentId, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Mã sinh viên đã tồn tại.", "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _studentSource.ResetCurrentItem();
            MessageBox.Show("Đã cập nhật sinh viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ChooseAvatar(object? sender, EventArgs e)
        {
            if (_studentSource.Current is not Student student)
            {
                MessageBox.Show("Vui lòng chọn hoặc thêm sinh viên trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var dialog = new OpenFileDialog
            {
                Title = "Chọn ảnh đại diện",
                Filter = "Tệp hình ảnh|*.jpg;*.jpeg;*.png;*.bmp|Tất cả tệp|*.*",
                CheckFileExists = true,
                Multiselect = false
            };

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                student.AvatarPath = dialog.FileName;
                LoadCurrentAvatar();
            }
        }

        private void ExportCsv(object? sender, EventArgs e)
        {
            if (_students.Count == 0)
            {
                MessageBox.Show("Danh sách sinh viên đang trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var dialog = new SaveFileDialog
            {
                Title = "Xuất danh sách sinh viên",
                Filter = "Tệp CSV|*.csv",
                DefaultExt = "csv",
                AddExtension = true,
                FileName = "danh-sach-sinh-vien.csv",
                OverwritePrompt = true
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
                using var writer = new StreamWriter(dialog.FileName, false, new System.Text.UTF8Encoding(true));
                writer.WriteLine("StudentId,FullName,Age,Gender,AvatarPath");
                foreach (var student in _students)
                {
                    writer.WriteLine(string.Join(",",
                        EscapeCsv(student.StudentId),
                        EscapeCsv(student.FullName),
                        student.Age,
                        EscapeCsv(student.Gender),
                        EscapeCsv(student.AvatarPath)));
                }

                MessageBox.Show("Đã xuất file CSV thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (IOException exception)
            {
                MessageBox.Show($"Không thể ghi file: {exception.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool TryReadStudent(out Student student)
        {
            student = new Student
            {
                StudentId = txtStudentId.Text.Trim(),
                FullName = txtFullName.Text.Trim(),
                Gender = cboGender.SelectedItem?.ToString() ?? "Nam",
                AvatarPath = string.Empty
            };

            if (!int.TryParse(txtAge.Text.Trim(), out var age))
            {
                MessageBox.Show("Tuổi phải là số nguyên.", "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            student.Age = age;
            return ValidateStudent(student);
        }

        private static bool ValidateStudent(Student student)
        {
            if (string.IsNullOrWhiteSpace(student.StudentId) || string.IsNullOrWhiteSpace(student.FullName))
            {
                MessageBox.Show("Mã sinh viên và họ tên không được để trống.", "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (student.Age < 1 || student.Age > 150)
            {
                MessageBox.Show("Tuổi phải nằm trong khoảng từ 1 đến 150.", "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void LoadCurrentAvatar()
        {
            picAvatar.Image?.Dispose();
            picAvatar.Image = null;

            if (_studentSource.Current is not Student student || string.IsNullOrWhiteSpace(student.AvatarPath) || !File.Exists(student.AvatarPath))
            {
                return;
            }

            try
            {
                using var image = Image.FromFile(student.AvatarPath);
                picAvatar.Image = new Bitmap(image);
            }
            catch (ArgumentException)
            {
                picAvatar.Image = null;
            }
            catch (IOException)
            {
                picAvatar.Image = null;
            }
        }

        private static string EscapeCsv(string? value)
        {
            value ??= string.Empty;
            return value.Contains(',') || value.Contains('"') || value.Contains('\r') || value.Contains('\n')
                ? $"\"{value.Replace("\"", "\"\"")}\""
                : value;
        }

    }
}
