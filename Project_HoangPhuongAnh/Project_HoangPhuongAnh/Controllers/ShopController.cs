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
        public ActionResult Shop(ShopInfo shopInfo)
        {
            TblSanPhamDao _sanPhamDao = new TblSanPhamDao();
            List<Tbl_sanpham> _listProduct = new List<Tbl_sanpham>();
            List<int> listPaging = new List<int>();

            // chỉ số trang hiện tại
            int currentPage = 1;
            if (shopInfo._currentPage != null)
            {
                currentPage = shopInfo._currentPage.Value;
            }
            int totalPage = 0;

            // số bản ghi tối đa trên 1 page
            int limit = 6;

            int limitPage = 3;

            string _product;
            _product = shopInfo._product;

            int _type_product;
            if ("nike".Equals(shopInfo._product))
            {
                _type_product = Constants.NIKE;

                Action(ref shopInfo);

                // lấy tổng số user tìm được
                int totalProduct = _sanPhamDao.GetTotalproduct(_type_product);

                // lấy tổng số page
                totalPage = Common.GetTotalPage(totalProduct, limit);

                // lấy vị trí data cần lấy
          
                int offset = Common.GetOffset(currentPage, limit);
                

                // lấy danh sách trang hiện tại
                listPaging = Common.GetListPaging(totalProduct, limit, currentPage);

                _listProduct = _sanPhamDao.GetAllProductWithTyle(_type_product, offset, limit);

            }
            else if ("adidas".Equals(shopInfo._product))
            {
                _type_product = Constants.ADIDAS;
                // lấy tổng số user tìm được
                int totalProduct = _sanPhamDao.GetTotalproduct(_type_product);

                // lấy tổng số page
                totalPage = Common.GetTotalPage(totalProduct, limit);

                // lấy vị trí data cần lấy
                int offset = Common.GetOffset(currentPage, limit);

                // lấy danh sách trang hiện tại
                listPaging = Common.GetListPaging(totalProduct, limit, currentPage);

                _listProduct = _sanPhamDao.GetAllProductWithTyle(_type_product, offset, limit);
            }
            else if ("sneakers".Equals(shopInfo._product))
            {
                _type_product = Constants.SNEAKERS;
                // lấy tổng số user tìm được
                int totalProduct = _sanPhamDao.GetTotalproduct(_type_product);

                // lấy tổng số page
                totalPage = Common.GetTotalPage(totalProduct, limit);

                // lấy vị trí data cần lấy
                int offset = Common.GetOffset(currentPage, limit);

                // lấy danh sách trang hiện tại
                listPaging = Common.GetListPaging(totalProduct, limit, currentPage);

                _listProduct = _sanPhamDao.GetAllProductWithTyle(_type_product, offset, limit);
            }
            ViewBag._listProduct = _listProduct;
            ViewBag.listPaging = listPaging;
            ViewBag.totalPage = totalPage;
            ViewBag._currentPage = currentPage;
            ViewBag.limitPage = limitPage;
            ViewBag.indexPage = currentPage;
            ViewBag.product = _product;
            ViewBag._gia = shopInfo._gia;
            ViewBag._discount = shopInfo._discount;
            ViewBag._tensanpham = shopInfo._tensanpham;

            return View();
        }

        public void Action(ref ShopInfo shopInfo)
        {
            if ("".Equals(shopInfo._action))
            {
                // xóa search
                Session.Remove("full_name");

                //xóa page
                Session.Remove("_currentPage");
            }
            // trường hợp tìm kiếm
            else if ("search".Equals(shopInfo._action))
            {
                //xóa page
                Session.Remove("_currentPage");
                shopInfo._tensanpham = Request.Unvalidated.QueryString["_tensanpham"];

                //gán 2 giá trị lên session để lưu kết quả
                Session["_tensanpham"] = shopInfo._tensanpham;
            }
            else if ("paging".Equals(shopInfo._action) || "back".Equals(shopInfo._action))
            {
                // lấy giá trị của full_name, nếu full_name null gán giá trị bằng rỗng
                shopInfo._tensanpham = Common.GetData(Session.Contents["_tensanpham"], "");

                if ("paging".Equals(shopInfo._action) || "back".Equals(shopInfo._action))
                {
                    // trường hợp paging
                    if ("paging".Equals(shopInfo._action))
                    {
                        // lấy và convert currentPage nếu không phải là số thì trả về bằng 1
                        shopInfo._currentPage = Common.ConvertToInteger(Request.QueryString["_currentPage"], 1);
                        Session["_currentPage"] = shopInfo._currentPage;
                    }
                    // trường hợp back
                    else
                    {
                        shopInfo._currentPage = Common.ConvertToInteger(Common.GetData(Session.Contents["_currentPage"], "1"), 1);
                    }
                }
            }
        }
    }
}