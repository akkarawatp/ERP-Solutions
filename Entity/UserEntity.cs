using System;
using Common.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class UserEntity
    {
        public long UserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? Updateddate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string Username { get; set; }
        public string Psswd { get; set; }
        public string PrefixName { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Gender { get; set; }
        public string OrganizeName { get; set; }
        public string DepartmentName { get; set; }
        public string PositionName { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public string ForceChangePsswd { get; set; }
        public int LoginFailCount { get; set; }
        public string ActiveStatus { get; set; }
        public string FullName
        {
            get {
                string ret = "";
                if (string.IsNullOrEmpty(this.PrefixName.NullSafeTrim()) == false) {
                    ret = this.PrefixName.NullSafeTrim();
                }

                ret += this.Firstname.NullSafeTrim() + " " + this.Lastname.NullSafeTrim();
                return ret;
            }

        }
    }
}
