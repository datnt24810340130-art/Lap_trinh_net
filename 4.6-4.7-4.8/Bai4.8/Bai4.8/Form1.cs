namespace Bai4._8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            IsMdiContainer = true;
            var dataForm = new StudentForm(UpdateProgress);
            dataForm.MdiParent = this;
            dataForm.Show();
        }

        private void UpdateProgress(int progress)
        {
            progressToolStripProgressBar.Value = Math.Clamp(progress, 0, 100);
            progressToolStripStatusLabel.Text = $"Tiến trình: {progress}%";
        }
    }
}
