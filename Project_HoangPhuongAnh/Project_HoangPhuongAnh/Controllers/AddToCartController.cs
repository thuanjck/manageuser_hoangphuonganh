using Project_HoangPhuongAnh.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_HoangPhuongAnh.Controllers
{
    public class AddToCartController : Controller
    {
        // GET: AddToCart
        public ActionResult Add(Tbl_sanpham sanpham)
        {
            if (Session["cart"] == null)
            {
                List<Tbl_sanpham> li = new List<Tbl_sanpham>();

                li.Add(sanpham);
                Session["cart"] = li;
                ViewBag.cart = li.Count();


                Session["count"] = 1;


            }
            else
            {
                List<Tbl_sanpham> li = (List<Tbl_sanpham>)Session["cart"];
                li.Add(sanpham);
                Session["cart"] = li;
                ViewBag.cart = li.Count();
                Session["count"] = Convert.ToInt32(Session["count"]) + 1;

            }
            return RedirectToAction("Shop", "Shop");
        }

        public ActionResult Myorder()
        {
            return View((List<Tbl_sanpham>)Session["cart"]);
        }

        public ActionResult Remove(Tbl_sanpham sanpham)
        {
            List<Tbl_sanpham> li = (List<Tbl_sanpham>)Session["cart"];
            li.RemoveAll(x => x._masp == sanpham._masp);
            Session["cart"] = li;
            Session["count"] = Convert.ToInt32(Session["count"]) - 1;
            return RedirectToAction("Myorder", "AddToCart");
            //return View();
        }
    }
}