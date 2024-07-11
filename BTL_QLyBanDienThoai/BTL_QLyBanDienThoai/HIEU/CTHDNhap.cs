using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_QLyBanDienThoai.HIEU
{
    public partial class CTHDNhap : Form
    {
        DungChung cf = new DungChung();
        string constr = ConfigurationManager.ConnectionStrings["QLBH"].ConnectionString;
        public CTHDNhap(string v)
        {
            InitializeComponent();
            cbHoaDon.Text = v;
        }
        private void hienCTHD()
        {
            string s = "SELECT * FROM v_CTHD WHERE [Mã hóa đơn] = " + cbHoaDon.Text;
            cf.FillDgv(s, dgvChitiet, "v_CTHD");
        }

        private void CTHDNhap_Load(object sender, EventArgs e)
        {
            string s1 = "SELECT idWarranty,timeWarranty FROM tblWarranty";
            string s2 = "SELECT idBill FROM tblBill";
            string s3 = "SELECT idDevice,Devicename FROM tblDevice";
            hienCTHD();
            cf.LoadComboBox2INT(s1, cbBaohanh);
            cf.LoadComboBox1int(s2, cbHoaDon, 0);
            cf.LoadComboBox2Data(s3, cbDienthoai);
        }

        private void dgvChitiet_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dgvChitiet.CurrentRow.Index;
            txtMachitiet.Text = dgvChitiet.Rows[i].Cells[0].Value.ToString();
            cbHoaDon.Text = dgvChitiet.Rows[i].Cells[1].Value.ToString();
            cbDienthoai.Text = dgvChitiet.Rows[i].Cells[3].Value.ToString();
            cbBaohanh.Text = dgvChitiet.Rows[i].Cells[5].Value.ToString();
            txtThueVAT.Text = dgvChitiet.Rows[i].Cells[8].Value.ToString();
            txtGiamgia.Text = dgvChitiet.Rows[i].Cells[7].Value.ToString();
            lbGia.Text = dgvChitiet.Rows[i].Cells[6].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string[] arr = cbDienthoai.SelectedItem.ToString().Split('.');
            string[] arrBH = cbBaohanh.SelectedItem.ToString().Split('.');
            string s1 = "SELECT COUNT(idWarranty) FROM tblBillDetails WHERE idWarranty =" + arrBH[0];
            string insert = "INSERT INTO tblBillDetails VALUES (" + cbHoaDon.Text + "," + arr[0] + ","
                + arrBH[0] + ", 0 ," + txtThueVAT.Text + "," + txtGiamgia.Text + ")";
            //if (cf.CheckTonTai(s1) > 0)
            //{
            //    MessageBox.Show("Mã chi tiết hóa đơn đã tồn tại!");
            //}
            //else
            //{
            if (cf.CheckTonTai(s1) > 0)
            {
                MessageBox.Show("Bảo hành này hiện đang được sử dụng!");
            }
            else
            {
                if (cf.IUDDuLieu(insert) == true)
                {
                    MessageBox.Show("Thêm dữ liệu thành công");
                    hienCTHD();
                }
                else
                {
                    MessageBox.Show("Thêm dữ liệu thất bại");
                }
            }
            //}
            //using (SqlConnection cnn = new SqlConnection(constr))
            //{
            //    using (SqlCommand cmd = cnn.CreateCommand())
            //    {
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        cmd.CommandText = "sp_UpdateWarranty";
            //        cmd.Parameters.AddWithValue("@idBill", cbHoaDon.SelectedItem);
            //        cmd.Parameters.AddWithValue("@idDevice", arr[0]);
            //        cmd.Parameters.AddWithValue("@idWarranty", arrBH[0]);
            //        cmd.Parameters.AddWithValue("@price", 0);
            //        cmd.Parameters.AddWithValue("@VAT", Convert.ToDouble(txtThueVAT.Text));
            //        cmd.Parameters.AddWithValue("@sale", Convert.ToDouble(txtGiamgia.Text));
            //        cnn.Open();
            //        int i = cmd.ExecuteNonQuery();
            //        if (i > 0)
            //            {
            //                MessageBox.Show("Thêm thành công ", "Thông báo", MessageBoxButtons.OK);
            //            }
            //            else
            //            {
            //                MessageBox.Show("Thêm thất bại ", "Thông báo", MessageBoxButtons.OK);
            //            }
            //        }
            //        hienCTHD();
            //    }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string s1 = "SELECT COUNT(idBillDetails) FROM tblBillDetails WHERE idBillDetails =" + txtMachitiet.Text;
            string delete = "DELETE FROM tblBillDetails WHERE idBillDetails =" + txtMachitiet.Text;
            string update2 = "UPDATE tblBill  SET amount = (SELECT COUNT(*) FROM tblBillDetails" +
                " WHERE tblBill.idBill = tblBillDetails.idBill)," +
                "totalPrice = (SELECT SUM(price) FROM tblBillDetails " +
                "WHERE tblBill.idBill = tblBillDetails.idBill)";

            if (cf.CheckTonTai(s1) == 0)
            {
                MessageBox.Show("Mã chi tiết hóa đơn không tồn tại!");
            }
            else
            {
                if (cf.IUDDuLieu(delete) && cf.IUDDuLieu(update2) == true)
                {
                    MessageBox.Show("Xóa dữ liệu thành công");
                    hienCTHD();
                }
                else
                {
                    MessageBox.Show("Xóa dữ liệu thất bại");
                }
            }
        }

        private void cbHoaDon_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}