using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using PokerPlayer;
using PokerPlayer.Generated;

namespace Specs
{
    [TestFixture]
    public class BasherTests
    {
        [Test]
        public void Call()
        {
            var bashers = new PlayerImpl();

            var result = bashers.BetRequest(new Rootobject()
                                                {
                                                    players = new Player[] { new Player(), new Player(), CreateBasher() },
                                                    small_blind = 10,
                                                    pot = 30,
                                                    current_buy_in = 20
                                                });

            Assert.AreEqual(20, result);
        }

        [Test]
        public void CallAsSmallBlind()
        {
            var bashers = new PlayerImpl();

            var result = bashers.BetRequest(new Rootobject()
            {
                players = new Player[] { new Player(), new Player(), CreateBasher(10) },
                small_blind = 10,
                pot = 30,
                current_buy_in = 20
            });

            Assert.AreEqual(10, result);
        }

        [Test]
        public void CallAsBigBlind()
        {
            var bashers = new PlayerImpl();

            var result = bashers.BetRequest(new Rootobject()
            {
                players = new Player[] { new Player(), new Player(), CreateBasher(20) },
                small_blind = 10,
                pot = 30,
                current_buy_in = 20
            });

            Assert.AreEqual(0, result);
        }

        [Test]
        public void Raise()
        {
            var bashers = new PlayerImpl();

            var result = bashers.BetRequest(new Rootobject()
            {
                players = new Player[] { new Player(), new Player(), CreateBasher(20, new [] { new Card("h", "A"), new Card("c", "A") }) },
                small_blind = 10,
                pot = 30,
                current_buy_in = 20,
            });

            Assert.AreEqual(0, result);
        }

        private static Player CreateBasher(int bet = 0, IEnumerable<Card> holeCards = null)
        {
            return new Player
                       {
                           version = "0.1-poker-bash",
                           name = "poker-bash",
                           bet = bet
                       };
        }
    }
}