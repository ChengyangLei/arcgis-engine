using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.AnalysisTools;

namespace Buff
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //判断Mapcontrol中是否包含图层
            if (this.axMapControl1.LayerCount == 0)
                return;
            //获取mapcontrol最终的第一图层
            ILayer pLayer = this.axMapControl1.Map.get_Layer(0);
            //输出路径，可以自行制定
            string strOutputPath = @"D:\Buffer.shp";
            //缓冲半径
            double dbDistace = 1.0;
            //获取一个geoprocessor的实例，避免与命名空间Geoprocessing中的Geoprocessor发生引用错误
            ESRI.ArcGIS.Geoprocessor.Geoprocessor gp = new ESRI.ArcGIS.Geoprocessor.Geoprocessor();
            //OverwriteOutput为真时，输出图层会覆盖当前文件夹下的同名图层
            gp.OverwriteOutput = true;
           //创建应Buffer工具的实例
            ESRI.ArcGIS.AnalysisTools.Buffer buffer = new ESRI.ArcGIS.AnalysisTools.Buffer(pLayer, strOutputPath, dbDistace);
            
            //执行缓冲区分析
            IGeoProcessorResult result = null;
            result = gp.Execute(buffer,null)as IGeoProcessorResult;
            //判断缓冲区分析是否成功
            if (result.Status != esriJobStatus.esriJobSucceeded)

                MessageBox.Show("图层" + pLayer.Name + "缓冲区生成失败！");
            else {

                MessageBox.Show("缓冲区生成成功！");
            //将生成成功的图层加入到Mapcontrol中
                int index = strOutputPath.LastIndexOf("\\");
                this.axMapControl1.AddShapeFile(strOutputPath.Substring(0,index),strOutputPath.Substring(index));
            }
            
            
        }
    }
}
