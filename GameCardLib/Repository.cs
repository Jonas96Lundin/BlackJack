using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlackJackDAL;

namespace GameCardLib
{
    public class Repository
    {
        /// <summary>
        /// Save player information to the database by creating a new entity or adding played game if the player already exists
        /// </summary>
        /// <param name="nickName"></param>
        /// <param name="firstName"></param>
        /// <param name="surName"></param>
        /// <param name="isGameWonList"></param>
        public void SavePlayerInfo(string nickName, string firstName, string surName, bool isGameWon, int newCurrency)
        {
            using (BlackJackContext blackJackContext = new BlackJackContext())
            {
                if (blackJackContext.Players.Find(nickName) != null)
                {
                    blackJackContext.Players.Find(nickName).PlayedGames.Add(new PlayedGame() { IsGameWon = isGameWon });
                    blackJackContext.Players.Find(nickName).TotalCurrency += newCurrency;
                    if (!string.IsNullOrEmpty(firstName))
                    {
                        blackJackContext.Players.Find(nickName).FirstName = firstName;
                    }
                    if (!string.IsNullOrEmpty(surName))
                    {
                        blackJackContext.Players.Find(nickName).SurName = surName;
                    }
                }
                else
                {
                    List<PlayedGame> tempList = new List<PlayedGame>();
                    tempList.Add(new PlayedGame() { IsGameWon = isGameWon });
                    blackJackContext.Players.Add(new BlackJackDAL.Player()
                    {
                        NickName = nickName,
                        FirstName = firstName,
                        SurName = surName,
                        PlayedGames = tempList,
                        TotalCurrency = newCurrency
                    });
                }
                blackJackContext.SaveChanges();
            }
        }

        /// <summary>
        /// Save Dealer information to the database
        /// </summary>
        /// <param name="score"></param>
        /// <param name="isGameWon"></param>
        public void SaveDealerInfo(int score, bool isGameWon)
        {
            using (BlackJackContext blackJackContext = new BlackJackContext())
            {
                blackJackContext.DealerInfo.Add(new DealerInfo()
                {
                    Score = score,
                    IsGameWon = isGameWon
                });
                blackJackContext.SaveChanges();
            }
        }

        /// <summary>
        /// Returns a list of strings with the players wins ordered
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllTimeWinsList()
        {
            using (BlackJackContext blackJackContext = new BlackJackContext())
            {
                List<string> result = new List<string>();
                foreach (BlackJackDAL.Player player in blackJackContext.Players
                    .Include(games => games.PlayedGames)
                    .OrderByDescending(nrOfWins => nrOfWins.PlayedGames.Where(isWon => isWon.IsGameWon).ToList().Count))
                {
                    var query = player.PlayedGames
                        .Where(isWon => isWon.IsGameWon)
                        .ToList();
                    result.Add(player.NickName.ToString() + ":   " + query.Count);
                }
                return result;
            }
        }

        /// <summary>
        /// Return a list of all players in the order of amount of won currency
        /// </summary>
        /// <returns></returns>
        public List<string> GetTotalCurrencyList()
        {
            using (BlackJackContext blackJackContext = new BlackJackContext())
            {
                List<string> result = new List<string>();
                foreach (BlackJackDAL.Player player in blackJackContext.Players.OrderByDescending(player => player.TotalCurrency))
                {
                    result.Add(player.NickName.ToString() + ":   " + player.TotalCurrency + "kr");
                }
                return result;
            }
        }

        /// <summary>
        /// Delete 
        /// </summary>
        /// <param name="nickName"></param>
        public void Delete(string nickName)
        {
            using (BlackJackContext blackJackContext = new BlackJackContext())
            {
                if (blackJackContext.Players.Find(nickName) != null)
                {
                    var player = blackJackContext.Players.Single(p => p.NickName == nickName);
                    var playedGames = blackJackContext.PlayedGames.Where(game => EF.Property<string>(game, "PlayerInfoNickName") == nickName);

                    foreach (var game in playedGames)
                    {
                        blackJackContext.PlayedGames.Remove(game);
                    }
                    blackJackContext.Remove(player);
                    blackJackContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Return a list of player information from player with the given nickname
        /// </summary>
        /// <param name="nickName"></param>
        /// <returns></returns>
        public List<string> FindPlayerInfo(string nickName)
        {
            using (BlackJackContext blackJackContext = new BlackJackContext())
            {
                List<string> playerInfo = new List<string>();
                
                if (blackJackContext.Players.Find(nickName) != null)
                {
                    var player = blackJackContext.Players.Where(p => p.NickName == nickName).Include(p => p.PlayedGames).FirstOrDefault();
                    playerInfo.Add(player.NickName);
                    playerInfo.Add(player.FirstName);
                    playerInfo.Add(player.SurName);
                    int gamesCount = player.PlayedGames.Count;
                    int wins = player.PlayedGames.Where(g => g.IsGameWon == true).Count();
                    int losses = player.PlayedGames.Where(g => g.IsGameWon == false).Count();
                    float winRatio = ((float)wins / (float)gamesCount) * 100;
                    playerInfo.Add(gamesCount.ToString());
                    playerInfo.Add(wins.ToString());
                    playerInfo.Add(wins.ToString());
                    playerInfo.Add(winRatio.ToString());
                    playerInfo.Add(player.TotalCurrency.ToString());

                    int pos = 0;
                    foreach (BlackJackDAL.Player p in blackJackContext.Players.OrderByDescending(p => p.TotalCurrency))
                    {
                        pos++;
                        if (p.NickName.ToUpper() == nickName.ToUpper())
                        {
                            playerInfo.Add(pos.ToString());
                            break;
                        }
                    }
                }
                else
                {
                    playerInfo.Add(string.Empty);
                }
                return playerInfo;
            }
        }
    }
}
