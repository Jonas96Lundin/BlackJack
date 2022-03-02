using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackDAL
{
    public class DealerInfo
    {
        public int DealerInfoId { get; set; }
        public bool IsGameWon { get; set; }
        public int Score { get; set; }
    }
}
