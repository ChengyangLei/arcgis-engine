using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
namespace MyGis实习二
{
    public partial class Form1 : Form
    {
        private Form2 frmMeasureResult = null;   //量算结果窗体
        private INewLineFeedback pNewLineFeedback;           //追踪线对象
        private INewPolygonFeedback pNewPolygonFeedback;     //追踪面对象
        private IPoint pPointPt = null;                      //鼠标点击点
        private IPoint pMovePt = null;                       //鼠标移动时的当前点
        private double dToltalLength = 0;                    //量测总长度
        private double dSegmentLength = 0;                   //片段距离
        private IPointCollection pAreaPointCol = new MultipointClass();  //面积量算时画的点进行存储；
        private string sMapUnits = "未知单位";             //地图单位变量
        private object missing = Type.Missing;

        //鹰眼同步
        private bool bCanDrag;              //鹰眼地图上的矩形框可移动的标志
        private IPoint pMoveRectPoint;      //记录在移动鹰眼地图上的矩形框时鼠标的位置
        private IEnvelope pEnv;             //记录数据视图的Extent

        IMapDocument mapDocument;
        public Form1()
        {
            InitializeComponent();
        }

        private void 加载地图文档ToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void 加载特定地图文档ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadMapDocument2();
        }
        //加载地图文档中特定地图
        private void loadMapDocument2()
        {
            System.Windows.Forms.OpenFileDialog openFileDialog;
            openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "打开地图文档";
            openFileDialog.Filter = "map documents(*.mxd)|*.mxd";
            openFileDialog.ShowDialog();
            string filePath = openFileDialog.FileName;
            if (axMapControl1.CheckMxFile(filePath))
            {
                IArray arrayMap = axMapControl1.ReadMxMaps(filePath, Type.Missing);
                int i;
                IMap map;
                for (i = 0; i < arrayMap.Count; i++)
                {
                    map = arrayMap.get_Element(i) as IMap;
                    if (map.Name == "Layers")
                    {
                        axMapControl1.MousePointer = esriControlsMousePointer.esriPointerHourglass;
                        axMapControl1.LoadMxFile(filePath, 0, Type.Missing);
                        axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show(filePath + "不是有效的地图文档");
            }
        }

        private void 加载SHP数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addShpfile();
        }
        private void addShpfile()
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
            catch (Exception e)
            {

                MessageBox.Show("加载失败！！" + e.ToString());

            }

        }

