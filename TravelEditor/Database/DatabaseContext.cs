using System.Collections.Generic;
using System.Data.Entity;
using TravelEditor.Models;

namespace TravelEditor.Database
{
    public class DatabaseContext: DbContext
    {
        public DbSet<Trip> Trips { get; set; }
        public virtual DbSet<Destination> Destinations { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Traveller> Travellers { get; set; }
        public DbSet<Attraction> Attractions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Destination>()
                .HasMany(d => d.Attractions)
                .WithRequired() 
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Trip>()
               .HasMany(t => t.Reviews)
               .WithRequired()
               .WillCascadeOnDelete(true);

            modelBuilder.Entity<Trip>()
                .HasMany(t => t.Travellers)
                .WithMany();
        }

    }

}
