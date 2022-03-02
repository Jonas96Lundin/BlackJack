using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLib
{
    public class Hand
    {
        //Variables
        Deck deck;

        //Properties
        public Card LastCard
        {
            get { return deck.GetAt(deck.NumberOfCards()); }
        }
        public int NumberOfCards
        {
            get { return deck.NumberOfCards(); }
        }
        public int Score
        {
            get { return deck.SumOfCards(); }
        }
        public int BlackJackScore
        {
            get { return deck.BlackJackSumOfCards(); }
        }

        /// <summary>
        /// Hand Constructor
        /// </summary>
        /// <param name="deck"></param>
        public Hand(Deck deck)
        {
            this.deck = deck;
        }

        /// <summary>
        /// Add card to the hand
        /// </summary>
        /// <param name="card"></param>
        public void AddCard(Card card)
        {
            deck.AddCard(card);
        }

        /// <summary>
        /// Clear the hand
        /// </summary>
        public void Clear()
        {
            deck.DiscardCards();
        }

        /// <summary>
        /// Return all cards in hand
        /// </summary>
        /// <returns></returns>
        public List<Card> GetAllCards()
        {
            return deck.GetAllCards();
        }

        /// <summary>
        /// Hide or show a card at given position
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="isHidden"></param>
        public void ToggleHideCardAt(int pos, bool isHidden)
        {
            deck.ToggleHideCardAt(pos, isHidden);
        }

        /// <summary>
        /// Return the deck in hand
        /// </summary>
        /// <returns></returns>
        public Deck GetDeck()
        {
            return deck;
        }

        public override string ToString()
        {
            return "This is a deck with: " + NumberOfCards + " amount of cards with the sum of: " + BlackJackScore;
        }
    }
}
