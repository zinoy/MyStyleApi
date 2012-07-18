using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace EvoqueMyStyle.Website
{
    public static class ConfigHelper
    {
        public static string APISecret = ConfigurationManager.AppSettings["API_Secret"];
        public static string SinaAppKey = ConfigurationManager.AppSettings["Sina_APPKEY"];
        public static string SinaSecret = ConfigurationManager.AppSettings["Sina_APPSECRET"];
        public static string ImagePath = ConfigurationManager.AppSettings["Image_Path"];
        public static string ExternalSecret = ConfigurationManager.AppSettings["External_Secret"];
    }
}