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

namespace BTL_QLyBanDienThoai.HON
{
    public partial class FormIDBillDetail : Form
    {
        public FormIDBillDetail()
        {
            InitializeComponent();
        }

        CommonFunction cf = new CommonFunction();
        string strCon = ConfigurationManager.ConnectionStrings["QLBH"].ConnectionString;
        string s = "SELECT idBillDetails AS [Mã chi tiết hóa đơn]" +
                 ",idBill AS [Mã hóa đơn],d.idDevice AS [Mã điện thoại]" +
                 ",deviceName AS [Tên điện thoại],w.idWarranty AS [Mã bảo hành]," +
                 "typee AS [Loại bảo hành],bd.price AS [Thành tiền]" +
                 ",VAT AS [Thuế VAT],sale AS [Giảm giá] FROM tblBillDetails bd " +
                 "INNER JOIN tblDevice d ON d.idDevice= bd.idDevice " +
                 "INNER JOIN tblWarranty w ON w.idWarranty=bd.idWarranty ";
        private void FormIDBillDetail_Load(object sender, EventArgs e)
        {

            string s1 = "SELECT idWarranty FROM tblWarranty";
            string s2 = "SELECT idBill FROM tblBill";
            string s3 = "SELECT idDevice,Devicename FROM tblDevice";
            cf.FillDgv(s, dgvChitiet, "tblBillDetails");
            cf.LoadComboBox1int(s1, cbBaohanh, 0);
            cf.LoadComboBox1int(s2, cbHoaDon, 0);
            cf.LoadComboBox2Data(s3, cbDienthoai);
        }

        private void dgvChitiet_CellContentClick(object sender, DataGridViewCellEventArgs e)
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

        //private void btnThem_Click(object sender, EventArgs e)
        //{
        //    string[] arr = cbDienthoai.SelectedItem.ToString().Split('.');
        //    string s1 = "SELECT COUNT(idBillDetails) FROM tblBillDetails WHERE idBillDetails =" + txtMachitiet.Text;
        //    string insert = "INSERT INTO tblBillDetails VALUES (" + txtMachitiet.Text
        //        + ", " + cbHoaDon.SelectedItem + ", " + arr[0] + ", "
        //        + cbBaohanh.SelectedItem + ", 0, " + txtThueVAT.Text + ", " + txtGiamgia.Text + ")";
        //    string update1 = "UPDATE tblBillDetails SET price = ROUND((1 + VAT) * ((1 - sale)" +
        //        " * (SELECT tblDevice.price FROM tblDevice WHERE tblBillDetails.idDevice = tblDevice.idDevice)),2)";
        //    string update2 = "UPDATE tblBill  SET amount = (SELECT COUNT(*) FROM tblBillDetails" +
        //        " WHERE tblBill.idBill = tblBillDetails.idBill)," +
        //        "totalPrice = (SELECT SUM(price) FROM tblBillDetails " +
        //        "WHERE tblBill.idBill = tblBillDetails.idBill)";
        //    if (cf.CheckTonTai(s1) > 0)
        //    {
        //        MessageBox.Show("Mã chi tiết hóa đơn đã tồn tại!");
        //    }
        //    else
        //    {
        //        if (cf.IUDDuLieu(insert) == true && cf.IUDDuLieu(update1) && cf.IUDDuLieu(update2) == true)
        //        {
        //            MessageBox.Show("Thêm dữ liệu thành công");
        //            cf.FillDgv(s, dgvChitiet, "tblBillDetails");
        //        }
        //        else
        //        {
        //            MessageBox.Show("Thêm dữ liệu thất bại");
        //        }
        //    }
        //}
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string[] arr = cbDienthoai.SelectedItem.ToString().Split('.');
                string s1 = "SELECT COUNT(idBillDetails) FROM tblBillDetails WHERE idBillDetails = @idBillDetails";
                string insert = "INSERT INTO tblBillDetails VALUES (@idBillDetails, @idBill, @idDevice, @idWarranty, 0, @VAT, @sale)";
                string update1 = "UPDATE tblBillDetails SET price = ROUND((1 + VAT) * ((1 - sale) * (SELECT tblDevice.price FROM tblDevice WHERE tblBillDetails.idDevice = tblDevice.idDevice)), 2)";
                string update2 = "UPDATE tblBill SET amount = (SELECT COUNT(*) FROM tblBillDetails WHERE tblBill.idBill = tblBillDetails.idBill), totalPrice = (SELECT SUM(price) FROM tblBillDetails WHERE tblBill.idBill = tblBillDetails.idBill)";

                using (SqlConnection sqlCon = new SqlConnection(strCon))
                {
                    sqlCon.Open();
                    using (SqlCommand sqlCmd = new SqlCommand(s1, sqlCon))
                    {
                        sqlCmd.Parameters.AddWithValue("@idBillDetails", txtMachitiet.Text);
                        int count = (int)sqlCmd.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("Mã chi tiết hóa đơn đã tồn tại!");
                            return;
                        }
                    }

                    using (SqlCommand sqlCmd = new SqlCommand(insert, sqlCon))
                    {
                        sqlCmd.Parameters.AddWithValue("@idBillDetails", txtMachitiet.Text);
                        sqlCmd.Parameters.AddWithValue("@idBill", cbHoaDon.SelectedItem);
                        sqlCmd.Parameters.AddWithValue("@idDevice", arr[0]);
                        sqlCmd.Parameters.AddWithValue("@idWarranty", cbBaohanh.SelectedItem);
                        sqlCmd.Parameters.AddWithValue("@VAT", txtThueVAT.Text);
                        sqlCmd.Parameters.AddWithValue("@sale", txtGiamgia.Text);

                        if (cf.IUDDuLieu(sqlCmd.CommandText) && cf.IUDDuLieu(update1) && cf.IUDDuLieu(update2))
                        {
                            MessageBox.Show("Thêm dữ liệu thành công");
                            cf.FillDgv(s, dgvChitiet, "tblBillDetails");
                        }
                        else
                        {
                            MessageBox.Show("Thêm dữ liệu thất bại");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                    cf.FillDgv(s, dgvChitiet, "tblBillDetails");
                }
                else
                {
                    MessageBox.Show("Xóa dữ liệu thất bại");
                }
            }
        }
    }
}
