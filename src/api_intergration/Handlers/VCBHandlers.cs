using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace api_intergration.Handlers
{
    public static class VCBHandlers
    {
        public static string api_base_url = Common.Constant.VCB_API_BASE();
        static HttpClient client = new HttpClient();

        public static void sendInfo(string cmnd)
        {
            try
            {
                string body = "{ 'cmnd' : " + cmnd + "}";
                HttpContent httpContent = new StringContent(body);
                client.PostAsync(api_base_url, httpContent);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}