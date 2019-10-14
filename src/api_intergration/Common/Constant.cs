using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace api_intergration.Common
{
    public class Constant
    {

        public static string MAIL_PORT {
            get => WebConfigurationManager.AppSettings["mail_port"];
        }

        public static string SHBC_API_BASE()
        {
            string url = WebConfigurationManager.AppSettings["shb_base_url"];
            string port = WebConfigurationManager.AppSettings["shb_base_port"];
            return string.Format("{0}:{1}", url, port);
        }

        public static string VCB_API_BASE()
        {
            return WebConfigurationManager.AppSettings["vcb_update_customer_url"];
        }

        public static string[] TOKEN_API()
        {
            string token_api_url = WebConfigurationManager.AppSettings["get_token_api"];
            string client_id = WebConfigurationManager.AppSettings["client_id"];
            string client_secret = WebConfigurationManager.AppSettings["client_secret"];
            string[] token_api_info = new string[]
            {
                token_api_url,
                client_id,
                client_secret
            };
            return token_api_info;
        }
    }
}