using MacroDB.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace MacroDB.Models{
    public class ManagementContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionBuilder.GetConnectionString());
        }

        public DbSet<ManagementModel> management { get; set; }
    }
}
