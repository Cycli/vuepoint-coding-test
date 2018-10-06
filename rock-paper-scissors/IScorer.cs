using System.Collections.Generic;

namespace RockPaperScissors
{
    interface IScorer
    {
        void Start(IEnumerable<IPlayer> players);
        void Update(IEnumerable<Turn> turns);  
        IEnumerable<PlayerScore> Scores { get; }  
    }
}