using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using RockPaperScissors;

namespace RockPaperScissorsTest
{
    [TestFixture]
    public class LearningStrategyTests
    {
        MockRandom _Random;
        IStrategy _Strategy;
        IEnumerable<HandGesture> _Gestures;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _Random = new MockRandom();
            _Gestures = new HandGesture[]
            {
                HandGesture.Rock,
                HandGesture.Paper,
                HandGesture.Scissors
            };
        }
        [SetUp]
        public void SetUp()
        {
            _Strategy = new LearningStrategy(_Random);
        }

        [Test]
        public void FirstRoundReturnsAnyGesture()
        {
            _Random.Set(0);
            var g1 = _Strategy.GetGesture().Result;
            _Random.Set(1);
            var g2 = _Strategy.GetGesture().Result;
            _Random.Set(2);
            var g3 = _Strategy.GetGesture().Result;
            var g = new HandGesture[]{g1, g2, g3};
            Assert.That(g.All(k => _Gestures.Contains(k)),@"The first move should generate any gesture with equal probability");
        }
        [Test]
        public void SecondRoundShouldLearnGestures()
        {
            var player1 = new SimplePlayer(@"Player 1", _Strategy);
            var player2  = new SimplePlayer(@"Player 2", new MockStrategy(HandGesture.Paper));
            var player3  = new SimplePlayer(@"Player 3", new MockStrategy(HandGesture.Scissors));

            // Play some initial turns
            var turns = new Turn[]{
                new Turn(player1, HandGesture.Rock), 
                new Turn(player2, HandGesture.Paper), 
                new Turn(player3, HandGesture.Scissors)
            };
            // Call the strategy's learner
            _Strategy.Learn(player1, turns).Wait();
            // This strategy just stores plays and picks the winner against them at random
            // Use the mock random to force the selection
            // Position 3 will win again paper = scissors
            // Position 4 will win again scissors = rock
            _Random.Set(3);
            var g1 = _Strategy.GetGesture().Result;
            _Random.Set(4);
            var g2 = _Strategy.GetGesture().Result;
            Assert.AreEqual(HandGesture.Scissors, g1, @"The strategy should play Scissors if responding to player 1's last move");
            Assert.AreEqual(HandGesture.Rock, g2, @"The strategy should play Rock if responding to player 2's last move");
        }
    }
}