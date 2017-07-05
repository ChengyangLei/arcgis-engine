using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
namespace lesson7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int flag = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            loadMapDocument();
        }
        //加载地图文档
        private void loadMapDocument()
        {
            System.Windows.Forms.OpenFileDialog openFileDialog;
            openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "打开地图文档";
            openFileDialog.Filter = "map documents(*.mxd)|*.mxd";
            openFileDialog.ShowDialog();
            string filePath = openFileDialog.FileName;
            if (axMapControl1.CheckMxFile(filePath))
            {
                axMapControl1.MousePointer = esriControlsMousePointer.esriPointerHourglass;
                axMapControl1.LoadMxFile(filePath, 0, Type.Missing);
                axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
            }
            else
            {
                MessageBox.Show(filePath + "不是有效的地图文档");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //点选
            flag = 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //圆选
            flag = 2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //矩形框选
            flag = 3;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //多边形选择
            flag = 4;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //清除选择
            IActiveView pActiveView = (IActiveView)( axMapControl1.Map );
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, axMapControl1.get_Layer(0), null);
            axMapControl1.Map.ClearSelection();
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, axMapControl1.get_Layer(0), null);
        }
        //名称查询
        private void button6_Click(object sender, EventArgs e)
        {
            string searchName = this.textBox1.Text.Trim();
            ILayer layer = axMapControl1.Map.get_Layer(0);
            IFeatureLayer featureLayer = layer as IFeatureLayer;
            IFeatureClass featureClass = featureLayer.FeatureClass;
            IQueryFilter queryFilter =new   QueryFilterClass ();
            IFeatureCursor featureCursor;
            IFeature feature = null;

            queryFilter.WhereClause ="continent ='" +searchName +"'";
            featureCursor = featureClass.Search(queryFilter, true);
            feature = featureCursor.NextFeature();
            if (feature !=null)
            {
                axMapControl1.Map.SelectFeature(axMapControl1.get_Layer(0), feature);
                axMapControl1.Refresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
            }
        }
        private void axMapControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent e)
        {
           
            axMapControl1.MousePointer =esriControlsMousePointer.esriPointerCrosshair;
            IGeometry geometry = null;            
            switch (flag)
            {
                case 1:
                    ESRI.ArcGIS.Geometry.Point point = new ESRI.ArcGIS.Geometry.PointClass();
                    point.X = e.mapX;
                    point.Y = e.mapY;
                    geometry = point as IGeometry;
                    break;
                case 2:
                    geometry = axMapControl1.TrackCircle();
                    break;
                case 3:
                    geometry = axMapControl1.TrackRectangle();
                    break;
                case 4:
                    geometry = axMapControl1.TrackPolygon();
                    break;             
            }           
            axMapControl1.Map.SelectByShape(geometry, null, false);
            axMapControl1.Refresh(esriViewDrawPhase.esriViewGeoSelection, null, null);            
        }

        
       
    }
}