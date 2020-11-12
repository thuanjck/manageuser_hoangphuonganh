using Project_HoangPhuongAnh.Models.DAO;
using Project_HoangPhuongAnh.Models.Entities;
using Project_HoangPhuongAnh.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_HoangPhuongAnh.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                TblUserDao user = new TblUserDao();
                List<Tbl_user> _userInfor = new List<Tbl_user>();
                List<int> listPaging = new List<int>();

                string full_name = Common.GetData(Session.Contents["full_name"], "");
                string sortType = "full_name";
                string sortByFullName = "ASC";
                string action = Request.QueryString["action"];

                // chỉ số trang hiện tại
                int currentPage = 1;

                int limitPage = 3;

                if ("".Equals(action))
                {
                    // xóa search
                    Session.Remove("full_name");

                    // xóa sort
                    Session.Remove("sortType");
                    Session.Remove("sortByFullName");

                    //xóa page
                    Session.Remove("currentPage");
                }
                // trường hợp tìm kiếm
                else if ("search".Equals(action))
                {
                    // xóa sort
                    Session.Remove("sortType");
                    Session.Remove("sortByFullName");

                    //xóa page
                    Session.Remove("currentPage");
                    full_name = Request.Unvalidated.QueryString["full_name"];

                    //gán 2 giá trị lên session để lưu kết quả
                    Session["full_name"] = full_name;
                }
                else if ("sort".Equals(action) || "paging".Equals(action) || "back".Equals(action))
                {
                    // lấy giá trị của full_name, nếu full_name null gán giá trị bằng rỗng
                    full_name = Common.GetData(Session.Contents["full_name"], "");

                    // trường hợp sort
                    if ("sort".Equals(action))
                    {
                        //xóa page
                        Session.Remove("currentPage");
                        sortType = Request.QueryString["sortType"];
                        string sortAttribute = Request.QueryString["sortAttribute"];
                        if (sortAttribute != null)
                        {
                            if ("full_name".Equals(sortType))
                            {
                                if ("ASC".Equals(sortAttribute))
                                {
                                    sortByFullName = "DESC";
                                }
                                else
                                {
                                    sortByFullName = "ASC";
                                }
                                Session["sortByFullName"] = sortByFullName;
                            }

                        }
                        Session["sortType"] = sortType;
                    }
                    else if ("paging".Equals(action) || "back".Equals(action))
                    {
                        // lấy sort từ session
                        sortType = Common.GetData(Session.Contents["sortType"], "full_name");
                        sortByFullName = Common.GetData(Session.Contents["sortByFullName"], "ASC");

                        // trường hợp paging
                        if ("paging".Equals(action))
                        {
                            // lấy và convert currentPage nếu không phải là số thì trả về bằng 1
                            currentPage = Common.ConvertToInteger(Request.QueryString["currentPage"], 1);
                            Session["currentPage"] = currentPage;
                        }
                        // trường hợp back
                        else
                        {
                            currentPage = Common.ConvertToInteger(Common.GetData(Session.Contents["currentPage"], "1"), 1);
                        }
                    }
                }

                int totalPage = 0;
                // lấy tổng số user tìm được
                int totalUser = user.GetTotalUsers(full_name);
                // số bản ghi tối đa trên 1 page
                int limit = 5;
                // lấy tổng số page
                totalPage = Common.GetTotalPage(totalUser, limit);
                // Nếu không có bản ghi nào hoặc currentPage có giá trị > totalPage, hoặc là số trang nhỏ hơn 1
                //  do người dùng sửa trên url thì hiển thị thông báo không có bản ghi nào
                if (totalUser == 0 || currentPage > totalPage || currentPage < 1)
                {
                    ViewBag.noRecordMessage = ErrorMessage.MSG005;
                }
                else
                {
                    // lấy vị trí data cần lấy
                    int offset = Common.GetOffset(currentPage, limit);
                    // lấy danh sách trang hiện tại
                    listPaging = Common.GetListPaging(totalUser, limit, currentPage);


                    // lấy danh sách userinfor
                    _userInfor = user.GetAllUser(offset, limit, full_name, sortType, sortByFullName);


                    ViewBag._userInfor = _userInfor;
                    ViewBag.sortByFullName = sortByFullName;
                    ViewBag.full_name = full_name;
                    ViewBag.listPaging = listPaging;
                    ViewBag.totalPage = totalPage;
                    ViewBag.currentPage = currentPage;
                    ViewBag.limitPage = limitPage;
                    ViewBag.indexPage = currentPage;

                }
                return View();
        }
            catch
            {
                // chuyển sang màn hình thông báo lỗi hệ thống
                return RedirectToAction("Error", "Error");
    }

}
    }
} 