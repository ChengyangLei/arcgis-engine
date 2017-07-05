
using System.Windows.Forms;

namespace MyGis实习二
{
    public partial class Form2 : Form
    {        //声明运行结果关闭事件
        public delegate void FormClosedEventHandler();
        public event FormClosedEventHandler frmClosed = null;

        public Form2()
        {
            InitializeComponent();
        }
        //窗口关闭时引发委托事件


        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (frmClosed != null)
            {
                frmClosed();
            }

        }




    }
}
