using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQL_Server_Test
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }



        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //this.Hide();
            Forgot_Psw fp = new Forgot_Psw();
            fp.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Reg rg = new Reg();
            rg.Show();
        }

        private void button1_Click(object sender, EventArgs e)   //登陆
        {
            if (textBox1.Text != "" & textBox2.Text != "")
            {
                //先判断数据库中有没有当前输入的用户名，有的话进行密码检测，有BUG


                //代码中注释第二种方式参数化查询登陆


                string BHY = @"Server=.\SQLEXPRESS;uid=sa;Pwd=123;Database=zq3076";
                using (SqlConnection BHYA = new SqlConnection(BHY))
                {

                    BHYA.Open();
                    string BHYS = string.Format("select Name from Userr where name = '{0}'", textBox1.Text.Trim());
                    //1.定义参数占位@参数相当于在sql中创建一个变量，所以也不需要实用''包含
                    //string BHYS = "select count(*) from Userr where name=@name and Password=@pwd";
                    //2.在C#中创建对应的参数对象，参数名称不区分大小写
                    //SqlParameter n = new SqlParameter("@name",textBox1.Text.Trim());
                    //SqlParameter p = new SqlParameter("pwd",textBox2.Text.Trim());
                    SqlCommand comma2 = new SqlCommand(BHYS, BHYA);
                    //3.还需要将创建好的参数传递的服务器让其使用，所以让comm对象将参数一起传递过去
                    //comma2.Parameters.Add(n);
                    //comma2.Parameters.Add(p);
                    if (comma2.ExecuteScalar() == null)//////
                    {
                        MessageBox.Show("用户名或者密码错误!");
                    }
                    else
                    {
                        string uss = comma2.ExecuteScalar().ToString();
                        //int nu =(int)comma2.ExecuteScalar() //判断当前数值大于0即可

                        if (uss == textBox1.Text)
                        { // 
                            string BHYY = @"Server=.\SQLEXPRESS;uid=sa;Pwd=123;Database=zq3076";
                            using (SqlConnection BHYAA = new SqlConnection(BHYY))
                            {
                                BHYAA.Open();
                                string BHYSS = string.Format("select [Password] from Userr where Name = '{0}' and [Password] = '{1}'", textBox1.Text.Trim(), textBox2.Text.Trim());
                                SqlCommand comma22 = new SqlCommand(BHYSS, BHYAA);
                                if (comma22.ExecuteScalar() == null)
                                {
                                    MessageBox.Show("用户名或者密码错误!");
                                }
                                else
                                {
                                    string usss = comma22.ExecuteScalar().ToString();
                                    if (usss == textBox2.Text)
                                    {
                                        textBox2.Text = "";
                                        Form1 f1 = new Form1();
                                        f1.Show();
                                    }
                                    else
                                    {
                                        MessageBox.Show("用户名或者密码错误!");
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("用户名或者密码错误!");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("请输入用户名或密码!");
            }
        }
    }
}

