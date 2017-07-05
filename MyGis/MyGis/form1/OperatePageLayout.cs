using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.Output;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
namespace MyGis.form1
{
        class OperatePageLayout
    {




        #region RGB颜色
        /// <summary>
        /// RGB颜色设置
        /// </summary>
        /// <param name="intR"></param>
        /// <param name="intG"></param>
        /// <param name="intB"></param>
        /// <returns></returns>
        public IRgbColor GetRgbColor(int intR, int intG, int intB)
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
        /// 获取指定路径文件夹下子文件夹名称
        /// </summary>
        /// <param name="sDirPath"></param>
        /// <returns></returns>
        public static List<string> GetChildDirectoryName(string sDirPath)
        {
            List<string> plstDirName = null;
            try
            {
                string sDirName = string.Empty;
                plstDirName = new List<string>();
                DirectoryInfo _direcInfo = new DirectoryInfo(sDirPath);
                DirectoryInfo[] _dirInfo = _direcInfo.GetDirectories();
                foreach (DirectoryInfo _directoryInfo in _dirInfo)
                {
                    sDirName = _directoryInfo.Name;
                    if (!plstDirName.Contains(sDirName))
                    {
                        plstDirName.Add(sDirName);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return plstDirName;
        }
        #endregion
        #region 获取指定文件夹下文件名称
        /// <summary>
        /// 获取指定文件夹下文件名称
        /// </summary>
        /// <param name="sDirPath"></param>
        /// <returns></returns>
        public static List<string> GetFiles(string sDirPath)
        {
            List<string> plstFileName = null;
            try
            {
                FileInfo[] _fileInfo = null;
                string sFileName = string.Empty;
                plstFileName = new List<string>();

                DirectoryInfo _direcInfo = new DirectoryInfo(sDirPath);
                _fileInfo = _direcInfo.GetFiles();
                foreach (FileInfo _fInfo in _fileInfo)
                {
                    sFileName = _fInfo.Name;
                    if (!plstFileName.Contains(sFileName))
                    {
                        plstFileName.Add(sFileName);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return plstFileName;
        }
        #endregion
        #region 模板替换
        /// <summary>
        /// 模板替换
        /// </summary>
        /// <param name="_pageLayoutCtrl"></param>
        /// <param name="sTemplatePath"></param>
        /// <returns></returns>
        public static bool UseTemplateMxtToPageLayout(AxPageLayoutControl _pageLayoutCtrl, string sTemplatePath)
        {
            bool bSuccess = false;
            try
            {
                IMap pMap = null;
                IActiveView pActiveView = null;
                IPageLayout pCurPageLayout = _pageLayoutCtrl.PageLayout;
                pActiveView = pCurPageLayout as IActiveView;
                pMap = pActiveView.FocusMap;

                //读取模板
                IMapDocument pTempMapDocument = new MapDocumentClass();
                pTempMapDocument.Open(sTemplatePath, "");

                IPageLayout pTempPageLayout = pTempMapDocument.PageLayout;
                IPage pTempPage = pTempPageLayout.Page;
                IPage pCurPage = pCurPageLayout.Page;

                //替换单位及地图方向
                pCurPage.Orientation = pTempPage.Orientation;
                pCurPage.Units = pTempPage.Units;

                //替换页面尺寸
                Double dWidth; Double dHeight;
                pTempPage.QuerySize(out dWidth, out dHeight);
                pCurPage.PutCustomSize(dWidth, dHeight);

                //删除当前layout中除了mapframe外的所有element
                IGraphicsContainer pGraph;
                pGraph = pCurPageLayout as IGraphicsContainer;
                pGraph.Reset();
                IElement pElement = pGraph.Next();
                IMapFrame pMapFrame = null;
                pMapFrame = pGraph.FindFrame(pMap) as IMapFrame;
                while (pElement != null)
                {
                    if (pElement is IMapFrame)
                    {
                        pMapFrame = pElement as IMapFrame;
                    }
                    else
                    {
                        pGraph.DeleteElement(pElement);
                        pGraph.Reset();
                    }
                    pElement = pGraph.Next();
                }

                //遍历模板中的PageLayout所有元素，替换当前PageLayout的所有元素
                IGraphicsContainer pTempGraph = pTempPageLayout as IGraphicsContainer;
                pTempGraph.Reset();
                pElement = pTempGraph.Next();
                IArray pArray = new ArrayClass();
                while (pElement != null)
                {
                    if (pElement is IMapFrame)
                    {
                        IElement pMapFrameElement = pMapFrame as IElement;
                        pMapFrameElement.Geometry = pElement.Geometry;
                    }
                    else
                    {
                        if (pElement is IMapSurroundFrame)
                        {
                            IMapSurroundFrame pTempMapSurroundFrame = pElement as IMapSurroundFrame;
                            pTempMapSurroundFrame.MapFrame = pMapFrame;
                            IMapSurround pTempMapSurround = pTempMapSurroundFrame.MapSurround;
                        }
                        pArray.Add(pElement);
                    }
                    pElement = pTempGraph.Next();
                }

                int pElementCount = pArray.Count;
                for (int i = 0; i < pArray.Count; i++)
                {
                    pGraph.AddElement(pArray.get_Element(pElementCount - 1 - i) as IElement, 0);
                }

                pActiveView.Refresh();
                bSuccess = true;
            }
            catch (Exception ex)
            {

            }
            return bSuccess;
        }
        #endregion  
    }
}

