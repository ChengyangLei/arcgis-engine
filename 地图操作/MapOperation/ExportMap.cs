using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Output;

namespace MapOperation
{
    public class ExportMap
    {
        public static void ExportView(IActiveView view, IGeometry pGeo, int OutputResolution,int Width, int Height, string ExpPath, bool bRegion)
        {
            IExport pExport = null;
            tagRECT exportRect = new tagRECT();
            IEnvelope pEnvelope = pGeo.Envelope;
            string sType = System.IO.Path.GetExtension(ExpPath);
            switch (sType)
            {
                case ".jpg":
                    pExport = new ExportJPEGClass();
                    break;
                case ".bmp":
                    pExport = new ExportBMPClass();
                    break;
                case ".gif":
                    pExport = new ExportGIFClass();
                    break;
                case ".tif":
                    pExport = new ExportTIFFClass();
                    break;
                case ".png":
                    pExport = new ExportPNGClass();
                    break;
                case ".pdf":
                    pExport = new ExportPDFClass();
                    break;
                default:
                    MessageBox.Show("没有输出格式，默认到JPEG格式");
                    pExport = new ExportJPEGClass();
                    break;
            } 
            pExport.ExportFileName = ExpPath;

            exportRect.left = 0; exportRect.top = 0;
            exportRect.right = Width;
            exportRect.bottom = Height;
            if (bRegion)
            {
                view.GraphicsContainer.DeleteAllElements();
                view.Refresh();
            }
            IEnvelope envelope = new EnvelopeClass();
            envelope.PutCoords((double)exportRect.left, (double)exportRect.top, (double)exportRect.right, (double)exportRect.bottom);
            pExport.PixelBounds = envelope;
            view.Output(pExport.StartExporting(), OutputResolution, ref exportRect, pEnvelope, null);
            pExport.FinishExporting();
            pExport.Cleanup();
        }

        /// <summary>
        /// 全域导出
        /// </summary>
        /// <param name="OutputResolution">输出分辨率</param>
        /// <param name="ExpPath">输出路径</param>
        /// <param name="view">视图</param>
        public static void ExportActiveView(int OutputResolution, string ExpPath, IActiveView view)
        {
            IExport pExport = null;
            tagRECT exportRect;
            IEnvelope envelope2 = view.Extent;
            int num = (int)Math.Round(view.ScreenDisplay.DisplayTransformation.Resolution);
            string sType = System.IO.Path.GetExtension(ExpPath);
            switch (sType)
            {
                case ".jpg":
                    pExport = new ExportJPEGClass();
                    break;
                case ".bmp":
                    pExport = new ExportBMPClass();
                    break;
                case ".gif":
                    pExport = new ExportGIFClass();
                    break;
                case ".tif":
                    pExport = new ExportTIFFClass();
                    break;
                case ".png":
                    pExport = new ExportPNGClass();
                    break;
                case ".pdf":
                    pExport = new ExportPDFClass();
                    break;
                default:
                    MessageBox.Show("没有输出格式，默认到JPEG格式");
                    pExport = new ExportJPEGClass();
                    break;
            }
            pExport.ExportFileName = ExpPath;
            exportRect.left = 0; exportRect.top = 0;
            exportRect.right = (int)Math.Round((double)(view.ExportFrame.right * (((double)OutputResolution) / ((double)num))));
            exportRect.bottom = (int)Math.Round((double)(view.ExportFrame.bottom * (((double)OutputResolution) / ((double)num))));
            IEnvelope envelope = new EnvelopeClass();
            envelope.PutCoords((double)exportRect.left, (double)exportRect.top, (double)exportRect.right, (double)exportRect.bottom);
            pExport.PixelBounds = envelope;
            view.Output(pExport.StartExporting(), OutputResolution, ref exportRect, envelope2, null);
            pExport.FinishExporting();
            pExport.Cleanup();
        }

