using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace BTL_QLyBanDienThoai.HIEU
{
    public partial class ThongTinTK : Form
    {
        public ThongTinTK(string v)
        {
            InitializeComponent();
            lbMaNV.Text = v;
        }

        private void ThongTinTK_Load(object sender, EventArgs e)
        {
            string contr = ConfigurationManager.ConnectionStrings["QLBH"].ConnectionString;
            string select = "SELECT [Họ tên], [Ngày sinh], [Địa chỉ], [SDT] FROM v_NV WHERE [Tài khoản] = '" + lbMaNV.Text + "'";
            using (SqlConnection cnn = new SqlConnection(contr))
            {
                using (SqlCommand cmd = new SqlCommand(select, cnn))
                {
                    cnn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtHoTen.Text = reader["Họ tên"].ToString();
                        txtNgaySinh.Text = reader["Ngày sinh"].ToString();
                        txtDiaChi.Text = reader["Địa chỉ"].ToString();
                        txtSDT.Text = reader["SDT"].ToString();
                    }
                    cnn.Close();
                }
            }
        }
    }
}
