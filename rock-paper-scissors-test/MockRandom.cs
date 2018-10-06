using RockPaperScissors;
namespace RockPaperScissorsTest
{
    class MockRandom : IRandom
    {
        int _IntValue;
        double _DoubleValue;
        public void Set(int value) => _IntValue = value;
        public void Set(double value) => _DoubleValue = value;
        public int Get(int maximum) => _IntValue;
        public double Get(double maximum) => _DoubleValue;
    }
}