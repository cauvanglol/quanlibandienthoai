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
using BTL_QLyBanDienThoai.TUAN;
namespace BTL_QLyBanDienThoai.HIEU
{
    public partial class QLDanhMuc : Form
    {
        public QLDanhMuc(string v)
        {
            InitializeComponent();
            tKToolStripMenuItem.Text = v;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string contr = ConfigurationManager.ConnectionStrings["QLBH"].ConnectionString;
            string select = "SELECT [Mã nhân viên],[Họ tên] FROM v_NV WHERE [Tài khoản] = '" + tKToolStripMenuItem.Text + "'";
            using (SqlConnection cnn = new SqlConnection(contr))
            {
                using (SqlCommand cmd = new SqlCommand(select, cnn))
                {
                    cnn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        mãNVToolStripMenuItem.Text = reader["Mã nhân viên"].ToString();
                        tToolStripMenuItem.Text = reader["Họ tên"].ToString();
                    }
                    cnn.Close();
                }
            }
            tToolStripMenuItem.HideDropDown();
        }
        private void quảnLýNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QLNhanVien form3 = new QLNhanVien();
            form3.MdiParent = this;
            form3.Show();
        }

        private void quảnLýKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QLKhachHang form2 = new QLKhachHang();
            form2.MdiParent = this;
            form2.Show();
        }

        



        private void quảnLýTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QLyTaiKhoan form3 = new QLyTaiKhoan();
            form3.MdiParent = this;
            form3.Show();
        }

        

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Bạn muốn đăng xuất", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dg == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void thooToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThongTinTK thongTin = new ThongTinTK(tKToolStripMenuItem.Text);
            thongTin.ShowDialog();
        }

        private void quảnLýHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QLHoaDon qLHoaDon = new QLHoaDon(mãNVToolStripMenuItem.Text + "." + tToolStripMenuItem.Text);
            qLHoaDon.ShowDialog();
        }

        private void mãNVToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void tToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void tKToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        
        private void quảnLýĐiệnThoạiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QLDienThoai lDienThoai = new QLDienThoai();
            lDienThoai.ShowDialog();
        }

        private void quảnLýBảoHànhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BaoHanh bh = new BaoHanh();
            bh.ShowDialog();

        }
        //private void Form1_Load(object sender, EventArgs e)
        //{
        //    string contr = ConfigurationManager.ConnectionStrings["QLDT"].ConnectionString;
        //    string select = "SELECT [Mã nhân viên],[Họ tên] FROM v_NV WHERE [Tài khoản] = '" + tKToolStripMenuItem.Text + "'";
        //    using (SqlConnection cnn = new SqlConnection(contr))
        //    {
        //        using (SqlCommand cmd = new SqlCommand(select, cnn))
        //        {
        //            cnn.Open();
        //            SqlDataReader reader = cmd.ExecuteReader();
        //            if (reader.Read())
        //            {
        //                mãNVToolStripMenuItem.Text = "Mã NV" + reader["Mã nhân viên"].ToString();
        //                tToolStripMenuItem.Text = reader["Họ tên"].ToString();
        //            }
        //            cnn.Close();
        //        }
        //    }
        //    if (KiemTraMK() == true)
        //    {
        //        quảnLýTàiKhoảnToolStripMenuItem.Enabled = false;
        //    }
        //}
        //private bool KiemTraMK()
        //{
        //    string constr = ConfigurationManager.ConnectionStrings["QLDT"].ConnectionString;
        //    using (SqlConnection cnn = new SqlConnection(constr))
        //    {
        //        using (SqlDataAdapter mydata = new SqlDataAdapter("select * from tblAccount where username = '" + tKToolStripMenuItem.Text + "' and rolee = 'Nhân viên' ", cnn))
        //        {
        //            using (DataTable tbl = new DataTable())
        //            {
        //                mydata.Fill(tbl);
        //                if (tbl.Rows.Count > 0)
        //                {
        //                    return true;

        //                }
        //                else
        //                    return false;
        //            }
        //        }
        //    }
        //}
    }
}
