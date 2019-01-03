using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanHang.Models;
using WebBanHang.Models.EF;

namespace WebBanHang.DAO
{
    public class SearchDAO
    {
        ShopMVC5DbContext db = new ShopMVC5DbContext();
        public IEnumerable<ProductModel> AdvancedSearch(string term, string loai, string hangsx, int? minprice, int? maxprice, int? sapxep)
        {
            var lst = (
                from p in db.Products.Where(x => x.Status == true)
                .OrderByDescending(x => x.CreateDate)
                select new ProductModel()
                {
                    ID = p.ID,
                    Name = p.Name,
                    Image = (from z in db.Images
                             where z.ProductID == p.ID
                             select new
                             {
                                 z.Src
                             }).Take(1).FirstOrDefault().Src,
                    MetaTitle = p.MetaTitle,
                    Description = p.Description,
                    Detail = p.Detail,
                    Quantity = p.Quantity,
                    Producer = p.Producer.Name,
                    CreateDate = p.CreateDate,
                    IsHot = p.IsHot,
                    ProductCategory = p.ProductCategory.Name,
                    Price = p.Price,
                    Sale = ((from s in db.Sales
                             where s.ProductID == p.ID && s.EndDate >= DateTime.Now
                             orderby s.ID descending
                             select new
                             {
                                 s.Discount
                             }).Take(1).FirstOrDefault().Discount)
                });

            if (!string.IsNullOrEmpty(term))
                lst = lst.Where(u => u.Name.Contains(term));
            if(!string.IsNullOrEmpty(loai))
                lst = (from p in lst where p.ProductCategory.Equals(loai) select p);
            if (!string.IsNullOrEmpty(hangsx))
                lst = (from p in lst where p.Producer.Equals(hangsx) select p);
            if (minprice != null)
                lst = (from p in lst where p.Price >= minprice select p);
            if (maxprice != null)
                lst = (from p in lst where p.Price <= maxprice select p);
            if (sapxep == 2) lst = lst.OrderByDescending(p => p.Price);
            else if (sapxep == 1) lst = lst.OrderBy(p => p.Price);
            else lst = lst.OrderBy(p => p.Name);
            return lst;
        }
    }
}