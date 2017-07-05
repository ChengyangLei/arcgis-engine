namespace MyGis.form1
{
    partial class Form2
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
            this.IbLayer = new System.Windows.Forms.Label();
            this.IbField = new System.Windows.Forms.Label();
            this.IblFind = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cboLayer = new System.Windows.Forms.ComboBox();
            this.cboField = new System.Windows.Forms.ComboBox();
            this.txtStateName = new System.Windows.Forms.TextBox();
            this.listBoxValues = new System.Windows.Forms.ListBox();
            this.buttonGetUniqeValue = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // IbLayer
            // 
            this.IbLayer.AutoSize = true;
            this.IbLayer.Location = new System.Drawing.Point(49, 58);
            this.IbLayer.Name = "IbLayer";
            this.IbLayer.Size = new System.Drawing.Size(67, 15);
            this.IbLayer.TabIndex = 0;
            this.IbLayer.Text = "选择图层";
            // 
            // IbField
            // 
            this.IbField.AutoSize = true;
            this.IbField.Location = new System.Drawing.Point(49, 113);
            this.IbField.Name = "IbField";
            this.IbField.Size = new System.Drawing.Size(67, 15);
            this.IbField.TabIndex = 1;
            this.IbField.Text = "字段名称";
            // 
            // IblFind
            // 
            this.IblFind.AutoSize = true;
            this.IblFind.Location = new System.Drawing.Point(49, 167);
            this.IblFind.Name = "IblFind";
            this.IblFind.Size = new System.Drawing.Size(67, 15);
            this.IblFind.TabIndex = 2;
            this.IblFind.Text = "查找内容";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(12, 217);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(122, 40);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "查找";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(12, 352);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(122, 40);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cboLayer
            // 
            this.cboLayer.FormattingEnabled = true;
            this.cboLayer.Location = new System.Drawing.Point(260, 50);
            this.cboLayer.Name = "cboLayer";
            this.cboLayer.Size = new System.Drawing.Size(136, 23);
            this.cboLayer.TabIndex = 5;
            this.cboLayer.SelectedIndexChanged += new System.EventHandler(this.cboLayer_SelectedIndexChanged);
            // 
            // cboField
            // 
            this.cboField.FormattingEnabled = true;
            this.cboField.Location = new System.Drawing.Point(257, 110);
            this.cboField.Name = "cboField";
            this.cboField.Size = new System.Drawing.Size(139, 23);
            this.cboField.TabIndex = 6;
            this.cboField.SelectedIndexChanged += new System.EventHandler(this.cboField_SelectedIndexChanged);
            // 
            // txtStateName
            // 
            this.txtStateName.Location = new System.Drawing.Point(256, 164);
            this.txtStateName.Name = "txtStateName";
            this.txtStateName.Size = new System.Drawing.Size(140, 25);
            this.txtStateName.TabIndex = 7;
            // 
            // listBoxValues
            // 
            this.listBoxValues.FormattingEnabled = true;
            this.listBoxValues.ItemHeight = 15;
            this.listBoxValues.Location = new System.Drawing.Point(209, 208);
            this.listBoxValues.Name = "listBoxValues";
            this.listBoxValues.Size = new System.Drawing.Size(255, 184);
            this.listBoxValues.TabIndex = 21;
            this.listBoxValues.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxValues_MouseDoubleClick);
            // 
            // buttonGetUniqeValue
            // 
            this.buttonGetUniqeValue.Location = new System.Drawing.Point(174, 244);
            this.buttonGetUniqeValue.Name = "buttonGetUniqeValue";
            this.buttonGetUniqeValue.Size = new System.Drawing.Size(29, 119);
            this.buttonGetUniqeValue.TabIndex = 22;
            this.buttonGetUniqeValue.Text = "获取属性值";
            this.buttonGetUniqeValue.UseVisualStyleBackColor = true;
            this.buttonGetUniqeValue.Click += new System.EventHandler(this.buttonGetUniqeValue_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 413);
            this.Controls.Add(this.buttonGetUniqeValue);
            this.Controls.Add(this.listBoxValues);
            this.Controls.Add(this.txtStateName);
            this.Controls.Add(this.cboField);
            this.Controls.Add(this.cboLayer);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.IblFind);
            this.Controls.Add(this.IbField);
            this.Controls.Add(this.IbLayer);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "属性查询";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label IbLayer;
        private System.Windows.Forms.Label IbField;
        private System.Windows.Forms.Label IblFind;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cboLayer;
        private System.Windows.Forms.ComboBox cboField;
        private System.Windows.Forms.TextBox txtStateName;
        private System.Windows.Forms.ListBox listBoxValues;
        private System.Windows.Forms.Button buttonGetUniqeValue;
    }
}