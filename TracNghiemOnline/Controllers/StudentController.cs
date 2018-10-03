using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TracNghiemOnline.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            if (!Common.UserInfomation.IsLogin)
                return RedirectToAction("Index", "Login");
            return View();
        }
        public ActionResult Logout()
        {
            //Common.UserSession.RemoveSession("User");
            //Common.UserSession.RemoveSession("Permission");
            Common.UserInfomation.Reset();
            return RedirectToAction("Index", "Login");
        }
    }
}