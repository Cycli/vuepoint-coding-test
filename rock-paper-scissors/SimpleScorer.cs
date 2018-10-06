using System;
using System.Collections.Generic;
using System.Linq;

namespace RockPaperScissors
{
    // An IScorer implementation that assigns 1 point for each winning gesture in a turn. For example, in a game
    // with 5 players, if the first player uses the gesture 'Rock' and three other players play 'Scissors',
    // then the player will score 3 points for that turn.
    class SimpleScorer : IScorer
    {
        static readonly Dictionary<HandGesture, HandGesture> _ScoringGestures = new Dictionary<HandGesture, HandGesture>()
        {
            {HandGesture.Paper, HandGesture.Rock},
            {HandGesture.Rock, HandGesture.Scissors},
            {HandGesture.Scissors, HandGesture.Paper},
        };
        IEnumerable<PlayerScore> _PlayerScores;

        public IEnumerable<PlayerScore> Scores => _PlayerScores;

        public void Start(IEnumerable<IPlayer> players) => _PlayerScores = players.Select(player => new PlayerScore(player, 0));

        public void Update(IEnumerable<Turn> turns)
        {
            _PlayerScores = _PlayerScores.Select(p => {

                // Find this player's turn. We assume that every player makes a turn
                var turn = turns.Single(t => ReferenceEquals(t.Player, p.Player));
                
                // What does this player's turn win against
                var winAgainstGesture = _ScoringGestures[turn.Gesture];

                // Find how many turns have been played where the gesture will win
                var wins = turns.Count(u => u.Gesture == winAgainstGesture);
                // Return a new score
                return new PlayerScore(p.Player, p.Score + wins);
            });
        }

        
    }
}