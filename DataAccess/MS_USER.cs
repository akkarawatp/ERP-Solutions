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
    
    public partial class MS_USER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MS_USER()
        {
            this.MS_ROLE_USER = new HashSet<MS_ROLE_USER>();
        }
    
        public long user_id { get; set; }
        public string created_by { get; set; }
        public System.DateTime creaed_date { get; set; }
        public string updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
        public string username { get; set; }
        public string psswd { get; set; }
        public Nullable<long> prefix_name_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string gender { get; set; }
        public string organize_name { get; set; }
        public string department_name { get; set; }
        public string position_name { get; set; }
        public Nullable<System.DateTime> last_login_time { get; set; }
        public string force_change_psswd { get; set; }
        public int login_fail_count { get; set; }
        public string active_status { get; set; }
    
        public virtual MS_PREFIX_NAME MS_PREFIX_NAME { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MS_ROLE_USER> MS_ROLE_USER { get; set; }
    }
}
