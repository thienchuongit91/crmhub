using api_intergration.Common;
using Newtonsoft.Json;
using RestSharp;
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

        internal static string CallLeadAPI(string jsonString)
        {
            
            var dataObject = JsonConvert.DeserializeObject<dynamic>(jsonString);
            if (dataObject.action != null)
            {   
                int action = dataObject.action;
                string dataJsonString = dataObject.data.ToString(Formatting.None);
                string result = "";
                result = CallSHBFCApi(dataJsonString, action);
                return result;
            }
            else
            {
                return "ERROR";
            }
        }

        private static string CallSHBFCApi(string dataJsonString, int action)
        {
            string tokenJson = GetToken();
            var tokenObject = JsonConvert.DeserializeObject<dynamic>(tokenJson);
            string authString = tokenObject.token_type.ToString() + " " + tokenObject.access_token.ToString();
            string result = "";
            switch (action)
            {
                case 1:
                    result = CallSHBFCApi_CheckList(dataJsonString, authString);
                    break;
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                default:
                    break;
            }
            return tokenJson;
        }

        private static string CallSHBFCApi_CheckList(string dataJsonString, string authString)
        {
            throw new NotImplementedException();
        }

        private static string GetToken()
        {
            string[] token_api_info = Constant.TOKEN_API();
            var client = new RestClient(token_api_info[0]);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", "client_id="+token_api_info[1]+ "&client_secret="+token_api_info[2]+"", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            string body = response.Content;
            return body;
        }
    }
}