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

using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ESRI.ArcGIS.Framework;

using MyGis.form1;
namespace MyGis
{
    public partial class Form1 : Form
    {        //TOC菜单
        IFeatureLayer pTocFeatureLayer = null;            //点击的要素图层
        private FormAtrribute frmAttribute = null;        //图层属性窗体        
        private ILayer pMoveLayer;                        //需要调整显示顺序的图层
        private int toIndex;                              //存放拖动图层移动到的索引号     
        private string sMapUnits = "未知单位";             //地图单位变量
        private object missing = Type.Missing;
        private IPoint pMovePt = null;
        private ISelectionEnvironment selectionEnvironment;
        private INewEnvelopeFeedback pNewEnvelopeFeedback;
        //选中图层
        private EnumMapSurroundType _enumMapSurType = EnumMapSurroundType.None;
        private IPoint pPointPt = null;
        private int mQueryMode;
        private int mLayerIndex;
        IMapDocument mapDocument = null;
        private OperatePageLayout m_OperatePageLayout = null;
        private IPoint m_MovePt = null;
        private IPoint m_PointPt = null;
        private IStyleGalleryItem pStyleGalleryItem;
        //AxMapControl mapControl
        //    this.mMapControl = mapControl;
        private string mTool;
        public Form1()
        {
            IActiveView pActiveView;
            IMap pMap;
            InitializeComponent();
            selectionEnvironment = new SelectionEnvironmentClass();
            //this.mMapControl = mapControl;
            IMapControlDefault pMapcontrol;
            pMapcontrol = axMapControl1.Object as IMapControlDefault;
            pMap = axMapControl1.Map;
            pActiveView = pMap as IActiveView;
            skinEngine1.SkinFile = System.Environment.CurrentDirectory + "\\Skins\\MSN.ssk";

        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void 属性查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 attributeQueryForm =
                new Form2(this.axMapControl1);
            //attributeQueryForm.CurrentMap = axMapControl1.Map;
            attributeQueryForm.Show();


        }

        private Point pMoveLayerPoint = new Point();

        private void btnAttribute_Click(object sender, EventArgs e)
        {
            if (frmAttribute == null || frmAttribute.IsDisposed)
            {
                frmAttribute = new FormAtrribute();
            }
            frmAttribute.CurFeatureLayer = pTocFeatureLayer;
            frmAttribute.InitUI();
            frmAttribute.ShowDialog();
        }

