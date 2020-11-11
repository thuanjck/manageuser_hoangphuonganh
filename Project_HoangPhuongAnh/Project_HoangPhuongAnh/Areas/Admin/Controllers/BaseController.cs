using Project_HoangPhuongAnh.Models.DAO;
using Project_HoangPhuongAnh.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Project_HoangPhuongAnh.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// Phương thức kiểm tra filter
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            TblUserDao userDao = new TblUserDao();
            var session = Session[Constants.EMAIL];
            if (session == null || !userDao.CheckAdmin(session.ToString()))
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Login", action = "Index" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}