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
    /// Interaction logic for NewGame.xaml
    /// </summary>
    public partial class NewGame : Window
    {
        //public event EventHandler<StartGameEventArgs> startGame;
        BlackJackManager manager;
        public NewGame(BlackJackManager manager)
        {
            InitializeComponent();
            this.manager = manager;
        }

        /// <summary>
        /// Start a new game and update UI accordingly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNGStart_Click(object sender, RoutedEventArgs e)
        {
            int nrOfDecks = UtilitiesLib.Parser.StringToInt(tbNGDecks.Text);
            int nrOfPlayers = UtilitiesLib.Parser.StringToInt(tbNGPlayers.Text);

            if (nrOfDecks < 1 || nrOfPlayers < 1)
            {
                MessageBox.Show("Amount of decks and players must be positive number");
            }
            else
            {
                ((MainWindow)Application.Current.MainWindow).labelDecks.Content = "Nr of Decks: " + nrOfDecks;
                ((MainWindow)Application.Current.MainWindow).labelPlayers.Content = "Nr of Players: " + nrOfPlayers;
                ((MainWindow)Application.Current.MainWindow).lbCurrentScore.Items.Clear();
                ((MainWindow)Application.Current.MainWindow).lbWins.Items.Clear();
                for (int i = 1; i <= nrOfPlayers; i++)
                {
                    ((MainWindow)Application.Current.MainWindow).lbCurrentScore.Items.Add("");
                    ((MainWindow)Application.Current.MainWindow).lbWins.Items.Add("");
                }

                manager.StartGame(nrOfDecks, nrOfPlayers);
                Close();
            }
        }
    }
}
