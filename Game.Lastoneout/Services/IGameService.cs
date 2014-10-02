using System;
using Game.Lastoneout.GameInfrastructure.AiPLayer;

namespace Game.Lastoneout.Services
{
    public interface IGameService
    {
        event EventHandler GameFinished;
        event EventHandler Updated;
        event EventHandler Started;
        string Player1Name { get; }
        string Player2Name { get; }
        bool EndTurn(int number);
        void Start(string player1Name, string player2Name);
        void Start(string playerName, string playerImageSource, AiPlayers aiPlayer);
        string GetPlayerImage();
        string GetAiPlayerImage();
        string GetAiPlayerMessage();
        TimeSpan GetAiPlayerDelay();
        int AiPlayerStep(int state);
        bool IsAiGame { get; }
        void Reset();
        int GetCount();
    }
}
