using System;
using System.Collections.Generic;
using Entity;

namespace WebSetting.Models
{
    [Serializable]
    public class SearchRolesViewModel
    {
        public IEnumerable<RoleEntity> RoleList { get; set; }
        public RolesSearchFilter SearchFilter { get; set; }
    }

    
}