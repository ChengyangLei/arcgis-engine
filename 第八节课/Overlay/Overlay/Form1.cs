using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.AnalysisTools;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Geoprocessor;

namespace Overlay
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnIntersect_Click(object sender, EventArgs e)
        {//添加两个以上图层时才允许叠置
            if (this.axMapControl1.LayerCount < 2)
                return;
            ESRI.ArcGIS.Geoprocessor.Geoprocessor gp = new ESRI.ArcGIS.Geoprocessor.Geoprocessor();
            //OverwriteOutput为真时，输出图层会覆盖当前文件夹下的同名图层
            gp.OverwriteOutput = true;
            //创建重置分析实例
            Intersect intersectTool = new Intersect();
            //获取mapcontrol中的前两个图层
            ILayer plnputLayer1 = this.axMapControl1.get_Layer(0);
            ILayer plnputLayer2 = this.axMapControl1.get_Layer(1);
            //转换为object类型
            object inputfeature1 = plnputLayer1;
            object inputfeature2 = plnputLayer2;
            //设置叠置分析的多个对象
            IGpValueTableObject pObject = new GpValueTableObjectClass();
            pObject.SetColumns(2);
            pObject.AddRow(ref inputfeature1);
            pObject.AddRow(ref inputfeature2);
            intersectTool.in_features = pObject;
            //设置输出路径
            string strTempPath = @"D:\Temp";
            string strOutputPath = strTempPath + plnputLayer1.Name + "_" + plnputLayer2.Name + "_Intersect.shp";
            intersectTool.out_feature_class = strOutputPath;
            //执行叠置分析
            IGeoProcessorResult result = null;
            result = gp.Execute(intersectTool,null)as IGeoProcessorResult;
            //判断叠置分析是否成功
            if (result.Status != ESRI.ArcGIS.esriSystem.esriJobStatus.esriJobSucceeded)
                MessageBox.Show("叠置求交失败！！");
            else {

                MessageBox.Show("叠置求交成功！！");
                int index = strOutputPath.LastIndexOf("\\");
                this.axMapControl1.AddShapeFile(strOutputPath.Substring(0,index),strOutputPath.Substring(index));
            
            }

        }
    }
}
