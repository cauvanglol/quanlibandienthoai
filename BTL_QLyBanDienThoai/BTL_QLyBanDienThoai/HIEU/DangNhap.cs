using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace BTL_QLyBanDienThoai.HIEU
{
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
        }

        private void btnDN_Click(object sender, EventArgs e)
        {
            if (TKMK() == true)
            {
                QLDanhMuc form1 = new QLDanhMuc(txtTenDangNhap.Text);
                form1.ShowDialog();
            }
            else
            {
                MessageBox.Show("Sai thông tin đăng nhập", "Thông bảo", MessageBoxButtons.OK);
                txtTenDangNhap.Text = "";
                txtMK.Text = "";
            }
        }
        private bool TKMK()
        {
            string constr = ConfigurationManager.ConnectionStrings["QLBH"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlDataAdapter mydata = new SqlDataAdapter("select * from tblAccount where username= '" + txtTenDangNhap.Text + "' and passwordd = '" + txtMK.Text + "'", cnn))
                {
                    using (DataTable tbl = new DataTable())
                    {
                        mydata.Fill(tbl);
                        if (tbl.Rows.Count > 0)
                        {
                            return true;
                        }
                        else
                            return false;
                    }
                }
            }
        }
        private void txtMK_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtTenDangNhap_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                txtMK.UseSystemPasswordChar = false;
            }
            else
            {
                txtMK.UseSystemPasswordChar = true;
            }
        }
    }
}
