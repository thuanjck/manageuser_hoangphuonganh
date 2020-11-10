using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Project_HoangPhuongAnh.Areas.Admin.DAO
{
    public class BaseDao
    {
        private string connectionString =
            "Data Source=TAVIETTHUAN\\SQLEXPRESS;Initial Catalog=manageuser_hoangphuonganh;Integrated Security=True";

        protected SqlConnection conn;

        /// <summary>
        /// Mở connection
        /// Create by ThuanTV 29/11/2019
        /// Created ThuanTV
        /// </summary>
        public SqlConnection OpenConnection()
        {
            try
            {
                conn = new SqlConnection(connectionString);

                conn.Open();
            }
            catch (Exception e)
            {
                throw e;
            }
            return conn;
        }

        /// <summary>
        /// Hàm đóng connection
        /// Create by ThuanTV 29/11/2019
        /// </summary>
        public void CloseConnection()
        {
            try
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Hàm get ra các column trong db
        /// Create by ThuanTV 2020/03/05
        /// </summary>
        /// <returns></returns>
        public List<string> GetColumnDB()
        {
            List<string> whitelist = new List<string>();
            try
            {
                if (OpenConnection() != null)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS");
                    stringBuilder.ToString();

                    using (SqlCommand command = new SqlCommand(stringBuilder.ToString(), conn))
                    {
                        SqlDataReader sqlDataReader = command.ExecuteReader();

                        while (sqlDataReader.Read())
                        {
                            whitelist.Add(sqlDataReader["COLUMN_NAME"].ToString());
                        }
                    }
                }
                return whitelist;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
    }
}