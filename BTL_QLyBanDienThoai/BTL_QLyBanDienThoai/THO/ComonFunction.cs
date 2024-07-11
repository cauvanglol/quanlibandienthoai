using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.Data;
namespace BTL_QLyBanDienThoai.THO
{
    class CommonFunction
    {
        public string cnt = ConfigurationManager.ConnectionStrings["QLBH"].ConnectionString;

        public void LoadComboBox(string s, ComboBox cb, int column)
        {
            using (SqlConnection sqlCon = new SqlConnection(cnt))
            {
                sqlCon.Open();
                using (SqlCommand sqlCmd = new SqlCommand(s, sqlCon))
                {
                    sqlCmd.CommandType = CommandType.Text;
                    cb.Items.Clear();
                    using (SqlDataReader sqlRead = sqlCmd.ExecuteReader())
                    {
                        while (sqlRead.Read())
                        {
                            string t = sqlRead.GetString(column);
                            cb.Items.Add(t);
                        }
                    }
                }
            }

        }
        public void LoadComboBox1int(string s, ComboBox cb, int column)
        {
            using (SqlConnection sqlCon = new SqlConnection(cnt))
            {
                sqlCon.Open();
                using (SqlCommand sqlCmd = new SqlCommand(s, sqlCon))
                {
                    sqlCmd.CommandType = CommandType.Text;
                    cb.Items.Clear();
                    using (SqlDataReader sqlRead = sqlCmd.ExecuteReader())
                    {
                        while (sqlRead.Read())
                        {
                            string t = sqlRead.GetInt32(column).ToString();
                            cb.Items.Add(t);
                        }
                    }
                }
            }

        }

        public void LoadComboBox2Data(string s, ComboBox cb)
        {
            using (SqlConnection sqlCon = new SqlConnection(cnt))
            {
                sqlCon.Open();
                using (SqlCommand sqlCmd = new SqlCommand(s, sqlCon))
                {
                    sqlCmd.CommandType = CommandType.Text;
                    cb.Items.Clear();
                    using (SqlDataReader sqlRead = sqlCmd.ExecuteReader())
                    {
                        while (sqlRead.Read())
                        {
                            string t = sqlRead.GetInt32(0).ToString();
                            string v = sqlRead.GetString(1);
                            string line = t + "." + v;
                            cb.Items.Add(line);
                        }
                    }
                }
            }

        }

        public int CheckTonTai(string s)
        {
            using (SqlConnection sqlCon = new SqlConnection(cnt))
            {
                sqlCon.Open();
                using (SqlCommand sqlCmd = new SqlCommand(s, sqlCon))
                {
                    sqlCmd.CommandType = CommandType.Text;
                    int soluong = (int)sqlCmd.ExecuteScalar();
                    sqlCon.Close();
                    return soluong;
                }
            }
        }

        public bool IUDDuLieu(string s)
        {
            using (SqlConnection sqlCon = new SqlConnection(cnt))
            {
                sqlCon.Open();
                using (SqlCommand sqlCmd = new SqlCommand(s, sqlCon))
                {
                    sqlCmd.CommandType = CommandType.Text;
                    int ketqua = sqlCmd.ExecuteNonQuery();
                    if (ketqua > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public void FillDgv(string s, DataGridView dgv, string nametable)
        {
            using (SqlConnection sqlCon = new SqlConnection(cnt))
            {
                sqlCon.Open();
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(s, sqlCon);
                SqlCommandBuilder sqlBuider = new SqlCommandBuilder(sqlAdapter);
                DataSet ds = new DataSet();
                sqlAdapter.Fill(ds, nametable);
                dgv.DataSource = ds.Tables[nametable];
            }
        }
    }
}
