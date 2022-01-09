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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /*1.创建数据库连接
         * 2.点击连接打开数据库连接
         * 3.切换当前操作数据库
         * 4.创建sql命令
         * 5.点击执行，返回值
         * 6.判断
         * 7.关闭连接
         */

        private void button1_Click(object sender, EventArgs e)
        {

            //1.创建连接通道
            SqlConnection scon = new SqlConnection();

            //2.告知连接通道具体如何连接
            string sconstr = @"Server=.\SQLEXPRESS;uid=sa;Pwd=123;Database=zq3076";

            //3.制定连接通道如何进行连接
            scon.ConnectionString = sconstr;

            //4.因为连接对象只是一个，还需要打开
            scon.Open();

            //5.创建需要执行的sql命令
            string sql = string.Format("insert into Classes values('{0}')", textBox1.Text.Trim());

            //创建执行命令对象，命令传递者
            SqlCommand comm = new SqlCommand();

            //制定命令怎么走
            comm.Connection = scon;

            //指定需要传递的命令语句
            comm.CommandText = sql;

            //让命令对象执行命令,同时接受从服务器返回的值
            //comm.ExecuteNonQuery()执行数据库的增删改，返回受影响的行数
            //comm.ExecuteScalar()执行查询，返回结果集的首行首列
            //comm.ExecuteReader()执行查询，返回DataReader对象
            int num = comm.ExecuteNonQuery();

            //对返回值判断
            if (num == 1)
            {
                MessageBox.Show("OK");

            }
            else
            {
                MessageBox.Show("NG");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //1.创建连接字符串
            string connstr = @"Server=.\SQLEXPRESS;uid=sa;Pwd=123;Database=zq3076";
            //2.创建连接通道
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                //3.打开连接
                conn.Open();
                //4.创建命令语句,可以创建N句
                string sql1 = string.Format("insert into Classes values('{0}')", textBox2.Text.Trim());
                //5.创建"执行"命令的对象,同时接受从服务器返回的值
                SqlCommand command1 = new SqlCommand(sql1, conn);
                //6.命令对象执行方法.命令语句不是由命令对象来进行执行的，
                //因为命令语句是服务器来执行的，服务器会执行完传递过来的所有命令，
                //同时也会返回所有的返回值.但是方法只能返回某一种值.意味着方法的本质作用就是接受
                //某一种用户需要的返回值
                int num1 = command1.ExecuteNonQuery();
                MessageBox.Show(num1 == 1 ? "OK" : "NG");

                //OR
                //string str3 = command1.ExecuteScalar().ToString();
                //MessageBox.Show(str3);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string Q1connstr = @"Server=.\SQLEXPRESS;uid=sa;Pwd=123;Database=zq3076";
            using (SqlConnection Q1conn = new SqlConnection(Q1connstr))
            {

                Q1conn.Open();
                string sql2 = string.Format("insert into Classes values('{0}');select @@identity", textBox3.Text.Trim());
                SqlCommand command2 = new SqlCommand(sql2, Q1conn);
                string sql22 = command2.ExecuteScalar().ToString();
                MessageBox.Show("表示列为:" + sql22);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //好用
            //string BHY = @"Server=.\SQLEXPRESS;uid=sa;Pwd=123;Database=zq3076";
            //using (SqlConnection BHYA = new SqlConnection(BHY))
            //{

            //    BHYA.Open();
            //    string BHYS = "update Classes set ClassID = 'OOSDO' where id= 6";
            //    SqlCommand comma2 = new SqlCommand(BHYS, BHYA);
            //    SqlDataReader dr = comma2.ExecuteReader();
            //}
        }
        private void button5_Click(object sender, EventArgs e)
        {

            string Q2connstr = @"Server=.\SQLEXPRESS;uid=sa;Pwd=123;Database=zq3076";
            using (SqlConnection Q2conn = new SqlConnection(Q2connstr))
            {

                Q2conn.Open();
                //如果有两个查询将触发下面的IF判断是否有第二个结果集
                string sql3 = "select * from Classes;select classid,salary from Teacher";
                //创建命令对象
                SqlCommand command3 = new SqlCommand(sql3, Q2conn);
                //创建读取器对象，可以从服务器每次读取出一行数据
                SqlDataReader reader = command3.ExecuteReader();
                //数据需要循环读取
                while (reader.Read())//先判断下行有无数据，如果有将指针再移动至下一行，还将数据读取到读取器对象数组中
                {
                    //先添加主项，再为主项添加子项
                    ListViewItem lv = new ListViewItem(reader[0].ToString());
                    //添加子项//与AddRange有何区别？
                    lv.SubItems.Add(reader["ClassID"].ToString());
                    //添加到ITEMS集合
                    this.listView1.Items.Add(lv);
                    
                }
                //如果有第二个结果集将数据放置在listview2里面
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                    ListViewItem lvv = new ListViewItem(reader[0].ToString());
                    lvv.SubItems.Add(reader[1].ToString());
                    this.listView2.Items.Add(lvv);
                    }
                }
            }
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            //判断又没有选择一行
            if (this.listView1.SelectedItems.Count == 0)
            {
                return;
            }
            //SubItems项的值会从第0项开始计算，也包含空间的主项值
            textBox5.Text = this.listView1.SelectedItems[0].SubItems[0].Text;
            textBox4.Text = this.listView1.SelectedItems[0].SubItems[1].Text;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count == 0)
            {
                return;

            }
            int idd = int.Parse(this.listView1.SelectedItems[0].SubItems[0].Text);
            string BHY = @"Server=.\SQLEXPRESS;uid=sa;Pwd=123;Database=zq3076";
            using (SqlConnection BHYA = new SqlConnection(BHY))
            {

                BHYA.Open();
                string BHYS = string.Format ("delete from Classes where id = '{0}'",idd);
                SqlCommand comma2 = new SqlCommand(BHYS, BHYA);
                int numm = comma2.ExecuteNonQuery();
                if(numm==1)
                {
                    MessageBox.Show("删除成功!");
                    this.listView1.Items.Remove(this.listView1.SelectedItems[0]);
                    //或者
                    //this.listView1.Items.RemoveAt(this.listView1.SelectedItems[0].Index);
                }
                else
                {
                    MessageBox.Show("删除失败!");
                }
            }
        }

        private void listView2_Click(object sender, EventArgs e)
        {
            //判断又没有选择一行
            if (this.listView2.SelectedItems.Count == 0)
            {
                return;
            }
            //SubItems项的值会从第0项开始计算，也包含空间的主项值
            textBox5.Text = this.listView2.SelectedItems[0].SubItems[0].Text;
            textBox4.Text = this.listView2.SelectedItems[0].SubItems[1].Text;
        }
    }
}