        /// <summary>
        /// 区域导出
        /// </summary>
        /// <param name="pGeo">几何图形</param>
        /// <param name="OutputResolution">输出分辨率</param>
        /// <param name="ExpPath">输出路径</param>
        /// <param name="view">视图</param>
        public static void ExportRegion(IGeometry pGeo, int OutputResolution, string ExpPath, IActiveView view)
        {
            IExport export = null;
            IWorldFileSettings settings = null;
            IEnvelope envelope2 = pGeo.Envelope;
            string str = ExpPath.Substring(ExpPath.Length - 3, 3).ToUpper();
            switch (str)
            {
                case "JPG":
                    settings = new ExportJPEGClass();
                    export = new ExportJPEGClass();
                    settings = export as IWorldFileSettings; ;
                    settings.MapExtent = envelope2;
                    settings.OutputWorldFile = false;
                    break;
                case "BMP":
                    settings = new ExportBMPClass();
                    export = new ExportBMPClass();
                    settings = export as IWorldFileSettings; ;
                    settings.MapExtent = envelope2;
                    settings.OutputWorldFile = false;
                    break;
                case "TIF":
                    settings = new ExportTIFFClass();
                    export = new ExportTIFFClass();
                    settings = export as IWorldFileSettings; ;
                    settings.MapExtent = envelope2;
                    settings.OutputWorldFile = false;
                    break;
                case "PNG":
                    settings = new ExportPNGClass();
                    export = new ExportPNGClass();
                    settings = export as IWorldFileSettings;
                    settings.MapExtent = envelope2;
                    settings.OutputWorldFile = false;
                    break;
                default:
                    break;
            }
            if (settings == null) return;
            export.ExportFileName = ExpPath;
            int num = (int)Math.Round(view.ScreenDisplay.DisplayTransformation.Resolution);
            tagRECT grect2 = new tagRECT();
            IEnvelope envelope3 = new EnvelopeClass();
            view.ScreenDisplay.DisplayTransformation.TransformRect(envelope2, ref grect2, 9);
            grect2.left = 0;
            grect2.top = 0;
            grect2.right = (int)Math.Round((double)((grect2.right - grect2.left) * (((double)OutputResolution) / ((double)num))));
            grect2.bottom = (int)Math.Round((double)((grect2.bottom - grect2.top) * (((double)OutputResolution) / ((double)num))));
            envelope3.PutCoords((double)grect2.left, (double)grect2.top, (double)grect2.right, (double)grect2.bottom);
            export.PixelBounds = envelope3;

            view.GraphicsContainer.DeleteAllElements();
            view.Output(export.StartExporting(), OutputResolution, ref grect2, envelope2, null);
            export.FinishExporting();
            export.Cleanup();
            AddElement(pGeo, view);
        }

        /// <summary>
        /// 视图窗口绘制几何图形元素
        /// </summary>
        /// <param name="pGeometry">几何图形</param>
        /// <param name="activeView">视图</param>
        public static void AddElement(IGeometry pGeometry, IActiveView activeView)
        {
            IRgbColor fillColor = GetRgbColor(204, 175, 235);
            IRgbColor lineColor = GetRgbColor(0, 0, 0);
            IElement pEle = CreateElement(pGeometry, lineColor, fillColor);
            IGraphicsContainer pGC = activeView.GraphicsContainer;
            if (pGC != null)
            {
                pGC.AddElement(pEle, 0);
                activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, pEle, null);
            }
        }

        /// <summary>
        /// 获取RGB颜色
        /// </summary>
        /// <param name="intR">红</param>
        /// <param name="intG">绿</param>
        /// <param name="intB">蓝</param>
        /// <returns></returns>
        private static IRgbColor GetRgbColor(int intR, int intG, int intB)
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
        /// 创建图形元素
        /// </summary>
        /// <param name="pGeometry">几何图形</param>
        /// <param name="lineColor">边框颜色</param>
        /// <param name="fillColor">填充颜色</param>
        /// <returns></returns>
        private static IElement CreateElement(IGeometry pGeometry, IRgbColor lineColor, IRgbColor fillColor)
        {
            if (pGeometry == null || lineColor == null || fillColor == null)
            {
                return null;
            }
            IElement pElem = null;
            try
            {
                if (pGeometry is IEnvelope)
                    pElem = new RectangleElementClass();
                else if (pGeometry is IPolygon)
                    pElem = new PolygonElementClass();
                else if (pGeometry is ICircularArc)
                {
                    ISegment pSegCircle = pGeometry as ISegment;//QI
                    ISegmentCollection pSegColl = new PolygonClass();
                    object o = Type.Missing;
                    pSegColl.AddSegment(pSegCircle, ref o, ref o);
                    IPolygon pPolygon = pSegColl as IPolygon;
                    pGeometry = pPolygon as IGeometry;
                    pElem = new CircleElementClass();
                }
                else if (pGeometry is IPolyline)
                    pElem = new LineElementClass();

                if (pElem == null)
                    return null;
                pElem.Geometry = pGeometry;
                IFillShapeElement pFElem = pElem as IFillShapeElement;
                ISimpleFillSymbol pSymbol = new SimpleFillSymbolClass();
                pSymbol.Color = fillColor;
                pSymbol.Outline.Color = lineColor;
                pSymbol.Style = esriSimpleFillStyle.esriSFSCross;
                if (pSymbol == null)
                {
                    return null;
                }
                pFElem.Symbol = pSymbol;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return pElem;
        }
    }
}
