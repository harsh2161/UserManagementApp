using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Mvc;
using UserManagementApp.BussinessLayer;
using UserManagementApp.Helper;
using UserManagementApp.Models;

namespace UserManagementApp.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult SignIn()
        {            
            if (Request.Cookies["AuthCookie"] != null)
            {
                string Token = Request.Cookies["AuthCookie"].Value;
                Token = Token.Replace("AuthCookie=", "");
                List<string> UserCredentials = TokenService.Decrypt(Token.Replace("AuthenticatedUser,", "")).Split(',').ToList();
                if (UserCredentials[1] == AvailableRoles.Admin.ToString())
                {
                    return RedirectToAction("Dashboard", "Home");
                }                
                else if (UserCredentials[1] == AvailableRoles.User.ToString())
                {
                    return RedirectToAction("GetUser", "Home");
                }
            }
            return View();
        }

        public ActionResult CheckAuthentication(UserCredentials userCredentials)
        {
            try
            {
                BussinessLayer.AuthenticationService AuthenticationService = new BussinessLayer.AuthenticationService();
                string Response = AuthenticationService.Authenticate(userCredentials);
                if (Response.Contains("AuthenticatedUser,"))
                {
                    HttpCookie MyCookie = new HttpCookie("AuthCookie");
                    MyCookie["AuthCookie"] = Response.Replace("AuthenticatedUser,", "");
                    MyCookie.Expires = DateTime.Now.AddDays(2);
                    HttpContext.Response.SetCookie(MyCookie);
                    List<string> UserCredentials = TokenService.Decrypt(Response.Replace("AuthenticatedUser,", "")).Split(',').ToList();
                    return Json(new { result = UserCredentials[1] });
                }
                return Json(new { result = Response });
            }
            catch(Exception ex)
            {
                return Json(new { result = ex.Message});
            }
        }

        public ActionResult SignOut()
        {
            if (Request.Cookies["AuthCookie"] != null)
            {
                var Cookie = new HttpCookie("AuthCookie")
                {
                    Expires = DateTime.Now.AddDays(-1)
                };
                Response.Cookies.Add(Cookie);
            }
            return RedirectToAction("SignIn");
        }
    }
}