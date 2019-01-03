using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanHang.Models.EF;

namespace WebBanHang.DAO
{
    public class OrderDetailDAO
    {
        private ShopMVC5DbContext db = null;
        public OrderDetailDAO()
        {
            db = new ShopMVC5DbContext();
        }
        public bool Create(OrderDetail orderDetail)
        {
            try
            {
                //giảm số lượng khi đặt sản phẩm
                var product = db.Products.Find(orderDetail.ProductID);
                product.Quantity = product.Quantity - orderDetail.Quantity;
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }
    }
}