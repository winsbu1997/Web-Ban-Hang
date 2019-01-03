using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Validation;
using System.Web;
using System.Xml.Linq;
using Microsoft.Ajax.Utilities;
using WebBanHang.Models;
using WebBanHang.Models.EF;

namespace WebBanHang.DAO
{
    public class ProductDAO
    {
        private ShopMVC5DbContext db = null;
        public ProductDAO()
        {
            db = new ShopMVC5DbContext();
        }
        public IEnumerable<ProductModel> GetNewProduct(int sl)
        {
            var products = (
                from p in db.Products.OrderByDescending(x => x.CreateDate)
                select new ProductModel()
                {
                    ID = p.ID,
                    Name = p.Name,
                    Code = p.Code,
                    Image = ((from z in db.Images
                              where z.ProductID == p.ID
                              select new
                              {
                                  z.Src
                              }).Take(1).FirstOrDefault().Src),
                    MetaTitle = p.MetaTitle,
                    Detail = p.Detail,
                    Quantity = p.Quantity,
                    Price = p.Price,
                    CreateDate = p.CreateDate,
                    Sale = ((from s in db.Sales
                             where s.ProductID == p.ID && s.EndDate >= DateTime.Now
                             orderby s.ID descending
                             select new
                             {
                                 s.Discount
                             }).Take(1).FirstOrDefault().Discount)

                }).ToList();
            var model = products.Take(sl);
            return model;
        }

        public IEnumerable<ProductModel> GetFeaturedProduct()
        {
            var products = (
                from p in db.Products.OrderByDescending(x => x.CreateDate)
                where p.IsHot == true
                select new ProductModel()
                {
                    ID = p.ID,
                    Name = p.Name,
                    Code = p.Code,
                    Image = ((from z in db.Images
                              where z.ProductID == p.ID
                              select new
                              {
                                  z.Src
                              }).Take(1).FirstOrDefault().Src),
                    MetaTitle = p.MetaTitle,
                    Detail = p.Detail,
                    Quantity = p.Quantity,
                    Price = p.Price,
                    CreateDate = p.CreateDate,
                    Sale = ((from s in db.Sales
                             where s.ProductID == p.ID && s.EndDate >= DateTime.Now
                             orderby s.ID descending
                             select new
                             {
                                 s.Discount
                             }).Take(1).FirstOrDefault().Discount)

                }).ToList();
            var model = products.Take(7);
            return model;
        }
        public IEnumerable<ProductModel> GetSaleProduct()
        {
            var products = (
                from p in db.Products.OrderByDescending(x => x.CreateDate)
                join t in db.Sales on p.ID equals t.ProductID

                select new ProductModel()
                {
                    ID = p.ID,
                    Name = p.Name,
                    Code = p.Code,
                    Image = ((from z in db.Images
                              where z.ProductID == p.ID
                              select new
                              {
                                  z.Src
                              }).Take(1).FirstOrDefault().Src),
                    MetaTitle = p.MetaTitle,
                    Detail = p.Detail,
                    Quantity = p.Quantity,
                    Price = p.Price,
                    CreateDate = p.CreateDate,
                    IsHot = p.IsHot,
                    Sale = ((from s in db.Sales
                             where s.ProductID == p.ID && s.EndDate >= DateTime.Now
                             orderby s.ID descending
                             select new
                             {
                                 s.Discount
                             }).Take(1).FirstOrDefault().Discount)

                }).ToList();
            var model = products.Take(7);
            return model;
        }

        public IEnumerable<ProductModel> GetBestSeller()
        {
            return null;
        }

        public ProductModel Detail(long id)
        {
            var product = (
                from p in db.Products
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
                    ProductCategory = p.ProductCategory.Name,
                    Price = p.Price,
                    CreateDate = p.CreateDate,
                    IsHot = p.IsHot,
                    Sale = ((from s in db.Sales
                             where s.ProductID == p.ID && s.EndDate >= DateTime.Now
                             orderby s.ID descending
                             select new
                             {
                                 s.Discount
                             }).Take(1).FirstOrDefault().Discount)
                });
            ProductModel model = product.SingleOrDefault(x => x.ID == id);
            return model;
        }

        public IEnumerable<ProductModel> GetAllProductProducer(ref int totalRecord,int pageIndex,int pageSize,long id,long categoryID)
        {
            var product = (
                from p in
                    db.Products.Where(x => x.Status == true && x.ProducerID == id && x.ProductCategoryID == categoryID)
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
            totalRecord = product.Count();
            var models = product.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return models;
        }
        public IEnumerable<ProductModel> GetAllProductPage(ref int totalRecord, int pageIndex, int pageSize,long id)
        {
            var product = (
                from p in
                    db.Products.Where(x => x.Status == true && x.ProductCategoryID == id)
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
                    ProductCategory = p.ProductCategory.Name,
                    IsHot = p.IsHot,
                    Price = p.Price,
                    Sale = ((from s in db.Sales
                             where s.ProductID == p.ID && s.EndDate >= DateTime.Now
                             orderby s.ID descending
                             select new
                             {
                                 s.Discount
                             }).Take(1).FirstOrDefault().Discount)
                });
            totalRecord = product.Count();
            var models = product.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return models;
        }

    }
}