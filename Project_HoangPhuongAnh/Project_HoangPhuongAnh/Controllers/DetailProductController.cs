using Project_HoangPhuongAnh.Models.DAO;
using Project_HoangPhuongAnh.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_HoangPhuongAnh.Controllers
{
    public class DetailProductController : Controller
    {
        // GET: DetailProduct
        public ActionResult DetailProduct(int product_id)
        {
            try
            {
                TblSanPhamDao _sanPhamDao = new TblSanPhamDao();
                Tbl_imageDao _imageDao = new Tbl_imageDao();

                List<Tbl_image> _listImageProduct = new List<Tbl_image>();
                Tbl_sanpham _product = new Tbl_sanpham();

                if (product_id != 0)
                {
                    _product = _sanPhamDao.GetProductWithID(product_id);
                    _listImageProduct = _imageDao.GetListImage(product_id);

                    string _size = _product._size;
                    string[] _listSize = _size.Split('-');
                    int _first_size = Int32.Parse(_listSize.First());
                    int _last_size = Int32.Parse(_listSize.Last());
                    // đẩy dữ liệu sang FE

                    ViewBag._product = _product;
                    ViewBag._listImageProduct = _listImageProduct;
                    ViewBag._first_size = _first_size;
                    ViewBag._last_size = _last_size;
                }
                return View();
            }
            catch
            {
                // chuyển sang màn hình thông báo lỗi hệ thống
                return RedirectToAction("Error", "ErrorProduct");
            }
            
        }
    }
}