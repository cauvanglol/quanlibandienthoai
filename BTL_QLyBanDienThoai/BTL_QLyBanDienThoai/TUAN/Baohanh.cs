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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;

namespace BTL_QLyBanDienThoai.TUAN
{
    public partial class BaoHanh : Form
    {
        public BaoHanh()
        {
            InitializeComponent();
        }
        string constr = ConfigurationManager.ConnectionStrings["QLBH"].ConnectionString;
        private void LayDSDL()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select * from v_tblWarranty", con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    using (SqlDataAdapter adt = new SqlDataAdapter(cmd))
                    {
                        DataTable tbl = new DataTable();
                        adt.Fill(tbl);
                        dvg1.DataSource = tbl;
                    }
                }
            }
        }
        //private bool kiemtraFK()
        //{
        //    int mbh = int.Parse(txtMaBH.Text);
        //    using (SqlConnection cnn = new SqlConnection(constr))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("SELECT * FROM tblWarranty WHERE idWarranty = " + mbh, cnn))
        //        {
        //            using (SqlDataAdapter adt = new SqlDataAdapter(cmd))
        //            {
        //                DataTable tb = new DataTable();
        //                adt.Fill(tb);
        //                if (tb.Rows.Count > 0)
        //                    return true;
        //                else
        //                    return false;
        //            }
        //        }
        //    }
        //}

        private void btnThem_Click(object sender, EventArgs e)
        {
            //if (kiemtraFK() == true)
            //{
            //    MessageBox.Show("Mã bảo hành đã tồn tại", "Thông báo", MessageBoxButtons.OK);
            //}
            //else
            //{
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_AddWarranty";
                    //int mb = int.Parse(txtMaBH.Text);
                    //DateTime ngaykt = DateTime.Parse(txtNgayKT.Text);
                    cmd.Parameters.AddWithValue("@typee", txtLoaiBH.Text);
                    cmd.Parameters.AddWithValue("@timeWarranty", txtTime.Text);
                    cmd.Parameters.AddWithValue("@support", txtHT.Text);
                    cmd.Parameters.AddWithValue("@note", txtNote.Text);
                    cnn.Open();
                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {
                        MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                    else
                    {
                        MessageBox.Show("Thêm thất bại", "Thông báo", MessageBoxButtons.OK);
                    }
                    LayDSDL();
                }
                //    }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_UpdateWarranty";
                    int mb = int.Parse(txtMaBH.Text);
                    cmd.Parameters.AddWithValue("@idWarranty", mb);
                    cmd.Parameters.AddWithValue("@typee", txtLoaiBH.Text);
                    cmd.Parameters.AddWithValue("@support", txtHT.Text);
                    cmd.Parameters.AddWithValue("@note", txtNote.Text);
                    cnn.Open();
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
                    LayDSDL();
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_DeleteWarranty";
                    int mb = int.Parse(txtMaBH.Text);
                    cmd.Parameters.AddWithValue("@idWarranty", mb);
                    cnn.Open();
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
                    LayDSDL();
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtMaBH.Text = null;
            txtLoaiBH.Text = null;
            txtNgayKT.Text = null;
            txtHT.Text = null;
            txtNote.Text = null;
            txtTime.Text = null;
            LayDSDL();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from v_tblWarranty where 1=1";
                    if (!String.IsNullOrEmpty(txtMaBH.Text))
                    {
                        cmd.CommandText += " and [Mã bảo hành] = @iMaBaoHanh";
                        cmd.Parameters.AddWithValue("@iMaBaoHanh", txtMaBH.Text);
                    }
                    if (!String.IsNullOrEmpty(txtLoaiBH.Text))
                    {
                        cmd.CommandText += " and [Loại bảo hành] like @loaiBH";
                        cmd.Parameters.AddWithValue("@loaiBH", txtLoaiBH.Text);
                    }
                    if (!String.IsNullOrEmpty(txtTime.Text))
                    {
                        cmd.CommandText += " and [Thời gian bảo hành] = @Time";
                        cmd.Parameters.AddWithValue("@Time", txtTime.Text);
                    }
                    if (cb.Checked == true)
                    {
                        cmd.CommandText += " and [Ngày kết thúc bảo hành] = @ngaykt";
                        cmd.Parameters.AddWithValue("@ngaykt", txtNgayKT.Text);
                    }
                    if (!String.IsNullOrEmpty(txtHT.Text))
                    {
                        cmd.CommandText += " and [Hệ thống hỗ trợ] = @ht";
                        cmd.Parameters.AddWithValue("@ht", txtHT.Text);
                    }
                    if (!String.IsNullOrEmpty(txtNote.Text))
                    {
                        cmd.CommandText += " and [Ghi chú] = @note";
                        cmd.Parameters.AddWithValue("@note", txtNote.Text);
                    }
                    cnn.Open();
                    using (SqlDataAdapter adt = new SqlDataAdapter(cmd))
                    {
                        DataTable tbl = new DataTable();
                        adt.Fill(tbl);
                        dvg1.DataSource = tbl;
                    }
                }
            }
        }

        private void dvg1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = dvg1.Rows[e.RowIndex];
            txtMaBH.Text = Convert.ToString(row.Cells["Mã bảo hành"].Value);
            txtLoaiBH.Text = Convert.ToString(row.Cells["Loại bảo hành"].Value);
            txtNgayKT.Text = Convert.ToString(row.Cells["Ngày kết thúc bảo hành"].Value);
            txtTime.Text = Convert.ToString(row.Cells["Thời gian bảo hành"].Value);
            txtNote.Text = Convert.ToString(row.Cells["Ghi chú"].Value);
            txtHT.Text = Convert.ToString(row.Cells["Hệ thống hỗ trợ"].Value);
        }

        private void BaoHanh_Load(object sender, EventArgs e)
        {
            LayDSDL();
        }
    }
}