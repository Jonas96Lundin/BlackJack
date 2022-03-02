using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using BlackJackDAL;

namespace GameCardLib
{
    /// <summary>
    /// Manager for handling Black Jack games
    /// </summary>
    public class BlackJackManager
    {
        // Variables
        Deck gameDeck;
        List<Player> players;
        Player[] activePlayers;
        public Player dealer;
        int currentPlayer;
        int deckAmount;
        int playerAmount;
        Repository repo;

        //Events
        public event EventHandler<ShowHandEventArgs> updateDealerHand;
        public event EventHandler<ShowHandEventArgs> updatePlayerHand;
        public event EventHandler<PlayerInfoEventArgs> updatePlayerInfo;
        public event EventHandler<PlayerInfoEventArgs> updateDealerScore;
        public event EventHandler<PlayerInfoEventArgs> updateWins;
        public event EventHandler<PlayerInfoEventArgs> openSavePlayer;
        public event EventHandler<HighScoreEventArgs> updateAllTimeWins;
        public event EventHandler<HighScoreEventArgs> updateTotalCurrencies;
        public event EventHandler<PlayerInfoEventArgs> updateBettingInfo;


        public event Action<string> setWinner;
        public event Action allBroke;

        /// <summary>
        /// BlackJackManager constructor
        /// </summary>
        public BlackJackManager()
        {
            players = new List<Player>();
            repo = new Repository();
        }

        /// <summary>
        /// Start a new game with a dealer, players and a new deck
        /// </summary>
        /// <param name="deckAmount"></param>
        /// <param name="playerAmount"></param>
        public void StartGame(int deckAmount, int playerAmount)
        {
            //Init activePlayers
            activePlayers = new Player[playerAmount];

            this.deckAmount = deckAmount;
            this.playerAmount = playerAmount;

            //Create a dealer and players
            dealer = new Player(0, "Dealer", new Hand(new Deck(new List<Card>())));
            dealer.NickName = "Dealer";
            players.Clear();
            for (int i = 1; i <= playerAmount; i++)
            {
                players.Add(new Player(i, "Player " + i, new Hand(new Deck(new List<Card>()))));
            }


            //Create new deck and shuffle it
            Shuffle();

            //Set players info
            openSavePlayer?.Invoke(this, new PlayerInfoEventArgs(1, 0, ""));

            //Assign cards to the players
            //SetGame();

            //updateAllTimeWins?.Invoke(this, new HighScoreEventArgs(repo.GetAllTimeWinsList()));
            //updateTotalCurrencies?.Invoke(this, new HighScoreEventArgs(repo.GetTotalCurrencyList()));
            UpdateScoreBoards();
        }

        /// <summary>
        /// Create new deck of cards
        /// </summary>
        private void NewDeck()
        {
            List<Card> gameDeckCards = new List<Card>();
            for (int i = 0; i < deckAmount; i++)
            {
                for (int j = 1; j < Enum.GetValues(typeof(Suit)).Length; j++)
                {
                    for (int k = 1; k < Enum.GetValues(typeof(Value)).Length; k++)
                    {
                        gameDeckCards.Add(new Card((Suit)j, (Value)k, false));
                    }
                }
            }
            gameDeck = new Deck(gameDeckCards);
        }

        /// <summary>
        /// Create new deck and shuffle it
        /// </summary>
        public void Shuffle()
        {
            NewDeck();
            gameDeck.Shuffle();
        }

        /// <summary>
        /// Assign cards to the dealer and players and invoke UI events
        /// </summary>
        public void SetGame()
        {
            if (gameDeck.NumberOfCards() < (deckAmount * 52 * 0.50))
            {
                Shuffle();
            }

            dealer.Hand = new Hand(new Deck(gameDeck.GetTwoCards()));
            dealer.Hand.ToggleHideCardAt(0, true);

            for (int i = 0; i < playerAmount; i++)
            {
                if (players[i].IsBroke)
                {
                    players[i].Hand = new Hand(new Deck(new List<Card>()));
                    activePlayers[i] = null;
                }
                else
                {
                    players[i].Hand = new Hand(new Deck(gameDeck.GetTwoCards()));
                    activePlayers[i] = players[i];
                }
            }

            currentPlayer = 0;

            foreach (Player player in players)
            {
                if (player.IsBroke)
                {
                    currentPlayer++;
                }
                else
                {
                    break;
                }
            }


            updateDealerHand?.Invoke(this, new ShowHandEventArgs(dealer.Hand.GetAllCards(), true));
            updatePlayerHand?.Invoke(this, new ShowHandEventArgs(players[currentPlayer].Hand.GetAllCards(), true));
            updatePlayerInfo?.Invoke(this, new PlayerInfoEventArgs(currentPlayer, players[currentPlayer].Hand.BlackJackScore, players[currentPlayer].NickName));
            updateDealerScore?.Invoke(this, new PlayerInfoEventArgs(0, dealer.Hand.BlackJackScore, dealer.NickName));
        }

