using System;
using System.ComponentModel.DataAnnotations;
using Common.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSetting.Models
{
    [Serializable]
    public class UserChangePsswdModel
    {
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Old Password")]
        [LocalizedStringLengthAttribute(Constants.MaxLength.Password, ErrorMessageResourceName = "ValErr_StringLength",
            ErrorMessageResourceType = typeof(Common.Resources.Resources))]
        [LocalizedRequired(ErrorMessage = "ValErr_RequiredField")]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        [LocalizedStringLengthAttribute(Constants.MaxLength.Password, ErrorMessageResourceName = "ValErr_StringLength",
            ErrorMessageResourceType = typeof(Common.Resources.Resources))]
        [LocalizedRequired(ErrorMessage = "ValErr_RequiredField")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [LocalizedStringLengthAttribute(Constants.MaxLength.Password, ErrorMessageResourceName = "ValErr_StringLength",
            ErrorMessageResourceType = typeof(Common.Resources.Resources))]
        [LocalizedRequired(ErrorMessage = "ValErr_RequiredField")]
        public string ConfirmNewPassword { get; set; }

        public string ForceChangePsswd { get; set; }

        public string ErrorMessage { get; set; }
    }
}