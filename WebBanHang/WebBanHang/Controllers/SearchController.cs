using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models.EF;
using WebBanHang.DAO;
using PagedList;

namespace WebBanHang.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        public ActionResult SearchForm()
        {
            ProductCategoryDAO db = new ProductCategoryDAO();
            var model = db.GetAllProductcate();
            return PartialView("_SearchForm",model);
        }
        [HttpPost]
        public ActionResult SearchByName(string term)
        {
            var db = new ProductDAO().GetNewProduct(10000);
            var model = db.Where(x => x.Name.Contains(term));
            var splist = (from p in model orderby p.ID descending select new { p.ID, p.Name, p.Price, p.Image, p.Sale, p.Quantity, p.MetaTitle }).Take(5);
            return Json(splist, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AdvancedSearchView(string term, string loai, string hangsx, int? minprice, int? maxprice, int? page , int? sapxep)
        {
            int pageSize = 9;
            ViewBag.Name = term;
            ViewBag.loai = loai;
            ViewBag.hangsx = hangsx;
            ViewBag.minprice = minprice;
            ViewBag.maxprice = maxprice;
            ViewBag.sapxep = sapxep;
            SearchDAO sp = new SearchDAO();
            var model = sp.AdvancedSearch(term, loai, hangsx, minprice, maxprice,sapxep);

            int pageNumber = (page ?? 1);
            return PartialView(model.ToPagedList(pageNumber,pageSize));
        }
    }
}