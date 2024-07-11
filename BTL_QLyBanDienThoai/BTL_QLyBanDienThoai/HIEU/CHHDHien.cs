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

namespace BTL_QLyBanDienThoai.HIEU
{
    public partial class CHHDHien : Form
    {
        DungChung cf = new DungChung();
        public CHHDHien(string v)
        {
            InitializeComponent();
            lbMaHoadon.Text = v;
        }

        private void CHHDHien_Load(object sender, EventArgs e)
        {
            string sqlStr = "SELECT * FROM v_CTHD WHERE v_CTHD.[Mã hóa đơn]=" + lbMaHoadon.Text;
            cf.FillDgv(sqlStr, dgvChitiet, "v_CTHD");
        }
    }
}
