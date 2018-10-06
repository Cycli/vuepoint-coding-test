using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace RockPaperScissors
{
    // A basic implementation of the IPlayer interface 
    class SimplePlayer : IPlayer
    {
        readonly string _Name;
        readonly IStrategy _Strategy;
        public SimplePlayer(string name, IStrategy strategy)
        {
            _Name = name;
            _Strategy = strategy;
        }
        public string Name => _Name;

        public async Task<HandGesture> GetGesture() => await _Strategy.GetGesture();

        public async Task Learn(IEnumerable<Turn> turns) => await _Strategy.Learn(this, turns);
    }
}