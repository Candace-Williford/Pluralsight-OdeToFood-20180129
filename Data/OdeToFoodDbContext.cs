using Microsoft.EntityFrameworkCore;
using OdeToFood.Models;

namespace OdeToFood.Data
{
    public class OdeToFoodDbContext : DbContext
    {
        public OdeToFoodDbContext(DbContextOptions options)
            : base(options)
        {
            //only have to take the options and pass them onto the DbContext
            //might usually want to save or inspect these options
        }

        public DbSet<Restaurant> Restaurants { get; set; }
    }
}