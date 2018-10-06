using NUnit.Framework;
using RockPaperScissors;
namespace RockPaperScissorsTest
{
    [TestFixture]
   public class HumanStrategyTests
    {

        [TestCase('r',HandGesture.Rock)]
        [TestCase('p',HandGesture.Paper)]
        [TestCase('s',HandGesture.Scissors)]
        public void GetGestureFromKeyPress(char key, HandGesture expectedGesture)
        {
            var input = new MockInput(key);
            var strategy = new HumanStrategy(input);
            var actualGesture = strategy.GetGesture().Result;
            // Simulate a keypress
            Assert.AreEqual(expectedGesture, actualGesture, $@"The keypress '{key}' should generate the gesture '{expectedGesture.ToString()}'.");
        }
    }
}