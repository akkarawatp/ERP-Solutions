using System;
using Common.Utilities;
using Common.Securities;
using log4net;
using DataAccess;
using Entity;
using System.Linq;

namespace BusinessLogic.Config
{
    public class SystemConfig
    {
        private static LogMessageBuilder _logMsg = new LogMessageBuilder();
        private static readonly ILog Logger = LogManager.GetLogger(typeof(CommonFacade));

        #region "SYSTEM Configuration"
        private const string MaxLoginFail = "max_login_fail";


        #endregion


        #region "Get DB Config"
        //public static int GetMaxLoginFail() {
        //    return GetSystemConfig().MaxLoginFail;
        //}

        public static DatabaseConfigInfoEntity getDbSettingInfo() {
            return GetSystemConfig().DbSettingInfo;
        }

        public static DatabaseConfigInfoEntity getDbPurchaseInfo()
        {
            return GetSystemConfig().DbPurchaseInfo;
        }

        public static DatabaseConfigInfoEntity getDbSalesInfo()
        {
            return GetSystemConfig().DbSalesInfo;
        }

        public static DatabaseConfigInfoEntity getDbWmsInfo()
        {
            return GetSystemConfig().DbWmsInfo;
        }

        public static DatabaseConfigInfoEntity getDbInventoryInfo()
        {
            return GetSystemConfig().DbInventoryInfo;
        }

        public static DatabaseConfigInfoEntity getDbTransportationInfo()
        {
            return GetSystemConfig().DbTransportationInfo;
        }

        public static DatabaseConfigInfoEntity getDbProductionInfo()
        {
            return GetSystemConfig().DbProductionInfo;
        }

        public static DatabaseConfigInfoEntity getDbPlanningInfo()
        {
            return GetSystemConfig().DbPlanningInfo;
        }

        private static SystemConfigEntity GetSystemConfig()
        {
            ERPSettingDataContext _context = new ERPSettingDataContext();
            Logger.Info(_logMsg.Clear().SetPrefixMsg("GetSystemConfig").ToInputLogString());
            try
            {
                IQueryable<SystemConfigEntity> query = from c in _context.CF_SYSTEM_CONFIG.AsNoTracking()
                                                       where c.system_config_id == 1
                                                       select new SystemConfigEntity
                                                       {
                                                           DbSetting = c.db_setting,
                                                           DbPurchase = c.db_purchase,
                                                           DbSales = c.db_sales,
                                                           DbWms = c.db_wms,
                                                           DbInventory = c.db_inventory,
                                                           DbTransportation = c.db_transportation,
                                                           DbProduction = c.db_prodction,
                                                           DbPlanning = c.db_planning
                                                       };
                return query.Any() ? query.FirstOrDefault() : null;
            }
            catch (Exception e)
            {
                Logger.Error("Exception :" + e.Message);
                return null;
            }
        }
        #endregion
    }
}
