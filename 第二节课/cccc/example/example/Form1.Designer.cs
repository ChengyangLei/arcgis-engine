namespace example
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开指定文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.另存为ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开地图文档ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开ShpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载CAD数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(697, 403);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(443, 88);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(55, 21);
            this.button1.TabIndex = 8;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(520, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开指定文件ToolStripMenuItem,
            this.保存ToolStripMenuItem,
            this.另存为ToolStripMenuItem,
            this.打开ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 打开指定文件ToolStripMenuItem
            // 
            this.打开指定文件ToolStripMenuItem.Name = "打开指定文件ToolStripMenuItem";
            this.打开指定文件ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.打开指定文件ToolStripMenuItem.Text = "打开指定文件";
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.保存ToolStripMenuItem.Text = "保存";
            // 
            // 另存为ToolStripMenuItem
            // 
            this.另存为ToolStripMenuItem.Name = "另存为ToolStripMenuItem";
            this.另存为ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.另存为ToolStripMenuItem.Text = "另存为";
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开地图文档ToolStripMenuItem,
            this.打开ShpToolStripMenuItem,
            this.加载CAD数据ToolStripMenuItem});
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.打开ToolStripMenuItem.Text = "打开";
            // 
            // 打开地图文档ToolStripMenuItem
            // 
            this.打开地图文档ToolStripMenuItem.Name = "打开地图文档ToolStripMenuItem";
            this.打开地图文档ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.打开地图文档ToolStripMenuItem.Text = "打开地图文档";
            // 
            // 打开ShpToolStripMenuItem
            // 
            this.打开ShpToolStripMenuItem.Name = "打开ShpToolStripMenuItem";
            this.打开ShpToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.打开ShpToolStripMenuItem.Text = "加载Shp数据";
            // 
            // 加载CAD数据ToolStripMenuItem
            // 
            this.加载CAD数据ToolStripMenuItem.Name = "加载CAD数据ToolStripMenuItem";
            this.加载CAD数据ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.加载CAD数据ToolStripMenuItem.Text = "加载CAD数据";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 355);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Text = "MyGis";
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开指定文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 另存为ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开地图文档ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开ShpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载CAD数据ToolStripMenuItem;
    }
}

