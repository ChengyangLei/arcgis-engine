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

using ESRI.ArcGIS.esriSystem;

namespace example
{
    public partial class Form1 : Form
    {
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
    }
}
