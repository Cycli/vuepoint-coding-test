using System.Collections.Generic;

namespace RockPaperScissors
{
    // The interface for displaying the player scores.
    interface IScoreView
    {
        void View(IEnumerable<PlayerScore> scores);
    }
}