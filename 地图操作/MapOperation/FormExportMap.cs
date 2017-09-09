using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;

namespace MapOperation
{
    public partial class FormExportMap : Form
    {
        private string pSavePath = "";
        private IActiveView pActiveView;
        private IGeometry pGeometry = null;
        /// <summary>
        /// 只读属性，地图导出空间图形
        /// </summary>
        public IGeometry GetGeometry
        {
            set
            {
                pGeometry = value;
            }
        }
        private bool bRegion = true;
        /// <summary>
        /// 只读属性，是全域导出还是自由区域导出
        /// </summary>
        public bool IsRegion
        {
            set
            {
                bRegion = value;
            }
        }

        public FormExportMap(AxMapControl mainAxMapControl)
        {
            InitializeComponent();
            pActiveView = mainAxMapControl.ActiveView;
        }

        private void FormExportMap_Load(object sender, EventArgs e)
        {
            InitFormSize();
        }

        private void InitFormSize()
        {
            cboResolution.Text = pActiveView.ScreenDisplay.DisplayTransformation.Resolution.ToString();
            cboResolution.Items.Add(cboResolution.Text);
            if (bRegion)
            {
                IEnvelope pEnvelope = pGeometry.Envelope;
                tagRECT pRECT = new tagRECT();
                pActiveView.ScreenDisplay.DisplayTransformation.TransformRect(pEnvelope, ref pRECT, 9);
                if (cboResolution.Text != "")
                {
                    txtWidth.Text = pRECT.right.ToString();
                    txtHeight.Text = pRECT.bottom.ToString();
                }  
            }
            else
            {
                if (cboResolution.Text != "")
                {
                    txtWidth.Text = pActiveView.ExportFrame.right.ToString();
                    txtHeight.Text = pActiveView.ExportFrame.bottom.ToString();
                }
            }
        }

        private void cboResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            double num = (int)Math.Round(pActiveView.ScreenDisplay.DisplayTransformation.Resolution);
            if (cboResolution.Text == "")
            {
                txtWidth.Text = "";
                txtHeight.Text = "";
                return;
            }
            if (bRegion)
            {
                IEnvelope pEnvelope = pGeometry.Envelope;
                tagRECT pRECT = new tagRECT();
                pActiveView.ScreenDisplay.DisplayTransformation.TransformRect(pEnvelope, ref pRECT, 9);
                if (cboResolution.Text != "")
                {
                    txtWidth.Text = Math.Round((double)(pRECT.right * (double.Parse(cboResolution.Text) / (double)num))).ToString();
                    txtHeight.Text = Math.Round((double)(pRECT.bottom * (double.Parse(cboResolution.Text) / (double)num))).ToString();
                }  
            }
            else
            {
                txtWidth.Text = Math.Round((double)(pActiveView.ExportFrame.right * (double.Parse(cboResolution.Text) / (double)num))).ToString();
                txtHeight.Text = Math.Round((double)(pActiveView.ExportFrame.bottom * (double.Parse(cboResolution.Text) / (double)num))).ToString();
            }
        }

        private void btnExPath_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfdExportMap = new SaveFileDialog();
            sfdExportMap.DefaultExt = "jpg|bmp|gig|tif|png|pdf";
            sfdExportMap.Filter = "JPGE 文件(*.jpg)|*.jpg|BMP 文件(*.bmp)|*.bmp|GIF 文件(*.gif)|*.gif|TIF 文件(*.tif)|*.tif|PNG 文件(*.png)|*.png|PDF 文件(*.pdf)|*.pdf";
            sfdExportMap.OverwritePrompt = true;
            sfdExportMap.Title = "保存为";
            txtExPath.Text = "";
            if (sfdExportMap.ShowDialog() != DialogResult.Cancel)
            {
                pSavePath = sfdExportMap.FileName;
                txtExPath.Text = sfdExportMap.FileName;
            }  
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (txtExPath.Text == "")
            {
                MessageBox.Show("请先确定导出路径!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cboResolution.Text == "")
            {
                if (txtExPath.Text == "")
                {
                    MessageBox.Show("请输入分辨率！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else if (Convert.ToInt16(cboResolution.Text) == 0)
            {
                MessageBox.Show("请正确输入分辨率！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                try
                {
                    int resolution = int.Parse(cboResolution.Text);  //输出图片的分辨率
                    int width = int.Parse(txtWidth.Text);            //输出图片的宽度，以像素为单位
                    int height = int.Parse(txtHeight.Text);          //输出图片的高度，以像素为单位
                    ExportMap.ExportView(pActiveView, pGeometry, resolution, width, height, pSavePath, bRegion);
                    pActiveView.GraphicsContainer.DeleteAllElements();
                    pActiveView.Refresh();

                    MessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch(Exception)
                {
                    MessageBox.Show("导出失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //局部导出时没有导出图像就退出
            pActiveView.GraphicsContainer.DeleteAllElements();
            pActiveView.Refresh();
            Dispose();
        }

        private void FormExportMap_FormClosed(object sender, FormClosedEventArgs e)
        {
            //局部导出时没有导出图像就关闭
            pActiveView.GraphicsContainer.DeleteAllElements();
            pActiveView.Refresh();
            Dispose();
        }
    }
}
