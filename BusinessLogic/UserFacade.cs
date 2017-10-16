using System;
using System.Data.Entity;
using System.Linq;
using Common.Resources;
using Common.Utilities;
using DataAccess;
using Entity;
using log4net;

namespace BusinessLogic
{
    public class UserFacade
    {
        //private CommonFacade _commonFacade;
        private readonly ERPSettingDataContext _context;
        //private UserDataAccess _userDataAccess;
        private LogMessageBuilder _logMsg = new LogMessageBuilder();
        private static readonly ILog Logger = LogManager.GetLogger(typeof(CommonFacade));
        public UserFacade()
        {
            _context = new ERPSettingDataContext();
        }

        public UserEntity Login(string username, string passwd)
        {
            Logger.Info(_logMsg.Clear().SetPrefixMsg("Login").Add("UserName", username).ToInputLogString());
            UserEntity user = GetUserByUsername(username);
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
        }

        public void CheckExceededMaxConcurrent(string username, System.Web.HttpSessionStateBase session)
        {
            if (!string.IsNullOrWhiteSpace(username))
            {
                if (session["sessionid"] == null)
                {
                    session["sessionid"] = "empty";
                }

                // check to see if your ID in the Logins table has LoggedIn = true - if so, continue, otherwise, redirect to Login page.
                if (IsYourLoginStillTrue(username, (session["sessionid"] as string)))
                {
                    // check to see if your user ID is being used elsewhere under a different session ID
                    if (!IsUserLoggedOnElsewhere(username, (session["sessionid"] as string)))
                    {
                        // Do nothing
                    }
                    else
                    {
                        // if it is being used elsewhere, update all their Logins records to LoggedIn = false, except for your session ID
                        LogEveryoneElseOut(username, (session["sessionid"] as string));
                    }
                }
                else
                {
                    // Go to Login page
                    session["sessionid"] = null;
                }
            }
            else
            {
                // Go to Logout page
                session["sessionid"] = null;
                session.Clear();
                session.Abandon();
                session.RemoveAll();
            }
        }

        #region "Functions"


        public UserEntity GetUserByUsername(string login)
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


        /// <summary>
        /// Check to see if your ID in the Logins table has LoggedIn = true
        /// If so, continue, otherwise, redirect to Login page.
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="sid"></param>
        /// <returns></returns>
        public bool IsYourLoginStillTrue(string loginName, string sid)
        {
            var query = from l in _context.TB_LOGIN_HISTORY
                        where l.logout_time == null && l.username == loginName && l.session_id == sid
                        select l;

            return query.Any();
        }

        /// <summary>
        /// Check to see if your login name is being used elsewhere under a different session ID
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="sid"></param>
        /// <returns></returns>
        public bool IsUserLoggedOnElsewhere(string loginName, string sid)
        {
            var query = from x in _context.TB_LOGIN_HISTORY
                        where x.logout_time == null && x.username == loginName && x.session_id != sid
                        select x;

            return query.Any();
        }

        /// <summary>
        /// If it is being used elsewhere, update all their Logins records to LoggedIn = false, except for your session ID
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="sid"></param>
        public void LogEveryoneElseOut(string loginName, string sid)
        {
            var query = from x in _context.TB_LOGIN_HISTORY
                        where x.logout_time == null && x.username == loginName && x.session_id != sid // Need to filter by login name
                        select x;

            DateTime LogoutTime = DateTime.Now;
            foreach (TB_LOGIN_HISTORY entity in query)
            {
                entity.logout_time = LogoutTime;
            }

            _context.SaveChanges();
        }

