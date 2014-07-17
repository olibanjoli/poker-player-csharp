using System.Collections.Generic;
using System.IO;
using System.Linq;

using Newtonsoft.Json;

namespace PokerPlayer
{
    using PokerPlayer.Generated;

    public class PlayerImpl
    {
        public static List<string> requestLog = new List<string>(); 

        private Rootobject _gameState;

        private const string TeamName = "Poker-Bash";

        public const string Version = "0.6";

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
                requestLog.Add(JsonConvert.SerializeObject(gameState));

                _gameState = gameState;

                if (this.IsPair("A", "K", "Q", "J"))
                {
                    return _gameState.current_buy_in * 4;
                }

                if (this.IsPair())
                {
                    return this.Call();
                }

                if (Basher.hole_cards.Any(c => c.rank == "Q")
                    && Basher.hole_cards.Any(c => c.CompareTo(new Card("s", "9")) <= 0))
                {
                    return 0;
                }
                if (Basher.hole_cards.Any(c => c.rank == "K")
                    && Basher.hole_cards.Any(c => c.CompareTo(new Card("s", "8")) <= 0))
                {
                    return 0;
                }
                if (Basher.hole_cards.Any(c => c.rank == "J")
                    && Basher.hole_cards.Any(c => c.CompareTo(new Card("s", "8")) <= 0))
                {
                    return 0;
                }
                if (Basher.hole_cards.Any(c => c.rank == "7")
                    && Basher.hole_cards.Any(c => c.CompareTo(new Card("s", "4")) <= 0))
                {
                    return 0;
                }

                if (this.IsCombination("5", "9") )
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