using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
namespace RockPaperScissors
{
    // A simple learning strategy that looks back at a limited recent play history and
    // picks a gesture based on a Monte Carlo-like selection.
    class LearningStrategy : IStrategy
    {
        List<HandGesture> _Frequency;
        IRandom _Random;
        static int _HistoryLookback = 500;
        // To learn, we need to know the rules
        static readonly Dictionary<HandGesture, HandGesture> _WinningGestures = new Dictionary<HandGesture, HandGesture>()
        {
            {HandGesture.Paper, HandGesture.Scissors},
            {HandGesture.Rock, HandGesture.Paper},
            {HandGesture.Scissors, HandGesture.Rock},
        };

        public LearningStrategy(IRandom random)
        {
            // To avoid having to check for an empty frequency later, seed the list with one instance
            // each of the gestures
            _Frequency = new List<HandGesture>()
            {
                HandGesture.Paper,
                HandGesture.Scissors,
                HandGesture.Rock
            };
            _Random = random;
        }

        public async Task<HandGesture> GetGesture()
        {
            return await Task.Factory.StartNew<HandGesture>(() => {
                var idx = _Random.Get(_Frequency.Count());
                var likelyGesture = _Frequency.ElementAt(idx);
                return _WinningGestures[likelyGesture];
            });
        }

         public async Task Learn(IPlayer me, IEnumerable<Turn> turns)
        {
            await Task.Factory.StartNew(() => {
                // Don't include my turns in the learning
                var otherTurns = turns.Where(t => !ReferenceEquals(me, t.Player)).Select(t => t.Gesture);
                _Frequency.AddRange(otherTurns);

                // Has the list become too lon - if so, clear out the oldest elements.
                var frequencyCount = _Frequency.Count();
                if(frequencyCount > _HistoryLookback)
                {
                    _Frequency.RemoveRange(0,frequencyCount - _HistoryLookback);
                }
            });
        }
    }
}