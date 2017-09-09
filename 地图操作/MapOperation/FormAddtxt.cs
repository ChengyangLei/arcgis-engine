using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using System.IO;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geometry;

namespace MapOperation
{
    public partial class FormAddtxt : Form
    {
        private AxMapControl buddyMap;

        public AxMapControl BuddyMap
        {
            get
            {
                return buddyMap;
            }
            set
            {
                buddyMap = value;
            }
        }

        struct CPoint
        {
            public string Name;
            public double X;
            public double Y;
        }

        List<string> pColumns = new List<string>();

        public FormAddtxt()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog pOFD = new OpenFileDialog();
            pOFD.Multiselect = false;
            pOFD.Title = "打开本地测量坐标文件";
            pOFD.InitialDirectory = Directory.GetCurrentDirectory();
            pOFD.Filter = "测量坐标文件（*.txt）|*.txt";
            if (pOFD.ShowDialog() == DialogResult.OK)
            {
                txtSource.Text = pOFD.FileName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Shape文件（*.shp）|*.shp";
            if (File.Exists(txtSource.Text)) 
            {
                saveFileDialog.FileName = System.IO.Path.GetFileNameWithoutExtension(txtSource.Text);
            }  
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtSave.Text = saveFileDialog.FileName;
            }
        }

        private List<CPoint> GetPoints(string surveyDataFullName)
        {
            try
            {
                List<CPoint> pList = new List<CPoint>();
                char[] charArray = new char[] { ',', ' ', '\t' };   //常用的分隔符为逗号、空格、制位符
                //文本信息读取
                FileStream fs = new FileStream(surveyDataFullName, FileMode.Open);
                StreamReader sr = new StreamReader(fs, Encoding.Default);
                string strLine = sr.ReadLine();
                if (strLine != null)
                {
                    string[] strArray = strLine.Split(charArray);
                    if (strArray.Length > 0)
                    {
                        for (int i = 0; i < strArray.Length; i++)
                        {
                            pColumns.Add(strArray[i]);
                        }
                    }

                    while ((strLine= sr.ReadLine())!=null)
                    {
                        //点信息的读取
                        strArray = strLine.Split(charArray);
                        CPoint pCPoint = new CPoint();
                        pCPoint.Name = strArray[0].Trim();
                        pCPoint.X = Convert.ToDouble(strArray[1]);
                        pCPoint.Y = Convert.ToDouble(strArray[2]);
                    
                        pList.Add(pCPoint);
                    }
                }
                else
                {
                    return null;
                }
                sr.Close();
                return pList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private IFeatureLayer CreateShpFromPoints(List<CPoint> cPointList, string filePath)
        {
            int index = filePath.LastIndexOf('\\');
            string folder = filePath.Substring(0, index);
            string shapeName = filePath.Substring(index + 1);  
            IWorkspaceFactory pWSF = new ShapefileWorkspaceFactoryClass();
            IFeatureWorkspace pFWS = (IFeatureWorkspace)pWSF.OpenFromFile(folder, 0);

            IFields pFields = new FieldsClass();
            IFieldsEdit pFieldsEdit;
            pFieldsEdit = (IFieldsEdit)pFields;

            IField pField = new FieldClass();
            IFieldEdit pFieldEdit = (IFieldEdit)pField;
            pFieldEdit.Name_2 = "Shape";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            IGeometryDef pGeometryDef = new GeometryDefClass();
            IGeometryDefEdit pGDefEdit = (IGeometryDefEdit)pGeometryDef;
            pGDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
            //定义坐标系
            ISpatialReferenceFactory pSRF = new SpatialReferenceEnvironmentClass();
            ISpatialReference pSpatialReference = pSRF.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_Beijing1954);
            pGDefEdit.SpatialReference_2 = pSpatialReference;

            pFieldEdit.GeometryDef_2 = pGeometryDef;
            pFieldsEdit.AddField(pField);
           
            IFeatureClass pFeatureClass;
            pFeatureClass = pFWS.CreateFeatureClass(shapeName, pFields, null, null, esriFeatureType.esriFTSimple, "Shape", "");

            IPoint pPoint = new PointClass();
            for (int j = 0; j < cPointList.Count; j++)
            {
                pPoint.X = cPointList[j].X;
                pPoint.Y = cPointList[j].Y;

                IFeature pFeature = pFeatureClass.CreateFeature();
                pFeature.Shape = pPoint;
                pFeature.Store();
            }
            
            IFeatureLayer pFeatureLayer = new FeatureLayerClass();
            pFeatureLayer.Name = shapeName;
            pFeatureLayer.FeatureClass = pFeatureClass;
            return pFeatureLayer;
        }

        // 验证空间是否有效
        private bool ValidateTxtbox()
        {
            if (txtSource.Text == "" || !File.Exists(txtSource.Text))
            {
                MessageBox.Show("测量数据无效，请重新选择！", "提示", MessageBoxButtons.OK);
                return false;
            }
            if (txtSave.Text == "" || System.IO.Path.GetExtension(txtSave.Text).ToLower() != ".shp")
            {
                MessageBox.Show("保存路径无效，请重新选择！", "提示", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (ValidateTxtbox())
            {
                List<CPoint> pCPointList = GetPoints(txtSource.Text);
                if (pCPointList == null)
                {
                    MessageBox.Show("所选文件为空，请重新选择！");
                }
                else
                {
                    IFeatureLayer pFeatureLayer = CreateShpFromPoints(pCPointList, txtSave.Text);
                    buddyMap.Map.AddLayer(pFeatureLayer);
                    this.Close();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }       
    }
}
