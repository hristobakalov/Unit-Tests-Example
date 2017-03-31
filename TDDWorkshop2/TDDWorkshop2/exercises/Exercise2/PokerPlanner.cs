using System;
using System.Collections.Generic;

namespace Exercise2
{
    public class PokerPlanner
    {
        private readonly IDictionary<string, Game> games;

        public PokerPlanner()
        {
            this.games = new Dictionary<string, Game>();
        }

        public Game NewGame(int numberOfPlayers)
        {
            return new Game(numberOfPlayers);
        }

        public Game NewGame(string gameName, int numberOfPlayers)
        {
            return games[gameName] = new Game(numberOfPlayers);
        }

        public Game GetGame(string gameName)
        {
            return games[gameName];
        }
    }
}
