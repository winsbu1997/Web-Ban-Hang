using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.DAO;
using PagedList;

namespace WebBanHang.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        public ActionResult Index(string searchString, int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new NewsDAO().GetAllNews(searchString).ToPagedList(pageNumber, pageSize));
        }

        [ChildActionOnly]
        public ActionResult CategoryNews()
        {
            var listCategory = new NewsDAO().GetAll("");
            return PartialView(listCategory);
        }
    }
}