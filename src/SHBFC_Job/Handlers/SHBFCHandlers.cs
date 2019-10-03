using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SHBFC_Job.Common;

namespace SHBFC_Job.Handlers
{
    public class SHBFCHandlers
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly string SHBBase_url = Constant.SHBC_API_BASE();

        public static string SHBFCIntergration(string jsonString)
        {
            var result = "";
            try
            {
                var dataObject = JsonConvert.DeserializeObject<dynamic>(jsonString);
                if (dataObject.action != null)
                {
                    int action = dataObject.action;
                    string dataJsonString = dataObject.data.ToString(Formatting.None);
                    result = CallLeadApi(dataJsonString, action);
                    return result;
                }
                else
                {
                    return "no action found";
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("{0}:{1}",nameof(SHBFCIntergration),ex);
            }
            return result;
        }

        private static string CallLeadApi(string dataJsonString, int action)
        {
            string result = "";
            try
            {
                var token = GetToken();
                if (!string.IsNullOrWhiteSpace(token))
                {
                    string leadURL = "";
                    var method = Method.POST;
                    switch (action)
                    {
                        case 1:
                            leadURL = "/crm/ts/v1/checklead";
                            break;
                        case 2:
                            leadURL = "/crm/ts/v1/sendlead";
                            break;
                        case 3:
                            leadURL = "/crm/ts/v1/tracklead/{0}";//{leadid}
                            method = Method.GET;
                            break;
                        case 4:
                            leadURL = "/crm/ts/v1/sendtql";
                            break;
                        case 5:
                            leadURL = "/crm/ts/v1/resendcourier";
                            break;
                        default:
                            break;
                    }
                    result = Checklead(token, dataJsonString,leadURL,method);
                }
                else
                {
                    result = "Invalid token";
                    log.ErrorFormat("{0}:{1}", nameof(GetToken), result);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("{0}:{1}", nameof(GetToken), ex); 
            }
            return result;
        }

        private static string Checklead(string token, string dataJsonString, string leadURL, Method method)
        {
            string result = "";
            try
            {
                var client = new RestClient(SHBBase_url + leadURL);
                var request = new RestRequest(method);
                request.AddHeader("Authorization", token);
                request.AddHeader("Content-type", "application/json");
                request.AddParameter("application/json", dataJsonString, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                string body = response.Content;
                if (!string.IsNullOrWhiteSpace(body))
                {
                    result = body;
                    log.InfoFormat("{0}:{1}", nameof(Checklead), "API url: "+leadURL+" Result: "+result+"");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("{0}:{1}", nameof(GetToken), ex);
            }
            return result;
        }

        private static string GetToken()
        {
            string tokenString = "";
            try
            {
                string[] token_api_info = Constant.TOKEN_API();
                var client = new RestClient(token_api_info[0]);
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("application/x-www-form-urlencoded", "client_id=" + token_api_info[1] + "&client_secret=" + token_api_info[2] + "", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                string body = response.Content;
                
                if (!string.IsNullOrWhiteSpace(body))
                {
                    var tokenObject = JsonConvert.DeserializeObject<dynamic>(body);
                    tokenString = tokenObject.token_type.ToString() + " " + tokenObject.access_token.ToString();
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("{0}:{1}", nameof(GetToken), ex);
            }
            
            return tokenString;
        }
    }
}
