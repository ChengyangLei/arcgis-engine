namespace MyGis.form1
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
            this.Label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboLayer = new System.Windows.Forms.ComboBox();
            this.txtBufferDistance = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.texOutputPath = new System.Windows.Forms.TextBox();
            this.btnOutputLayer = new System.Windows.Forms.Button();
            this.btnBuffer = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(14, 69);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(67, 15);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "选择图层";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "缓冲半径";
            // 
            // cboLayer
            // 
            this.cboLayer.FormattingEnabled = true;
            this.cboLayer.Location = new System.Drawing.Point(188, 66);
            this.cboLayer.Name = "cboLayer";
            this.cboLayer.Size = new System.Drawing.Size(175, 23);
            this.cboLayer.TabIndex = 2;
            // 
            // txtBufferDistance
            // 
            this.txtBufferDistance.Location = new System.Drawing.Point(188, 139);
            this.txtBufferDistance.Name = "txtBufferDistance";
            this.txtBufferDistance.Size = new System.Drawing.Size(173, 25);
            this.txtBufferDistance.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 256);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "输出图层";
            // 
            // texOutputPath
            // 
            this.texOutputPath.Location = new System.Drawing.Point(188, 253);
            this.texOutputPath.Name = "texOutputPath";
            this.texOutputPath.ReadOnly = true;
            this.texOutputPath.Size = new System.Drawing.Size(175, 25);
            this.texOutputPath.TabIndex = 6;
            // 
            // btnOutputLayer
            // 
            this.btnOutputLayer.Location = new System.Drawing.Point(386, 247);
            this.btnOutputLayer.Name = "btnOutputLayer";
            this.btnOutputLayer.Size = new System.Drawing.Size(46, 33);
            this.btnOutputLayer.TabIndex = 7;
            this.btnOutputLayer.Text = "...";
            this.btnOutputLayer.UseVisualStyleBackColor = true;
            this.btnOutputLayer.Click += new System.EventHandler(this.btnOutputLayer_Click);
            // 
            // btnBuffer
            // 
            this.btnBuffer.Location = new System.Drawing.Point(100, 337);
            this.btnBuffer.Name = "btnBuffer";
            this.btnBuffer.Size = new System.Drawing.Size(84, 30);
            this.btnBuffer.TabIndex = 8;
            this.btnBuffer.Text = "分析";
            this.btnBuffer.UseVisualStyleBackColor = true;
            this.btnBuffer.Click += new System.EventHandler(this.btnBuffer_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(297, 338);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 29);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // BufferForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 412);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnBuffer);
            this.Controls.Add(this.btnOutputLayer);
            this.Controls.Add(this.texOutputPath);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtBufferDistance);
            this.Controls.Add(this.cboLayer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Label1);
            this.Name = "BufferForm";
            this.Text = "缓冲区分析";
            this.Load += new System.EventHandler(this.BufferForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboLayer;
        private System.Windows.Forms.TextBox txtBufferDistance;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox texOutputPath;
        private System.Windows.Forms.Button btnOutputLayer;
        private System.Windows.Forms.Button btnBuffer;
        private System.Windows.Forms.Button btnCancel;
    }
}