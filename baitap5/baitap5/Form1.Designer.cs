namespace baitap5
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.GroupBox groupBoxInputs;
        private System.Windows.Forms.TableLayoutPanel inputsTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Panel rightPanel;
        private System.Windows.Forms.ListBox listBoxItems;
        private System.Windows.Forms.Panel buttonsPanel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxInputs = new System.Windows.Forms.GroupBox();
            this.inputsTable = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.listBoxItems = new System.Windows.Forms.ListBox();
            this.buttonsPanel = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 2;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanelMain.RowCount = 1;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // groupBoxInputs
            // 
            this.groupBoxInputs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxInputs.Text = "Input";
            this.groupBoxInputs.Padding = new System.Windows.Forms.Padding(10);
            // 
            // inputsTable
            // 
            this.inputsTable.ColumnCount = 2;
            this.inputsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.inputsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.inputsTable.RowCount = 3;
            this.inputsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.inputsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.inputsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.inputsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputsTable.Padding = new System.Windows.Forms.Padding(5);
            // 
            // label1
            // 
            this.label1.Text = "Field 1:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            // 
            // label2
            // 
            this.label2.Text = "Field 2:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            // 
            // label3
            // 
            this.label3.Text = "Field 3:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            // 
            // add controls to inputsTable
            // 
            this.inputsTable.Controls.Add(this.label1, 0, 0);
            this.inputsTable.Controls.Add(this.textBox1, 1, 0);
            this.inputsTable.Controls.Add(this.label2, 0, 1);
            this.inputsTable.Controls.Add(this.textBox2, 1, 1);
            this.inputsTable.Controls.Add(this.label3, 0, 2);
            this.inputsTable.Controls.Add(this.textBox3, 1, 2);
            this.groupBoxInputs.Controls.Add(this.inputsTable);
            // 
            // rightPanel
            // 
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightPanel.Padding = new System.Windows.Forms.Padding(10);
            // 
            // listBoxItems
            // 
            this.listBoxItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // buttonsPanel
            // 
            this.buttonsPanel.Height = 50;
            this.buttonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonsPanel.BackColor = System.Drawing.Color.Transparent;
            // 
            // buttons
            // 
            this.btnAdd.Text = "Add";
            this.btnEdit.Text = "Edit";
            this.btnDelete.Text = "Delete";
            this.btnAdd.Size = new System.Drawing.Size(75, 30);
            this.btnEdit.Size = new System.Drawing.Size(75, 30);
            this.btnDelete.Size = new System.Drawing.Size(75, 30);
            // add buttons to buttonsPanel
            this.buttonsPanel.Controls.Add(this.btnAdd);
            this.buttonsPanel.Controls.Add(this.btnEdit);
            this.buttonsPanel.Controls.Add(this.btnDelete);
            // add list and buttons to rightPanel
            this.rightPanel.Controls.Add(this.listBoxItems);
            this.rightPanel.Controls.Add(this.buttonsPanel);
            // add left and right to main table
            this.tableLayoutPanelMain.Controls.Add(this.groupBoxInputs, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.rightPanel, 1, 0);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Text = "Form1";

            // adjust button positions when buttonsPanel resized
            this.buttonsPanel.Resize += (s, e) =>
            {
                int right = this.buttonsPanel.ClientSize.Width - 10;
                this.btnDelete.Location = new System.Drawing.Point(right - this.btnDelete.Width, 10);
                right -= (this.btnDelete.Width + 10);
                this.btnEdit.Location = new System.Drawing.Point(right - this.btnEdit.Width, 10);
                right -= (this.btnEdit.Width + 10);
                this.btnAdd.Location = new System.Drawing.Point(right - this.btnAdd.Width, 10);
            };
        }

        #endregion
    }
}
