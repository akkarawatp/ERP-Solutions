using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Resources;
using Common.Utilities;
using DataAccess;
using Entity;
using log4net;
using Common.Securities;
using BusinessLogic.Config;

namespace BusinessLogic
{
    public class MasterRoleFacase
    {
        private readonly ERPSettingDataContext _context;
        private LogMessageBuilder _logMsg = new LogMessageBuilder();
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MasterRoleFacase));

        public MasterRoleFacase()
        {
            _context = new ERPSettingDataContext();
        }

        public bool SaveRole(RoleEntity roleEntity)
        {
            bool ret = false;
            _context.Configuration.AutoDetectChangesEnabled = false;
            try
            {
                MS_ROLE role;
                bool isEdit = roleEntity.RoleId.HasValue;
                if (isEdit == false)
                    role = new MS_ROLE();
                else
                {
                    role = _context.MS_ROLE.SingleOrDefault(r => r.role_id == roleEntity.RoleId.Value);
                    if (role == null) {
                        Logger.ErrorFormat("ROLE ID: {0} does not exist", roleEntity.RoleId.Value);
                        return false;
                    }
                }

                role.role_name = roleEntity.RoleName;
                role.active_status = roleEntity.ActiveStatus;

                if (isEdit == false)
                {
                    role.creaed_date = DateTime.Now;
                    role.created_by = roleEntity.Username;
                    _context.MS_ROLE.Add(role);

                }
                else
                {
                    role.updated_by = roleEntity.Username;
                    role.updated_date = DateTime.Now;

                    if (_context.Configuration.AutoDetectChangesEnabled == false)
                    {
                        // Set state to Modified
                        _context.Entry(role).State = System.Data.Entity.EntityState.Modified;
                    }
                }

                ret = (_context.SaveChanges() > 0);
                
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
    }
}
