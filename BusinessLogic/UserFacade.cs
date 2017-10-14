using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Resources;
using Common.Securities;
using Common.Utilities;
using DataAccess;
using Entity;
using log4net;

namespace BusinessLogic
{
    public class UserFacade
    {
        //private CommonFacade _commonFacade;
        private readonly ERPSettingDataAccess _context;
        //private UserDataAccess _userDataAccess;
        private LogMessageBuilder _logMsg = new LogMessageBuilder();
        private static readonly ILog Logger = LogManager.GetLogger(typeof(CommonFacade));
        public UserFacade()
        {
            _context = new ERPSettingDataAccess();
        }

        public UserEntity Login(string username, string passwd)
        {
            Logger.Info(_logMsg.Clear().SetPrefixMsg("Login").Add("UserName", username).ToInputLogString());
            UserEntity user = GetUserByLogin(username);
            if (user != null)
            {
                string encryptPwd = passwd;

                if (user.Psswd == encryptPwd)
                {
                    return user;
                }
                else
                {
                    throw new CustomException(Resources.Msg_InvalidPassword);
                }

            }
            else
            {
                throw new CustomException(Resources.Msg_UserRoleNotFound);
            }

            return user;
        }

        #region "Functions"
        

        public UserEntity GetUserByLogin(string login)
        {
            IQueryable<UserEntity> query = from u in _context.MS_USER.AsNoTracking()
                                           where u.username.ToUpper() == login.ToUpper() && u.active_status == Constants.ApplicationStatus.Active
                                           select new UserEntity {
                                               UserId = u.user_id,
                                               Username = u.username,
                                               Psswd = u.psswd,
                                               PrefixName=u.prefix_name,
                                               Firstname=u.first_name,
                                               Lastname=u.last_name,
                                               Gender=u.gender,
                                               OrganizeName=u.organize_name,
                                               DepartmentName=u.department_name,
                                               PositionName=u.position_name,
                                               LastLoginTime=u.last_login_time,
                                               ForceChangePsswd=u.force_change_psswd,
                                               LoginFailCount=u.login_fail_count,
                                           };

            var user = query.Any() ? query.FirstOrDefault() : null;
            return user;

        }


        public void SaveLogin(string loginName, string sid)
        {
            _context.Configuration.AutoDetectChangesEnabled = false;

            try
            {
                //var query = from x in _context.TB_L_LOGIN
                //            where x.LOGGED_IN == 1 && x.LOGIN_NAME == loginName && x.SESSION_ID == sid
                //            // Need to filter by login name
                //            select x;

                //if (query.Any())
                //{
                //    TB_L_LOGIN entity = query.FirstOrDefault();
                //    entity.LOGGED_IN = 0;
                //    _context.Entry(entity).Property("LOGGED_IN").IsModified = true;
                //}

                //TB_L_LOGIN newLogin = new TB_L_LOGIN();
                //newLogin.LOGGED_IN = 1;
                //newLogin.LOGIN_NAME = loginName;
                //newLogin.SESSION_ID = sid;
                //newLogin.CREATE_DATE = DateTime.Now;
                //_context.TB_L_LOGIN.Add(newLogin);
                //this.Save();
            }
            catch (Exception ex)
            {
                Logger.Error("Exception occur:\n", ex);
            }
            finally
            {
                _context.Configuration.AutoDetectChangesEnabled = false;
            }
        }
        #endregion
    }
}
