using bai6._2.BUS;
using bai6._2.DTO;
using System.Data;

namespace bai6._2;

public partial class Form1 : Form
{
    private readonly EmployeeService _employeeService = new();
    private readonly AttendanceService _attendanceService = new();
    private readonly PayrollService _payrollService = new();
    private readonly DataGridView _employeeGrid = new();
    private readonly DataGridView _attendanceGrid = new();
    private readonly TextBox _nameSearch = new();
    private readonly TextBox _nationalIdSearch = new();
    private readonly ComboBox _departmentFilter = new();
    private readonly TextBox _employeeName = new();
    private readonly TextBox _nationalId = new();
    private readonly ComboBox _employeeDepartment = new();
    private readonly DateTimePicker _dateOfBirth = new();
    private readonly NumericUpDown _baseSalary = new();
    private readonly NumericUpDown _salaryCoefficient = new();
    private readonly DateTimePicker _monthPicker = new();
    private DataTable? _attendanceTable;
    private int _selectedEmployeeId;

    public Form1()
    {
        InitializeComponent();
        Text = "HRM & Payroll System";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(1000, 680);
        Size = new Size(1280, 820);
        BuildInterface();
        Load += Form1_Load;
    }

    private void BuildInterface()
    {
        var tabs = new TabControl { Dock = DockStyle.Fill };
        var employeesTab = new TabPage("Employees");
        var attendanceTab = new TabPage("Attendance & Payroll");
        employeesTab.Controls.Add(BuildEmployeePage());
        attendanceTab.Controls.Add(BuildAttendancePage());
        tabs.TabPages.Add(employeesTab);
        tabs.TabPages.Add(attendanceTab);
        Controls.Add(tabs);
    }

