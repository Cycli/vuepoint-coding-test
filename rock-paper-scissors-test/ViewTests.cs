using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;
using System;
using RockPaperScissors;
using System.IO;
using System.Text;

namespace RockPaperScissorsTest
{
    [TestFixture]
    public class ViewTests
    {

        IScoreView _ScoreView;
        ITurnView _TurnView;
        IEnumerable<IPlayer> _Players;
        TextWriter _TextWriter;



        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _ScoreView = new ConsoleScoreView();
            _TurnView = new ConsoleTurnView();

            _Players = new SimplePlayer[]{
                new SimplePlayer(@"Player 1", new RandomStrategy(null)),
                new SimplePlayer(@"Player 2", new RandomStrategy(null)),
                new SimplePlayer(@"Player 3", new RandomStrategy(null))
            };
        }
        [SetUp]
        public void SetUp()
        {
            _TextWriter = new StringWriter();
            Console.SetOut(_TextWriter);
        }
        [TearDown]
        public void TearDown()
        {
            _TextWriter.Close();
            // reset the output to the default
            var standardOutput = new StreamWriter(Console.OpenStandardOutput());
            Console.SetOut(standardOutput);
        }

        [Test]
        public void OutputTurnToConsole()
        {
            // Use a string builder to ensure line endings are OS correct.
            var sb = new StringBuilder();
            sb.AppendLine(@"                   PLAYER      SHAPE");
            sb.AppendLine(@"                 Player 1       Rock");
            sb.AppendLine(@"                 Player 2      Paper");
            sb.AppendLine(@"                 Player 3   Scissors");
            sb.AppendLine(@"------------------------------------");

            var expected = sb.ToString();

            var turns = new Turn[]
            {
                new Turn(_Players.ElementAt(0),HandGesture.Rock),
                new Turn(_Players.ElementAt(1),HandGesture.Paper),
                new Turn(_Players.ElementAt(2),HandGesture.Scissors),
            };
            _TurnView.View(turns);
            var actual = _TextWriter.ToString();
            // Don't match the entire string but just pickout the highlights
            Assert.IsNotNull(actual, @"The Turn Viewer should return a string");
            Assert.IsNotEmpty(actual, @"The Turn Viewer should return a non-empty string");
            Assert.AreEqual(expected, actual, @"The string should show the header and player turns");
        }
        [Test]
        public void OutputScoreToConsole()
        {
            // Use a string builder to ensure line endings are OS correct.
            var sb = new StringBuilder();
            sb.AppendLine(@"                   PLAYER      SCORE");
            sb.AppendLine(@" 1 :             Player 1          1");
            sb.AppendLine(@" 2 :             Player 2          1");
            sb.AppendLine(@" 3 :             Player 3          1");
            sb.AppendLine(@"------------------------------------");

            var expected = sb.ToString();

            var scorer = new SimpleScorer();
            scorer.Start(_Players);

            var turns = new Turn[]
            {
                new Turn(_Players.ElementAt(0),HandGesture.Rock),
                new Turn(_Players.ElementAt(1),HandGesture.Paper),
                new Turn(_Players.ElementAt(2),HandGesture.Scissors),
            };
            scorer.Update(turns);
            _ScoreView.View(scorer.Scores);
            var actual = _TextWriter.ToString();
            // Don't match the entire string but just pickout the highlights
            Assert.IsNotNull(actual, @"The Score Viewer should return a string");
            Assert.IsNotEmpty(actual, @"The Score Viewer should return a non-empty string");
            Assert.AreEqual(expected, actual, @"The string should show the header and player scores");
        }
    }
}