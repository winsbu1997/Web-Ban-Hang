using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanHang.Models.EF;

namespace WebBanHang.DAO
{
    public class ImageDAO
    {
        private ShopMVC5DbContext db = null;
        public ImageDAO()
        {
            db = new ShopMVC5DbContext();
        }
        public List<string> GetAllImage(long id)
        {
            List<string> image = (from p in db.Images where p.ProductID == id select p.Src).ToList();
            return image;
        } 
    }
}