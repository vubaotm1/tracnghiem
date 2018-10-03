using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TracNghiemOnline.Models;

namespace TracNghiemOnline.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            if (Common.UserInfomation.IsLogin)
            {
                if (Common.UserInfomation.id_permission == 1)
                    return RedirectToAction("Index", "Admin");
                if (Common.UserInfomation.id_permission == 2)
                    return RedirectToAction("Index", "Teacher");
                if (Common.UserInfomation.id_permission == 3)
                    return RedirectToAction("Index", "Student");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginModel model)
        {
            if(ModelState.IsValid)
            {
                if (model.IsValid(model))
                {
                    if (Common.UserInfomation.id_permission == 1)
                        return RedirectToAction("Index", "Admin");
                    if (Common.UserInfomation.id_permission == 2)
                        return RedirectToAction("Index", "Teacher");
                    if (Common.UserInfomation.id_permission == 3)
                        return RedirectToAction("Index", "Student");
                }
                else
                    ViewBag.error = "Tài khoản hoặc mật khẩu không đúng";
            } else
                ViewBag.error = "Có lỗi xảy ra trong quá trình xử lý, vui lòng thử lại sau.";
            return View();
        }
    }
}