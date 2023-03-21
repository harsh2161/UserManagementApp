using System;
using System.Collections.Generic;
using System.Web.Mvc;
using UserManagementApp.BussinessLayer;
using UserManagementApp.Models;

namespace UserManagementApp.Controllers
{
    [ValidateInput(false)]
    public class HomeController : Controller
    {
        public ActionResult Dashboard(SearchFilter searchFilter)
        {
            return View();
        }
        public ActionResult RetrieveAll(SearchFilter searchFilter)
        {
            try
            {
                GetService GetService = new GetService(Request.Cookies["AuthCookie"].Value);
                List<User> GetList = GetService.Get<User>(searchFilter, "all");
                if(GetList.Count == 0)
                {
                    return Json(new { result = "Empty List" });
                }
                return View(GetList);
            }
            catch (Exception ex)
            {
                return Json(new { result = ex.Message });
            }
        }
        public ActionResult ReteriveData(string id)
        {
            GetService GetService = new GetService(Request.Cookies["AuthCookie"].Value);
            List<User> GetList = GetService.Get<User>(null, id);
            return View(GetList[0]);           
        }
        public JsonResult CreateData(User user)
        {
            try
            {
                CreateService CreateService = new CreateService(Request.Cookies["AuthCookie"] == null ? null : Request.Cookies["AuthCookie"].Value);
                var Response = CreateService.Create(user);
                return Json(new { result = Response });
            }
            catch(Exception ex)
            {
                return Json(new { result = ex.Message });
            }
        }
        public ActionResult DeleteData(string id)
        {
            DeleteService DeleteService = new DeleteService(Request.Cookies["AuthCookie"] == null ? null : Request.Cookies["AuthCookie"].Value);
            string Response = DeleteService.Delete(id);
            Response = Response.Replace("\"", "");
            if (Response == "SuccesfullyDeletedYourself")
            {
                return RedirectToAction("SignOut", "Account");
            }
            else
            {
                return RedirectToAction("Dashboard");
            }
        }
        public ActionResult UpdateUser(User user)
        {
            try
            {
                UpdateService UpdateService = new UpdateService(Request.Cookies["AuthCookie"] == null ? null : Request.Cookies["AuthCookie"].Value);
                var Response = UpdateService.Update(user);
                return Json(new { result = Response });
            }
            catch(Exception ex)
            {
                return Json(new { result = ex.Message });
            }
        }
        public ActionResult Update(string id)
        {
            GetService GetService = new GetService(Request.Cookies["AuthCookie"].Value);
            List<User> GetList = GetService.Get<User>(null,id);
            return View(GetList[0]);
        }        
        public ActionResult CreateUser()
        {
            return View();
        }
        public ActionResult GetUser()
        {
            GetService GetService = new GetService(Request.Cookies["AuthCookie"].Value);
            List<User> GetList = GetService.Get<User>(null,null);
            return View(GetList[0]);
        }
    }
}