        private void btnZoomToLayer_Click(object sender, EventArgs e)
        {

            if (pTocFeatureLayer == null) return;
            (axMapControl1.Map as IActiveView).Extent = pTocFeatureLayer.AreaOfInterest;
            (axMapControl1.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }

        private void btnLayerSel_Click_1(object sender, EventArgs e)
        {
            pTocFeatureLayer.Selectable = true;
            btnLayerSel.Enabled = !btnLayerSel.Enabled;

        }

        private void btnLayerUnSel_Click_1(object sender, EventArgs e)
        {
            pTocFeatureLayer.Selectable = false;
            btnLayerUnSel.Enabled = !btnLayerUnSel.Enabled;
        }

        private void btnRemoveLayer_Click(object sender, EventArgs e)
        {
            try
            {
                if (pTocFeatureLayer == null) return;
                DialogResult result = MessageBox.Show("是否删除(" + pTocFeatureLayer.Name + ")图层", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    axMapControl1.Map.DeleteLayer(pTocFeatureLayer);
                    axMapControl2.Map.DeleteLayer(pTocFeatureLayer);
                }
                axMapControl1.ActiveView.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        //private void 空间查询ToolStripMenuItem_Click_1(object sender, EventArgs e)
        //{

        //    //新创建属性查询窗体
        //    Form3 formQueryBySpatial = new Form3(this.axMapControl1);
        //    if (formQueryBySpatial.ShowDialog() == DialogResult.OK)
        //    {
        //        //this.mTool = "SpaceQuery";
        //        this.mQueryMode = formQueryBySpatial.mQueryMode;
        //        this.mLayerIndex = formQueryBySpatial.mLayerIndex;
        //        this.axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerCrosshair;

        //    }
        //    this.axMapControl1.Map.ClearSelection();
        //    IActiveView pActiveView = this.axMapControl1.ActiveView;

        //    m_PointPt = pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
        //    IEnvelope pEnvelope = new EnvelopeClass();
        //    string pMouseOperate = null;
        //    switch (pMouseOperate)
        //        {


        //            case "ZoomIn":
        //                pEnvelope = axMapControl1.TrackRectangle();
        //                //如果拉框范围为空则返回
        //                if (pEnvelope == null || pEnvelope.IsEmpty || pEnvelope.Height == 0 || pEnvelope.Width == 0)
        //                {
        //                    return;
        //                }
        //                //如果有拉框范围，则放大到拉框范围
        //                pActiveView.Extent = pEnvelope;
        //                pActiveView.Refresh();
        //                break;
        //            case "ZoomOut":
        //                pEnvelope = axMapControl1.TrackRectangle();

        //                //如果拉框范围为空则退出
        //                if (pEnvelope == null || pEnvelope.IsEmpty || pEnvelope.Height == 0 || pEnvelope.Width == 0)
        //                {
        //                    return;
        //                }
        //                //如果有拉框范围，则以拉框范围为中心，缩小倍数为：当前视图范围/拉框范围
        //                else
        //                {
        //                    double dWidth = pActiveView.Extent.Width * pActiveView.Extent.Width / pEnvelope.Width;
        //                    double dHeight = pActiveView.Extent.Height * pActiveView.Extent.Height / pEnvelope.Height;
        //                    double dXmin = pActiveView.Extent.XMin -
        //                                   ((pEnvelope.XMin - pActiveView.Extent.XMin) * pActiveView.Extent.Width /
        //                                    pEnvelope.Width);
        //                    double dYmin = pActiveView.Extent.YMin -
        //                                   ((pEnvelope.YMin - pActiveView.Extent.YMin) * pActiveView.Extent.Height /
        //                                    pEnvelope.Height);
        //                    double dXmax = dXmin + dWidth;
        //                    double dYmax = dYmin + dHeight;
        //                    pEnvelope.PutCoords(dXmin, dYmin, dXmax, dYmax);
        //                }
        //                pActiveView.Extent = pEnvelope;
        //                pActiveView.Refresh();
        //                break;
        //            case "Pan":
        //                axMapControl1.Pan();
        //                break;





        //        case "SpaceQuery":
        //            //panel1.Visible=true
        //            IGeometry pGeometry=null;
        //            if (this.mQueryMode==0)
        //            {pGeometry=this.axMapControl1.TrackRectangle();
        //            }
        //            else if (this.mQueryMode==1){
        //           pGeometry=this.axMapControl1.TrackLine(); 
        //            }
        //            else if(this.mQueryMode==2){
        //            ITopologicalOperator pTopo;
        //                IGeometry pBuffer;
        //                pGeometry=pPoint;
        //                pTopo=pGeometry as ITopologicalOperator;
        //                pBuffer=pTopo.Buffer(0.1);
        //                pGeometry=pBuffer.Envelope;

        //            }
        //            else if (this.mQueryMode==3){

        //            pGeometry=this.axMapControl1.TrackCircle();
        //            }
        //  IFeatureLayer pFeatureLayer=this.axMapControl1.get_Layer(this.mLayerIndex)as  IFeatureLayer;
        //            DataTable pDataTable=this.LoadQueryResult(this.axMapControl1,pFeatureLayer,pGeometry);
        //            this.dataGridView1.DataSource=pDataTable.DefaultView;
        //            this.dataGridView1.Refresh();
        //            break;
        //        default:
        //            break;
        //    }
        //    formQueryBySpatial.CurrentMap = axMapControl1.Map;
        //    //显示空间查询窗体
        //    formQueryBySpatial.Show();
        //}

        private void axMapControl1_OnExtentUpdated(object sender, IMapControlEvents2_OnExtentUpdatedEvent e)
        {
            ////创建鹰眼中线框
            //IGraphicsContainer pGraphicsContainer = axMapControl2.Map as IGraphicsContainer;
            //IEnvelope pEnv = (IEnvelope)e.newEnvelope;
            //IRectangleElement pRectangeEle = new RectangleElementClass();
            //IActiveView pActiveView = pGraphicsContainer as IActiveView;
            //IElement pEle = pRectangeEle as IElement;
            //pEle.Geometry = pEnv;
            ////设置线框的边线对象，包括颜色和线宽
            //IRgbColor pColor = new RgbColorClass();
            //pColor.Red = 255;
            //pColor.Green = 0;
            //pColor.Blue = 0;
            //pColor.Transparency = 255;
            ////产生一个线符号对象
            //ILineSymbol pOutline = new SimpleLineSymbolClass();
            //pOutline.Width = 1;
            //pOutline.Color = pColor;
            ////设置颜色属性
            //pColor = new RgbColorClass();
            //pColor.Red = 255;
            //pColor.Green = 0;
            //pColor.Blue = 0;
            //pColor.Transparency = 0;
            ////设置填充符号的属性
            //IFillSymbol pFillSymbol = new SimpleFillSymbolClass();
            //pFillSymbol.Color = pColor;
            //pFillSymbol.Outline = pOutline;
            //IFillShapeElement pFillShapeEle = pEle as IFillShapeElement;
            //pFillShapeEle.Symbol = pFillSymbol;
            ////在绘制前，清楚axmapcontrol2中的任何图像
            //pGraphicsContainer.DeleteAllElements();
            ////鹰眼视图中添加线框
            //pGraphicsContainer.AddElement((IElement)pFillShapeEle, 0);
            ////刷新地图
            //pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            //得到新范围
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

        private void axMapControl2_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            //if (axMapControl2.Map.LayerCount > 0)
            //{

            //    if (e.button == 1)
            //    {

            //        IPoint pPoint = new PointClass();
            //        pPoint.PutCoords(e.mapX, e.mapY);
            //        axMapControl1.CenterAt(pPoint);
            //        axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);

            //    }
            //    else if (e.button == 2)
            //    {
            //        IEnvelope pEnv = axMapControl2.TrackRectangle();
            //        axMapControl1.Extent = pEnv;
            //        axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);

            //    }

            //}



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
            //if (e.button == 1)
            //{

            //    IPoint pPoint = new PointClass();
            //    pPoint.PutCoords(e.mapX, e.mapY);
            //    axMapControl1.CenterAt(pPoint);
            //    axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);

            //}
            if (e.button != 1)
                return;

            IPoint pPoint = new PointClass();
            pPoint.X = e.mapX;
            pPoint.Y = e.mapY;
            //pPoint.PutCoords(e.mapX, e.mapY);
            axMapControl1.CenterAt(pPoint);
            axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);


        }


        private void axMapControl1_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {
            //if (axMapControl1.LayerCount > 0)
            //{

            //    axMapControl2.Map = new MapClass();
            //    for (int i = 0; i <= axMapControl1.Map.LayerCount - 1; i++)
            //    {

            //        axMapControl2.AddLayer(axMapControl1.get_Layer(i));

            //    }
            //    axMapControl2.Extent = axMapControl1.Extent;
            //    axMapControl2.Refresh();
            //}
            IMap pMap;
            pMap = axMapControl1.Map;

            for (int i = pMap.LayerCount - 1; i >= 0; i--)
            {

                axMapControl2.Map.AddLayer(pMap.get_Layer(i));

            }
            axMapControl2.Extent = axMapControl2.FullExtent;
            axMapControl2.Refresh();

        }
        #region 鹰眼
        //private ILayer GetOverviewLayer(IMap map)
        //{


        //        //获取主视图的第一图层
        //        ILayer pLayer = map.get_Layer(0);
        //        //遍历其他图层，并比较视图范围的宽度，返回宽度最大的图层
        //        ILayer pTempLayer = null;


        //        for (int i = 1; i < map.LayerCount; i++)
        //        {
        //            pTempLayer = map.get_Layer(i);
        //            if (pLayer.AreaOfInterest.Width < pTempLayer.AreaOfInterest.Width)
        //                pLayer = pTempLayer;

        //        }


        //        return pLayer;
        #endregion


        //}
        //private void axMapControl1_OnFullExtentUpdated(object sender, IMapControlEvents2_OnFullExtentUpdatedEvent e)
        //{
        //    //获取鹰眼图层
        //    this.axMapControl2.AddLayer(this.GetOverviewLayer(this.axMapControl1.Map));
        //    //设置mapcontrol显示范围至数据的全局范围
        //    this.axMapControl2.Extent = this.axMapControl1.FullExtent;
        //    //刷新鹰眼控件地图
        //    this.axMapControl2.Refresh();
        //}

        private void Form1_Load(object sender, EventArgs e)
        {
            this.axTOCControl1.SetBuddyControl(axMapControl1.Object);


        }
        private void axTOCControl1_OnMouseUp_1(object sender, ITOCControlEvents_OnMouseUpEvent e)
        {
            try
            {
                if (e.button == 1 && pMoveLayer != null && pMoveLayerPoint.Y != e.y)
                {
                    esriTOCControlItem pItem = esriTOCControlItem.esriTOCControlItemNone;
                    IBasicMap pBasicMap = null; object unk = null;
                    object data = null; ILayer pLayer = null;
                    axTOCControl1.HitTest(e.x, e.y, ref pItem, ref pBasicMap, ref pLayer, ref unk, ref data);
                    IMap pMap = axMapControl1.ActiveView.FocusMap;
                    if (pItem == esriTOCControlItem.esriTOCControlItemLayer || pLayer != null)
                    {
                        if (pMoveLayer != pLayer)
                        {
                            ILayer pTempLayer;
                            //获得鼠标弹起时所在图层的索引号
                            for (int i = 0; i < pMap.LayerCount; i++)
                            {
                                pTempLayer = pMap.get_Layer(i);
                                if (pTempLayer == pLayer)
                                {
                                    toIndex = i;
                                }
                            }
                        }
                    }
                    //移动到最前面
                    else if (pItem == esriTOCControlItem.esriTOCControlItemMap)
                    {
                        toIndex = 0;
                    }
                    //移动到最后面
                    else if (pItem == esriTOCControlItem.esriTOCControlItemNone)
                    {
                        toIndex = pMap.LayerCount - 1;
                    }
                    pMap.MoveLayer(pMoveLayer, toIndex);
                    axMapControl1.ActiveView.Refresh();
                    axTOCControl1.Update();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void axTOCControl1_OnMouseDown_1(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            try
            {
                if (e.button == 2)
                {
                    esriTOCControlItem pItem = esriTOCControlItem.esriTOCControlItemNone;
                    IBasicMap pMap = null;
                    ILayer pLayer = null;
                    object unk = null;
                    object data = null;
                    axTOCControl1.HitTest(e.x, e.y, ref pItem, ref pMap, ref pLayer, ref unk, ref data);
                    pTocFeatureLayer = pLayer as IFeatureLayer;
                    if (pItem == esriTOCControlItem.esriTOCControlItemLayer && pTocFeatureLayer != null)
                    {
                        btnLayerSel.Enabled = !pTocFeatureLayer.Selectable;
                        btnLayerUnSel.Enabled = pTocFeatureLayer.Selectable;
                        contextMenuStrip1.Show(Control.MousePosition);
                    }
                }
                if (e.button == 1)
                {
                    esriTOCControlItem pItem = esriTOCControlItem.esriTOCControlItemNone;
                    IBasicMap pMap = null; object unk = null;
                    object data = null; ILayer pLayer = null;
                    axTOCControl1.HitTest(e.x, e.y, ref pItem, ref pMap, ref pLayer, ref unk, ref data);
                    if (pLayer == null) return;

                    pMoveLayerPoint.PutCoords(e.x, e.y);
                    if (pItem == esriTOCControlItem.esriTOCControlItemLayer)
                    {
                        if (pLayer is IAnnotationSublayer)
                        {
                            return;
                        }
                        else
                        {
                            pMoveLayer = pLayer;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void menuBuffer_Click(object sender, EventArgs e)
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

        private void menuOverlay_Click(object sender, EventArgs e)
        {
            OverlayForm over = new OverlayForm(this.axMapControl1);
            over.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //splitConta.Panel2.Enabled = false;
        }

        private void 加载MXD文档ToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void 加载Shp数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addShpfile();
        }
        //保存地图文档
        //     private void saveDocument()
        //     {

        //         string fileSavePath = @"e:\new.mxd";
        //         try
        //         {
        //             string smxdFileName = axMapControl1.DocumentFilename;
        //             IMapDocument pMapDocument = new MapDocumentClass();
        //             if (smxdFileName != null && axMapControl1.CheckMxFile(smxdFileName))
        //             {
        //                 if (pMapDocument.get_IsReadOnly(smxdFileName))
        //                 {
        //                     MessageBox.Show("地图为只读，不能保存！");
        //                     pMapDocument.Close();
        //                     return;
        //                 }

        //             }
        //             else
        //             {

        //                 SaveFileDialog pSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
        //                 pSaveFileDialog.Title = "请选择保存路径";
        //                 pSaveFileDialog.Filter = "ArcMap()文档(*.mxd)|*.mxd|ArcMap模板(*.mxt)|*.mxt";
        //                 pSaveFileDialog.OverwritePrompt = true;
        //                 pSaveFileDialog.RestoreDirectory = true;
        //                 if (pSaveFileDialog.ShowDialog() == DialogResult.OK)
        //                 {

        //                     smxdFileName = pSaveFileDialog.FileName;
        //                 }
        //                 else { return; }
        //             }


        //         mapDocument.New(fileSavePath);
        //         mapDocument.ReplaceContents(axMapControl1.Map as IMxdContents);
        //         mapDocument.Save(mapDocument.UsesRelativePaths,true);//保存为绝对路径
        //         mapDocument.Close();
        //         MessageBox.Show("保存地图文档成功！");
        //     }
        //catch(Exception ex)
        //{
        // MessageBox.Show(ex.Message);

        //     }
        //     }
        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveDocument();
        }
        private void saveDocument()
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
            catch (Exception e)
            {
                MessageBox.Show("保存地图文档失败！！！" + e.ToString());
            }
        }
        private void 加载图层文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addLayerFile();
        }
        //添加图层文件
        private void addLayerFile()
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
            catch (Exception e)
            {
                MessageBox.Show("添加图层失败" + e.ToString());
            }
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

        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            axMapControl1.Pan();
        }

        private void axPageLayoutControl1_OnMouseDown(object sender, IPageLayoutControlEvents_OnMouseDownEvent e)
        {
            axPageLayoutControl1.Pan();
            try
            {
                if (_enumMapSurType != EnumMapSurroundType.None)
                {
                    IActiveView pActiveView = null;
                    pActiveView = axPageLayoutControl1.PageLayout as IActiveView;
                    m_PointPt = pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
                    if (pNewEnvelopeFeedback == null)
                    {
                        pNewEnvelopeFeedback = new NewEnvelopeFeedbackClass();
                        pNewEnvelopeFeedback.Display = pActiveView.ScreenDisplay;
                        pNewEnvelopeFeedback.Start(m_PointPt);
                    }
                    else
                    {
                        pNewEnvelopeFeedback.MoveTo(m_PointPt);
                    }

                }
            }
            catch
            {
            }
        }

        private void axPageLayoutControl1_OnMouseMove(object sender, IPageLayoutControlEvents_OnMouseMoveEvent e)
        {
            try
            {
                if (_enumMapSurType != EnumMapSurroundType.None)
                {
                    if (pNewEnvelopeFeedback != null)
                    {
                        m_MovePt = (axPageLayoutControl1.PageLayout as IActiveView).ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
                        pNewEnvelopeFeedback.MoveTo(m_MovePt);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void axPageLayoutControl1_OnMouseUp(object sender, IPageLayoutControlEvents_OnMouseUpEvent e)
        {
            if (_enumMapSurType != EnumMapSurroundType.None)
            {
                if (pNewEnvelopeFeedback != null)
                {
                    IActiveView pActiveView = null;
                    pActiveView = axPageLayoutControl1.PageLayout as IActiveView;
                    IEnvelope pEnvelope = pNewEnvelopeFeedback.Stop();
                    AddMapSurround(pActiveView, _enumMapSurType, pEnvelope);
                    pNewEnvelopeFeedback = null;
                    _enumMapSurType = EnumMapSurroundType.None;
                }
            }
        }

        private void AddMapSurround(IActiveView pAV, EnumMapSurroundType _enumMapSurroundType, IEnvelope pEnvelope)
        {
            try
            {
                switch (_enumMapSurroundType)
                {
                    case EnumMapSurroundType.NorthArrow:
                        addNorthArrow(axPageLayoutControl1.PageLayout, pEnvelope, pAV);
                        break;
                    case EnumMapSurroundType.ScaleBar:
                        makeScaleBar(pAV, axPageLayoutControl1.PageLayout, pEnvelope);
                        break;
                    case EnumMapSurroundType.Legend:
                        MakeLegend(pAV, axPageLayoutControl1.PageLayout, pEnvelope);
                        break;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void MakeLegend(IActiveView pActiveView, IPageLayout pPageLayout, IEnvelope pEnv)
        {
            UID pID = new UID();
            pID.Value = "esriCarto.Legend";
            IGraphicsContainer pGraphicsContainer = pPageLayout as IGraphicsContainer;
            IMapFrame pMapFrame = pGraphicsContainer.FindFrame(pActiveView.FocusMap) as IMapFrame;
            IMapSurroundFrame pMapSurroundFrame = pMapFrame.CreateSurroundFrame(pID, null);//根据唯一标示符，创建与之对应MapSurroundFrame
            IElement pDeletElement = axPageLayoutControl1.FindElementByName("Legend");//获取PageLayout中的图例元素
            if (pDeletElement != null)
            {
                pGraphicsContainer.DeleteElement(pDeletElement);  //如果已经存在图例，删除已经存在的图例
            }
            //设置MapSurroundFrame背景
            ISymbolBackground pSymbolBackground = new SymbolBackgroundClass();
            IFillSymbol pFillSymbol = new SimpleFillSymbolClass();
            ILineSymbol pLineSymbol = new SimpleLineSymbolClass();
            pLineSymbol.Color = m_OperatePageLayout.GetRgbColor(0, 0, 0);
            pFillSymbol.Color = m_OperatePageLayout.GetRgbColor(240, 240, 240);
            pFillSymbol.Outline = pLineSymbol;
            pSymbolBackground.FillSymbol = pFillSymbol;
            pMapSurroundFrame.Background = pSymbolBackground;
            //添加图例
            IElement pElement = pMapSurroundFrame as IElement;
            pElement.Geometry = pEnv as IGeometry;
            IMapSurround pMapSurround = pMapSurroundFrame.MapSurround;
            ILegend pLegend = pMapSurround as ILegend;
            pLegend.ClearItems();
            pLegend.Title = "图例";
            for (int i = 0; i < pActiveView.FocusMap.LayerCount; i++)
            {
                ILegendItem pLegendItem = new HorizontalLegendItemClass();
                pLegendItem.Layer = pActiveView.FocusMap.get_Layer(i);//获取添加图例关联图层             
                pLegendItem.ShowDescriptions = false;
                pLegendItem.Columns = 1;
                pLegendItem.ShowHeading = true;
                pLegendItem.ShowLabels = true;
                pLegend.AddItem(pLegendItem);//添加图例内容
            }
            pGraphicsContainer.AddElement(pElement, 0);
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }
        void addNorthArrow(IPageLayout pPageLayout, IEnvelope pEnv, IActiveView pActiveView)
        {
            IMap pMap = pActiveView.FocusMap;
            IGraphicsContainer pGraphicsContainer = pPageLayout as IGraphicsContainer;
            IMapFrame pMapFrame = pGraphicsContainer.FindFrame(pMap) as IMapFrame;
            if (pStyleGalleryItem == null) return;
            IMapSurroundFrame pMapSurroundFrame = new MapSurroundFrameClass();
            pMapSurroundFrame.MapFrame = pMapFrame;
            INorthArrow pNorthArrow = new MarkerNorthArrowClass();
            pNorthArrow = pStyleGalleryItem.Item as INorthArrow;
            pNorthArrow.Size = pEnv.Width * 50;
            pMapSurroundFrame.MapSurround = (IMapSurround)pNorthArrow;//根据用户的选取，获取相应的MapSurround            
            IElement pElement = axPageLayoutControl1.FindElementByName("NorthArrows");//获取PageLayout中的指北针元素
            if (pElement != null)
            {
                pGraphicsContainer.DeleteElement(pElement);  //如果存在指北针，删除已经存在的指北针
            }
            IElementProperties pElePro = null;
            pElement = (IElement)pMapSurroundFrame;
            pElement.Geometry = (IGeometry)pEnv;
            pElePro = pElement as IElementProperties;
            pElePro.Name = "NorthArrows";
            pGraphicsContainer.AddElement(pElement, 0);
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }
        public void makeScaleBar(IActiveView pActiveView, IPageLayout pPageLayout, IEnvelope pEnv)
        {
            IMap pMap = pActiveView.FocusMap;
            IGraphicsContainer pGraphicsContainer = pPageLayout as IGraphicsContainer;
            IMapFrame pMapFrame = pGraphicsContainer.FindFrame(pMap) as IMapFrame;
            if (pStyleGalleryItem == null) return;
            IMapSurroundFrame pMapSurroundFrame = new MapSurroundFrameClass();
            pMapSurroundFrame.MapFrame = pMapFrame;
            pMapSurroundFrame.MapSurround = (IMapSurround)pStyleGalleryItem.Item;
            IElement pElement = axPageLayoutControl1.FindElementByName("ScaleBar");
            if (pElement != null)
            {
                pGraphicsContainer.DeleteElement(pElement);  //删除已经存在的比例尺
            }
            IElementProperties pElePro = null;
            pElement = (IElement)pMapSurroundFrame;
            pElement.Geometry = (IGeometry)pEnv;
            pElePro = pElement as IElementProperties;
            pElePro.Name = "ScaleBar";
            pGraphicsContainer.AddElement(pElement, 0);
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
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

        private void 视图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectTab(this.tabPage2);
        }
        string pMouseOperate = null;
        private void axMapControl1_OnMouseDown_1(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            //this.axMapControl1.Map.ClearSelection();
            //IActiveView pActiveView = this.axMapControl1.ActiveView;
            //IPoint pPoint = pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x,e.y);
            //switch (mTool) { 
            //    case "SpaceQuery":
            //        IGeometry pGeometry = null;
            //        if (this.mQueryMode == 0) 
            //        {

            //            pGeometry = this.axMapControl1.TrackRectangle();

            //        }
            //        else if (this.mQueryMode == 1) 
            //        {
            //            pGeometry = this.axMapControl1.TrackLine();

            //        }
            //        else if (this.mQueryMode == 3)
            //        {
            //            ITopologicalOperator pTopo;
            //            IGeometry pBuffer;
            //            pGeometry = pPoint;
            //            pTopo = pGeometry as ITopologicalOperator;
            //            pBuffer = pTopo.Buffer(0.1);
            //            pGeometry = pBuffer.Envelope;


            //        }
            //        else if (this.mQueryMode == 2)
            //        {

            //            pGeometry = this.axMapControl1.TrackCircle();

            //        }
            //        IFeatureLayer pFeaturelayer = this.axMapControl1.get_Layer(this.mLayerIndex)as IFeatureLayer;
            //        DataTable pDataTable = this.LoadQueryResult(this.axMapControl1,pFeaturelayer, pGeometry);
            // this.DataTable.Refresh();
            //        break;
            //    default:
            //        break;

            //}
            this.axMapControl1.Map.ClearSelection();
            //获?取¨?当ì?à前??视o¨?图a?
            IActiveView pActiveView = this.axMapControl1.ActiveView;
            //获?取¨?鼠o¨?标à¨o点ì?
            IPoint pPoint = pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
            switch (mTool)
            {
                //设|¨¨置?鼠o¨?标à¨o形?状á??
                case "SpaceQuery":
                    IGeometry pGeometry = null;
                    if (this.mQueryMode == 0)

                    //矩?形?查¨|询?￥
                    {
                        pGeometry = this.axMapControl1.TrackRectangle();
                    }
                    else if (this.mQueryMode == 1)
                    //线?查¨|询?￥
                    {
                        pGeometry = this.axMapControl1.TrackLine();
                    }
                    else if (this.mQueryMode == 3)
                    //点ì?查¨|询?￥
                    {
                        ITopologicalOperator pTopo;
                        IGeometry pBuffer;
                        pGeometry = pPoint;
                        pTopo = pGeometry as ITopologicalOperator;
                        //根¨′据Y点ì?位?创???建?§缓o冲?区?，ê?缓o冲?半??径?为a0.1，ê?可¨|修T改?
                        pBuffer = pTopo.Buffer(0.1);
                        pGeometry = pBuffer.Envelope;
                    }
                    else if (this.mQueryMode == 2)

                    //圆2查¨|询?￥
                    {
                        pGeometry = this.axMapControl1.TrackCircle();
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

        private void axMapControl1_ControlRemoved(object sender, ControlEventArgs e)
        {
            axMapControl2.Refresh();
        }

        private void 空间查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 psa = new Form3(this.axMapControl1);
            if (psa.ShowDialog() == DialogResult.OK)
            {
                this.mTool = "SpaceQuery";
                this.mQueryMode = psa.mQueryMode;
                this.mLayerIndex = psa.mLayerIndex;
                this.axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerCrosshair;

            }

        }

        private void axMapControl1_OnDoubleClick(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnDoubleClickEvent e)
        {

        }

    }
}