using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHBFC_Job.Common
{
    public class Constant
    {
        public static string SHBC_API_BASE()
        {
            string url = ConfigurationManager.AppSettings["shb_base_url"];
            string port = ConfigurationManager.AppSettings["shb_base_port"];
            return string.Format("{0}:{1}", url, port);
        }

        public static string[] TOKEN_API()
        {
            string token_api_url = ConfigurationManager.AppSettings["get_token_api"];
            string client_id = ConfigurationManager.AppSettings["client_id"];
            string client_secret = ConfigurationManager.AppSettings["client_secret"];
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
