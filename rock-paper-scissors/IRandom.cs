namespace RockPaperScissors
{
    // The interface for rando number generation
    interface IRandom
    {
        int Get(int maximum);
        double Get(double maximum);
    }
}