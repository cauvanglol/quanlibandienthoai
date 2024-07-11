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
using BTL_QLyBanDienThoai.HON;
namespace BTL_QLyBanDienThoai.THO
{
    public partial class FormBill : Form
    {
        CommonFunction cf = new CommonFunction();
        string strCon = ConfigurationManager.ConnectionStrings["QLBH"].ConnectionString;
        string sqlStrdgv = "  SELECT idBill AS [Mã hóa đơn]," +
                "tblBill.idCustomer AS[Mã khách hàng]," +
                "tblCustomer.fullName AS[Tên khách hàng]," +
                "tblBill.idEmployee AS[Mã nhân viên]," +
                "tblEmployee.fullName AS[Tên nhân viên]," +
                "datee AS[Ngày lập],totalPrice AS[Tổng tiền]," +
                "CASE WHEN statuss = 1 THEN N'Đã thanh toán' ELSE N'Chưa thanh toán' END AS[Trạng thái]," +
                "amount AS[Số lượng]" +
                "FROM tblBill, tblCustomer, tblEmployee " +
                "WHERE tblBill.idCustomer = tblCustomer.idCustomer " +
                "AND tblBill.idEmployee = tblEmployee.idEmployee ";

        public FormBill()
        {
            InitializeComponent();
        }

