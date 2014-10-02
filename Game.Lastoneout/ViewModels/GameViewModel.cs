using System;
using System.Threading.Tasks;
using Game.Lastoneout.Events;
using Game.Lastoneout.Helpers;
using Game.Lastoneout.Services;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Game.Lastoneout.ViewModels
{
    public class GameViewModel : BindableBase
    {
        #region properties
        private bool _gameOver;
        public bool GameOver
        {
            get { return _gameOver; }
            set { SetProperty(ref _gameOver, value); }
        }
        
        private PlayerViewModel _player1;
        public PlayerViewModel Player1
        {
            get { return _player1; }
            set { SetProperty(ref _player1, value); }
        }

        private PlayerViewModel _player2;
        public PlayerViewModel Player2
        {
            get { return _player2; }
            set { SetProperty(ref _player2, value); }
        }

        private int _count;
        public int Count
        {
            get { return _count; }
            set { SetProperty(ref _count, value); }
        }

        private bool _gameActive;
        public bool GameActive
        {
            get { return _gameActive; }
            set { SetProperty(ref _gameActive, value); }
        }

        private string _gameOverText;
        public string GameOverText
        {
            get { return _gameOverText; }
            set { SetProperty(ref _gameOverText, value); }
        }
        #endregion

        private readonly IGameService _gameService;

        public GameViewModel(IGameService gameService, IEventAggregator eventAggregator)
        {
            _gameService = gameService;
            Player1 = new PlayerViewModel(gameService, eventAggregator);
            Player2 = new PlayerViewModel(gameService, eventAggregator);

            eventAggregator.GetEvent<EndTurnEvent>().Subscribe(hash =>
            {
                if (!GameActive)
                    return;
                Player1.IsActive = !Player1.IsActive;
                Player2.IsActive = !Player2.IsActive;
            }
            , true);

            _gameService.Started += async (sender, args) =>
            {
                await Task.Delay(TimeSpan.FromMilliseconds(2000));
                var whoIsFirst = RandomHelper.FlipACoin();
                Player1.IsActive = whoIsFirst;
                Player2.IsActive = !whoIsFirst;
                GameActive = true;
            };

            _gameService.Updated += (sender, args) =>
            {
                Player1.PlayerName = gameService.Player1Name;
                Player2.PlayerName = gameService.Player2Name;
                Count = _gameService.GetCount();
            };

            _gameService.GameFinished += (sender, args) =>
            {
                GameActive = false;
                var winner = Player2.IsActive ? Player1 : Player2;
                GameOverText = string.Format("Congratulations {0}!\nYou've just won!", winner.PlayerName);
                GameOver = true;
                Player1.IsActive = Player2.IsActive = false;
            };

            Player1.PlayerName = _gameService.Player1Name;
            Player2.PlayerName = _gameService.Player2Name;
            
        }

    }
}
