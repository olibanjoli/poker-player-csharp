using System.Collections.Generic;
using System.Linq;

namespace PokerPlayer
{
    using PokerPlayer.Generated;

    public class PlayerImpl
    {


        private Rootobject _gameState;

        private const string TeamName = "Poker-Bash";

        public const string Version = "0.4";

        public string Check()
        {
            return string.Concat("I am running! My team: ", TeamName);
        }

        public void Showdown(Rootobject gameState)
        {
            this._gameState = gameState;
        }

        public int BetRequest(Rootobject gameState)
        {
            try
            {
                _gameState = gameState;

                if (this.IsPair("A", "K", "Q", "J"))
                {
                    return _gameState.current_buy_in * 4;
                }

                if (this.IsCombination("2", "7") ||
                    this.IsCombination("3", "7") ||
                    this.IsCombination("5", "9") ||
                    this.IsCombination("6", "J") ||
                    this.IsCombination("7", "J") ||
                    this.IsCombination("8", "J") ||
                    this.IsCombination("9", "Q") ||
                    this.IsCombination("2", "Q") ||
                    this.IsCombination("3", "Q") ||
                    this.IsCombination("4", "Q") ||
                    this.IsCombination("5", "Q") ||
                    this.IsCombination("6", "Q") ||
                    this.IsCombination("7", "Q") ||
                    this.IsCombination("8", "Q") ||
                    this.IsCombination("9", "K") ||
                    this.IsCombination("2", "K") ||
                    this.IsCombination("3", "K") ||
                    this.IsCombination("4", "K") ||
                    this.IsCombination("5", "K") ||
                    this.IsCombination("6", "K") ||
                    this.IsCombination("7", "K") ||
                    this.IsCombination("8", "K") ||
                    this.IsCombination("4", "7"))
                {
                    return 0;
                }
            }
            catch
            {
                return int.MaxValue;
            }

            return this.Call();
        }

        public bool HaveCardLowerThan(Card limit)
        {
            return Basher.hole_cards.Any(c => c.CompareTo(limit) < 0);
        }

        private bool IsCombination(string rank1, string rank2)
        {
            return Basher.hole_cards.Any(c => c.rank == rank1) && Basher.hole_cards.Any(c => c.rank == rank2);
        }

        private bool IsPair(params string[] ranks)
        {
            return ranks.Any(IsPair);
        }

        private bool IsPair()
        {
            return this.Basher.hole_cards.First().rank == this.Basher.hole_cards[1].rank;
        }

        private bool IsPair(string rank)
        {
            return this.Basher.hole_cards.All(p => p.rank == rank);
        }

        private int Call()
        {
            return _gameState.current_buy_in - Basher.bet;
        }

        private Player Basher
        {
            get
            {
                return _gameState.players.Where(p => !string.IsNullOrEmpty(p.name)).First(p => p.name.Contains(TeamName));
            }
        }
    }
}