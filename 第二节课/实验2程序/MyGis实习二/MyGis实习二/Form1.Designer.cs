namespace MyGis实习二
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载地图文档ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载SHP数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载CAD数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载特定地图文档ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载图层文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载栅格数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.另存为ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.地图浏览ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.地图放大ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.地图缩小ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.漫游ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清除图层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.移除图层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.量测ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.长度量测ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.面积量测ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.axMapControl2 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.地图浏览ToolStripMenuItem,
            this.清除图层ToolStripMenuItem,
            this.移除图层ToolStripMenuItem,
            this.量测ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(985, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开ToolStripMenuItem,
            this.保存ToolStripMenuItem,
            this.另存为ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.加载地图文档ToolStripMenuItem,
            this.加载SHP数据ToolStripMenuItem,
            this.加载CAD数据ToolStripMenuItem,
            this.加载特定地图文档ToolStripMenuItem,
            this.加载图层文件ToolStripMenuItem,
            this.加载栅格数据ToolStripMenuItem});
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(176, 24);
            this.打开ToolStripMenuItem.Text = "添加数据&New";
            // 
            // 加载地图文档ToolStripMenuItem
            // 
            this.加载地图文档ToolStripMenuItem.Name = "加载地图文档ToolStripMenuItem";
            this.加载地图文档ToolStripMenuItem.Size = new System.Drawing.Size(198, 24);
            this.加载地图文档ToolStripMenuItem.Text = "加载地图文档";
            this.加载地图文档ToolStripMenuItem.Click += new System.EventHandler(this.加载地图文档ToolStripMenuItem_Click);
            // 
            // 加载SHP数据ToolStripMenuItem
            // 
            this.加载SHP数据ToolStripMenuItem.Name = "加载SHP数据ToolStripMenuItem";
            this.加载SHP数据ToolStripMenuItem.Size = new System.Drawing.Size(198, 24);
            this.加载SHP数据ToolStripMenuItem.Text = "加载Shp数据";
            this.加载SHP数据ToolStripMenuItem.Click += new System.EventHandler(this.加载SHP数据ToolStripMenuItem_Click);
            // 
            // 加载CAD数据ToolStripMenuItem
            // 
            this.加载CAD数据ToolStripMenuItem.Name = "加载CAD数据ToolStripMenuItem";
            this.加载CAD数据ToolStripMenuItem.Size = new System.Drawing.Size(198, 24);
            this.加载CAD数据ToolStripMenuItem.Text = "加载CAD数据";
            this.加载CAD数据ToolStripMenuItem.Click += new System.EventHandler(this.加载CAD数据ToolStripMenuItem_Click);
            // 
            // 加载特定地图文档ToolStripMenuItem
            // 
            this.加载特定地图文档ToolStripMenuItem.Name = "加载特定地图文档ToolStripMenuItem";
            this.加载特定地图文档ToolStripMenuItem.Size = new System.Drawing.Size(198, 24);
            this.加载特定地图文档ToolStripMenuItem.Text = "加载特定地图文档";
            this.加载特定地图文档ToolStripMenuItem.Click += new System.EventHandler(this.加载特定地图文档ToolStripMenuItem_Click);
            // 
            // 加载图层文件ToolStripMenuItem
            // 
            this.加载图层文件ToolStripMenuItem.Name = "加载图层文件ToolStripMenuItem";
            this.加载图层文件ToolStripMenuItem.Size = new System.Drawing.Size(198, 24);
            this.加载图层文件ToolStripMenuItem.Text = "加载图层文件";
            this.加载图层文件ToolStripMenuItem.Click += new System.EventHandler(this.加载图层文件ToolStripMenuItem_Click);
            // 
            // 加载栅格数据ToolStripMenuItem
            // 
            this.加载栅格数据ToolStripMenuItem.Name = "加载栅格数据ToolStripMenuItem";
            this.加载栅格数据ToolStripMenuItem.Size = new System.Drawing.Size(198, 24);
            this.加载栅格数据ToolStripMenuItem.Text = "加载栅格数据";
            this.加载栅格数据ToolStripMenuItem.Click += new System.EventHandler(this.加载栅格数据ToolStripMenuItem_Click);
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(176, 24);
            this.保存ToolStripMenuItem.Text = "保存&Open";
            this.保存ToolStripMenuItem.Click += new System.EventHandler(this.保存ToolStripMenuItem_Click);
            // 
            // 另存为ToolStripMenuItem
            // 
            this.另存为ToolStripMenuItem.Name = "另存为ToolStripMenuItem";
            this.另存为ToolStripMenuItem.Size = new System.Drawing.Size(176, 24);
            this.另存为ToolStripMenuItem.Text = "另存为&Save as";
            this.另存为ToolStripMenuItem.Click += new System.EventHandler(this.另存为ToolStripMenuItem_Click);
            // 
            // 地图浏览ToolStripMenuItem
            // 
            this.地图浏览ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.地图放大ToolStripMenuItem,
            this.地图缩小ToolStripMenuItem,
            this.漫游ToolStripMenuItem,
            this.全图ToolStripMenuItem});
            this.地图浏览ToolStripMenuItem.Name = "地图浏览ToolStripMenuItem";
            this.地图浏览ToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.地图浏览ToolStripMenuItem.Text = "地图浏览";
            // 
            // 地图放大ToolStripMenuItem
            // 
            this.地图放大ToolStripMenuItem.Name = "地图放大ToolStripMenuItem";
            this.地图放大ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.地图放大ToolStripMenuItem.Text = "地图放大";
            this.地图放大ToolStripMenuItem.Click += new System.EventHandler(this.地图放大ToolStripMenuItem_Click);
            // 
            // 地图缩小ToolStripMenuItem
            // 
            this.地图缩小ToolStripMenuItem.Name = "地图缩小ToolStripMenuItem";
            this.地图缩小ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.地图缩小ToolStripMenuItem.Text = "地图缩小";
            this.地图缩小ToolStripMenuItem.Click += new System.EventHandler(this.地图缩小ToolStripMenuItem_Click);
            // 
            // 漫游ToolStripMenuItem
            // 
            this.漫游ToolStripMenuItem.Name = "漫游ToolStripMenuItem";
            this.漫游ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.漫游ToolStripMenuItem.Text = "漫游";
            this.漫游ToolStripMenuItem.Click += new System.EventHandler(this.漫游ToolStripMenuItem_Click);
            // 
            // 全图ToolStripMenuItem
            // 
            this.全图ToolStripMenuItem.Name = "全图ToolStripMenuItem";
            this.全图ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.全图ToolStripMenuItem.Text = "全图";
            this.全图ToolStripMenuItem.Click += new System.EventHandler(this.全图ToolStripMenuItem_Click);
            // 
            // 清除图层ToolStripMenuItem
            // 
            this.清除图层ToolStripMenuItem.Name = "清除图层ToolStripMenuItem";
            this.清除图层ToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.清除图层ToolStripMenuItem.Text = "清除图层";
            this.清除图层ToolStripMenuItem.Click += new System.EventHandler(this.清除图层ToolStripMenuItem_Click);
            // 
            // 移除图层ToolStripMenuItem
            // 
            this.移除图层ToolStripMenuItem.Name = "移除图层ToolStripMenuItem";
            this.移除图层ToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.移除图层ToolStripMenuItem.Text = "移除图层";
            this.移除图层ToolStripMenuItem.Click += new System.EventHandler(this.移除图层ToolStripMenuItem_Click);
            // 
            // 量测ToolStripMenuItem
            // 
            this.量测ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.长度量测ToolStripMenuItem,
            this.面积量测ToolStripMenuItem});
            this.量测ToolStripMenuItem.Name = "量测ToolStripMenuItem";
            this.量测ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.量测ToolStripMenuItem.Text = "量测";
            // 
            // 长度量测ToolStripMenuItem
            // 
            this.长度量测ToolStripMenuItem.Name = "长度量测ToolStripMenuItem";
            this.长度量测ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.长度量测ToolStripMenuItem.Text = "长度量测";
            this.长度量测ToolStripMenuItem.Click += new System.EventHandler(this.长度量测ToolStripMenuItem_Click);
            // 
            // 面积量测ToolStripMenuItem
            // 
            this.面积量测ToolStripMenuItem.Name = "面积量测ToolStripMenuItem";
            this.面积量测ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.面积量测ToolStripMenuItem.Text = "面积量测";
            this.面积量测ToolStripMenuItem.Click += new System.EventHandler(this.面积量测ToolStripMenuItem_Click);
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(551, 364);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Left;
            this.splitContainer1.Location = new System.Drawing.Point(0, 56);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.axTOCControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.axMapControl2);
            this.splitContainer1.Size = new System.Drawing.Size(263, 563);
            this.splitContainer1.SplitterDistance = 376;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // axTOCControl1
            // 
            this.axTOCControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axTOCControl1.Location = new System.Drawing.Point(0, 0);
            this.axTOCControl1.Name = "axTOCControl1";
            this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
            this.axTOCControl1.Size = new System.Drawing.Size(263, 376);
            this.axTOCControl1.TabIndex = 0;
            // 
            // axMapControl2
            // 
            this.axMapControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapControl2.Location = new System.Drawing.Point(0, 0);
            this.axMapControl2.Name = "axMapControl2";
            this.axMapControl2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl2.OcxState")));
            this.axMapControl2.Size = new System.Drawing.Size(263, 182);
            this.axMapControl2.TabIndex = 0;
            this.axMapControl2.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl2_OnMouseDown);
            this.axMapControl2.OnMouseUp += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseUpEventHandler(this.axMapControl2_OnMouseUp);
            this.axMapControl2.OnMouseMove += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseMoveEventHandler(this.axMapControl2_OnMouseMove);
            // 
            // axMapControl1
            // 
            this.axMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapControl1.Location = new System.Drawing.Point(263, 56);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(722, 563);
            this.axMapControl1.TabIndex = 8;
            this.axMapControl1.UseWaitCursor = true;
            this.axMapControl1.OnExtentUpdated += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnExtentUpdatedEventHandler(this.axMapControl1_OnExtentUpdated);
            this.axMapControl1.OnMapReplaced += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMapReplacedEventHandler(this.axMapControl1_OnMapReplaced);
            // 
            // axToolbarControl1
            // 
            this.axToolbarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.axToolbarControl1.Location = new System.Drawing.Point(0, 28);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(985, 28);
            this.axToolbarControl1.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 619);
            this.Controls.Add(this.axMapControl1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.axToolbarControl1);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载地图文档ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载SHP数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载CAD数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载特定地图文档ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 另存为ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清除图层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载图层文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 移除图层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载栅格数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 地图浏览ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 地图放大ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 地图缩小ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 漫游ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 量测ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 长度量测ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 面积量测ToolStripMenuItem;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl2;
    }
}

