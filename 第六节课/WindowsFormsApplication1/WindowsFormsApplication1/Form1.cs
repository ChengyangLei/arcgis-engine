using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void axMapControl1_OnExtentUpdated(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnExtentUpdatedEvent e)
        {
            //创建鹰眼中线框
            IGraphicsContainer pGraphicsContainer = axMapControl2.Map as IGraphicsContainer;
            IEnvelope pEnv = (IEnvelope)e.newEnvelope;
            IRectangleElement pRectangeEle = new RectangleElementClass();
            IActiveView pActiveView = pGraphicsContainer as IActiveView;
            IElement pEle = pRectangeEle as IElement;
            pEle.Geometry = pEnv;
            //设置线框的边线对象，包括颜色和线宽
            IRgbColor pColor = new RgbColorClass();
            pColor.Red = 255;
            pColor.Green = 0;
            pColor.Blue = 0;
            pColor.Transparency = 255;
            //产生一个线符号对象
            ILineSymbol pOutline = new SimpleLineSymbolClass();
            pOutline.Width = 1;
            pOutline.Color = pColor;
            //设置颜色属性
            pColor = new RgbColorClass();
            pColor.Red = 255;
            pColor.Green = 0;
            pColor.Blue = 0;
            pColor.Transparency = 0;
            //设置填充符号的属性
            IFillSymbol pFillSymbol = new SimpleFillSymbolClass();
            pFillSymbol.Color = pColor;
            pFillSymbol.Outline = pOutline;
            IFillShapeElement pFillShapeEle = pEle as IFillShapeElement;
            pFillShapeEle.Symbol = pFillSymbol;
            //在绘制前，清楚axmapcontrol2中的任何图像
            pGraphicsContainer.DeleteAllElements();
            //鹰眼视图中添加线框
            pGraphicsContainer.AddElement((IElement)pFillShapeEle, 0);
            //刷新地图
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        private void axMapControl2_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent e)
        {
            if (axMapControl2.Map.LayerCount > 0)
            {

                if (e.button == 1)
                {

                    IPoint pPoint = new PointClass();
                    pPoint.PutCoords(e.mapX, e.mapY);
                    axMapControl1.CenterAt(pPoint);
                    axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);

                }
                else if (e.button == 2)
                {
                    IEnvelope pEnv = axMapControl2.TrackRectangle();
                    axMapControl1.Extent = pEnv;
                    axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);

                }

            }
        }

        private void axMapControl2_OnMouseMove(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseMoveEvent e)
        {
            if (e.button == 1)
            {

                IPoint pPoint = new PointClass();
                pPoint.PutCoords(e.mapX, e.mapY);
                axMapControl1.CenterAt(pPoint);
                axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);

            }
        }
        private ILayer GetOverviewLayer(IMap map)
        {

            //获取主视图的第一图层
            ILayer pLayer = map.get_Layer(0);
            //遍历其他图层，并比较视图范围的宽度，返回宽度最大的图层
            ILayer pTempLayer = null;
            for (int i = 1; i < map.LayerCount; i++)
            {
                pTempLayer = map.get_Layer(i);
                if (pLayer.AreaOfInterest.Width < pTempLayer.AreaOfInterest.Width)
                    pLayer = pTempLayer;

            }
            return pLayer;

        }

        private void axMapControl1_OnMapReplaced(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMapReplacedEvent e)
        {
            if (axMapControl1.LayerCount > 0)
            {

                axMapControl2.Map = new MapClass();
                for (int i = 0; i <= axMapControl1.Map.LayerCount - 1; i++)
                {

                    axMapControl2.AddLayer(axMapControl1.get_Layer(i));

                }
                axMapControl2.Extent = axMapControl1.Extent;
                axMapControl2.Refresh();
            }
            }

        private void axMapControl1_OnFullExtentUpdated(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnFullExtentUpdatedEvent e)
        {
            //获取鹰眼图层
            this.axMapControl2.AddLayer(this.GetOverviewLayer(this.axMapControl1.Map));
            //设置mapcontrol显示范围至数据的全局范围
            this.axMapControl2.Extent = this.axMapControl1.FullExtent;
            //刷新鹰眼控件地图
            this.axMapControl2.Refresh();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.axTOCControl1.SetBuddyControl(axMapControl1.Object);
        }
        
    }
}
