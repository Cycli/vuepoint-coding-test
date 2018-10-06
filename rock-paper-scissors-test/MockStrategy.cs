using System.Collections.Generic;
using System.Threading.Tasks;
using RockPaperScissors;

namespace RockPaperScissorsTest
{
    // A basic strategy mock class that just issues the same gesture each play
    class MockStrategy : IStrategy
    {
        readonly HandGesture _Gesture;
        public MockStrategy(HandGesture gesture)
        {
            _Gesture = gesture;
        }
        public Task<HandGesture> GetGesture() => Task.FromResult(_Gesture);

        public Task Learn(IPlayer me, IEnumerable<Turn> turns) => Task.CompletedTask;
    }
}