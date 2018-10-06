using RockPaperScissors;

namespace RockPaperScissorsTest
{
    class MockInput : IInput
    {
        readonly char _Key;
        public MockInput(char key)
        {
            _Key = key;
        }
        public char Read() => _Key;
    }
}