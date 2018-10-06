namespace RockPaperScissors
{
    // A basic data object holding plater turn information.
    class Turn
    {
        public readonly IPlayer Player;
        public readonly HandGesture Gesture;

        public Turn(IPlayer player, HandGesture gesture)
        {
            Player = player;
            Gesture = gesture;
        }

    }
}