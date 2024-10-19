using System.Data.Entity;
using TravelEditor.Models;

namespace TravelEditor.Database
{
    public class DatabaseContext: DbContext
    {
        public DbSet<Trip> trips { get; set; }
        public DbSet<Destination> destinations { get; set; }
        public DbSet<Review> reviews { get; set; }
        public DbSet<Traveller> travellers { get; set; }
        public DbSet<Attraction> attractions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Destination>()
                .HasMany(d => d.Attractions)
                .WithRequired() 
                .WillCascadeOnDelete(true); 
        }

    }

}
