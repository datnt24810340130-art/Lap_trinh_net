using System.ComponentModel;
using System.Text.RegularExpressions;

namespace baitap1
{
    public class frmGymMemberManager : Form
    {
        private readonly BindingList<MemberModel> _members = new();
        private readonly BindingSource _bindingSource = new();
        private readonly ErrorProvider _errorProvider = new();

        private TextBox txtMemberId = null!;
        private TextBox txtFullName = null!;
        private TextBox txtPhone = null!;
        private ComboBox cboPackage = null!;
        private DateTimePicker dtpStartDate = null!;
        private TextBox txtFee = null!;
        private DataGridView dgvMembers = null!;
        private Button btnAdd = null!;
        private Button btnEdit = null!;
        private Button btnDelete = null!;
        private Button btnRefresh = null!;

        public frmGymMemberManager()
        {
            InitializeForm();
            ConfigureDataGrid();
            ConfigureBindings();
            ConfigureValidation();

            _bindingSource.DataSource = _members;
            LoadSampleData();
            RefreshSelection();
        }

        private void InitializeForm()
        {
            Text = "Quản lý hội viên phòng gym";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(1000, 620);
            MinimumSize = new Size(900, 540);
            BackColor = Color.FromArgb(245, 247, 250);

            var panelTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 220,
                Padding = new Padding(16),
                BackColor = Color.FromArgb(245, 247, 250)
            };

