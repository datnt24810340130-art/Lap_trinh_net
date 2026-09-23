namespace bt4._3
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
        private System.Windows.Forms.TextBox txtDisplay;
        private System.Windows.Forms.Button btn0;
        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.Button btn2;
        private System.Windows.Forms.Button btn3;
        private System.Windows.Forms.Button btn4;
        private System.Windows.Forms.Button btn5;
        private System.Windows.Forms.Button btn6;
        private System.Windows.Forms.Button btn7;
        private System.Windows.Forms.Button btn8;
        private System.Windows.Forms.Button btn9;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSub;
        private System.Windows.Forms.Button btnMul;
        private System.Windows.Forms.Button btnDiv;
        private System.Windows.Forms.Button btnEq;
        private System.Windows.Forms.Button btnClear;

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.txtDisplay = new System.Windows.Forms.TextBox();
            this.btn0 = new System.Windows.Forms.Button();
            this.btn1 = new System.Windows.Forms.Button();
            this.btn2 = new System.Windows.Forms.Button();
            this.btn3 = new System.Windows.Forms.Button();
            this.btn4 = new System.Windows.Forms.Button();
            this.btn5 = new System.Windows.Forms.Button();
            this.btn6 = new System.Windows.Forms.Button();
            this.btn7 = new System.Windows.Forms.Button();
            this.btn8 = new System.Windows.Forms.Button();
            this.btn9 = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSub = new System.Windows.Forms.Button();
            this.btnMul = new System.Windows.Forms.Button();
            this.btnDiv = new System.Windows.Forms.Button();
            this.btnEq = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // txtDisplay
            // 
            this.txtDisplay.Location = new System.Drawing.Point(12, 12);
            this.txtDisplay.Name = "txtDisplay";
            this.txtDisplay.Size = new System.Drawing.Size(260, 23);
            this.txtDisplay.TabIndex = 0;
            this.txtDisplay.ReadOnly = true;
            // 
            // Number buttons (layout simple 3x4)
            // 
            int startX = 12;
            int startY = 50;
            int btnW = 60;
            int btnH = 40;
            int gap = 6;

            // row 1: 7 8 9
            this.btn7.Location = new System.Drawing.Point(startX + 0 * (btnW + gap), startY + 0 * (btnH + gap));
            this.btn7.Name = "btn7";
            this.btn7.Size = new System.Drawing.Size(btnW, btnH);
            this.btn7.TabIndex = 1;
            this.btn7.Text = "7";
            this.btn7.UseVisualStyleBackColor = true;
            this.btn7.Click += new System.EventHandler(this.NumberButton_Click);

            this.btn8.Location = new System.Drawing.Point(startX + 1 * (btnW + gap), startY + 0 * (btnH + gap));
            this.btn8.Name = "btn8";
            this.btn8.Size = new System.Drawing.Size(btnW, btnH);
            this.btn8.TabIndex = 2;
            this.btn8.Text = "8";
            this.btn8.UseVisualStyleBackColor = true;
            this.btn8.Click += new System.EventHandler(this.NumberButton_Click);

            this.btn9.Location = new System.Drawing.Point(startX + 2 * (btnW + gap), startY + 0 * (btnH + gap));
            this.btn9.Name = "btn9";
            this.btn9.Size = new System.Drawing.Size(btnW, btnH);
            this.btn9.TabIndex = 3;
            this.btn9.Text = "9";
            this.btn9.UseVisualStyleBackColor = true;
            this.btn9.Click += new System.EventHandler(this.NumberButton_Click);

            // row 2: 4 5 6
            this.btn4.Location = new System.Drawing.Point(startX + 0 * (btnW + gap), startY + 1 * (btnH + gap));
            this.btn4.Name = "btn4";
            this.btn4.Size = new System.Drawing.Size(btnW, btnH);
            this.btn4.TabIndex = 4;
            this.btn4.Text = "4";
            this.btn4.UseVisualStyleBackColor = true;
            this.btn4.Click += new System.EventHandler(this.NumberButton_Click);

            this.btn5.Location = new System.Drawing.Point(startX + 1 * (btnW + gap), startY + 1 * (btnH + gap));
            this.btn5.Name = "btn5";
            this.btn5.Size = new System.Drawing.Size(btnW, btnH);
            this.btn5.TabIndex = 5;
            this.btn5.Text = "5";
            this.btn5.UseVisualStyleBackColor = true;
            this.btn5.Click += new System.EventHandler(this.NumberButton_Click);

            this.btn6.Location = new System.Drawing.Point(startX + 2 * (btnW + gap), startY + 1 * (btnH + gap));
            this.btn6.Name = "btn6";
            this.btn6.Size = new System.Drawing.Size(btnW, btnH);
            this.btn6.TabIndex = 6;
            this.btn6.Text = "6";
            this.btn6.UseVisualStyleBackColor = true;
            this.btn6.Click += new System.EventHandler(this.NumberButton_Click);

            // row 3: 1 2 3
            this.btn1.Location = new System.Drawing.Point(startX + 0 * (btnW + gap), startY + 2 * (btnH + gap));
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(btnW, btnH);
            this.btn1.TabIndex = 7;
            this.btn1.Text = "1";
            this.btn1.UseVisualStyleBackColor = true;
            this.btn1.Click += new System.EventHandler(this.NumberButton_Click);

            this.btn2.Location = new System.Drawing.Point(startX + 1 * (btnW + gap), startY + 2 * (btnH + gap));
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(btnW, btnH);
            this.btn2.TabIndex = 8;
            this.btn2.Text = "2";
            this.btn2.UseVisualStyleBackColor = true;
            this.btn2.Click += new System.EventHandler(this.NumberButton_Click);

            this.btn3.Location = new System.Drawing.Point(startX + 2 * (btnW + gap), startY + 2 * (btnH + gap));
            this.btn3.Name = "btn3";
            this.btn3.Size = new System.Drawing.Size(btnW, btnH);
            this.btn3.TabIndex = 9;
            this.btn3.Text = "3";
            this.btn3.UseVisualStyleBackColor = true;
            this.btn3.Click += new System.EventHandler(this.NumberButton_Click);

            // row 4: 0 . =
            this.btn0.Location = new System.Drawing.Point(startX + 0 * (btnW + gap), startY + 3 * (btnH + gap));
            this.btn0.Name = "btn0";
            this.btn0.Size = new System.Drawing.Size(btnW * 2 + gap, btnH);
            this.btn0.TabIndex = 10;
            this.btn0.Text = "0";
            this.btn0.UseVisualStyleBackColor = true;
            this.btn0.Click += new System.EventHandler(this.NumberButton_Click);

            this.btnEq.Location = new System.Drawing.Point(startX + 2 * (btnW + gap), startY + 3 * (btnH + gap));
            this.btnEq.Name = "btnEq";
            this.btnEq.Size = new System.Drawing.Size(btnW, btnH);
            this.btnEq.TabIndex = 11;
            this.btnEq.Text = "=";
            this.btnEq.UseVisualStyleBackColor = true;

            // 
            // Operators: + - * /
            // 
            int opX = startX + 3 * (btnW + gap) + 10;
            this.btnAdd.Location = new System.Drawing.Point(opX, startY + 0 * (btnH + gap));
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(btnW, btnH);
            this.btnAdd.TabIndex = 12;
            this.btnAdd.Text = "+";
            this.btnAdd.UseVisualStyleBackColor = true;

            this.btnSub.Location = new System.Drawing.Point(opX, startY + 1 * (btnH + gap));
            this.btnSub.Name = "btnSub";
            this.btnSub.Size = new System.Drawing.Size(btnW, btnH);
            this.btnSub.TabIndex = 13;
            this.btnSub.Text = "-";
            this.btnSub.UseVisualStyleBackColor = true;

            this.btnMul.Location = new System.Drawing.Point(opX, startY + 2 * (btnH + gap));
            this.btnMul.Name = "btnMul";
            this.btnMul.Size = new System.Drawing.Size(btnW, btnH);
            this.btnMul.TabIndex = 14;
            this.btnMul.Text = "*";
            this.btnMul.UseVisualStyleBackColor = true;

            this.btnDiv.Location = new System.Drawing.Point(opX, startY + 3 * (btnH + gap));
            this.btnDiv.Name = "btnDiv";
            this.btnDiv.Size = new System.Drawing.Size(btnW, btnH);
            this.btnDiv.TabIndex = 15;
            this.btnDiv.Text = "/";
            this.btnDiv.UseVisualStyleBackColor = true;

            // 
            // Clear button
            // 
            this.btnClear.Location = new System.Drawing.Point(opX, startY + 4 * (btnH + gap));
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(btnW, btnH);
            this.btnClear.TabIndex = 16;
            this.btnClear.Text = "C";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 320);
            this.Controls.Add(this.txtDisplay);
            this.Controls.Add(this.btn7);
            this.Controls.Add(this.btn8);
            this.Controls.Add(this.btn9);
            this.Controls.Add(this.btn4);
            this.Controls.Add(this.btn5);
            this.Controls.Add(this.btn6);
            this.Controls.Add(this.btn1);
            this.Controls.Add(this.btn2);
            this.Controls.Add(this.btn3);
            this.Controls.Add(this.btn0);
            this.Controls.Add(this.btnEq);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnSub);
            this.Controls.Add(this.btnMul);
            this.Controls.Add(this.btnDiv);
            this.Controls.Add(this.btnClear);
            this.Name = "Form1";
            this.Text = "Calculator UI";
            ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
