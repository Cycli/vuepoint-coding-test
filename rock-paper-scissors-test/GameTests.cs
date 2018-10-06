using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using RockPaperScissors;
namespace RockPaperScissorsTest
{
    [TestFixture]
    class GameTests
    {
        IEnumerable<IPlayer> _NoPlayers;
        IEnumerable<IPlayer> _OnePlayer;
        IEnumerable<IPlayer> _TwoPlayers;
        IEnumerable<IPlayer> _ThreePlayers;
        IScorer _Scorer;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _Scorer = new SimpleScorer();

            var rockPlayer = new SimplePlayer(@"Rock Player", new MockStrategy(HandGesture.Rock));
            var paperPlayer = new SimplePlayer(@"Rock Player", new MockStrategy(HandGesture.Paper));
            var scissorsPlayer = new SimplePlayer(@"Rock Player", new MockStrategy(HandGesture.Scissors));
            _NoPlayers = new IPlayer[]{};
            _OnePlayer = new IPlayer[]{rockPlayer};
            _TwoPlayers = new IPlayer[]{rockPlayer, paperPlayer};
            _ThreePlayers = new IPlayer[]{rockPlayer, paperPlayer, scissorsPlayer};

        }
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CantPlayWithNoPlayersThrowsException()
        {
            var game = new Game(_NoPlayers, _Scorer, 5);
            RunRounds(game).Wait();
            Assert.IsEmpty(_Scorer.Scores, @"The game should not have any players");
        }
        [Test]
        public void PlayWithOnePlayer()
        {
            var game = new Game(_OnePlayer, _Scorer, 5);
            RunRounds(game).Wait();
            Assert.AreEqual(1, _Scorer.Scores.Count(), @"The game should have 1 player");
        }
        [Test]
        public void PlayWithTwoPlayers()
        {
            var game = new Game(_TwoPlayers, _Scorer, 5);
            RunRounds(game).Wait();
            Assert.AreEqual(2, _Scorer.Scores.Count(), @"The game should have 2 players");
        }
        [Test]
        public void PlayWithThreePlayers()
        {
            var game = new Game(_ThreePlayers, _Scorer, 5);
            RunRounds(game).Wait();
            Assert.AreEqual(3, _Scorer.Scores.Count(), @"The game should have 3 players");
        }

        [Test]
        public void RunRoundWithNoPlayers()
        {
            var game = new Game(_NoPlayers, _Scorer, 1);
            var turns = game.RunRound().Result;
            Assert.IsEmpty(turns, @"The game round will produce no turns");
        }
        [Test]
        public void RunRoundWithThreePlayers()
        {
            var game = new Game(_ThreePlayers, _Scorer, 1);
            var turns = game.RunRound().Result;
            Assert.AreEqual(3, turns.Count(), @"The game round will produce three turns");
            Assert.AreEqual(HandGesture.Rock, turns.ElementAt(0).Gesture, @"The first player should play 'Rock'");
            Assert.AreEqual(HandGesture.Paper, turns.ElementAt(1).Gesture, @"The second player should play 'Paper'");
            Assert.AreEqual(HandGesture.Scissors, turns.ElementAt(2).Gesture, @"The third player should play 'Scissors'");            
        }
        [Test]
        public void GameIsNotFinished()
        {
            var game = new Game(_ThreePlayers, _Scorer, 5);
            for(int i=0; i<3; i++)
            {
                game.RunRound().Wait();
            }
            Assert.IsFalse(game.IsFinished(), @"The game should not be finished");
        }
        [Test]
        public void GameIsFinished()
        {
            var game = new Game(_ThreePlayers, _Scorer, 5);
            for(int i=0; i<5; i++)
            {
                game.RunRound().Wait();
            }
            Assert.IsTrue(game.IsFinished(), @"The game should be finished");
        }
        async Task RunRounds(IGame game)
        {
            while(!game.IsFinished())
            {
                 await game.RunRound();
            }            
        }
    }
}