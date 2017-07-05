using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace WindowsFormsApplication1
{
    public partial  class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private void txtStateName_KeyUp(object sender, KeyEventArgs e)
        {
            //判断鼠标键值，如果Enter键按下抬起后，进入查询
            if (e.KeyCode == Keys.Enter)
            {
                //定义图层，要素游标，查询过滤器，要素
                IFeatureLayer pFeatureLayer;
                IFeatureCursor pFeatureCursor;
                IQueryFilter pQueryFilter;
                IFeature pFeature;
                //获取图层
                pFeatureLayer = this.axMapControl1.Map.get_Layer(1) as IFeatureLayer;
                //如果图层不是states，程序退出
                if (pFeatureLayer.Name != "Continents")
                    return;
                //清除上次查询结果
                this.axMapControl1.Map.ClearSelection();
                //pQueryfilter的实例化
                pQueryFilter = new QueryFilterClass();
                //设置查询过滤条件
                pQueryFilter.WhereClause = "CONTINENT='" + txtStateName.Text + "'";
                //查询
                pFeatureCursor = pFeatureLayer.Search(pQueryFilter, true);
                //获取查询到的要素
                pFeature = pFeatureCursor.NextFeature();
                //判断是否获取到要素
                if (pFeature != null)
                {
                    //选择要素
                    this.axMapControl1.Map.SelectFeature(pFeatureLayer, pFeature);
                    //放大到要素
                    this.axMapControl1.Extent = pFeature.Shape.Envelope;
                }
                else
                {
                    //没有得到pFeature的提示
                    MessageBox.Show("没有找到名为" + txtStateName.Text + "的地方", "提示");
                }
            }
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
        }
        }
    }

