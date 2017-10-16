using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Common.Utilities;
using BusinessLogic;
using Entity;

namespace WebSetting.Models
{
    [Serializable]
    public class LoginViewModel
    {
        [Display(Name = "User name")]
        [LocalizedStringLengthAttribute(Constants.MaxLength.Username, ErrorMessageResourceName = "ValErr_StringLength",
            ErrorMessageResourceType = typeof(Common.Resources.Resources))]
        [LocalizedRequired(ErrorMessage = "ValErr_RequiredField")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [LocalizedStringLengthAttribute(Constants.MaxLength.Password, ErrorMessageResourceName = "ValErr_StringLength",
            ErrorMessageResourceType = typeof(Common.Resources.Resources))]
        [LocalizedRequired(ErrorMessage = "ValErr_RequiredField")]
        public string Password { get; set; }

        //[Display(Name = "Remember on this computer")]
        //public bool RememberMe { get; set; }

        public string ErrorMessage { get; set; }

        public UserEntity IsValid(string username, string password, DateTime loginTime)
        {
            UserFacade userFacade = null;
            try
            {
                userFacade = new UserFacade();
                UserEntity user = userFacade.Login(username, password);

                if (user == null)
                {
                    return null;
                }
                else
                {
                    return user;
                }
            }
            finally
            {
                if (userFacade != null) { userFacade = null; }
            }
        }
    }
}