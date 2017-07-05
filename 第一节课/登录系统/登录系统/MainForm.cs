using System;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Animation;
using stdole;
using System.Collections.Generic;
using ESRI.ArcGIS.CatalogUI;
using System.Diagnostics;
using ESRI.ArcGIS.Output;
using System.ComponentModel;
using System.Data;
//using PrintPreview;
using ESRI.ArcGIS.Analyst3D;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using 登录系统.form;




namespace 登录系统
{
    public partial class MainForm : Form
    {
        IMapDocument mapDocument = null;
        private IPoint pMovePt = null;
        private string sMapUnits = "未知单位";
        private string mTool;
        public int mQueryMode;
        public int mLayerIndex;
        private string currentFieldName;    //设置临时类变量来存储字段名称
        private IFeatureLayer mFeatureLayer;
        public string strOutputPath;
        private IMap currentMap;
        public MainForm()
        {
            InitializeComponent();
        }
        public IMap CurrentMap
        {
            set
            {
                currentMap = value;
            }
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.axTOCControl1.SetBuddyControl(axMapControl1.Object);
            this.splitContainer1.IsSplitterFixed = true;
            

        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // 在地图中根据图层名称获得矢量图层。
        private IFeatureLayer GetFeatureLayerByName(IMap map, string layerName)
        {
            //对地图中的图层进行遍历
            for (int i = 0; i < map.LayerCount; i++)
            {
                //如果该图层为图层组类型，则分别对所包含的每个图层进行操作
                if (map.get_Layer(i) is GroupLayer)
                {
                    //使用ICompositeLayer接口进行遍历操作
                    ICompositeLayer compositeLayer = map.get_Layer(i) as ICompositeLayer;
                    for (int j = 0; j < compositeLayer.Count; j++)
                    {
                        //如果图层名称为所要查询的图层名称，则返回IFeatureLayer接口的矢量图层对象
                        if (compositeLayer.get_Layer(j).Name == layerName)
                            return (IFeatureLayer)compositeLayer.get_Layer(j);
                    }
                }
                //如果图层不是图层组类型，则直接进行判断
                else
                {
                    if (map.get_Layer(i).Name == layerName)
                        return (IFeatureLayer)map.get_Layer(i);
                }
            }
            return null;
        }
        // 将矢量图层中所有要素的几何体进行Union操作得到一个合并后的新几何体。
        private IGeometry GetFeatureLayerGeometryUnion(IFeatureLayer featureLayer)
        {
            //定义IGeometry接口的对象，存储每一步拓扑操作后得到的几何体
            IGeometry geometry = null;
            //使用ITopologicalOperator接口进行几何体的拓扑操作
            ITopologicalOperator topologicalOperator;
            //使用null作为查询过滤器得到图层中所有要素的游标
            IFeatureCursor featureCursor = featureLayer.Search(null, false);
            //获取IFeature接口的游标中的第一个元素
            IFeature feature = featureCursor.NextFeature();
            //当游标不为空时
            while (feature != null)
            {
                //如果几何体不为空
                if (geometry != null)
                {
                    //进行接口转换，使用当前几何体的ITopologicalOperator接口进行拓扑操作
                    topologicalOperator = geometry as ITopologicalOperator;
                    //执行拓扑合并操作，将当前要素的几何体与已有几何体进行Union，返回新的合并后的几何体
                    geometry = topologicalOperator.Union(feature.Shape);
                }
                else
                    geometry = feature.Shape;
                //移动游标到下一个要素
                feature = featureCursor.NextFeature();
            }
            //返回最新合并后的几何体
            return geometry;
        }
        // 根据已配置的查询条件来执行空间查询操作。
        private void SelectFeaturesBySpatial()
        {
            //定义和创建用于空间查询的ISpatialFilter接口的对象
            ISpatialFilter spatialFilter = new SpatialFilterClass();
            //默认设定用于查询的空间几何体为当前地图源图层中所有要素几何体的集合
            spatialFilter.Geometry = GetFeatureLayerGeometryUnion
                (GetFeatureLayerByName(axMapControl1.Map, comboBoxSourceLayer.SelectedItem.ToString()));
            //根据对空间选择方法的选择采用相应的空间选择方法
            switch (comboBoxMethods.SelectedIndex)
            {
                case 0:
                    spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;//与源图层要素相交
                    break;
                case 1:
                    spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelWithin;//在源图层要素范围内
                    break;
                case 2:
                    spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;//包含源图层要素
                    break;
                case 3:
                    spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelOverlaps;//与源图层重叠的部分
                    break;
                case 4:
                    spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelTouches;////接触源图层要素的边界
                    break;
                case 5:
                    spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelCrosses;//质心在源图层要素内
                    break;
                default:
                    spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;//与原图层要素的轮廓交叉
                    break;
            }
            //对所选择的目标图层进行遍历，并对每一个图层进行空间查询操作，查询结果将放在选择集中
            IFeatureLayer featureLayer;
            //对所有被选择的目标图层进行遍历
            for (int i = 0; i < checkedListBoxTargetLayers.CheckedItems.Count; i++)
            {
                //根据选择的目标图层名称获得对应的矢量图层
                featureLayer = GetFeatureLayerByName(axMapControl1.Map, (string)checkedListBoxTargetLayers.CheckedItems[i]);
                //进行接口转换，使用IFeatureSelection接口选择要素
                IFeatureSelection featureSelection = featureLayer as IFeatureSelection;
                //使用IFeatureSelection接口的SelectFeatures方法根据空间查询过滤器选择要素，将其放在新的选择集中
                featureSelection.SelectFeatures((IQueryFilter)spatialFilter, esriSelectionResultEnum.esriSelectionResultAdd, false);
            }

            //进行接口转换，使用IActiveView接口进行视图操作
            IActiveView activeView = axMapControl1.Map as IActiveView;
            //部分刷新操作，只刷新地理选择集的内容
            activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, activeView.Extent);
        }

