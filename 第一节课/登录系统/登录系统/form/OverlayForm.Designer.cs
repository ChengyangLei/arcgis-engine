namespace 登录系统.form
{
    partial class OverlayForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.texMessage = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnBuffer = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnOverlayFeat = new System.Windows.Forms.Button();
            this.btnInputFeat = new System.Windows.Forms.Button();
            this.tetOutputPath = new System.Windows.Forms.TextBox();
            this.cboOverLay = new System.Windows.Forms.ComboBox();
            this.texOverlayFeat = new System.Windows.Forms.TextBox();
            this.texInputFeat = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.texMessage);
            this.groupBox1.Location = new System.Drawing.Point(35, 347);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(454, 147);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "处理过程信息";
            // 
            // texMessage
            // 
            this.texMessage.AcceptsTab = true;
            this.texMessage.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.texMessage.Location = new System.Drawing.Point(15, 24);
            this.texMessage.Multiline = true;
            this.texMessage.Name = "texMessage";
            this.texMessage.ReadOnly = true;
            this.texMessage.Size = new System.Drawing.Size(429, 122);
            this.texMessage.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(330, 270);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(101, 33);
            this.btnCancel.TabIndex = 26;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnBuffer
            // 
            this.btnBuffer.Location = new System.Drawing.Point(74, 271);
            this.btnBuffer.Name = "btnBuffer";
            this.btnBuffer.Size = new System.Drawing.Size(108, 32);
            this.btnBuffer.TabIndex = 25;
            this.btnBuffer.Text = "分析";
            this.btnBuffer.UseVisualStyleBackColor = true;
            this.btnBuffer.Click += new System.EventHandler(this.btnBuffer_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(403, 208);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(87, 33);
            this.button3.TabIndex = 24;
            this.button3.Text = "...";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnOverlayFeat
            // 
            this.btnOverlayFeat.Location = new System.Drawing.Point(403, 88);
            this.btnOverlayFeat.Name = "btnOverlayFeat";
            this.btnOverlayFeat.Size = new System.Drawing.Size(88, 33);
            this.btnOverlayFeat.TabIndex = 23;
            this.btnOverlayFeat.Text = "...";
            this.btnOverlayFeat.UseVisualStyleBackColor = true;
            this.btnOverlayFeat.Click += new System.EventHandler(this.btnOverlayFeat_Click);
            // 
            // btnInputFeat
            // 
            this.btnInputFeat.Location = new System.Drawing.Point(403, 15);
            this.btnInputFeat.Name = "btnInputFeat";
            this.btnInputFeat.Size = new System.Drawing.Size(88, 32);
            this.btnInputFeat.TabIndex = 22;
            this.btnInputFeat.Text = "...";
            this.btnInputFeat.UseVisualStyleBackColor = true;
            this.btnInputFeat.Click += new System.EventHandler(this.btnInputFeat_Click);
            // 
            // tetOutputPath
            // 
            this.tetOutputPath.Location = new System.Drawing.Point(176, 214);
            this.tetOutputPath.Name = "tetOutputPath";
            this.tetOutputPath.ReadOnly = true;
            this.tetOutputPath.Size = new System.Drawing.Size(194, 25);
            this.tetOutputPath.TabIndex = 21;
            // 
            // cboOverLay
            // 
            this.cboOverLay.FormattingEnabled = true;
            this.cboOverLay.Location = new System.Drawing.Point(175, 158);
            this.cboOverLay.Name = "cboOverLay";
            this.cboOverLay.Size = new System.Drawing.Size(195, 23);
            this.cboOverLay.TabIndex = 20;
            // 
            // texOverlayFeat
            // 
            this.texOverlayFeat.Location = new System.Drawing.Point(175, 88);
            this.texOverlayFeat.Name = "texOverlayFeat";
            this.texOverlayFeat.ReadOnly = true;
            this.texOverlayFeat.Size = new System.Drawing.Size(195, 25);
            this.texOverlayFeat.TabIndex = 19;
            // 
            // texInputFeat
            // 
            this.texInputFeat.Location = new System.Drawing.Point(174, 21);
            this.texInputFeat.Name = "texInputFeat";
            this.texInputFeat.ReadOnly = true;
            this.texInputFeat.Size = new System.Drawing.Size(196, 25);
            this.texInputFeat.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 214);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 15);
            this.label4.TabIndex = 17;
            this.label4.Text = "输出图层：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 16;
            this.label3.Text = "叠置方法：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 15;
            this.label2.Text = "叠置要素：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 14;
            this.label1.Text = "输入要素：";
            // 
            // OverlayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 509);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnBuffer);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnOverlayFeat);
            this.Controls.Add(this.btnInputFeat);
            this.Controls.Add(this.tetOutputPath);
            this.Controls.Add(this.cboOverLay);
            this.Controls.Add(this.texOverlayFeat);
            this.Controls.Add(this.texInputFeat);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "OverlayForm";
            this.Text = "叠置分析";
            this.Load += new System.EventHandler(this.OverlayForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox texMessage;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnBuffer;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnOverlayFeat;
        private System.Windows.Forms.Button btnInputFeat;
        private System.Windows.Forms.TextBox tetOutputPath;
        private System.Windows.Forms.ComboBox cboOverLay;
        private System.Windows.Forms.TextBox texOverlayFeat;
        private System.Windows.Forms.TextBox texInputFeat;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}