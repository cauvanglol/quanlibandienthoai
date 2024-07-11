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
    public partial class QLyTaiKhoan : Form
    {
        public QLyTaiKhoan()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (txtTenDN.Text == "")
            {
                errorProvider1.SetError(txtTenDN, "Tên đăng nhập không được để trống");
            }
            if (KT_TK() == true)
            {
                errorProvider1.SetError(txtTenDN, "Tên đăng nhập đã tồn tại");
            }
            if (txtMK.Text == "")
            {
                errorProvider1.SetError(txtMK, "Mật khẩu không được để trống");
            }
            if (txtMK.Text.Length < 6)
            {
                errorProvider1.SetError(txtMK, "Mật khẩu ít nhất phải có 6 ký tự");
            }
            if (txtMKLai.Text == "")
            {
                errorProvider1.SetError(txtMKLai, "Mật khẩu không được để trống");
            }
            if (txtMKLai.Text != txtMK.Text)
            {
                errorProvider1.SetError(txtMKLai, "Mật khẩu không khớp");
            }
            else if (txtTenDN.Text != " " && KT_TK() == false && txtMK.Text != " " && txtMK.Text.Length > 5 && txtMKLai.Text != " " && txtMKLai.Text == txtMK.Text)
            {
                string constr = ConfigurationManager.ConnectionStrings["QLBH"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.CommandText = "procThemTK";

                        cmd.Parameters.AddWithValue("@username", txtTenDN.Text);
                        cmd.Parameters.AddWithValue("@passwordd", txtMK.Text);
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
                        LayTK();
                    }
                }
            }
        }
        private void LayTK()
        {
            string constr = ConfigurationManager.ConnectionStrings["QLBH"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select * from v_TK", con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    using (SqlDataAdapter adt = new SqlDataAdapter(cmd))
                    {
                        DataTable tbl = new DataTable();
                        adt.Fill(tbl);
                        dataGridView1.DataSource = tbl;
                    }
                }
            }
        }
        private bool KT_TK()
        {
            string constr = ConfigurationManager.ConnectionStrings["QLBH"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlDataAdapter mydata = new SqlDataAdapter("select * from tblAccount where username = '" + txtTenDN.Text + "'", cnn))
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

        private void QLyTaiKhoan_Load(object sender, EventArgs e)
        {
            LayTK();
        }

        private void txtMK_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMKLai_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtMK.UseSystemPasswordChar = false;
            }
            else
            {
                txtMK.UseSystemPasswordChar = true;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                txtMKLai.UseSystemPasswordChar = false;
            }
            else
            {
                txtMKLai.UseSystemPasswordChar = true;
            }
        }

        private void txtTenDN_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTenDN_Validating(object sender, CancelEventArgs e)
        {

        }

        private void txtMK_Validating(object sender, CancelEventArgs e)
        {

        }

        private void txtMKLai_Validating(object sender, CancelEventArgs e)
        {

        }
        private bool KiemTraUserName()
        {
            string constr = ConfigurationManager.ConnectionStrings["QLBH"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlDataAdapter mydata = new SqlDataAdapter("select * from tblAccount, tblEmployee where tblAccount.username = tblEmployee.username and tblAccount.username = '" + txtTenDN.Text + "'", cnn))
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
        private bool KiemTraMK()
        {
            string constr = ConfigurationManager.ConnectionStrings["QLBH"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlDataAdapter mydata = new SqlDataAdapter("select * from tblAccount where username = '" + txtTenDN.Text + "' and passwordd = '" + txtMK.Text + "'", cnn))
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
        private void button2_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["QLBH"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.CommandText = "XoaTK";

                    cmd.Parameters.AddWithValue("@username", txtTenDN.Text);
                    con.Open();
                    DialogResult dg = MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dg == DialogResult.Yes)
                    {
                        if (KiemTraUserName() == true)
                        {
                            MessageBox.Show("Tài khoản này đang được sử dụng, không thể xóa", "Thông báo");
                        }
                        else
                        {
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
                        }
                    }
                    LayTK();
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = dataGridView1.Rows[e.RowIndex];
            txtTenDN.Text = Convert.ToString(row.Cells["Tài khoản"].Value);
            txtMK.Text = Convert.ToString(row.Cells["Mật khấu"].Value);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["QLBH"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.CommandText = "DoiMK";

                    cmd.Parameters.AddWithValue("@username", txtTenDN.Text);
                    cmd.Parameters.AddWithValue("@passwordd", txtMK.Text);
                    con.Open();
                    DialogResult dg = MessageBox.Show("Bạn có chắc chắn muốn đổi mật khẩu", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dg == DialogResult.Yes)
                    {
                        errorProvider1.Clear();
                        if (KiemTraMK() == true)
                        {
                            MessageBox.Show("Mật khẩu giống với mật khẩu cũ", "Thông báo");
                        }
                        if (txtMK.Text == "")
                        {
                            errorProvider1.SetError(txtMK, "Mật khẩu không được để trống");
                        }
                        if (txtMK.Text.Length < 6)
                        {
                            errorProvider1.SetError(txtMK, "Mật khẩu ít nhất phải có 6 ký tự");
                        }
                        if (txtMKLai.Text == "")
                        {
                            errorProvider1.SetError(txtMKLai, "Mật khẩu không được để trống");
                        }
                        if (txtMKLai.Text != txtMK.Text)
                        {
                            errorProvider1.SetError(txtMKLai, "Mật khẩu không khớp");
                        }
                        else if (txtMK.Text != " " && txtMK.Text.Length > 5 && txtMKLai.Text != " " && txtMKLai.Text == txtMK.Text && KiemTraMK() == false)
                        {
                            {
                                int i = cmd.ExecuteNonQuery();
                                if (i > 0)
                                {
                                    MessageBox.Show("Đổi mật khẩu thành công ", "Thông báo", MessageBoxButtons.OK);
                                }
                                else
                                {
                                    MessageBox.Show("Đổi mật khẩu thất bại ", "Thông báo", MessageBoxButtons.OK);
                                }
                            }
                        }
                        LayTK();
                    }
                }
            }
        }
    }
}