        public bool UpdateLastLoginTime(long userId, DateTime loginTime) {
            bool ret = false;
            _context.Configuration.AutoDetectChangesEnabled = false;
            try
            {
                var user = _context.MS_USER.SingleOrDefault(u => u.user_id == userId);
                user.last_login_time = loginTime;
                user.login_fail_count = 0;

                if (_context.Configuration.AutoDetectChangesEnabled == false)
                {
                    // Set state to Modified
                    _context.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    ret = (_context.SaveChanges() > 0);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Exception occur:\n", ex);
            }
            finally {
                _context.Configuration.AutoDetectChangesEnabled = false;
            }

            return ret;
        }

        
        private bool UpdateLogoutTime(long loginHisID, DateTime logoutTime) {
            bool ret = false;
            _context.Configuration.AutoDetectChangesEnabled = false;
            try
            {
                var log = _context.TB_LOGIN_HISTORY.SingleOrDefault(l => l.login_history_id == loginHisID);
                log.logout_time = logoutTime;
                log.updated_by = log.created_by;
                log.updated_date = logoutTime;
                
                if (_context.Configuration.AutoDetectChangesEnabled == false)
                {
                    // Set state to Modified
                    _context.Entry(log).State = System.Data.Entity.EntityState.Modified;
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Exception occur:\n", ex);
            }
            finally
            {
                _context.Configuration.AutoDetectChangesEnabled = false;
            }

            return ret;
        }

        public long SaveLoginHistory(UserEntity user, string sid, string SystemCode , DateTime loginTime, string ClientIP, string ClientBrowser, string ServerUrl)
        {
            long ret = 0;
            _context.Configuration.AutoDetectChangesEnabled = false;

            try
            {
                TB_LOGIN_HISTORY newLogin = new TB_LOGIN_HISTORY();
                newLogin.creaed_date = loginTime;
                newLogin.created_by = user.Username;
                newLogin.token = Guid.NewGuid().ToString();
                newLogin.session_id = sid;
                newLogin.username = user.Username;
                newLogin.first_name = user.Firstname;
                newLogin.last_name = user.Lastname;
                newLogin.logon_time = loginTime;
                newLogin.system_code = SystemCode;
                newLogin.client_ip = ClientIP;
                newLogin.client_browser = ClientBrowser;
                newLogin.server_url = ServerUrl;
                _context.TB_LOGIN_HISTORY.Add(newLogin);
                _context.SaveChanges();
                ret = newLogin.login_history_id;
            }
            catch (Exception ex)
            {
                Logger.Error("Exception occur:\n", ex);
            }
            finally
            {
                _context.Configuration.AutoDetectChangesEnabled = false;
            }

            return ret;
        }

        public bool UpdateChangePsswd(string userName, long loginHisId, string changeByUsename, string newPsswd, string systemCode) {
            bool ret = false;

            using (DbContextTransaction trans = _context.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted)) {
                _context.Configuration.AutoDetectChangesEnabled = false;

                try
                {
                    var user = _context.MS_USER.SingleOrDefault(u => u.username == userName);
                    string oldPasswd = user.psswd;

                    user.psswd = EncryptTxt(newPsswd);
                    user.login_fail_count = 0;

                    DateTime changeDate = DateTime.Now;

                    if (_context.Configuration.AutoDetectChangesEnabled == false)
                    {
                        // Set state to Modified
                        _context.Entry(user).State = EntityState.Modified;

                        //Insert Password Change History
                        TB_CHANGE_PSSWD_HISTORY cLnq = new TB_CHANGE_PSSWD_HISTORY();
                        cLnq.creaed_date = changeDate;
                        cLnq.created_by = changeByUsename;
                        cLnq.change_time = changeDate;
                        cLnq.username = userName;
                        cLnq.system_code = systemCode;
                        cLnq.old_psswd = oldPasswd;   //Encrypt แล้ว
                        cLnq.new_psswd = user.psswd;  //Encrypt แล้ว
                        _context.TB_CHANGE_PSSWD_HISTORY.Add(cLnq);

                        ret = UpdateLogoutTime(loginHisId, changeDate);
                        if (ret == true)
                        {
                            ret = (_context.SaveChanges() > 0);
                        }
                    }

                    if (ret == true)
                        trans.Commit();
                    else
                        trans.Rollback();

                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    Logger.Error("Exception occur:\n", ex);
                }
                finally {
                    _context.Configuration.AutoDetectChangesEnabled = false;
                }
            }
            return ret;


        }


        public string EncryptTxt(string txtIn) {
            return txtIn;
        }
        #endregion
    }
}
