using crm_data_hub.Hubs;
using crm_data_hub.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace crm_data_hub.Repositories
{
    public class DataCollectionRepository
    {
        readonly string _connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public DataCollection GetLatestData()
        {
            DataCollection dataCollection = new DataCollection();
            using (var cnn = new SqlConnection(_connString))
            {
                cnn.Open();
                using (var command = new SqlCommand("select top 1 * from DataCollection order by Modified_Date desc", cnn))
                {
                    command.Notification = null;
                    var dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
                    if (cnn.State == ConnectionState.Closed)
                        cnn.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        dataCollection.id = (int)reader["id"];
                        dataCollection.data = reader["data"].ToString();
                        dataCollection.IsSendToSHB = (bool)reader["IsSendToSHB"];
                        dataCollection.Created_Date = (DateTime)reader["Created_Date"];
                        dataCollection.Modified_Date = (DateTime)reader["Modified_Date"];
                    }
                    
                }
            }
            return dataCollection;
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                DbChangedHub.DbChangeTrigger();
            }
        }
    }
}