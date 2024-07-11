using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BTL_QLyBanDienThoai.TUAN;
using BTL_QLyBanDienThoai.LONG;
using BTL_QLyBanDienThoai.HON;
using BTL_QLyBanDienThoai.HIEU;
namespace BTL_QLyBanDienThoai.THO
{
    public partial class FormMDI : Form
    {
        FormBill fb;
        FormRegister fr;

        public FormMDI(string s)
        {
            InitializeComponent();
            lbVaitro.Text = s;
        }

        public FormMDI()
        {
        }

        private void AnForm()
        {
            if (fb != null)
            {
                fb.Hide();
            }
            if (fr != null)
            {
                fr.Hide();
            }
        }
        private void hóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {

            AnForm();
            fb = new FormBill();
            fb.MdiParent = FormMDI.ActiveForm;
            fb.Show();

        }

        private void đăngKýTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lbVaitro.Text.Trim() == "Quản trị viên")
            {
                if (fr == null)
                {
                    AnForm();
                }
                fr = new FormRegister();
                fr.MdiParent = FormMDI.ActiveForm;
                fr.Show();
            }
            else
            {
                MessageBox.Show("Hãy đăng nhập với vai trò Quản trị viên để sử dụng chức năng này");
            }

        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLogin fl = new FormLogin();
            fl.Show();
            Hide();
        }

        private void chiTiếtHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormIDBillDetail fbd = new FormIDBillDetail();
            fbd.MdiParent = FormMDI.ActiveForm;
            fbd.Show();
        }

        private void FormMDI_Load(object sender, EventArgs e)
        {
            lbVaitro.Show();
        }

        private void dịchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Baohanh bh = new Baohanh();
            //bh.MdiParent = FormMDI.ActiveForm;
            //bh.Show();
        }

        private void sảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMain dt = new FormMain();
            dt.MdiParent = FormMDI.ActiveForm;
            dt.Show();
        }

        private void nhânSựToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string v;
            //QLDanhMuc dm = new QLDanhMuc(v);
            //dm.MdiParent = this.MdiParent;
            //dm.Show();
        }
    }
}