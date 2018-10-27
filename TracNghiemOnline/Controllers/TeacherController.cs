using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TracNghiemOnline.Common;
namespace TracNghiemOnline.Controllers
{
    public class TeacherController : Controller
    {
        User user = new User();
        // GET: Teacher
        public ActionResult Index()
        {
            if (!user.IsLogin())
                return RedirectToAction("Index", "Login");
            return View();
        }
        public ActionResult Logout()
        {
            //Common.UserSession.RemoveSession("User");
            //Common.UserSession.RemoveSession("Permission");
            user.Reset();
            return RedirectToAction("Index", "Login");
        }
    }
}