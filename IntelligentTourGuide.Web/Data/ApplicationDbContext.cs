using IntelligentTourGuide.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentTourGuide.Web.Data
{
    public class ApplicationDbContext: IdentityDbContext
    {

        public DbSet<State> States { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<PlaceDetail> PlaceDetails { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
