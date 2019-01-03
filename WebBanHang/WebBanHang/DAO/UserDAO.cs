using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web;
using WebBanHang.Models.EF;
namespace WebBanHang.DAO
{
    public class UserDAO
    {
        private ShopMVC5DbContext db = null;
        public UserDAO()
        {
            db = new ShopMVC5DbContext();
        }
        public User FindEmail(string email)
        {
            var user = db.Users.SingleOrDefault(x => x.Email == email);
            return user;
        }
        public User FindUserName(string name)
        {
            var user = db.Users.SingleOrDefault(x => x.Username == name);
            return user;
        }
        public int CreateUSer(User user)
        {
            var checkName = FindUserName(user.Username);
            var checkEmail = FindEmail(user.Email);
            if (checkName == null)
            {
                if (checkEmail == null)
                {
                    if (user.Permission == null) user.Permission = 2;
                    if (user.Avatar == null) user.Avatar = "/Data/ Client/Images/Phone/3.png";
                    user.Active = true;
                    user.CreateDate = DateTime.Now;
                    //  code kierm tra loi EntityValidate lỗi ràng buỗjc
                    try
                    {
                        db.Users.Add(user);
                        db.SaveChanges();
                    }
                    catch (DbEntityValidationException ex)
                    {
                        // Retrieve the error messages as a list of strings.
                        var errorMessages = ex.EntityValidationErrors
                                .SelectMany(x => x.ValidationErrors)
                                .Select(x => x.ErrorMessage);

                        // Join the list to a single string.
                        var fullErrorMessage = string.Join("; ", errorMessages);

                        // Combine the original exception message with the new one.
                        var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                        // Throw a new DbEntityValidationException with the improved exception message.
                        throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                    }
                    return 1;
                }
                else
                {
                    // ton tai email
                    return 0;
                }
            }
            // ten dang nhap ton tai
            return -1;
        }

        // login -- client

        public int LoginClient(string UserName, string Password)
        {
            var user = db.Users.SingleOrDefault(x => x.Username == UserName && x.Password == Password);
            if(user != null)
            {
                if(user.Active == true)
                {
                    return 1;
                }
                else
                {
                    //khong duoc quyen truy cap
                    return 0;
                }
            }
            return -1;
        }
        public string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
        public bool UpdatePass(string email, string pass)
        {
            try
            {
                var user = FindEmail(email);
                user.Password = pass;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}