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
using System.Windows.Navigation;
using System.Windows.Shapes;
using GameCardLib;
using Microsoft.Win32;

namespace BlackJack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected BlackJackManager manager;
        int currentBet;
        int currentCurrency;
        public MainWindow()
        {
            InitializeComponent();
            manager = new BlackJackManager();
            currentBet = 0;
            buttonHit.IsEnabled = false;
            buttonStand.IsEnabled = false;
            buttonShuffle.IsEnabled = false;
            buttonContinue.IsEnabled = false;
            canvasBetting.Visibility = Visibility.Collapsed;

            //Subscribe to manager events
            manager.updateDealerHand += UpdateDealerHand;
            manager.updatePlayerHand += UpdatePlayerHand;
            manager.setWinner += SetWinner;
            manager.openSavePlayer += OpenPlayerInfoWindow;
            manager.updateAllTimeWins += UpdateAllTimeWins;
            manager.updateTotalCurrencies += UpdateTotalCurrencies;
            manager.updateBettingInfo += UpdateBettingInfo;
            manager.allBroke += AllPlayersBroke;

            //Subscribe to manager events with lambda
            manager.updateDealerScore += (sender, e) => labelDealerScore.Content = "Score: " + e.Score;
            manager.updatePlayerInfo += (sender, e) =>
            {
                labelPlayer.Content = "Player: " + e.Nickname;
                labelPlayerScore.Content = "Score: " + e.Score;
                lbCurrentScore.Items[e.CurrentPlayer] = e.Nickname + ":   " + e.Score;
            };

            manager.updateWins += (sender, e) =>
            {
                lbWins.Items[e.CurrentPlayer - 1] = e.Nickname + ":   " + e.Score + "kr";
            };

            manager.UpdateScoreBoards();
        }

        #region Button click
        /// <summary>
        /// Open new game window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNewGame_Click(object sender, RoutedEventArgs e)
        {
            NewGame window = new NewGame(manager) { Owner = this };
            window.Show();
            ResetUI();
            ResetCardSlots(canvasDealer);
            ResetCardSlots(canvasPlayer);
        }

        /// <summary>
        /// Shuffle the deck
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonShuffle_Click(object sender, RoutedEventArgs e)
        {
            manager.Shuffle();
            ResetCardSlots(canvasDealer);
            ResetCardSlots(canvasPlayer);
            buttonShuffle.IsEnabled = false;
            ResetUI();

            //TEST
            //manager.DeletePlayerHighScore("HS");
        }

        /// <summary>
        /// Player gets another card
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonHit_Click(object sender, RoutedEventArgs e)
        {
            buttonHit.IsEnabled = false;
            buttonStand.IsEnabled = false;
            manager.Hit();
        }

        /// <summary>
        /// Player stand its turn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStand_Click(object sender, RoutedEventArgs e)
        {
            buttonHit.IsEnabled = false;
            buttonStand.IsEnabled = false;
            manager.Stand();
        }

        /// <summary>
        /// Continue the game with new hands
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonContinue_Click(object sender, RoutedEventArgs e)
        {
            ResetUI();
            ResetCardSlots(canvasDealer);
            ResetCardSlots(canvasPlayer);
            manager.StartPlaceBet();
            buttonContinue.IsEnabled = false;
            buttonShuffle.IsEnabled = false;
        }

        /// <summary>
        /// Tell manager to start betting sequence
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPlaceBet_Click(object sender, RoutedEventArgs e)
        {
            canvasBetting.Visibility = Visibility.Hidden;
            labelCurrentBet.Content = "Current Bet: ";
            manager.PlaceBet(currentBet);
            manager.PlaceNextBet();

        }

        /// <summary>
        /// Open a new window to show player info with an event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFindPlayer_Click(object sender, RoutedEventArgs e)
        {
            FindPlayer window = new FindPlayer(manager, tbFindPlayer.Text) { Owner = this };
            window.Show();
        }
        #endregion

        /// <summary>
        /// Method to open a new player window with an event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenPlayerInfoWindow(object sender, PlayerInfoEventArgs e)
        {
            PlayerInfo window = new PlayerInfo(manager, e.CurrentPlayer) { Owner = this };
            window.Show();
        }

        #region Update UI

        /// <summary>
        /// Update the images for the dealer cards
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateDealerHand(object sender, ShowHandEventArgs e/* List<Card> cardList*/)
        {
            ResetCardSlots(canvasDealer);
            for (int i = 0; i < e.CardList.Count; i++)
            {
                string cardLocation = "pack://application:,,,/GameCardLib;Component/Cards/" + e.CardList[i].Value.ToString() + e.CardList[i].Suit.ToString() + ".png";
                if (canvasDealer.Children[i] is Image)
                {
                    ((Image)canvasDealer.Children[i]).Source = new BitmapImage(new Uri(cardLocation));
                }
            }
        }

        /// <summary>
        /// Update the images for the active players cards
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdatePlayerHand(object sender, ShowHandEventArgs e/*List<Card> cardList*/)
        {
            ResetCardSlots(canvasPlayer);
            buttonHit.IsEnabled = e.ButtonActive;
            buttonStand.IsEnabled = e.ButtonActive;
            //List<Card> cardList = new List<Card> { new Card(Suit.Hearts, Value.Eight), new Card(Suit.Clubs, Value.Eight), new Card(Suit.Spades, Value.Ace), new Card(Suit.Diamonds, Value.King) };
            for (int i = 0; i < e.CardList.Count; i++)
            {
                string cardLocation = "pack://application:,,,/GameCardLib;Component/Cards/" + e.CardList[i].Value.ToString() + e.CardList[i].Suit.ToString() + ".png";
                if (canvasPlayer.Children[i] is Image)
                {
                    ((Image)canvasPlayer.Children[i]).Source = new BitmapImage(new Uri(cardLocation));
                }
            }
        }

        /// <summary>
        /// Tell the player who wins the game
        /// </summary>
        /// <param name="result"></param>
        private void SetWinner(string result)
        {
            labelWinner.Content = "Winner: " + result;
            buttonContinue.IsEnabled = true;
            buttonShuffle.IsEnabled = true;
            buttonHit.IsEnabled = false;
            buttonStand.IsEnabled = false;
        }

        /// <summary>
        /// Update UI if all players are broke
        /// </summary>
        public void AllPlayersBroke()
        {
            labelWinner.Content = "All Players are Broke!";
            buttonContinue.IsEnabled = false;
            buttonShuffle.IsEnabled = false;
            buttonHit.IsEnabled = false;
            buttonStand.IsEnabled = false;
        }

        /// <summary>
        /// Update UI with all time wins as an event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateAllTimeWins(object sender, HighScoreEventArgs e)
        {
            lbAllTimeWins.Items.Clear();
            foreach (string score in e.Highscores)
            {
                lbAllTimeWins.Items.Add(score);
            }
        }

        /// <summary>
        /// Update UI with total currency wins as an event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateTotalCurrencies(object sender, HighScoreEventArgs e)
        {
            lbHighscoreCurrency.Items.Clear();
            foreach (string score in e.Highscores)
            {
                lbHighscoreCurrency.Items.Add(score);
            }
        }

        /// <summary>
        /// Update UI with betting info as an event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateBettingInfo(object sender, PlayerInfoEventArgs e)
        {
            canvasBetting.Visibility = Visibility.Visible;
            labelCurrency.Content = "Currency: " + e.Score;
            labelPlayer.Content = "Player: " + e.Nickname;
            currentCurrency = e.Score;
            currentBet = 0;
            sliderBet.Minimum = 0;
            sliderBet.Maximum = currentCurrency;
            sliderBet.Value = 0;
        }
        #endregion

        #region Reset UI

        /// <summary>
        /// Reset controls that shows information to the player
        /// </summary>
        private void ResetUI()
        {
            labelWinner.Content = "";
            labelPlayerScore.Content = "Score: ";
            labelDealerScore.Content = "Score: ";
            labelPlayer.Content = "Player: ";
            for (int i = 0; i < lbCurrentScore.Items.Count; i++)
            {
                lbCurrentScore.Items[i] = "";
            }
        }

        /// <summary>
        /// Reset the shown cards in the given canvas
        /// </summary>
        /// <param name="canvas">Canvas that should be resetted</param>
        private void ResetCardSlots(Canvas canvas)
        {
            foreach (UIElement control in canvas.Children)
            {
                if (control is Image)
                {
                    ((Image)control).Source = null;
                }
            }
        }
        #endregion

        /// <summary>
        /// Set label and currentBet when slider value is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            labelCurrentBet.Content = "Current Bet: " + (int)sliderBet.Value;
            currentBet = (int)sliderBet.Value;
        }

    }
}
