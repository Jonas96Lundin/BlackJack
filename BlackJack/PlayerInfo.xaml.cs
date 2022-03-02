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
using GameCardLib;

namespace BlackJack
{
    /// <summary>
    /// Interaction logic for PlayerInfo.xaml
    /// </summary>
    public partial class PlayerInfo : Window
    {
        BlackJackManager manager;
        int playerId;
        public PlayerInfo(BlackJackManager manager, int playerId)
        {
            InitializeComponent();
            this.manager = manager;
            labelPlayerInfo.Content = "Player " + playerId;
            this.playerId = playerId;
        }

        /// <summary>
        /// Call manager to save player if nickname is chosen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSavePlayer_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbNickname.Text))
            {
                MessageBox.Show("Must have Nickname");
            }
            else
            {
                labelPlayerInfo.Content = "Player " + playerId;
                manager.SavePlayer(playerId, tbNickname.Text, tbFirstname.Text, tbSurname.Text);
                Close();
                ((MainWindow)Application.Current.MainWindow).Activate();
            }
        }
    }
}
