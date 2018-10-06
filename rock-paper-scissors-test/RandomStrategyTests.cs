using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using RockPaperScissors;

namespace RockPaperScissorsTest
{
    [TestFixture]
    public class RandomStrategyTests
    {
        MockRandom _Random;
        IStrategy _Strategy;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _Random = new MockRandom();
        }
        [SetUp]
        public void SetUp()
        {
            _Strategy = new RandomStrategy(_Random);
        }
        [TestCase(0, HandGesture.Rock)]
        [TestCase(1, HandGesture.Paper)]
        [TestCase(2, HandGesture.Scissors)]
        public void GetGesturesFromRandom(int rnd, HandGesture expectedGesture)
        {
            _Random.Set(rnd);
            var g = _Strategy.GetGesture().Result;
            Assert.AreEqual(expectedGesture, g, $@"The random index {rnd} should return the gesture {expectedGesture}.");
        }
    }
}