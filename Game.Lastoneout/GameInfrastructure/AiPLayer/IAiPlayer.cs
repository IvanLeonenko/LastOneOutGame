using System;

namespace Game.Lastoneout.GameInfrastructure.AiPLayer
{
    public interface IAiPlayer
    {
        string Name { get; }
        string ImageSource { get; }
        string GetMessage();
        TimeSpan GetDelay();
        int GetMove(int state);
    }
}
