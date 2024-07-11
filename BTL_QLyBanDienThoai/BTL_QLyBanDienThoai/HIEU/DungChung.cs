using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
namespace BTL_QLyBanDienThoai.HIEU
{
    class DungChung
    {
        //khởi tạo kết nối ban đầu
        SqlCommand command;
        SqlDataAdapter adapter = new SqlDataAdapter();
        public DataTable table = new DataTable();
        string strCon = @"Data Source=NGOQUANGHON\SQLEXPRESS;Initial Catalog = QLyBanDienThoai; Persist Security Info=True;User ID = sa; Password=1";
        SqlConnection sqlCon = null;
        public DungChung()
        {
        }
        private void checkCon()//kiểm tra kết nối
        {
            if (sqlCon == null)//Nếu kết nối null thì khởi tạo
            {
                sqlCon = new SqlConnection(strCon);
            }
            if (sqlCon.State == ConnectionState.Closed)//Nếu kết nối đóng thì mở
            {
                sqlCon.Open();
            }
        }
        public void selectCom(string s)//thực hiện lệnh select s bất kì
        {
            checkCon();
            command = sqlCon.CreateCommand();
            command.CommandText = s;
            adapter.SelectCommand = command;
        }
        public void executeCom(string s)// thực hiện lệnh s bất kì
        {
            checkCon();
            command = sqlCon.CreateCommand();
            command.CommandText = s;
            command.ExecuteNonQuery();
        }
        public string getRole(string s)// lấy role từ bảng nhân viên
        {
            checkCon();
            command = sqlCon.CreateCommand();
            command.CommandText = s;
            if (command.ExecuteScalar() == null)
            {
                return "";
            }
            else
            {
                return command.ExecuteScalar().ToString();
            }
        }
        public int count(string s)//thực hiện lệnh đếm s
        {
            checkCon();
            command = sqlCon.CreateCommand();
            command.CommandText = s;
            return (int)command.ExecuteScalar();
        }
        public void loadData(DataGridView dgv)// đẩy dữ liệu từ table vào một data grid view bất kì
        {
            checkCon();
            table.Clear();
            adapter.Fill(table);
            dgv.DataSource = table;
        }
        public void loadData(string s, DataGridView dgv)// đẩy dữ liệu từ table vào data grid view có lệnh select
        {
            selectCom(s);
            table.Clear();
            adapter.Fill(table);
            dgv.DataSource = table;
        }
        public void LoadComboBox2Data(string s, ComboBox cb)
        {
            using (SqlConnection sqlCon = new SqlConnection(strCon))
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
        public void LoadComboBox2INT(string s, ComboBox cb)
        {
            using (SqlConnection sqlCon = new SqlConnection(strCon))
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
                            string v = sqlRead.GetInt32(1).ToString();
                            string line = t + "." + v + " tháng";
                            cb.Items.Add(line);
                        }
                    }
                }
            }
        }
        public int CheckTonTai(string s)
        {
            using (SqlConnection sqlCon = new SqlConnection(strCon))
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
            using (SqlConnection sqlCon = new SqlConnection(strCon))
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
            using (SqlConnection sqlCon = new SqlConnection(strCon))
            {
                sqlCon.Open();
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(s, sqlCon);
                SqlCommandBuilder sqlBuider = new SqlCommandBuilder(sqlAdapter);
                DataSet ds = new DataSet();
                sqlAdapter.Fill(ds, nametable);
                dgv.DataSource = ds.Tables[nametable];
            }
        }
        public void LoadComboBox1int(string s, ComboBox cb, int column)
        {
            using (SqlConnection sqlCon = new SqlConnection(strCon))
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
    }

}
