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
using MyGis.form1;
namespace MyGis.form1
{


    public partial class Form2 : Form
    {
        private string currentFieldName;    //设置临时类变量来存储字段名称
        //private IFeatureLayer currentFeatureLayer;  //设置临时类变量来使用IFeatureLayer接口的当前图层对象
        //public int mQueryMode;
        //public int mLayerIndex;
        private AxMapControl mMapControl;
        //选中图层
        private IFeatureLayer mFeatureLayer;
        //private IMap currentMap;
        //private string currentFieldName;    //设置临时类变量来存储字段名称
        public Form2(AxMapControl mapControl)
        {
            InitializeComponent();
            this.mMapControl = mapControl;

        }
        //public IMap CurrentMap
        //{
        //    set
        //    {
        //        currentMap = value;
        //    }
        //}




        private void Form2_Load(object sender, EventArgs e)
        {//mapcontrol中没有图层时返回
            if (this.mMapControl.LayerCount <= 0)
                return;
            //获取MapControl中 的全部图层名称
            ILayer pLayer;
            string strLayerName;
            for (int i = 0; i < this.mMapControl.LayerCount; i++)
            {
                pLayer = this.mMapControl.get_Layer(i);
                strLayerName = pLayer.Name;
                this.cboLayer.Items.Add(strLayerName);
            }
            this.cboLayer.SelectedIndex = 0;

        }

        private void cboLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboField.Items.Clear();
            txtStateName.Text = "";
            mFeatureLayer = mMapControl.get_Layer(cboLayer.SelectedIndex) as IFeatureLayer;
            IFeatureClass pFeatureClass = mFeatureLayer.FeatureClass;
            string strFldName;
            for (int i = 0; i < pFeatureClass.Fields.FieldCount; i++)
            {
                strFldName = pFeatureClass.Fields.get_Field(i).Name;
                this.cboField.Items.Add(strFldName);
            }
            this.cboField.SelectedIndex = 0;
            
           

        }





        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                IEnvelope pEnv;
                pEnv = mMapControl.ActiveView.Extent;
                IPoint pPoint;
                pPoint = new PointClass();
                pPoint.X = pEnv.XMin + pEnv.Width / 2;
                pPoint.Y = pEnv.XMin + pEnv.Width / 2;
                IFeatureCursor pFeatureCursor;
                IQueryFilter pQueryFilter;
                IFeature pFeature;

                if (this.mMapControl.LayerCount <= 0)
                    return;
                mFeatureLayer = mMapControl.get_Layer(cboLayer.SelectedIndex) as IFeatureLayer;
                this.mMapControl.Map.ClearSelection();
                this.mMapControl.ActiveView.Refresh();
                pQueryFilter = new QueryFilterClass();
                //pQueryFilter.WhereClause=cboField.Text+"='"+txtStateName.Text+"'";
                //pQueryFilter.WhereClause = cboField.Text + "=" + "'" + txtStateName.Text + "'";
                // pQueryFilter.WhereClause = cboField.SelectedItem.ToString() + "=" + "'" + txtStateName.Text + "'";
                pQueryFilter.WhereClause = cboField.Text + "=" + txtStateName.Text;
                // pQueryFilter.WhereClause = "CONTINENT='" + txtStateName.Text+"'";
                pFeatureCursor = mFeatureLayer.Search(pQueryFilter, true);
                pFeature = pFeatureCursor.NextFeature();
                if (pFeature != null)
                {

                    this.mMapControl.Map.SelectFeature(mFeatureLayer, pFeature);
                    pFeature.Shape.Envelope.CenterAt(pPoint);
                    this.mMapControl.Extent = pFeature.Shape.Envelope;

                }
                else
                {

                    MessageBox.Show("没有找到相关要素！", "提示");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("要查找的信息与" + this.cboField.SelectedItem + "所包含的类型不匹配！！");
            }

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void cboField_SelectedIndexChanged(object sender, EventArgs e)
        {
            //首先将listBoxValues控件中的字段属性值清空
            listBoxValues.Items.Clear();
            txtStateName.Text = "";
            //将buttonGetUniqeValue按钮控件置为可用状态
            if (buttonGetUniqeValue.Enabled == false)
                buttonGetUniqeValue.Enabled = true;

            //设置整个窗体可用的字段名称
            string str = cboField.SelectedItem.ToString();
            
            currentFieldName = str;
        }

        private void buttonGetUniqeValue_Click(object sender, EventArgs e)
        {

            //try
            //{
                //使用FeatureClass对象的IDataset接口来获取dataset和workspace的信息
                IDataset dataset = (IDataset)mFeatureLayer.FeatureClass;
                //使用IQueryDef接口的对象来定义和查询属性信息。通过IWorkspace接口的CreateQueryDef()方法创建该对象。
                IQueryDef queryDef = ((IFeatureWorkspace)dataset.Workspace).CreateQueryDef();
                //设置所需查询的表格名称为dataset的名称
                queryDef.Tables = dataset.Name;
                
                ////设置查询的字段名称。可以联合使用SQL语言的关键字，如查询唯一值可以使用DISTINCT关键字。
                queryDef.SubFields = "DISTINCT ("+ currentFieldName +")";
                //执行查询并返回ICursor接口的对象来访问整个结果的集合
                ICursor cursor = queryDef.Evaluate();
                //使用IField接口获取当前所需要使用的字段的信息
                IFields fields = mFeatureLayer.FeatureClass.Fields;
                IField field = fields.get_Field(fields.FindField(currentFieldName));

                //对整个结果集合进行遍历，从而添加所有的唯一值
                //使用IRow接口来操作结果集合。首先定位到第一个查询结果。
                IRow row = cursor.NextRow();
                //如果查询结果非空，则一直进行添加操作
                while (row != null)
                {
                    //对String类型的字段，唯一值的前后添加'和'，以符合SQL语句的要求
                    //if (field.Type == esriFieldType.esriFieldTypeString)
                   // {
                        //listBoxValues.Items.Add("\'" + row.get_Value(0).ToString() + "\'");
                   // }
                   // else
                   // {
                        listBoxValues.Items.Add(row.get_Value(0).ToString());
                   // }
                    //继续执行下一个结果的添加
                    row = cursor.NextRow();
                }
            //}
            //catch (Exception ex)
            //{

            //}
        }

        private void listBoxValues_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtStateName.Text = listBoxValues.SelectedItem.ToString();
        }

 
    }

}

