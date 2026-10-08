namespace bai5ontapwindownform
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            splitContainer1 = new SplitContainer();
            tabControlLeft = new TabControl();
            tabPageCustomer = new TabPage();
            groupBoxCustomer = new GroupBox();
            txtCustomerName = new TextBox();
            label1 = new Label();
            tabPageShipping = new TabPage();
            groupBoxShipping = new GroupBox();
            comboShipping = new ComboBox();
            dgvItems = new DataGridView();
            colItem = new DataGridViewTextBoxColumn();
            colQuantity = new DataGridViewTextBoxColumn();
            colWeight = new DataGridViewTextBoxColumn();
            colUnitPrice = new DataGridViewTextBoxColumn();
            colLineTotal = new DataGridViewTextBoxColumn();
            statusStrip1 = new StatusStrip();
            tslTime = new ToolStripStatusLabel();
            tslTotalQuantity = new ToolStripStatusLabel();
            tslTotalWeight = new ToolStripStatusLabel();
            tslTotalAmount = new ToolStripStatusLabel();
            errorProvider1 = new ErrorProvider(components);
            timer1 = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tabControlLeft.SuspendLayout();
            tabPageCustomer.SuspendLayout();
            groupBoxCustomer.SuspendLayout();
            tabPageShipping.SuspendLayout();
            groupBoxShipping.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvItems).BeginInit();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(tabControlLeft);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(dgvItems);
            splitContainer1.Size = new Size(1196, 424);
            splitContainer1.SplitterDistance = 388;
            splitContainer1.TabIndex = 0;
            // 
            // tabControlLeft
            // 
            tabControlLeft.Controls.Add(tabPageCustomer);
            tabControlLeft.Controls.Add(tabPageShipping);
            tabControlLeft.Dock = DockStyle.Fill;
            tabControlLeft.Location = new Point(0, 0);
            tabControlLeft.Name = "tabControlLeft";
            tabControlLeft.SelectedIndex = 0;
            tabControlLeft.Size = new Size(388, 424);
            tabControlLeft.TabIndex = 0;
            // 
            // tabPageCustomer
            // 
            tabPageCustomer.Controls.Add(groupBoxCustomer);
            tabPageCustomer.Location = new Point(4, 29);
            tabPageCustomer.Name = "tabPageCustomer";
            tabPageCustomer.Padding = new Padding(3);
            tabPageCustomer.Size = new Size(380, 391);
            tabPageCustomer.TabIndex = 0;
            tabPageCustomer.Text = "Customer";
            tabPageCustomer.UseVisualStyleBackColor = true;
            // 
            // groupBoxCustomer
            // 
            groupBoxCustomer.Controls.Add(txtCustomerName);
            groupBoxCustomer.Controls.Add(label1);
            groupBoxCustomer.Dock = DockStyle.Top;
            groupBoxCustomer.Location = new Point(3, 3);
            groupBoxCustomer.Name = "groupBoxCustomer";
            groupBoxCustomer.Size = new Size(374, 120);
            groupBoxCustomer.TabIndex = 0;
            groupBoxCustomer.TabStop = false;
            groupBoxCustomer.Text = "Customer Info";
            // 
            // txtCustomerName
            // 
            txtCustomerName.Location = new Point(8, 55);
            txtCustomerName.Name = "txtCustomerName";
            txtCustomerName.Size = new Size(228, 27);
            txtCustomerName.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 22);
            label1.Name = "label1";
            label1.Size = new Size(119, 20);
            label1.TabIndex = 0;
            label1.Text = "Customer Name:";
            // 
            // tabPageShipping
            // 
            tabPageShipping.Controls.Add(groupBoxShipping);
            tabPageShipping.Location = new Point(4, 29);
            tabPageShipping.Name = "tabPageShipping";
            tabPageShipping.Padding = new Padding(3);
            tabPageShipping.Size = new Size(252, 395);
            tabPageShipping.TabIndex = 1;
            tabPageShipping.Text = "Shipping";
            tabPageShipping.UseVisualStyleBackColor = true;
            // 
            // groupBoxShipping
            // 
            groupBoxShipping.Controls.Add(comboShipping);
            groupBoxShipping.Dock = DockStyle.Top;
            groupBoxShipping.Location = new Point(3, 3);
            groupBoxShipping.Name = "groupBoxShipping";
            groupBoxShipping.Size = new Size(246, 120);
            groupBoxShipping.TabIndex = 0;
            groupBoxShipping.TabStop = false;
            groupBoxShipping.Text = "Shipping Type";
            // 
            // comboShipping
            // 
            comboShipping.DropDownStyle = ComboBoxStyle.DropDownList;
            comboShipping.FormattingEnabled = true;
            comboShipping.Items.AddRange(new object[] { "Standard", "Express", "Same Day" });
            comboShipping.Location = new Point(8, 40);
            comboShipping.Name = "comboShipping";
            comboShipping.Size = new Size(228, 28);
            comboShipping.TabIndex = 0;
            // 
            // dgvItems
            // 
            dgvItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvItems.Columns.AddRange(new DataGridViewColumn[] { colItem, colQuantity, colWeight, colUnitPrice, colLineTotal });
            dgvItems.Dock = DockStyle.Fill;
            dgvItems.Location = new Point(0, 0);
            dgvItems.Name = "dgvItems";
            dgvItems.RowHeadersWidth = 51;
            dgvItems.RowTemplate.Height = 25;
            dgvItems.Size = new Size(804, 424);
            dgvItems.TabIndex = 0;
            // 
            // colItem
            // 
            colItem.HeaderText = "Item Name";
            colItem.MinimumWidth = 6;
            colItem.Name = "colItem";
            colItem.Width = 180;
            // 
            // colQuantity
            // 
            colQuantity.HeaderText = "Quantity";
            colQuantity.MinimumWidth = 6;
            colQuantity.Name = "colQuantity";
            colQuantity.Width = 70;
            // 
            // colWeight
            // 
            colWeight.HeaderText = "Weight (kg)";
            colWeight.MinimumWidth = 6;
            colWeight.Name = "colWeight";
            colWeight.Width = 80;
            // 
            // colUnitPrice
            // 
            colUnitPrice.HeaderText = "Unit Price";
            colUnitPrice.MinimumWidth = 6;
            colUnitPrice.Name = "colUnitPrice";
            colUnitPrice.Width = 90;
            // 
            // colLineTotal
            // 
            colLineTotal.HeaderText = "Line Total";
            colLineTotal.MinimumWidth = 6;
            colLineTotal.Name = "colLineTotal";
            colLineTotal.ReadOnly = true;
            colLineTotal.Width = 110;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { tslTime, tslTotalQuantity, tslTotalWeight, tslTotalAmount });
            statusStrip1.Location = new Point(0, 424);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1196, 26);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // tslTime
            // 
            tslTime.Name = "tslTime";
            tslTime.Size = new Size(140, 20);
            tslTime.Text = "Time: --/--/---- --:--";
            // 
            // tslTotalQuantity
            // 
            tslTotalQuantity.Name = "tslTotalQuantity";
            tslTotalQuantity.Size = new Size(84, 20);
            tslTotalQuantity.Text = "Total Qty: 0";
            // 
            // tslTotalWeight
            // 
            tslTotalWeight.Name = "tslTotalWeight";
            tslTotalWeight.Size = new Size(119, 20);
            tslTotalWeight.Text = "Total Wt: 0.00 kg";
            // 
            // tslTotalAmount
            // 
            tslTotalAmount.Name = "tslTotalAmount";
            tslTotalAmount.Size = new Size(108, 20);
            tslTotalAmount.Text = "Total Amt: 0.00";
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1196, 450);
            Controls.Add(splitContainer1);
            Controls.Add(statusStrip1);
            Name = "Form1";
            Text = "Delivery Order Dashboard";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tabControlLeft.ResumeLayout(false);
            tabPageCustomer.ResumeLayout(false);
            groupBoxCustomer.ResumeLayout(false);
            groupBoxCustomer.PerformLayout();
            tabPageShipping.ResumeLayout(false);
            groupBoxShipping.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvItems).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControlLeft;
        private System.Windows.Forms.TabPage tabPageCustomer;
        private System.Windows.Forms.TabPage tabPageShipping;
        private System.Windows.Forms.GroupBox groupBoxCustomer;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxShipping;
        private System.Windows.Forms.ComboBox comboShipping;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnitPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLineTotal;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tslTime;
        private System.Windows.Forms.ToolStripStatusLabel tslTotalQuantity;
        private System.Windows.Forms.ToolStripStatusLabel tslTotalWeight;
        private System.Windows.Forms.ToolStripStatusLabel tslTotalAmount;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Timer timer1;
    }
}
