using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanHang.Models.EF;

namespace WebBanHang.DAO
{
    public class ProductCategoryDAO
    {
        private ShopMVC5DbContext db = null;
        public ProductCategoryDAO()
        {
            db = new ShopMVC5DbContext();
        }
        public List<ProductCategory> GetMetatitle()
        {
            List<ProductCategory> meta = (from p in db.ProductCategories select p).ToList();
            return meta;
        }
        public IEnumerable<ProductCategory> GetAllProductcate()
        {
            return (from p in db.ProductCategories select p);
        }
        public ProductCategory ViewDetail(long id)
        {
            return db.ProductCategories.Find(id);
        }
    }
}