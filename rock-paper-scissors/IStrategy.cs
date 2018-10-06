using System.Collections.Generic;
using System.Threading.Tasks;
namespace RockPaperScissors
{
    // The interface for implementations of game strategy, including deriving a gesture and learning.
    interface IStrategy
    {
        Task<HandGesture> GetGesture();
        Task Learn(IPlayer me, IEnumerable<Turn> turns);
    }
}