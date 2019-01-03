using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanHang.Models.EF;
namespace WebBanHang.DAO
{
    public class ProducerDAO
    {
        private ShopMVC5DbContext db = null;
        public ProducerDAO(){
            db = new ShopMVC5DbContext();
        }
        public IQueryable<Producer> GetAll(string searchString)
        {
            IQueryable<Producer> model = db.Producers;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model;
        }
        public Producer ViewDetail(long id)
        {
            return db.Producers.Find(id);
        }
    }
}