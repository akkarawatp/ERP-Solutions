using System;
using Common.Utilities;
using Common.Resources;
using Entity.Common;

namespace Entity
{
    public class UserEntity
    {
        public long UserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string Username { get; set; }
        public string Psswd { get; set; }
        public PrefixNameEntity PrefixName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string GenderDisplay
        {
            get {
                return Constants.PersonalGender.GetMessage(Gender);
            }
        }
        public string OrganizeName { get; set; }
        public string DepartmentName { get; set; }
        public string PositionName { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public string LastLoginTimeDisplay {
            get
            {
                return LastLoginTime.FormatDateTime(Constants.DateTimeFormat.DefaultFullDateTime);
            }
        }
        public string ForceChangePsswd { get; set; }
        public int LoginFailCount { get; set; }
        public string ActiveStatus { get; set; }
        public string ActiveStatusDisplay
        {
            get
            {
                return Constants.ApplicationStatus.GetMessage(ActiveStatus);
            }
        }
        public string FullName
        {
            get {
                string ret = "";
                if (this.PrefixName != null) {
                    ret = PrefixName.PrefixName;
                }

                ret += this.FirstName.NullSafeTrim() + " " + this.LastName.NullSafeTrim();
                return ret;
            }

        }
        public string LastUpdateUserDisplay
        {
            get
            {
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
                if (UpdatedDate.HasValue == true)
                    ret = DateUtil.ToStringAsDateTime(UpdatedDate);
                else
                    ret = DateUtil.ToStringAsDateTime(CreatedDate);

                return ret;
            }
        }

        public long RoleIdValue { get; set; }
    }

    public class UserSearchFilter : Pager
    {
        public string SearchUsername { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OrganizeName { get; set; }
        public string DepartmentName { get; set; }
        public string PositionName { get; set; }
        public string ActiveStatus { get; set; }
    }
}
