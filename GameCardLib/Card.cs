using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLib
{
    public class Card
    {
        //Variables
        private Suit suit;
        private Value value;
        private bool isHidden;

        //Properties

        /// <summary>
        /// Returns Suit Backside if the card is hidden (backside upwards), otherwise return the real suit
        /// </summary>
        public Suit Suit
        {
            get
            {
                if (isHidden)
                {
                    return Suit.Backside;
                }
                else
                {
                    return suit;
                }
            }
            set { suit = value; }
        }

        /// <summary>
        /// Returns Value Backside if the card is hidden (backside upwards), otherwise return the real value
        /// </summary>
        public Value Value
        {
            get {
                if (isHidden)
                {
                    return Value.Backside;
                }
                else
                {
                    return value;
                }
            }
            set { this.value = value; }
        }
        public bool IsHidden
        {
            get { return isHidden; }
            set { isHidden = value; }
        }

        /// <summary>
        /// Card constructor
        /// </summary>
        /// <param name="suit"></param>
        /// <param name="value"></param>
        /// <param name="isHidden"></param>
        public Card(Suit suit, Value value, bool isHidden)
        {
            this.suit = suit;
            this.value = value;
            this.isHidden = isHidden;
        }

        public override string ToString()
        {
            return suit.ToString() + " " + value.ToString();
        }
    }
}
