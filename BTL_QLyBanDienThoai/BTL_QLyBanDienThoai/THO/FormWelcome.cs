using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace BTL_QLyBanDienThoai.THO
{
    public partial class FormWelcome : Form
    {
        public FormWelcome(string t)
        {
            InitializeComponent();
            lbTendangnhap.Text = t;
        }

        public string cnt = ConfigurationManager.ConnectionStrings["QLBH"].ConnectionString;

        private void FormWelcome_Load(object sender, EventArgs e)
        {
            string s = "SELECT DISTINCT fullName FROM tblEmployee WHERE username = '" + lbTendangnhap.Text + "'";
            string s1 = "SELECT rolee FROM tblAccount WHERE username ='" + lbTendangnhap.Text + "'";
            lbTendangnhap.Hide();
            using (SqlConnection sqlCon = new SqlConnection(cnt))
            {
                sqlCon.Open();
                using (SqlCommand sqlCmd = new SqlCommand(s, sqlCon))
                {
                    sqlCmd.CommandType = CommandType.Text;
                    using (SqlDataReader sqlRead = sqlCmd.ExecuteReader())
                    {
                        while (sqlRead.Read())
                        {
                            string[] arr = sqlRead.GetString(0).Split(' ');
                            lbTen.Text = arr[arr.Length - 1];
                        }
                    }
                }
            }
            using (SqlConnection sqlCon = new SqlConnection(cnt))
            {
                sqlCon.Open();
                using (SqlCommand sqlCmd = new SqlCommand(s1, sqlCon))
                {
                    sqlCmd.CommandType = CommandType.Text;
                    using (SqlDataReader sqlRead = sqlCmd.ExecuteReader())
                    {
                        while (sqlRead.Read())
                        {
                            lbChucvu.Text = sqlRead.GetString(0);
                        }
                    }
                }
            }
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            FormMDI fmdi = new FormMDI(lbChucvu.Text);
            fmdi.Show();
            Hide();
        }
    }
}