using Project_HoangPhuongAnh.Models.Entities;
using Project_HoangPhuongAnh.Utils;
using System;
using System.Collections.Generic;
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

        /// <summary>
        /// Hàm lấy ra tất cả user không phải là admin
        /// Create by ThuanTV 11/27/2019
        /// </summary>
        /// <returns></returns>
        public List<Tbl_user> GetAllUser(int offset, int limit, string full_name, string sortType, string sortByFullName)
        {
            List<Tbl_user> listUserInfor = new List<Tbl_user>();
            try
            {
                if (OpenConnection() != null)
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append("SELECT u.user_id, u.email, u.full_name, u.tel, u.birthday, u.address_user ");
                    builder.Append(" FROM tbl_user u  ");
                    builder.Append(" WHERE u.role = @rule ");
                    
                    //kiểm tra full_name khác rỗng
                    if (!string.IsNullOrEmpty(full_name))
                    {
                        builder.Append(" AND full_name LIKE @full_name ESCAPE '!' ");
                    }
                    //điều kiện sort
                    List<string> whiteList = new List<string>();
                    whiteList = GetColumnDB();
                    if (whiteList.Contains(sortType))
                    {
                        if ("full_name".Equals(sortType))
                        {
                            builder.Append(" ORDER BY u.full_name ");
                            builder.Append(sortByFullName);
                        }
                    }

                    // add limit và offset
                    builder.Append(" OFFSET @offset");
                    builder.Append(" ROWS FETCH NEXT @limit");
                    builder.Append(" ROW ONLY;");

                    builder.ToString();
                    using (SqlCommand command = new SqlCommand(builder.ToString(), conn))
                    {

                        command.Parameters.AddWithValue("@rule", Constants.RULE_USER);

                        if (!string.IsNullOrEmpty(full_name))
                        {
                            command.Parameters.AddWithValue("@full_name", "%" + Common.ReplaceWildCard(full_name) + "%");
                        }
                        command.Parameters.AddWithValue("@limit", limit);
                        command.Parameters.AddWithValue("@offset", offset);
                        _sqlReader = command.ExecuteReader();

                        while (_sqlReader.Read())
                        {
                            Tbl_user user = new Tbl_user();
                            user._user_id = (int)_sqlReader["user_id"];
                            user._email = _sqlReader["email"].ToString();
                            user._full_name = _sqlReader["full_name"].ToString();
                            user._tel = _sqlReader["tel"].ToString();
                            user._birthday = (DateTime)_sqlReader["birthday"];
                            user._address_user = _sqlReader["address_user"].ToString();

                            listUserInfor.Add(user);
                        }

                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("TblUserDao : GetAllUser " + e.StackTrace);
            }
            finally
            {
                CloseConnection();
            }
            return listUserInfor;
        }

        /// <summary>
        /// hàm kiểm tra tồn tại user
        /// Create by ThuanTV 06/12/2019
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public bool CheckExistedUserID(int user_id)
        {
            Boolean _result = false;
            try
            {
                if (OpenConnection() != null)
                {
                    StringBuilder query = new StringBuilder();
                    query.Append(" select * from tbl_user as u");
                    query.Append(" where u.user_id = @user_id ");
                    SqlCommand sqlCommand = new SqlCommand(query.ToString(), conn);
                    sqlCommand.Prepare();
                    sqlCommand.Parameters.AddWithValue("@user_id", user_id);
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
        /// get data userInfor theo user)id
        /// Create by ThuanTV 11/27/2019
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public Tbl_user GetUserByUserID(int user_id)
        {
            Tbl_user userInfor = new Tbl_user();
            try
            {
                if (OpenConnection() != null)
                {
                    StringBuilder query = new StringBuilder();
                    query.Append("SELECT u.user_id, u.email, u.full_name, u.tel, u.birthday, u.address_user, u.role");
                    query.Append("FROM tbl_user as u ");
                    query.Append("WHERE u.user_id= @user_id ;");


                    using (SqlCommand command = new SqlCommand(query.ToString(), conn))
                    {
                        command.Parameters.AddWithValue("@user_id", user_id);

                        _sqlReader = command.ExecuteReader();
                        while (_sqlReader.Read())
                        {

                            userInfor._user_id = (int)_sqlReader["user_id"];
                            userInfor._email = _sqlReader["email"].ToString().Trim();
                            userInfor._full_name = _sqlReader["full_name"].ToString().Trim();
                            userInfor._tel = _sqlReader["tel"].ToString().Trim();
                            userInfor._birthday = (DateTime)_sqlReader["birthday"];
                            userInfor._address_user = _sqlReader["address_user"].ToString().Trim();
                            userInfor._role = (int)_sqlReader["role"];
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                CloseConnection();
            }
            return userInfor;
        }
    }
}