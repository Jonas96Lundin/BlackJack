using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLib
{
    /// <summary>
    /// Event args for sending highscore info
    /// </summary>
    public class HighScoreEventArgs
    {
        private List<string> highscores;

        public List<string> Highscores { get { return highscores; } }

        /// <summary>
        /// HighScoreEventArgs constructor
        /// </summary>
        /// <param name="highscores"></param>
        public HighScoreEventArgs(List<string> highscores)
        {
            this.highscores = highscores;
        }
    }
}
