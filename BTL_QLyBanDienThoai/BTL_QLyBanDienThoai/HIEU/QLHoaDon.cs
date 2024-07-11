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
    public partial class QLHoaDon : Form
    {
        DungChung cf = new DungChung();
        public QLHoaDon(string s)
        {
            InitializeComponent();
            cbNhanvien.Text = s;
        }
        string constr = ConfigurationManager.ConnectionStrings["QLBH"].ConnectionString;


        private void QLHoaDon_Load(object sender, EventArgs e)
        {
            button2.Hide();
            btnSua.Hide();
            btnXoa.Hide();
            btnTimkiem.Hide();
            btnHienchitiet.Hide();
            btnIn.Hide();
            lbCanhbao.Hide();
            chkTongtien.Hide();
            chkNhanvien.Hide();
            chkKhachhang.Hide();
            lbTu.Hide();
            txtTu.Hide();
            lbDen.Hide();
            txtDen.Hide();
            btnChiTiet.Hide();
            LayDSDL();
            cf.LoadComboBox2Data("SELECT idEmployee, fullname FROM tblEmployee", cbNhanvien);
            cf.LoadComboBox2Data("SELECT idCustomer, fullname FROM tblCustomer", cbKhachhang);
        }

        private void LayDSDL()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select * from v_HoaDon", con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    using (SqlDataAdapter adt = new SqlDataAdapter(cmd))
                    {
                        DataTable tbl = new DataTable();
                        adt.Fill(tbl);
                        dgv.DataSource = tbl;
                    }
                }
            }
        }
        private void HideControl(Button b)
        {
            btnIn.Hide();
            button2.Hide();
            btnChiTiet.Hide();
            btnSua.Hide();
            btnXoa.Hide();
            btnTimkiem.Hide();
            btnHienchitiet.Hide();
            lbCanhbao.Hide();
            chkTongtien.Hide();
            chkNhanvien.Hide();
            chkKhachhang.Hide();
            lbTu.Hide();
            txtTu.Hide();
            lbDen.Hide();
            txtDen.Hide();
            b.Show();
        }
        private void AnNhap()
        {
            txtMahoadon.Enabled = false;
            cbKhachhang.Enabled = false;
            cbNhanvien.Enabled = false;
            dtp.Enabled = false;
            chkbThanhtoan.Enabled = false;
        }
        private void HienNhap()
        {
            txtMahoadon.Enabled = true;
            cbKhachhang.Enabled = true;
            cbNhanvien.Enabled = true;
            dtp.Enabled = true;
            chkbThanhtoan.Enabled = true;
        }
        private void SetMauPa(Panel pa)
        {
            paThem.BackColor = Color.AliceBlue;
            paSua.BackColor = Color.AliceBlue;
            paXoa.BackColor = Color.AliceBlue;
            paTimkiem.BackColor = Color.AliceBlue;
            paXemchitiet.BackColor = Color.AliceBlue;
            paBaocao.BackColor = Color.AliceBlue;
            pa.BackColor = Color.Bisque;
        }


        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            string select = "SELECT * FROM v_HoaDon WHERE 1=1 ";
            if (chkNhanvien.Checked == true && chkKhachhang.Checked == false && chkTongtien.Checked == false)
            {
                string[] arrNhanvien = cbNhanvien.SelectedItem.ToString().Split('.');
                string Manhanvien = select + " AND [Mã khách hàng] = " + arrNhanvien[0];
                cf.FillDgv(Manhanvien, dgv, "v_HoaDon");
            }
            else if (chkNhanvien.Checked == true && chkKhachhang.Checked == true && chkTongtien.Checked == false)
            {
                string[] arrNhanvien = cbNhanvien.SelectedItem.ToString().Split('.');
                string[] arrKhachhang = cbKhachhang.SelectedItem.ToString().Split('.');
                string Manhanvien = select + " AND [Mã nhân viên] = " + arrNhanvien[0];
                string Manhanvien_Khachhang = Manhanvien + " AND [Mã khách hàng] = " + arrKhachhang[0];
                cf.FillDgv(Manhanvien_Khachhang, dgv, "v_HoaDon");
            }
            else if (chkNhanvien.Checked == true && chkKhachhang.Checked == true && chkTongtien.Checked == true)
            {
                string[] arrNhanvien = cbNhanvien.SelectedItem.ToString().Split('.');
                string[] arrKhachhang = cbKhachhang.SelectedItem.ToString().Split('.');
                string Manhanvien = select + " AND [Mã nhân viên] = " + arrNhanvien[0];
                string Manhanvien_Khachhang = Manhanvien + " AND [Mã khách hàng] = " + arrKhachhang[0];
                string Manhanvien_Khachhang_Tongtien = Manhanvien_Khachhang + " AND [Tổng tiền] BETWEEN " + txtTu.Text + " AND " + txtDen.Text;
                cf.FillDgv(Manhanvien_Khachhang_Tongtien, dgv, "v_HoaDon");

            }
            else if (chkNhanvien.Checked == true && chkKhachhang.Checked == false && chkTongtien.Checked == true)
            {
                string[] arrNhanvien = cbNhanvien.SelectedItem.ToString().Split('.');
                string Manhanvien = select + " AND [Mã nhân viên] = " + arrNhanvien[0];
                string Manhanvien_Tongtien = Manhanvien + " AND [Tổng tiền] BETWEEN " + txtTu.Text + " AND " + txtDen.Text;
                cf.FillDgv(Manhanvien_Tongtien, dgv, "v_HoaDon");

            }
            else if (chkNhanvien.Checked == false && chkKhachhang.Checked == true && chkTongtien.Checked == false)
            {
                string[] arrKhachhang = cbKhachhang.SelectedItem.ToString().Split('.');
                string Makhachhang = select + " AND [Mã khách hàng] = " + arrKhachhang[0];
                cf.FillDgv(Makhachhang, dgv, "v_HoaDon");
            }
            else if (chkNhanvien.Checked == false && chkKhachhang.Checked == true && chkTongtien.Checked == true)
            {
                string[] arrKhachhang = cbKhachhang.SelectedItem.ToString().Split('.');
                string Makhachhang = select + " AND [Mã khách hàng] = " + arrKhachhang[0];
                string Makhachhang_Tongtien = Makhachhang + " AND [Tổng tiền] BETWEEN " + txtTu.Text + " AND " + txtDen.Text;
                cf.FillDgv(Makhachhang_Tongtien, dgv, "v_HoaDon");
            }
            else if (chkNhanvien.Checked == false && chkKhachhang.Checked == false && chkTongtien.Checked == true)
            {
                string Tongtien = select + " AND [Tổng tiền] BETWEEN " + txtTu.Text + " AND " + txtDen.Text; ;
                cf.FillDgv(Tongtien, dgv, "v_HoaDon");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //string sqlStr = "SELECT COUNT(*) FROM tblBill WHERE idBill =" + txtMahoadon.Text;
            if (txtMahoadon.Text != null && cbKhachhang.SelectedItem != null && cbNhanvien.Text != null)
            {
                string[] arrKhachhang = cbKhachhang.SelectedItem.ToString().Split('.');
                string[] arrNhanvien = cbNhanvien.Text.ToString().Split('.');
                int trangthai = 0;
                if (chkbThanhtoan.Checked == true)
                {
                    trangthai = 1;
                }
                string insert = "INSERT INTO tblBill VALUES (" + arrKhachhang[0] +
               ", " + arrNhanvien[0] + ", '" + dtp.Value + "', 0 ," + trangthai + ", 0)";
                //if (cf.CheckTonTai(sqlStr) == 0)
                //{
                if (cf.IUDDuLieu(insert) == true)
                {
                    MessageBox.Show("Thêm thành công!");
                    LayDSDL();
                }
                else
                {
                    MessageBox.Show("Thêm thất bại!");
                }
                //}
                //else
                //{
                //    MessageBox.Show("Mã hóa đơn đã tồn tại. Hãy nhập mã hóa đơn khác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }
            else
            {
                MessageBox.Show("Không được để trống dữ liệu!");
            }
            btnChiTiet.Show();
        }
        //private void button2_Click(object sender, EventArgs e)
        //{
        //    //string sqlStr = "SELECT COUNT(*) FROM tblBill WHERE idBill =" + txtMahoadon.Text;
        //    string dtpngay = dtp.Value.ToString();
        //    string[] arr = dtpngay.Split(' ')[0].Split('/');
        //    string ngay = arr[0];
        //    string thang = arr[1];
        //    string nam = arr[2];
        //    string date = nam + "-" + thang + "-" + ngay;
        //    if (txtMahoadon.Text != null && cbKhachhang.SelectedItem != null && cbNhanvien.Text != null)
        //    {

        //        string[] arrKhachhang = cbKhachhang.SelectedItem.ToString().Split('.');
        //        string[] arrNhanvien = cbNhanvien.Text.ToString().Split('.');
        //        int trangthai = 0;
        //        if (chkbThanhtoan.Checked == true)
        //        {
        //            trangthai = 1;
        //        }
        //        string insert = "INSERT INTO tblBill VALUES (" + arrKhachhang[0] +
        //       ", " + arrNhanvien[0] + ", '" + date + "', 0 ," + trangthai + ", 0)";
        //        //if (cf.CheckTonTai(sqlStr) == 0)
        //        //{
        //        if (cf.IUDDuLieu(insert) == true)
        //        {
        //            MessageBox.Show("Thêm thành công!");
        //            LayDSDL();
        //        }
        //        else
        //        {
        //            MessageBox.Show("Thêm thất bại!");
        //        }
        //        //}
        //        //else
        //        //{
        //        //    MessageBox.Show("Mã hóa đơn đã tồn tại. Hãy nhập mã hóa đơn khác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        //}
        //    }
        //    else
        //    {
        //        MessageBox.Show("Không được để trống dữ liệu!");
        //    }
        //    btnChiTiet.Show();
        //}
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dtp.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày sửa không thể lớn hơn ngày hiện tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Dừng lại và không thực hiện tiếp
            }
            string[] arrKhachhang = cbKhachhang.SelectedItem.ToString().Split('.');
            string[] arrNhanvien = cbNhanvien.SelectedItem.ToString().Split('.');
            int trangthai = 0;
            if (chkbThanhtoan.Checked == true)
            {
                trangthai = 1;
            }
            string update = "UPDATE tblBill SET idCustomer =" + arrKhachhang[0]
                + ", idEmployee =" + arrNhanvien[0] + ", datee='" + dtp.Value + "',statuss=" + trangthai
                + " WHERE idBill =" + txtMahoadon.Text;
            if (cf.IUDDuLieu(update) == true)
            {
                MessageBox.Show("Sửa dữ liệu thành công!");
                LayDSDL();
            }
            else
            {
                MessageBox.Show("Sửa dữ liệu thất bại!");
            }
        }
        //private void btnSua_Click(object sender, EventArgs e)
        //{
        //    //string sqlStr = "SELECT COUNT(*) FROM tblBill WHERE idBill =" + txtMahoadon.Text;
        //    string dtpngay = dtp.Value.ToString();
        //    string[] arr = dtpngay.Split(' ')[0].Split('/');
        //    string ngay = arr[0];
        //    string thang = arr[1];
        //    string nam = arr[2];
        //    string date = nam + "-" + thang + "-" + ngay;
        //    if (txtMahoadon.Text != null && cbKhachhang.SelectedItem != null && cbNhanvien.Text != null)
        //    {

        //        string[] arrKhachhang = cbKhachhang.SelectedItem.ToString().Split('.');
        //        string[] arrNhanvien = cbNhanvien.Text.ToString().Split('.');
        //        int trangthai = 0;
        //        if (chkbThanhtoan.Checked == true)
        //        {
        //            trangthai = 1;
        //        }
        //        string insert = "INSERT INTO tblBill VALUES (" + arrKhachhang[0] +
        //       ", " + arrNhanvien[0] + ", '" + date + "', 0 ," + trangthai + ", 0)";
        //        //if (cf.CheckTonTai(sqlStr) == 0)
        //        //{
        //        if (cf.IUDDuLieu(insert) == true)
        //        {
        //            MessageBox.Show("Thêm thành công!");
        //            LayDSDL();
        //        }
        //        else
        //        {
        //            MessageBox.Show("Thêm thất bại!");
        //        }
        //        //}
        //        //else
        //        //{
        //        //    MessageBox.Show("Mã hóa đơn đã tồn tại. Hãy nhập mã hóa đơn khác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        //}
        //    }
        //    else
        //    {
        //        MessageBox.Show("Không được để trống dữ liệu!");
        //    }
        //    btnChiTiet.Show();
        //}
        private void btnXoa_Click(object sender, EventArgs e)
        {
            double tt;
            if (txtTongTien.Text == null || txtTongTien.Text == "")
            {
                tt = 0;
            }
            else
            {
                tt = Convert.ToDouble(txtTongTien.Text);
            }
            int a = Convert.ToInt32(txtSoluong.Text);
            if (tt > 0 && a > 0)
            {
                MessageBox.Show("Chỉ có thể xóa các hóa đơn có tổng tiền và tổng số sản phẩm = 0");
                return;
            }
            string delete = "DELETE FROM tblBIll WHERE totalPrice = 0 AND amount = 0 AND idBill = " + txtMahoadon.Text;
            if (cf.IUDDuLieu(delete) == true)
            {
                MessageBox.Show("Xoá dữ liệu thành công!");
                LayDSDL();
            }
            else
            {
                MessageBox.Show("Xóa dữ liệu thất bại!");
            }
        }

        private void btnHienchitiet_Click(object sender, EventArgs e)
        {
            CHHDHien hien = new CHHDHien(txtMahoadon.Text);
            hien.ShowDialog();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            HoaDonBanHangcs donBanHangcs = new HoaDonBanHangcs(txtMahoadon.Text);
            donBanHangcs.ShowDialog();
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int i;
            i = dgv.CurrentRow.Index;

            String[] date = dgv.Rows[i].Cells[5].Value.ToString().Split('/');

            int ngay = Int32.Parse(date[0]);
            int thang = Int32.Parse(date[1]);
            int nam = Int32.Parse(date[2].Split(' ')[0]);

            txtMahoadon.Text = dgv.Rows[i].Cells[0].Value.ToString();
            cbKhachhang.Text = dgv.Rows[i].Cells[1].Value.ToString()
                + "." + dgv.Rows[i].Cells[2].Value.ToString();
            cbNhanvien.Text = dgv.Rows[i].Cells[3].Value.ToString()
                + "." + dgv.Rows[i].Cells[4].Value.ToString();
            dtp.Value = new DateTime(nam, thang, ngay);
            txtTongTien.Text = dgv.Rows[i].Cells[6].Value.ToString();
            if (dgv.Rows[i].Cells[7].Value.ToString() == "Đã thanh toán")
            {
                chkbThanhtoan.Checked = true;
            }
            else
            {
                chkbThanhtoan.Checked = false;
            }
            txtSoluong.Text = dgv.Rows[i].Cells[8].Value.ToString();
            if (paSua.BackColor == Color.Bisque)
            {
                if (chkbThanhtoan.Checked == true)
                {
                    chkbThanhtoan.Enabled = false;
                }
                else
                {
                    chkbThanhtoan.Enabled = true;
                }
            }
        }
        private void HienControl(Button b)
        {
            btnChiTiet.Show();
        }

        private void lbThem_Click(object sender, EventArgs e)
        {
            chkTongtien.Hide();
            chkNhanvien.Hide();
            chkKhachhang.Hide();
            lbTu.Hide();
            txtTu.Hide();
            lbDen.Hide();
            txtDen.Hide();
            HienNhap();
            HideControl(button2);
            HienControl(btnChiTiet);
            SetMauPa(paThem);
            dtp.Value = DateTime.Today;
            dtp.Enabled = false;
            txtMahoadon.Enabled = true;
        }

        private void lbSua_Click(object sender, EventArgs e)
        {
            AnNhap();
            HienNhap();
            HideControl(btnSua);
            SetMauPa(paSua);
            dtp.Enabled = true;
            txtMahoadon.Enabled = false;
            if (chkbThanhtoan.Checked == true)
            {
                chkbThanhtoan.Enabled = false;
            }
            else
            {
                chkbThanhtoan.Enabled = true;
            }
        }


        private void lbXoa_Click(object sender, EventArgs e)
        {
            HideControl(btnXoa);
            lbCanhbao.Show();
            AnNhap();
            SetMauPa(paXoa);
        }

        private void lbTimkiem_Click(object sender, EventArgs e)
        {
            HideControl(btnTimkiem);
            SetMauPa(paTimkiem);
            AnNhap();
            chkTongtien.Show();
            chkNhanvien.Show();
            chkKhachhang.Show();
        }

        private void lbXemchitiet_Click(object sender, EventArgs e)
        {
            AnNhap();
            HideControl(btnHienchitiet);
            SetMauPa(paXemchitiet);
            txtMahoadon.Enabled = true;
            //FormBillDetail fbd = new FormBillDetail(txtMahoadon.Text);
        }

        private void lbBaocao_Click(object sender, EventArgs e)
        {
            SetMauPa(paBaocao);
            HideControl(btnIn);
            HienNhap();
        }

        private void chkTongtien_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTongtien.Checked == true)
            {
                lbTu.Show();
                txtTu.Show();
                lbDen.Show();
                txtDen.Show();
            }
            else
            {
                lbTu.Hide();
                txtTu.Hide();
                lbDen.Hide();
                txtDen.Hide();
            }
        }

        private void chkKhachhang_CheckedChanged(object sender, EventArgs e)
        {
            if (chkKhachhang.Checked == true)
            {
                cbKhachhang.Enabled = true;
            }
            else
            {
                cbKhachhang.Enabled = false;
            }
        }

        private void chkNhanvien_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNhanvien.Checked == true)
            {
                cbNhanvien.Enabled = true;
            }
            else
            {
                cbNhanvien.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LayDSDL();
        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            CTHDNhap nhap = new CTHDNhap(txtMahoadon.Text);
            nhap.Show();
        }

        private void txtTongTien_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string select = "select * from v_HoaDon where 1=1";
            string[] arrNhanVien = cbNhanvien.SelectedItem.ToString().Split('.');
            string MaNhanVien = "select * from v_HoaDon where 1=1 AND [ MÃ NHÂN VIÊN ] = " + arrNhanVien[0] + "AND CCCD= '" + txtCCCD + ".";
            if (cf.CheckTonTai)
            {

            }
        }
        //private bool KiemTraCCD()
        //{
        //    string constr = ConfigurationManager.ConnectionStrings["QLBH"].ConnectionString;
        //    using (SqlConnection cnn = new SqlConnection(constr))
        //    {
        //        using (SqlDataAdapter mydata = new SqlDataAdapter("SELECT * from tblBill,tblEmployee AS "))
        //        {
        //            mydata.Fill(tbl);
        //            if (tbl.row.count > 0)
        //            {
        //                return true;
        //            }
        //            else return false;
        //        }
        //    }
        //}
    }
}