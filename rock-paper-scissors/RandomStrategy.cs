using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace RockPaperScissors
{
    // An implementation of playing strategy that, assuming the associated random number generator produces unfiformly
    // distributed random numbers, picks a gesture completely at random.
    class RandomStrategy : IStrategy
    {
        readonly IRandom _Random;
        public RandomStrategy(IRandom random)
        {
            _Random = random;
        }
        public async Task<HandGesture> GetGesture()
        {
            return await Task.Factory.StartNew<HandGesture>(() => {
                // Fake some thinking time
                var r = _Random.Get(3);
                return (HandGesture)r;
            });            
        }

        public async Task Learn(IPlayer me, IEnumerable<Turn> turns)
        {
            // Noop - this algorithm does not learn
            await Task.CompletedTask;
        }
    }
}