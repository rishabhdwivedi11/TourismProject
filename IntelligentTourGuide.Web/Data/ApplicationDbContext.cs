using IntelligentTourGuide.Web.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentTourGuide.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        internal IEnumerable states;

        public ApplicationDbContext(DbContextOptions options)
          : base(options)
        {
        }

        public DbSet<State> States { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<PlaceDetail> PlaceDetails { get; set; }
    }
}
