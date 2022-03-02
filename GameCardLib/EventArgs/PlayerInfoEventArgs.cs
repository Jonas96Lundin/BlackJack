using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLib
{
    /// <summary>
    /// Event args for sending player info
    /// </summary>
    public class PlayerInfoEventArgs : EventArgs
    {
        //Variables
        private int currentPlayer;
        private int score;
        private string nickName;

        //Properties
        public int CurrentPlayer
        {
            get { return currentPlayer; }
        }
        public int Score
        {
            get { return score; }
        }
        public string Nickname
        {
            get { return nickName; }
        }

        /// <summary>
        /// PlayerInfoEventArgs Constructor
        /// </summary>
        /// <param name="currentPlayer"></param>
        /// <param name="score"></param>
        public PlayerInfoEventArgs(int currentPlayer, int score, string nickName)
        {
            this.currentPlayer = currentPlayer;
            this.score = score;
            this.nickName = nickName;
        }
    }
}
