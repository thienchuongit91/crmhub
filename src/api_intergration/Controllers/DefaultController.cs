using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using api_intergration.Handlers;

namespace api_intergration.Controllers
{
    public class DefaultController : ApiController
    {
        [HttpPost]
        [Route("api/shb/checklead")]
        public async System.Threading.Tasks.Task<string> CheckLeadAsync([FromBody] string jsonObj)
        {
            SHBFCHandlers handlers = new SHBFCHandlers();
            var result = await handlers.CheckLeadAsync(jsonObj);
            return result;
        }
    }
}
