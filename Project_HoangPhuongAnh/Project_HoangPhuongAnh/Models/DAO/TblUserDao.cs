using Project_HoangPhuongAnh.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace Project_HoangPhuongAnh.Models.DAO
{
    public class TblUserDao : BaseDao
    {
        SqlDataReader _sqlReader;
        /// <summary>
        /// hàm kiểm tra đăng nhập
        /// Create by ThuanTV 06/12/2019
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CheckLogin(string email, string password)
        {
            Boolean _result = false;
            try
            {
                if (OpenConnection() != null)
                {
                    StringBuilder query = new StringBuilder();
                    query.Append("use manageuser_hoangphuonganh;");
                    query.Append(" select * from tbl_user as u");
                    query.Append(" where u.email = @email ");
                    query.Append(" and u.password = @password");
                    query.Append(" and u.role = 0");
                    SqlCommand sqlCommand = new SqlCommand(query.ToString(), conn);
                    sqlCommand.Prepare();
                    sqlCommand.Parameters.AddWithValue("@email", email);
                    sqlCommand.Parameters.AddWithValue("@password", password);
                    _sqlReader = sqlCommand.ExecuteReader();
                    _result = _sqlReader.Read();
                }
            }
            catch (SqlException e)
            {
                // ném lỗi
                throw e;
            }
            finally
            {
                // ĐÓng kết nối với DB.
                CloseConnection();
            }
            return _result;
        }
    }
}