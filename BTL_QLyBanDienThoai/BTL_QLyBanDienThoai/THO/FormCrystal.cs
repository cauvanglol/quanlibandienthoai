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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace BTL_QLyBanDienThoai.THO
{
    public partial class FormCrystal : Form
    {
        string strCon = ConfigurationManager.ConnectionStrings["QLBH"].ConnectionString;
        public FormCrystal(string s)
        {
            InitializeComponent();
            lbMahoadon.Text = s;
        }

        private void InBill()
        {
            ReportDocument cryRpt = new ReportDocument();
            // LoadCR();
            using (SqlConnection sqlCon = new SqlConnection(strCon))
            {
                using (SqlCommand sqlCmd = new SqlCommand("LayDataBaoCao", sqlCon))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@mahoadon", lbMahoadon.Text);

                    // Mở kết nối và thực thi stored procedure
                    sqlCon.Open();
                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    // Tạo một DataTable và điền dữ liệu từ SqlDataReader vào nó
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    // Đóng kết nối
                    sqlCon.Close();

                    // Load báo cáo
                    cryRpt.Load(@"D:\C #\BTL_QLyBanDienThoai\BTL_QLyBanDienThoai\CrystalReport.rpt");

                    // Đặt nguồn dữ liệu cho báo cáo
                    cryRpt.SetDataSource(dt);

                    // Gắn báo cáo vào CrystalReportViewer
                    crystalReportViewer1.ReportSource = cryRpt;
                    crystalReportViewer1.RefreshReport();
                }
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            InBill();
        }
    }
}