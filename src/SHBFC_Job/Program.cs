using log4net.Config;
using SHBFC_Job.Handlers;
using SHBFC_Job.Models;
using SHBFC_Job.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHBFC_Job
{
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
            try
            {
                List<DataCollection> dataCollections = new List<DataCollection>();
                dataCollections = DataCollectionRepository.GetLatestDataCollection();
                if (dataCollections != null && dataCollections.Count > 0)
                {
                    foreach (var item in dataCollections)
                    {
                        string result = "";
                        string jsondata = item.data;
                        result = SHBFCHandlers.SHBFCIntergration(jsondata);
                        CheckListResult checkListResult = new CheckListResult();
                        checkListResult.DataId = item.id;
                        checkListResult.Result = result;
                        checkListResult.Called_Date = DateTime.Now;

                        //update status Data collection
                        item.IsSendToSHB = true;
                        DataCollectionDbContext dataCollectionDbContext = new DataCollectionDbContext();
                        var datatemp = dataCollectionDbContext.dataCollections.Where(i => i.id == item.id).First();
                        datatemp.IsSendToSHB = true;
                        dataCollectionDbContext.SaveChanges();

                        //add to checklist table
                        CheckListResultDbContext checkListResultDbContext = new CheckListResultDbContext();
                        checkListResultDbContext.CheckListResultDb.Add(checkListResult);
                        checkListResultDbContext.SaveChanges();
                    }
                }
                else
                {
                    log.InfoFormat("{0}:{1}", nameof(Main), "No records found");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("{0}:{1}", nameof(Main), ex);
            }
        }
    }
}
