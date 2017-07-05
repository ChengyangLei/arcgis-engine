namespace MyGis.form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.texInputFeat = new System.Windows.Forms.TextBox();
            this.texOverlayFeat = new System.Windows.Forms.TextBox();
            this.cboOverLay = new System.Windows.Forms.ComboBox();
            this.tetOutputPath = new System.Windows.Forms.TextBox();
            this.btnInputFeat = new System.Windows.Forms.Button();
            this.btnOverlayFeat = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnBuffer = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.texMessage = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "输入要素：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "叠置要素：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 190);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "叠置方法：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 238);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "输出图层：";
            // 
            // texInputFeat
            // 
            this.texInputFeat.Location = new System.Drawing.Point(160, 45);
            this.texInputFeat.Name = "texInputFeat";
            this.texInputFeat.ReadOnly = true;
            this.texInputFeat.Size = new System.Drawing.Size(196, 25);
            this.texInputFeat.TabIndex = 4;
            // 
            // texOverlayFeat
            // 
            this.texOverlayFeat.Location = new System.Drawing.Point(161, 112);
            this.texOverlayFeat.Name = "texOverlayFeat";
            this.texOverlayFeat.ReadOnly = true;
            this.texOverlayFeat.Size = new System.Drawing.Size(195, 25);
            this.texOverlayFeat.TabIndex = 5;
            // 
            // cboOverLay
            // 
            this.cboOverLay.FormattingEnabled = true;
            this.cboOverLay.Location = new System.Drawing.Point(161, 182);
            this.cboOverLay.Name = "cboOverLay";
            this.cboOverLay.Size = new System.Drawing.Size(195, 23);
            this.cboOverLay.TabIndex = 6;
            // 
            // tetOutputPath
            // 
            this.tetOutputPath.Location = new System.Drawing.Point(162, 238);
            this.tetOutputPath.Name = "tetOutputPath";
            this.tetOutputPath.ReadOnly = true;
            this.tetOutputPath.Size = new System.Drawing.Size(194, 25);
            this.tetOutputPath.TabIndex = 7;
            // 
            // btnInputFeat
            // 
            this.btnInputFeat.Location = new System.Drawing.Point(389, 39);
            this.btnInputFeat.Name = "btnInputFeat";
            this.btnInputFeat.Size = new System.Drawing.Size(88, 32);
            this.btnInputFeat.TabIndex = 8;
            this.btnInputFeat.Text = "...";
            this.btnInputFeat.UseVisualStyleBackColor = true;
            this.btnInputFeat.Click += new System.EventHandler(this.btnInputFeat_Click);
            // 
            // btnOverlayFeat
            // 
            this.btnOverlayFeat.Location = new System.Drawing.Point(389, 112);
            this.btnOverlayFeat.Name = "btnOverlayFeat";
            this.btnOverlayFeat.Size = new System.Drawing.Size(88, 33);
            this.btnOverlayFeat.TabIndex = 9;
            this.btnOverlayFeat.Text = "...";
            this.btnOverlayFeat.UseVisualStyleBackColor = true;
            this.btnOverlayFeat.Click += new System.EventHandler(this.btnOverlayFeat_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(389, 232);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(87, 33);
            this.button3.TabIndex = 10;
            this.button3.Text = "...";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnBuffer
            // 
            this.btnBuffer.Location = new System.Drawing.Point(60, 295);
            this.btnBuffer.Name = "btnBuffer";
            this.btnBuffer.Size = new System.Drawing.Size(108, 32);
            this.btnBuffer.TabIndex = 11;
            this.btnBuffer.Text = "分析";
            this.btnBuffer.UseVisualStyleBackColor = true;
            this.btnBuffer.Click += new System.EventHandler(this.btnBuffer_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(316, 294);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(101, 33);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.texMessage);
            this.groupBox1.Location = new System.Drawing.Point(21, 371);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(454, 147);
            this.groupBox1.TabIndex = 13;
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
            // OverlayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 531);
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
            this.ForeColor = System.Drawing.SystemColors.Highlight;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OverlayForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "叠置分析";
            this.Load += new System.EventHandler(this.OverlayForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox texInputFeat;
        private System.Windows.Forms.TextBox texOverlayFeat;
        private System.Windows.Forms.ComboBox cboOverLay;
        private System.Windows.Forms.TextBox tetOutputPath;
        private System.Windows.Forms.Button btnInputFeat;
        private System.Windows.Forms.Button btnOverlayFeat;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnBuffer;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox texMessage;
    }
}