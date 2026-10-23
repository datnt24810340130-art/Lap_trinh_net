namespace bai5
{
    public partial class frmCryptoStockTicker : Form
    {
        private readonly Random _random = new();
        private CancellationTokenSource? _monitorCancellation;

        private decimal _currentPrice = 65000m;
        private int _syncSuccessCount = 0;
        private bool _isMonitoring = false;

        private Button? btnStart;
        private Button? btnPause;
        private Label? lblBTCPrice;
        private StatusStrip? statusStrip;
        private ToolStripStatusLabel? lblLastUpdate;
        private ToolStripStatusLabel? lblSuccessCount;
        private DataGridView? dgvHistoryLog;

        public frmCryptoStockTicker()
        {
            InitializeComponent();
            InitializeTickerData();
        }

        private void InitializeComponent()
        {
            this.Text = "Crypto Stock Ticker";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(920, 560);
            this.MinimumSize = new Size(800, 450);
            this.FormClosing += FormCryptoStockTicker_FormClosing;

            btnStart = new Button
            {
                Text = "Bắt đầu Giám sát",
                Size = new Size(180, 42),
                Location = new Point(20, 20),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            btnStart.FlatAppearance.BorderSize = 0;
            btnStart.Click += btnStart_Click;

            btnPause = new Button
            {
                Text = "Tạm dừng",
                Size = new Size(140, 42),
                Location = new Point(220, 20),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Enabled = false
            };
            btnPause.FlatAppearance.BorderSize = 0;
            btnPause.Click += btnPause_Click;

            lblBTCPrice = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 24F, FontStyle.Bold),
                Location = new Point(20, 90),
                Text = $"{_currentPrice:C0}",
                ForeColor = Color.Green,
                Anchor = AnchorStyles.Left | AnchorStyles.Top
            };

            dgvHistoryLog = new DataGridView
            {
                Location = new Point(20, 150),
                Size = new Size(860, 300),
                AutoGenerateColumns = false,
                AllowUserToAddRows = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D
            };

            dgvHistoryLog.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Thời gian",
                Name = "Time",
                DataPropertyName = "Time",
                ReadOnly = true
            });
            dgvHistoryLog.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Giá BTC/USD",
                Name = "Price",
                DataPropertyName = "Price",
                ReadOnly = true
            });
            dgvHistoryLog.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Xu hướng",
                Name = "Trend",
                DataPropertyName = "Trend",
                ReadOnly = true
            });

            statusStrip = new StatusStrip
            {
                Dock = DockStyle.Bottom,
                SizingGrip = false
            };

            lblLastUpdate = new ToolStripStatusLabel { Text = "--:--:--" };
            lblSuccessCount = new ToolStripStatusLabel { Text = "0 lượt đồng bộ" };

            statusStrip.Items.Add(lblLastUpdate);
            statusStrip.Items.Add(new ToolStripStatusLabel(" | "));
            statusStrip.Items.Add(lblSuccessCount);

            this.Controls.Add(btnStart);
            this.Controls.Add(btnPause);
            this.Controls.Add(lblBTCPrice);
            this.Controls.Add(dgvHistoryLog);
            this.Controls.Add(statusStrip);
            this.ResumeLayout(false);
        }

        private void InitializeTickerData()
        {
            lblBTCPrice!.Text = $"{_currentPrice:C0}";
            lblBTCPrice.ForeColor = Color.Green;
            UpdateStatusStrip();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (_isMonitoring)
            {
                return;
            }

            _monitorCancellation?.Dispose();
            _monitorCancellation = new CancellationTokenSource();

            _isMonitoring = true;
            btnStart.Enabled = false;
            btnPause.Enabled = true;

            _ = Task.Run(() => MonitorPriceLoop(_monitorCancellation.Token));
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            PauseMonitoring();
        }

        private void MonitorPriceLoop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                Thread.Sleep(2000);

                if (token.IsCancellationRequested)
                {
                    break;
                }

                decimal nextPrice = GenerateRandomPrice();
                if (InvokeRequired)
                {
                    Invoke(new Action(() => ApplyNewPrice(nextPrice)));
                    continue;
                }

                ApplyNewPrice(nextPrice);
            }
        }

        private decimal GenerateRandomPrice()
        {
            decimal variation = _random.Next(-3000, 3001);
            decimal nextPrice = _currentPrice + variation;
            if (nextPrice < 50000m)
            {
                nextPrice = 50000m;
            }

            if (nextPrice > 120000m)
            {
                nextPrice = 120000m;
            }

            return nextPrice;
        }

        private void ApplyNewPrice(decimal nextPrice)
        {
            bool isIncrease = nextPrice > _currentPrice;
            _currentPrice = nextPrice;
            _syncSuccessCount++;

            lblBTCPrice!.Text = $"{_currentPrice:C0}";
            lblBTCPrice.ForeColor = isIncrease ? Color.Green : Color.Red;

            DateTime updateTime = DateTime.Now;
            string trendText = isIncrease ? "Tăng" : "Giảm";
            dgvHistoryLog!.Rows.Add(updateTime.ToString("HH:mm:ss"), $"{_currentPrice:C0}", trendText);

            if (dgvHistoryLog.Rows.Count > 20)
            {
                dgvHistoryLog.Rows.RemoveAt(0);
            }

            UpdateStatusStrip(updateTime);
        }

        private void UpdateStatusStrip(DateTime? updateTime = null)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateStatusStrip(updateTime)));
                return;
            }

            lblLastUpdate!.Text = updateTime?.ToString("HH:mm:ss") ?? "--:--:--";
            lblSuccessCount!.Text = $"{_syncSuccessCount} lượt đồng bộ";
        }

        private void PauseMonitoring()
        {
            _isMonitoring = false;
            _monitorCancellation?.Cancel();
            btnStart.Enabled = true;
            btnPause.Enabled = false;
        }

        private void FormCryptoStockTicker_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isMonitoring)
            {
                PauseMonitoring();
            }
        }
    }
}
