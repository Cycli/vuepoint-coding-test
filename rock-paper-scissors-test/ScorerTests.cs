using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using RockPaperScissors;

namespace RockPaperScissorsTest
{
    [TestFixture]
    public class ScorerTests
    {
        IScorer _Scorer;
        IEnumerable<IPlayer> _Players;
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _Scorer = new SimpleScorer();
            // No need to set a strategy
            _Players = new SimplePlayer[]{
                new SimplePlayer(@"Player 1", new RandomStrategy(null)),
                new SimplePlayer(@"Player 2", new RandomStrategy(null)),
                new SimplePlayer(@"Player 3", new RandomStrategy(null))
            };
        }
        [SetUp]
        public void Setup()
        {
            _Scorer.Start(_Players);
        }

        [Test]
        public void StartScorer()
        {
            var scores = _Scorer.Scores;
            Assert.AreEqual(3, scores.Count(), @"There should be three scores initialised");
            Assert.IsTrue(scores.All(s => _Players.Contains(s.Player)), @"All players have scores");
            Assert.IsTrue(scores.All(s => s.Score == 0), @"All player initial scores should be 0");
        }
        [TestCase(HandGesture.Rock, HandGesture.Scissors, HandGesture.Scissors, 2, 0, 0)]
        [TestCase(HandGesture.Rock, HandGesture.Paper, HandGesture.Scissors, 1, 1, 1)]
        [TestCase(HandGesture.Rock, HandGesture.Paper, HandGesture.Paper, 0, 1, 1)]
        [TestCase(HandGesture.Rock, HandGesture.Rock, HandGesture.Rock, 0, 0, 0)]
        [TestCase(HandGesture.Rock, HandGesture.Rock, HandGesture.Scissors, 1, 1, 0)]
        public void PlayerScoresWithTurns(HandGesture g1, HandGesture g2, HandGesture g3, int score1, int score2, int score3)
        {
            var turn1 = new Turn(_Players.ElementAt(0), g1);
            var turn2 = new Turn(_Players.ElementAt(1), g2);
            var turn3 = new Turn(_Players.ElementAt(2), g3);
            _Scorer.Update(new Turn[] { turn1, turn2, turn3 });
            Assert.AreEqual(score1, _Scorer.Scores.ElementAt(0).Score, $@"Player 1 should score {score1} points");
            Assert.AreEqual(score2, _Scorer.Scores.ElementAt(1).Score, $@"Player 2 should score {score2} points");
            Assert.AreEqual(score3, _Scorer.Scores.ElementAt(2).Score, $@"Player 3 should score {score3} points");
        }
        [Test]
        public void PlayerScoresAccumulate()
        {
            PlayerScoresWithTurns(HandGesture.Rock, HandGesture.Rock, HandGesture.Scissors, 1, 1, 0);
            PlayerScoresWithTurns(HandGesture.Paper, HandGesture.Scissors, HandGesture.Rock, 2, 2, 1);
            PlayerScoresWithTurns(HandGesture.Rock, HandGesture.Rock, HandGesture.Rock, 2, 2, 1);
            PlayerScoresWithTurns(HandGesture.Scissors, HandGesture.Scissors, HandGesture.Paper, 3, 3, 1);
        }
    }
}