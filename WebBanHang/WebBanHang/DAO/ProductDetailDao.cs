using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanHang.Models.EF;

namespace WebBanHang.DAO
{
    public class ProductDetailDao
    {
        private ShopMVC5DbContext db = null;
        public ProductDetailDao()
        {
            db = new ShopMVC5DbContext();
        }
        public IEnumerable<ProductDetail> GetAllProductDetail(long id)
        {
            IEnumerable<ProductDetail> TSKiThuat = (from p in db.ProductDetails
                                                    where p.ProductID == id
                                                    select p
                                                    );
            return TSKiThuat;
        }
    }
}