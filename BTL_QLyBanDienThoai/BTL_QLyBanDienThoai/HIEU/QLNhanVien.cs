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
using System.Globalization;

namespace BTL_QLyBanDienThoai.HIEU
{
    public partial class QLNhanVien : Form
    {
        public QLNhanVien()
        {
            InitializeComponent();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private bool KiemTraUserName()
        {
            string constr = ConfigurationManager.ConnectionStrings["QLBH"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlDataAdapter mydata = new SqlDataAdapter("SELECT * FROM tblEmployee WHERE username = '" + txtTenDangNhap + "'", cnn))
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

        private void btnThem_Click(object sender, EventArgs e)
        {
            string g;
            DateTime ngaysinh = DateTime.ParseExact(txtNgaySinh.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (radioButton1.Checked == true)
            {
                g = "Nam";
            }
            else
            {
                g = "Nữ";
            }
            if (KiemTraUserName() == true)
            {
                MessageBox.Show("Tài khoản đã được sử dụng", "Thông báo", MessageBoxButtons.OK);
            }
            else
            {
                string constr = ConfigurationManager.ConnectionStrings["QLBH"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.CommandText = "procThemNhanVien";

                        cmd.Parameters.AddWithValue("@username", txtTenDangNhap.Text);
                        cmd.Parameters.AddWithValue("@fullName", txtTenNV.Text);
                        cmd.Parameters.AddWithValue("@gender", g);
                        cmd.Parameters.AddWithValue("@birthday", ngaysinh);
                        cmd.Parameters.AddWithValue("@adress", txtDiaChi.Text);
                        cmd.Parameters.AddWithValue("@phoneNumber", txtSDT.Text);
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
        }
        private void LayNV()
        {
            string constr = ConfigurationManager.ConnectionStrings["QLBH"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select * from v_NV", con))
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

        private void Form3_Load(object sender, EventArgs e)
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

                    cmd.Parameters.AddWithValue("@idEmployee", iMaNV);
                    con.Open();
                    DialogResult dg = MessageBox.Show("Bạn có chắc chắn muốn xóa", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dg == DialogResult.Yes)
                    {
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Xóa thành công ", "Thông báo", MessageBoxButtons.OK);
                        }
                        else
                        {
                            MessageBox.Show("Xóa thất bại ", "Thông báo", MessageBoxButtons.OK);
                        }
                    }
                    LayNV();
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["QLBH"].ConnectionString;
            int iMaNV = Convert.ToInt32(txtMaNV.Text);
            string g;
            if (radioButton1.Checked == true)
            {
                g = "Nam";
            }
            else
            {
                g = "Nữ";
            }
            DateTime ngaysinh = DateTime.ParseExact(txtNgaySinh.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.CommandText = "procSuaNV";

                    cmd.Parameters.AddWithValue("@idEmployee", iMaNV);
                    cmd.Parameters.AddWithValue("@fullName", txtTenNV.Text);
                    cmd.Parameters.AddWithValue("@gender", g);
                    cmd.Parameters.AddWithValue("@birthday", ngaysinh);
                    cmd.Parameters.AddWithValue("@adress", txtDiaChi.Text);
                    cmd.Parameters.AddWithValue("@phoneNumber", txtSDT.Text);
                    con.Open();
                    DialogResult dg = MessageBox.Show("Bạn có chắc chắn muốn sửa", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dg == DialogResult.Yes)
                    {
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Sửa thành công ", "Thông báo", MessageBoxButtons.OK);
                        }
                        else
                        {
                            MessageBox.Show("Sửa thất bại ", "Thông báo", MessageBoxButtons.OK);
                        }
                    }
                    LayNV();
                }
            }
        }

        private void dvgNV_Click(object sender, EventArgs e)
        {

        }

        private void dvgNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = dvgNV.Rows[e.RowIndex];
            txtMaNV.Text = Convert.ToString(row.Cells["Mã nhân viên"].Value);
            txtTenDangNhap.Text = Convert.ToString(row.Cells["Tài khoản"].Value);
            txtTenNV.Text = Convert.ToString(row.Cells["Họ tên"].Value);
            txtNgaySinh.Text = Convert.ToString(row.Cells["Ngày sinh"].Value);
            txtDiaChi.Text = Convert.ToString(row.Cells["Địa chỉ"].Value);
            txtSDT.Text = Convert.ToString(row.Cells["SDT"].Value);

            if (row.Cells["Giới tính"].Value.ToString() == "Nam")
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton1.Checked = false;
            }
            if (row.Cells["Giới tính"].Value.ToString() == "Nữ")
            {
                radioButton2.Checked = true;
            }
            else
            {
                radioButton2.Checked = false;
            }

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemNV();
        }
        private void TimKiemNV()
        {
            string constr = ConfigurationManager.ConnectionStrings["QLBH"].ConnectionString;
            string g;
            if (radioButton1.Checked == true)
            {
                g = "Nam";
            }
            else
            {
                g = "Nữ";
            }
            using (SqlConnection connection = new SqlConnection(constr))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = "select * from v_NV where 1=1";
                    if (!String.IsNullOrEmpty(txtMaNV.Text))
                    {
                        command.CommandText += " and [Mã nhân viên] = @iMaNV";
                        command.Parameters.AddWithValue("@iMaNV", txtMaNV.Text);
                    }
                    if (!String.IsNullOrEmpty(txtTenDangNhap.Text))
                    {
                        command.CommandText += " and [Tài khoản] = @username";
                        command.Parameters.AddWithValue("@username", txtTenDangNhap.Text);
                    }
                    if (!String.IsNullOrEmpty(txtTenNV.Text))
                    {
                        command.CommandText += " and [Họ tên] like @sTenNV";
                        command.Parameters.AddWithValue("@sTenNV", "%" + txtTenNV.Text + "%");
                    }
                    if (radioButton1.Checked == true || radioButton2.Checked == true)
                    {
                        command.CommandText += " and [Giới tính] = @sGioiTinh";
                        command.Parameters.AddWithValue("@sGioiTinh", g);
                    }
                    if (checkBox1.Checked == true)
                    {
                        command.CommandText += " and [Ngày sinh] = @NgaySinh";
                        command.Parameters.AddWithValue("@NgaySinh", txtNgaySinh.Text);
                    }
                    if (!String.IsNullOrEmpty(txtDiaChi.Text))
                    {
                        command.CommandText += " and [Địa chỉ] like @DiaChi";
                        command.Parameters.AddWithValue("@DiaChi", "%" + txtDiaChi.Text + "%");
                    }
                    if (!String.IsNullOrEmpty(txtSDT.Text))
                    {
                        command.CommandText += " and [SDT] like @SDT";
                        command.Parameters.AddWithValue("@SDT", "%" + txtSDT.Text + "%");
                    }
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        DataTable tbl = new DataTable();
                        dataAdapter.Fill(tbl);
                        dvgNV.DataSource = tbl;
                    }
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtMaNV.Text = "";
            txtTenDangNhap.Text = "";
            txtTenNV.Text = "";
            txtNgaySinh.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            LayNV();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Form3 form3 = new Form3();
            //form3.ShowDialog();
        }
        private bool KiemTraCCD()
        {
            //string constr = ConfigurationManager.ConnectionStrings["QLBH"].ConnectionString;
            //using (SqlConnection cnn = new SqlConnection(constr))
            //{
            //    using (SqlDataAdapter mydata = new SqlDataAdapter("SELECT * from tblBill,tblEmployee where "))
            //    {
            //        mydata.Fill(tbl);
            //        if (tbl.row.count > 0)
            //        {
            //            return true;
            //        }
            //        else return false
            //    }
            //}
        }
        private void txtCCCD_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }
    }
}
