using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TracNghiemOnline.Models
{
    public class LoginModel
    {
        trac_nghiem_onlineEntities db = new trac_nghiem_onlineEntities();

        [Display(Name = "Tài Khoản")]
        public string Username { get; set; }
        [Display(Name = "Mật Khẩu")]
        public string Password { get; set; }

        public bool IsValid(LoginModel model)
        {
            model.Password = Common.Encryptor.MD5Hash(model.Password);
            try
            {
                if (Convert.ToBoolean(db.admins.First(x => x.username == model.Username && x.password == model.Password).id_admin))
                {
                    SetAdminSession(db.admins.First(x => x.username == model.Username && x.password == model.Password).id_admin);
                    return true;
                }
            } catch(Exception){}
            try
            {
                if (Convert.ToBoolean(db.teachers.First(x => x.username == model.Username && x.password == model.Password).id_teacher))
                {
                    SetTeacherSession(db.teachers.First(x => x.username == model.Username && x.password == model.Password).id_teacher);
                    return true;
                }
            } catch (Exception) { }
            try
            {
                if (Convert.ToBoolean(db.students.First(x => x.username == model.Username && x.password == model.Password).id_student))
                {
                    SetStudentSession(db.students.First(x => x.username == model.Username && x.password == model.Password).id_student);
                    return true;
                }
            } catch (Exception) { }
            return false; 
        }
        public void SetAdminSession(int userID)
        {
            admin user = db.admins.SingleOrDefault(x => x.id_admin == userID);
            Common.UserInfomation.IsLogin = true;
            Common.UserInfomation.id_user = user.id_admin;
            Common.UserInfomation.username = user.username;
            Common.UserInfomation.email = user.email;
            Common.UserInfomation.avatar = user.avatar;
            Common.UserInfomation.name = user.name;
            Common.UserInfomation.gender = user.gender;
            Common.UserInfomation.birthday = user.birthday;
            Common.UserInfomation.phone = user.phone;
            Common.UserInfomation.id_permission = user.id_permission;
            Common.UserInfomation.last_login = user.last_login;
            Common.UserInfomation.last_seen = user.last_seen;
            Common.UserInfomation.last_seen_url = user.last_seen_url;
            Common.UserInfomation.timestamps = user.timestamps;
            //Common.UserSession.AddSession("Permission", admin.id_permission);
            //Common.UserSession.AddSession("User", user);
        }
        public void SetTeacherSession(int userID)
        {
            teacher user = db.teachers.SingleOrDefault(x => x.id_teacher == userID);
            Common.UserInfomation.IsLogin = true;
            Common.UserInfomation.id_user = user.id_teacher;
            Common.UserInfomation.username = user.username;
            Common.UserInfomation.email = user.email;
            Common.UserInfomation.avatar = user.avatar;
            Common.UserInfomation.name = user.name;
            Common.UserInfomation.gender = user.gender;
            Common.UserInfomation.birthday = user.birthday;
            Common.UserInfomation.phone = user.phone;
            Common.UserInfomation.id_permission = user.id_permission;
            Common.UserInfomation.id_speciality = user.id_speciality;
            Common.UserInfomation.last_login = user.last_login;
            Common.UserInfomation.last_seen = user.last_seen;
            Common.UserInfomation.last_seen_url = user.last_seen_url;
            Common.UserInfomation.timestamps = user.timestamps;
            //Common.UserSession.AddSession("Permission", teacher.id_permission);
            //Common.UserSession.AddSession("User", user);
        }
        public void SetStudentSession(int userID)
        {
            student user = db.students.SingleOrDefault(x => x.id_student == userID);
            Common.UserInfomation.IsLogin = true;
            Common.UserInfomation.id_user = user.id_student;
            Common.UserInfomation.username = user.username;
            Common.UserInfomation.email = user.email;
            Common.UserInfomation.avatar = user.avatar;
            Common.UserInfomation.name = user.name;
            Common.UserInfomation.gender = user.gender;
            Common.UserInfomation.birthday = user.birthday;
            Common.UserInfomation.phone = user.phone;
            Common.UserInfomation.id_permission = user.id_permission;
            Common.UserInfomation.id_speciality = user.id_speciality;
            Common.UserInfomation.is_testing = user.is_testing;
            Common.UserInfomation.time_start = user.time_start;
            Common.UserInfomation.time_remaining = user.time_remaining;
            Common.UserInfomation.last_login = user.last_login;
            Common.UserInfomation.last_seen = user.last_seen;
            Common.UserInfomation.last_seen_url = user.last_seen_url;
            Common.UserInfomation.timestamps = user.timestamps;
            //Common.UserSession.AddSession("Permission", student.id_permission);
            //Common.UserSession.AddSession("User", user);
        }
    }
}