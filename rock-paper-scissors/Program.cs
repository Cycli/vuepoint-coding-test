using System;
using System.Collections.Generic;
// Allow only the test assembly to see the classes in the implementation
using System.Threading.Tasks;
namespace RockPaperScissors
{
    // The program entry class. Most setup parameters are hardwired in here but could be set
    // by configuration, but the number of rounds can be passed in as an integer argument.
    class Program
    {
        static async Task Main(string[] args)
        {
            if(args.Length == 0 || !uint.TryParse(args[0], out var rounds))
            {
                throw new ArgumentException("You must pass the number of rounds you wish to play.");
            }
            if(rounds == 0)
            {
                throw new ArgumentException("The number of rounds cannot be zero.");
            }

            var random = new UniformRandom();
            IEnumerable<IPlayer> players = new IPlayer[]{
                 new SimplePlayer(@"Paul", new HumanStrategy(new ConsoleInput())),
                 new SimplePlayer(@"Peter", new LearningStrategy(random)),
                 new SimplePlayer(@"Katy", new LearningStrategy(random)),
                 new SimplePlayer(@"Sami", new RandomStrategy(random)),
            };

            IScorer scorer = new SimpleScorer();

            var scoreView = new ConsoleScoreView();
            var turnView = new ConsoleTurnView();

            IGame game = new Game(players, scorer, rounds); 

            while(!game.IsFinished())
            {
                var turns = await game.RunRound();
                turnView.View(turns);
            }

            scoreView.View(scorer.Scores);
        }
    }
}
