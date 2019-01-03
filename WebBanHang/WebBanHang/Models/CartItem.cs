using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanHang.Models.EF;
namespace WebBanHang.Models
{
    [Serializable]
    public class CartItem
    {
        public ProductModel ProductModel { get; set; }
        public int Quantity { get; set; }
    }
}