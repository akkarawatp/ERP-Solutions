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
    
    public partial class TB_LOGIN_HISTORY
    {
        public long login_history_id { get; set; }
        public string created_by { get; set; }
        public Nullable<System.DateTime> creaed_date { get; set; }
        public string updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
        public string token { get; set; }
        public string session_id { get; set; }
        public string username { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public System.DateTime logon_time { get; set; }
        public Nullable<System.DateTime> logout_time { get; set; }
        public string system_code { get; set; }
        public string client_ip { get; set; }
        public string client_browser { get; set; }
        public string server_url { get; set; }
    }
}