using System.ComponentModel;
using System.Globalization;

namespace bai2
{
    public partial class Form1 : Form
    {
        private readonly BindingList<BookModel> _books = new();
        private int _currentBookId = 1;

        public Form1()
        {
            InitializeComponent();
            ConfigureForm();
            AttachEventHandlers();
            LoadSampleData();
            UpdateStatusStrip();
        }

        private void ConfigureForm()
        {
            dgvBooks.AutoGenerateColumns = false;
            dgvBooks.DataSource = _books;

            var columns = new[]
            {
                new DataGridViewTextBoxColumn { DataPropertyName = nameof(BookModel.Id), HeaderText = "Mã" },
                new DataGridViewTextBoxColumn { DataPropertyName = nameof(BookModel.Title), HeaderText = "Tên sách" },
                new DataGridViewTextBoxColumn { DataPropertyName = nameof(BookModel.Author), HeaderText = "Tác giả" },
                new DataGridViewTextBoxColumn { DataPropertyName = nameof(BookModel.Category), HeaderText = "Thể loại" },
                new DataGridViewTextBoxColumn { DataPropertyName = nameof(BookModel.Price), HeaderText = "Giá" },
                new DataGridViewTextBoxColumn { DataPropertyName = nameof(BookModel.Quantity), HeaderText = "Số lượng" },
                new DataGridViewTextBoxColumn { DataPropertyName = nameof(BookModel.CoverImagePath), HeaderText = "Ảnh bìa" }
            };

            dgvBooks.Columns.AddRange(columns);
            dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBooks.MultiSelect = false;

            txtBookId.ReadOnly = true;
        }

        private void AttachEventHandlers()
        {
            btnBrowseCover.Click += btnBrowseCover_Click;
            btnAddBook.Click += btnAddBook_Click;
            btnDeleteBook.Click += btnDeleteBook_Click;
            btnSaveCsv.Click += btnSaveCsv_Click;
            btnOpenCsv.Click += btnOpenCsv_Click;
            btnClearForm.Click += btnClearForm_Click;
            dgvBooks.SelectionChanged += dgvBooks_SelectionChanged;
            formClosingTimer.Tick += formClosingTimer_Tick;
            menuOpenCsv.Click += btnOpenCsv_Click;
            menuSaveCsv.Click += btnSaveCsv_Click;
            menuExit.Click += (_, _) => Close();
            menuAddBook.Click += btnAddBook_Click;
            menuDeleteBook.Click += btnDeleteBook_Click;
            menuSearchBook.Click += menuSearchBook_Click;
            toolOpenCsv.Click += btnOpenCsv_Click;
            toolSaveCsv.Click += btnSaveCsv_Click;
            toolAddBook.Click += btnAddBook_Click;
            toolSearchBook.Click += menuSearchBook_Click;
        }

        private void LoadSampleData()
        {
            _books.Add(new BookModel { Id = _currentBookId++, Title = "Lập trình C#", Author = "Nguyễn Thành Đạt", Category = "CNTT", Price = 120000, Quantity = 5, CoverImagePath = "" });
            _books.Add(new BookModel { Id = _currentBookId++, Title = "SQL Server", Author = "Trần Thị B", Category = "Cơ sở dữ liệu", Price = 150000, Quantity = 3, CoverImagePath = "" });
            _books.Add(new BookModel { Id = _currentBookId++, Title = "Cấu trúc dữ liệu", Author = "Lê Văn C", Category = "Khoa học máy tính", Price = 180000, Quantity = 2, CoverImagePath = "" });
        }

