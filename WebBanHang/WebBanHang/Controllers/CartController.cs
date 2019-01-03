using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebBanHang.DAO;
using WebBanHang.Models;
using WebBanHang.Models.EF;

namespace WebBanHang.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        private const string CartSession = "CartSession";
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        //[ChildActionOnly]
        public ActionResult CartHeader()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;

            }
            decimal TotalPrice = 0;
            int i = 0;
            decimal[] value = new decimal[10000];
            foreach (var item in list)
            {
                i++;
                var price = item.ProductModel.Price;
                if (item.ProductModel.Sale.HasValue)
                {
                    price = price - (item.ProductModel.Sale * price) / 100;
                }
                TotalPrice += (decimal)(price * item.Quantity);
                value[i] = (decimal)price;
            }
            ViewBag.Price = value;
            ViewBag.Total = TotalPrice;
            return PartialView(list);
        }
        public ActionResult Success()
        {
            return View();
        }
        public ActionResult AddToCart(long ProductID, int quantity)
        {
            var product = new ProductDAO().Detail(ProductID);
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.ProductModel.ID == ProductID))
                {
                    foreach (var item in list)
                    {
                        if (item.ProductModel.ID == ProductID)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    var tmp = new CartItem();
                    tmp.ProductModel = product;
                    tmp.Quantity = quantity;
                    list.Add(tmp);
                }
                Session[CartSession] = list;
            }
            else
            {
                var tmp = new CartItem();
                tmp.ProductModel = product;
                tmp.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(tmp);
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }

        public JsonResult Update(string cartModel)
        {
            var cart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];//lấy danh sách các sản phẩm trong giỏ hàng hiện có

            foreach (var item in sessionCart)
            {//lặp lấy sản phảm update
                var itemCart = cart.SingleOrDefault(x => x.ProductModel.ID == item.ProductModel.ID);
                if (itemCart != null)
                {
                    item.Quantity = itemCart.Quantity;
                }
            }

            return Json(new
            {
                status = true
            });
        }
        public JsonResult Delete(long id)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];//lấy danh sách các sản phẩm trong giỏ hàng hiện có
            sessionCart.RemoveAll(x => x.ProductModel.ID == id);
            Session[CartSession] = sessionCart;

            return Json(new
            {
                status = true
            });
        }

        // thanh toan 
        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult Payment(string shipName, string shipPhone, string shipAddress, string shipEmail)
        {
            var order = new Order();
            order.ShipCreateDate = DateTime.Now;
            order.ShipName = shipName;
            order.ShipAddress = shipAddress;
            order.ShipEmail = shipEmail;
            order.ShipPhone = shipPhone;
            order.Status = 0;
            //insert order

            var id = new OrderDAO().Create(order);//trả về id của order
            try
            {
                var cart = (List<CartItem>)Session[CartSession];
                var db = new OrderDetailDAO();
                decimal total = 0;
                foreach (var item in cart)
                {
                    decimal price = (decimal)item.ProductModel.Price;
                    var orderDetail = new OrderDetail();
                    orderDetail.ProductID = item.ProductModel.ID;
                    orderDetail.OrderID = id;
                    if (item.ProductModel.Sale.HasValue)
                    {
                        price = price - (decimal)(item.ProductModel.Sale * price / 100);
                    }
                    orderDetail.Price = price;
                    orderDetail.Quantity = item.Quantity;
                    orderDetail.SubPrice = item.Quantity * price;
                    total += (decimal)orderDetail.SubPrice;

                    db.Create(orderDetail);
                    Session[CartSession] = null;
                }
                new OrderDAO().EditTotal(total, id);
                EmailTool emailTool = new EmailTool();
                emailTool.SendMail(GetParent(id));

            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("Success", "Cart");
        }
        [HttpPost]
        public ActionResult PaymentUserLogin()
        {
            var username = Session["Member"];//lấy session gáng giá trị vào order
            var model = new UserDAO().FindUserName(username.ToString());
            var order = new Order();
            order.ShipCreateDate = DateTime.Now;
            order.ShipName = model.Name;
            order.ShipAddress = model.Address;
            order.ShipEmail = model.Email;
            order.ShipPhone = model.Phone;
            order.Status = 0;
            order.UserID = model.ID;
            //insert order

            var id = new OrderDAO().Create(order);//trả về id của order
            try
            {
                var cart = (List<CartItem>)Session[CartSession];
                var db = new OrderDetailDAO();
                decimal total = 0;
                foreach (var item in cart)
                {
                    decimal price = (decimal)item.ProductModel.Price;
                    var orderDetail = new OrderDetail();
                    orderDetail.ProductID = item.ProductModel.ID;
                    orderDetail.OrderID = id;
                    if (item.ProductModel.Sale.HasValue)
                    {
                        price = price - (decimal)(item.ProductModel.Sale * price / 100);
                    }
                    orderDetail.Price = price;
                    orderDetail.Quantity = item.Quantity;
                    orderDetail.SubPrice = item.Quantity * price;
                    total += (decimal)orderDetail.SubPrice;

                    db.Create(orderDetail);
                    Session[CartSession] = null;
                }
                new OrderDAO().EditTotal(total, id);
                EmailTool emailTool = new EmailTool();
                emailTool.SendMail(GetParent(id));

            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("Success", "Cart");
        }
        public EmailModel GetParent(long id)
        {

            OrderDAO orderDao = new OrderDAO();
            var order = orderDao.Detail(id);
            decimal? total = 0;
            string sub = "[Thông báo] Chúng tôi xét duyệt đơn hàng của bạn";
            string bo = @"";
            bo += "Xin chào " + order.ShipName + ",<br><br><br>";
            bo += "<table style='border-collapse: collapse;border-color: #666666;border-width: 1px;color:#333333;'>";
            bo += "<tr>";
            bo += "<td style='background:#dedede;padding: 8px;border-width: 1px;border-color: #666666;'>Tên sản phẩm</td>";
            bo += "<td style='background:#dedede;padding: 8px;border-width: 1px;border-color: #666666;'>Giá</td>";
            bo += "<td style='background:#dedede;padding: 8px;border-width: 1px;border-color: #666666;'>Số lượng</td>";
            bo += "<td style='background:#dedede;padding: 8px;border-width: 1px;border-color: #666666;'>Tổng</td>";
            bo += "</tr>";

            foreach (var item in order.OrderDetails)
            {
                bo += "<tr >";
                bo += "<td style='padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;border-width: 1px;'>" + item.Product.Name + "</td>";
                bo += "<td style='padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;border-width: 1px;'>" + item.Price + "</td>";
                bo += "<td style='padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;border-width: 1px;'>" + item.Quantity + "</td>";
                bo += "<td style='padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;border-width: 1px;'>" + item.Quantity * item.Price + "</td>";
                bo += "</tr>";
                total += item.Price * item.Quantity;
            }
            bo += "<tr>";
            bo += "<td colspan='3' style='padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;border-width: 1px;'>Tổng tiền</td>";
            bo += "<td style='padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;border-width: 1px;'>" + total + "VNĐ" + "</td>";
            bo += "</tr>";
            bo += "</table><br/>";
            bo += "Cảm ơn bạn đã đặt hàng chúng tôi sẽ liên hệ với bạn trong thời gian sớm nhất ";
            EmailModel email = new EmailModel(order.ShipEmail, sub, bo);
            return email;
        }
    }
}