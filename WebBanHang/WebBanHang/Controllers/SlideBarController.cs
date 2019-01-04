using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanHang.Controllers
{
    public class SlideBarController : Controller
    {
        // GET: SlideBar
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductFilter(int? min,int? max)
        {
           
            ViewBag.minprice = min;
            ViewBag.maxprice = max;

            return PartialView("ProductFilter");
        }
    }
}