    private Control BuildEmployeePage()
    {
        var page = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 3,
            Padding = new Padding(10)
        };
        page.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        page.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        page.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        var filters = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true, WrapContents = true };
        filters.Controls.Add(new Label { Text = "Name", AutoSize = true, Margin = new Padding(4, 9, 4, 4) });
        _nameSearch.Width = 180;
        filters.Controls.Add(_nameSearch);
        filters.Controls.Add(new Label { Text = "Department", AutoSize = true, Margin = new Padding(4, 9, 4, 4) });
        _departmentFilter.Width = 180;
        _departmentFilter.DropDownStyle = ComboBoxStyle.DropDownList;
        filters.Controls.Add(_departmentFilter);
        filters.Controls.Add(new Label { Text = "National ID", AutoSize = true, Margin = new Padding(4, 9, 4, 4) });
        _nationalIdSearch.Width = 150;
        filters.Controls.Add(_nationalIdSearch);
        filters.Controls.Add(CreateButton("Search", SearchEmployees));
        filters.Controls.Add(CreateButton("Show all", () =>
        {
            _nameSearch.Clear();
            _nationalIdSearch.Clear();
            if (_departmentFilter.Items.Count > 0) _departmentFilter.SelectedIndex = 0;
            SearchEmployees();
        }));
        page.Controls.Add(filters, 0, 0);

        ConfigureGrid(_employeeGrid);
        _employeeGrid.SelectionChanged += EmployeeGrid_SelectionChanged;
        page.Controls.Add(_employeeGrid, 0, 1);

        var editArea = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            AutoSize = true,
            ColumnCount = 4,
            RowCount = 3,
            Padding = new Padding(0, 8, 0, 0)
        };
        editArea.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        editArea.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        editArea.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        editArea.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        AddEditorField(editArea, "Full name", _employeeName, 0, 0);
        AddEditorField(editArea, "Department", _employeeDepartment, 2, 0);
        AddEditorField(editArea, "Date of birth", _dateOfBirth, 0, 1);
        AddEditorField(editArea, "National ID", _nationalId, 2, 1);
        AddEditorField(editArea, "Base salary", _baseSalary, 0, 2);
        AddEditorField(editArea, "Salary coefficient", _salaryCoefficient, 2, 2);

        _dateOfBirth.Format = DateTimePickerFormat.Short;
        _dateOfBirth.ShowCheckBox = true;
        _dateOfBirth.Checked = false;
        ConfigureNumber(_baseSalary, 2, 100_000, 1_000_000_000m, 1_000_000m);
        ConfigureNumber(_salaryCoefficient, 2, 1, 100m, 0.1m);

        var actions = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true };
        actions.Controls.Add(CreateButton("Add", AddEmployee));
        actions.Controls.Add(CreateButton("Update", UpdateEmployee));
        actions.Controls.Add(CreateButton("Delete", DeleteEmployee));
        actions.Controls.Add(CreateButton("Clear", ClearEmployeeForm));
        editArea.Controls.Add(actions, 0, 3);
        editArea.SetColumnSpan(actions, 4);
        editArea.RowCount = 4;
        page.Controls.Add(editArea, 0, 2);
        return page;
    }

    private Control BuildAttendancePage()
    {
        var page = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 3,
            Padding = new Padding(10)
        };
        page.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        page.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        page.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        var toolbar = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true };
        toolbar.Controls.Add(new Label { Text = "Month", AutoSize = true, Margin = new Padding(4, 9, 4, 4) });
        _monthPicker.Format = DateTimePickerFormat.Custom;
        _monthPicker.CustomFormat = "MM/yyyy";
        _monthPicker.ShowUpDown = true;
        toolbar.Controls.Add(_monthPicker);
        toolbar.Controls.Add(CreateButton("Load month", LoadAttendance));
        toolbar.Controls.Add(CreateButton("Save attendance", SaveAttendance));
        toolbar.Controls.Add(CreateButton("Close payroll month", ClosePayroll));
        page.Controls.Add(toolbar, 0, 0);

        ConfigureGrid(_attendanceGrid);
        _attendanceGrid.AllowUserToAddRows = false;
        _attendanceGrid.AllowUserToDeleteRows = false;
        _attendanceGrid.DataError += (_, e) =>
        {
            e.ThrowException = false;
            MessageBox.Show(this, "Enter valid numeric attendance values.", "Invalid value",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        };
        page.Controls.Add(_attendanceGrid, 0, 1);

        page.Controls.Add(new Label
        {
            Text = "Payroll: (Base salary × coefficient ÷ 22) × work days + overtime hours × 50,000",
            AutoSize = true,
            Padding = new Padding(0, 8, 0, 0)
        }, 0, 2);
        return page;
    }

    private void Form1_Load(object? sender, EventArgs e)
    {
        RunSafely(() =>
        {
            var departments = _employeeService.GetDepartments();
            _departmentFilter.DataSource = new[] { new DepartmentDto { MaPB = 0, TenPB = "All departments" } }
                .Concat(departments).ToList();
            _departmentFilter.DisplayMember = nameof(DepartmentDto.TenPB);
            _departmentFilter.ValueMember = nameof(DepartmentDto.MaPB);

            _employeeDepartment.DataSource = new[] { new DepartmentDto { MaPB = 0, TenPB = "(No department)" } }
                .Concat(departments).ToList();
            _employeeDepartment.DisplayMember = nameof(DepartmentDto.TenPB);
            _employeeDepartment.ValueMember = nameof(DepartmentDto.MaPB);

            SearchEmployees();
            LoadAttendance();
        });
    }

    private void SearchEmployees()
    {
        RunSafely(() =>
        {
            int departmentId = _departmentFilter.SelectedValue is int value ? value : 0;
            _employeeGrid.DataSource = _employeeService.SearchEmployees(
                _nameSearch.Text, departmentId == 0 ? null : departmentId, _nationalIdSearch.Text);
            SetHeader(_employeeGrid, nameof(EmployeeDto.MaNV), "Employee ID");
            SetHeader(_employeeGrid, nameof(EmployeeDto.HoTen), "Full name");
            SetHeader(_employeeGrid, nameof(EmployeeDto.NgaySinh), "Date of birth");
            SetHeader(_employeeGrid, nameof(EmployeeDto.CCCD), "National ID");
            SetHeader(_employeeGrid, nameof(EmployeeDto.MaPB), "Department ID");
            SetHeader(_employeeGrid, nameof(EmployeeDto.LuongCoBan), "Base salary");
            SetHeader(_employeeGrid, nameof(EmployeeDto.HeSoLuong), "Salary coefficient");
        });
    }

    private void EmployeeGrid_SelectionChanged(object? sender, EventArgs e)
    {
        if (_employeeGrid.CurrentRow?.DataBoundItem is not EmployeeDto employee)
        {
            return;
        }

        _selectedEmployeeId = employee.MaNV;
        _employeeName.Text = employee.HoTen;
        _nationalId.Text = employee.CCCD ?? string.Empty;
        _baseSalary.Value = Math.Clamp(employee.LuongCoBan, _baseSalary.Minimum, _baseSalary.Maximum);
        _salaryCoefficient.Value = Math.Clamp((decimal)employee.HeSoLuong,
            _salaryCoefficient.Minimum, _salaryCoefficient.Maximum);
        _dateOfBirth.Checked = employee.NgaySinh.HasValue;
        if (employee.NgaySinh.HasValue)
        {
            _dateOfBirth.Value = employee.NgaySinh.Value;
        }

        _employeeDepartment.SelectedValue = employee.MaPB ?? 0;
    }

    private void AddEmployee()
    {
        RunSafely(() =>
        {
            _employeeService.AddEmployee(ReadEmployeeForm());
            SearchEmployees();
            ClearEmployeeForm();
        });
    }

    private void UpdateEmployee()
    {
        RunSafely(() =>
        {
            EmployeeDto employee = ReadEmployeeForm();
            employee.MaNV = _selectedEmployeeId;
            _employeeService.UpdateEmployee(employee);
            SearchEmployees();
        });
    }

    private void DeleteEmployee()
    {
        if (_selectedEmployeeId == 0 || MessageBox.Show(this, "Delete the selected employee?", "Confirm delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        {
            return;
        }

        RunSafely(() =>
        {
            _employeeService.DeleteEmployee(_selectedEmployeeId);
            SearchEmployees();
            ClearEmployeeForm();
        });
    }

    private EmployeeDto ReadEmployeeForm() => new()
    {
        HoTen = _employeeName.Text,
        NgaySinh = _dateOfBirth.Checked ? _dateOfBirth.Value.Date : null,
        CCCD = string.IsNullOrWhiteSpace(_nationalId.Text) ? null : _nationalId.Text.Trim(),
        MaPB = _employeeDepartment.SelectedValue is int departmentId && departmentId != 0 ? departmentId : null,
        LuongCoBan = _baseSalary.Value,
        HeSoLuong = (double)_salaryCoefficient.Value
    };

    private void ClearEmployeeForm()
    {
        _selectedEmployeeId = 0;
        _employeeName.Clear();
        _nationalId.Clear();
        _dateOfBirth.Checked = false;
        _baseSalary.Value = _baseSalary.Minimum;
        _salaryCoefficient.Value = 1;
        if (_employeeDepartment.Items.Count > 0) _employeeDepartment.SelectedIndex = 0;
        _employeeGrid.ClearSelection();
    }

    private void LoadAttendance()
    {
        RunSafely(() =>
        {
            _attendanceTable = _attendanceService.LoadMonth(_monthPicker.Value.ToString("MM/yyyy"));
            _attendanceGrid.DataSource = _attendanceTable;
            SetHeader(_attendanceGrid, "MaNV", "Employee ID");
            SetHeader(_attendanceGrid, "HoTen", "Employee name");
            SetHeader(_attendanceGrid, "SoNgayCong", "Work days");
            SetHeader(_attendanceGrid, "SoGioOT", "Overtime hours");
            SetHeader(_attendanceGrid, "IsLocked", "Closed");
            _attendanceGrid.Columns["MaCong"].Visible = false;
            _attendanceGrid.Columns["ThangNam"].Visible = false;
            _attendanceGrid.Columns["MaNV"].ReadOnly = true;
            _attendanceGrid.Columns["HoTen"].ReadOnly = true;
            _attendanceGrid.Columns["IsLocked"].ReadOnly = true;
            bool locked = _attendanceTable.AsEnumerable().Any(row => row.Field<bool>("IsLocked"));
            _attendanceGrid.ReadOnly = locked;
        });
    }

    private void SaveAttendance()
    {
        RunSafely(() =>
        {
            _attendanceGrid.EndEdit();
            if (_attendanceGrid.DataSource is BindingSource source) source.EndEdit();
            int rows = _attendanceService.SaveMonth(_attendanceTable
                ?? throw new InvalidOperationException("Load an attendance month first."));
            MessageBox.Show(this, $"Attendance saved. Rows synchronized: {rows}.", "Attendance",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadAttendance();
        });
    }

    private void ClosePayroll()
    {
        if (MessageBox.Show(this, $"Close payroll for {_monthPicker.Value:MM/yyyy}? Attendance will be locked.",
                "Confirm payroll close", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
        {
            return;
        }

        RunSafely(() =>
        {
            int employees = _payrollService.CloseMonth(_monthPicker.Value.ToString("MM/yyyy"));
            MessageBox.Show(this, $"Payroll closed for {employees} employees.", "Payroll",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadAttendance();
        });
    }

    private void RunSafely(Action action)
    {
        try
        {
            action();
        }
        catch (Exception exception)
        {
            MessageBox.Show(this, exception.Message, "HRM & Payroll", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private static void ConfigureGrid(DataGridView grid)
    {
        grid.Dock = DockStyle.Fill;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.MultiSelect = false;
        grid.ReadOnly = true;
        grid.AllowUserToAddRows = false;
        grid.RowHeadersVisible = false;
    }

    private static Button CreateButton(string text, Action action)
    {
        var button = new Button { Text = text, AutoSize = true, Margin = new Padding(4) };
        button.Click += (_, _) => action();
        return button;
    }

    private static void AddEditorField(TableLayoutPanel layout, string label, Control editor, int column, int row)
    {
        layout.Controls.Add(new Label { Text = label, AutoSize = true, Anchor = AnchorStyles.Left,
            Margin = new Padding(4, 8, 8, 4) }, column, row);
        editor.Dock = DockStyle.Fill;
        editor.Margin = new Padding(4);
        layout.Controls.Add(editor, column + 1, row);
    }

    private static void ConfigureNumber(NumericUpDown control, int decimals, decimal increment, decimal maximum, decimal value)
    {
        control.DecimalPlaces = decimals;
        control.Increment = increment;
        control.Minimum = 0;
        control.Maximum = maximum;
        control.Value = value;
        control.ThousandsSeparator = true;
    }

    private static void SetHeader(DataGridView grid, string columnName, string header)
    {
        if (grid.Columns.Contains(columnName))
        {
            grid.Columns[columnName].HeaderText = header;
        }
    }
}
