using Project_HoangPhuongAnh.Models.DAO;
using Project_HoangPhuongAnh.Models.Entities;
using Project_HoangPhuongAnh.Utils;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Project_HoangPhuongAnh.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult Shop(string product)
        {
            TblSanPhamDao _sanPhamDao = new TblSanPhamDao();
            List<Tbl_sanpham> _listProduct = new List<Tbl_sanpham>();
            List<int> listPaging = new List<int>();
            // chỉ số trang hiện tại
            int currentPage = 1;

            int totalPage = 0;

            // số bản ghi tối đa trên 1 page
            int limit = 6;

            int limitPage = 3;

            // lấy vị trí data cần lấy
            int offset = Common.GetOffset(currentPage, limit);


            int _type_product;
            if ("nike".Equals(product))
            {
                _type_product = Constants.NIKE;

                // lấy tổng số user tìm được
                int totalProduct = _sanPhamDao.GetTotalproduct(_type_product);

                // lấy tổng số page
                totalPage = Common.GetTotalPage(totalProduct, limit);

                // lấy danh sách trang hiện tại
                listPaging = Common.GetListPaging(totalProduct, limit, currentPage);

                _listProduct = _sanPhamDao.getAllProductWithTyle(_type_product, offset, limit);


            }
            else if ("adidas".Equals(product))
            {
                _type_product = Constants.ADIDAS;
                // lấy tổng số user tìm được
                int totalProduct = _sanPhamDao.GetTotalproduct(_type_product);

                // lấy tổng số page
                totalPage = Common.GetTotalPage(totalProduct, limit);

                // lấy danh sách trang hiện tại
                listPaging = Common.GetListPaging(totalProduct, limit, currentPage);

                _listProduct = _sanPhamDao.getAllProductWithTyle(_type_product, offset, limit);

            }
            ViewBag._listProduct = _listProduct;
            ViewBag.listPaging = listPaging;
            ViewBag.totalPage = totalPage;
            ViewBag.currentPage = currentPage;
            ViewBag.limitPage = limitPage;
            ViewBag.indexPage = currentPage;

            return View();
        }
    }
}