        /// <summary>
        /// Current player gets a new card and invoke UI event
        /// </summary>
        public async void Hit()
        {
            players[currentPlayer].Hand.AddCard(gameDeck.GetAt(0));

            updatePlayerHand?.Invoke(this, new ShowHandEventArgs(players[currentPlayer].Hand.GetAllCards(), false));
            updatePlayerInfo?.Invoke(this, new PlayerInfoEventArgs(currentPlayer, players[currentPlayer].Hand.BlackJackScore, players[currentPlayer].NickName));

            await Task.Run(async () => await Task.Delay(1000));

            if (players[currentPlayer].Hand.BlackJackScore >= 21)
            {
                activePlayers[currentPlayer] = null;
            }

            NextPlayer();
        }

        /// <summary>
        /// Remove current player from activePlayers array if its standing
        /// </summary>
        public void Stand()
        {
            activePlayers[currentPlayer] = null;
            NextPlayer();
        }

        /// <summary>
        /// Switch to next player and if all players are standing switch to dealers turn
        /// </summary>
        private void NextPlayer()
        {
            currentPlayer++;


            for (int i = 0; i < players.Count; i++)
            {
                if (currentPlayer == players.Count)
                {
                    currentPlayer = 0;
                }
                if (activePlayers[currentPlayer] == null)
                {
                    currentPlayer++;
                }
                else
                {
                    updatePlayerHand?.Invoke(this, new ShowHandEventArgs(players[currentPlayer].Hand.GetAllCards(), true));
                    updatePlayerInfo?.Invoke(this, new PlayerInfoEventArgs(currentPlayer, players[currentPlayer].Hand.BlackJackScore, players[currentPlayer].NickName));
                    return;
                }
            }
            DealerTurn();
        }

        /// <summary>
        /// Dealer draws a new card until score is 17 or more, if any players score are below 22
        /// </summary>
        public async void DealerTurn()
        {
            dealer.Hand.ToggleHideCardAt(0, false);
            updateDealerHand?.Invoke(this, new ShowHandEventArgs(dealer.Hand.GetAllCards(), true));
            updateDealerScore?.Invoke(this, new PlayerInfoEventArgs(0, dealer.Hand.BlackJackScore, dealer.NickName));

            foreach (Player player in players)
            {
                if (player.Hand.BlackJackScore < 22 && !player.IsBroke)
                {

                    while (dealer.Hand.BlackJackScore < 17)
                    {
                        await Task.Run(async () => await Task.Delay(1000));
                        dealer.Hand.AddCard(gameDeck.GetAt(0));

                        updateDealerHand?.Invoke(this, new ShowHandEventArgs(dealer.Hand.GetAllCards(), true));
                        updateDealerScore?.Invoke(this, new PlayerInfoEventArgs(0, dealer.Hand.BlackJackScore, dealer.NickName));
                    }
                    break;
                }
            }

            EndGame();
        }

        /// <summary>
        /// When all players and dealer is done, compare scores and assign winner(s)
        /// </summary>
        private void EndGame()
        {
            int topScore = 0;
            string winners = "";

            foreach (Player player in players)
            {
                if (player.Hand.BlackJackScore > dealer.Hand.BlackJackScore && player.Hand.BlackJackScore < 22 || player.Hand.BlackJackScore < 22 && dealer.Hand.BlackJackScore > 21 && !player.IsBroke)
                {
                    winners += player.NickName + " & ";
                    player.Currency += player.BettedCurrency * 2;
                    updateWins?.Invoke(this, new PlayerInfoEventArgs(player.PlayerID, player.Currency, player.NickName));
                    player.AddGame(true);
                    repo.SavePlayerInfo(player.NickName, player.FirstName, player.SurName, true, player.BettedCurrency);
                }
                else if(!player.IsBroke)
                {
                    player.AddGame(false);
                    repo.SavePlayerInfo(player.NickName, player.FirstName, player.SurName, false, -player.BettedCurrency);
                    if (player.Currency <= 0)
                    {
                        player.IsBroke = true;
                    }
                }
            }

            if (winners != "")
            {
                //Set winners label
                setWinner?.Invoke(winners.TrimEnd(new char[] { '&', ' ' }));
                repo.SaveDealerInfo(dealer.Hand.BlackJackScore, false);
            }
            else
            {
                //Set dealer wins label
                setWinner?.Invoke(dealer.ToString());
                repo.SaveDealerInfo(dealer.Hand.BlackJackScore, true);
            }
            //updateAllTimeWins?.Invoke(this, new HighScoreEventArgs(repo.GetAllTimeWinsList()));
            //updateTotalCurrencies?.Invoke(this, new HighScoreEventArgs(repo.GetTotalCurrencyList()));
            UpdateScoreBoards();
        }

