using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Controls;
namespace MyGis.form1
{
    public partial class Form3 : Form
    {//获取主界面中的mapControl
        private AxMapControl mMapControl;
        private IFeatureLayer mFeatureLayer;
        private IMap currentMap;
        public int mQueryMode;
        public int mLayerIndex;
        public Form3(AxMapControl mapControl)
        {
            InitializeComponent();
            this.mMapControl = mapControl;


        }
        public IMap CurrentMap
        {
            set
            {
                currentMap = value;
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            if (this.mMapControl.LayerCount <= 0)
                return;
            ILayer pLayer;
            string atrLayerName;
            for (int i = 0; i < this.mMapControl.LayerCount; i++)
            {

                pLayer = this.mMapControl.get_Layer(i);
                atrLayerName = pLayer.Name;
                this.cboLayer.Items.Add(atrLayerName);
            }
                //加载查询方式
                this.cboMode.Items.Add("矩形查询");
                this.cboMode.Items.Add("线查询");
                this.cboMode.Items.Add("圆查询");
                this.cboMode.Items.Add("点查询");
                this.cboLayer.SelectedIndex = 0;
                this.cboMode.SelectedIndex = 0;
            }
        



        private void btnCancel_Click(object sender, EventArgs e)
        {
            
            this.cboLayer.SelectedIndex = 0;
            this.cboMode.SelectedIndex = 0;
            this.Dispose();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            if (this.cboLayer.Items.Count <= 0) {
                MessageBox.Show("当前mapcontrol没有图层！","提示");
                return;
            
            
            }
            this.mLayerIndex = this.cboLayer.SelectedIndex;
            this.mQueryMode = this.cboMode.SelectedIndex;
        }
    }
}
