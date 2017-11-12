using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using Common.Resources;
using Common.Utilities;
using LinqKit;
using DataAccess;
using Entity;
using log4net;
using Common.Securities;
using BusinessLogic.Config;

namespace BusinessLogic
{
    public class UserFacade
    {
        private readonly ERPSettingDataContext _context;
        private LogMessageBuilder _logMsg = new LogMessageBuilder();
        private static readonly ILog Logger = LogManager.GetLogger(typeof(UserFacade));
        public UserFacade()
        {
            _context = new ERPSettingDataContext();
        }

        public IEnumerable<UserEntity> searchUserList(UserSearchFilter SearchFilter)
        {
            var whr = PredicateBuilder.True<MS_USER>();
            var query = from u in _context.MS_USER.AsExpandable().Where(whr)
                        from p in _context.MS_PREFIX_NAME.Where(i => i.prefix_name_id == u.prefix_name_id).DefaultIfEmpty()
                        select new UserEntity
                        {
                            UserId = u.user_id,
                            CreatedDate = u.creaed_date,
                            CreatedBy = u.created_by,
                            UpdatedDate = u.updated_date,
                            UpdatedBy = u.updated_by,
                            Username = u.username,
                            PrefixName = new PrefixNameEntity { PrefixNameId = u.prefix_name_id, PrefixName = p.prefix_name, ActiveStatus = p.active_status },
                            FirstName = u.first_name,
                            LastName = u.last_name,
                            Gender = u.gender,
                            OrganizeName = u.organize_name,
                            DepartmentName = u.department_name,
                            PositionName = u.position_name,
                            LastLoginTime = u.last_login_time,
                            ActiveStatus = u.active_status
                        };

            int startPageIndex = (SearchFilter.PageNo - 1) * SearchFilter.PageSize;
            SearchFilter.TotalRecords = query.Count();
            if (startPageIndex >= SearchFilter.TotalRecords)
            {
                startPageIndex = 0;
                SearchFilter.PageNo = 1;
            }

            query = SetUserListSort(query, SearchFilter);
            return query.Skip(startPageIndex).Take(SearchFilter.PageSize).ToList();

        }

        private static IQueryable<UserEntity> SetUserListSort(IEnumerable<UserEntity> userList, UserSearchFilter SearchFilter)
        {
            if (SearchFilter.SortOrder.ToUpper(CultureInfo.InvariantCulture).Equals("ASC"))
            {
                switch (SearchFilter.SortOrder.ToUpper(CultureInfo.InvariantCulture))
                {
                    case "UserName":
                        return userList.OrderBy(a => a.Username).AsQueryable();
                    case "FullName":
                        return userList.OrderBy(a => a.FullName).AsQueryable();
                    case "Gender":
                        return userList.OrderBy(a => a.Gender).AsQueryable();
                    case "OrganizeName":
                        return userList.OrderBy(a => a.OrganizeName).AsQueryable();
                    case "DepartmentName":
                        return userList.OrderBy(a => a.DepartmentName).AsQueryable();
                    case "PositionName":
                        return userList.OrderBy(a => a.PositionName).AsQueryable();
                    case "LastLoginTime":
                        return userList.OrderBy(a => a.LastLoginTime).AsQueryable();
                    case "ActiveStatus":
                        return userList.OrderBy(a => a.ActiveStatus).AsQueryable();
                    default:
                        return userList.OrderBy(a => a.FullName).AsQueryable();
                }
            }
            else
            {
                switch (SearchFilter.SortOrder.ToUpper(CultureInfo.InvariantCulture))
                {
                    case "UserName":
                        return userList.OrderByDescending(a => a.Username).AsQueryable();
                    case "FullName":
                        return userList.OrderByDescending(a => a.FullName).AsQueryable();
                    case "Gender":
                        return userList.OrderByDescending(a => a.Gender).AsQueryable();
                    case "OrganizeName":
                        return userList.OrderByDescending(a => a.OrganizeName).AsQueryable();
                    case "DepartmentName":
                        return userList.OrderByDescending(a => a.DepartmentName).AsQueryable();
                    case "PositionName":
                        return userList.OrderByDescending(a => a.PositionName).AsQueryable();
                    case "LastLoginTime":
                        return userList.OrderByDescending(a => a.LastLoginTime).AsQueryable();
                    case "ActiveStatus":
                        return userList.OrderByDescending(a => a.ActiveStatus).AsQueryable();
                    default:
                        return userList.OrderByDescending(a => a.FullName).AsQueryable();
                }
            }
        }


        #region "User Login Functions"

        public UserEntity Login(string username, string passwd)
        {
            Logger.Info(_logMsg.Clear().SetPrefixMsg("Login").Add("UserName", username).ToInputLogString());
            UserEntity user = GetUserByUsername(username);
            if (user != null)
            {
                //ตรวจสอบจำนวนครั้งที่ใส่รหัสผ่านผิด
                int maxLoginFail = PolicyConfig.GetMaxLoginFail();
                if (user.LoginFailCount >= maxLoginFail) {
                    Logger.Info(_logMsg.Clear().SetPrefixMsg("Login").Add("Alert Max Login Fail ", username).ToInputLogString());

                    throw new CustomException(string.Format(Resources.Msg_LoginMaxFail, maxLoginFail.ToString()));
                }

                if (user.Psswd == passwd)
                {
                    return user;
                }
                else
                {
                    UpdateLoginFailCount(user.UserId, user.LoginFailCount + 1);
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

        


        public UserEntity GetUserByUsername(string login)
        {
            IQueryable<UserEntity> query = from u in _context.MS_USER.AsNoTracking()
                                           from p in _context.MS_PREFIX_NAME.Where(x => x.prefix_name_id == u.prefix_name_id).DefaultIfEmpty()
                                           where u.username.ToUpper() == login.ToUpper() && u.active_status == Constants.ApplicationStatus.Active
                                           select new UserEntity {
                                               UserId = u.user_id,
                                               Username = u.username,
                                               Psswd = u.psswd,
                                               PrefixName = new PrefixNameEntity { PrefixNameId = u.prefix_name_id, PrefixName = p.prefix_name, ActiveStatus = p.active_status },
                                               FirstName=u.first_name,
                                               LastName=u.last_name,
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

        public bool UpdateLoginFailCount(long userId, int loginFailCount) {
            bool ret = false;
            _context.Configuration.AutoDetectChangesEnabled = false;
            try {
                var user = _context.MS_USER.SingleOrDefault(u => u.user_id == userId);
                user.login_fail_count = loginFailCount;

                if (_context.Configuration.AutoDetectChangesEnabled == false)
                {
                    // Set state to Modified
                    _context.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    ret = (_context.SaveChanges() > 0);
                }


            }
            catch (Exception ex) {
                Logger.Error("Exception occur:\n", ex);
            }
            finally {
                _context.Configuration.AutoDetectChangesEnabled = false;
            }

            return ret;
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

        
        public bool UpdateLogoutTime(long loginHisID, DateTime logoutTime, bool isSaveChange) {
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
                    if (isSaveChange == true)
                        _context.SaveChanges();
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
                newLogin.first_name = user.FirstName;
                newLogin.last_name = user.LastName;
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

                    user.psswd = StringCipher.EncryptTxt(newPsswd);
                    user.force_change_psswd = "N";
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

                        ret = UpdateLogoutTime(loginHisId, changeDate, false);
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

        
        #endregion
    }
}