        private void btnBrowseCover_Click(object sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp",
                Title = "Chọn ảnh bìa sách"
            };

            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                var filePath = openFileDialog.FileName;
                txtCoverImagePath.Text = filePath;
                try
                {
                    using var image = Image.FromFile(filePath);
                    picBookCover.Image = new Bitmap(image);
                }
                catch
                {
                    MessageBox.Show(this, "Không thể đọc ảnh được chọn.", "Lỗi ảnh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnAddBook_Click(object sender, EventArgs e)
        {
            if (!ValidateBookInput())
            {
                return;
            }

            var book = new BookModel
            {
                Id = _currentBookId++,
                Title = txtBookTitle.Text.Trim(),
                Author = txtBookAuthor.Text.Trim(),
                Category = txtBookCategory.Text.Trim(),
                Price = decimal.Parse(txtBookPrice.Text, CultureInfo.InvariantCulture),
                Quantity = int.Parse(txtBookQuantity.Text),
                CoverImagePath = txtCoverImagePath.Text.Trim()
            };

            _books.Add(book);
            ClearForm();
            UpdateStatusStrip();
            MessageBox.Show(this, "Thêm sách thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDeleteBook_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show(this, "Vui lòng chọn sách cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selected = (BookModel)dgvBooks.SelectedRows[0].DataBoundItem;
            _books.Remove(selected);
            ClearForm();
            UpdateStatusStrip();
            MessageBox.Show(this, "Đã xóa sách thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSaveCsv_Click(object sender, EventArgs e)
        {
            using var saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV files|*.csv",
                Title = "Lưu file CSV danh sách sách",
                DefaultExt = "csv"
            };

            if (saveFileDialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            var lines = new List<string> { "Id,Title,Author,Category,Price,Quantity,CoverImagePath" };
            foreach (var book in _books)
            {
                lines.Add(book.ToCsvLine());
            }

            File.WriteAllLines(saveFileDialog.FileName, lines);
            MessageBox.Show(this, "Xuất CSV thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnOpenCsv_Click(object sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog
            {
                Filter = "CSV files|*.csv",
                Title = "Mở file CSV danh sách sách"
            };

            if (openFileDialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
                var lines = File.ReadAllLines(openFileDialog.FileName);
                if (lines.Length < 2)
                {
                    MessageBox.Show(this, "File CSV rỗng hoặc không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _books.Clear();
                for (var i = 1; i < lines.Length; i++)
                {
                    var line = lines[i].Trim();
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    var book = BookModel.FromCsv(line);
                    _books.Add(book);
                    _currentBookId = Math.Max(_currentBookId, book.Id + 1);
                }

                ClearForm();
                UpdateStatusStrip();
                MessageBox.Show(this, "Nhập CSV thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Lỗi nhập CSV", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearForm_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void dgvBooks_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count == 0)
            {
                return;
            }

            var selected = (BookModel)dgvBooks.SelectedRows[0].DataBoundItem;
            if (selected == null)
            {
                return;
            }

            txtBookId.Text = selected.Id.ToString();
            txtBookTitle.Text = selected.Title;
            txtBookAuthor.Text = selected.Author;
            txtBookCategory.Text = selected.Category;
            txtBookPrice.Text = selected.Price.ToString(CultureInfo.InvariantCulture);
            txtBookQuantity.Text = selected.Quantity.ToString();
            txtCoverImagePath.Text = selected.CoverImagePath;

            if (!string.IsNullOrWhiteSpace(selected.CoverImagePath) && File.Exists(selected.CoverImagePath))
            {
                using var image = Image.FromFile(selected.CoverImagePath);
                picBookCover.Image = new Bitmap(image);
            }
            else
            {
                picBookCover.Image = null;
            }
        }

        private void menuSearchBook_Click(object sender, EventArgs e)
        {
            var keyword = Microsoft.VisualBasic.Interaction.InputBox("Nhập tên hoặc tác giả cần tìm:", "Tìm kiếm sách");
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return;
            }

            var results = _books.Where(b =>
                b.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                b.Author.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();

            if (results.Count == 0)
            {
                MessageBox.Show(this, "Không tìm thấy sách phù hợp.", "Kết quả tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var firstMatch = results[0];
            var index = _books.IndexOf(firstMatch);
            if (index >= 0)
            {
                dgvBooks.ClearSelection();
                dgvBooks.Rows[index].Selected = true;
                dgvBooks.FirstDisplayedScrollingRowIndex = index;
            }
        }

        private void ClearForm()
        {
            txtBookId.Clear();
            txtBookTitle.Clear();
            txtBookAuthor.Clear();
            txtBookCategory.Clear();
            txtBookPrice.Clear();
            txtBookQuantity.Clear();
            txtCoverImagePath.Clear();
            picBookCover.Image = null;
            dgvBooks.ClearSelection();
        }

        private bool ValidateBookInput()
        {
            if (string.IsNullOrWhiteSpace(txtBookTitle.Text) ||
                string.IsNullOrWhiteSpace(txtBookAuthor.Text) ||
                string.IsNullOrWhiteSpace(txtBookCategory.Text) ||
                string.IsNullOrWhiteSpace(txtBookPrice.Text) ||
                string.IsNullOrWhiteSpace(txtBookQuantity.Text))
            {
                MessageBox.Show(this, "Vui lòng nhập đầy đủ thông tin sách.", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(txtBookPrice.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out _))
            {
                MessageBox.Show(this, "Giá sách không hợp lệ.", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(txtBookQuantity.Text, out _))
            {
                MessageBox.Show(this, "Số lượng sách không hợp lệ.", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void UpdateStatusStrip()
        {
            var totalBooks = _books.Count;
            var totalValue = _books.Sum(book => book.Price * book.Quantity);
            var now = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            toolStripStatusLabelTotalBooks.Text = $"Tổng số đầu sách: {totalBooks}";
            toolStripStatusLabelTotalValue.Text = $"Tổng giá trị kho: {totalValue:N0} VNĐ";
            toolStripStatusLabelCurrentTime.Text = $"Thời gian hiện tại: {now}";
        }

        private void formClosingTimer_Tick(object sender, EventArgs e)
        {
            UpdateStatusStrip();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void lblBookCover_Click(object sender, EventArgs e)
        {

        }
    }
}
