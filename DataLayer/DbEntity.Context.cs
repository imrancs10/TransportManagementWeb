﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TransportManagementEntities : DbContext
    {
        public TransportManagementEntities()
            : base("name=TransportManagementEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<ClientConcernPerson> ClientConcernPersons { get; set; }
        public virtual DbSet<ClientDetail> ClientDetails { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<ServiceOrderDetail> ServiceOrderDetails { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<TranshipmentDetail> TranshipmentDetails { get; set; }
        public virtual DbSet<UnitDetail> UnitDetails { get; set; }
        public virtual DbSet<UserDetail> UserDetails { get; set; }
        public virtual DbSet<VehicleDetail> VehicleDetails { get; set; }
        public virtual DbSet<VehicleType> VehicleTypes { get; set; }
        public virtual DbSet<VendorBankDetail> VendorBankDetails { get; set; }
        public virtual DbSet<VendorConcernPerson> VendorConcernPersons { get; set; }
        public virtual DbSet<VendorDetail> VendorDetails { get; set; }
        public virtual DbSet<VendorLineHaulDetail> VendorLineHaulDetails { get; set; }
        public virtual DbSet<WeightLookup> WeightLookups { get; set; }
    }
}
