using System;
using System.Threading.Tasks;
using Game.Common.Helpers;
using Game.Lastoneout.Events;
using Game.Lastoneout.GameInfrastructure;
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

        private DeskViewModel _desk;
        public DeskViewModel Desk
        {
            get { return _desk; }
            set { SetProperty(ref _desk, value); }
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
        
        public DelegateCommand RestartCommand { get; private set; }
        public DelegateCommand ReturnCommand { get; private set; }

        public GameViewModel(IGameService gameService, IEventAggregator eventAggregator)
        {
            Player1 = new PlayerViewModel(eventAggregator);
            Player2 = new PlayerViewModel(eventAggregator);
            Desk = new DeskViewModel(gameService);

            gameService.Started += async (sender, args) =>
            {
                GameOver = false;
                Player1.PlayerName = gameService.Player1Name;
                Player2.PlayerName = gameService.Player2Name;
                Count = gameService.GetCount();

                Player2.IsAiPlayer = gameService.IsAiGame;
                Player1.ImageSource = gameService.GetPlayerImage();
                Player2.ImageSource = gameService.GetAiPlayerImage();
                UpdatePlayersState(gameService);
                await ChoosingPlayerDelay();
                var whoIsFirst = RandomHelper.FlipACoin();
                Player1.IsActive = whoIsFirst;
                Player2.IsActive = !whoIsFirst;
                GameActive = true;
                if (Player2.IsActive)
                    await AiTurn(gameService);
            };

            eventAggregator.GetEvent<EndTurnEvent>().Subscribe(async step =>
            {
                if (!GameActive)
                    return;
                gameService.EndTurn(step);
                await AiTurn(gameService);
            }
            , true);

            gameService.Updated += (sender, args) =>
            {
                Count = gameService.GetCount();
                if (Count > 0)
                {
                    Player1.IsActive = !Player1.IsActive;
                    Player2.IsActive = !Player2.IsActive;
                    UpdatePlayersState(gameService);
                }
            };

            gameService.GameFinished += (sender, args) =>
            {
                GameActive = false;
                var winner = Player2.IsActive ? Player1 : Player2;
                Player1.IsActive = Player2.IsActive = false;
                GameOverText = string.Format("Congratulations {0}!\nYou've just won!", winner.PlayerName);
                GameOver = true;
            };

            RestartCommand = new DelegateCommand(gameService.Reset);
            ReturnCommand = new DelegateCommand(() => eventAggregator.GetEvent<ReturnEvent>().Publish(null));
        }

        private async Task AiTurn(IGameService gameService)
        {
            if (gameService.IsAiGame && _count > 0)
            {
                await Task.Delay(gameService.GetAiPlayerDelay());
                gameService.EndTurn(gameService.AiPlayerStep(_count));
            }
        }

        private void UpdatePlayersState(IGameService gameService)
        {
            Player1.Show3Toggle = Player2.Show3Toggle = Count >= 3;
            Player1.Show2Toggle = Player2.Show2Toggle = Count >= 2;
        }

        private async Task ChoosingPlayerDelay()
        {
            ChoosingPlayer = true;
            await Task.Delay(TimeSpan.FromMilliseconds(2000));
            ChoosingPlayer = false;
        }
    }
}
