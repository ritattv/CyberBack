﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CyberDataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class cyberdbEntities : DbContext
    {
        public cyberdbEntities()
            : base("name=cyberdbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Coach> Coach { get; set; }
        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<Health> Health { get; set; }
        public virtual DbSet<Organizer> Organizer { get; set; }
        public virtual DbSet<Player> Player { get; set; }
        public virtual DbSet<Team> Team { get; set; }
        public virtual DbSet<Team_Tournament> Team_Tournament { get; set; }
        public virtual DbSet<Tournament> Tournament { get; set; }
        public virtual DbSet<Stats> Stats { get; set; }
    }
}