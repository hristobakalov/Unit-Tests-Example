using System;
using System.Collections.Generic;
using System.Threading;

namespace Exercise2
{
    public class Game
    {
        private int numberOfPlayers;
        private List<string> cards;

        private string[] acceptableCards = new string[] {"1", "2", "3", "5", "8", "13", "20", "40", "100", "Coffee"};

        private AutoResetEvent gameCompleted = new AutoResetEvent(false);

        public Game(int numberOfPlayers)
        {
            this.numberOfPlayers = numberOfPlayers;
            this.cards = new List<string>();
        }

        public bool IsCompleted
        {
            get
            {
                return this.cards.Count >= this.numberOfPlayers;
            }
        }

        public void Play(string card)
        {
            if (IsCompleted)
            {
                throw new InvalidOperationException("the game is already completed!");
            }
            bool isValid = false;
            foreach (var cardValue in acceptableCards)
            {
                if (cardValue == card)
                {
                    isValid = true;
                    break;
                }
            }
            if (isValid)
            {
                this.cards.Add(card);

                // is the game complete
                if (this.IsCompleted)
                {
                    // signal this game is complete
                    this.gameCompleted.Set();
                }
            }
            else{
                throw new ArgumentException("Your card is not valid!");
            }
            
        }

        public string[] WaitForResult(int timeout)
        {
            if (this.IsCompleted || this.gameCompleted.WaitOne(timeout))
            {
                return this.cards.ToArray();
            }

            throw new System.TimeoutException("Not waiting longer for other players to submit their cards.");
        }

        public void ResetGame()
        {
            gameCompleted = new AutoResetEvent(false);
            cards.Clear();
        }
    }
}
