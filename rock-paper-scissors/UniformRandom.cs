using System;

namespace RockPaperScissors
{
    // A IRandom implementation that generates pseudo-random, uniformly distributed integer and double 
    // distributions
    class UniformRandom : IRandom
    {
        readonly Random _Random;
        public UniformRandom()
        {
            _Random = new Random();
        }
        public UniformRandom(int seed)
        {
            _Random = new Random(seed);
        }
        public int Get(int maximum) => _Random.Next(maximum);
        public double Get(double maximum) => maximum * _Random.NextDouble();
    }
}