﻿using NidTid.Domain.Entities;
using System.Data.Entity;

namespace NidTid.Domain.Concrete
{
    public class EFDbContext : DbContext {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<MeterPost> MeterPosts { get; set; }
    }
}
