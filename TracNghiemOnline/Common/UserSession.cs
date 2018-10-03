using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TracNghiemOnline.Common
{
    public static class UserSession
    {
        public static void AddSession(string sessionName, object obj)
        {
            HttpContext.Current.Session.Add(sessionName, obj);
        }
        public static object GetSession(string sessionName)
        {
            return HttpContext.Current.Session[sessionName];
        }
        public static void RemoveSession(string sessionName)
        {
            HttpContext.Current.Session.Remove(sessionName);
        }
    }
}