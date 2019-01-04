using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.DAO;
using WebBanHang.Models.EF;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    public class ProductController : Controller
    {
        private const string ProductSession = "ProductSession";
        // GET: Product
        public ActionResult Index()
        {
            return View();   
        }

        [ChildActionOnly]
        public ActionResult NewProduct()
        {
            ProductDAO db = new ProductDAO();
            var model = db.GetNewProduct(5);
            return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult FeaturedProduct()
        {
            ProductDAO db = new ProductDAO();
            var model = db.GetFeaturedProduct();
            return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult SaleProduct()
        {
            ProductDAO db = new ProductDAO();
            var model = db.GetSaleProduct();
            ViewBag.Count = model.ToList().Count;
            return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult BestSeller()
        {
            ProductDAO db = new ProductDAO();
            var model = db.GetFeaturedProduct();
            return PartialView(model);
        }
        public ActionResult RecentProduct()
        {
            var product = Session[ProductSession];
            var list = new List<ProductModel>();
            if(product != null)
            {
                list = (List<ProductModel>)product;
            }
            return PartialView(list);
        }
        public ActionResult Detail(long id)
        {
            ProductDAO db = new ProductDAO();
            var model = db.Detail(id);

            var product = Session[ProductSession];
            if (product != null)
            {
                var list = (List<ProductModel>)product;
                if (list.Exists(x => x.ID == id) == false)
                {
                    list.Add(model);
                }
                Session[ProductSession] = list;
            }
            else
            {
                var list = new List<ProductModel>();
                list.Add(model);
                Session[ProductSession] = list;
            }


            List<string> ListImages = new ImageDAO().GetAllImage(id);
            var ListDetail = new ProductDetailDao().GetAllProductDetail(id);
            ViewBag.ListDetail = ListDetail;
            ViewBag.ListImages = ListImages;
            return View(model);
        }
    }
}