            var groupInfo = new GroupBox
            {
                Text = "Thông tin hội viên",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(31, 41, 55),
                Padding = new Padding(10)
            };

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 3,
                Padding = new Padding(6),
                BackColor = Color.Transparent
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));

            var labels = new[]
            {
                new { Text = "Mã hội viên", Control = (Control)(txtMemberId = CreateTextBox(true)) },
                new { Text = "Họ và tên", Control = (Control)(txtFullName = CreateTextBox()) },
                new { Text = "Số điện thoại", Control = (Control)(txtPhone = CreateTextBox()) },
                new { Text = "Gói tập", Control = (Control)(cboPackage = CreateComboBox()) },
                new { Text = "Ngày đăng ký", Control = (Control)(dtpStartDate = CreateDateTimePicker()) },
                new { Text = "Phí dịch vụ", Control = (Control)(txtFee = CreateTextBox()) }
            };

            int index = 0;
            foreach (var item in labels)
            {
                var label = new Label
                {
                    Text = item.Text,
                    Dock = DockStyle.Fill,
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(55, 65, 81),
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Margin = new Padding(4)
                };
                item.Control.Margin = new Padding(4);
                layout.Controls.Add(label, index % 2 == 0 ? 0 : 2, index / 2);
                layout.Controls.Add(item.Control, index % 2 == 0 ? 1 : 3, index / 2);
                index++;
            }

            groupInfo.Controls.Add(layout);
            panelTop.Controls.Add(groupInfo);

            var actionPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                Padding = new Padding(8),
                BackColor = Color.FromArgb(255, 255, 255)
            };

            btnAdd = new Button { Text = "Thêm mới", Width = 120, Height = 36, BackColor = Color.FromArgb(34, 197, 94), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            btnEdit = new Button { Text = "Sửa thông tin", Width = 135, Height = 36, BackColor = Color.FromArgb(59, 130, 246), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            btnDelete = new Button { Text = "Xóa hội viên", Width = 130, Height = 36, BackColor = Color.FromArgb(239, 68, 68), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            btnRefresh = new Button { Text = "Làm mới", Width = 110, Height = 36, BackColor = Color.FromArgb(148, 163, 184), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };

            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            btnRefresh.Click += BtnRefresh_Click;

            var flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoScroll = false,
                Padding = new Padding(10, 12, 10, 0)
            };

            flow.Controls.Add(btnAdd);
            flow.Controls.Add(btnEdit);
            flow.Controls.Add(btnDelete);
            flow.Controls.Add(btnRefresh);
            actionPanel.Controls.Add(flow);

            dgvMembers = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowHeadersVisible = false,
                BorderStyle = BorderStyle.None,
                BackgroundColor = Color.White,
                Font = new Font("Segoe UI", 9.5F),
                EnableHeadersVisualStyles = false,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(15, 23, 42),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            };

            var container = new Panel { Dock = DockStyle.Fill, Padding = new Padding(12, 8, 12, 12) };
            container.Controls.Add(dgvMembers);

            Controls.Add(container);
            Controls.Add(actionPanel);
            Controls.Add(panelTop);
            _errorProvider.ContainerControl = this;
            _errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            _errorProvider.Icon = SystemIcons.Warning;

            _errorProvider.SetIconAlignment(txtMemberId, ErrorIconAlignment.MiddleRight);
            _errorProvider.SetIconAlignment(txtFullName, ErrorIconAlignment.MiddleRight);
            _errorProvider.SetIconAlignment(txtPhone, ErrorIconAlignment.MiddleRight);
            _errorProvider.SetIconAlignment(txtFee, ErrorIconAlignment.MiddleRight);
            _errorProvider.SetIconPadding(txtMemberId, 12);
            _errorProvider.SetIconPadding(txtFullName, 12);
            _errorProvider.SetIconPadding(txtPhone, 12);
            _errorProvider.SetIconPadding(txtFee, 12);

            txtFullName.MaximumSize = new Size(280, 0);
            txtPhone.MaximumSize = new Size(280, 0);
            txtFee.MaximumSize = new Size(280, 0);
            txtMemberId.MaximumSize = new Size(260, 0);

            txtFullName.Width = 260;
            txtPhone.Width = 260;
            txtFee.Width = 260;
            txtMemberId.Width = 220;
        }

        private void ConfigureDataGrid()
        {
            dgvMembers.AutoGenerateColumns = false;
            dgvMembers.DataSource = _bindingSource;
            dgvMembers.Columns.Clear();

            dgvMembers.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Mã HV",
                DataPropertyName = nameof(MemberModel.MemberId),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvMembers.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Tên hội viên",
                DataPropertyName = nameof(MemberModel.FullName),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvMembers.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "SĐT",
                DataPropertyName = nameof(MemberModel.Phone),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            dgvMembers.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Gói tập",
                DataPropertyName = nameof(MemberModel.Package),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            dgvMembers.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Ngày ĐK",
                DataPropertyName = nameof(MemberModel.StartDate),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                DefaultCellStyle = { Format = "dd/MM/yyyy" }
            });

            dgvMembers.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Phí dịch vụ",
                DataPropertyName = nameof(MemberModel.Fee),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                DefaultCellStyle = { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
            });
        }

        private void ConfigureBindings()
        {
            _bindingSource.DataSource = _members;
            txtMemberId.DataBindings.Add("Text", _bindingSource, nameof(MemberModel.MemberId), true, DataSourceUpdateMode.OnPropertyChanged);
            txtFullName.DataBindings.Add("Text", _bindingSource, nameof(MemberModel.FullName), true, DataSourceUpdateMode.OnPropertyChanged);
            txtPhone.DataBindings.Add("Text", _bindingSource, nameof(MemberModel.Phone), true, DataSourceUpdateMode.OnPropertyChanged);
            cboPackage.DataBindings.Add("Text", _bindingSource, nameof(MemberModel.Package), true, DataSourceUpdateMode.OnPropertyChanged);
            dtpStartDate.DataBindings.Add("Value", _bindingSource, nameof(MemberModel.StartDate), true, DataSourceUpdateMode.OnPropertyChanged);
            txtFee.DataBindings.Add("Text", _bindingSource, nameof(MemberModel.Fee), true, DataSourceUpdateMode.OnPropertyChanged);

            _bindingSource.PositionChanged += (_, _) => RefreshValidation();
            dgvMembers.SelectionChanged += (_, _) => RefreshValidation();

            txtFullName.Validating += (_, _) => ValidateCurrentMember();
            txtPhone.Validating += (_, _) => ValidateCurrentMember();
            txtFee.Validating += (_, _) => ValidateCurrentMember();
            txtFullName.TextChanged += (_, _) => ValidateCurrentMember();
            txtPhone.TextChanged += (_, _) => ValidateCurrentMember();
            txtFee.TextChanged += (_, _) => ValidateCurrentMember();
        }

        private void ConfigureValidation()
        {
            txtFullName.KeyPress += (_, e) =>
            {
                if (char.IsControl(e.KeyChar)) return;
                if (char.IsLetter(e.KeyChar) || char.IsWhiteSpace(e.KeyChar) || e.KeyChar == '-') return;
                e.Handled = true;
            };

            txtPhone.KeyPress += (_, e) =>
            {
                if (char.IsControl(e.KeyChar)) return;
                if (char.IsDigit(e.KeyChar)) return;
                e.Handled = true;
            };

            txtFee.KeyPress += (_, e) =>
            {
                if (char.IsControl(e.KeyChar)) return;
                if (char.IsDigit(e.KeyChar)) return;
                e.Handled = true;
            };
        }

        private void LoadSampleData()
        {
            _members.Add(new MemberModel { MemberId = "HV001", FullName = "Nguyễn Văn An", Phone = "0912345678", Package = "Theo tháng", StartDate = new DateTime(2025, 1, 10), Fee = 350000 });
            _members.Add(new MemberModel { MemberId = "HV002", FullName = "Trần Thị Bình", Phone = "0987654321", Package = "6 tháng", StartDate = new DateTime(2025, 2, 15), Fee = 1800000 });
            _members.Add(new MemberModel { MemberId = "HV003", FullName = "Lê Hoàng Cường", Phone = "0909123456", Package = "1 năm", StartDate = new DateTime(2025, 3, 20), Fee = 2800000 });
            _bindingSource.DataSource = _members;
        }

        private void RefreshSelection()
        {
            if (_members.Count > 0)
            {
                _bindingSource.Position = 0;
            }
            else
            {
                var emptyMember = new MemberModel { MemberId = GenerateMemberId(), Package = "Theo tháng", StartDate = DateTime.Today };
                _bindingSource.DataSource = _members;
                txtMemberId.Text = emptyMember.MemberId;
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var member = new MemberModel
            {
                MemberId = GenerateMemberId(),
                FullName = "",
                Phone = "",
                Package = "Theo tháng",
                StartDate = DateTime.Today,
                Fee = 0
            };
            _members.Add(member);
            _bindingSource.Position = _members.Count - 1;
            ClearErrors();
            txtFullName.Focus();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (_bindingSource.Current is not MemberModel member)
            {
                MessageBox.Show("Vui lòng chọn hội viên để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (ValidateCurrentMember())
            {
                MessageBox.Show("Cập nhật thông tin hội viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (_bindingSource.Current is not MemberModel member)
            {
                MessageBox.Show("Vui lòng chọn hội viên cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa hội viên {member.FullName} ({member.MemberId})?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                _members.Remove(member);
                ClearErrors();
                if (_members.Count == 0)
                {
                    txtMemberId.Text = string.Empty;
                    txtFullName.Text = string.Empty;
                    txtPhone.Text = string.Empty;
                    txtFee.Text = string.Empty;
                    cboPackage.SelectedIndex = 0;
                    dtpStartDate.Value = DateTime.Today;
                }
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            ClearErrors();
            if (_members.Count > 0)
            {
                _bindingSource.Position = 0;
            }
            else
            {
                txtMemberId.Text = string.Empty;
                txtFullName.Text = string.Empty;
                txtPhone.Text = string.Empty;
                txtFee.Text = string.Empty;
                cboPackage.SelectedIndex = 0;
                dtpStartDate.Value = DateTime.Today;
            }
        }

        private bool ValidateCurrentMember()
        {
            if (_bindingSource.Current is not MemberModel member)
            {
                ClearErrors();
                return false;
            }

            bool isValid = true;
            ClearErrors();

            if (string.IsNullOrWhiteSpace(member.FullName) || !Regex.IsMatch(member.FullName.Trim(), "^[\\p{L}\\s]+$") || member.FullName.Trim().Length < 2)
            {
                _errorProvider.SetError(txtFullName, "Tên hội viên không được để trống và chỉ chứa chữ cái, khoảng trắng.");
                isValid = false;
            }

            if (!Regex.IsMatch(member.Phone ?? string.Empty, "^\\d{10}$"))
            {
                _errorProvider.SetError(txtPhone, "Số điện thoại phải có đúng 10 chữ số.");
                isValid = false;
            }

            if (member.Fee <= 0)
            {
                _errorProvider.SetError(txtFee, "Phí dịch vụ phải là số nguyên dương.");
                isValid = false;
            }

            return isValid;
        }

        private void RefreshValidation()
        {
            if (_bindingSource.Current is MemberModel)
            {
                ValidateCurrentMember();
            }
            else
            {
                ClearErrors();
            }
        }

        private void ClearErrors()
        {
            _errorProvider.SetError(txtFullName, string.Empty);
            _errorProvider.SetError(txtPhone, string.Empty);
            _errorProvider.SetError(txtFee, string.Empty);
        }

        private string GenerateMemberId()
        {
            var lastNumber = _members
                .Select(m => m.MemberId)
                .Where(id => !string.IsNullOrWhiteSpace(id) && id.StartsWith("HV", StringComparison.OrdinalIgnoreCase))
                .Select(id => int.TryParse(id.Substring(2), out var value) ? value : 0)
                .DefaultIfEmpty(0)
                .Max();

            return $"HV{lastNumber + 1:000}";
        }

        private static TextBox CreateTextBox(bool readOnly = false)
        {
            return new TextBox
            {
                Dock = DockStyle.None,
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                Width = 240,
                Font = new Font("Segoe UI", 10F),
                BorderStyle = BorderStyle.FixedSingle,
                ReadOnly = readOnly,
                Margin = new Padding(0, 0, 16, 0)
            };
        }

        private static ComboBox CreateComboBox()
        {
            var combo = new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F),
                Margin = new Padding(0, 0, 0, 0),
                Items = { "Theo tháng", "6 tháng", "1 năm" }
            };
            combo.SelectedIndex = 0;
            return combo;
        }

        private static DateTimePicker CreateDateTimePicker()
        {
            return new DateTimePicker
            {
                Dock = DockStyle.Fill,
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Today,
                Font = new Font("Segoe UI", 10F),
                Margin = new Padding(0, 0, 0, 0)
            };
        }
    }
}
