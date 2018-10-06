using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace RockPaperScissors
{
    // The player interface, defining the contract for implmenting player gestures and learning.
    interface IPlayer
    {
        string Name { get; }
        Task<HandGesture> GetGesture();
        Task Learn(IEnumerable<Turn> turns);

    }
}