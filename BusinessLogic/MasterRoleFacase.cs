using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Globalization;
using LinqKit;
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

        public IEnumerable<RoleEntity> searchRoleList(RolesSearchFilter SearchFilter)
        {
            var whr = PredicateBuilder.True<MS_ROLE>();
            if (SearchFilter.RoleName != null) whr = whr.And(r => r.role_name.Contains(SearchFilter.RoleName.Trim()));

            var query = from r in _context.MS_ROLE.AsExpandable().Where(whr)
                        select new RoleEntity
                        {
                            RoleId = r.role_id,
                            RoleName = r.role_name,
                            ActiveStatus = r.active_status,
                            CreatedBy = r.created_by,
                            CreatedDate = r.creaed_date,
                            UpdatedBy = r.updated_by,
                            Updateddate = r.updated_date
                        };

            int startPageIndex = (SearchFilter.PageNo - 1) * SearchFilter.PageSize;
            SearchFilter.TotalRecords = query.Count();
            if (startPageIndex >= SearchFilter.TotalRecords)
            {
                startPageIndex = 0;
                SearchFilter.PageNo = 1;
            }

            query = SetRoleListSort(query, SearchFilter);
            return query.Skip(startPageIndex).Take(SearchFilter.PageSize).ToList();
        }

        private static IQueryable<RoleEntity> SetRoleListSort(IEnumerable<RoleEntity> roleList, RolesSearchFilter SearchFilter)
        {
            if (SearchFilter.SortOrder.ToUpper(CultureInfo.InvariantCulture).Equals("ASC"))
            {
                switch (SearchFilter.SortOrder.ToUpper(CultureInfo.InvariantCulture))
                {
                    case "RoleName":
                        return roleList.OrderBy(a => a.RoleName).AsQueryable();
                    case "ActiveStatus":
                        return roleList.OrderBy(a => a.ActiveStatus).AsQueryable();
                    default:
                        return roleList.OrderBy(a => a.RoleName).AsQueryable();
                }
            }
            else
            {
                switch (SearchFilter.SortOrder.ToUpper(CultureInfo.InvariantCulture))
                {
                    case "RoleName":
                        return roleList.OrderByDescending(a => a.RoleName).AsQueryable();
                    case "ActiveStatus":
                        return roleList.OrderByDescending(a => a.ActiveStatus).AsQueryable();
                    default:
                        return roleList.OrderByDescending(a => a.RoleName).AsQueryable();
                }
            }
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

        public RoleEntity getMasterRoleEntity(long roleId)
        {
            RoleEntity role = null;
            try {
                var query = from r in _context.MS_ROLE
                       where r.role_id == roleId
                       select new RoleEntity
                       {
                           RoleId = roleId,
                           RoleName = r.role_name,
                           ActiveStatus = r.active_status,
                           CreatedBy = r.created_by,
                           CreatedDate = r.creaed_date,
                           UpdatedBy = r.updated_by,
                           Updateddate = r.updated_date
                       };
                role = query.Any() ? query.FirstOrDefault() : null;
            }
            catch (Exception ex)
            {
                Logger.Error("Exception occur:\n", ex);
            }

            return role;
        }
    }
}
