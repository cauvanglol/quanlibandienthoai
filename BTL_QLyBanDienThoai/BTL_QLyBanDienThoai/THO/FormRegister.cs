using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace BTL_QLyBanDienThoai.THO
{
    public partial class FormRegister : Form
    {
        public string strCon = ConfigurationManager.ConnectionStrings["QLBH"].ConnectionString;
        CommonFunction cf = new CommonFunction();

        public FormRegister()
        {
            InitializeComponent();
        }

        private void FormDangky_Load(object sender, EventArgs e)
        {
            string sqlStr1 = "SELECT DISTINCT gender FROM tblEmployee";
            string sqlStr2 = "SELECT DISTINCT rolee FROM tblAccount";
            cf.LoadComboBox(sqlStr1, cbGioitinh, 0);
            cf.LoadComboBox(sqlStr2, cbChucvu, 0);
            picAnMK.Hide();
        }

        private void txtTendangnhap_Validating(object sender, CancelEventArgs e)
        {
            string sqlStr = "SELECT COUNT(*) FROM tblAccount WHERE username = '" + txtTendangnhap.Text + "'";
            if (string.IsNullOrEmpty(txtTendangnhap.Text))
            {
                e.Cancel = true;
                txtTendangnhap.Focus();
                errorProvider.SetError(txtTendangnhap, "Tên đăng nhập không được để trống");
            }
            else if (cf.CheckTonTai(sqlStr) > 0)
            {
                e.Cancel = true;
                txtTendangnhap.Focus();
                errorProvider.SetError(txtTendangnhap, "Tên đăng nhập đã tồn tại");
            }
            else
            {
                errorProvider.Clear();
            }
        }

        private void txtMatkhau2_Validating(object sender, CancelEventArgs e)
        {
            if (txtMatkhau1.Text != txtMatkhau2.Text)
            {
                e.Cancel = true;
                txtMatkhau2.Focus();
                errorProvider.SetError(txtMatkhau2, "Mật khẩu không trùng");
            }
            else
            {
                errorProvider.Clear();
            }
        }

        private void picHienMK_Click_1(object sender, EventArgs e)
        {
            picAnMK.Show();
            picHienMK.Hide();
            txtMatkhau1.PasswordChar = '\0';
            txtMatkhau2.PasswordChar = '\0';
        }

        private void picAnMK_Click_1(object sender, EventArgs e)
        {
            picAnMK.Hide();
            picHienMK.Show();
            txtMatkhau1.PasswordChar = '*';
            txtMatkhau2.PasswordChar = '*';
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            string sqlStr1 = "INSERT INTO tblAccount VALUES('" + txtTendangnhap.Text.Trim()
                + "', '" + txtMatkhau1.Text + "', N'" + cbChucvu.SelectedItem + "')";
            string sqlStr2 = "INSERT INTO tblEmployee VALUES(" + txtManhanvien.Text + ", '" + txtTendangnhap.Text
                + "', N'" + txtHoten.Text.Trim() + "', N'" + cbGioitinh.SelectedItem + "', '" + dtpNgaysinh.Value
                + "', N'" + txtDiachi.Text.Trim() + "', '" + txtSDT.Text + "')";
            string sqlStr3 = "SELECT COUNT(*) FROM tblAccount WHERE username = '" + txtTendangnhap.Text + "'";
            string sqlStr4 = "SELECT COUNT(*) FROM tblEmployee WHERE idEmployee = '" + txtManhanvien.Text + "'";
            if (txtMatkhau1.Text == txtMatkhau2.Text)
            {
                cf.IUDDuLieu(sqlStr1);
                cf.IUDDuLieu(sqlStr2);
                if (cf.CheckTonTai(sqlStr3) > 0 && cf.CheckTonTai(sqlStr4) > 0)
                {
                    MessageBox.Show("Đăng ký thành công");
                    Hide();
                    FormLogin fl = new FormLogin();
                    fl.Show();
                }
                else
                {
                    MessageBox.Show("Đăng ký thất bại");
                }
            }
            else
            {
                MessageBox.Show("Mật khẩu nhập lại không trùng");
            }
        }

        private void txtManhanvien_Validating(object sender, CancelEventArgs e)
        {
            string sqlStr = "SELECT COUNT(*) FROM tblEmployee WHERE idEmployee = '" + txtManhanvien.Text + "'";
            if (string.IsNullOrEmpty(txtManhanvien.Text))
            {
                e.Cancel = true;
                txtManhanvien.Focus();
                errorProvider.SetError(txtManhanvien, "Mã nhân viên không được để trống");
            }
            else if (cf.CheckTonTai(sqlStr) > 0)
            {
                e.Cancel = true;
                txtManhanvien.Focus();
                errorProvider.SetError(txtManhanvien, "Mã nhân viên đã tồn tại");
            }
            else
            {
                errorProvider.Clear();
            }
        }
    }
}