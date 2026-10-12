namespace Bai4._8
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel progressToolStripStatusLabel;
        private ToolStripProgressBar progressToolStripProgressBar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            statusStrip = new StatusStrip();
            progressToolStripStatusLabel = new ToolStripStatusLabel();
            progressToolStripProgressBar = new ToolStripProgressBar();
            statusStrip.SuspendLayout();
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 650);
            Text = "Bài 4.8 - Lập trình bất đồng bộ";
            statusStrip.Items.AddRange(new ToolStripItem[] { progressToolStripStatusLabel, progressToolStripProgressBar });
            statusStrip.Location = new Point(0, 628);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(1000, 22);
            progressToolStripStatusLabel.Name = "progressToolStripStatusLabel";
            progressToolStripStatusLabel.Size = new Size(84, 17);
            progressToolStripStatusLabel.Text = "Tiến trình: 0%";
            progressToolStripProgressBar.Name = "progressToolStripProgressBar";
            progressToolStripProgressBar.Size = new Size(150, 16);
            progressToolStripProgressBar.Step = 1;
            progressToolStripProgressBar.Minimum = 0;
            progressToolStripProgressBar.Maximum = 100;
            Controls.Add(statusStrip);
            MainMenuStrip = null;
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
        }

        #endregion
    }
}
