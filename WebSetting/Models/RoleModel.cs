using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace WebSetting.Models
{
    public class RoleModel
    {
        public long? roleID { get; set; }
        public string roleName { get; set; }
        public string activeStatus { get; set; }
        public SelectList ActiveStatusList { get; set; }
    }
}