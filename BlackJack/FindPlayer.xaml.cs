using GameCardLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BlackJack
{
    /// <summary>
    /// Interaction logic for FindPlayer.xaml
    /// </summary>
    public partial class FindPlayer : Window
    {
        BlackJackManager manager;
        string nickName;
        public FindPlayer(BlackJackManager manager, string nickName)
        {
            InitializeComponent();
            this.manager = manager;
            this.nickName = nickName;
            FindPlayerInfo();
        }

        /// <summary>
        /// Retrieve information about the player from the manager and update UI accordingly
        /// </summary>
        private void FindPlayerInfo()
        {
            List<string> playerInformation = manager.FindPlayerInfo(nickName);
            if (playerInformation[0] == string.Empty)
            {
                labelPlayerNickName.Content = "No Player Found!";
            }
            else
            {
                labelPlayerNickName.Content = playerInformation[0];
                labelFullName.Content = "Full Name: " + playerInformation[1] + " " + playerInformation[2];
                labelPlayedGames.Content = "Played Games: " + playerInformation[3];
                labelWins.Content = "Wins: " + playerInformation[4];
                labelLosses.Content = "Losses: " + playerInformation[5];
                labelWinRatio.Content = "Win Ratio: " + playerInformation[6] + "%";
                labelCurrencyWins.Content = "Total Winnings: " + playerInformation[7];
                labelHighscorePos.Content = "Highscore Position: "  + playerInformation[8];
            }
        }
    }
}
