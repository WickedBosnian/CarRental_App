﻿using CarRental_Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_Infrastructure.Helpers
{
    public partial class CarRentalDBContext : DbContext
    {
        public CarRentalDBContext(DbContextOptions<CarRentalDBContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Client> Clients { get; set; }
        public DbSet<Reservation> Reservations { get; set; } = null!;
        public DbSet<ReservedVehicle> ReservedVehicles { get; set; } = null!;
        public DbSet<Vehicle> Vehicles { get; set; } = null!;
        public DbSet<VehicleManufacturer> VehicleManufacturers { get; set; } = null!;
        public DbSet<VehicleType> VehicleTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server = (local); Database = CarRental; Trusted_Connection = true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Gender).IsFixedLength();
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

            });

            modelBuilder.Entity<ReservedVehicle>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<VehicleManufacturer>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<VehicleType>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
