using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_QLyBanDienThoai.HON
{
    public partial class FormBillDetail : Form
    {
        CommonFunction cf = new CommonFunction();
        public FormBillDetail(string v)
        {
            InitializeComponent();
            lbMahoadon.Text = v;
        }

        private void FormBillDetail_Load(object sender, EventArgs e)
        {
            string sqlStr = "SELECT idBillDetails AS [Mã chi tiết hóa đơn]" +
                ",idBill AS [Mã hóa đơn],d.idDevice AS [Mã điện thoại]" +
                ",deviceName AS [Tên điện thoại],w.idWarranty AS [Mã bảo hành]," +
                "typee AS [Loại bảo hành],bd.price AS [Thành tiền]" +
                ",VAT AS [Thuế VAT],sale AS [Giảm giá] FROM tblBillDetails bd " +
                "INNER JOIN tblDevice d ON d.idDevice= bd.idDevice " +
                "INNER JOIN tblWarranty w ON w.idWarranty=bd.idWarranty WHERE idBill='" + lbMahoadon.Text + "'";
            cf.FillDgv(sqlStr, dgvChitiet, "tblBillDetails");
        }
    }
}
