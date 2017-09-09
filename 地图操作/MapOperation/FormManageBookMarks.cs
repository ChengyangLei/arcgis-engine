using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;

namespace MapOperation
{
    public partial class FormManageBookMarks : Form
    {
        private IMap _currentMap = null;
        Dictionary<string, ISpatialBookmark> pDictionary = new Dictionary<string, ISpatialBookmark>();
        IMapBookmarks mapBookmarks = null;

        public FormManageBookMarks(IMap pMap)
        {
            InitializeComponent();
            _currentMap = pMap;    //获取当前地图
            InitControl();
        }

        //获取空间书签，对tlstBookMark进行初始化
        private void InitControl()
        {
            mapBookmarks = _currentMap as IMapBookmarks;
            IEnumSpatialBookmark enumSpatialBookmarks = mapBookmarks.Bookmarks;
            enumSpatialBookmarks.Reset();
            ISpatialBookmark pSpatialBookmark = enumSpatialBookmarks.Next();

            string sBookMarkName = string.Empty;
            while (pSpatialBookmark != null)
            {
                sBookMarkName = pSpatialBookmark.Name;
                //增加树节点
                tviewBookMark.Nodes.Add(sBookMarkName);
                //添加到数据字典
                pDictionary.Add(sBookMarkName, pSpatialBookmark);
                pSpatialBookmark = enumSpatialBookmarks.Next();
            }
        }

        //定位
        private void btnLocate_Click(object sender, EventArgs e)
        {
            TreeNode pSelectedNode = tviewBookMark.SelectedNode;
            //获得选中的书签对象
            ISpatialBookmark pSpatialBM = pDictionary[pSelectedNode.Text];
            //缩放到选中书签的视图范围
            pSpatialBM.ZoomTo(_currentMap);
            IActiveView pActiveView = _currentMap as IActiveView;
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }

        //删除书签
        private void btnDelete_Click(object sender, EventArgs e)
        {
            TreeNode pSelectedNode = tviewBookMark.SelectedNode;
            ISpatialBookmark pSpatialBookmark = pDictionary[pSelectedNode.Text];
            //删除选中的书签对象
            mapBookmarks.RemoveBookmark(pSpatialBookmark);
            //删除字典中数据
            pDictionary.Remove(pSelectedNode.Text);
            //删除树节点
            tviewBookMark.Nodes.Remove(pSelectedNode);
            tviewBookMark.Refresh();
        }

        //取消
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        //双击定位书签
        private void tlstBookMark_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //调用定位执行命令
            btnLocate.PerformClick();
        }

        /// <summary>
        /// 返回选中节点所对应的书签
        /// </summary>
        /// <param name="selectedNode">选中节点</param>
        /// <returns></returns>
        //public ISpatialBookmark findSpatialBookmark(TreeNode selectedNode)
        //{
        //    //访问地图中所包含的书签，并获取书名序列
        //    IMapBookmarks mapBookmarks = _currentMap as IMapBookmarks;
        //    IEnumSpatialBookmark enumSpatialBookmark = mapBookmarks.Bookmarks;

        //    //对地图中所包含的书签进行遍历
        //    enumSpatialBookmark.Reset();
        //    ISpatialBookmark spatialBookmark = enumSpatialBookmark.Next();

        //    //获取与列表框所选名称相同的书签
        //    while (spatialBookmark != null)
        //    {
        //        if (selectedNode.Text == spatialBookmark.Name)
        //        {
        //            return spatialBookmark;
        //            break;
        //        }
        //        spatialBookmark = enumSpatialBookmark.Next();
        //    }
        //    return null;
        //}
    }
}
