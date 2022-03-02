using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesLib;

namespace GameCardLib
{
    public class Deck
    {
        //Variables
        ListManager<Card> cards;
        Random randomArranger;

        /// <summary>
        /// Deck Constructor
        /// </summary>
        /// <param name="cardList"></param>
        public Deck(List<Card> cardList)
        {
            InitializeDeck(cardList);
            randomArranger = new Random();
        }

        /// <summary>
        /// Create the card list with a ListManager and add the given cardList to it
        /// </summary>
        /// <param name="cardList"></param>
        private void InitializeDeck(List<Card> cardList)
        {
            cards = new ListManager<Card>();
            cards.List.AddRange(cardList);
        }

        /// <summary>
        /// Add card to the deck
        /// </summary>
        /// <param name="card"></param>
        public void AddCard(Card card)
        {
            cards.Add(card);
        }

        /// <summary>
        /// Discard all cards from the deck
        /// </summary>
        public void DiscardCards()
        {
            cards.DeleteAll();
        }

        /// <summary>
        /// Get a card at given positon
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public Card GetAt(int pos)
        {
            Card tempCard = cards.GetAt(pos);
            cards.DeleteAt(pos);
            return tempCard;
        }

        /// <summary>
        /// Get the two first card in the deck
        /// </summary>
        /// <returns></returns>
        public List<Card> GetTwoCards()
        {
            List<Card> twoCards = new List<Card>();
            twoCards.Add(cards.GetAt(0));
            twoCards.Add(cards.GetAt(1));
            cards.DeleteAt(0);
            cards.DeleteAt(0);
            return twoCards;
        }

        /// <summary>
        /// Return the number of cards in the deck
        /// </summary>
        /// <returns></returns>
        public int NumberOfCards()
        {
            return cards.Count;
        }

        /// <summary>
        /// Shuffle the deck
        /// </summary>
        public void Shuffle()
        {
            int n = NumberOfCards();
            while( n > 1)
            {
                n--;
                int k = randomArranger.Next(n + 1);
                Card card = cards.GetAt(k);
                cards.ChangeAt(cards.GetAt(n), k);
                cards.ChangeAt(card, n);
            }
        }

        /// <summary>
        /// Remove card from the deck at the given position
        /// </summary>
        /// <param name="pos"></param>
        public void RemoveCard(int pos)
        {
            cards.DeleteAt(pos);
        }

        /// <summary>
        /// Returns the sum of all cards in the deck
        /// </summary>
        /// <returns></returns>
        public int SumOfCards()
        {
            int sum = 0;
            foreach (Card card in cards.List)
            {
                sum += (int)card.Value;
            }
            return sum;
        }

        /// <summary>
        /// Returns the sum of all cards in the deck with BlackJack value rules
        /// </summary>
        /// <returns></returns>
        public int BlackJackSumOfCards()
        {
            int sum = 0;
            int nrOfAces = 0;
            foreach (Card card in cards.List)
            {
                if ((int)card.Value > 10)
                {
                    sum += 10;
                }
                else
                {
                    sum += (int)card.Value;
                }
                if(card.Value == Value.Ace)
                {
                    nrOfAces += 1;
                }
            }
            for (int i = 0; i < nrOfAces; i++)
            {
                if (sum < 12)
                {
                    sum += 10;
                }
            }

            return sum;
        }

        /// <summary>
        /// Return a list of all cards in the deck
        /// </summary>
        /// <returns></returns>
        public List<Card> GetAllCards()
        {
            return cards.List;
        }

        /// <summary>
        /// Hide or show a card at given position in the deck
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="isHidden"></param>
        public void ToggleHideCardAt(int pos, bool isHidden)
        {
            cards.List[pos].IsHidden = isHidden;
        }

        public override string ToString()
        {
            return "This is a deck with: " + NumberOfCards() + " amount of cards with the sum of: " + SumOfCards();
        }
    }
}
