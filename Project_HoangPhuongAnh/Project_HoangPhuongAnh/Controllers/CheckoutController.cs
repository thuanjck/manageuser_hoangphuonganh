using System.Web.Mvc;

namespace Project_HoangPhuongAnh.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        
        public ActionResult Checkout(string shoe_item, string amount)
        {
            return View();
        }
    }
}