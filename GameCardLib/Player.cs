using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLib
{
    public class Player
    {
        //Variables 
        Hand hand;
        string name;
        int playerID;
        int nrOfWins;
        List<bool> isGameWonList;
        string nickName, firstName, surName;
        int currency;
        int bettedCurrency;
        bool isBroke;

        //Properties
        public Hand Hand
        {
            get { return hand; }
            set
            {
                if (value != null)
                {
                    hand = value;
                }
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    name = value;
                }
            }
        }
        public int PlayerID
        {
            get { return playerID; }
            set
            {
                playerID = value;
            }
        }
        public int NrOfWins
        {
            get { return nrOfWins; }
            set { nrOfWins = value; }
        }
        public string NickName { get { return nickName; } set { nickName = value; } }
        public string FirstName { get { return firstName; } set { firstName = value; } }
        public string SurName { get { return surName; } set { surName = value; } }
        public int Currency
        {
            get { return currency; }
            set { currency = value; }
        }
        public int BettedCurrency
        {
            get { return bettedCurrency; }
            set { bettedCurrency = value; }
        }
        public bool IsBroke 
        { 
            get { return isBroke; }
            set { isBroke = value; } 
        }

        /// <summary>
        /// Player constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="hand"></param>
        public Player(int id, string name, Hand hand)
        {
            playerID = id;
            this.name = name;
            this.hand = hand;
            nrOfWins = 0;
            isGameWonList = new List<bool>();
            currency = 100;
            isBroke = false;
        }

        public void AddGame(bool isGameWon)
        {
            isGameWonList.Add(isGameWon);
        }
        public List<bool> GetIsGameWonList()
        {
            return isGameWonList;
        }
        public void ClearIsGameWonList()
        {
            isGameWonList.Clear();
        }

        public override string ToString()
        {
            return name;
        }
    }
}
