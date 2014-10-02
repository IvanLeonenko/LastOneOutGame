using System;
using System.Threading.Tasks;
using Game.Common.Helpers;
using Game.Lastoneout.Events;
using Game.Lastoneout.Services;
using Microsoft.Practices.Prism.Commands;
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
        private bool _choosingPlayer;
        public bool ChoosingPlayer
        {
            get { return _choosingPlayer; }
            set { SetProperty(ref _choosingPlayer, value); }
        }
        #endregion

        private readonly IGameService _gameService;

        public DelegateCommand RestartCommand { get; private set; }
        public DelegateCommand ReturnCommand { get; private set; }

        public GameViewModel(IGameService gameService, IEventAggregator eventAggregator)
        {
            _gameService = gameService;
            Player1 = new PlayerViewModel(gameService, eventAggregator);
            Player2 = new PlayerViewModel(gameService, eventAggregator);

            eventAggregator.GetEvent<EndTurnEvent>().Subscribe(async hash =>
            {
                if (!GameActive)
                    return;
                Player1.IsActive = !Player1.IsActive;
                Player2.IsActive = !Player2.IsActive;
                await AiTurn();
            }
            , true);

            _gameService.Started += async (sender, args) =>
            {
                GameOver = false;
                Player2.IsAiPlayer = _gameService.IsAiGame;
                await ChoosingPlayerDelay();
                var whoIsFirst = RandomHelper.FlipACoin();
                Player1.IsActive = whoIsFirst;
                Player2.IsActive = !whoIsFirst;
                GameActive = true;
                await AiTurn();
            };

            _gameService.Updated += (sender, args) =>
            {
                UpdatePlayersData();
                Count = _gameService.GetCount();
            };

            _gameService.GameFinished += (sender, args) =>
            {
                GameActive = false;
                var winner = Player2.IsActive ? Player1 : Player2;
                GameOverText = string.Format("Congratulations {0}!\nYou've just won!", winner.PlayerName);
                Player1.IsActive = false;
                Player2.IsActive = false;
                GameOver = true;
            };

            RestartCommand = new DelegateCommand(() => _gameService.Reset());
            ReturnCommand = new DelegateCommand(() => eventAggregator.GetEvent<ReturnEvent>().Publish(null));

            UpdatePlayersData();
        }

        private async Task AiTurn()
        {
            if (_gameService.IsAiGame && _count > 0)
            {
                await Task.Delay(_gameService.GetAiPlayerDelay());
                if (_gameService.EndTurn(_gameService.AiPlayerStep(_count)))
                {
                    Player1.IsActive = !Player1.IsActive;
                    Player2.IsActive = !Player2.IsActive;
                }
            }
        }

        private void UpdatePlayersData()
        {
            Player1.PlayerName = _gameService.Player1Name;
            Player2.PlayerName = _gameService.Player2Name;
        }

        private async Task ChoosingPlayerDelay()
        {
            ChoosingPlayer = true;
            await Task.Delay(TimeSpan.FromMilliseconds(2000));
            ChoosingPlayer = false;
        }
    }
}
