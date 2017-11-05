using System;
using System.Collections.Generic;
using System.Linq;
using Entity;
using Entity.Common;

namespace WebSetting.Models
{
    [Serializable]
    public class SearchRolesViewModel
    {
        public IEnumerable<RoleEntity> RoleList { get; set; }
        public RolesSearchFilter SearchFilter { get; set; }
    }

    
}