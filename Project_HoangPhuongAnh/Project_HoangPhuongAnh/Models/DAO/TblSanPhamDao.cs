using Project_HoangPhuongAnh.Models.Entities;
using Project_HoangPhuongAnh.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace Project_HoangPhuongAnh.Models.DAO
{
    public class TblSanPhamDao : BaseDao
    {
        SqlDataReader _sqlReader;

        public List<Tbl_sanpham> GetAllProductWithTyle(int _maloai, int _offset, int _limit, string _tensanpham, int _gia_min, int _gia_max, int _discount)
        {
            List<Tbl_sanpham> lstSanPhams = new List<Tbl_sanpham>();
            try
            {
                if (OpenConnection() != null)
                {
                    StringBuilder query = new StringBuilder();
                    query.Append("SELECT * FROM tbl_sanpham WHERE maloai = @maloai ");
                    // Kiểm tra user có search theo tên sản phẩm hay khong
                    if (!string.IsNullOrEmpty(_tensanpham))
                    {
                        query.Append(" AND tensanpham LIKE @tensanpham ESCAPE '!' ");
                    }
                    // kiểm tra user có search sản phẩm theo khoảng giá hay không
                    if (_gia_min != -1 && _gia_max != 0)
                    {
                        query.Append(" AND gia_ban between @gia_min AND @gia_max ");
                    }
                    // kiểm tra user có search theo khuyến mại hay không
                    if (_discount != 0)
                    {
                        query.Append(" AND discount < @discount ");
                    }
                    // order by theo truong maloai
                    query.Append(" order by maloai ");
                    // add limit và offset
                    query.Append(" OFFSET @offset ");
                    query.Append(" ROWS FETCH NEXT @limit ");
                    query.Append(" ROW ONLY;");
                    SqlCommand sqlCommand = new SqlCommand(query.ToString(), conn);
                    sqlCommand.Prepare();
                    sqlCommand.Parameters.AddWithValue("@maloai", _maloai);
                    if (!string.IsNullOrEmpty(_tensanpham))
                    {
                        sqlCommand.Parameters.AddWithValue("@tensanpham", "%" + Common.ReplaceWildCard(_tensanpham) + "%");
                    }
                    if (_gia_min != -1 && _gia_max != 0)
                    {
                        sqlCommand.Parameters.AddWithValue("@gia_min", _gia_min);
                        sqlCommand.Parameters.AddWithValue("@gia_max", _gia_max);
                    }
                    if (_discount != 0)
                    {
                        sqlCommand.Parameters.AddWithValue("@discount", _discount);
                    }
                    sqlCommand.Parameters.AddWithValue("@offset", _offset);
                    sqlCommand.Parameters.AddWithValue("@limit", _limit);
                    _sqlReader = sqlCommand.ExecuteReader();

                    while (_sqlReader.Read())
                    {
                        Tbl_sanpham tblSanPham = new Tbl_sanpham();
                        tblSanPham._masp = (int)_sqlReader["masp"];
                        tblSanPham._maloai = (int)_sqlReader["maloai"];
                        tblSanPham._tensanpham = _sqlReader["tensanpham"].ToString();
                        var _gia_mua = _sqlReader["gia_mua"].ToString();
                        var _gia_ban = _sqlReader["gia_mua"].ToString();
                        CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");   // try with "en-US"
                        tblSanPham._gia_mua = double.Parse(_gia_mua).ToString("#,###", cul.NumberFormat);
                        tblSanPham._gia_ban = double.Parse(_gia_ban).ToString("#,###", cul.NumberFormat);
                        tblSanPham._size = _sqlReader["size"].ToString();
                        tblSanPham._soluong = (int)_sqlReader["soluong"];
                        tblSanPham._thongtin = _sqlReader["thongtin"].ToString();
                        tblSanPham._ngaynhaphang = (DateTime)_sqlReader["ngaynhaphang"];
                        tblSanPham._hinhanh = _sqlReader["hinhanh"].ToString();
                        tblSanPham.discount = (int)_sqlReader["discount"];

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

        public int GetTotalproduct(int _maloai, string _tensanpham, int _gia_min, int _gia_max, int _discount)
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
                    // Kiểm tra user có search theo tên sản phẩm hay khong
                    if (!string.IsNullOrEmpty(_tensanpham))
                    {
                        query.Append(" AND tensanpham LIKE @tensanpham ESCAPE '!' ");
                    }
                    // kiểm tra user có search sản phẩm theo khoảng giá hay không
                    if (_gia_min != -1 && _gia_max != 0)
                    {
                        query.Append(" AND gia_ban between @gia_min AND @gia_max ");
                    }
                    // kiểm tra user có search theo khuyến mại hay không
                    if (_discount != 0)
                    {
                        query.Append(" AND discount > @discount ");
                    }
                    // Khởi tạo
                    SqlCommand comd = new SqlCommand(query.ToString(), conn);
                    comd.Prepare();
                    comd.Parameters.Clear();
                    comd.Parameters.AddWithValue("@maloai", _maloai);
                    if (!string.IsNullOrEmpty(_tensanpham))
                    {
                        comd.Parameters.AddWithValue("@tensanpham", "%" + Common.ReplaceWildCard(_tensanpham) + "%");
                    }
                    if (_gia_min != -1 && _gia_max != 0)
                    {
                        comd.Parameters.AddWithValue("@gia_min", _gia_min);
                        comd.Parameters.AddWithValue("@gia_max", _gia_max);
                    }
                    if (_discount != 0)
                    {
                        comd.Parameters.AddWithValue("@discount", _discount);
                    }
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
                        var _gia_mua = _sqlReader["gia_mua"].ToString();
                        var _gia_ban = _sqlReader["gia_mua"].ToString();
                        CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");   // try with "en-US"
                        product._gia_mua = double.Parse(_gia_mua).ToString("#,###", cul.NumberFormat);
                        product._gia_ban = double.Parse(_gia_ban).ToString("#,###", cul.NumberFormat);
                        product._size = _sqlReader["size"].ToString();
                        product._soluong = (int)_sqlReader["soluong"];
                        product._thongtin = _sqlReader["thongtin"].ToString();
                        product._ngaynhaphang = (DateTime)_sqlReader["ngaynhaphang"];
                        product._hinhanh = _sqlReader["hinhanh"].ToString();
                        product.discount = (int)_sqlReader["discount"];
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