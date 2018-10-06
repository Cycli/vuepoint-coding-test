using System;
using System.Collections.Generic;
using System.Linq;

namespace RockPaperScissors
{
    // A basic implementation of the IScoreView interface that generates readable
    // output of the final scores
    class ConsoleScoreView : IScoreView
    {
        readonly static string NAME_HEADER = "PLAYER";
        readonly static string SCORE_HEADER = "SCORE";
        public void View(IEnumerable<PlayerScore> scores)
        {
            Console.WriteLine($"     {NAME_HEADER,20} {SCORE_HEADER,10}");
            if(scores.Any())
            {
                int i = 1;
                foreach(var score in scores.OrderByDescending(s => s.Score))
                {
                    Console.WriteLine($"{i++,2} : {score.Player.Name,20} {score.Score,10}");
                }
            }
            else
            {
                    Console.WriteLine($"There are no players!");
            }
            Console.WriteLine($"------------------------------------");
        }
    }
}