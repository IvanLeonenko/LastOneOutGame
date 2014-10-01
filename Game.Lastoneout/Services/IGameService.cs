using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        void SetAILevel(byte level);
        void Reset();
        int GetCount();
    }
}
