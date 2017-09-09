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

namespace MapOperation
{
    public partial class FormMain : Form
    {
        #region 变量定义

        //地图导出窗体
        private FormExportMap frmExpMap = null;

        //长度、面积量算
        private FormMeasureResult frmMeasureResult = null;   //量算结果窗体
        private INewLineFeedback pNewLineFeedback;           //追踪线对象
        private INewPolygonFeedback pNewPolygonFeedback;     //追踪面对象
        private IPoint pPointPt = null;                      //鼠标点击点
        private IPoint pMovePt = null;                       //鼠标移动时的当前点
        private double dToltalLength = 0;                    //量测总长度
        private double dSegmentLength = 0;                   //片段距离
        //private IPointCollection pAreaPointCol = new MultipointClass() ;  //面积量算时画的点进行存储；  

        private string sMapUnits = "未知单位";             //地图单位变量
        private object missing = Type.Missing;       

        //TOC菜单
        IFeatureLayer pTocFeatureLayer = null;            //点击的要素图层
        private FormAtrribute frmAttribute = null;        //图层属性窗体
        private ILayer pMoveLayer;                        //需要调整显示顺序的图层
        private int toIndex;                              //存放拖动图层移动到的索引号     

        //鹰眼同步
        private bool bCanDrag;              //鹰眼地图上的矩形框可移动的标志
        private IPoint pMoveRectPoint;      //记录在移动鹰眼地图上的矩形框时鼠标的位置
        private IEnvelope pEnv;             //记录数据视图的Extent
        #endregion

        #region 初始化
        public FormMain()
        {
            InitializeComponent();
            axTOCControl.SetBuddyControl(mainMapControl);

            EagleEyeMapControl.Extent = mainMapControl.FullExtent;
            pEnv = EagleEyeMapControl.Extent;
            DrawRectangle(pEnv);
        }
        #endregion

        #region 数据加载

