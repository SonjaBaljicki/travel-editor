using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    }
}
