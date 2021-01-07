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

            string _product = "";
            _product = shopInfo._product;

            string _tensanpham = "";
            if (shopInfo._tensanpham != null || !"".Equals(shopInfo._tensanpham))
            {
                _tensanpham = shopInfo._tensanpham;
            }

            int _gia_min = -1;
            int _gia_max = 0;
            if (shopInfo._gia != null)
            {
                int price_range = int.Parse(shopInfo._gia);
                if (price_range == 500000)
                {
                    _gia_min = 0;
                    _gia_max = 500000;
                } else if (price_range == 700000)
                {
                    _gia_min = 500000;
                    _gia_max = 700000;
                }
                else if (price_range == 900000)
                {
                    _gia_min = 700000;
                    _gia_max = 900000;
                }
                else if (price_range == 1000000)
                {
                    _gia_min = 900000;
                    _gia_max = 99999999;
                }
            }

            int discount_price = 0;
            if (shopInfo._discount != null)
            {
                discount_price = int.Parse(shopInfo._discount);
            }

            int _type_product;
            if (Session["_product"]==null)
            {
                Session["_product"] = "";
            }
            if ("nike".Equals(shopInfo._product))
            {
                _type_product = Constants.NIKE;

                Action(ref shopInfo);

                // lấy tổng số user tìm được
                int totalProduct = _sanPhamDao.GetTotalproduct(_type_product, _tensanpham, _gia_min, _gia_max, discount_price);

                // lấy tổng số page
                totalPage = Common.GetTotalPage(totalProduct, limit);

                // lấy vị trí data cần lấy
          
                int offset = Common.GetOffset(currentPage, limit);
                

                // lấy danh sách trang hiện tại
                listPaging = Common.GetListPaging(totalProduct, limit, currentPage);

                _listProduct = _sanPhamDao.GetAllProductWithTyle(_type_product, offset, limit, _tensanpham, _gia_min, _gia_max, discount_price);

            }
            else if ("adidas".Equals(shopInfo._product))
            {
                _type_product = Constants.ADIDAS;
                // lấy tổng số user tìm được
                int totalProduct = _sanPhamDao.GetTotalproduct(_type_product, _tensanpham, _gia_min, _gia_max, discount_price);

                // lấy tổng số page
                totalPage = Common.GetTotalPage(totalProduct, limit);

                // lấy vị trí data cần lấy
                int offset = Common.GetOffset(currentPage, limit);

                // lấy danh sách trang hiện tại
                listPaging = Common.GetListPaging(totalProduct, limit, currentPage);

                _listProduct = _sanPhamDao.GetAllProductWithTyle(_type_product, offset, limit, _tensanpham, _gia_min, _gia_max, discount_price);
            }
            else if ("sneakers".Equals(shopInfo._product))
            {
                _type_product = Constants.SNEAKERS;
                // lấy tổng số user tìm được
                int totalProduct = _sanPhamDao.GetTotalproduct(_type_product, _tensanpham, _gia_min, _gia_max, discount_price);

                // lấy tổng số page
                totalPage = Common.GetTotalPage(totalProduct, limit);

                // lấy vị trí data cần lấy
                int offset = Common.GetOffset(currentPage, limit);

                // lấy danh sách trang hiện tại
                listPaging = Common.GetListPaging(totalProduct, limit, currentPage);

                _listProduct = _sanPhamDao.GetAllProductWithTyle(_type_product, offset, limit, _tensanpham, _gia_min, _gia_max, discount_price);
            }
            ViewBag._listProduct = _listProduct;
            ViewBag.listPaging = listPaging;
            ViewBag.totalPage = totalPage;
            ViewBag._currentPage = currentPage;
            ViewBag.limitPage = limitPage;
            ViewBag.indexPage = currentPage;
            ViewBag.product = shopInfo._product;
            ViewBag._gia = shopInfo._gia;
            ViewBag._discount = shopInfo._discount;
            ViewBag._tensanpham = shopInfo._tensanpham;

            return View();
        }

        public void Action(ref ShopInfo shopInfo)
        {
            if ("".Equals(shopInfo._action) || shopInfo._action == null)
            {

                //xóa page
                Session.Remove("_currentPage");
                Session.Remove("_tensanpham");
                Session.Remove("_gia");
                Session.Remove("_discount");

                shopInfo._product = Request.Unvalidated.QueryString["_product"];
                Session["_product"] = shopInfo._product;
            }
            // trường hợp tìm kiếm
            else if ("search".Equals(shopInfo._action))
            {
                //xóa page
                Session.Remove("_currentPage");
                shopInfo._tensanpham = Request.Unvalidated.QueryString["_tensanpham"];
                shopInfo._gia = Request.Unvalidated.QueryString["_gia"];
                shopInfo._discount = Request.Unvalidated.QueryString["_discount"];
                shopInfo._product = Request.Unvalidated.QueryString["_product"];

                Session["_product"] = shopInfo._product;

            }
            else if ("paging".Equals(shopInfo._action) || "back".Equals(shopInfo._action))
            {
                // lấy giá trị của full_name, nếu full_name null gán giá trị bằng rỗng
                shopInfo._tensanpham = Common.GetData(Session.Contents["_tensanpham"], "");
                shopInfo._gia = Request.Unvalidated.QueryString["_gia"];
                shopInfo._discount = Request.Unvalidated.QueryString["_discount"];

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