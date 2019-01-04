using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanHang.Models.EF;

namespace WebBanHang.DAO
{
    public class NewsDAO
    {
        private ShopMVC5DbContext db = null;
        public NewsDAO()
        {
            db = new ShopMVC5DbContext();
        }
        /// <summary>
        /// lấy danh sách sản phẩm
        /// </summary>
        /// <returns></returns>
        public IEnumerable<News> GetAllNews(string searchString)
        {
            IQueryable<News> model = db.Newss.OrderByDescending(x => x.CreateDate);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Title.Contains(searchString));
            }
            return model;
        }
        public IEnumerable<NewsCategory> GetAllNewsCategory()
        {
            IQueryable<NewsCategory> lst = db.NewsCategorys;
            return lst;
        }
        public IEnumerable<NewsCategory> GetAll(string searchString)
        {
            IQueryable<NewsCategory> model = db.NewsCategorys;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model;
        }
        /// <summary>
        /// lấy thông tin sản phẩm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public News ViewDetail(long id)
        {
            var model = db.Newss.Find(id);          
            return model;
        }
    }
}