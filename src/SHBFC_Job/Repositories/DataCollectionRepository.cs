using SHBFC_Job.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHBFC_Job.Repositories
{
    
    public class DataCollectionRepository
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static List<DataCollection> GetLatestDataCollection()
        {
            List<DataCollection> dataCollections = new List<DataCollection>();
            try
            {
                DataCollectionDbContext dataCollectionDb = new DataCollectionDbContext();
                dataCollections = dataCollectionDb.dataCollections.Where(p => p.IsSendToSHB == false).ToList();                
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return dataCollections;
        }
    }
}
