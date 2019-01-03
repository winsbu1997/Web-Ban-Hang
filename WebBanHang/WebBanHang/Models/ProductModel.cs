﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanHang.Models
{
    public class ProductModel
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Image { get; set; }
        public string MetaTitle { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool IsHot { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        [AllowHtml]
        public string Detail { get; set; }
        public decimal? Sale { get; set; }
        public string Status { get; set; }
        public string Producer { get; set; }
        public string ProductCategory { get; set; }
    }
}