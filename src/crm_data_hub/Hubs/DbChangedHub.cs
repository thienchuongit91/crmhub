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
        private static string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

        [HubMethodName("DBChangedTrigger")]
        public static void DbChangeTrigger()
        {

        }
    }
}