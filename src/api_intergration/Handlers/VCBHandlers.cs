using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace api_intergration.Handlers
{
    public static class VCBHandlers
    {
        public static string api_base_url = Common.Constant.VCB_API_BASE();
        static HttpClient client = new HttpClient();

        public async static void sendInfo(string cmnd)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                var parameters = new Dictionary<string, string> { { "cmnd", cmnd }};
                var encodedContent = new FormUrlEncodedContent(parameters);
                var httpResponseMessage = await client.PostAsync(api_base_url, encodedContent);
                string resp = await httpResponseMessage.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                
                throw;
            }

        }
    }
}