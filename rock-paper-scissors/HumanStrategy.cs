using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    // A strategy implementation that supports external gesture choice 
    class HumanStrategy : IStrategy
    {
        IInput _Input;

        public HumanStrategy(IInput input)
        {
            _Input = input;
        }

        public Task<HandGesture> GetGesture()
        {
            Console.WriteLine("Pick a gesture - R = Rock, P = Paper, S = Scissors");
            HandGesture? gesture = null;
            while(gesture is null)
            {
                var key = _Input.Read();
                switch(key)
                {
                    case 'R':
                    case 'r':
                        gesture = HandGesture.Rock;
                    break;
                    case 'P':
                    case 'p':
                        gesture = HandGesture.Paper;
                    break;
                    case 'S':
                    case 's':
                        gesture = HandGesture.Scissors;
                    break;
                }
            }
            return Task.FromResult(gesture.Value);
        }

        public async Task Learn(IPlayer me, IEnumerable<Turn> turns)
        {
            // Noop - humans do not learn
            await Task.CompletedTask;
        }
    }
}