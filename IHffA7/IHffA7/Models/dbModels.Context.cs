﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IHffA7.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ihffTestConnectionGenerated : DbContext
    {
        public ihffTestConnectionGenerated()
            : base("name=ihffTestConnectionGenerated")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Activities> Activities { get; set; }
        public virtual DbSet<Films> Films { get; set; }
        public virtual DbSet<Filmscreenings> Filmscreenings { get; set; }
        public virtual DbSet<Locations> Locations { get; set; }
        public virtual DbSet<Reservations> Reservations { get; set; }
        public virtual DbSet<Restaurants> Restaurants { get; set; }
        public virtual DbSet<Rooms> Rooms { get; set; }
        public virtual DbSet<Specials> Specials { get; set; }
        public virtual DbSet<Specialscreenings> Specialscreenings { get; set; }
        public virtual DbSet<WishlistItems> WishlistItems { get; set; }
        public virtual DbSet<Wishlists> Wishlists { get; set; }
    }
}
