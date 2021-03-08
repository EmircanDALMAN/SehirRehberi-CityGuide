using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityGuideWebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityGuideWebAPI.DataAccess
{
    public class CityGuideContext : DbContext
    {
        public CityGuideContext(DbContextOptions<CityGuideContext> options):base(options)
        {
            
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