        #region LoadMxFile方法加载地图文档文件
        private void btnLoadMxFile_Click(object sender, EventArgs e)
        {
            //加载数据前如果有数据则清空
            try
            {
                OpenFileDialog pOpenFileDialog = new OpenFileDialog();
                pOpenFileDialog.CheckFileExists = true;
                pOpenFileDialog.Title = "打开地图文档";
                pOpenFileDialog.Filter = "ArcMap文档(*.mxd)|*.mxd;|ArcMap模板(*.mxt)|*.mxt|发布地图文件(*.pmf)|*.pmf|所有地图格式(*.mxd;*.mxt;*.pmf)|*.mxd;*.mxt;*.pmf";
                pOpenFileDialog.Multiselect = false;   //不允许多个文件同时选择
                pOpenFileDialog.RestoreDirectory = true;   //存储打开的文件路径
                if (pOpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string pFileName = pOpenFileDialog.FileName;
                    if (pFileName == "")
                    {
                        return;
                    }
                    if (mainMapControl.CheckMxFile(pFileName)) //检查地图文档有效性
                    {
                        ClearAllData();
                        mainMapControl.LoadMxFile(pFileName);
                    }
                    else
                    {
                        MessageBox.Show(pFileName + "是无效的地图文档!", "信息提示");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开地图文档失败" + ex.Message);
            }
        }
        #endregion

        #region IMapDocument方法加载Mxd文档文件
        private void btnIMapDocument_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog pOpenFileDialog = new OpenFileDialog();
                pOpenFileDialog.CheckFileExists = true;
                pOpenFileDialog.Title = "打开地图文档";
                pOpenFileDialog.Filter = "ArcMap文档(*.mxd)|*.mxd;|ArcMap模板(*.mxt)|*.mxt|发布地图文件(*.pmf)|*.pmf|所有地图格式(*.mxd;*.mxt;*.pmf)|*.mxd;*.mxt;*.pmf";
                pOpenFileDialog.Multiselect = false;
                pOpenFileDialog.RestoreDirectory = true;
                if (pOpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string pFileName = pOpenFileDialog.FileName;
                    if (pFileName == "")
                    {
                        return;
                    }

                    if (mainMapControl.CheckMxFile(pFileName)) //检查地图文档有效性
                    {
                        //将数据载入pMapDocument并与Map控件关联
                        IMapDocument pMapDocument = new MapDocument();//using ESRI.ArcGIS.Carto;
                        pMapDocument.Open(pFileName, "");
                        //获取Map中激活的地图文档
                        mainMapControl.Map = pMapDocument.ActiveView.FocusMap;
                        mainMapControl.ActiveView.Refresh();
                    }
                    else
                    {
                        MessageBox.Show(pFileName + "是无效的地图文档!", "信息提示");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开地图文档失败" + ex.Message);
            }
        }
        #endregion

        #region ControlsOpenDocCommandClass加载地图
        private void btncontrolsOpenDocCommandClass_Click(object sender, EventArgs e)
        {
            ICommand command = new ControlsOpenDocCommandClass();
            command.OnCreate(mainMapControl.Object);
            command.OnClick();
        }
        #endregion

        #region 加载Shape文件
        private void btnAddShapefile_Click(object sender, EventArgs e)
        {
            //ClearAllData();
            try
            {
                OpenFileDialog pOpenFileDialog = new OpenFileDialog();
                pOpenFileDialog.CheckFileExists = true;
                pOpenFileDialog.Title = "打开Shape文件";
                pOpenFileDialog.Filter = "Shape文件（*.shp）|*.shp";
                pOpenFileDialog.ShowDialog();

                ////获取文件路径
                //FileInfo pFileInfo = new FileInfo(pOpenFileDialog.FileName);
                //string pPath = pOpenFileDialog.FileName.Substring(0, pOpenFileDialog.FileName.Length - pFileInfo.Name.Length);
                //mainMapControl.AddShapeFile(pPath, pFileInfo.Name);

                IWorkspaceFactory pWorkspaceFactory;
                IFeatureWorkspace pFeatureWorkspace;
                IFeatureLayer pFeatureLayer;

                string pFullPath = pOpenFileDialog.FileName;
                if (pFullPath == "") return;

                int pIndex = pFullPath.LastIndexOf("\\");
                string pFilePath = pFullPath.Substring(0, pIndex); //文件路径
                string pFileName = pFullPath.Substring(pIndex + 1); //文件名

                //实例化ShapefileWorkspaceFactory工作空间，打开Shape文件
                pWorkspaceFactory = new ShapefileWorkspaceFactory();
                pFeatureWorkspace = (IFeatureWorkspace)pWorkspaceFactory.OpenFromFile(pFilePath, 0);
                //创建并实例化要素集
                IFeatureClass pFeatureClass = pFeatureWorkspace.OpenFeatureClass(pFileName);
                pFeatureLayer = new FeatureLayer();
                pFeatureLayer.FeatureClass = pFeatureClass;
                pFeatureLayer.Name = pFeatureLayer.FeatureClass.AliasName;

                ClearAllData();    //新增删除数据

                mainMapControl.Map.AddLayer(pFeatureLayer);
                mainMapControl.ActiveView.Refresh();
                //同步鹰眼
                SynchronizeEagleEye();
            }
            catch (Exception ex)
            {
                MessageBox.Show("图层加载失败！" + ex.Message);
            }
        }
        #endregion

        #region 加载栅格文件
        private void btnAddRaster_Click(object sender, EventArgs e)
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
            mainMapControl.AddLayer(pLayer, 0);
        }
        #endregion

        #region 分图层加载CAD数据
        private void btnAddCADByLayer_Click(object sender, EventArgs e)
        {
            IWorkspaceFactory pWorkspaceFactory;
            IFeatureWorkspace pFeatureWorkspace;
            IFeatureLayer pFeatureLayer;
            IFeatureClass pFeatureClass;

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

            pWorkspaceFactory = new CadWorkspaceFactory();
            pFeatureWorkspace = (IFeatureWorkspace)pWorkspaceFactory.OpenFromFile(pFilePath, 0);
            //加载CAD文件中的线文件
            pFeatureClass = pFeatureWorkspace.OpenFeatureClass(pFileName + ":polyline"); 
            pFeatureLayer = new FeatureLayerClass();
            pFeatureLayer.Name = pFileName;
            pFeatureLayer.FeatureClass = pFeatureClass;

            ClearAllData();    //新增删除数据

            mainMapControl.Map.AddLayer(pFeatureLayer);
            mainMapControl.ActiveView.Refresh();
            //同步鹰眼
            SynchronizeEagleEye();
        }
        #endregion

        #region 加载整幅CAD图数据
        private void btnAddWholeCAD_Click(object sender, EventArgs e)
        {
            IWorkspaceFactory pWorkspaceFactory;
            IFeatureWorkspace pFeatureWorkspace;
            IFeatureLayer pFeatureLayer;
            IFeatureDataset pFeatureDataset;

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
            //打开CAD数据集
            pWorkspaceFactory = new CadWorkspaceFactoryClass(); //using ESRI.ArcGIS.DataSourcesFile;
            pFeatureWorkspace = (IFeatureWorkspace)pWorkspaceFactory.OpenFromFile(pFilePath, 0);
            //打开一个要素集
            pFeatureDataset = pFeatureWorkspace.OpenFeatureDataset(pFileName);
            //IFeatureClassContainer可以管理IFeatureDataset中的每个要素类
            IFeatureClassContainer pFeatClassContainer = (IFeatureClassContainer)pFeatureDataset;

            ClearAllData();    //新增删除数据

            //对CAD文件中的要素进行遍历处理
            for (int i = 0; i < pFeatClassContainer.ClassCount; i++)
            {
                IFeatureClass pFeatClass = pFeatClassContainer.get_Class(i);
                //如果是注记，则添加注记层
                if (pFeatClass.FeatureType == esriFeatureType.esriFTCoverageAnnotation)
                {
                    pFeatureLayer = new CadAnnotationLayerClass();
                    pFeatureLayer.Name = pFeatClass.AliasName;
                    pFeatureLayer.FeatureClass = pFeatClass;
                    mainMapControl.Map.AddLayer(pFeatureLayer);
                }
                else //如果是点、线、面则添加要素层
                {
                    pFeatureLayer = new FeatureLayerClass();
                    pFeatureLayer.Name = pFeatClass.AliasName;
                    pFeatureLayer.FeatureClass = pFeatClass;
                    mainMapControl.Map.AddLayer(pFeatureLayer);
                }
                mainMapControl.ActiveView.Refresh();  
            }
            //同步鹰眼
            SynchronizeEagleEye();
        }
        #endregion

        #region 把CAD作为栅格地图进行加载
        private void btnAddRasterByCAD_Click(object sender, EventArgs e)
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

            mainMapControl.Map.AddLayer(pCadLayer);
            mainMapControl.ActiveView.Refresh();
        }
        #endregion

