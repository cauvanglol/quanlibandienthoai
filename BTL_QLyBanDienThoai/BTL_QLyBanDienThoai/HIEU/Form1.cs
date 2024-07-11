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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        private void LayNV()
        {
            string constr = ConfigurationManager.ConnectionStrings["QLBH"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select * from v_NhanVien", con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    using (SqlDataAdapter adt = new SqlDataAdapter(cmd))
                    {
                        DataTable tbl = new DataTable();
                        adt.Fill(tbl);
                        dvgNV.DataSource = tbl;
                    }
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["QLBH"].ConnectionString;
            int iMaNV = Convert.ToInt32(txtMaNV.Text);
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.CommandText = "procThemNhanVien";

                    cmd.Parameters.AddWithValue("@iMaNhanVien", iMaNV);
                    cmd.Parameters.AddWithValue("@sTenNhanVien", txtTenNV.Text);
                    cmd.Parameters.AddWithValue("@sGioiTinh", txtGioiTinh.Text);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Thêm thành công ", "Thông báo", MessageBoxButtons.OK);
                    }
                    else
                    {
                        MessageBox.Show("Thêm thất bại ", "Thông báo", MessageBoxButtons.OK);
                    }
                    LayNV();
                }
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LayNV();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["QLBH"].ConnectionString;
            int iMaNV = Convert.ToInt32(txtMaNV.Text);
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.CommandText = "procXoaNV";

                    cmd.Parameters.AddWithValue("@iMaNhanVien", iMaNV);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Xóa thành công ", "Thông báo", MessageBoxButtons.OK);
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại ", "Thông báo", MessageBoxButtons.OK);
                    }
                    LayNV();
                }
            }
        }

        private void dvgNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = dvgNV.Rows[e.RowIndex];
            txtMaNV.Text = Convert.ToString(row.Cells["Mã nhân viên"].Value);
            txtTenNV.Text = Convert.ToString(row.Cells["Tên nhân viên"].Value);
            txtGioiTinh.Text = Convert.ToString(row.Cells["Giới tính"].Value);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["QLBH"].ConnectionString;
            int iMaNV = Convert.ToInt32(txtMaNV.Text);
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.CommandText = "procSuaNV";

                    cmd.Parameters.AddWithValue("@iMaNhanVien", iMaNV);
                    cmd.Parameters.AddWithValue("@sTenNhanVien", txtTenNV.Text);
                    cmd.Parameters.AddWithValue("@sGioiTinh", txtGioiTinh.Text);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Sửa thành công ", "Thông báo", MessageBoxButtons.OK);
                    }
                    else
                    {
                        MessageBox.Show("Sửa thất bại ", "Thông báo", MessageBoxButtons.OK);
                    }
                    LayNV();
                }
            }
        }
    }
}
