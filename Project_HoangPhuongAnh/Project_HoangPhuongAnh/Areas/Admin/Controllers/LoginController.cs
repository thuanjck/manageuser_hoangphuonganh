using Project_HoangPhuongAnh.Utils;
using Project_HoangPhuongAnh.ValidateUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_HoangPhuongAnh.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// tầng controller thực hiện nhiệm vụ lấy dữ liệu từ bên view xử lí và trả lại dữ liệu cho bên view
        /// Created by ThuanTV 27/11/2019
        /// </summary>
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Login()
        {
            try
            {
                if (Request.HttpMethod.ToLower() == "post")
                {
                    System.Web.Helpers.AntiForgery.Validate();
                }
                TblUserValidate tblUserValidate = new TblUserValidate();

                List<string> listErrorMessage = new List<string>();
                string email = Request.Unvalidated.Form["email"];

                string password = Request.Form["password"];

                listErrorMessage = tblUserValidate.ValidateLogin(email, password);
                if (listErrorMessage.Count > 0)
                {
                    ViewBag.loginName = email;
                    ViewBag.listErrorMessage = listErrorMessage;
                    return View("Index");
                }
                else
                {
                    Session.Add(Constants.EMAIL, email);
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                // chuyển sang màn hình thông báo lỗi hệ thống
                return RedirectToAction("SystemError", "SystemError");
            }
        }
    }
}