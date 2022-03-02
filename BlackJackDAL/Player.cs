using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackDAL
{
    public class Player
    {
        [Key]
        public string NickName { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public int TotalCurrency { get; set; }
        public List<PlayedGame> PlayedGames { get; set; }

        public Player()
        {
            PlayedGames = new List<PlayedGame>();
        }
    }
}
