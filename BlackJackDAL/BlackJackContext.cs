using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackDAL
{
    public class BlackJackContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayedGame> PlayedGames { get; set; }
        public DbSet<DealerInfo> DealerInfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=(LocalDB)\MSSQLLocalDB;Database=BlackJackDb;Trusted_Connection=True");
            /*SqlConnection conConnect = new SqlConnection*/
            //optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=MAU\C#\BlackJack\BlackJackDb.mdf;Integrated Security=True");
            //optionsBuilder.UseSqlServer(@"Server=(LocalDB)\MSSQLLocalDB;Database=BlackJack\BlackJackDb.mdf;Trusted_Connection=True");
            //optionsBuilder.UseSqlServer(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Admin\Documents\MAU\C#\BlackJack\BlackJackDb.mdf;Integrated Security=True;Connect Timeout=30");
            AppDomain.CurrentDomain.SetData("DataDirectory", Directory.GetCurrentDirectory());
            optionsBuilder.UseSqlServer(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =|DataDirectory|\BlackJackDb.mdf;Integrated Security=True;Connect Timeout=30");
            //optionsBuilder.UseSqlServer("Data Source = (LocalDb)\v11.0; Initial Catalog = Things; Integrated Security = SSPI");
        }
    }
}
