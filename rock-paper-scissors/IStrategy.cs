using System.Collections.Generic;
using System.Threading.Tasks;
namespace RockPaperScissors
{
    // The interface for implementations of game strategy, including deriving a gesture and learning.
    // To add a bit of interest these have been defined as asynchronous functions. This will
    // allow all 'players' to deliberate on their next move at the same time (subject to
    // thread availability). This thinking time could be simulated in each
    // strategy using a Thread Sleep
    interface IStrategy
    {
        Task<HandGesture> GetGesture();
        Task Learn(IPlayer me, IEnumerable<Turn> turns);
    }
}