        private void buttonOK_Click(object sender, System.EventArgs e)
        {

            try
            {
                SelectFeaturesBySpatial();

            }
            catch
            { }
        }

        private void buttonApply_Click(object sender, System.EventArgs e)
        {
            try
            {
                SelectFeaturesBySpatial();
            }
            catch
            { }
        }

        private void toolStripButton1_Click(object sender, System.EventArgs e)
        {
            BufferForm bufferForm = new BufferForm(this.axMapControl1.Object);
            if (bufferForm.ShowDialog() == DialogResult.OK)
            {
                //获取输出文件路径
                string strBufferPath = bufferForm.strOutputPath;
                //缓冲区图层载入到mapcontrol中
                int index = strBufferPath.LastIndexOf("\\");
                this.axMapControl1.AddShapeFile(strBufferPath.Substring(0, index), strBufferPath.Substring(index));
            }

        }

        private void toolStripButton2_Click(object sender, System.EventArgs e)
        {
            OverlayForm over = new OverlayForm(this.axMapControl1);
            over.Show();
        }

        private void tabPage4_Click(object sender, System.EventArgs e)
        {
            if (this.axMapControl1.LayerCount <= 0)
                return;
            //获取MapControl中 的全部图层名称
            ILayer pLayer;
            string strLayerName;
            for (int i = 0; i < this.axMapControl1.LayerCount; i++)
            {
                pLayer = this.axMapControl1.get_Layer(i);
                strLayerName = pLayer.Name;
                this.comboBox1.Items.Add(strLayerName);
            }
            this.comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            cboField.Items.Clear();
            txtStateName.Text = "";
            mFeatureLayer = axMapControl1.get_Layer(comboBox1.SelectedIndex) as IFeatureLayer;
            IFeatureClass pFeatureClass = mFeatureLayer.FeatureClass;
            string strFldName;
            for (int i = 0; i < pFeatureClass.Fields.FieldCount; i++)
            {
                strFldName = pFeatureClass.Fields.get_Field(i).Name;
                this.cboField.Items.Add(strFldName);
            }
            this.cboField.SelectedIndex = 0;
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            try
            {
                IEnvelope pEnv;
                pEnv = axMapControl1.ActiveView.Extent;
                IPoint pPoint;
                pPoint = new PointClass();
                pPoint.X = pEnv.XMin + pEnv.Width / 2;
                pPoint.Y = pEnv.XMin + pEnv.Width / 2;
                IFeatureCursor pFeatureCursor;
                IQueryFilter pQueryFilter;
                IFeature pFeature;

                if (this.axMapControl1.LayerCount <= 0)
                    return;
                mFeatureLayer = axMapControl1.get_Layer(comboBox1.SelectedIndex) as IFeatureLayer;
                this.axMapControl1.Map.ClearSelection();
                this.axMapControl1.ActiveView.Refresh();
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

                    this.axMapControl1.Map.SelectFeature(mFeatureLayer, pFeature);
                    pFeature.Shape.Envelope.CenterAt(pPoint);
                    this.axMapControl1.Extent = pFeature.Shape.Envelope;

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

        private void cboField_SelectedIndexChanged(object sender, System.EventArgs e)
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

        private void buttonGetUniqeValue_Click(object sender, System.EventArgs e)
        {

            //使用FeatureClass对象的IDataset接口来获取dataset和workspace的信息
            IDataset dataset = (IDataset)mFeatureLayer.FeatureClass;
            //使用IQueryDef接口的对象来定义和查询属性信息。通过IWorkspace接口的CreateQueryDef()方法创建该对象。
            IQueryDef queryDef = ((IFeatureWorkspace)dataset.Workspace).CreateQueryDef();
            //设置所需查询的表格名称为dataset的名称
            queryDef.Tables = dataset.Name;

            ////设置查询的字段名称。可以联合使用SQL语言的关键字，如查询唯一值可以使用DISTINCT关键字。
            queryDef.SubFields = "DISTINCT (" + currentFieldName + ")";
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

                listBoxValues.Items.Add(row.get_Value(0).ToString());

                //继续执行下一个结果的添加
                row = cursor.NextRow();
            }

        }

        private void listBoxValues_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtStateName.Text = listBoxValues.SelectedItem.ToString();
        }

