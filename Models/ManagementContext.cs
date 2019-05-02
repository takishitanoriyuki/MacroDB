using MacroDB.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace MacroDB.Models{
    public class ManagementContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "localhost";   // update me
                builder.UserID = "SA";              // update me
                builder.Password = "taki_0416";      // update me
                builder.InitialCatalog = "MacroDB";
            optionsBuilder.UseSqlServer(builder.ConnectionString);
        }

        public DbSet<ManagementModel> management { get; set; }
    }
}
