namespace bai4
{
    public partial class Form1 : Form
    {
        private CancellationTokenSource? _processingCts;

        public Form1()
        {
            InitializeComponent();
            InitializeFormData();
        }

        private void InitializeFormData()
        {
            dtpFrom.Value = DateTime.Today.AddDays(-30);
            dtpTo.Value = DateTime.Today;

            cbbBranch.Items.Clear();
            cbbBranch.Items.Add("Tất cả chi nhánh");
            cbbBranch.Items.Add("Chi nhánh Hà Nội");
            cbbBranch.Items.Add("Chi nhánh Đà Nẵng");
            cbbBranch.Items.Add("Chi nhánh TP.HCM");
            cbbBranch.SelectedIndex = 0;

            dgvRevenueData.DataSource = null;
            lblProcessingStatus.Text = "Sẵn sàng";
            pgbLoading.Value = 0;
            btnCancel.Enabled = false;
        }

        private async void btnStatistics_Click(object sender, EventArgs e)
        {
            if (_processingCts != null)
            {
                return;
            }

            _processingCts = new CancellationTokenSource();
            var token = _processingCts.Token;

            btnStatistics.Enabled = false;
            btnCancel.Enabled = true;
            pgbLoading.Value = 0;
            pgbLoading.Style = ProgressBarStyle.Continuous;
            lblProcessingStatus.Text = "Đang truy xuất dữ liệu...";

            try
            {
                var startDate = dtpFrom.Value.Date;
                var endDate = dtpTo.Value.Date;
                var selectedBranch = cbbBranch.SelectedItem?.ToString();

                Action<int> updateProgress = null!;
                updateProgress = value =>
                {
                    if (InvokeRequired)
                    {
                        BeginInvoke(updateProgress, value);
                        return;
                    }

                    pgbLoading.Value = Math.Min(value, pgbLoading.Maximum);
                    lblProcessingStatus.Text = $"Đang xử lý... {value}%";
                };

                var result = await Task.Run(async () =>
                {
                    var records = new List<RevenueTransaction>();
                    var totalSteps = 100;

                    for (int step = 1; step <= totalSteps; step++)
                    {
                        token.ThrowIfCancellationRequested();
                        await Task.Delay(60, token);
                        updateProgress(step);
                    }

                    records = BuildRevenueTransactions(startDate, endDate, selectedBranch, token);
                    return records;
                }, token);

                if (token.IsCancellationRequested)
                {
                    return;
                }

                dgvRevenueData.DataSource = result;
                lblProcessingStatus.Text = $"Hoàn tất: {result.Count} giao dịch";
                pgbLoading.Value = pgbLoading.Maximum;
            }
            catch (OperationCanceledException)
            {
                lblProcessingStatus.Text = "Đã hủy thao tác!";
                pgbLoading.Value = 0;
                dgvRevenueData.DataSource = null;
            }
            catch (Exception ex)
            {
                lblProcessingStatus.Text = $"Lỗi: {ex.Message}";
                pgbLoading.Value = 0;
            }
            finally
            {
                btnStatistics.Enabled = true;
                btnCancel.Enabled = false;
                _processingCts?.Dispose();
                _processingCts = null;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (_processingCts == null)
            {
                return;
            }

            _processingCts.Cancel();
            lblProcessingStatus.Text = "Đã hủy thao tác!";
            pgbLoading.Value = 0;
            btnCancel.Enabled = false;
        }

        private List<RevenueTransaction> BuildRevenueTransactions(DateTime startDate, DateTime endDate, string? selectedBranch, CancellationToken token)
        {
            var records = new List<RevenueTransaction>();
            var random = new Random();
            var branches = new[]
            {
                "Chi nhánh Hà Nội",
                "Chi nhánh Đà Nẵng",
                "Chi nhánh TP.HCM"
            };

            var totalDays = Math.Max(1, (endDate - startDate).Days + 1);

            for (int i = 1; i <= 300; i++)
            {
                token.ThrowIfCancellationRequested();

                var transactionDate = startDate.AddDays(random.Next(totalDays));
                var branch = selectedBranch == "Tất cả chi nhánh"
                    ? branches[random.Next(branches.Length)]
                    : selectedBranch ?? branches[random.Next(branches.Length)];

                records.Add(new RevenueTransaction
                {
                    Id = i,
                    Branch = branch,
                    TransactionDate = transactionDate,
                    Amount = random.Next(500000, 25000000),
                    PaymentMethod = random.Next(0, 2) == 0 ? "Tiền mặt" : "Chuyển khoản",
                    Note = random.Next(0, 2) == 0 ? "Bán hàng" : "Dịch vụ"
                });
            }

            return records;
        }
    }

    public class RevenueTransaction
    {
        public int Id { get; set; }
        public string Branch { get; set; } = string.Empty;
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
    }
}
