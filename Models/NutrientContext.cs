using MacroDB.Models;
using Microsoft.EntityFrameworkCore;

namespace MacroDB.Models{
    public class NutrientContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionBuilder.GetConnectionString());
        }

        public DbSet<NutrientModel> nutrients { get; set; }
    }
}
