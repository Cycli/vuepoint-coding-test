using System.Collections.Generic;

namespace RockPaperScissors
{
    // The interface for classes displaying player turn information.
    interface ITurnView
    {
        void View(IEnumerable<Turn> turns);
    }
}