using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace api_intergration.Handlers
{
    public class SHBFCHandlers
    {
        public static string api_base_url = Common.Constant.SHBC_API_BASE();
        static HttpClient client = new HttpClient();
        public async Task<string> CheckLeadAsync(string jsonString)
        {
            string CheckleadApi = "/crm/ts/v1/checklead";
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(api_base_url + CheckleadApi, content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string result = await response.Content.ReadAsStringAsync();
                return result;
            }
            else
            {
                return "Error";
            }
            
        }
    }
}