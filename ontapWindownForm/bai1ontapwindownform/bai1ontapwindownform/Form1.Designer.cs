namespace bai1ontapwindownform
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            txtUnitPrice = new TextBox();
            txtQuantity = new TextBox();
            txtDiscount = new TextBox();
            lblTotal = new Label();
            btnCalculate = new Button();
            btnReset = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(24, 24);
            label1.Name = "label1";
            label1.Size = new Size(116, 20);
            label1.TabIndex = 0;
            label1.Text = "Đơn giá dịch vụ:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(24, 64);
            label2.Name = "label2";
            label2.Size = new Size(114, 20);
            label2.TabIndex = 2;
            label2.Text = "Số lượng khách:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(24, 104);
            label3.Name = "label3";
            label3.Size = new Size(122, 20);
            label3.TabIndex = 4;
            label3.Text = "Mã giảm giá (%):";
            // 
            // txtUnitPrice
            // 
            txtUnitPrice.Location = new Point(150, 20);
            txtUnitPrice.Name = "txtUnitPrice";
            txtUnitPrice.Size = new Size(150, 27);
            txtUnitPrice.TabIndex = 1;
            // 
            // txtQuantity
            // 
            txtQuantity.Location = new Point(150, 60);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(150, 27);
            txtQuantity.TabIndex = 3;
            // 
            // txtDiscount
            // 
            txtDiscount.Location = new Point(150, 100);
            txtDiscount.Name = "txtDiscount";
            txtDiscount.Size = new Size(150, 27);
            txtDiscount.TabIndex = 5;
            // 
            // lblTotal
            // 
            lblTotal.BorderStyle = BorderStyle.FixedSingle;
            lblTotal.Location = new Point(24, 150);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(276, 30);
            lblTotal.TabIndex = 6;
            lblTotal.Text = "Tổng tiền: 0.00";
            lblTotal.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnCalculate
            // 
            btnCalculate.Location = new Point(24, 200);
            btnCalculate.Name = "btnCalculate";
            btnCalculate.Size = new Size(120, 30);
            btnCalculate.TabIndex = 7;
            btnCalculate.Text = "Tính tiền";
            btnCalculate.UseVisualStyleBackColor = true;
            btnCalculate.Click += btnCalculate_Click;
            // 
            // btnReset
            // 
            btnReset.Location = new Point(180, 200);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(120, 30);
            btnReset.TabIndex = 8;
            btnReset.Text = "Làm mới";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += btnReset_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(330, 260);
            Controls.Add(btnReset);
            Controls.Add(btnCalculate);
            Controls.Add(lblTotal);
            Controls.Add(txtDiscount);
            Controls.Add(label3);
            Controls.Add(txtQuantity);
            Controls.Add(label2);
            Controls.Add(txtUnitPrice);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Service Charge Calculator";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUnitPrice;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.TextBox txtDiscount;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Button btnReset;
    }
}