        private void 清除图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deletelayer();
        }
        private void deletelayer()
        {
            try
            {

                for (int i = axMapControl1.LayerCount - 1; i >= 0; i--)
                {

                    axMapControl1.DeleteLayer(i);
                }

            }
            catch (Exception e)
            {


                MessageBox.Show("删除图层失败！！！" + e.ToString());

            }
        }

        private void 加载图层文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addLayerfile();
        }
        private void addLayerfile()
        {

            System.Windows.Forms.OpenFileDialog openfileDialog = new OpenFileDialog();
            openfileDialog.Title = "打开图层文件";
            openfileDialog.Filter = "map documents(*.lyr)|*.lyr";
            openfileDialog.ShowDialog();
            string filePath = openfileDialog.FileName;
            try
            {

                axMapControl1.AddLayerFromFile(filePath);


            }
            catch (Exception e)
            {

                MessageBox.Show("添加图层失败！！！！" + e.ToString());
            }

        }
        private void movelayer()
        {

            if (axMapControl1.LayerCount > 0)
            {


                try
                {
                    axMapControl1.MoveLayerTo(axMapControl1.LayerCount - 1, 0);

                }
                catch (Exception e)
                {


                    MessageBox.Show("移除图层失败！！！" + e.ToString());

                }


            }
        }

        private void 移除图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            movelayer();
        }

        private void 加载CAD数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IWorkspaceFactory pCadWorkspaceFactory;
            IWorkspace pWorkspace;
            ICadDrawingWorkspace pCadDrawingWorkspace;
            ICadDrawingDataset pCadDrawingDataset;
            ICadLayer pCadLayer;

            OpenFileDialog pOpenFileDialog = new OpenFileDialog();
            pOpenFileDialog.Filter = "CAD(*.dwg)|*.dwg";
            pOpenFileDialog.Title = "打开CAD数据文件";
            pOpenFileDialog.ShowDialog();

            string pFullPath = pOpenFileDialog.FileName;
            if (pFullPath == "")
            {
                return;
            }
            //获取文件名和文件路径
            int pIndex = pFullPath.LastIndexOf("\\");
            string pFilePath = pFullPath.Substring(0, pIndex);
            string pFileName = pFullPath.Substring(pIndex + 1);
            pCadWorkspaceFactory = new CadWorkspaceFactoryClass();

            pWorkspace = pCadWorkspaceFactory.OpenFromFile(pFilePath, 0);
            pCadDrawingWorkspace = (ICadDrawingWorkspace)pWorkspace;
            //获得CAD文件的数据集
            pCadDrawingDataset = pCadDrawingWorkspace.OpenCadDrawingDataset(pFileName);
            pCadLayer = new CadLayerClass();
            pCadLayer.CadDrawingDataset = pCadDrawingDataset;

            axMapControl1.Map.AddLayer(pCadLayer);

        }



        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveDocument();

        }





        //保存地图文档
        private void saveDocument()
        {
            if (mapDocument.get_IsReadOnly(mapDocument.DocumentFilename) == true)
            {
                MessageBox.Show("地图文档是只读的无法保存");
            }
            string fileSavePath = @"e:\new.mxd";
            try
            {
                mapDocument.Save(mapDocument.UsesRelativePaths, true);
                MessageBox.Show("保存地图文档成功");
            }
            catch (Exception e)
            {
                MessageBox.Show("保存地图文档失败！！！" + e.ToString());
            }
        }
        private void 加载栅格数据ToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
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
        string pMouseOperate = null;
        private void 地图放大ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMapControl1.CurrentTool = null;
            pMouseOperate = "ZoomIn";
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerZoomIn;
            //axMapControl1.MousePointer = esriControlsMousePointer.esriPointerZoomIn;
            //mapForm.MapOperator = MapOperation.ZOOMIN;
        }

        private void 地图缩小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMapControl1.CurrentTool = null;
            pMouseOperate = "ZoomOut";
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerZoomOut;
        }

        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {//屏幕坐标点转化为地图坐标点
            pPointPt = (axMapControl1.Map as IActiveView).ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);

            if (e.button == 1)
            {
                IActiveView pActiveView = axMapControl1.ActiveView;
                IEnvelope pEnvelope = new EnvelopeClass();

                switch (pMouseOperate)
                {
                    #region 拉框放大

                    case "ZoomIn":
                        pEnvelope = axMapControl1.TrackRectangle();
                        //如果拉框范围为空则返回
                        if (pEnvelope == null || pEnvelope.IsEmpty || pEnvelope.Height == 0 || pEnvelope.Width == 0)
                        {
                            return;
                        }
                        //如果有拉框范围，则放大到拉框范围
                        pActiveView.Extent = pEnvelope;
                        pActiveView.Refresh();//刷新地图显示
                        break;                    
                       #endregion; 
                    
                    case "Pan":
                        axMapControl1.Pan();
                        break;
                    case "MeasureLength":
                        //判断追踪线对象是否为空，若是则实例化并设置当前鼠标点为起始点
                        if (pNewLineFeedback == null)
                        {
                            //实例化追踪线对象
                            pNewLineFeedback = new NewLineFeedbackClass();
                            pNewLineFeedback.Display = (axMapControl1.Map as IActiveView).ScreenDisplay;
                            //设置起点，开始动态线绘制
                            pNewLineFeedback.Start(pPointPt);
                            dToltalLength = 0;
                        }
                        else //如果追踪线对象不为空，则添加当前鼠标点
                        {
                            pNewLineFeedback.AddPoint(pPointPt);
                        }
                        //pGeometry = m_PointPt;
                        if (dSegmentLength != 0)
                        {
                            dToltalLength = dToltalLength + dSegmentLength;
                        }
                        break;
                    case "MeasureArea":
                        if (pNewPolygonFeedback == null)
                        {
                            //实例化追踪面对象
                            pNewPolygonFeedback = new NewPolygonFeedback();
                            pNewPolygonFeedback.Display = (axMapControl1.Map as IActiveView).ScreenDisplay;
                            ;
                            pAreaPointCol.RemovePoints(0, pAreaPointCol.PointCount);
                            //开始绘制多边形
                            pNewPolygonFeedback.Start(pPointPt);
                            pAreaPointCol.AddPoint(pPointPt, ref missing, ref missing);
                        }
                        else
                        {
                            pNewPolygonFeedback.AddPoint(pPointPt);
                            pAreaPointCol.AddPoint(pPointPt, ref missing, ref missing);
                        }
                        break;    
                        
                }
            }
        }

        private void 漫游ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMapControl1.CurrentTool = null;
            pMouseOperate = "Pan";
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerPan;
        }

        private void 全图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMapControl1.Extent = axMapControl1.FullExtent;
        }

        private void 长度量测ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMapControl1.CurrentTool = null;
            pMouseOperate = "MeasureLength";
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
            
            
           
            if (frmMeasureResult == null || frmMeasureResult.IsDisposed)
            {
                frmMeasureResult = new Form2();
                frmMeasureResult.frmClosed += new Form2.FormClosedEventHandler(frmMeasureResult_frmColsed);
                frmMeasureResult.lblMeasureResult.Text = "";
                frmMeasureResult.Text = "距离量测";
                frmMeasureResult.Show();
            }
            else
            {
                frmMeasureResult.Activate();
            }
        }
        //测量结果窗口关闭响应事件
        private void frmMeasureResult_frmColsed()
        {
            //清空线对象
            if (pNewLineFeedback != null)
            {
                pNewLineFeedback.Stop();
                pNewLineFeedback = null;
            }
            //清空面对象
            if (pNewPolygonFeedback != null)
            {
                pNewPolygonFeedback.Stop();
                pNewPolygonFeedback = null;
                pAreaPointCol.RemovePoints(0, pAreaPointCol.PointCount); //清空点集中所有点
            }
            //清空量算画的线、面对象
            axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);
            //结束量算功能
            pMouseOperate = string.Empty;
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
        }
        //TOC右键菜单的添加及功能实现
        

        private void 面积量测ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMapControl1.CurrentTool = null;
            pMouseOperate = "MeasureArea";
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
            if (frmMeasureResult == null || frmMeasureResult.IsDisposed)
            {
                frmMeasureResult = new Form2();
                frmMeasureResult.frmClosed += new Form2.FormClosedEventHandler(frmMeasureResult_frmColsed);
                frmMeasureResult.lblMeasureResult.Text = "";
                frmMeasureResult.Text = "面积量测";
                frmMeasureResult.Show();
            }
            else
            {
                frmMeasureResult.Activate();
            }
        }

        private void AddAllDataset(IWorkspace pWorkspace, AxMapControl mapControl)
        {
            IEnumDataset pEnumDataset = pWorkspace.get_Datasets(ESRI.ArcGIS.Geodatabase.esriDatasetType.esriDTAny);
            pEnumDataset.Reset();
            //将Enum数据集中的数据一个个读到DataSet中
            IDataset pDataset = pEnumDataset.Next();
            //判断数据集是否有数据
            while (pDataset != null)
            {
                if (pDataset is IFeatureDataset)  //要素数据集
                {
                    IFeatureWorkspace pFeatureWorkspace = (IFeatureWorkspace)pWorkspace;
                    IFeatureDataset pFeatureDataset = pFeatureWorkspace.OpenFeatureDataset(pDataset.Name);
                    IEnumDataset pEnumDataset1 = pFeatureDataset.Subsets;
                    pEnumDataset1.Reset();
                    IGroupLayer pGroupLayer = new GroupLayerClass();
                    pGroupLayer.Name = pFeatureDataset.Name;
                    IDataset pDataset1 = pEnumDataset1.Next();
                    while (pDataset1 != null)
                    {
                        if (pDataset1 is IFeatureClass)  //要素类
                        {
                            IFeatureLayer pFeatureLayer = new FeatureLayerClass();
                            pFeatureLayer.FeatureClass = pFeatureWorkspace.OpenFeatureClass(pDataset1.Name);
                            if (pFeatureLayer.FeatureClass != null)
                            {
                                pFeatureLayer.Name = pFeatureLayer.FeatureClass.AliasName;
                                pGroupLayer.Add(pFeatureLayer);
                                mapControl.Map.AddLayer(pFeatureLayer);
                            }
                        }
                        pDataset1 = pEnumDataset1.Next();
                    }
                }
                else if (pDataset is IFeatureClass) //要素类
                {
                    IFeatureWorkspace pFeatureWorkspace = (IFeatureWorkspace)pWorkspace;
                    IFeatureLayer pFeatureLayer = new FeatureLayerClass();
                    pFeatureLayer.FeatureClass = pFeatureWorkspace.OpenFeatureClass(pDataset.Name);

                    pFeatureLayer.Name = pFeatureLayer.FeatureClass.AliasName;
                    mapControl.Map.AddLayer(pFeatureLayer);
                }
                else if (pDataset is IRasterDataset) //栅格数据集
                {
                    IRasterWorkspaceEx pRasterWorkspace = (IRasterWorkspaceEx)pWorkspace;
                    IRasterDataset pRasterDataset = pRasterWorkspace.OpenRasterDataset(pDataset.Name);
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
                    IRasterLayer pRasterLayer = new RasterLayerClass();
                    pRasterLayer.CreateFromDataset(pRasterDataset);
                    ILayer pLayer = pRasterLayer as ILayer;
                    mapControl.AddLayer(pLayer, 0);
                }
                pDataset = pEnumDataset.Next();
            }

            mapControl.ActiveView.Refresh();
            
            
        }

        private void axMapControl1_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            sMapUnits = GetMapUnit(axMapControl1.Map.MapUnits);
           // barCoorTxt.Text = String.Format("当前坐标：X = {0:#.###} Y = {1:#.###} {2}", e.mapX, e.mapY, sMapUnits);
            pMovePt = (axMapControl1.Map as IActiveView).ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);

            #region 长度量算
            if (pMouseOperate == "MeasureLength")
            {
                if (pNewLineFeedback != null)
                {
                    pNewLineFeedback.MoveTo(pMovePt);
                }
                double deltaX = 0; //两点之间X差值
                double deltaY = 0; //两点之间Y差值

                if ((pPointPt != null) && (pNewLineFeedback != null))
                {
                    deltaX = pMovePt.X - pPointPt.X;
                    deltaY = pMovePt.Y - pPointPt.Y;
                    dSegmentLength = Math.Round(Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY)), 3);
                    dToltalLength = dToltalLength + dSegmentLength;
                    if (frmMeasureResult != null)
                    {
                        frmMeasureResult.lblMeasureResult.Text = String.Format(
                            "当前线段长度：{0:.###}{1};\r\n总长度为: {2:.###}{1}",
                            dSegmentLength, sMapUnits, dToltalLength);
                        dToltalLength = dToltalLength - dSegmentLength; //鼠标移动到新点重新开始计算
                    }
                    frmMeasureResult.frmClosed += new Form2.FormClosedEventHandler(frmMeasureResult_frmColsed);
                }
            }
            #endregion

            #region 面积量算
            if (pMouseOperate == "MeasureArea")
            {
                if (pNewPolygonFeedback != null)
                {
                    pNewPolygonFeedback.MoveTo(pMovePt);
                }

                IPointCollection pPointCol = new Polygon();
                IPolygon pPolygon = new PolygonClass();
                IGeometry pGeo = null;

                ITopologicalOperator pTopo = null;
                for (int i = 0; i <= pAreaPointCol.PointCount - 1; i++)
                {
                    pPointCol.AddPoint(pAreaPointCol.get_Point(i), ref missing, ref missing);
                }
                pPointCol.AddPoint(pMovePt, ref missing, ref missing);

                if (pPointCol.PointCount < 3) return;
                pPolygon = pPointCol as IPolygon;

                if ((pPolygon != null))
                {
                    pPolygon.Close();
                    pGeo = pPolygon as IGeometry;
                    pTopo = pGeo as ITopologicalOperator;
                    //使几何图形的拓扑正确
                    pTopo.Simplify();
                    pGeo.Project(axMapControl1.Map.SpatialReference);
                    IArea pArea = pGeo as IArea;

                    frmMeasureResult.lblMeasureResult.Text = String.Format(
                        "总面积为：{0:.####}平方{1};\r\n总长度为：{2:.####}{1}",
                        pArea.Area, sMapUnits, pPolygon.Length);
                    pPolygon = null;
                }
            }
            #endregion
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

        private void axMapControl1_OnDoubleClick(object sender, IMapControlEvents2_OnDoubleClickEvent e)
        {
            #region 长度量算
            if (pMouseOperate == "MeasureLength")
            {
                if (frmMeasureResult != null)
                {
                    frmMeasureResult.lblMeasureResult.Text = "线段总长度为：" + dToltalLength + sMapUnits;
                }
                if (pNewLineFeedback != null)
                {
                    pNewLineFeedback.Stop();
                    pNewLineFeedback = null;
                    //清空所画的线对象
                    (axMapControl1.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);
                }
                dToltalLength = 0;
                dSegmentLength = 0;
            }
            #endregion

            #region 面积量算
            if (pMouseOperate == "MeasureArea")
            {
                if (pNewPolygonFeedback != null)
                {
                    pNewPolygonFeedback.Stop();
                    pNewPolygonFeedback = null;
                    //清空所画的线对象
                    (axMapControl1.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);
                }
                pAreaPointCol.RemovePoints(0, pAreaPointCol.PointCount); //清空点集中所有点
            }
            #endregion
        }

        private void axMapControl1_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {
            SynchronizeEagleEye();
        }
        private void SynchronizeEagleEye()
        {
            if (axMapControl2.LayerCount > 0)
            {
                axMapControl2.ClearLayers();
            }
            //设置鹰眼和主地图的坐标系统一致
            axMapControl2.SpatialReference = axMapControl1.SpatialReference;
            for (int i = axMapControl1.LayerCount - 1; i >= 0; i--)
            {
                //使鹰眼视图与数据视图的图层上下顺序保持一致
                ILayer pLayer = axMapControl1.get_Layer(i);
                if (pLayer is IGroupLayer || pLayer is ICompositeLayer)
                {
                    ICompositeLayer pCompositeLayer = (ICompositeLayer)pLayer;
                    for (int j = pCompositeLayer.Count - 1; j >= 0; j--)
                    {
                        ILayer pSubLayer = pCompositeLayer.get_Layer(j);
                        IFeatureLayer pFeatureLayer = pSubLayer as IFeatureLayer;
                        if (pFeatureLayer != null)
                        {
                            //由于鹰眼地图较小，所以过滤点图层不添加
                            if (pFeatureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPoint
                                && pFeatureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryMultipoint)
                            {
                                axMapControl2.AddLayer(pLayer);
                            }
                        }
                    }
                }
                else
                {
                    IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
                    if (pFeatureLayer != null)
                    {
                        if (pFeatureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPoint
                            && pFeatureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryMultipoint)
                        {
                            axMapControl2.AddLayer(pLayer);
                        }
                    }
                }
                //设置鹰眼地图全图显示  
                axMapControl2.Extent = axMapControl1.FullExtent;
                pEnv = axMapControl1.Extent as IEnvelope;
                DrawRectangle(pEnv);
                axMapControl2.ActiveView.Refresh();
            }
        }

        private void axMapControl2_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            if (axMapControl2.Map.LayerCount > 0)
            {
                //按下鼠标左键移动矩形框
                if (e.button == 1)
                {
                    //如果指针落在鹰眼的矩形框中，标记可移动
                    if (e.mapX > pEnv.XMin && e.mapY > pEnv.YMin && e.mapX < pEnv.XMax && e.mapY < pEnv.YMax)
                    {
                        bCanDrag = true;
                    }
                    pMoveRectPoint = new PointClass();
                    pMoveRectPoint.PutCoords(e.mapX, e.mapY);  //记录点击的第一个点的坐标
                }
                //按下鼠标右键绘制矩形框
                else if (e.button == 2)
                {
                    IEnvelope pEnvelope = axMapControl2.TrackRectangle();

                    IPoint pTempPoint = new PointClass();
                    pTempPoint.PutCoords(pEnvelope.XMin + pEnvelope.Width / 2, pEnvelope.YMin + pEnvelope.Height / 2);
                    axMapControl1.Extent = pEnvelope;
                    //矩形框的高宽和数据试图的高宽不一定成正比，这里做一个中心调整
                    axMapControl1.CenterAt(pTempPoint);
                }
            }
        }

        private void axMapControl2_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            if (e.mapX > pEnv.XMin && e.mapY > pEnv.YMin && e.mapX < pEnv.XMax && e.mapY < pEnv.YMax)
            {
                //如果鼠标移动到矩形框中，鼠标换成小手，表示可以拖动
                axMapControl2.MousePointer = esriControlsMousePointer.esriPointerHand;
                if (e.button == 2)  //如果在内部按下鼠标右键，将鼠标演示设置为默认样式
                {
                    axMapControl2.MousePointer = esriControlsMousePointer.esriPointerDefault;
                }
            }
            else
            {
                //在其他位置将鼠标设为默认的样式
                axMapControl2.MousePointer = esriControlsMousePointer.esriPointerDefault;
            }

            if (bCanDrag)
            {
                double Dx, Dy;  //记录鼠标移动的距离
                Dx = e.mapX - pMoveRectPoint.X;
                Dy = e.mapY - pMoveRectPoint.Y;
                pEnv.Offset(Dx, Dy); //根据偏移量更改 pEnv 位置
                pMoveRectPoint.PutCoords(e.mapX, e.mapY);
                DrawRectangle(pEnv);
                axMapControl1.Extent = pEnv;
            }
        }



        private void axMapControl2_OnMouseUp(object sender, IMapControlEvents2_OnMouseUpEvent e)
        {
            if (e.button == 1 && pMoveRectPoint != null)
            {
                if (e.mapX == pMoveRectPoint.X && e.mapY == pMoveRectPoint.Y)
                {
                    axMapControl1.CenterAt(pMoveRectPoint);
                }
                bCanDrag = false;
            }

        }

        private void axMapControl1_OnExtentUpdated(object sender, IMapControlEvents2_OnExtentUpdatedEvent e)
        {
            //得到当前视图范围
            pEnv = (IEnvelope)e.newEnvelope;
            DrawRectangle(pEnv);
        }
        //在鹰眼地图上面画矩形框
        private void DrawRectangle(IEnvelope pEnvelope)
        {
            //在绘制前，清除鹰眼中之前绘制的矩形框
            IGraphicsContainer pGraphicsContainer = axMapControl2.Map as IGraphicsContainer;
            IActiveView pActiveView = pGraphicsContainer as IActiveView;
            pGraphicsContainer.DeleteAllElements();
            //得到当前视图范围
            IRectangleElement pRectangleElement = new RectangleElementClass();
            IElement pElement = pRectangleElement as IElement;
            pElement.Geometry = pEnvelope;
            //设置矩形框（实质为中间透明度面）
            IRgbColor pColor = new RgbColorClass();
            pColor = GetRgbColor(255, 0, 0);
            pColor.Transparency = 255;
            ILineSymbol pOutLine = new SimpleLineSymbolClass();
            pOutLine.Width = 2;
            pOutLine.Color = pColor;

            IFillSymbol pFillSymbol = new SimpleFillSymbolClass();
            pColor = new RgbColorClass();
            pColor.Transparency = 0;
            pFillSymbol.Color = pColor;
            pFillSymbol.Outline = pOutLine;
            //向鹰眼中添加矩形框
            IFillShapeElement pFillShapeElement = pElement as IFillShapeElement;
            pFillShapeElement.Symbol = pFillSymbol;
            pGraphicsContainer.AddElement((IElement)pFillShapeElement, 0);
            //刷新
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }
        private IRgbColor GetRgbColor(int intR, int intG, int intB)
        {
            IRgbColor pRgbColor = null;
            if (intR < 0 || intR > 255 || intG < 0 || intG > 255 || intB < 0 || intB > 255)
            {
                return pRgbColor;
            }
            pRgbColor = new RgbColorClass();
            pRgbColor.Red = intR;
            pRgbColor.Green = intG;
            pRgbColor.Blue = intB;
            return pRgbColor;
        }


 






    }
}
                    