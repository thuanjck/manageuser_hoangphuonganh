using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_HoangPhuongAnh.Areas.Admin.Controllers
{
    public class LogoutController : Controller
    {
        /// <summary>
        /// khi người dùng ấn logout
        /// Created by ThuanTV 27/11/2019
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            // thực hiện xóa session
            Session.RemoveAll();
            return new RedirectResult(@"~\Admin\Home\Index");
        }
    }
}