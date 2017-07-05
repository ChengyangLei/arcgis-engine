using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace 登录系统
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("请输入有效的用户名或密码！", "提示");
            }
            else
            {
                string ConnectionString = "server=.;uid=sa;pwd=12345678;database=fhjc;";
                //创建Connection连接对象
                DataTable dt = new DataTable();
                SqlConnection myConnection = new SqlConnection(ConnectionString);
                try
                {
                    myConnection = new SqlConnection(ConnectionString);
                    myConnection.Open();
                    //根据输入的用户名和密码到数据库查询是否有符合账号和密码的记录


                    string mysql = "select * from tblUser where 用户名 ='" + textBox1.Text + "' and  密码='" + textBox2.Text + "'";

                    SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter(mysql, myConnection);

                    dt = new DataTable();
                    objSqlDataAdapter.Fill(dt);

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
                finally
                {
                    if (myConnection.State == ConnectionState.Open)
                    {
                        myConnection.Close();

                    }
                }

                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("登陆成功");
                    this.Visible = false;
                    try
                    {
                        MainForm ss = new MainForm();
                        ss.Show();
                    }
                    catch (Exception ex)
                    {

                    }
                }

                else
                {
                    MessageBox.Show("输入登录信息不正确，请从新输入！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.textBox1.Text = "";            //清空文本框
                    this.textBox2.Text = "";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 ss = new Form1(); 
            ss.Close(); 
            Form3 aa = new Form3(); 
            aa.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            skinEngine1.SkinFile = System.Environment.CurrentDirectory + "\\Skins\\mp10.ssk";  //选择皮肤文件
        }
    }
}

