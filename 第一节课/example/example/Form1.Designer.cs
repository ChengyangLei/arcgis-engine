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
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.另存为ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据加载ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载地图文档ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载Shapefile数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载栅格数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载CAD数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载个人地理数据库数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载文件地理数据库数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载ARCSDE空间数据库数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载文本文件数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.空间分析ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.缓冲区分析ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.叠置分析ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.裁剪分析ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.路径分析ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图层绘制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.绘制点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.点查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.线查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.属性查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.拉框查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.量测ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查询统计ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.地图制图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.空间数据编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.加载特定地图文档ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(647, 403);
            this.axLicenseControl1.Margin = new System.Windows.Forms.Padding(4);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 0;
            // 
            // axToolbarControl1
            // 
            this.axToolbarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.axToolbarControl1.Location = new System.Drawing.Point(0, 28);
            this.axToolbarControl1.Margin = new System.Windows.Forms.Padding(4);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(811, 28);
            this.axToolbarControl1.TabIndex = 3;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.数据加载ToolStripMenuItem,
            this.空间分析ToolStripMenuItem,
            this.图层绘制ToolStripMenuItem,
            this.查询ToolStripMenuItem,
            this.量测ToolStripMenuItem,
            this.查询统计ToolStripMenuItem,
            this.地图制图ToolStripMenuItem,
            this.空间数据编辑ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(811, 28);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.保存ToolStripMenuItem,
            this.另存为ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(123, 24);
            this.保存ToolStripMenuItem.Text = "保存";
            // 
            // 另存为ToolStripMenuItem
            // 
            this.另存为ToolStripMenuItem.Name = "另存为ToolStripMenuItem";
            this.另存为ToolStripMenuItem.Size = new System.Drawing.Size(123, 24);
            this.另存为ToolStripMenuItem.Text = "另存为";
            // 
            // 数据加载ToolStripMenuItem
            // 
            this.数据加载ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.加载地图文档ToolStripMenuItem,
            this.加载Shapefile数据ToolStripMenuItem,
            this.加载栅格数据ToolStripMenuItem,
            this.加载CAD数据ToolStripMenuItem,
            this.加载个人地理数据库数据ToolStripMenuItem,
            this.加载文件地理数据库数据ToolStripMenuItem,
            this.加载ARCSDE空间数据库数据ToolStripMenuItem,
            this.加载文本文件数据ToolStripMenuItem,
            this.加载特定地图文档ToolStripMenuItem});
            this.数据加载ToolStripMenuItem.Name = "数据加载ToolStripMenuItem";
            this.数据加载ToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.数据加载ToolStripMenuItem.Text = "数据加载";
            // 
            // 加载地图文档ToolStripMenuItem
            // 
            this.加载地图文档ToolStripMenuItem.Name = "加载地图文档ToolStripMenuItem";
            this.加载地图文档ToolStripMenuItem.Size = new System.Drawing.Size(272, 24);
            this.加载地图文档ToolStripMenuItem.Text = "加载地图文档";
            this.加载地图文档ToolStripMenuItem.Click += new System.EventHandler(this.加载地图文档ToolStripMenuItem_Click);
            // 
            // 加载Shapefile数据ToolStripMenuItem
            // 
            this.加载Shapefile数据ToolStripMenuItem.Name = "加载Shapefile数据ToolStripMenuItem";
            this.加载Shapefile数据ToolStripMenuItem.Size = new System.Drawing.Size(272, 24);
            this.加载Shapefile数据ToolStripMenuItem.Text = "加载Shapefile数据";
            // 
            // 加载栅格数据ToolStripMenuItem
            // 
            this.加载栅格数据ToolStripMenuItem.Name = "加载栅格数据ToolStripMenuItem";
            this.加载栅格数据ToolStripMenuItem.Size = new System.Drawing.Size(272, 24);
            this.加载栅格数据ToolStripMenuItem.Text = "加载栅格数据";
            // 
            // 加载CAD数据ToolStripMenuItem
            // 
            this.加载CAD数据ToolStripMenuItem.Name = "加载CAD数据ToolStripMenuItem";
            this.加载CAD数据ToolStripMenuItem.Size = new System.Drawing.Size(272, 24);
            this.加载CAD数据ToolStripMenuItem.Text = "加载CAD数据";
            // 
            // 加载个人地理数据库数据ToolStripMenuItem
            // 
            this.加载个人地理数据库数据ToolStripMenuItem.Name = "加载个人地理数据库数据ToolStripMenuItem";
            this.加载个人地理数据库数据ToolStripMenuItem.Size = new System.Drawing.Size(272, 24);
            this.加载个人地理数据库数据ToolStripMenuItem.Text = "加载个人地理数据库数据";
            // 
            // 加载文件地理数据库数据ToolStripMenuItem
            // 
            this.加载文件地理数据库数据ToolStripMenuItem.Name = "加载文件地理数据库数据ToolStripMenuItem";
            this.加载文件地理数据库数据ToolStripMenuItem.Size = new System.Drawing.Size(272, 24);
            this.加载文件地理数据库数据ToolStripMenuItem.Text = "加载文件地理数据库数据";
            // 
            // 加载ARCSDE空间数据库数据ToolStripMenuItem
            // 
            this.加载ARCSDE空间数据库数据ToolStripMenuItem.Name = "加载ARCSDE空间数据库数据ToolStripMenuItem";
            this.加载ARCSDE空间数据库数据ToolStripMenuItem.Size = new System.Drawing.Size(272, 24);
            this.加载ARCSDE空间数据库数据ToolStripMenuItem.Text = "加载ARCSDE空间数据库数据";
            // 
            // 加载文本文件数据ToolStripMenuItem
            // 
            this.加载文本文件数据ToolStripMenuItem.Name = "加载文本文件数据ToolStripMenuItem";
            this.加载文本文件数据ToolStripMenuItem.Size = new System.Drawing.Size(272, 24);
            this.加载文本文件数据ToolStripMenuItem.Text = "加载文本文件数据";
            // 
            // 空间分析ToolStripMenuItem
            // 
            this.空间分析ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.缓冲区分析ToolStripMenuItem,
            this.叠置分析ToolStripMenuItem,
            this.裁剪分析ToolStripMenuItem,
            this.路径分析ToolStripMenuItem});
            this.空间分析ToolStripMenuItem.Name = "空间分析ToolStripMenuItem";
            this.空间分析ToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.空间分析ToolStripMenuItem.Text = "空间分析";
            // 
            // 缓冲区分析ToolStripMenuItem
            // 
            this.缓冲区分析ToolStripMenuItem.Name = "缓冲区分析ToolStripMenuItem";
            this.缓冲区分析ToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.缓冲区分析ToolStripMenuItem.Text = "缓冲区分析";
            // 
            // 叠置分析ToolStripMenuItem
            // 
            this.叠置分析ToolStripMenuItem.Name = "叠置分析ToolStripMenuItem";
            this.叠置分析ToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.叠置分析ToolStripMenuItem.Text = "叠置分析";
            // 
            // 裁剪分析ToolStripMenuItem
            // 
            this.裁剪分析ToolStripMenuItem.Name = "裁剪分析ToolStripMenuItem";
            this.裁剪分析ToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.裁剪分析ToolStripMenuItem.Text = "裁剪分析";
            // 
            // 路径分析ToolStripMenuItem
            // 
            this.路径分析ToolStripMenuItem.Name = "路径分析ToolStripMenuItem";
            this.路径分析ToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.路径分析ToolStripMenuItem.Text = "路径分析";
            // 
            // 图层绘制ToolStripMenuItem
            // 
            this.图层绘制ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.绘制点ToolStripMenuItem});
            this.图层绘制ToolStripMenuItem.Name = "图层绘制ToolStripMenuItem";
            this.图层绘制ToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.图层绘制ToolStripMenuItem.Text = "图层绘制";
            // 
            // 绘制点ToolStripMenuItem
            // 
            this.绘制点ToolStripMenuItem.Name = "绘制点ToolStripMenuItem";
            this.绘制点ToolStripMenuItem.Size = new System.Drawing.Size(123, 24);
            this.绘制点ToolStripMenuItem.Text = "绘制点";
            // 
            // 查询ToolStripMenuItem
            // 
            this.查询ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.点查询ToolStripMenuItem,
            this.线查询ToolStripMenuItem,
            this.属性查询ToolStripMenuItem,
            this.拉框查询ToolStripMenuItem});
            this.查询ToolStripMenuItem.Name = "查询ToolStripMenuItem";
            this.查询ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.查询ToolStripMenuItem.Text = "查询";
            // 
            // 点查询ToolStripMenuItem
            // 
            this.点查询ToolStripMenuItem.Name = "点查询ToolStripMenuItem";
            this.点查询ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.点查询ToolStripMenuItem.Text = "点查询";
            // 
            // 线查询ToolStripMenuItem
            // 
            this.线查询ToolStripMenuItem.Name = "线查询ToolStripMenuItem";
            this.线查询ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.线查询ToolStripMenuItem.Text = "线查询";
            // 
            // 属性查询ToolStripMenuItem
            // 
            this.属性查询ToolStripMenuItem.Name = "属性查询ToolStripMenuItem";
            this.属性查询ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.属性查询ToolStripMenuItem.Text = "属性查询";
            // 
            // 拉框查询ToolStripMenuItem
            // 
            this.拉框查询ToolStripMenuItem.Name = "拉框查询ToolStripMenuItem";
            this.拉框查询ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.拉框查询ToolStripMenuItem.Text = "拉框查询";
            // 
            // 量测ToolStripMenuItem
            // 
            this.量测ToolStripMenuItem.Name = "量测ToolStripMenuItem";
            this.量测ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.量测ToolStripMenuItem.Text = "量测";
            // 
            // 查询统计ToolStripMenuItem
            // 
            this.查询统计ToolStripMenuItem.Name = "查询统计ToolStripMenuItem";
            this.查询统计ToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.查询统计ToolStripMenuItem.Text = "查询统计";
            // 
            // 地图制图ToolStripMenuItem
            // 
            this.地图制图ToolStripMenuItem.Name = "地图制图ToolStripMenuItem";
            this.地图制图ToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.地图制图ToolStripMenuItem.Text = "地图制图";
            // 
            // 空间数据编辑ToolStripMenuItem
            // 
            this.空间数据编辑ToolStripMenuItem.Name = "空间数据编辑ToolStripMenuItem";
            this.空间数据编辑ToolStripMenuItem.Size = new System.Drawing.Size(111, 24);
            this.空间数据编辑ToolStripMenuItem.Text = "空间数据编辑";
            // 
            // axTOCControl1
            // 
            this.axTOCControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.axTOCControl1.Location = new System.Drawing.Point(0, 56);
            this.axTOCControl1.Name = "axTOCControl1";
            this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
            this.axTOCControl1.Size = new System.Drawing.Size(231, 548);
            this.axTOCControl1.TabIndex = 5;
            // 
            // axMapControl1
            // 
            this.axMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapControl1.Location = new System.Drawing.Point(231, 56);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(580, 548);
            this.axMapControl1.TabIndex = 6;
            // 
            // 加载特定地图文档ToolStripMenuItem
            // 
            this.加载特定地图文档ToolStripMenuItem.Name = "加载特定地图文档ToolStripMenuItem";
            this.加载特定地图文档ToolStripMenuItem.Size = new System.Drawing.Size(272, 24);
            this.加载特定地图文档ToolStripMenuItem.Text = "加载特定地图文档";
            this.加载特定地图文档ToolStripMenuItem.Click += new System.EventHandler(this.加载特定地图文档ToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 604);
            this.Controls.Add(this.axMapControl1);
            this.Controls.Add(this.axTOCControl1);
            this.Controls.Add(this.axToolbarControl1);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 另存为ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 空间分析ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 缓冲区分析ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 叠置分析ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 裁剪分析ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 路径分析ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 图层绘制ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 绘制点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 点查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 线查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 属性查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 拉框查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 数据加载ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载地图文档ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载Shapefile数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载栅格数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载CAD数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载个人地理数据库数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载文件地理数据库数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载ARCSDE空间数据库数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载文本文件数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 量测ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查询统计ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 地图制图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 空间数据编辑ToolStripMenuItem;
        private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private System.Windows.Forms.ToolStripMenuItem 加载特定地图文档ToolStripMenuItem;
    }
}

