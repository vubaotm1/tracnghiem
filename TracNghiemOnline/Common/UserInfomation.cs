using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TracNghiemOnline.Common
{
    public static class UserInfomation
    {
        public static bool IsLogin { get; set; } = false;
        public static int id_user { get; set; }
        public static string username { get; set; }
        public static string email { get; set; }
        public static string avatar { get; set; }
        public static string name { get; set; }
        public static string gender { get; set; }
        public static System.DateTime birthday { get; set; }
        public static string phone { get; set; }
        public static int id_permission { get; set; }
        public static int id_class { get; set; }
        public static int id_speciality { get; set; }
        public static Nullable<int> is_testing { get; set; }
        public static Nullable<System.DateTime> time_start { get; set; }
        public static string time_remaining { get; set; }
        public static Nullable<System.DateTime> last_login { get; set; }
        public static string last_seen { get; set; }
        public static string last_seen_url { get; set; }
        public static Nullable<System.DateTime> timestamps { get; set; }

        public static void Reset()
        {
            IsLogin = false;
        }
        public static bool IsAdmin()
        {
            if (Common.UserInfomation.IsLogin && Common.UserInfomation.id_permission == 1)
                return true;
            return false;
        }
        public static bool IsTeacher()
        {
            if (Common.UserInfomation.IsLogin && Common.UserInfomation.id_permission == 2)
                return true;
            return false;
        }
        public static bool IsStudent()
        {
            if (Common.UserInfomation.IsLogin && Common.UserInfomation.id_permission == 3)
                return true;
            return false;
        }
    }
}