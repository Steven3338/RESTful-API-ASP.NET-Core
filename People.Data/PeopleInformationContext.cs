using Microsoft.EntityFrameworkCore;
using PeopleInformation.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleInformation.Data
{
    public class PeopleInformationContext : DbContext
    {
        public PeopleInformationContext(DbContextOptions<PeopleInformationContext> options): base(options)
        {

        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entityType.Name).Property<DateTime>("LastModified");
                modelBuilder.Entity(entityType.Name).Ignore("IsDirty");
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(
            //  "Server = (localdb)\\mssqllocaldb; Database = CustomerDataCoreWeb; Trusted_Connection = True; ",
            //  options => options.MaxBatchSize(30));
            //optionsBuilder.EnableSensitiveDataLogging();
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries()
             .Where(e => e.State == EntityState.Added ||
                         e.State == EntityState.Modified))
            {
                entry.Property("LastModified").CurrentValue = DateTime.Now;
            }
            return base.SaveChanges();
        }
    }
}
