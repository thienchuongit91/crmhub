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
using System.Drawing;

namespace api_intergration.Controllers
{
    public class DefaultController : ApiController
    {
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
                        if (jsonStringObj.action == null)
                        {
                            response = Request.CreateResponse(HttpStatusCode.BadRequest, "Missing action in request body");
                            return response;
                        }
                        else if(jsonStringObj.action == "Add")
                        {
                            string cmnd = jsonStringObj.data.CMND;
                            string email = jsonStringObj.data.Email;
                            if (!string.IsNullOrEmpty(cmnd) && !string.IsNullOrEmpty(email))
                            {
                                Bitmap qrCode = QRCodeHandlers.GenerateQRCode(cmnd);
                                MailHandlers.SendMail(email, qrCode);
                                VCBHandlers.sendInfo(cmnd);
                            }
                        }

                        string jsonString = JsonConvert.SerializeObject(jsonStringObj);

                        //create history data
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
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Internal Server Errors");
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