        #region 加载personGeodatabase
        private void btnAddPersonGeodatabase_Click(object sender, EventArgs e)
        {
            IWorkspaceFactory pAccessWorkspaceFactory;

            OpenFileDialog pOpenFileDialog = new OpenFileDialog();
            pOpenFileDialog.Filter = "Personal Geodatabase(*.mdb)|*.mdb";
            pOpenFileDialog.Title = "打开PersonGeodatabase文件";
            pOpenFileDialog.ShowDialog();

            string pFullPath = pOpenFileDialog.FileName;
            if (pFullPath == "")
            {
                return;
            }
            pAccessWorkspaceFactory = new AccessWorkspaceFactory(); //using ESRI.ArcGIS.DataSourcesGDB;
            //获取工作空间
            IWorkspace pWorkspace = pAccessWorkspaceFactory.OpenFromFile(pFullPath, 0);

            ClearAllData();    //新增删除数据

            //加载工作空间里的数据
            AddAllDataset(pWorkspace, mainMapControl);
        }
        #endregion

        #region 加载文件地理库
        private void btnAddFileDatabase_Click(object sender, EventArgs e)
        {
            IWorkspaceFactory pFileGDBWorkspaceFactory;

            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() != DialogResult.OK) return;
            string pFullPath = dlg.SelectedPath;

            if (pFullPath == "")
            {
                return;
            }
            pFileGDBWorkspaceFactory = new FileGDBWorkspaceFactoryClass(); //using ESRI.ArcGIS.DataSourcesGDB;

            ClearAllData();    //新增删除数据

            //获取工作空间
            IWorkspace pWorkspace = pFileGDBWorkspaceFactory.OpenFromFile(pFullPath, 0);
            AddAllDataset(pWorkspace, mainMapControl);
        }
        #endregion

        #region 加载SDE数据库
        /// <summary>
        /// 服务器连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddSDEByService_Click(object sender, EventArgs e)
        {
            IWorkspace pWorkspace;
            pWorkspace = arcSDEWorkspaceOpen("192.168.70.110", "esri_sde", "sde", "sde", "", "SDE.DEFAULT");

            //如果工作空间不为空则进行加载
            if (pWorkspace != null)
            {
                AddAllDataset(pWorkspace, mainMapControl);
            }
        }

        /// <summary>
        /// 直连
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddSDEByDirect_Click(object sender, EventArgs e)
        {
            IWorkspace pWorkspace;
            pWorkspace = arcSDEWorkspaceOpen("", "sde:oracle11g:orcl", "sde", "sde", "", "SDE.DEFAULT");

            //如果工作空间不为空则进行加载
            if (pWorkspace != null)
            {
                AddAllDataset(pWorkspace, mainMapControl);
            }
        }

