﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ERPSettingDataContext : DbContext
    {
        public ERPSettingDataContext()
            : base("name=ERPSettingDataContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<MS_USER> MS_USER { get; set; }
        public virtual DbSet<TB_LOGIN_HISTORY> TB_LOGIN_HISTORY { get; set; }
        public virtual DbSet<TB_CHANGE_PSSWD_HISTORY> TB_CHANGE_PSSWD_HISTORY { get; set; }
    }
}