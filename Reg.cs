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
    public partial class Reg : Form
    {
        public Reg()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" & textBox2.Text != "")
            {
                string BHY = @"Server=.\SQLEXPRESS;uid=sa;Pwd=123;Database=zq3076";
                using (SqlConnection BHYA = new SqlConnection(BHY))
                {

                    BHYA.Open();
                    //由于插入的空数据在SQL里面会以空字符串出现而不是DBNULL所以注释掉一下行
                    //string BHYS = string.Format("insert into Userr values('{0}','{1}','{2}','{3}','{4}','{5}')"
                    //    , textBox1.Text.Trim(), textBox2.Text.Trim(), textBox3.Text.Trim(), comboBox1.Text.Trim()
                    //    , textBox5.Text.Trim(), textBox6.Text.Trim());
                    //以下代码为空项时，SQL里面为NULL而不是空字符串
                    string BHYS = "insert into Userr values(@user,@pwd,@email,@mibao,@daan,@beizhu)";
                    SqlParameter[] ps =
                    {   //##########
                        new SqlParameter("user",textBox1.Text.Trim()),
                        new SqlParameter("pwd",textBox2.Text.Trim()),
                        new SqlParameter("email",string.IsNullOrEmpty(textBox3.Text.Trim())?DBNull.Value:(object)textBox3.Text.Trim()),//##########
                        new SqlParameter("mibao",string.IsNullOrEmpty(comboBox1.Text.Trim())?DBNull.Value:(object)comboBox1.Text.Trim()),//##########
                        new SqlParameter("daan",string.IsNullOrEmpty(textBox5.Text.Trim())?DBNull.Value:(object)textBox5.Text.Trim()),//##########
                        new SqlParameter("beizhu",string.IsNullOrEmpty(textBox6.Text.Trim())?DBNull.Value:(object)textBox6.Text.Trim())//##########
                        //##########
                    };
                    SqlCommand comma2 = new SqlCommand(BHYS, BHYA);
                    //参数加载到parameter
                    comma2.Parameters.AddRange(ps);

                    // SqlDataReader dr = comma2.ExecuteReader();
                    int num1 = comma2.ExecuteNonQuery();
                    MessageBox.Show(num1 == 1 ? "OK" : "NG");
                }
            }
            else
            {
                MessageBox.Show("信息填写有误!");


            }
            }
        }
    }

