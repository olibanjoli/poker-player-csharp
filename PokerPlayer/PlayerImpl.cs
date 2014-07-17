using System.Linq;

namespace PokerPlayer
{
    using PokerPlayer.Generated;

    public class PlayerImpl
    {
        private Rootobject _gameState;

        private const string TeamName = "Poker-Bash";

        public const string Version = "0.2";

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

                if (Basher.hole_cards.All(p => p.rank == "A"))
                {
                    return _gameState.current_buy_in * 4;
                }
            }
            catch
            {
                return this.Call();
            }

            return this.Call();
        }

        private int Call()
        {
            return _gameState.current_buy_in - Basher.bet;
        }

        private Player Basher
        {
            get
            {
                return _gameState.players.First(p => p.name.Contains(TeamName));
            }
        }

    }
}