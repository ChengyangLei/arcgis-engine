using System;
using System.Windows.Forms;

namespace MapOperation
{
    public partial class FormBookmark : Form
    {
        private string m_bookmark; //书签名
        private int m_check;       //是否创建书签

        public FormBookmark()
        {
            InitializeComponent();
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            m_bookmark = txtBookmark.Text;
            txtBookmark.Text = "";
            m_check = 1;
            this.Close();
        }

        //取消
        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtBookmark.Text = "";
            m_check = 0;
            this.Close();
        }

        //设置书签名为只读
        public string Bookmark
        {
            get { return m_bookmark; }
        }

        //是否创建书签变量为只读
        public int Check
        {
            get { return m_check; }
        }

        private void FormBookmark_Load(object sender, EventArgs e)
        {
            btnOk.Enabled = false;
        }

        private void txtBookmark_TextChanged(object sender, EventArgs e)
        {
            if (txtBookmark.Text == "")
            {
                btnOk.Enabled = false;
            }
            else
            {
                btnOk.Enabled = true;
            }
        }
    }
}
