using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace RockPaperScissors
{
    // A simple implementation of the IGame interface with asynchronous turns and learning
    class Game : IGame
    {
        uint _Rounds = 0;
        uint _Round = 0;
        readonly IEnumerable<IPlayer> _Players;

        readonly IScorer _Scorer;
        public Game(IEnumerable<IPlayer> players, IScorer scorer, uint rounds)
        {            
            _Players = players;
            _Scorer = scorer;
            // Initialise the players
            _Scorer.Start(_Players);
            _Rounds = rounds;
            _Round = 0;
        }
        public async Task<IEnumerable<Turn>> RunRound()
        {
            _Round++;
            var turns = await Task.WhenAll(_Players.Select(async p => new Turn(p, await p.GetGesture())));
            _Scorer.Update(turns);

            await Task.WhenAll(_Players.Select(async p => await p.Learn(turns)));
            return turns;
        }
        public bool IsFinished() => _Round == _Rounds;

    }
}