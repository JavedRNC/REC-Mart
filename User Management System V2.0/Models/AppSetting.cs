using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace User_Management_System_V2._0.Models
{
    public class AppSetting
    {
        public static string ConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;
        }
    }
}