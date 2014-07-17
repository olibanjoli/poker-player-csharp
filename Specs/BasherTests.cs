using System.Collections.Generic;
using System.Linq;
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

            Assert.AreEqual(0, result);
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

            Assert.AreEqual(80, result);
        }

        [Test]
        public void Pair()
        {
            var bashers = new PlayerImpl();

            var result = bashers.BetRequest(new Rootobject()
            {
                players = new Player[] { new Player() {name = "asdf"}, new Player() {name = "ajsidfj"}, CreateBasher(0, new[] { new Card("h", "4"), new Card("c", "4") }) },
                small_blind = 10,
                pot = 30,
                current_buy_in = 20,
            });

            Assert.AreEqual(0, result);
        }

        [Test]
        public void RaiseExceptionTest()
        {
            var bashers = new PlayerImpl();

            var result = bashers.BetRequest(new Rootobject()
            {
                players = new Player[] { new Player(), new Player(), CreateBasher(0, new[] { new Card("h", "4"), new Card("c", "10") }) },
                small_blind = 10,
                pot = 30,
                current_buy_in = 20,
            });

            Assert.AreEqual(0, result);
        }


        [Test]
        public void RankComparable()
        {
            var c1 = new Card("s", "k");
            var c2 = new Card("s", "3");

            var result = c1.CompareTo(c2);

            Assert.AreEqual(1, result);
        }

        [Test]
        public void Blub()
        {
            PlayerService.GetGameStateFromForm(@"[{ 	""players"": [{ 		""name"": ""Team Rocket"", 		""stack"": 0, 		""status"": ""out"", 		""bet"": 0, 		""hole_cards"": null, 		""version"": ""0.0.7"", 		""id"": 0 	}, 	{ 		""name"": ""Basher"", 		""stack"": 1000, 		""status"": ""active"", 		""bet"": 0, 		""version"": ""0.7"", 		""id"": 1, 		""hole_cards"": [{ 			""rank"": ""4"", 			""suit"": ""hearts"" 		}, 		{ 			""rank"": ""9"", 			""suit"": ""hearts"" 		}] 	}, 	{ 		""name"": ""Scalatron"", 		""stack"": 1000, 		""status"": ""active"", 		""bet"": 0, 		""version"": ""0.1.0"", 		""id"": 2, 		""hole_cards"": null 	}, 	{ 		""name"": ""Grischa"", 		""stack"": 990, 		""status"": ""active"", 		""bet"": 10, 		""version"": ""Budweiser"", 		""id"": 3, 		""hole_cards"": null 	}, 	{ 		""name"": ""Bender"", 		""stack"": 980, 		""status"": ""active"", 		""bet"": 20, 		""version"": ""Always calling player"", 		""id"": 4, 		""hole_cards"": null 	}, 	{ 		""name"": ""Heroku Bender"", 		""stack"": 900, 		""status"": ""active"", 		""bet"": 100, 		""version"": ""Always calling player"", 		""id"": 5, 		""hole_cards"": null 	}], 	""small_blind"": 10, 	""orbits"": 0, 	""dealer"": 2, 	""community_cards"": [], 	""current_buy_in"": 100, 	""pot"": 130 }]");
        }

        private static Player CreateBasher(int bet = 0, IEnumerable<Card> holeCards = null)
        {
            if (holeCards == null)
            {
                holeCards = new List<Card>() { new Card { rank = "2", suit = "s" }, new Card { rank = "7", suit = "h" }};
            }
            return new Player
                       {
                           version = "0.1-poker-bash",
                           name = "Poker-Bash",
                           bet = bet,
                           hole_cards = holeCards.ToArray()
                       };
        }
    }
}