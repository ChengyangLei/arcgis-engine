namespace 登录系统
{
    partial class BufferForm
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnBuffer = new System.Windows.Forms.Button();
            this.btnOutputLayer = new System.Windows.Forms.Button();
            this.texOutputPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBufferDistance = new System.Windows.Forms.TextBox();
            this.cboLayer = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(292, 319);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 29);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnBuffer
            // 
            this.btnBuffer.Location = new System.Drawing.Point(95, 318);
            this.btnBuffer.Name = "btnBuffer";
            this.btnBuffer.Size = new System.Drawing.Size(84, 30);
            this.btnBuffer.TabIndex = 17;
            this.btnBuffer.Text = "分析";
            this.btnBuffer.UseVisualStyleBackColor = true;
            this.btnBuffer.Click += new System.EventHandler(this.btnBuffer_Click);
            // 
            // btnOutputLayer
            // 
            this.btnOutputLayer.Location = new System.Drawing.Point(381, 228);
            this.btnOutputLayer.Name = "btnOutputLayer";
            this.btnOutputLayer.Size = new System.Drawing.Size(46, 33);
            this.btnOutputLayer.TabIndex = 16;
            this.btnOutputLayer.Text = "...";
            this.btnOutputLayer.UseVisualStyleBackColor = true;
            this.btnOutputLayer.Click += new System.EventHandler(this.btnOutputLayer_Click);
            // 
            // texOutputPath
            // 
            this.texOutputPath.Location = new System.Drawing.Point(183, 234);
            this.texOutputPath.Name = "texOutputPath";
            this.texOutputPath.ReadOnly = true;
            this.texOutputPath.Size = new System.Drawing.Size(175, 25);
            this.texOutputPath.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 237);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 14;
            this.label4.Text = "输出图层";
            // 
            // txtBufferDistance
            // 
            this.txtBufferDistance.Location = new System.Drawing.Point(183, 120);
            this.txtBufferDistance.Name = "txtBufferDistance";
            this.txtBufferDistance.Size = new System.Drawing.Size(173, 25);
            this.txtBufferDistance.TabIndex = 13;
            // 
            // cboLayer
            // 
            this.cboLayer.FormattingEnabled = true;
            this.cboLayer.Location = new System.Drawing.Point(183, 47);
            this.cboLayer.Name = "cboLayer";
            this.cboLayer.Size = new System.Drawing.Size(175, 23);
            this.cboLayer.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "缓冲半径";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(9, 50);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(67, 15);
            this.Label1.TabIndex = 10;
            this.Label1.Text = "选择图层";
            // 
            // BufferForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 394);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnBuffer);
            this.Controls.Add(this.btnOutputLayer);
            this.Controls.Add(this.texOutputPath);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtBufferDistance);
            this.Controls.Add(this.cboLayer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Label1);
            this.MaximumSize = new System.Drawing.Size(454, 441);
            this.MinimumSize = new System.Drawing.Size(454, 441);
            this.Name = "BufferForm";
            this.Text = "缓冲区分析";
            this.Load += new System.EventHandler(this.BufferForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnBuffer;
        private System.Windows.Forms.Button btnOutputLayer;
        private System.Windows.Forms.TextBox texOutputPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBufferDistance;
        private System.Windows.Forms.ComboBox cboLayer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Label1;
    }
}