using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.AnalysisTools;
using ESRI.ArcGIS.esriSystem;

namespace MyGis.form1
{
    public partial class BufferForm : Form
    {
        //接受mapcontrol中的数据
        private IHookHelper mHookHelper = new HookHelperClass();
        //缓冲区文件输出路径
        public string strOutputPath;
        //重写构造函数，添加参数hook，用于传入mapcontrol中的数据
        public BufferForm(object hook)
        {
            InitializeComponent();
            this.mHookHelper.Hook = hook;
            //skinEngine1.SkinFile = System.Environment.CurrentDirectory + "\\Skins\\XPBlue.ssk";  //选择皮肤文件
        }
        //添加自定义函数用于根据图层名称获取要素图层并返回
        private IFeatureLayer GetFatureLayer(string layerName) 
        {

            IFeatureLayer pfeatureLayer = null;
            //遍历图层，获取与名称匹配的图层
            for (int i = 0; i < this.mHookHelper.FocusMap.LayerCount; i++)
            {

                ILayer pLayer = this.mHookHelper.FocusMap.get_Layer(i);
                if (pLayer.Name == layerName) {

                    pfeatureLayer = pLayer as IFeatureLayer;
                
                }
            
            
            }
            if (pfeatureLayer != null)
                return pfeatureLayer;
            else
                return null;
        
        }

        private void BufferForm_Load(object sender, EventArgs e)
        {
            //传入数据为空时返回
            if (null == mHookHelper || null == mHookHelper.Hook || 0 == mHookHelper.FocusMap.LayerCount)
                return;
            //获取图层名称并加入cbolayer
            for (int i = 0; i < this.mHookHelper.FocusMap.LayerCount; i++) {

                ILayer pLayer = this.mHookHelper.FocusMap.get_Layer(i);
                cboLayer.Items.Add(pLayer.Name);
            
            }
            if (cboLayer.Items.Count > 0)
                cboLayer.SelectedIndex = 0;
            //设置生成文件的默认输出路径和名称
            string tempDir = @"D:\Temp\";

            texOutputPath.Text = System.IO.Path.Combine(tempDir,((string)cboLayer.SelectedItem+"_buffer.shp"));
            //设置默认地图单位
            //lblUnit.Text = Convert.ToString(mHookHelper.FocusMap.MapUnits);
        }

        private void btnOutputLayer_Click(object sender, EventArgs e)
        {
            //定义输出文件路径
            SaveFileDialog saveDlg = new SaveFileDialog();
            //检查路径是否存在
            saveDlg.CheckPathExists = true;
            saveDlg.Filter = "Shapefile(*.shp)|*.shp";
            //保存时覆盖同名文件
            saveDlg.OverwritePrompt = true;
            saveDlg.Title = "输出路径";
            //对话框关闭前还原当前目录
            saveDlg.RestoreDirectory = true;
            saveDlg.FileName = (string)cboLayer.SelectedItem + "_buffer.shp";
            //读取文件输出路径到txtOutputPath
            DialogResult dr = saveDlg.ShowDialog();
            if (dr == DialogResult.OK)
                texOutputPath.Text = saveDlg.FileName;
        }

        private void btnBuffer_Click(object sender, EventArgs e)
        {
            //缓冲距离
            double bufferDistance;
            //输入的缓冲距离转换为double
            double.TryParse(txtBufferDistance.Text.ToString(),out bufferDistance);
            //判断输出路径是否合法
            if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(texOutputPath.Text)) || ".shp" != System.IO.Path.GetExtension(texOutputPath.Text)) {
                MessageBox.Show("输出路径错误！！！");
                return;
            
            }
            if (mHookHelper.FocusMap.LayerCount == 0)
                return;
            //获取图层
            IFeatureLayer pFeatureLayer = GetFatureLayer((string)cboLayer.SelectedItem);
            if (null == pFeatureLayer) {
                MessageBox.Show("图层"+(string)cboLayer.SelectedItem+"不存在！\r\n");
                return;
            
            }
            //获取一个geoprocessor的实例
            Geoprocessor gp = new Geoprocessor();
            gp.OverwriteOutput = true;
            strOutputPath = texOutputPath.Text;
            //创建应Buffer工具的实例
            ESRI.ArcGIS.AnalysisTools.Buffer buffer = new ESRI.ArcGIS.AnalysisTools.Buffer(pFeatureLayer, strOutputPath,bufferDistance.ToString());
            //执行缓冲区分析
            IGeoProcessorResult result = null;
            result = (IGeoProcessorResult)gp.Execute(buffer, null);
            //判断缓冲区分析是否成功
            if (result.Status != esriJobStatus.esriJobSucceeded)

                MessageBox.Show("图层" + pFeatureLayer.Name + "缓冲区生成失败！");
            else
            {
                this.DialogResult = DialogResult.OK;
                MessageBox.Show("缓冲区生成成功！");
                
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
