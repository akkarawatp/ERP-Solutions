using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSetting.Models
{
    [Serializable]
    public class SearchRolesViewModel
    {
        public RolesSearchFilter SearchFilter { get; set; }
    }

    public class RolesSearchFilter {
        public string RoleName{ get; set; }
    }
}