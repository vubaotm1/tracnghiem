using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TracNghiemOnline.Models
{
    public class AdminModel
    {
        trac_nghiem_onlineEntities db = new trac_nghiem_onlineEntities();

        public void UpdateLastLogin()
        {
            var update = (from x in db.admins where x.id_admin == Common.UserInfomation.id_user select x).Single();
            update.last_login = DateTime.Now;
            db.SaveChanges();
        }
        public void UpdateLastSeen(string name,string url)
        {
            var update = (from x in db.admins where x.id_admin == Common.UserInfomation.id_user select x).Single();
            update.last_seen = name;
            update.last_seen_url = url;
            db.SaveChanges();
        }
        public Dictionary<string, int> GetDashBoard()
        {
            var ListCount = new Dictionary<string, int>();
            int CountAdmin = db.admins.Count();
            ListCount.Add("CountAdmin", CountAdmin);
            int CountTeacher = db.teachers.Count();
            ListCount.Add("CountTeacher", CountTeacher);
            int CountStudent = db.students.Count();
            ListCount.Add("CountStudent", CountStudent);
            int CountGrade = db.grades.Count();
            ListCount.Add("CountGrade", CountGrade);
            int CountClass = db.classes.Count();
            ListCount.Add("CountClass", CountClass);
            int CountSpeciality = db.specialities.Count();
            ListCount.Add("CountSpeciality", CountSpeciality);
            int CountSubject = db.subjects.Count();
            ListCount.Add("CountSubject", CountSubject);
            int CountQuestion = db.questions.Count();
            ListCount.Add("CountQuestion", CountQuestion);
            int CountTest = db.tests.Count();
            ListCount.Add("CountTest", CountTest);
            return ListCount;
        }
        public List<admin> GetAdmins()
        {
            return db.admins.ToList();
        }
        public admin GetAdmin(int id)
        {
            admin admin = new admin();
            try
            {
                admin = db.admins.SingleOrDefault(x => x.id_admin == id);
            } catch (Exception e) {
                Console.WriteLine(e);
            }
            return admin;
        }
        public bool AddAdmin(string name ,string username, string password, string gender, string email, string birthday)
        {
            var admin = new admin();
            admin.name = name;
            admin.username = username;
            admin.password = Common.Encryptor.MD5Hash(password);
            admin.gender = gender;
            admin.email = email;
            admin.id_permission = 1;
            admin.avatar = "avatar-default.jpg";
            admin.birthday = Convert.ToDateTime(birthday);
            try
            {
                db.admins.Add(admin);
                db.SaveChanges();
            } catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }
        public bool DeleteAdmin(int id)
        {
            try
            {
                var delete = (from x in db.admins where x.id_admin == id select x).Single();
                db.admins.Remove(delete);
                db.SaveChanges();
            } catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }
        public bool EditAdmin(int id_admin, string name, string username, string password, string gender, string email, string birthday)
        {
            try
            {
                var update = (from x in db.admins where x.id_admin == id_admin select x).Single();
                update.name = name;
                update.username = username;
                update.email = email;
                update.gender = gender;
                update.birthday = Convert.ToDateTime(birthday);
                if(password != null)
                    update.password = Common.Encryptor.MD5Hash(password);
                db.SaveChanges();
            } catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }
        public List<TeacherViewModel> GetTeachers()
        {
            List<TeacherViewModel> teachers = (from x in db.teachers join s in db.specialities on x.id_speciality equals s.id_speciality select new TeacherViewModel{ teacher = x, speciality = s}).ToList();
            return teachers;
        }
        public List<speciality> GetSpecialities()
        {
            return db.specialities.ToList();
        }
        public bool AddTeacher(string name, string username, string password, string gender, string email, string birthday, int id_speciality)
        {
            var teacher = new teacher();
            teacher.name = name;
            teacher.username = username;
            teacher.password = Common.Encryptor.MD5Hash(password);
            teacher.gender = gender;
            teacher.email = email;
            teacher.id_permission = 2;
            teacher.id_speciality = id_speciality;
            teacher.avatar = "avatar-default.jpg";
            teacher.birthday = Convert.ToDateTime(birthday);
            try
            {
                db.teachers.Add(teacher);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }
        public bool DeleteTeacher(int id)
        {
            try
            {
                var delete = (from x in db.teachers where x.id_teacher == id select x).Single();
                db.teachers.Remove(delete);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }
        public teacher GetTeacher(int id)
        {
            teacher teacher = new teacher();
            try
            {
                teacher = db.teachers.SingleOrDefault(x => x.id_teacher == id);
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }
            return teacher;
        }
        public bool EditTeacher(int id_teacher, string name, string username, string password, string gender, string email, string birthday, int id_speciality)
        {
            try
            {
                var update = (from x in db.teachers where x.id_teacher == id_teacher select x).Single();
                update.name = name;
                update.username = username;
                update.email = email;
                update.gender = gender;
                update.id_speciality = id_speciality;
                update.birthday = Convert.ToDateTime(birthday);
                if (password != null)
                    update.password = Common.Encryptor.MD5Hash(password);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }
        public List<@class> GetClasses()
        {
            return db.classes.ToList();
        }
        public List<StudentViewModel> GetStudents()
        {
            List<StudentViewModel> students = (from x in db.students
                                               join s in db.specialities on x.id_speciality equals s.id_speciality
                                               join c in db.classes on x.id_class equals c.id_class
                                               select new StudentViewModel { student = x, speciality = s, Class = c }).ToList();
            return students;
        }
        public bool AddStudent(string name, string username, string password, string gender, string email, string birthday, int id_speciality, int id_class)
        {
            var student = new student();
            student.name = name;
            student.username = username;
            student.password = Common.Encryptor.MD5Hash(password);
            student.gender = gender;
            student.email = email;
            student.id_permission = 3;
            student.id_speciality = id_speciality;
            student.id_class = id_class;
            student.avatar = "avatar-default.jpg";
            student.birthday = Convert.ToDateTime(birthday);
            try
            {
                db.students.Add(student);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }
        public bool DeleteStudent(int id)
        {
            try
            {
                var delete = (from x in db.students where x.id_student == id select x).Single();
                db.students.Remove(delete);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }
        public student GetStudent(int id)
        {
            student student = new student();
            try
            {
                student = db.students.SingleOrDefault(x => x.id_student == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return student;
        }
        public bool EditStudent(int id_student, string name, string username, string password, string gender, string email, string birthday, int id_speciality, int id_class)
        {
            try
            {
                var update = (from x in db.students where x.id_student == id_student select x).Single();
                update.name = name;
                update.username = username;
                update.email = email;
                update.gender = gender;
                update.id_speciality = id_speciality;
                update.id_class = id_class;
                update.birthday = Convert.ToDateTime(birthday);
                if (password != null)
                    update.password = Common.Encryptor.MD5Hash(password);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }
    }
}