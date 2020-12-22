using Project_HoangPhuongAnh.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace Project_HoangPhuongAnh.Models.DAO
{
    public class Tbl_imageDao : BaseDao
    {
        SqlDataReader _sqlReader;

        /// <summary>
        /// Get all image product theo masp
        /// </summary>
        /// <param name="_masp"></param>
        /// <returns></returns>
        public List<Tbl_image> GetListImage(int _masp)
        {
            List<Tbl_image> lstImage = new List<Tbl_image>();
            try
            {
                if (OpenConnection() != null)
                {
                    StringBuilder query = new StringBuilder();
                    query.Append(" select img.hinhanh ");
                    query.Append(" from  tbl_image as img ");
                    query.Append(" inner join tbl_sanpham s  on ");
                    query.Append(" s.masp = img.masp ");
                    
                    if (_masp != 0)
                    {
                        query.Append(" where img.masp = @masp ");
                    }
                    SqlCommand sqlCommand = new SqlCommand(query.ToString(), conn);
                    sqlCommand.Prepare();
                    if (_masp != 0)
                    {
                        sqlCommand.Parameters.AddWithValue("@masp", _masp);

                    }
                    _sqlReader = sqlCommand.ExecuteReader();

                    while (_sqlReader.Read())
                    {
                        Tbl_image tblImage = new Tbl_image();
                        tblImage._hinhanh = _sqlReader["hinhanh"].ToString();
                        

                        lstImage.Add(tblImage);
                    }
                }

            }
            catch (SqlException e)
            {
                Console.WriteLine("Tbl_imageDao : GetListImage " + e.StackTrace);
            }
            finally
            {
                CloseConnection();
            }
            return lstImage;
        }
    }
}