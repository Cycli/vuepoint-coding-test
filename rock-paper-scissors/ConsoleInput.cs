using System;
namespace RockPaperScissors
{
    class ConsoleInput : IInput
    {
        public char Read()
        {
            var keyInfo = Console.ReadKey();
            return keyInfo.KeyChar;
        }
    }
}