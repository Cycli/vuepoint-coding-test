using System.Collections.Generic;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    // The game interface, defining how game rounds are run and when the game is finished.
    interface IGame
    {
        Task<IEnumerable<Turn>> RunRound();
        bool IsFinished();

    }
}