//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class MS_ROLE_MENU
    {
        public long role_menu_id { get; set; }
        public string created_by { get; set; }
        public System.DateTime creaed_date { get; set; }
        public string updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
        public long role_id { get; set; }
        public long menu_id { get; set; }
        public string is_view { get; set; }
        public string is_edit { get; set; }
        public string is_delete { get; set; }
        public string is_na { get; set; }
    
        public virtual MS_MENU MS_MENU { get; set; }
        public virtual MS_ROLE MS_ROLE { get; set; }
    }
}
