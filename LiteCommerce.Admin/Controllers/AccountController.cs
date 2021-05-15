using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
namespace LiteCommerce.Admin.Controllers
{
    
    public class AccountController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Login(string loginName = "", string password = "")
        {
            ViewBag.LoginName = loginName;

            if (Request.HttpMethod == "POST")
            {
                var account = AccountService.Authorize(loginName, CryptHelper.Md5(password));
                if (account == null)
                {
                    ModelState.AddModelError("a", "Biến ra đăng nhập lại.");
                    return View();
                }

                FormsAuthentication.SetAuthCookie(CookieHelper.AccountToCookieString(account), false);

                return RedirectToAction("Index", "Home");
            }
            else
            {

                return View();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePassword()
        {
            string accountId = "", oldPassword = "", newPassword = "", confimPassword = "";
            ViewBag.UserName = accountId;
            ViewBag.OldPassword = oldPassword;
            ViewBag.NewPassword = newPassword;
            ViewBag.ConfirmPassword = confimPassword;
            if (accountId != null && newPassword != null && oldPassword != null)
            {
                if (newPassword == confimPassword)
                {
                    bool result = AccountService.ChangePassword(accountId, CryptHelper.Md5(oldPassword), CryptHelper.Md5(newPassword));
                    if (result == true)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đổi mật khẩu không thành công. Xin thử lại!");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Mật khẩu mới không trùng");
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }
    }
}