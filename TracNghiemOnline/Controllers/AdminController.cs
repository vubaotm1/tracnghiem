using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using TracNghiemOnline.Models;
namespace TracNghiemOnline.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        AdminModel Model = new AdminModel();

        public ActionResult Index()
        {
            if (!Common.UserInfomation.IsAdmin())
                return View("Error");
            Model.UpdateLastLogin();
            Model.UpdateLastSeen("Trang Chủ", Url.Action("Index"));
            Dictionary<string, int> ListCount = Model.GetDashBoard();
            return View(ListCount);
        }
        public ActionResult Logout()
        {
            if (!Common.UserInfomation.IsAdmin())
                return View("Error");
            Common.UserInfomation.Reset();
            return RedirectToAction("Index", "Login");
        }
        public ActionResult AdminManager()
        {
            if (!Common.UserInfomation.IsAdmin())
                return View("Error");
            Model.UpdateLastSeen("Quản Lý Admin", Url.Action("AdminManager"));
            return View(Model.GetAdmins());
        }
        [HttpPost]
        public ActionResult AddAdmin(FormCollection form)
        {
            if (!Common.UserInfomation.IsAdmin())
                return View("Error");
            Model.UpdateLastSeen("Thêm Admin", Url.Action("AddAdmin"));
            string name = form["name"];
            string username = form["username"];
            string password = form["password"];
            string email = form["email"];
            string gender = form["gender"];
            string birthday = form["birthday"];
            bool add = Model.AddAdmin(name,username,password,gender,email,birthday);
            if(add)
            {
                TempData["status_id"] = true;
                TempData["status"] = "Thêm Thành Công";
            }
            else
            {
                TempData["status_id"] = false;
                TempData["status"] = "Thêm Thất Bại";
            }
            return RedirectToAction("AdminManager");
        }
        public ActionResult DeleteAdmin(string id)
        {
            if (!Common.UserInfomation.IsAdmin())
                return View("Error");
            Model.UpdateLastSeen("Xóa Admin", Url.Action("DeleteAdmin"));
            int id_admin = Convert.ToInt32(id);
            bool del = Model.DeleteAdmin(id_admin);
            if (del)
            {
                TempData["status_id"] = true;
                TempData["status"] = "Xóa Thành Công";
            }
            else
            {
                TempData["status_id"] = false;
                TempData["status"] = "Xóa Thất Bại";
            }
            return RedirectToAction("AdminManager");
        }
        [HttpPost]
        public ActionResult DeleteAdmin(FormCollection form)
        {
            if (!Common.UserInfomation.IsAdmin())
                return View("Error");
            Model.UpdateLastSeen("Xóa Admin", Url.Action("DeleteAdmin"));
            string[] ids = Regex.Split(form["checkbox"], ",");
            TempData["status_id"] = true;
            TempData["status"] = "Xóa Thất Bại ID: ";
            foreach (string id in ids)
            {
                int id_admin = Convert.ToInt32(id);
                bool del = Model.DeleteAdmin(id_admin);
                if (!del)
                {
                    TempData["status_id"] = false;
                    TempData["status"] += id_admin.ToString() + ",";
                }
            }
            if((bool)TempData["status_id"])
            {
                TempData["status"] = "Xóa Thành Công";
            }
            return RedirectToAction("AdminManager");
        }
        public ActionResult EditAdmin(string id)
        {
            if (!Common.UserInfomation.IsAdmin())
                return View("Error");
            int id_admin = Convert.ToInt32(id);
            try
            {
                admin admin = Model.GetAdmin(id_admin);
                Model.UpdateLastSeen("Sửa Admin " + admin.name, Url.Action("EditAdmin/" + id));
                return View(admin);
            } catch(Exception)
            {
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult EditAdmin(FormCollection form)
        {
            if (!Common.UserInfomation.IsAdmin())
                return View("Error");
            int id_admin = Convert.ToInt32(form["id_admin"]);
            string name = form["name"];
            string username = form["username"];
            string password = form["password"];
            string email = form["email"];
            string gender = form["gender"];
            string birthday = form["birthday"];
            bool edit = Model.EditAdmin(id_admin, name, username, password, gender, email, birthday);
            if (edit)
            {
                TempData["status_id"] = true;
                TempData["status"] = "Sửa Thành Công";
            }
            else
            {
                TempData["status_id"] = false;
                TempData["status"] = "Sửa Thất Bại";
            }
            return RedirectToAction("EditAdmin/"+id_admin);
        }
        public ActionResult TeacherManager()
        {
            if (!Common.UserInfomation.IsAdmin())
                return View("Error");
            Model.UpdateLastSeen("Quản Lý Giáo Viên", Url.Action("TeacherManager"));
            ViewBag.ListSpecialities = Model.GetSpecialities();
            return View(Model.GetTeachers());
        }
        [HttpPost]
        public ActionResult AddTeacher(FormCollection form)
        {
            if (!Common.UserInfomation.IsAdmin())
                return View("Error");
            Model.UpdateLastSeen("Thêm Giảng Viên", Url.Action("AddTeacher"));
            string name = form["name"];
            string username = form["username"];
            string password = form["password"];
            string email = form["email"];
            string gender = form["gender"];
            string birthday = form["birthday"];
            int id_speciality = Convert.ToInt32(form["id_speciality"]);
            bool add = Model.AddTeacher(name, username, password, gender, email, birthday, id_speciality);
            if (add)
            {
                TempData["status_id"] = true;
                TempData["status"] = "Thêm Thành Công";
            }
            else
            {
                TempData["status_id"] = false;
                TempData["status"] = "Thêm Thất Bại";
            }
            return RedirectToAction("TeacherManager");
        }
        public ActionResult DeleteTeacher(string id)
        {
            if (!Common.UserInfomation.IsAdmin())
                return View("Error");
            Model.UpdateLastSeen("Xóa Giảng Viên", Url.Action("DeleteTeacher"));
            int id_teacher = Convert.ToInt32(id);
            bool del = Model.DeleteTeacher(id_teacher);
            if (del)
            {
                TempData["status_id"] = true;
                TempData["status"] = "Xóa Thành Công";
            }
            else
            {
                TempData["status_id"] = false;
                TempData["status"] = "Xóa Thất Bại";
            }
            return RedirectToAction("TeacherManager");
        }
        [HttpPost]
        public ActionResult DeleteTeacher(FormCollection form)
        {
            if (!Common.UserInfomation.IsAdmin())
                return View("Error");
            Model.UpdateLastSeen("Xóa Giảng Viên", Url.Action("DeleteTeacher"));
            string[] ids = Regex.Split(form["checkbox"], ",");
            TempData["status_id"] = true;
            TempData["status"] = "Xóa Thất Bại ID: ";
            foreach (string id in ids)
            {
                int id_teacher = Convert.ToInt32(id);
                bool del = Model.DeleteTeacher(id_teacher);
                if (!del)
                {
                    TempData["status_id"] = false;
                    TempData["status"] += id_teacher.ToString() + ",";
                }
            }
            if ((bool)TempData["status_id"])
            {
                TempData["status"] = "Xóa Thành Công";
            }
            return RedirectToAction("TeacherManager");
        }
        public ActionResult EditTeacher(string id)
        {
            if (!Common.UserInfomation.IsAdmin())
                return View("Error");
            int id_teacher = Convert.ToInt32(id);
            try
            {
                teacher teacher = Model.GetTeacher(id_teacher);
                Model.UpdateLastSeen("Sửa Giảng Viên " + teacher.name, Url.Action("EditTeacher/" + id));
                ViewBag.ListSpecialities = Model.GetSpecialities();
                return View(teacher);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult EditTeacher(FormCollection form)
        {
            if (!Common.UserInfomation.IsAdmin())
                return View("Error");
            int id_teacher = Convert.ToInt32(form["id_teacher"]);
            string name = form["name"];
            string username = form["username"];
            string password = form["password"];
            string email = form["email"];
            string gender = form["gender"];
            string birthday = form["birthday"];
            int id_speciality = Convert.ToInt32(form["id_speciality"]);
            bool edit = Model.EditTeacher(id_teacher, name, username, password, gender, email, birthday, id_speciality);
            if (edit)
            {
                TempData["status_id"] = true;
                TempData["status"] = "Sửa Thành Công";
            }
            else
            {
                TempData["status_id"] = false;
                TempData["status"] = "Sửa Thất Bại";
            }
            return RedirectToAction("EditTeacher/" + id_teacher);
        }
        public ActionResult StudentManager()
        {
            if (!Common.UserInfomation.IsAdmin())
                return View("Error");
            Model.UpdateLastSeen("Quản Lý Sinh Viên", Url.Action("StudentManager"));
            ViewBag.ListSpecialities = Model.GetSpecialities();
            ViewBag.ListClass = Model.GetClasses();
            return View(Model.GetStudents());
        }
        [HttpPost]
        public ActionResult AddStudent(FormCollection form)
        {
            if (!Common.UserInfomation.IsAdmin())
                return View("Error");
            Model.UpdateLastSeen("Thêm Sinh Viên", Url.Action("AddStudent"));
            string name = form["name"];
            string username = form["username"];
            string password = form["password"];
            string email = form["email"];
            string gender = form["gender"];
            string birthday = form["birthday"];
            int id_speciality = Convert.ToInt32(form["id_speciality"]);
            int id_class = Convert.ToInt32(form["id_class"]);
            bool add = Model.AddStudent(name, username, password, gender, email, birthday, id_speciality, id_class);
            if (add)
            {
                TempData["status_id"] = true;
                TempData["status"] = "Thêm Thành Công";
            }
            else
            {
                TempData["status_id"] = false;
                TempData["status"] = "Thêm Thất Bại";
            }
            return RedirectToAction("StudentManager");
        }
        public ActionResult DeleteStudent(string id)
        {
            if (!Common.UserInfomation.IsAdmin())
                return View("Error");
            Model.UpdateLastSeen("Xóa Sinh Viên", Url.Action("DeleteStudent"));
            int id_student = Convert.ToInt32(id);
            bool del = Model.DeleteStudent(id_student);
            if (del)
            {
                TempData["status_id"] = true;
                TempData["status"] = "Xóa Thành Công";
            }
            else
            {
                TempData["status_id"] = false;
                TempData["status"] = "Xóa Thất Bại";
            }
            return RedirectToAction("StudentManager");
        }
        [HttpPost]
        public ActionResult DeleteStudent(FormCollection form)
        {
            if (!Common.UserInfomation.IsAdmin())
                return View("Error");
            Model.UpdateLastSeen("Xóa Sinh Viên", Url.Action("DeleteStudent"));
            string[] ids = Regex.Split(form["checkbox"], ",");
            TempData["status_id"] = true;
            TempData["status"] = "Xóa Thất Bại ID: ";
            foreach (string id in ids)
            {
                int id_student = Convert.ToInt32(id);
                bool del = Model.DeleteStudent(id_student);
                if (!del)
                {
                    TempData["status_id"] = false;
                    TempData["status"] += id_student.ToString() + ",";
                }
            }
            if ((bool)TempData["status_id"])
            {
                TempData["status"] = "Xóa Thành Công";
            }
            return RedirectToAction("StudentManager");
        }
        public ActionResult EditStudent(string id)
        {
            if (!Common.UserInfomation.IsAdmin())
                return View("Error");
            int id_student = Convert.ToInt32(id);
            try
            {
                student student = Model.GetStudent(id_student);
                Model.UpdateLastSeen("Sửa Sinh Viên " + student.name, Url.Action("EditStudent/" + id));
                ViewBag.ListSpecialities = Model.GetSpecialities();
                ViewBag.ListClass = Model.GetClasses();
                return View(student);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult EditStudent(FormCollection form)
        {
            if (!Common.UserInfomation.IsAdmin())
                return View("Error");
            int id_student = Convert.ToInt32(form["id_student"]);
            string name = form["name"];
            string username = form["username"];
            string password = form["password"];
            string email = form["email"];
            string gender = form["gender"];
            string birthday = form["birthday"];
            int id_speciality = Convert.ToInt32(form["id_speciality"]);
            int id_class = Convert.ToInt32(form["id_class"]);
            bool edit = Model.EditStudent(id_student, name, username, password, gender, email, birthday, id_speciality, id_class);
            if (edit)
            {
                TempData["status_id"] = true;
                TempData["status"] = "Sửa Thành Công";
            }
            else
            {
                TempData["status_id"] = false;
                TempData["status"] = "Sửa Thất Bại";
            }
            return RedirectToAction("EditStudent/" + id_student);
        }
        public ActionResult ClassManager()
        {
            if (!Common.UserInfomation.IsAdmin())
                return View("Error");
            Model.UpdateLastSeen("Quản Lý Khóa/Lớp", Url.Action("ClassManager"));
            return View();
        }
        public ActionResult SpecialityManager()
        {
            if (!Common.UserInfomation.IsAdmin())
                return View("Error");
            Model.UpdateLastSeen("Quản Lý Ngành", Url.Action("SpecialityManager"));
            return View();
        }
        public ActionResult SubjectManager()
        {
            if (!Common.UserInfomation.IsAdmin())
                return View("Error");
            Model.UpdateLastSeen("Quản Lý Môn", Url.Action("SubjectManager"));
            return View();
        }
        public ActionResult QuestionManager()
        {
            if (!Common.UserInfomation.IsAdmin())
                return View("Error");
            Model.UpdateLastSeen("Quản Lý Câu Hỏi", Url.Action("QuestionManager"));
            return View();
        }
        public ActionResult TestManager()
        {
            if (!Common.UserInfomation.IsAdmin())
                return View("Error");
            Model.UpdateLastSeen("Quản Lý Bài Thi", Url.Action("TestManager"));
            return View();
        }
    }
}