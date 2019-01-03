using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.DAO;
using System.Web.Routing;
using WebBanHang.Models.EF;

namespace WebBanHang
{
    public class RouteConfig
    {

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "DetailProduct",
                url: "chi-tiet/{metatile}-{id}",
                defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
                namespaces: new string[] { "WebBanHang.Controllers" }

            );
            ProductCategoryDAO model = new ProductCategoryDAO();
            List<ProductCategory> list = model.GetMetatitle();
            foreach (var item in list)
            {
                routes.MapRoute(
                name: item.Name,
                url: item.MetaTitle + "/{metatile}-{idCate}-{id}",
                defaults: new { controller = "Product", action = "Producer_ProductCategory", idCate = UrlParameter.Optional, id = UrlParameter.Optional },
                namespaces: new string[] { "WebBanHang.Controllers" }

            );}

            foreach (var item in list)
            {
                routes.MapRoute(
                name: "Tim kiem theo muc"+item.ID,
                url: "Tim-kiem/" + item.MetaTitle + "-{idCate}",
                defaults: new { controller = "Product", action = "Index", idCate = UrlParameter.Optional },
                namespaces: new string[] { "WebBanHang.Controllers" }

            );}

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "WebBanHang.Controllers" }
            );

        }
    }
}
