using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.DAO;
using WebBanHang.Models;
using WebBanHang.Models.EF;
using WebBanHang.Common;

namespace WebBanHang.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        //load menu main;
        [ChildActionOnly]
        public ActionResult MainMenuPartiview()
        {
            MainMenuModel model = new MainMenuModel();
            var MenuList = model.GetMenuList();
            return PartialView(MenuList);
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            var pass = Encryptor.Encrypt(user.Password);
            user.Password = pass;
            var db = new UserDAO();
            var kq = db.CreateUSer(user);
            if(kq == 1)
            {
                //dang ki thanh cong
                TempData["Success"] = "Thêm tài khoản thành công";
                Login(user.Username, Decryptor.Decrypt(user.Password));
                return RedirectToAction("Index", "Home");
            }
            else if(kq == 0)
            {
                TempData["Error"] = "Email đã được đăng kí";
            }
            else if(kq == -1)
            {
                TempData["Error"] = "Tài khoản đã tồn tại";
            }
            else
            {
                ModelState.AddModelError("", "Thêm tài khoản thất bại");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username,string password)
        {
            var pass = Encryptor.Encrypt(password);
            int kq = new UserDAO().LoginClient(username, pass);
            var user = new UserDAO().FindUserName(username);
            if(kq == -1)
            {
                TempData["Error"] = "Tài khoản không tồn tại";
            }
            else if(kq == 0)
            {
                TempData["Error"] = "Tài khoản đã bị khóa";
            }
            else if(kq == 1)
            {
                Session["Member"] = username;
                Session["MemberID"] = user.ID;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Error"] = "Đăng nhập không thành công";                
            }
            return View("Register");
        }
        public ActionResult Logout()
        {
            Session["Member"] = null;
            Session["Error"] = null;
            Session["Success"] = null;
            return RedirectToAction("Index", "Home");
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            if (ModelState.IsValid)
            {
                var check = new UserDAO().FindEmail(email);
                if (check != null)
                {
                    var newpass = new UserDAO().CreatePassword(10);
                    var creat = new UserDAO().UpdatePass(email, Encryptor.Encrypt(newpass));
                    if (creat)
                    {
                        EmailTool emailTool = new EmailTool();
                        emailTool.SendMail(GetParent(email, newpass));
                        TempData["Success"] = "Đã lấy lại mật khẩu thành công ,chung tôi đã gửi một email chứa mật khẩu tới tài khoản của bạn!";
                        return View();
                    }

                }
            }
            TempData["Error"] = "Lấy lại mật khẩu không thành công!";

            return View();
        }
        public EmailModel GetParent(string e, string pass)
        {
            string sub = "[Thông báo] Bạn đã lấy lại mật khẩu cho tài khoản";
            string bo = @"";
            bo += "Mật khẩu mới tài khoản của bạn là : <span style='color:red;'>" + pass + "</p>";
            EmailModel email = new EmailModel(e, sub, bo);
            return email;
        }

    }
}