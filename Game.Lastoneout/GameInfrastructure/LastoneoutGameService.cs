using System;
using Game.Lastoneout.GameInfrastructure.AiPLayer;

namespace Game.Lastoneout.GameInfrastructure
{
    class LastoneoutGameService : IGameService
    {
        private int _count;
        private IAiPlayer _aiPlayer;
        private string _playerImageSource;
        private readonly IAiPlayerProvider _aiPlayerProvider;

        public event EventHandler GameFinished;
        public event EventHandler Updated;
        public event EventHandler Started;

        public string Player1Name { get; private set; }
        public string Player2Name { get; private set; }

        public LastoneoutGameService(IAiPlayerProvider aiPlayerProvider)
        {
            _aiPlayerProvider = aiPlayerProvider;
        }

        protected virtual void OnStarted()
        {
            var handler = Started;
            if (handler != null) handler(this, EventArgs.Empty);
        }
        
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
            IsAiGame = false;
            Reset();
        }
        
        public void Start(string playerName, string playerImageSource, AiPlayers aiPlayer)
        {
            Player1Name = playerName;
            _aiPlayer = _aiPlayerProvider.GetAiPlayer(aiPlayer);
            Player2Name = _aiPlayer.Name;
            _playerImageSource = playerImageSource;
            IsAiGame = true;
            Reset();
        }

        public string GetPlayerImage()
        {
            return _playerImageSource;
        }

        public string GetAiPlayerImage()
        {
            return _aiPlayer.ImageSource;
        }

        public string GetAiPlayerMessage()
        {
            return _aiPlayer.GetMessage();
        }

        public TimeSpan GetAiPlayerDelay()
        {
            return _aiPlayer.GetDelay();
        }

        public int AiPlayerStep(int state)
        {
            return _aiPlayer.GetMove(state);
        }

        public bool IsAiGame { get; private set; }

        public void Reset()
        {
            _count = 20;
            OnStarted();
        }

        public int GetCount()
        {
            return _count;
        }
    }
}
