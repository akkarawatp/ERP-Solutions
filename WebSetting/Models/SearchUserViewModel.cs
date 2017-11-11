using System;
using System.Collections.Generic;
using Entity;
using System.Web.Mvc;

namespace WebSetting.Models
{
    [Serializable]
    public class SearchUserViewModel
    {
        public SelectList ActiveStatusList { get; set; }
        public IEnumerable<UserEntity> UserList { get; set; }
        public UserSearchFilter SearchFilter { get; set; }
    }
}