        private void HideControl(Button b)
        {
            btnIn.Hide();
            btnThem.Hide();
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

        private void FormBill_Load(object sender, EventArgs e)
        {
            btnThem.Hide();
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
            cf.FillDgv(sqlStrdgv, dgv, "tblBill");
            cf.LoadComboBox2Data("SELECT idEmployee, fullname FROM tblEmployee", cbNhanvien);
            cf.LoadComboBox2Data("SELECT idCustomer, fullname FROM tblCustomer", cbKhachhang);
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dgv.CurrentRow.Index;

            String[] date = dgv.Rows[i].Cells[5].Value.ToString().Split('/');

            int ngay = Int32.Parse(date[1]);
            int thang = Int32.Parse(date[0]);
            int nam = Int32.Parse(date[2].Split(' ')[0]);

            txtMahoadon.Text = dgv.Rows[i].Cells[0].Value.ToString();
            cbKhachhang.Text = dgv.Rows[i].Cells[1].Value.ToString()
                + "." + dgv.Rows[i].Cells[2].Value.ToString();
            cbNhanvien.Text = dgv.Rows[i].Cells[3].Value.ToString()
                + "." + dgv.Rows[i].Cells[4].Value.ToString();
            dtp.Value = new DateTime(nam, thang, ngay);
            lbTongtien.Text = dgv.Rows[i].Cells[6].Value.ToString();
            if (dgv.Rows[i].Cells[7].Value.ToString() == "Đã thanh toán")
            {
                chkbThanhtoan.Checked = true;
            }
            else
            {
                chkbThanhtoan.Checked = false;
            }
            lbSoluong.Text = dgv.Rows[i].Cells[8].Value.ToString();
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
            HideControl(btnThem);
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

        private void lbTimkiem_Click(object sender, EventArgs e)
        {
            HideControl(btnTimkiem);
            SetMauPa(paTimkiem);
            AnNhap();
            chkTongtien.Show();
            chkNhanvien.Show();
            chkKhachhang.Show();
        }

        private void lbXoa_Click(object sender, EventArgs e)
        {
            HideControl(btnXoa);
            lbCanhbao.Show();
            AnNhap();
            SetMauPa(paXoa);
        }

        private void lbXemchitiet_Click(object sender, EventArgs e)
        {
            AnNhap();
            HideControl(btnHienchitiet);
            SetMauPa(paXemchitiet);
            txtMahoadon.Enabled = true;
            FormBillDetail fbd = new FormBillDetail(txtMahoadon.Text);
        }

        private void lbBaocao_Click(object sender, EventArgs e)
        {
            SetMauPa(paBaocao);
            HideControl(btnIn);
            HienNhap();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string sqlStr = "SELECT COUNT(*) FROM tblBill WHERE idBill =" + txtMahoadon.Text;
            if (txtMahoadon.Text != null && cbKhachhang.SelectedItem != null && cbNhanvien.SelectedItem != null)
            {
                string[] arrKhachhang = cbKhachhang.SelectedItem.ToString().Split('.');
                string[] arrNhanvien = cbNhanvien.SelectedItem.ToString().Split('.');
                int trangthai = 0;
                if (chkbThanhtoan.Checked == true)
                {
                    trangthai = 1;
                }
                string insert = "INSERT INTO tblBill VALUES (" + txtMahoadon.Text + ", " + arrKhachhang[0] +
               ", " + arrNhanvien[0] + ", '" + dtp.Value + "', 0 ," + trangthai + ", 0)";
                if (cf.CheckTonTai(sqlStr) == 0)
                {
                    if (cf.IUDDuLieu(insert) == true)
                    {
                        MessageBox.Show("Thêm thành công!");
                        cf.FillDgv(sqlStrdgv, dgv, "tblBill");
                    }
                    else
                    {
                        MessageBox.Show("Thêm thất bại!");
                    }
                }
                else
                {
                    MessageBox.Show("Mã hóa đơn đã tồn tại. Hãy nhập mã hóa đơn khác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Không được để trống dữ liệu!");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
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
                cf.FillDgv(sqlStrdgv, dgv, "tblBill");
            }
            else
            {
                MessageBox.Show("Sửa dữ liệu thất bại!");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string delete = "DELETE FROM tblBIll WHERE totalPrice = 0 AND amount = 0 ";
            if (cf.IUDDuLieu(delete) == true)
            {
                MessageBox.Show("Xoá dữ liệu thành công!");
                cf.FillDgv(sqlStrdgv, dgv, "tblBill");
            }
            else
            {
                MessageBox.Show("Xóa dữ liệu thất bại!");
            }
        }

        private void btnHienchitiet_Click(object sender, EventArgs e)
        {
            FormBillDetail fbd = new FormBillDetail(txtMahoadon.Text);
            fbd.MdiParent = FormMDI.ActiveForm;
            fbd.Show();
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {


            string select = "  SELECT idBill AS [Mã hóa đơn]," +
                "tblBill.idCustomer AS[Mã khách hàng]," +
                "tblCustomer.fullName AS[Tên khách hàng]," +
                "tblBill.idEmployee AS[Mã nhân viên]," +
                "tblEmployee.fullName AS[Tên nhân viên]," +
                "datee AS[Ngày lập],totalPrice AS[Tổng tiền]," +
                "CASE WHEN statuss = 1 THEN N'Đã thanh toán' ELSE N'Chưa thanh toán' END AS[Trạng thái]," +
                "amount AS[Số lượng]" +
                "FROM tblBill, tblCustomer, tblEmployee " +
                "WHERE tblBill.idCustomer = tblCustomer.idCustomer " +
                "AND tblBill.idEmployee = tblEmployee.idEmployee";

            if (chkNhanvien.Checked == true && chkKhachhang.Checked == false && chkTongtien.Checked == false)
            {
                string[] arrNhanvien = cbNhanvien.SelectedItem.ToString().Split('.');
                string Manhanvien = select + " AND tblBill.idEmployee = " + arrNhanvien[0];
                cf.FillDgv(Manhanvien, dgv, "tblBill");
            }
            else if (chkNhanvien.Checked == true && chkKhachhang.Checked == true && chkTongtien.Checked == false)
            {
                string[] arrNhanvien = cbNhanvien.SelectedItem.ToString().Split('.');
                string[] arrKhachhang = cbKhachhang.SelectedItem.ToString().Split('.');
                string Manhanvien = select + " AND tblBill.idEmployee = " + arrNhanvien[0];
                string Manhanvien_Khachhang = Manhanvien + " AND tblBill.idCustomer = " + arrKhachhang[0];
                cf.FillDgv(Manhanvien_Khachhang, dgv, "tblBill");
            }
            else if (chkNhanvien.Checked == true && chkKhachhang.Checked == true && chkTongtien.Checked == true)
            {
                string[] arrNhanvien = cbNhanvien.SelectedItem.ToString().Split('.');
                string[] arrKhachhang = cbKhachhang.SelectedItem.ToString().Split('.');
                string Manhanvien = select + " AND tblBill.idEmployee = " + arrNhanvien[0];
                string Manhanvien_Khachhang = Manhanvien + " AND tblBill.idCustomer = " + arrKhachhang[0];
                string Manhanvien_Khachhang_Tongtien = Manhanvien_Khachhang + " AND totalPrice BETWEEN " + txtTu.Text + " AND " + txtDen.Text;
                cf.FillDgv(Manhanvien_Khachhang_Tongtien, dgv, "tblBill");

            }
            else if (chkNhanvien.Checked == true && chkKhachhang.Checked == false && chkTongtien.Checked == true)
            {
                string[] arrNhanvien = cbNhanvien.SelectedItem.ToString().Split('.');
                string Manhanvien = select + " AND tblBill.idEmployee = " + arrNhanvien[0];
                string Manhanvien_Tongtien = Manhanvien + " AND totalPrice BETWEEN " + txtTu.Text + " AND " + txtDen.Text;
                cf.FillDgv(Manhanvien_Tongtien, dgv, "tblBill");

            }
            else if (chkNhanvien.Checked == false && chkKhachhang.Checked == true && chkTongtien.Checked == false)
            {
                string[] arrKhachhang = cbKhachhang.SelectedItem.ToString().Split('.');
                string Makhachhang = select + " AND tblBill.idCustomer = " + arrKhachhang[0];
                cf.FillDgv(Makhachhang, dgv, "tblBill");
            }
            else if (chkNhanvien.Checked == false && chkKhachhang.Checked == true && chkTongtien.Checked == true)
            {
                string[] arrKhachhang = cbKhachhang.SelectedItem.ToString().Split('.');
                string Makhachhang = select + " AND tblBill.idCustomer = " + arrKhachhang[0];
                string Makhachhang_Tongtien = Makhachhang + " AND totalPrice BETWEEN " + txtTu.Text + " AND " + txtDen.Text;
                cf.FillDgv(Makhachhang_Tongtien, dgv, "tblBill");
            }
            else if (chkNhanvien.Checked == false && chkKhachhang.Checked == false && chkTongtien.Checked == true)
            {
                string Tongtien = select + " AND totalPrice BETWEEN " + txtTu.Text + " AND " + txtDen.Text; ;
                cf.FillDgv(Tongtien, dgv, "tblBill");
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

        private void btnIn_Click(object sender, EventArgs e)
        {
            FormCrystal fc = new FormCrystal(txtMahoadon.Text);
            fc.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cf.FillDgv(sqlStrdgv, dgv, "tblBill");
        }
    }
}