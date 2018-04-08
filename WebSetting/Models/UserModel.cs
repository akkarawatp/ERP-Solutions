using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Utilities;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebSetting.Models
{
    public class UserModel
    {
        public long UserId { get; set; }
        [Display(Name = "Lbl_Username", ResourceType = typeof(Common.Resources.Resources))]
        [LocalizedRequired(ErrorMessage = "ValErr_RequiredField")]
        public string Username { get; set; }

        [Display(Name = "Lbl_Password", ResourceType = typeof(Common.Resources.Resources))]
        [LocalizedRequired(ErrorMessage = "ValErr_RequiredField")]
        public string Psswd { get; set; }

        [Display(Name = "Lbl_ConfirmPasswd", ResourceType = typeof(Common.Resources.Resources))]
        [LocalizedRequired(ErrorMessage = "ValErr_RequiredField")]
        public string ConfirmPsswd { get; set; }

        [Display(Name = "Lbl_PrefixName", ResourceType = typeof(Common.Resources.Resources))]
        [LocalizedRequired(ErrorMessage = "ValErr_RequiredField")]
        public int PrefixId { get; set; }

        [Display(Name = "Lbl_FirstName", ResourceType = typeof(Common.Resources.Resources))]
        [LocalizedRequired(ErrorMessage = "ValErr_RequiredField")]
        public string FirstName { get; set; }

        [Display(Name = "Lbl_LastName", ResourceType = typeof(Common.Resources.Resources))]
        [LocalizedRequired(ErrorMessage = "ValErr_RequiredField")]
        public string LastName { get; set; }

        [Display(Name = "Lbl_Gender", ResourceType = typeof(Common.Resources.Resources))]
        [LocalizedRequired(ErrorMessage = "ValErr_RequiredField")]
        public string Gender { get; set; }

        public string OrganizeName { get; set; }
        public string DepartmentName { get; set; }
        public string PositionName { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public string LastLoginTimeDisplay
        {
            get {
                return LastLoginTime.FormatDateTime(Constants.DateTimeFormat.DefaultFullDateTime);
            }
        }
        public bool ForceChangePsswd { get; set; }
        public bool ActiveStatus { get; set; }
        public SelectList PrefixNameSelectList { get; set; }
    }
}