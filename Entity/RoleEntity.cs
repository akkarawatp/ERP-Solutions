using System;
using Entity.Common;
using Common.Utilities;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class RoleEntity
    {
        public long? RoleId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? Updateddate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string RoleName { get; set; }
        public string ActiveStatus { get; set; }
        public string ActiveStatusDisplay
        {
            get {
                return Constants.ApplicationStatus.GetMessage(ActiveStatus);
            }
        }
        public string Username { get; set; }
        public string LastUpdateUserDisplay
        {
            get {
                string ret = "";
                if (!string.IsNullOrEmpty(UpdatedBy))
                    ret = UpdatedBy;
                else
                    ret = CreatedBy;

                return ret;
            }
        }
        public string LastUpdateDateDisplay
        {
            get
            {
                string ret = "";
                if (Updateddate.HasValue == true)
                    ret = DateUtil.ToStringAsDateTime(Updateddate);
                else
                    ret = DateUtil.ToStringAsDateTime(CreatedDate);

                return ret;
            }
        }

    }

    public class RolesSearchFilter : Pager
    {
        public string RoleName { get; set; }
    }
}
