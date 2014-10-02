using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Lastoneout.Services
{
    class LastoneoutGameService : IGameService
    {
        private int _count;

        public event EventHandler GameFinished;
        public event EventHandler Updated;
        public event EventHandler Started;

        protected virtual void OnStarted()
        {
            var handler = Started;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public string Player1Name { get; private set; }
        public string Player2Name { get; private set; }

        protected virtual void OnUpdated()
        {
            var handler = Updated;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        protected virtual void OnGameFinished()
        {
            var handler = GameFinished;
            if (handler != null) handler(this, EventArgs.Empty);
        }
        
        public bool EndTurn(int step)
        {
            var canEndTurn = false;
            var newCount = _count - step;
            if (newCount >= 0)
            {
                canEndTurn = true;
                _count = newCount;
                OnUpdated();
                if (_count == 0)
                    OnGameFinished();
            }
            return canEndTurn;
        }

        public void Start(string player1Name, string player2Name)
        {
            Player1Name = player1Name;
            Player2Name = player2Name;
            Reset();
        }

        public void SetAILevel(byte level)
        {
            //throw new NotImplementedException();
        }

        public void Reset()
        {
            _count = 20;
            OnStarted();
            OnUpdated();
        }

        public int GetCount()
        {
            return _count;
        }
    }
}
