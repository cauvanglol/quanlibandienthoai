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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        string cnt = ConfigurationManager.ConnectionStrings["QLBH"].ConnectionString;

        private void FormDangnhap_Load(object sender, EventArgs e)
        {
            picAnMK.Hide();
        }

        private void txtTendangnhap_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTendangnhap.Text))
            {
                e.Cancel = true;
                txtTendangnhap.Focus();
                errorProvider1.SetError(txtTendangnhap, "Tên đăng nhập không được để trống");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void picAnMK_Click_1(object sender, EventArgs e)
        {
            picAnMK.Hide();
            picHienMK.Show();
            txtMatkhau.PasswordChar = '*';
        }

        private void picHienMK_Click_1(object sender, EventArgs e)
        {
            picAnMK.Show();
            picHienMK.Hide();
            txtMatkhau.PasswordChar = '\0';
        }

        private void pictureBox4_Click_1(object sender, EventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(cnt))
            {
                string sqlStr = "SELECT * FROM tblAccount";
                sqlCon.Open();
                using (SqlCommand sqlCmd = new SqlCommand(sqlStr, sqlCon))
                {
                    sqlCmd.CommandType = CommandType.Text;
                    using (SqlDataReader sqlRead = sqlCmd.ExecuteReader())
                    {
                        bool flag = true;
                        while (sqlRead.Read())
                        {
                            string username = sqlRead.GetString(0);
                            string pass = sqlRead.GetString(1);
                            string role = sqlRead.GetString(2);

                            if (txtTendangnhap.Text == username)
                            {
                                flag = false;
                                if (txtMatkhau.Text == pass)
                                {
                                    FormWelcome fw = new FormWelcome(txtTendangnhap.Text);
                                    fw.Show();
                                    Hide();
                                }
                                else
                                {
                                    MessageBox.Show("Sai mật khẩu. Vui lòng kiểm tra lại");
                                }
                                break;
                            }
                        }
                        if (flag == true)
                        {
                            MessageBox.Show("Tài khoản không tồn tại. Vui lòng kiểm tra lại");
                        }
                    }
                }
                sqlCon.Close();
            }
        }
    }
}