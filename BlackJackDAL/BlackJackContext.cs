using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            optionsBuilder.UseSqlServer(@"Server=(LocalDB)\MSSQLLocalDB;Database=BlackJackDb;Trusted_Connection=True");
        }
    }
}