        /// <summary>
        /// Save player information to current player
        /// </summary>
        /// <param name="playerID"></param>
        /// <param name="nickName"></param>
        /// <param name="firstName"></param>
        /// <param name="surName"></param>
        public void SavePlayer(int playerID, string nickName, string firstName, string surName)
        {
            players[playerID - 1].NickName = nickName;
            players[playerID - 1].FirstName = firstName;
            players[playerID - 1].SurName = surName;
            updateWins?.Invoke(this, new PlayerInfoEventArgs(players[playerID - 1].PlayerID, players[playerID - 1].Currency, players[playerID - 1].NickName));

            SaveNextPlayer(playerID);
        }

        /// <summary>
        /// Open next player to save, if all players are saved start the betting sequence
        /// </summary>
        /// <param name="playerID"></param>
        public void SaveNextPlayer(int playerID)
        {
            if (playerID < players.Count)
            {
                openSavePlayer?.Invoke(this, new PlayerInfoEventArgs(playerID + 1, players[playerID].Hand.BlackJackScore, players[playerID].NickName));
            }
            else
            {
                StartPlaceBet();
            }
        }

        /// <summary>
        /// Delete player from database
        /// </summary>
        /// <param name="nickName"></param>
        public void DeletePlayerHighScore(string nickName)
        {
            repo.Delete(nickName);
        }

        /// <summary>
        /// Update UI and if all players has betted set game 
        /// </summary>
        public void PlaceNextBet()
        {
            if (currentPlayer < players.Count)
            {
                if (players[currentPlayer].IsBroke)
                {
                    currentPlayer++;
                    PlaceNextBet();
                    return;
                }
                updateBettingInfo?.Invoke(this, new PlayerInfoEventArgs(currentPlayer + 1, players[currentPlayer].Currency, players[currentPlayer].NickName));
            }
            else
            {
                SetGame();
            }
        }

        /// <summary>
        /// Start place bet sequence by checking if players are broke
        /// </summary>
        public void StartPlaceBet()
        {
            currentPlayer = 0;
            foreach (Player player in players)
            {
                if (player.IsBroke)
                {
                    currentPlayer++;
                }
                else
                {
                    break;
                }
                if (currentPlayer == players.Count)
                {
                    allBroke?.Invoke();
                    return;
                }
            }
            
            PlaceNextBet();
        }

        /// <summary>
        /// Save betting info to current player
        /// </summary>
        /// <param name="currentBet"></param>
        public void PlaceBet(int currentBet)
        {
            players[currentPlayer].BettedCurrency = currentBet;
            players[currentPlayer].Currency -= currentBet;
            updateWins?.Invoke(this, new PlayerInfoEventArgs(players[currentPlayer].PlayerID, players[currentPlayer].Currency, players[currentPlayer].NickName));
            currentPlayer++;
        }

        /// <summary>
        /// Retrieve player info from repository
        /// </summary>
        /// <param name="nickName"></param>
        /// <returns></returns>
        public List<string> FindPlayerInfo(string nickName)
        {
            List<string> playerInformation = new List<string>();
            playerInformation.AddRange(repo.FindPlayerInfo(nickName));


            return playerInformation;
        }

        /// <summary>
        /// Update score boards in presentation layer
        /// </summary>
        public void UpdateScoreBoards()
        {
            updateAllTimeWins?.Invoke(this, new HighScoreEventArgs(repo.GetAllTimeWinsList()));
            updateTotalCurrencies?.Invoke(this, new HighScoreEventArgs(repo.GetTotalCurrencyList()));
        }
    }
}
