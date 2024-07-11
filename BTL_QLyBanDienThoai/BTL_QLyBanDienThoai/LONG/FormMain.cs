using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_QLyBanDienThoai.LONG
{
    public partial class FormMain : Form
    {
        CodeChung chung = new CodeChung();
        public FormMain()
        {
            InitializeComponent();
            
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            chkbFMaDienThoai.Hide();
            chkbFDonGia.Hide();
            chkbFHaiSim.Hide();
            btnTimKiem.Hide();
            cbDonGia.Hide();
            cbHeDieuHanh.Hide();
            chung.loadData("select * from tblDevice", dgv);
        }
        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dgv.CurrentRow.Index;
            txtMaDienThoai.Text = dgv.Rows[i].Cells[0].Value.ToString();
            txtTenDienThoai.Text = dgv.Rows[i].Cells[1].Value.ToString();
            txtManHinh.Text = dgv.Rows[i].Cells[2].Value.ToString();
            txtHeDieuHanh.Text = dgv.Rows[i].Cells[3].Value.ToString();
            txtCamera.Text = dgv.Rows[i].Cells[4].Value.ToString();
            txtCPU.Text = dgv.Rows[i].Cells[5].Value.ToString();
            txtRAM.Text = dgv.Rows[i].Cells[6].Value.ToString();
            txtDungLuong.Text = dgv.Rows[i].Cells[7].Value.ToString();
            chkbHaiSim.Checked = Convert.ToBoolean(dgv.Rows[i].Cells[8].Value);
            txtPin.Text = dgv.Rows[i].Cells[9].Value.ToString();
            txtMauSac.Text = dgv.Rows[i].Cells[10].Value.ToString();
            txtDonGia.Text = dgv.Rows[i].Cells[11].Value.ToString();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            int haiSim = chkbHaiSim.CheckState == CheckState.Checked ? 1 : 0;
            if (chung.count("select count(*) from tblDevice where idDevice = " + txtMaDienThoai.Text) != 0)
            {
                MessageBox.Show("Mã điện thoại đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult confirm = MessageBox.Show("Bạn có muốn thêm trường này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    chung.executeCom("insert into tblDevice values (" + txtMaDienThoai.Text + ",'"
                        + txtTenDienThoai.Text + "','" + txtManHinh.Text + "','" + txtHeDieuHanh.Text + "','"
                        + txtCamera.Text + "','" + txtCPU.Text + "','" + txtRAM.Text + "','" + txtDungLuong.Text
                        + "'," + haiSim + ",'" + txtPin.Text + "',N'" + txtMauSac.Text + "'," + txtDonGia.Text + ")");
                    chung.loadData("select * from tblDevice", dgv);
                }
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            int haiSim = chkbHaiSim.CheckState == CheckState.Checked ? 1 : 0;
            if (chung.count("select count(*) from tblDevice where idDevice = " + txtMaDienThoai.Text) == 0)
            {
                MessageBox.Show("Mã điện thoại không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult confirm = MessageBox.Show("Bạn có muốn sửa trường này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    chung.executeCom("UPDATE tblDevice set idDevice = " + txtMaDienThoai.Text + ", deviceName = '"
                        + txtTenDienThoai.Text + "', screen = '" + txtManHinh.Text + "', operatingSystem = '"
                        + txtHeDieuHanh.Text + "', camera = '" + txtCamera.Text + "', CPU = '" + txtCPU.Text
                        + "', RAM = '" + txtRAM.Text + "', memory = '" + txtDungLuong.Text + "', SIM = " + haiSim
                        + ", battery = '" + txtPin.Text + "', color = N'" + txtMauSac.Text + "', price = "
                        + txtDonGia.Text + " WHERE idDevice = " + txtMaDienThoai.Text + ";");
                    chung.loadData("select * from tblDevice", dgv);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (chung.count("select count(*) from tblBillDetails where idDevice = " + txtMaDienThoai.Text) != 0)
            {
                MessageBox.Show("Không được xoá thiết bị này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult confirm = MessageBox.Show("Bạn có muốn xoá trường này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    chung.executeCom("DELETE FROM tblDevice WHERE idDevice = " + txtMaDienThoai.Text);
                    chung.loadData(dgv);
                }
            }
        }

        private void chkbTimkiem_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbTimkiem.Checked == true)
            {
                chkbFMaDienThoai.Show();
                chkbFDonGia.Show();
                chkbFHaiSim.Show();
                chkbFHeDieuHanh.Show();
                btnThem.Hide();
                btnSua.Hide();
                btnXoa.Hide();
                btnIn.Hide();
                txtMaDienThoai.Enabled = false;
                txtTenDienThoai.Enabled = false;
                txtManHinh.Enabled = false;
                txtHeDieuHanh.Enabled = false;
                txtCamera.Enabled = false;
                txtCPU.Enabled = false;
                txtRAM.Enabled = false;
                txtDungLuong.Enabled = false;
                chkbHaiSim.Enabled = false;
                txtPin.Enabled = false;
                txtMauSac.Enabled = false;
                txtDonGia.Enabled = false;
            }
            else
            {
                chkbFMaDienThoai.Checked = false;
                chkbFDonGia.Checked = false;
                chkbFHaiSim.Checked = false;
                chkbFHeDieuHanh.Checked = false;
                chkbFMaDienThoai.Hide();
                chkbFDonGia.Hide();
                chkbFHaiSim.Hide();
                chkbFHeDieuHanh.Hide();
                btnThem.Show();
                btnSua.Show();
                btnXoa.Show();
                btnIn.Show();
                txtMaDienThoai.Enabled = true;
                txtTenDienThoai.Enabled = true;
                txtManHinh.Enabled = true;
                txtHeDieuHanh.Enabled = true;
                txtCamera.Enabled = true;
                txtCPU.Enabled = true;
                txtRAM.Enabled = true;
                txtDungLuong.Enabled = true;
                chkbHaiSim.Enabled = true;
                txtPin.Enabled = true;
                txtMauSac.Enabled = true;
                txtDonGia.Enabled = true;
                chung.loadData("select * from tblDevice", dgv);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (chkbTimkiem.Checked == true)
            {
                if (chkbFMaDienThoai.Checked == true && txtMaDienThoai.Text != null && txtMaDienThoai.Text != "")
                {
                    chung.selectCom("select * from tblDevice WHERE idDevice = " + txtMaDienThoai.Text + "");
                }
                else if (chkbFHaiSim.Checked == true)
                {
                    int haiSim = chkbHaiSim.CheckState == CheckState.Checked ? 1 : 0;
                    if (chkbFHeDieuHanh.Checked == true && cbHeDieuHanh.Text != null && cbHeDieuHanh.Text != "")
                    {
                        chung.selectCom("select * from tblDevice WHERE operatingSystem = '" + cbHeDieuHanh.Text + "' and SIM = " + haiSim);
                    }
                    else
                    {
                        chung.selectCom("select * from tblDevice WHERE SIM = " + haiSim);
                    }
                }
                else if (chkbFDonGia.Checked == true && cbDonGia.Text != null && cbDonGia.Text != "")
                {
                    switch (cbDonGia.Text)
                    {
                        case "<500":
                            chung.selectCom("select * from tblDevice WHERE price < 500");
                            break;
                        case "500-1000":
                            chung.selectCom("select * from tblDevice WHERE price between 500 and 1000");
                            break;
                        case ">1000":
                            chung.selectCom("select * from tblDevice WHERE price > 1000");
                            break;
                        default:
                            MessageBox.Show("Không thể tìm kiếm! Vui lòng kiểm tra lại tùy chọn tìm kiếm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                }
                else if (chkbFHeDieuHanh.Checked == true && cbHeDieuHanh.Text != null && cbHeDieuHanh.Text != "")
                {
                    chung.selectCom("select * from tblDevice WHERE operatingSystem = '" + cbHeDieuHanh.Text + "'");
                }
                else
                {
                    MessageBox.Show("Không thể tìm kiếm! Vui lòng kiểm tra lại tùy chọn tìm kiếm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Không thể tìm kiếm! Vui lòng kiểm tra lại tùy chọn tìm kiếm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            chung.loadData(dgv);
        }

        private void chkbFMaDienThoai_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbFMaDienThoai.Checked == true)
            {
                btnTimKiem.Show();
                chkbFDonGia.Hide();
                chkbFHaiSim.Hide();
                chkbFHeDieuHanh.Hide();
                chkbFDonGia.Checked = false;
                chkbFHaiSim.Checked = false;
                chkbFHeDieuHanh.Checked = false;
                txtMaDienThoai.Enabled = true;
            }
            else
            {
                btnTimKiem.Hide();
                chkbFDonGia.Show();
                chkbFHaiSim.Show();
                chkbFHeDieuHanh.Show();
                txtMaDienThoai.Enabled = false;
            }
        }

        private void chkbFDonGia_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbFDonGia.Checked == true)
            {
                btnTimKiem.Show();
                chkbFMaDienThoai.Hide();
                chkbFHaiSim.Hide();
                chkbFHeDieuHanh.Hide();
                chkbFMaDienThoai.Checked = false;
                chkbFHaiSim.Checked = false;
                chkbFHeDieuHanh.Checked = false;
                txtDonGia.Hide();
                cbDonGia.Show();
                string[] items = { "<500", "500-1000", ">1000" };
                cbDonGia.Items.AddRange(items);
            }
            else
            {
                btnTimKiem.Hide();
                chkbFMaDienThoai.Show();
                chkbFHaiSim.Show();
                chkbFHeDieuHanh.Show();
                txtDonGia.Show();
                cbDonGia.Hide();
                cbDonGia.Items.Clear();
            }
        }

        private void chkbFHaiSim_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbFHaiSim.Checked == true)
            {
                btnTimKiem.Show();
                chkbFMaDienThoai.Hide();
                chkbFDonGia.Hide();
                chkbFMaDienThoai.Checked = false;
                chkbFDonGia.Checked = false;
                chkbHaiSim.Enabled = true;
            }
            else
            {
                if (!chkbFHeDieuHanh.Checked)
                {
                    btnTimKiem.Hide();
                }
                chkbFMaDienThoai.Show();
                chkbFDonGia.Show();
                chkbFHeDieuHanh.Show();
                chkbHaiSim.Enabled = false;
            }
        }

        private void chkbFHeDieuHanh_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbFHeDieuHanh.Checked == true)
            {
                btnTimKiem.Show();
                chkbFMaDienThoai.Hide();
                chkbFDonGia.Hide();
                chkbFMaDienThoai.Checked = false;
                chkbFDonGia.Checked = false;
                txtHeDieuHanh.Hide();
                cbHeDieuHanh.Show();
                string[] items = { "Android", "IOS" };
                cbHeDieuHanh.Items.AddRange(items);
            }
            else
            {
                if (!chkbFHaiSim.Checked)
                {
                    btnTimKiem.Hide();
                }
                chkbFMaDienThoai.Show();
                chkbFHaiSim.Show();
                chkbFDonGia.Show();
                txtHeDieuHanh.Show();
                cbHeDieuHanh.Hide();
                cbHeDieuHanh.Items.Clear();
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            FormReport fr = new FormReport(lbNguoidung.Text);
            fr.Show();
        }
    }
}