using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanHang.Models.EF;

namespace WebBanHang.DAO
{
    public class OrderDAO
    {
        private ShopMVC5DbContext db = null;
        public OrderDAO()
        {
            db = new ShopMVC5DbContext();
        }
        public long Create(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.ID;
        }
        public void EditTotal(decimal total,long id)
        {
            var x = Detail(id);
            x.TotalPrice = total;
            db.SaveChanges();
        }
        //detail order
        public Order Detail(long id)
        {
            var model = db.Orders.SingleOrDefault(x => x.ID == id);
            return model;
        }
    }
}