using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
//Bài làm của Đỗ Thành Long - 22A1001D0201

namespace BTL_QLyBanDienThoai.LONG
{
    class CodeChung
    {
        //khởi tạo kết nối ban đầu
        SqlCommand command;
        SqlDataAdapter adapter = new SqlDataAdapter();
        public DataTable table = new DataTable();
        public string cnt = ConfigurationManager.ConnectionStrings["QLBH"].ConnectionString;
        SqlConnection sqlCon = null;
        public CodeChung()
        {
        }
        private void checkCon()//kiểm tra kết nối
        {
            if (sqlCon == null)//Nếu kết nối null thì khởi tạo
            {
                sqlCon = new SqlConnection(cnt);
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
    }
}
