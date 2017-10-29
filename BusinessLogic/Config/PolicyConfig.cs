using System;
using Common.Utilities;
using log4net;
using DataAccess;
using Entity;
using System.Linq;

namespace BusinessLogic.Config
{
    public class PolicyConfig
    {
        private static LogMessageBuilder _logMsg = new LogMessageBuilder();
        private static readonly ILog Logger = LogManager.GetLogger(typeof(CommonFacade));

        #region "SYSTEM Configuration"
        private const string MaxLoginFail = "max_login_fail";


        #endregion


        #region "Get DB Config"
        public static int GetMaxLoginFail() {
            return GetSystemConfig().MaxLoginFail;
        }

        private static PolicyConfigEntity GetSystemConfig()
        {
            ERPSettingDataContext _context = new ERPSettingDataContext();
            Logger.Info(_logMsg.Clear().SetPrefixMsg("GetPolicyConfig").ToInputLogString());
            try
            {
                IQueryable<PolicyConfigEntity> query = from c in _context.CF_POLICY_CONFIG.AsNoTracking()
                                                       where c.policy_config_id == 1
                                                       select new PolicyConfigEntity
                                                       {
                                                           MaxLoginFail = c.max_login_fail
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
