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
    public class DetailUserController : BaseController
    {
        // GET: Admin/DetailUser
        public ActionResult ViewUser()
        {
            try
            {
                TblUserDao tblUserDao = new TblUserDao();
                // lấy user_id từ request
                int user_id = Common.ConvertToInteger(Request.QueryString["user_id"], 0);
                // nếu user_id lớn 0 và tồn tại trong db
                if (user_id > 0 && tblUserDao.CheckExistedUserID(user_id))
                {
                    Tbl_user userInfor = new Tbl_user();
                    userInfor = tblUserDao.GetUserByUserID(user_id);
                    ViewBag.userInfor = userInfor;
                    ViewBag.user_id = user_id;
                    return View();
                }
                else
                {
                    // chuyển về màn hình lỗi
                    return new RedirectResult(@"~\SystemError\SystemError?error=ER013");
                }
            }
            catch
            {
                // chuyển sang màn hình thông báo lỗi hệ thống
                return RedirectToAction("Error", "Error");
            }
        }
    }
}