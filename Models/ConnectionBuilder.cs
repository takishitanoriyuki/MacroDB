using System.Data.SqlClient;

namespace MacroDB.Models{
    public static class ConnectionBuilder{
        public static string GetConnectionString(){
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "localhost";   // update me
            builder.UserID = "SA";              // update me
            builder.Password = "taki_0416";      // update me
            builder.InitialCatalog = "MacroDB";

            return builder.ConnectionString;
        }
    }
}