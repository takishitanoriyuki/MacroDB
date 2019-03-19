using MacroDB.Models;
using Microsoft.EntityFrameworkCore;

namespace MacroDB.Models{
    public class NutrientContext : DbContext
    {
        public NutrientContext(DbContextOptions<NutrientContext> options)
            : base(options)
        {
        }

        public DbSet<NutrientModel> nutrients { get; set; }
    }
}
