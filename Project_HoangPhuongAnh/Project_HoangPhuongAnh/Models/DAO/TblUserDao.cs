using Project_HoangPhuongAnh.Utils;
using System;
using System.Data.SqlClient;
using System.Text;

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

        public bool CheckAdmin(string email)
        {
            Boolean _result = false;
            try
            {
                if (OpenConnection() != null)
                {
                    StringBuilder query = new StringBuilder();
                    query.Append(" select * from tbl_user as u");
                    query.Append(" where u.email = @email ");
                    query.Append(" and u.role = 0");
                    SqlCommand sqlCommand = new SqlCommand(query.ToString(), conn);
                    sqlCommand.Prepare();
                    sqlCommand.Parameters.AddWithValue("@email", email);
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

        /// <summary>
        /// lấy ra tổng số user trong bảng tbl_user
        /// Create by ThuanTV 11/27/2019
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="fullName"></param>
        /// <returns></returns>
        public int GetTotalUsers(string full_name)
        {
            int totalRecord = 0;
            try
            {
                if (OpenConnection() != null)
                {
                    StringBuilder query = new StringBuilder();
                    query.Append("select count(*) as NumTotal from tbl_user u ");
                    query.Append(" where u.role = 1 ");
                    if (full_name != null && full_name != "")
                    {
                        query.Append(" and u.full_name like @full_name ESCAPE '!'  ");
                    }
                    // Khởi tạo
                    SqlCommand comd = new SqlCommand(query.ToString(), conn);
                    comd.Prepare();
                    comd.Parameters.Clear();
                    comd.Parameters.AddWithValue("@full_name", "%" + Common.ReplaceWildCard(full_name) + "%");
                    comd.Connection = conn;
                    _sqlReader = comd.ExecuteReader();
                    if (_sqlReader.Read())
                    {
                        // Lấy ra số lượng.
                        totalRecord = int.Parse(_sqlReader["NumTotal"].ToString());
                    }
                }
            }
            catch (SqlException e)
            {
                // Ném lỗi.
                throw e;
            }
            finally
            {
                // Đóng kết nối với DB
                CloseConnection();
            }
            return totalRecord;
        }
    }
}