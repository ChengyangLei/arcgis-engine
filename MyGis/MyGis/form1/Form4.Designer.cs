namespace MyGis.form1
{
    partial class FormAtrribute
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
            this.dataGridAttribute = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAttribute)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridAttribute
            // 
            this.dataGridAttribute.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridAttribute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridAttribute.Location = new System.Drawing.Point(0, 0);
            this.dataGridAttribute.Name = "dataGridAttribute";
            this.dataGridAttribute.RowTemplate.Height = 27;
            this.dataGridAttribute.Size = new System.Drawing.Size(1099, 604);
            this.dataGridAttribute.TabIndex = 0;
            // 
            // FormAtrribute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1099, 604);
            this.Controls.Add(this.dataGridAttribute);
            this.Name = "FormAtrribute";
            this.Text = "属性表";
            this.Load += new System.EventHandler(this.FormAtrribute_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAttribute)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridAttribute;
    }
}