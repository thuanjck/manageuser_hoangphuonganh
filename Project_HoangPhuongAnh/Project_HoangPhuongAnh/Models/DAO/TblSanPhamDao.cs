using Project_HoangPhuongAnh.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace Project_HoangPhuongAnh.Models.DAO
{
    public class TblSanPhamDao : BaseDao
    {
        SqlDataReader _sqlReader;

        public List<Tbl_sanpham> getAllProductWithTyle(int _maloai, int _offset, int _limit)
        {
            List<Tbl_sanpham> lstSanPhams = new List<Tbl_sanpham>();
            try
            {
                if (OpenConnection() != null)
                {
                    StringBuilder query = new StringBuilder();
                    query.Append("SELECT * FROM tbl_sanpham WHERE maloai = @maloai");
                    // order by theo truong maloai
                    query.Append(" order by maloai ");
                    // add limit và offset
                    query.Append(" OFFSET @offset");
                    query.Append(" ROWS FETCH NEXT @limit");
                    query.Append(" ROW ONLY;");
                    SqlCommand sqlCommand = new SqlCommand(query.ToString(), conn);
                    sqlCommand.Prepare();
                    sqlCommand.Parameters.AddWithValue("@maloai", _maloai);
                    sqlCommand.Parameters.AddWithValue("@offset", _offset);
                    sqlCommand.Parameters.AddWithValue("@limit", _limit);
                    _sqlReader = sqlCommand.ExecuteReader();

                    while (_sqlReader.Read())
                    {
                        Tbl_sanpham tblSanPham = new Tbl_sanpham();
                        tblSanPham._masp = (int)_sqlReader["masp"];
                        tblSanPham._maloai = (int)_sqlReader["maloai"];
                        tblSanPham._tensanpham = _sqlReader["tensanpham"].ToString();
                        tblSanPham._gia_mua = (int)_sqlReader["gia_mua"];
                        tblSanPham._gia_ban = (int)_sqlReader["gia_ban"];
                        tblSanPham._size = _sqlReader["size"].ToString();
                        tblSanPham._soluong = (int)_sqlReader["soluong"];
                        tblSanPham._thongtin = _sqlReader["thongtin"].ToString();
                        tblSanPham._ngaynhaphang = (DateTime)_sqlReader["ngaynhaphang"];
                        tblSanPham._hinhanh = _sqlReader["hinhanh"].ToString();

                        lstSanPhams.Add(tblSanPham);
                    }
                }
                
            }
            catch (SqlException e)
            {
                Console.WriteLine("TblSanPhamDao : getAllProductWithTyle " + e.StackTrace);
            }
            finally
            {
                CloseConnection();
            }
            return lstSanPhams;
        }

        public int GetTotalproduct(int _maloai)
        {
            int totalProduct = 0;
            try
            {
                if (OpenConnection() != null)
                {
                    StringBuilder query = new StringBuilder();
                    query.Append("select count(*) as NumTotal from tbl_sanpham s ");
                    if (_maloai != 0)
                    {
                        query.Append(" where s.maloai = @maloai");
                    }
                    // Khởi tạo
                    SqlCommand comd = new SqlCommand(query.ToString(), conn);
                    comd.Prepare();
                    comd.Parameters.Clear();
                    comd.Parameters.AddWithValue("@maloai", _maloai);
                    comd.Connection = conn;
                    _sqlReader = comd.ExecuteReader();
                    if (_sqlReader.Read())
                    {
                        // Lấy ra số lượng.
                        totalProduct = int.Parse(_sqlReader["NumTotal"].ToString());
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
            return totalProduct;
        }

        public Tbl_sanpham GetProductWithID(int _masp)
        {
            Tbl_sanpham product = new Tbl_sanpham();
            try
            {
                if (OpenConnection() != null)
                {
                    StringBuilder query = new StringBuilder();
                    query.Append("select * from tbl_sanpham s ");
                    if (_masp != 0)
                    {
                        query.Append(" where s.masp = @masp");
                    }
                    // Khởi tạo
                    SqlCommand comd = new SqlCommand(query.ToString(), conn);
                    comd.Prepare();
                    comd.Parameters.Clear();
                    comd.Parameters.AddWithValue("@masp", _masp);
                    comd.Connection = conn;
                    _sqlReader = comd.ExecuteReader();
                    while (_sqlReader.Read())
                    {
                        product._masp = (int)_sqlReader["masp"];
                        product._maloai = (int)_sqlReader["maloai"];
                        product._tensanpham = _sqlReader["tensanpham"].ToString();
                        product._gia_mua = (int)_sqlReader["gia_mua"];
                        product._gia_ban = (int)_sqlReader["gia_ban"];
                        product._size = _sqlReader["size"].ToString();
                        product._soluong = (int)_sqlReader["soluong"];
                        product._thongtin = _sqlReader["thongtin"].ToString();
                        product._ngaynhaphang = (DateTime)_sqlReader["ngaynhaphang"];
                        product._hinhanh = _sqlReader["hinhanh"].ToString();
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
            return product;
        }
    }
}