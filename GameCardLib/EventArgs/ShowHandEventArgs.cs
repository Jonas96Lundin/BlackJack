using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLib
{
    /// <summary>
    /// Event args for sending info about a players hand/cards
    /// </summary>
    public class ShowHandEventArgs : EventArgs
    {
        //Variables
        private List<Card> cardList;
        private bool buttonActive;

        //Properties
        public List<Card> CardList
        {
            get { return cardList; }
        }
        public bool ButtonActive
        {
            get { return buttonActive; }
        }

        /// <summary>
        /// ShowHandEventArgs Constructor
        /// </summary>
        /// <param name="cardList"></param>
        /// <param name="buttonActive"></param>
        public ShowHandEventArgs(List<Card> cardList, bool buttonActive)
        {
            this.cardList = cardList;
            this.buttonActive = buttonActive;
        }
    }
}
