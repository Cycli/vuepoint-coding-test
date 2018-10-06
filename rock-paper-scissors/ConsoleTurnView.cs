using System;
using System.Collections.Generic;
using System.Linq;

namespace RockPaperScissors
{
    // A basic implementation of the ITurnView interface that provides basic
    // output of each player's turn
    class ConsoleTurnView : ITurnView
    {
        readonly static string NAME_HEADER = "PLAYER";
        readonly static string SHAPE_HEADER = "SHAPE";
        public void View(IEnumerable<Turn> turns)
        {
            Console.WriteLine($"     {NAME_HEADER,20} {SHAPE_HEADER,10}");
            if(turns.Any())
            {
                foreach(var turn in turns.OrderBy(s => s.Player.Name))
                {
                    Console.WriteLine($"     {turn.Player.Name,20} {Enum.GetName(typeof(HandGesture), turn.Gesture),10}");
                }
            }
            else
            {
                    Console.WriteLine($"There are no turns!");
            }
            Console.WriteLine($"------------------------------------");
        }        
    }
}