        private void tabPage5_Click(object sender, System.EventArgs e)
        {

            this.mTool = "SpaceQuery";

            this.axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerCrosshair;


            if (this.axMapControl1.LayerCount <= 0)
                return;
            ILayer pLayer;
            string atrLayerName;
            for (int i = 0; i < this.axMapControl1.LayerCount; i++)
            {

                pLayer = this.axMapControl1.get_Layer(i);
                atrLayerName = pLayer.Name;
                this.cboLayer.Items.Add(atrLayerName);
            }
            //加载查询方式

            this.cboMode1.Items.Add("矩形查询");
            this.cboMode1.Items.Add("线查询");
            this.cboMode1.Items.Add("圆查询");
            this.cboMode1.Items.Add("点查询");
            this.cboLayer.SelectedIndex = 0;
            this.cboMode1.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.cboLayer.SelectedIndex = 3;
            this.cboMode1.SelectedIndex = 0;
            this.axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerHand;
            

        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            if (this.cboLayer.Items.Count <= 0)
            {
                MessageBox.Show("当前mapcontrol没有图层！", "提示");
                return;


            }
            this.mLayerIndex = this.cboLayer.SelectedIndex;
            this.mQueryMode = this.cboMode1.SelectedIndex;
        }

        private void axMapControl2_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            if (e.button == 1)
            {

                IPoint pPoint = new PointClass();
                pPoint.X = e.mapX;
                pPoint.Y = e.mapY;
                IEnvelope pEnvelop = axMapControl1.Extent as IEnvelope;
                pEnvelop.CenterAt(pPoint);
                //pPoint.PutCoords(e.mapX, e.mapY);
                //axMapControl1.CenterAt(pPoint);
                axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);

            }
            else if (e.button == 2)
            {
                IEnvelope pEnv = axMapControl2.TrackRectangle();
                axMapControl1.Extent = pEnv;
                axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);

            }
        }

        private void axMapControl2_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            if (e.button != 1)
                return;

            IPoint pPoint = new PointClass();
            pPoint.X = e.mapX;
            pPoint.Y = e.mapY;
            //pPoint.PutCoords(e.mapX, e.mapY);
            axMapControl1.CenterAt(pPoint);
            axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }

        private void axMapControl1_OnExtentUpdated(object sender, IMapControlEvents2_OnExtentUpdatedEvent e)
        {
            IEnvelope pEnvelope = (IEnvelope)e.newEnvelope;
            IGraphicsContainer pGraphicsContainer = axMapControl2.Map as IGraphicsContainer;
            IActiveView pActiveView = pGraphicsContainer as IActiveView;
            //在绘制前清楚axmapcontrol2中的任何图像元素
            pGraphicsContainer.DeleteAllElements();
            IRectangleElement prectangleElement = new RectangleElementClass();
            IElement pElement = prectangleElement as IElement;
            pElement.Geometry = pEnvelope;
            //设置鹰眼图中的红线框
            IRgbColor pColor = new RgbColorClass();
            pColor.Red = 200;
            pColor.Green = 0;
            pColor.Blue = 0;
            pColor.Transparency = 255;
            //产生一个线符号对象
            ILineSymbol pOutline = new SimpleLineSymbolClass();
            pOutline.Width = 2;
            pOutline.Color = pColor;
            //设置颜色属性
            pColor = new RgbColorClass();
            pColor.Red = 200;
            pColor.Green = 0;
            pColor.Blue = 0;
            pColor.Transparency = 0;
            //设置填充符号的属性
            IFillSymbol pFillSymbol = new SimpleFillSymbolClass();
            pFillSymbol.Color = pColor;
            pFillSymbol.Outline = pOutline;
            IFillShapeElement pFillShapeEle = pElement as IFillShapeElement;
            pFillShapeEle.Symbol = pFillSymbol;
            pGraphicsContainer.AddElement((IElement)pFillShapeEle, 0);
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        private void axMapControl1_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {
            IMap pMap;
            pMap = axMapControl1.Map;

            for (int i = pMap.LayerCount - 1; i >= 0; i--)
            {

                axMapControl2.Map.AddLayer(pMap.get_Layer(i));

            }
            axMapControl2.Extent = axMapControl2.FullExtent;
            axMapControl2.Refresh();
        }

        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            this.axMapControl1.Map.ClearSelection();

            IActiveView pActiveView = this.axMapControl1.ActiveView;

            IPoint pPoint = pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
            switch (mTool)
            {

                case "SpaceQuery":
                    IGeometry pGeometry = null;
                    if (this.mQueryMode == 0)
                    {
                        pGeometry = this.axMapControl1.TrackRectangle();
                    }
                    else if (this.mQueryMode == 1)
                    {
                        pGeometry = this.axMapControl1.TrackLine();
                    }
                    else if (this.mQueryMode == 3)
                    {
                        ITopologicalOperator pTopo;
                        IGeometry pBuffer;
                        pGeometry = pPoint;
                        pTopo = pGeometry as ITopologicalOperator;

                        pBuffer = pTopo.Buffer(0.1);
                        pGeometry = pBuffer.Envelope;
                    }
                    else if (this.mQueryMode == 2)
                    {
                        pGeometry = this.axMapControl1.TrackCircle();
                    }
                    else if (this.mQueryMode == 3)
                    {

                    }
                    IFeatureLayer pFeatureLayer = this.axMapControl1.get_Layer(this.mLayerIndex) as IFeatureLayer;
                    DataTable pDataTable = this.LoadQueryResult(this.axMapControl1, pFeatureLayer, pGeometry);
                    this.DataTable.DataSource = pDataTable.DefaultView;
                    this.DataTable.Refresh();
                    break;
                default:
                    break;
            }
        }
        private DataTable LoadQueryResult(AxMapControl mapControl, IFeatureLayer featureLayer, IGeometry geometry)
        {
            IFeatureClass pFeatureClass = featureLayer.FeatureClass;

            IFields pFields = pFeatureClass.Fields;
            DataTable pDataTable = new DataTable();
            for (int i = 0; i < pFields.FieldCount; i++)
            {
                string strFldName;
                strFldName = pFields.get_Field(i).AliasName;
                pDataTable.Columns.Add(strFldName);
            }

            ISpatialFilter pSpatialFilter = new SpatialFilterClass();
            pSpatialFilter.Geometry = geometry;

            switch (pFeatureClass.ShapeType)
            {
                case esriGeometryType.esriGeometryPoint:
                    pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                    break;
                case esriGeometryType.esriGeometryPolyline:
                    pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelCrosses;
                    break;
                case esriGeometryType.esriGeometryPolygon:
                    pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                    break;
            }

            pSpatialFilter.GeometryField = pFeatureClass.ShapeFieldName;
            IQueryFilter pQueryFilter;
            IFeatureCursor pFeatureCursor;
            IFeature pFeature;

            pQueryFilter = pSpatialFilter as IQueryFilter;
            pFeatureCursor = featureLayer.Search(pQueryFilter, true);
            pFeature = pFeatureCursor.NextFeature();
            while (pFeature != null)
            {
                string strFldValue = null;
                DataRow dr = pDataTable.NewRow();

                for (int i = 0; i < pFields.FieldCount; i++)
                {
                    string strFldName = pFields.get_Field(i).Name;
                    if (strFldName == "Shape")
                    {
                        strFldValue = Convert.ToString(pFeature.Shape.GeometryType);
                    }
                    else
                    {
                        strFldValue = Convert.ToString(pFeature.get_Value(i));
                        dr[i] = strFldValue;
                    }
                }
                pDataTable.Rows.Add(dr);

                mapControl.Map.SelectFeature((ILayer)featureLayer, pFeature);
                mapControl.ActiveView.Refresh();
                pFeature = pFeatureCursor.NextFeature();
            }
            return pDataTable;
        }

        private void axMapControl1_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            this.toolStripStatusLabel1.Text = "比例尺 1:" + ((long)this.axMapControl1.MapScale).ToString();
            // this.barCoorTxt.Text = "当前坐标 X=" + e.mapX.ToString() + "Y=" + e.mapY.ToString() + "" + this.axMapControl1.MapUnits;

            sMapUnits = GetMapUnit(axMapControl1.Map.MapUnits);
            barCoorTxt.Text = String.Format("当前坐标：X = {0:#.###} Y = {1:#.###} {2}", e.mapX, e.mapY, sMapUnits);
            pMovePt = (axMapControl1.Map as IActiveView).ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
        }
        private string GetMapUnit(esriUnits _esriMapUnit)
        {
            string sMapUnits = string.Empty;
            switch (_esriMapUnit)
            {
                case esriUnits.esriCentimeters:
                    sMapUnits = "厘米";
                    break;
                case esriUnits.esriDecimalDegrees:
                    sMapUnits = "十进制";
                    break;
                case esriUnits.esriDecimeters:
                    sMapUnits = "分米";
                    break;
                case esriUnits.esriFeet:
                    sMapUnits = "尺";
                    break;
                case esriUnits.esriInches:
                    sMapUnits = "英寸";
                    break;
                case esriUnits.esriKilometers:
                    sMapUnits = "千米";
                    break;
                case esriUnits.esriMeters:
                    sMapUnits = "米";
                    break;
                case esriUnits.esriMiles:
                    sMapUnits = "英里";
                    break;
                case esriUnits.esriMillimeters:
                    sMapUnits = "毫米";
                    break;
                case esriUnits.esriNauticalMiles:
                    sMapUnits = "海里";
                    break;
                case esriUnits.esriPoints:
                    sMapUnits = "点";
                    break;
                case esriUnits.esriUnitsLast:
                    sMapUnits = "UnitsLast";
                    break;
                case esriUnits.esriUnknownUnits:
                    sMapUnits = "未知单位";
                    break;
                case esriUnits.esriYards:
                    sMapUnits = "码";
                    break;
                default:
                    break;
            }
            return sMapUnits;
        }

        private void tabPage3_Click(object sender, System.EventArgs e)
        {
            try
            {
                //清空目标图层列表
                checkedListBoxTargetLayers.Items.Clear();

                string layerName;   //设置临时变量存储图层名称

                //对Map中的每个图层进行判断并添加图层名称
                for (int i = 0; i < axMapControl1.LayerCount; i++)
                {
                    //如果该图层为图层组类型，则分别对所包含的每个图层进行操作
                    if (axMapControl1.get_Layer(i) is GroupLayer)
                    {
                        //使用ICompositeLayer接口进行遍历操作
                        ICompositeLayer compositeLayer = axMapControl1.get_Layer(i) as ICompositeLayer;
                        for (int j = 0; j < compositeLayer.Count; j++)
                        {
                            //将图层的名称添加到checkedListBoxTargetLayers控件和comboBoxMethods控件中
                            layerName = compositeLayer.get_Layer(j).Name;
                            checkedListBoxTargetLayers.Items.Add(layerName);
                            comboBoxSourceLayer.Items.Add(layerName);
                        }
                    }
                    //如果图层不是图层组类型，则直接添加名称
                    else
                    {
                        layerName = axMapControl1.get_Layer(i).Name;
                        checkedListBoxTargetLayers.Items.Add(layerName);
                        comboBoxSourceLayer.Items.Add(layerName);
                    }
                }
                comboBoxMethods.Items.Add("与原图层相交");
                comboBoxMethods.Items.Add("在源图层要素范围内");
                comboBoxMethods.Items.Add("包含源图层要素");
                comboBoxMethods.Items.Add("与源图层重叠的部分");
                comboBoxMethods.Items.Add("接触源图层要素的边界");
                comboBoxMethods.Items.Add("质心在源图层要素内");
                //将comboBoxSourceLayer控件的默认选项设置为第一个图层的名称
                comboBoxSourceLayer.SelectedIndex = 0;
                //将comboBoxMethods控件的默认选项设置为第一种空间选择方法
                comboBoxMethods.SelectedIndex = 0;
            }
            catch { }

        }

        private void axMapControl1_ControlRemoved(object sender, ControlEventArgs e)
        {
            axMapControl2.Refresh();
        }

        private void axMapControl1_OnAfterScreenDraw(object sender, IMapControlEvents2_OnAfterScreenDrawEvent e)
        {
            IActiveView pActiveView = (IActiveView)axPageLayoutControl1.ActiveView.FocusMap;
            IDisplayTransformation displayTransformation = pActiveView.ScreenDisplay.DisplayTransformation;
            displayTransformation.VisibleBounds = axMapControl1.Extent;
            axPageLayoutControl1.ActiveView.Refresh();
            CopyToPageLayout();
        }
        private void CopyToPageLayout()
        {
            IObjectCopy pObjectCopy = new ObjectCopyClass();
            object copyFromMap = axMapControl1.Map;
            object copiedMap = pObjectCopy.Copy(copyFromMap);//复制地图到copiedMap
            object copyToMap = axPageLayoutControl1.ActiveView.FocusMap;
            pObjectCopy.Overwrite(copiedMap, ref copyToMap);//复制地图
            axPageLayoutControl1.ActiveView.Refresh();

        }

        private void aaToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            this.tabControl3.SelectTab(this.tabPage2);
        }

        private void 打开mxd文档ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            string strName = null;
            //定义OpenFileDialog，获取并打开地图文档
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "打开MXD";
            openFileDialog.Filter = "MXD文件(*.mxd)|*.mxd";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                strName = openFileDialog.FileName;
                if (strName != "")
                {

                    this.axMapControl1.LoadMxFile(strName);

                }


            }
            this.axMapControl1.Extent = this.axMapControl1.FullExtent;
        }

        private void 打开栅格数据ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog pOpenFileDialog = new OpenFileDialog();
            pOpenFileDialog.CheckFileExists = true;
            pOpenFileDialog.Title = "打开Raster文件";
            pOpenFileDialog.Filter = "栅格文件 (*.*)|*.bmp;*.tif;*.jpg;*.img|(*.bmp)|*.bmp|(*.tif)|*.tif|(*.jpg)|*.jpg|(*.img)|*.img";
            pOpenFileDialog.ShowDialog();

            string pRasterFileName = pOpenFileDialog.FileName;
            if (pRasterFileName == "")
            {
                return;
            }

            string pPath = System.IO.Path.GetDirectoryName(pRasterFileName);
            string pFileName = System.IO.Path.GetFileName(pRasterFileName);

            IWorkspaceFactory pWorkspaceFactory = new RasterWorkspaceFactory();
            IWorkspace pWorkspace = pWorkspaceFactory.OpenFromFile(pPath, 0);
            IRasterWorkspace pRasterWorkspace = pWorkspace as IRasterWorkspace;
            IRasterDataset pRasterDataset = pRasterWorkspace.OpenRasterDataset(pFileName);
            //影像金字塔判断与创建
            IRasterPyramid3 pRasPyrmid;
            pRasPyrmid = pRasterDataset as IRasterPyramid3;
            if (pRasPyrmid != null)
            {
                if (!(pRasPyrmid.Present))
                {
                    pRasPyrmid.Create(); //创建金字塔
                }
            }
            IRaster pRaster;
            pRaster = pRasterDataset.CreateDefaultRaster();
            IRasterLayer pRasterLayer;
            pRasterLayer = new RasterLayerClass();
            pRasterLayer.CreateFromRaster(pRaster);
            ILayer pLayer = pRasterLayer as ILayer;
            axMapControl1.AddLayer(pLayer, 0);
        }

        private void 打开shp数据ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "打开Shp文件";
            openFileDialog.Filter = "map documents(*shp)|*.shp";
            openFileDialog.ShowDialog();
            //获取文件路径

            string filePath = openFileDialog.FileName;
            if (filePath == "") return;
            int pIndex = filePath.LastIndexOf("\\");
            string PfilePath = filePath.Substring(0, pIndex);
            string PFileName = filePath.Substring(pIndex + 1);
            try
            {

                axMapControl1.AddShapeFile(PfilePath, PFileName);

            }
            catch (Exception ex)
            {

                MessageBox.Show("加载失败！！" + ex.ToString());

            }
        }

        private void 保存ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            string ss = mapDocument.DocumentFilename;
            if (mapDocument.get_IsReadOnly(ss) == true)
            {
                MessageBox.Show("地图文档是只读的无法保存");
            }
            string fileSavePath = @"e:\new.mxd";
            try
            {
                mapDocument.Save(mapDocument.UsesRelativePaths, true);
                MessageBox.Show("保存地图文档成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存地图文档失败！！！" + ex.ToString());
            }
        }

        private void 打开图层文件ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openfileDialog;
            openfileDialog = new OpenFileDialog();
            openfileDialog.Title = "打开图层文件";
            openfileDialog.Filter = "map documents(*.lyr)| * .lyr";
            openfileDialog.ShowDialog();
            string filePath = openfileDialog.FileName;
            try
            {
                axMapControl1.AddLayerFromFile(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加图层失败" + ex.ToString());
            }
        }

        private void 另存为ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            try
            {
                SaveFileDialog pSaveFileDialog = new SaveFileDialog();
                pSaveFileDialog.Title = "另存为";
                pSaveFileDialog.OverwritePrompt = true;
                pSaveFileDialog.Filter = "ArcMap文档（*.mxd）|*.mxd|ArcMap模板（*.mxt）|*.mxt";
                pSaveFileDialog.RestoreDirectory = true;
                if (pSaveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string sFilePath = pSaveFileDialog.FileName;

                    IMapDocument pMapDocument = new MapDocumentClass();
                    pMapDocument.New(sFilePath);
                    pMapDocument.ReplaceContents(axMapControl1.Map as IMxdContents);
                    pMapDocument.Save(true, true);
                    pMapDocument.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Dispose();
        }

        private void 影像叠加ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            ISceneGraph pSceneGraph = this.axSceneControl1.SceneGraph;
            IScene pScene = pSceneGraph.Scene;
            ILayer layer = pScene.get_Layer(0);
            ITinLayer tinLayer = layer as ITinLayer;
            layer = pScene.get_Layer(1);
            IRasterLayer rasterLayer = layer as IRasterLayer;
            ITinAdvanced tinAdvanced;
            ISurface surface;
            tinAdvanced = tinLayer.Dataset as ITinAdvanced;
            surface = tinAdvanced.Surface;

            ILayerExtensions layerExtensions = (ILayerExtensions)rasterLayer;
            I3DProperties i3dProperties = null;

            for (int i = 0; i < layerExtensions.ExtensionCount; i++)
            {
                if (layerExtensions.get_Extension(i) is I3DProperties)
                {
                    i3dProperties = (I3DProperties)layerExtensions.get_Extension(i);
                }
            }//get 3d properties from extension

            i3dProperties.BaseOption = esriBaseOption.esriBaseSurface;
            i3dProperties.BaseSurface = surface;
            i3dProperties.Apply3DProperties(rasterLayer);
            pSceneGraph.RefreshViewers();
        }

        private void axSceneControl1_OnMouseDown(object sender, ISceneControlEvents_OnMouseDownEvent e)
        {
            ICommand cmd = new ControlsSceneNavigateToolClass();
            cmd.OnCreate(axSceneControl1.Object);
            cmd.OnClick();
            axSceneControl1.CurrentTool = cmd as ITool;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
