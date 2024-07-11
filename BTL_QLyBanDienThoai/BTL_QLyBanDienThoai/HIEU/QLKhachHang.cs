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
    public partial class QLKhachHang : Form
    {
        public QLKhachHang()
        {
            InitializeComponent();
        }
        private void LayKH()
        {
            string constr = ConfigurationManager.ConnectionStrings["QLBH"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select * from v_KhachHang", con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    using (SqlDataAdapter adt = new SqlDataAdapter(cmd))
                    {
                        DataTable tbl = new DataTable();
                        adt.Fill(tbl);
                        dvgKH.DataSource = tbl;
                    }
                }
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            LayKH();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["QLBH"].ConnectionString;
            //int iMaKH = Convert.ToInt32(txtMaKH.Text);
            DateTime ngaysinh = DateTime.ParseExact(txtNgaySinh.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string g;
            if (radioButton1.Checked == true)
            {
                g = "Nam";
            }
            else
            {
                g = "Nữ";
            }
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.CommandText = "procThemKhachHang";

                    cmd.Parameters.AddWithValue("@fullName", txtTenKH.Text);
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
                    LayKH();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["QLBH"].ConnectionString;
            int iMaKH = Convert.ToInt32(txtMaKH.Text);
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.CommandText = "procXoaKhachHang";

                    cmd.Parameters.AddWithValue("@idCustomer", iMaKH);
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
                    LayKH();
                }
            }
        }

        private void dvgKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = dvgKH.Rows[e.RowIndex];
            txtMaKH.Text = Convert.ToString(row.Cells["Mã khách hàng"].Value);
            txtTenKH.Text = Convert.ToString(row.Cells["Họ tên"].Value);
            if (row.Cells["Giới tính"].Value.ToString() == "Nam")
            {
                radioButton1.Checked = true;
            }
            if (row.Cells["Giới tính"].Value.ToString() == "Nữ")
            {
                radioButton2.Checked = true;
            }
            txtNgaySinh.Text = Convert.ToString(row.Cells["Ngày sinh"].Value);
            txtDiaChi.Text = Convert.ToString(row.Cells["Địa chỉ"].Value);
            txtSDT.Text = Convert.ToString(row.Cells["SDT"].Value);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["QLBH"].ConnectionString;
            int iMaKH = Convert.ToInt32(txtMaKH.Text);
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
                    cmd.CommandText = "procSuaKh";

                    cmd.Parameters.AddWithValue("@idCustomer", iMaKH);
                    cmd.Parameters.AddWithValue("@fullName", txtTenKH.Text);
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
                    LayKH();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            TimKiemKH();
        }
        private void TimKiemKH()
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
                    command.CommandText = "select * from v_KhachHang where 1=1";
                    if (!String.IsNullOrEmpty(txtMaKH.Text))
                    {
                        //command.Parameters[0].Value = textBox1.Text;
                        command.CommandText += " and [Mã khách hàng] = @iMaKH";
                        command.Parameters.AddWithValue("@iMaKH", txtMaKH.Text);
                    }
                    if (!String.IsNullOrEmpty(txtTenKH.Text))
                    {
                        command.CommandText += " and [Họ tên] like @sTenKH";
                        command.Parameters.AddWithValue("@sTenKH", "%" + txtTenKH.Text + "%");
                    }
                    if (radioButton1.Checked == true || radioButton2.Checked == true)
                    {
                        command.CommandText += " and [Giới tính] = @sGioiTinh";
                        command.Parameters.AddWithValue("@sGioiTinh", g);
                    }
                    if (!String.IsNullOrEmpty(txtDiaChi.Text))
                    {
                        command.CommandText += " and [Địa chỉ] like @DiaChi";
                        command.Parameters.AddWithValue("@DiaChi", "%" + txtDiaChi.Text + "%");
                    }
                    if (!String.IsNullOrEmpty(txtSDT.Text))
                    {
                        command.CommandText += " and [SDT] = @sSDT";
                        command.Parameters.AddWithValue("@sSDT", txtSDT.Text);
                    }
                    if (checkBox1.Checked == true)
                    {
                        command.CommandText += " and [Ngày sinh] = @NgaySinh";
                        command.Parameters.AddWithValue("@NgaySinh", txtNgaySinh.Text);
                    }
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        DataTable tbl = new DataTable();
                        dataAdapter.Fill(tbl);
                        dvgKH.DataSource = tbl;
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtMaKH.Text = "";
            txtTenKH.Text = "";
            txtNgaySinh.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            LayKH();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
