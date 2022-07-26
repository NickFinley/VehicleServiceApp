﻿using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace VehicleServiceApp.Models
{
    public class VehicleContext : DbContext
    {
        public VehicleContext(DbContextOptions<VehicleContext> options)
            : base(options)
        {

        }

        public DbSet<Vehicle> Vehicles { get; set; } = null!;
    }
}
