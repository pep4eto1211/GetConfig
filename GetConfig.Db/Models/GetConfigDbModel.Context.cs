﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GetConfig.Db.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GetConfigDbEntities : DbContext
    {
        public GetConfigDbEntities()
            : base("name=GetConfigDbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ConfigValue> ConfigValues { get; set; }
        public virtual DbSet<ProjectsUser> ProjectsUsers { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
    }
}
