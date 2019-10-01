using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace crm_data_hub.Hubs
{
    public class DbChangedHub : Hub
    {
        [HubMethodName("DbChangeTrigger")]
        public static void DbChangeTrigger()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<DbChangedHub>();
            context.Clients.All.GetLatestData();
        }
    }
}