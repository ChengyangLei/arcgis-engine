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
using ESRI.ArcGIS.ArcMap;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Output;
using ESRI.ArcGIS.GlobeCore;
using System.Diagnostics;
namespace example
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void 打开地图文档ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadmapDocument();

        }
        private void loadmapDocument()
        {


            System.Windows.Forms.OpenFileDialog openFileDialog = new OpenFileDialog();
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

        private void button1_Click(object sender, EventArgs e)
        {
            loadmapDocument();
        }


    }
}
