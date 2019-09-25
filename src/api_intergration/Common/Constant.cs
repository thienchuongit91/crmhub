using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace api_intergration.Common
{
    public class Constant
    {
        public static string SHBC_API_BASE()
        {
            string url = WebConfigurationManager.AppSettings["shb_base_url"];
            string port = WebConfigurationManager.AppSettings["shb_base_port"];
            return string.Format("{0}:{1}", url, port);
        }
    }
}