﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Disty.Model.MySql
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DistyModelContainer : DbContext
    {
        public DistyModelContainer()
            : base("name=DistyModelContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<List> Lists { get; set; }
        public virtual DbSet<Dept> Depts { get; set; }
        public virtual DbSet<Email> Emails { get; set; }
    }
}
