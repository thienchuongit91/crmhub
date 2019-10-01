using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using api_intergration.Handlers;
using Newtonsoft.Json;
using api_intergration.Models;

namespace api_intergration.Controllers
{
    public class DefaultController : ApiController
    {
        [HttpPost]
        [Route("api/shb/checklead")]
        public async Task<string> CheckLeadAsync([FromBody] string jsonObj)
        {
            SHBFCHandlers handlers = new SHBFCHandlers();
            var result = await handlers.CheckLeadAsync(jsonObj);
            return result;
        }

        [HttpPost]
        [Route("api/datahub/collect")]
        public HttpResponseMessage DataCollection([FromBody] dynamic jsonStringObj)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            DataCollectionDbContext dbContext = new DataCollectionDbContext();
            try
            {
                var headers = Request.Headers;
                if (headers.Contains("client_id") && headers.Contains("client_secret"))
                {
                    if (headers.GetValues("client_id").FirstOrDefault() == "eac5d782-3a48-4a23-ae97-002681dc4dfd" && headers.GetValues("client_secret").FirstOrDefault() == "182548a4d4b34e9aaee83380730f4152")
                    {
                        string jsonString = JsonConvert.SerializeObject(jsonStringObj);
                        var currentdate = DateTime.Now;
                        DataCollection dataCollection = new DataCollection();
                        dataCollection.data = jsonString;
                        dataCollection.IsSendToSHB = false;
                        dataCollection.Created_Date = currentdate;
                        dataCollection.Modified_Date = currentdate;
                        dbContext.dataCollections.Add(dataCollection);
                        dbContext.SaveChanges();
                        int dataid = dataCollection.id;
                        response = Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(new String[] {
                            "Sending data successfully",
                            "Data id: "+dataid.ToString()+""
                        }));
                    }
                    else
                    {
                        response = Request.CreateResponse(HttpStatusCode.Unauthorized, "error: client_id or client_secret is incorrect");
                    }
                    
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, JsonConvert.SerializeObject("Missing client_id or client_secret"));
                    
                }
                return response;
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
                return response;
            }
           
        }
        [HttpGet]
        [Route("api/datahub/test")]
        public HttpResponseMessage test()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = Request.CreateResponse(HttpStatusCode.OK, "TESTING OK!");
            return response;
        }
    }
}
