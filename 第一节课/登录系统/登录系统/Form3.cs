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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        public bool bbool;

       

        //private void buttonX1_Click(object sender, EventArgs e)
        //{

        //}

        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    if (circularProgress1.Value < circularProgress1.Maximum)
        //    {
        //        circularProgress1.Value++;

        //    }
        //    else {
        //        timer1.Enabled = false;
        //    }
        //}

        private void button2_Click(object sender, EventArgs e)
        {
            if (bbool == false)
            {
                //circularProgress1.Value = 0;
                //circularProgress1.Minimum = 0;
                //circularProgress1.Maximum = 10;
                timer1.Enabled = true;
                string conn = "server=.;uid=sa;pwd=12345678;database=fhjc;";
                SqlConnection myconn = new SqlConnection(conn);
                myconn.Open();
                string mysql = "insert into tblUser(用户名,密码) values ('" + textBox1.Text + "'," + "'" + textBox2.Text + "') ";
                SqlCommand com2 = new SqlCommand();
                com2.CommandText = mysql;//commandtext属性                 
                com2.Connection = myconn;//connection属性                   
                com2.ExecuteNonQuery();
                myconn.Close();
                MessageBox.Show("注册成功！点击确定，返回登录界面。", "提示");
                this.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string User, Pwd;
            string con = "server=.;uid=sa;pwd=12345678;database=fhjc;";
            SqlConnection mycon = new SqlConnection(con);
            mycon.Open();
            string cmd = "select * from tblUser";
            SqlCommand com = new SqlCommand();
            com.CommandText = cmd;//commandtext属性             
            com.Connection = mycon;//connection属性             
            com.ExecuteNonQuery();
            SqlDataReader reader = com.ExecuteReader();
            while (reader.Read())//从数据库读取用户信息             
            {
                User = reader["用户名"].ToString();
                Pwd = reader["密码"].ToString();
                if (textBox1.Text.Trim() == reader["用户名"].ToString().Trim())
                {
                    labelX3.Text = "用户存在";
                    bbool = true;
                    return;
                }
                else if (textBox1.Text.Trim() != reader["用户名"].ToString().Trim())
                {
                    labelX3.Text = "恭喜你，该用户名可以使用。";
                    bbool = false;
                }
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
