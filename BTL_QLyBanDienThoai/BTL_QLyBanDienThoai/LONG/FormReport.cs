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
    public partial class FormReport : Form
    {
        public FormReport(string v)
        {
            InitializeComponent();
            lbUserName.Text = "User: " + v;
        }
    }
}