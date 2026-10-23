namespace bai4
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblFromDate;
        private System.Windows.Forms.Label lblToDate;
        private System.Windows.Forms.Label lblBranch;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.ComboBox cbbBranch;
        private System.Windows.Forms.Button btnStatistics;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ProgressBar pgbLoading;
        private System.Windows.Forms.Label lblProcessingStatus;
        private System.Windows.Forms.DataGridView dgvRevenueData;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();

            this.lblFromDate = new System.Windows.Forms.Label();
            this.lblToDate = new System.Windows.Forms.Label();
            this.lblBranch = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.cbbBranch = new System.Windows.Forms.ComboBox();
            this.btnStatistics = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pgbLoading = new System.Windows.Forms.ProgressBar();
            this.lblProcessingStatus = new System.Windows.Forms.Label();
            this.dgvRevenueData = new System.Windows.Forms.DataGridView();

            this.SuspendLayout();

            this.lblFromDate.AutoSize = true;
            this.lblFromDate.Location = new System.Drawing.Point(24, 28);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(67, 15);
            this.lblFromDate.TabIndex = 0;
            this.lblFromDate.Text = "Từ ngày:";

            this.lblToDate.AutoSize = true;
            this.lblToDate.Location = new System.Drawing.Point(270, 28);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(60, 15);
            this.lblToDate.TabIndex = 1;
            this.lblToDate.Text = "Đến ngày:";

            this.lblBranch.AutoSize = true;
            this.lblBranch.Location = new System.Drawing.Point(520, 28);
            this.lblBranch.Name = "lblBranch";
            this.lblBranch.Size = new System.Drawing.Size(58, 15);
            this.lblBranch.TabIndex = 2;
            this.lblBranch.Text = "Chi nhánh:";

            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(95, 24);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(150, 23);
            this.dtpFrom.TabIndex = 3;

            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(335, 24);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(150, 23);
            this.dtpTo.TabIndex = 4;

            this.cbbBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbBranch.Location = new System.Drawing.Point(585, 24);
            this.cbbBranch.Name = "cbbBranch";
            this.cbbBranch.Size = new System.Drawing.Size(180, 23);
            this.cbbBranch.TabIndex = 5;

            this.btnStatistics.Location = new System.Drawing.Point(785, 18);
            this.btnStatistics.Name = "btnStatistics";
            this.btnStatistics.Size = new System.Drawing.Size(155, 35);
            this.btnStatistics.TabIndex = 6;
            this.btnStatistics.Text = "Thống Kê Doanh Thu";
            this.btnStatistics.UseVisualStyleBackColor = true;
            this.btnStatistics.Click += new System.EventHandler(this.btnStatistics_Click);

            this.btnCancel.Location = new System.Drawing.Point(785, 59);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(155, 35);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Hủy Thao Tác";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.pgbLoading.Location = new System.Drawing.Point(24, 90);
            this.pgbLoading.Name = "pgbLoading";
            this.pgbLoading.Size = new System.Drawing.Size(700, 23);
            this.pgbLoading.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pgbLoading.TabIndex = 8;
            this.pgbLoading.Maximum = 100;

            this.lblProcessingStatus.AutoSize = true;
            this.lblProcessingStatus.Location = new System.Drawing.Point(24, 126);
            this.lblProcessingStatus.Name = "lblProcessingStatus";
            this.lblProcessingStatus.Size = new System.Drawing.Size(0, 15);
            this.lblProcessingStatus.TabIndex = 9;
            this.lblProcessingStatus.ForeColor = System.Drawing.Color.DarkGreen;

            this.dgvRevenueData.AllowUserToAddRows = false;
            this.dgvRevenueData.AllowUserToDeleteRows = false;
            this.dgvRevenueData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRevenueData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRevenueData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRevenueData.Location = new System.Drawing.Point(24, 160);
            this.dgvRevenueData.Name = "dgvRevenueData";
            this.dgvRevenueData.ReadOnly = true;
            this.dgvRevenueData.RowHeadersWidth = 51;
            this.dgvRevenueData.Size = new System.Drawing.Size(916, 300);
            this.dgvRevenueData.TabIndex = 10;
            this.dgvRevenueData.AutoGenerateColumns = true;

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 490);
            this.Controls.Add(this.dgvRevenueData);
            this.Controls.Add(this.lblProcessingStatus);
            this.Controls.Add(this.pgbLoading);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnStatistics);
            this.Controls.Add(this.cbbBranch);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.lblBranch);
            this.Controls.Add(this.lblToDate);
            this.Controls.Add(this.lblFromDate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = true;
            this.MinimumSize = new System.Drawing.Size(760, 420);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bảng điều khiển thống kê doanh thu";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
