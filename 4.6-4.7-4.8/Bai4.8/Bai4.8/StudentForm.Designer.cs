namespace Bai4._8;

partial class StudentForm
{
    private System.ComponentModel.IContainer components = null;
    private Button loadButton;
    private ProgressBar progressBar;
    private Label progressLabel;
    private Label instructionLabel;
    private DataGridView studentGridView;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        loadButton = new Button();
        progressBar = new ProgressBar();
        progressLabel = new Label();
        instructionLabel = new Label();
        studentGridView = new DataGridView();
        ((System.ComponentModel.ISupportInitialize)studentGridView).BeginInit();
        SuspendLayout();
        loadButton.Location = new Point(20, 48);
        loadButton.Name = "loadButton";
        loadButton.Size = new Size(160, 35);
        loadButton.Text = "Nạp danh sách";
        loadButton.UseVisualStyleBackColor = true;
        loadButton.Click += loadButton_Click;
        instructionLabel.AutoSize = true;
        instructionLabel.Location = new Point(20, 18);
        instructionLabel.Text = "Nạp dữ liệu sinh viên bất đồng bộ:";
        progressBar.Location = new Point(200, 56);
        progressBar.Name = "progressBar";
        progressBar.Size = new Size(500, 23);
        progressBar.Minimum = 0;
        progressBar.Maximum = 100;
        progressLabel.AutoSize = true;
        progressLabel.Location = new Point(715, 60);
        progressLabel.Name = "progressLabel";
        progressLabel.Text = "0%";
        studentGridView.AllowUserToAddRows = false;
        studentGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        studentGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        studentGridView.Location = new Point(20, 105);
        studentGridView.Name = "studentGridView";
        studentGridView.ReadOnly = true;
        studentGridView.RowHeadersVisible = false;
        studentGridView.Size = new Size(900, 450);
        studentGridView.TabIndex = 0;
        ClientSize = new Size(950, 580);
        Controls.Add(studentGridView);
        Controls.Add(progressLabel);
        Controls.Add(progressBar);
        Controls.Add(instructionLabel);
        Controls.Add(loadButton);
        Text = "Danh sách sinh viên";
        ((System.ComponentModel.ISupportInitialize)studentGridView).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}