        /// <summary>
        /// 通过SDE连接打开SDE数据库
        /// </summary>
        /// <param name="server">服务器IP</param>
        /// <param name="instance">数据库实例，应用服务器连接为：5151或esri_sde，直连为sde:oracle11g:orcl(orcl为服务名)</param>
        /// <param name="user">SDE用户名</param>
        /// <param name="password">用户密码</param>
        /// <param name="database">数据库</param>
        /// <param name="version">SDE版本，缺省为"SDE.DEFAULT"</param>
        /// <returns></returns>
        private IWorkspace arcSDEWorkspaceOpen(string server, string instance, string user, string password, string database, string version)
        {
            IWorkspace pWorkSpace = null;
            //创建和实例化数据集
            IPropertySet pPropertySet = new PropertySetClass();
            pPropertySet.SetProperty("SERVER", server);
            pPropertySet.SetProperty("INSTANCE", instance);
            pPropertySet.SetProperty("USER", user);
            pPropertySet.SetProperty("PASSWORD", password);
            pPropertySet.SetProperty("DATABASE", database);
            pPropertySet.SetProperty("VERSION", version);
            IWorkspaceFactory2 pWorkspaceFactory = new SdeWorkspaceFactoryClass();

            try
            {
                pWorkSpace = pWorkspaceFactory.Open(pPropertySet, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return pWorkSpace;
        }
        #endregion

        #region 加载txt文本数据
        private void btnAddTxt_Click(object sender, EventArgs e)
        {
            FormAddtxt frmAddtxt = new FormAddtxt();
            frmAddtxt.BuddyMap = mainMapControl;
            frmAddtxt.Show();
        }      
        #endregion
        
        #endregion

        #region 保存
        //保存地图
        private void btnSaveMap_Click(object sender, EventArgs e)
        {
            try
            {
                string sMxdFileName = mainMapControl.DocumentFilename;
                IMapDocument pMapDocument = new MapDocumentClass();
                if (sMxdFileName!=null && mainMapControl.CheckMxFile(sMxdFileName))
                {     
                    if (pMapDocument.get_IsReadOnly(sMxdFileName))
                    {
                        MessageBox.Show("本地图文档是只读的，不能保存!");
                        pMapDocument.Close();
                        return;
                    }  
                }
                else
                {
                    SaveFileDialog pSaveFileDialog = new SaveFileDialog();
                    pSaveFileDialog.Title = "请选择保存路径";
                    pSaveFileDialog.OverwritePrompt = true;
                    pSaveFileDialog.Filter = "ArcMap文档（*.mxd）|*.mxd|ArcMap模板（*.mxt）|*.mxt";
                    pSaveFileDialog.RestoreDirectory = true;
                    if (pSaveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        sMxdFileName = pSaveFileDialog.FileName;
                    }
                    else
                    {
                        return;
                    }
                }

                pMapDocument.New(sMxdFileName);
                pMapDocument.ReplaceContents(mainMapControl.Map as IMxdContents);
                pMapDocument.Save(pMapDocument.UsesRelativePaths, true);
                pMapDocument.Close();
                MessageBox.Show("保存地图文档成功!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //ICommand command = new ControlsSaveAsDocCommandClass();
            //command.OnCreate(mainMapControl.Object);
            //command.OnClick();
        }

        //地图另存为
        private void btnSaveAsMap_Click(object sender, EventArgs e)
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
                    pMapDocument.ReplaceContents(mainMapControl.Map as IMxdContents);
                    pMapDocument.Save(true, true);
                    pMapDocument.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 地图浏览
        //拉框放大
        string pMouseOperate = null;
        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            mainMapControl.CurrentTool = null;
            pMouseOperate = "ZoomIn";
            mainMapControl.MousePointer = esriControlsMousePointer.esriPointerZoomIn;
        }

        //拉框缩小
        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            mainMapControl.CurrentTool = null;
            pMouseOperate = "ZoomOut";
            mainMapControl.MousePointer = esriControlsMousePointer.esriPointerZoomOut;

            #region 自定义拉框缩小
            //IEnvelope trackExtent, currentExtent, NewIEN = null;
            //currentExtent = mainMapControl.Extent;
            //trackExtent = mainMapControl.TrackRectangle();
            //double dXmin = 0, dYmin = 0, dXmax = 0, dYmax = 0, dHeight = 0, dWidth = 0;
            //dWidth = currentExtent.Width * (currentExtent.Width / trackExtent.Width);
            //dHeight = currentExtent.Height * (currentExtent.Height / trackExtent.Height);
            //dXmin = currentExtent.XMin - ((trackExtent.XMin - currentExtent.XMin) * (currentExtent.Width / trackExtent.Width));
            //dYmin = currentExtent.YMin - ((trackExtent.YMin - currentExtent.YMin) * (currentExtent.Height / trackExtent.Height));
            //dXmax = dXmin + dWidth;
            //dYmax = dYmin + dHeight;

            //NewIEN = new EnvelopeClass();
            //NewIEN.PutCoords(dXmin, dYmin, dXmax, dYmax);
            //mainMapControl.Extent = NewIEN;
            #endregion
        }

        //逐级放大
        private void btnZoomInStep_Click(object sender, EventArgs e)
        {
            IEnvelope pEnvelope;
            pEnvelope = mainMapControl.Extent;
            pEnvelope.Expand(0.5, 0.5, true);     //这里设置放大为2倍，可以根据需要具体设置
            mainMapControl.Extent = pEnvelope;
            mainMapControl.ActiveView.Refresh();
        }

        //逐级缩小
        private void btnZoomOutStep_Click(object sender, EventArgs e)
        {
            //IEnvelope pEnvelope;
            //pEnvelope = mainMapControl.Extent;
            //pEnvelope.Expand(1.5, 1.5, true);
            //mainMapControl.Extent = pEnvelope;
            //mainMapControl.ActiveView.Refresh();

            IActiveView pActiveView = mainMapControl.ActiveView;
            IPoint centerPoint = new PointClass();
            centerPoint.PutCoords((pActiveView.Extent.XMin + pActiveView.Extent.XMax) / 2, (pActiveView.Extent.YMax + pActiveView.Extent.YMin) / 2);
            IEnvelope envlope = pActiveView.Extent;
            envlope.Expand(1.5, 1.5, true);       //和放大的区别在于Expand函数的参数不同
            pActiveView.Extent.CenterAt(centerPoint);
            pActiveView.Extent = envlope;
            pActiveView.Refresh();
        }

        //漫游
        private void btnPan_Click(object sender, EventArgs e)
        {
            mainMapControl.CurrentTool = null;
            pMouseOperate = "Pan";
            mainMapControl.MousePointer = esriControlsMousePointer.esriPointerPan;
        }

        IExtentStack pExtentStack;
        //前一视图       
        private void btnFrontView_Click(object sender, EventArgs e)
        {        
            pExtentStack = mainMapControl.ActiveView.ExtentStack;
            //判断是否可以回到前一视图，第一个视图没有前一视图
            if (pExtentStack.CanUndo())
            {
                pExtentStack.Undo();
                btnForWardView.Enabled = true;
                if (!pExtentStack.CanUndo())
                {
                    btnFrontView.Enabled = false;
                }
            }
            mainMapControl.ActiveView.Refresh();
        }

        //后一视图
        private void btnForWardView_Click(object sender, EventArgs e)
        {
            pExtentStack = mainMapControl.ActiveView.ExtentStack;
            //判断是否可以回到后一视图，最后一个视图没有后一视图
            if (pExtentStack.CanRedo())
            {
                pExtentStack.Redo();
                btnFrontView.Enabled = true;
                if (!pExtentStack.CanRedo())
                {
                    btnForWardView.Enabled = false;
                }
            }
            mainMapControl.ActiveView.Refresh();
        }

        //全图显示
        private void btnFullView_Click(object sender, EventArgs e)
        {
            mainMapControl.Extent = mainMapControl.FullExtent;
        }
        #endregion

        #region 地图导出
        //区域导出
        private void btnExportRegion_Click(object sender, EventArgs e)
        {
            mainMapControl.CurrentTool = null;
            mainMapControl.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
            pMouseOperate = "ExportRegion";
        }

        //全域导出
        private void btnExportMap_Click(object sender, EventArgs e)
        {
            if (frmExpMap == null || frmExpMap.IsDisposed)
            {
                frmExpMap = new FormExportMap(mainMapControl);    
            }
            frmExpMap.IsRegion = false;
            frmExpMap.GetGeometry = mainMapControl.ActiveView.Extent;
            frmExpMap.Show();
            frmExpMap.Activate();
        }
        #endregion

        #region 书签管理
        //添加书签
        private void btnAddBookMark_Click(object sender, EventArgs e)
        {
            FormBookmark frmBookMark = new FormBookmark();
            frmBookMark.ShowDialog();
            string pName = string.Empty;
            int check = frmBookMark.Check;

            if (check == 1)
            {
                pName = frmBookMark.Bookmark;
            }
            if (string.IsNullOrEmpty(pName)) return;

            //书签进行重名判断
            IMapBookmarks mapBookmarks= mainMapControl.Map as IMapBookmarks;
            IEnumSpatialBookmark enumSpatialBookmarks = mapBookmarks.Bookmarks;
            enumSpatialBookmarks.Reset();
            ISpatialBookmark pSpatialBookmark;

            while ((pSpatialBookmark = enumSpatialBookmarks.Next()) != null)
            {
                if (pName == pSpatialBookmark.Name)
                {
                    DialogResult dr = MessageBox.Show("此书签名已存在！是否替换？", "提示", MessageBoxButtons.YesNoCancel);
                    if (dr == DialogResult.Yes)
                    {
                        mapBookmarks.RemoveBookmark(pSpatialBookmark);
                    }
                    else if (dr == DialogResult.No)
                    {
                        frmBookMark.ShowDialog();
                    }
                    else
                    {
                        return;
                    }
                }
            }

            //获取当前地图的对象
            IActiveView pActiveView = mainMapControl.Map as IActiveView;
            //创建一个新的书签并设置其位置范围为当前视图的范围
            IAOIBookmark pBookmark = new AOIBookmarkClass();
            pBookmark.Location = pActiveView.Extent;
            //获得书签名
            pBookmark.Name = pName;
            //通过IMapBookmarks接口访问当前地图书签集，添加书签到地图的书签集中
            IMapBookmarks pMapBookmarks = mainMapControl.Map as IMapBookmarks;
            pMapBookmarks.AddBookmark(pBookmark);
        }

        //管理书签
        private void btnMangeBookMark_Click(object sender, EventArgs e)
        {
            try
            {
                FormManageBookMarks frmManageBookMark = new FormManageBookMarks(mainMapControl.Map);
                frmManageBookMark.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 地图量测
        //距离量测
        private void btnMeasureLength_Click(object sender, EventArgs e)
        {
            mainMapControl.CurrentTool = null;
            pMouseOperate = "MeasureLength";
            mainMapControl.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
            if (frmMeasureResult == null || frmMeasureResult.IsDisposed)
            {
                frmMeasureResult = new FormMeasureResult();
                frmMeasureResult.frmClosed += new FormMeasureResult.FormClosedEventHandler(frmMeasureResult_frmColsed);
                frmMeasureResult.lblMeasureResult.Text = "";
                frmMeasureResult.Text = "距离量测";
                frmMeasureResult.Show();
            }
            else
            {
                frmMeasureResult.Activate();
            }
        }

        //面积测量
        private void btnMeasureArea_Click(object sender, EventArgs e)
        {
            mainMapControl.CurrentTool = null;
            pMouseOperate = "MeasureArea";
            mainMapControl.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
            if (frmMeasureResult == null || frmMeasureResult.IsDisposed)
            {
                frmMeasureResult = new FormMeasureResult();
                frmMeasureResult.frmClosed += new FormMeasureResult.FormClosedEventHandler(frmMeasureResult_frmColsed);
                frmMeasureResult.lblMeasureResult.Text = "";
                frmMeasureResult.Text = "面积量测";
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
            mainMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);
            //结束量算功能
            pMouseOperate = string.Empty;
            mainMapControl.MousePointer = esriControlsMousePointer.esriPointerDefault;
        }
        #endregion

        #region 要素选择
        //要素选择
        private void btnSelFeature_Click(object sender, EventArgs e)
        {
            //bSelectFeature = true;
            #region 调用类库资源
            mainMapControl.CurrentTool = null;
            ControlsSelectFeaturesTool pTool = new ControlsSelectFeaturesToolClass();
            pTool.OnCreate(mainMapControl.Object);
            mainMapControl.CurrentTool = pTool as ITool;
            #endregion
            //pMouseOperate = "SelFeature";
        }

        //缩放至选择
        private void btnZoomToSel_Click(object sender, EventArgs e)
        {
            #region 调用类库资源
            //ICommand pCommand = new ESRI.ArcGIS.Controls.ControlsZoomToSelectedCommandClass();
            //pCommand.OnCreate(mainMapControl.Object);
            //pCommand.OnClick();
            #endregion

            int nSlection = mainMapControl.Map.SelectionCount;
            if (nSlection == 0)
            {
                MessageBox.Show("请先选择要素！", "提示");
            }
            else
            {
                ISelection selection = mainMapControl.Map.FeatureSelection;
                IEnumFeature enumFeature = (IEnumFeature) selection;
                enumFeature.Reset();
                IEnvelope pEnvelope = new EnvelopeClass();
                IFeature pFeature = enumFeature.Next();
                while (pFeature != null)
                {
                    pEnvelope.Union(pFeature.Extent);
                    pFeature = enumFeature.Next();
                }
                pEnvelope.Expand(1.1, 1.1, true);
                mainMapControl.ActiveView.Extent = pEnvelope;
                mainMapControl.ActiveView.Refresh();
            }
        }

        //清除选择
        private void btnClearSel_Click(object sender, EventArgs e)
        {
            #region 调用类库资源
            //ICommand pCommand = new ESRI.ArcGIS.Controls.ControlsClearSelectionCommandClass();
            //pCommand.OnCreate(mainMapControl.Object);
            //pCommand.OnClick();
            #endregion

            IActiveView pActiveView = mainMapControl.ActiveView;
            pActiveView.FocusMap.ClearSelection();
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, pActiveView.Extent);
        }
        #endregion

        #region mainMapControl事件
        private void mainMapControl_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            //屏幕坐标点转化为地图坐标点
            pPointPt = (mainMapControl.Map as IActiveView).ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);

            if (e.button == 1)
            {
                IActiveView pActiveView = mainMapControl.ActiveView;
                IEnvelope pEnvelope = new EnvelopeClass();

                switch (pMouseOperate)
                {
                    #region 拉框放大

                    case "ZoomIn":
                        pEnvelope = mainMapControl.TrackRectangle();
                        //如果拉框范围为空则返回
                        if (pEnvelope == null || pEnvelope.IsEmpty || pEnvelope.Height == 0 || pEnvelope.Width == 0)
                        {
                            return;
                        }
                        //如果有拉框范围，则放大到拉框范围
                        pActiveView.Extent = pEnvelope;
                        pActiveView.Refresh();
                        break;

                        #endregion

                    #region 拉框缩小

                    case "ZoomOut":
                        pEnvelope = mainMapControl.TrackRectangle();

                        //如果拉框范围为空则退出
                        if (pEnvelope == null || pEnvelope.IsEmpty || pEnvelope.Height == 0 || pEnvelope.Width == 0)
                        {
                            return;
                        }
                            //如果有拉框范围，则以拉框范围为中心，缩小倍数为：当前视图范围/拉框范围
                        else
                        {
                            double dWidth = pActiveView.Extent.Width*pActiveView.Extent.Width/pEnvelope.Width;
                            double dHeight = pActiveView.Extent.Height*pActiveView.Extent.Height/pEnvelope.Height;
                            double dXmin = pActiveView.Extent.XMin -
                                           ((pEnvelope.XMin - pActiveView.Extent.XMin)*pActiveView.Extent.Width/
                                            pEnvelope.Width);
                            double dYmin = pActiveView.Extent.YMin -
                                           ((pEnvelope.YMin - pActiveView.Extent.YMin)*pActiveView.Extent.Height/
                                            pEnvelope.Height);
                            double dXmax = dXmin + dWidth;
                            double dYmax = dYmin + dHeight;
                            pEnvelope.PutCoords(dXmin, dYmin, dXmax, dYmax);
                        }
                        pActiveView.Extent = pEnvelope;
                        pActiveView.Refresh();
                        break;

                        #endregion

                    #region 漫游

                    case "Pan":
                        mainMapControl.Pan();
                        break;

                        #endregion

                    #region 选择要素

                    case "SelFeature":
                        IEnvelope pEnv = mainMapControl.TrackRectangle();
                        IGeometry pGeo = pEnv as IGeometry;
                        //矩形框若为空，即为点选时，对点范围进行扩展
                        if (pEnv.IsEmpty == true)
                        {
                            tagRECT r;
                            r.left = e.x - 5;
                            r.top = e.y - 5;
                            r.right = e.x + 5;
                            r.bottom = e.y + 5;
                            pActiveView.ScreenDisplay.DisplayTransformation.TransformRect(pEnv, ref r, 4);
                            pEnv.SpatialReference = pActiveView.FocusMap.SpatialReference;
                        }
                        pGeo = pEnv as IGeometry;
                        mainMapControl.Map.SelectByShape(pGeo, null, false);
                        mainMapControl.Refresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                        break;

                        #endregion

                    #region 区域导出
                    case "ExportRegion":
                        //删除视图中数据
                        mainMapControl.ActiveView.GraphicsContainer.DeleteAllElements();
                        mainMapControl.ActiveView.Refresh();
                        IPolygon pPolygon = DrawPolygon(mainMapControl);
                        if (pPolygon == null) return;
                        ExportMap.AddElement(pPolygon, mainMapControl.ActiveView);
                        if (frmExpMap == null || frmExpMap.IsDisposed)
                        {
                            frmExpMap = new FormExportMap(mainMapControl);
                        }
                        frmExpMap.IsRegion = true;
                        frmExpMap.GetGeometry = pPolygon as IGeometry;
                        frmExpMap.Show();
                        frmExpMap.Activate();
                        break;
                    #endregion

                    #region 距离量算
                    case "MeasureLength":
                        //判断追踪线对象是否为空，若是则实例化并设置当前鼠标点为起始点
                        if (pNewLineFeedback == null)
                        {
                            //实例化追踪线对象
                            pNewLineFeedback = new NewLineFeedbackClass();
                            pNewLineFeedback.Display = (mainMapControl.Map as IActiveView).ScreenDisplay;
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
                    #endregion

                    #region 面积量算
                    case "MeasureArea":
                        if (pNewPolygonFeedback == null)
                        {
                            //实例化追踪面对象
                            pNewPolygonFeedback = new NewPolygonFeedback();
                            pNewPolygonFeedback.Display = (mainMapControl.Map as IActiveView).ScreenDisplay;
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
                #endregion

                    #region 要素选择
                    case "SelectFeature":
                        IPoint point = new PointClass();
                        IGeometry pGeometry = point as IGeometry;
                        mainMapControl.Map.SelectByShape(pGeometry, null, false);
                        mainMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                        break;
                #endregion

                    default:
                        break;
                }
            }
            else if (e.button == 2)
            {
                pMouseOperate = "";
                mainMapControl.MousePointer = esriControlsMousePointer.esriPointerDefault;
            }
        }

        private void mainMapControl_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            sMapUnits = GetMapUnit(mainMapControl.Map.MapUnits);
            barCoorTxt.Text = String.Format("当前坐标：X = {0:#.###} Y = {1:#.###} {2}", e.mapX, e.mapY, sMapUnits);
            pMovePt = (mainMapControl.Map as IActiveView).ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);

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
                    frmMeasureResult.frmClosed += new FormMeasureResult.FormClosedEventHandler(frmMeasureResult_frmColsed);
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
                    pGeo.Project(mainMapControl.Map.SpatialReference);
                    IArea pArea = pGeo as IArea;

                    frmMeasureResult.lblMeasureResult.Text = String.Format(
                        "总面积为：{0:.####}平方{1};\r\n总长度为：{2:.####}{1}",
                        pArea.Area, sMapUnits, pPolygon.Length);
                    pPolygon = null;
                }
            }
            #endregion
        }

        private void mainMapControl_OnDoubleClick(object sender, IMapControlEvents2_OnDoubleClickEvent e)
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
                    (mainMapControl.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);
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
                    (mainMapControl.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);
                }
                pAreaPointCol.RemovePoints(0, pAreaPointCol.PointCount); //清空点集中所有点
            }
            #endregion
        }
        #endregion

        #region 封装的方法
        /// <summary>
        /// 加载工作空间里面的要素和栅格数据
        /// </summary>
        /// <param name="pWorkspace"></param>
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
            //同步鹰眼
            SynchronizeEagleEye();
        }

        private void ClearAllData()
        {
            if (mainMapControl.Map != null && mainMapControl.Map.LayerCount > 0)
            {
                //新建mainMapControl中Map
                IMap dataMap = new MapClass();
                dataMap.Name = "Map";
                mainMapControl.DocumentFilename = string.Empty;
                mainMapControl.Map = dataMap;

                //新建EagleEyeMapControl中Map
                IMap eagleEyeMap = new MapClass();
                eagleEyeMap.Name = "eagleEyeMap";
                EagleEyeMapControl.DocumentFilename = string.Empty;
                EagleEyeMapControl.Map = eagleEyeMap;
            }
        }

        /// <summary>
        /// 获取RGB颜色
        /// </summary>
        /// <param name="intR">红</param>
        /// <param name="intG">绿</param>
        /// <param name="intB">蓝</param>
        /// <returns></returns>
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

        /// <summary>
        /// 获取地图单位
        /// </summary>
        /// <param name="_esriMapUnit"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 绘制多边形
        /// </summary>
        /// <param name="mapCtrl"></param>
        /// <returns></returns>
        public IPolygon DrawPolygon(AxMapControl mapCtrl)
        {
            IGeometry pGeometry = null;
            if (mapCtrl == null) return null;
            IRubberBand rb = new RubberPolygonClass();
            pGeometry = rb.TrackNew(mapCtrl.ActiveView.ScreenDisplay, null);
            return pGeometry as IPolygon;
        }
        #endregion

        #region 鹰眼的实现及同步
        private void mainMapControl_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {
            SynchronizeEagleEye();
        }

        private void SynchronizeEagleEye()
        {
            if (EagleEyeMapControl.LayerCount > 0)
            {
                EagleEyeMapControl.ClearLayers();
            }
            //设置鹰眼和主地图的坐标系统一致
            EagleEyeMapControl.SpatialReference = mainMapControl.SpatialReference;
            for (int i = mainMapControl.LayerCount - 1; i >= 0; i--)
            {
                //使鹰眼视图与数据视图的图层上下顺序保持一致
                ILayer pLayer = mainMapControl.get_Layer(i);
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
                                EagleEyeMapControl.AddLayer(pLayer);
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
                            EagleEyeMapControl.AddLayer(pLayer);
                        }
                    }
                }
                //设置鹰眼地图全图显示  
                EagleEyeMapControl.Extent = mainMapControl.FullExtent;
                pEnv = mainMapControl.Extent as IEnvelope;
                DrawRectangle(pEnv);
                EagleEyeMapControl.ActiveView.Refresh();
            }
        }

        private void EagleEyeMapControl_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            if (EagleEyeMapControl.Map.LayerCount > 0)
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
                    IEnvelope pEnvelope = EagleEyeMapControl.TrackRectangle();

                    IPoint pTempPoint = new PointClass();
                    pTempPoint.PutCoords(pEnvelope.XMin + pEnvelope.Width / 2, pEnvelope.YMin + pEnvelope.Height / 2);
                    mainMapControl.Extent = pEnvelope;
                    //矩形框的高宽和数据试图的高宽不一定成正比，这里做一个中心调整
                    mainMapControl.CenterAt(pTempPoint);
                }
            }
        }

        //移动矩形框
        private void EagleEyeMapControl_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            if (e.mapX > pEnv.XMin && e.mapY > pEnv.YMin && e.mapX < pEnv.XMax && e.mapY < pEnv.YMax)
            {
                //如果鼠标移动到矩形框中，鼠标换成小手，表示可以拖动
                EagleEyeMapControl.MousePointer = esriControlsMousePointer.esriPointerHand;             
                if (e.button == 2)  //如果在内部按下鼠标右键，将鼠标演示设置为默认样式
                {
                    EagleEyeMapControl.MousePointer = esriControlsMousePointer.esriPointerDefault;
                }
            }
            else
            {
                //在其他位置将鼠标设为默认的样式
                EagleEyeMapControl.MousePointer = esriControlsMousePointer.esriPointerDefault;
            }

            if (bCanDrag)
            {
                double Dx, Dy;  //记录鼠标移动的距离
                Dx = e.mapX - pMoveRectPoint.X;
                Dy = e.mapY - pMoveRectPoint.Y;
                pEnv.Offset(Dx, Dy); //根据偏移量更改 pEnv 位置
                pMoveRectPoint.PutCoords(e.mapX, e.mapY);
                DrawRectangle(pEnv);
                mainMapControl.Extent = pEnv;
            }
        }

        private void EagleEyeMapControl_OnMouseUp(object sender, IMapControlEvents2_OnMouseUpEvent e)
        {
            if (e.button == 1 && pMoveRectPoint!=null)
            {
                if (e.mapX == pMoveRectPoint.X && e.mapY == pMoveRectPoint.Y)
                {
                    mainMapControl.CenterAt(pMoveRectPoint);
                }
                bCanDrag = false;
            }
        }

        //绘制矩形框
        private void mainMapControl_OnExtentUpdated(object sender, IMapControlEvents2_OnExtentUpdatedEvent e)
        {
            //得到当前视图范围
            pEnv = (IEnvelope)e.newEnvelope;
            DrawRectangle(pEnv);
        }

        //在鹰眼地图上面画矩形框
        private void DrawRectangle(IEnvelope pEnvelope)
        {
            //在绘制前，清除鹰眼中之前绘制的矩形框
            IGraphicsContainer pGraphicsContainer = EagleEyeMapControl.Map as IGraphicsContainer;
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
        #endregion

        #region 布局视图与数据视图的同步
        private void CopyToPageLayout()
        {
            IObjectCopy pObjectCopy = new ObjectCopyClass();
            object copyFromMap = mainMapControl.Map;
            object copiedMap = pObjectCopy.Copy(copyFromMap);//复制地图到copiedMap中
            object copyToMap = axPageLayoutControl.ActiveView.FocusMap;
            pObjectCopy.Overwrite(copiedMap, ref copyToMap); //复制地图
            axPageLayoutControl.ActiveView.Refresh();
        }

        private void mainMapControl_OnAfterScreenDraw(object sender, IMapControlEvents2_OnAfterScreenDrawEvent e)
        {
            IActiveView pActiveView = (IActiveView)axPageLayoutControl.ActiveView.FocusMap;
            IDisplayTransformation displayTransformation = pActiveView.ScreenDisplay.DisplayTransformation;
            displayTransformation.VisibleBounds = mainMapControl.Extent;
            axPageLayoutControl.ActiveView.Refresh();
            CopyToPageLayout();
        }
        #endregion

        #region TOC右键菜单的添加及功能实现
        private Point pMoveLayerPoint= new Point();  //鼠标在TOC中左键按下时点的位置
        //TOC右键菜单的添加
        private void axTOCControl_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
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
                    axTOCControl.HitTest(e.x, e.y, ref pItem, ref pMap, ref pLayer, ref unk, ref data);
                    pTocFeatureLayer = pLayer as IFeatureLayer;
                    if (pItem == esriTOCControlItem.esriTOCControlItemLayer && pTocFeatureLayer != null)
                    {
                        btnLayerSel.Enabled = !pTocFeatureLayer.Selectable;
                        btnLayerUnSel.Enabled = pTocFeatureLayer.Selectable;
                        contextMenuStrip.Show(Control.MousePosition);
                    }
                }
                if (e.button == 1)
                {
                    esriTOCControlItem pItem = esriTOCControlItem.esriTOCControlItemNone;
                    IBasicMap pMap = null; object unk = null;
                    object data = null; ILayer pLayer = null;
                    axTOCControl.HitTest(e.x, e.y, ref pItem, ref pMap, ref pLayer, ref unk, ref data);
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

        private void axTOCControl_OnMouseUp(object sender, ITOCControlEvents_OnMouseUpEvent e)
        {
            try
            {
                if (e.button == 1 && pMoveLayer != null && pMoveLayerPoint.Y != e.y)
                {
                    esriTOCControlItem pItem = esriTOCControlItem.esriTOCControlItemNone;
                    IBasicMap pBasicMap = null; object unk = null;
                    object data = null; ILayer pLayer = null;
                    axTOCControl.HitTest(e.x, e.y, ref pItem, ref pBasicMap, ref pLayer, ref unk, ref data);
                    IMap pMap = mainMapControl.ActiveView.FocusMap;
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
                    mainMapControl.ActiveView.Refresh();
                    axTOCControl.Update();
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        // 属性表窗口
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

        //缩放到图层
        private void btnZoomToLayer_Click(object sender, EventArgs e)
        {
            if (pTocFeatureLayer == null) return;
            (mainMapControl.Map as IActiveView).Extent = pTocFeatureLayer.AreaOfInterest;
            (mainMapControl.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }

        // 移除图层
        private void btnRemoveLayer_Click(object sender, EventArgs e)
        {
            try
            {
                if (pTocFeatureLayer == null) return;
                DialogResult result = MessageBox.Show("是否删除[" + pTocFeatureLayer.Name + "]图层", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    mainMapControl.Map.DeleteLayer(pTocFeatureLayer);
                }
                mainMapControl.ActiveView.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLayerSel_Click(object sender, EventArgs e)
        {
            pTocFeatureLayer.Selectable = true;
            btnLayerSel.Enabled = !btnLayerSel.Enabled;
        }

        private void btnLayerUnsel_Click(object sender, EventArgs e)
        {
            pTocFeatureLayer.Selectable = false;
            btnLayerUnSel.Enabled = !btnLayerUnSel.Enabled;
        }
        #endregion
    }
}
