using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Output;
using ESRI.ArcGIS.SystemUI;
namespace lesson3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addLayerFile();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            addShapeFile();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            deleteLayer();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            moveLayer();
        }
    }

//添加图层文件
     private void addLayerFile()

{
    System.Windows.Forms.OpenFileDialog openfileDialog;
   openfileDialog=new OpenFileDialog();
    openFileDialog.Title = "打开图层文件"
    openFileDialog.Filter = "map documents(*.lyr)| * .lyr";
    openFileDialog.showDialog();
    string filePath = openFileDialog.FileName;
    try 
    {
    axMapCntrol1.AddlayerFromFile(filePath);
    }
    catch (Exception e)
   {
    MessageBox.Show("添加图层失败" + e.ToString());
   }
}
//添加 Shape 文件
private void addShapeFile()
{
  System.Windows.Forms.OpenFileDialog openfileDialog;
  openFileDialog = new OpenFileDialog();
  openFileDialog.Title = "打开图层文件"
  openFileDialog.Filter = "map documents(*.shp)| * .shp";
  openFileDialog.showDialog();
  //FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
  String path=openFileDialog.FileName.Substring(0,openFileDialog.FileName.Length
  file Info.Name.Length);  
  try 
  {
   //axMapCntrol1.AddShapeFromFile (Path,fileInfo.Name );
  }
  catch (Exception e)
  {
   MessageBox.Show("添加图层失败!!!! " + e.ToString());
  }

//删除图层
private void deleteLayer()
{
  try
 {
     //删除地图中所有的图层
     for (int i = axMapControl1.LayerCount-1;i>=0;i--)
     {axMapControl1.DeteLayer(i);
  }
catch (Exception e)
  {
   MessageBox.Show("删除图层失败" + e.ToString());                   
  }
}
//移除图层
private void moveLayer()
   {
    if (axMapControl1.LyerCount > 0)
    {
       try
       {
          //将最下层图层文件移动到最上层
          axMapControl1.MoveLayerTo(axMapControl1.LayerCount - 1,0);
       }
       catch (Exception e)
       {
           MessageBox.Show("移除图层失败!!!" + e.Tostring());
       }
}
}
}
    