using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Game.Lastoneout.Events;
using Game.Lastoneout.GameInfrastructure.AiPLayer;
using Game.Lastoneout.Services;
using Game.Shell.Services;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;

namespace Game.Shell.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        public DelegateCommand PvpToMainCommand { get; private set; }
        public DelegateCommand GoToPvpSetupCommand { get; private set; }
        public DelegateCommand GoToPvpcSetupCommand { get; private set; }
        public DelegateCommand ExitCommand { get; private set; }
        public DelegateCommand<object> StartCommand { get; private set; }

        private readonly IUserInfoService userInfo;

        public ShellViewModel(IUserInfoService userInfoService, IEventAggregator eventAggregator)
        {
            // Initialize this ViewModel's commands.
            ExitCommand = new DelegateCommand(Application.Current.Shutdown);
            PvpToMainCommand = new DelegateCommand(() => { ResetStates(); PvpToMain = true; });
            GoToPvpcSetupCommand = new DelegateCommand(() => { ResetStates(); IsAiPlayerGame = true; GoToPvpSetup = true; });
            GoToPvpSetupCommand = new DelegateCommand(() => { ResetStates(); GoToPvpSetup = true; });
            eventAggregator.GetEvent<ReturnEvent>().Subscribe(x => { ResetStates(); GameToStart = true; });
            StartCommand = new DelegateCommand<object>(StartAction, x => (!string.IsNullOrEmpty(Player1) && !string.IsNullOrEmpty(Player2) && !IsAiPlayerGame) || IsAiPlayerGame);
            
            userInfo = userInfoService;
            Player1 = userInfoService.GetUserName();
        }

        private void ResetStates()
        {
            PvpToMain = false; 
            GoToPvpSetup = false; 
            GameToStart = false; 
            GoToStart = false;
        }

        private string _profileImage;
        public string ProfileImage
        {
            get { return _profileImage; }
            set
            {
                SetProperty(ref _profileImage, value);
            }
        }

        private string _player1;
        public string Player1
        {
            get { return _player1; }
            set
            {
                SetProperty(ref _player1, value); 
                StartCommand.RaiseCanExecuteChanged();
            }
        }

        private string _player2;
        public string Player2
        {
            get { return _player2; }
            set
            {
                SetProperty(ref _player2, value);
                StartCommand.RaiseCanExecuteChanged();
            }
        }

        private AiPlayers _selectedAiPlayer;
        public AiPlayers SelectedAiPlayer
        {
            get { return _selectedAiPlayer; }
            set { SetProperty(ref _selectedAiPlayer, value); }
        }

        public IEnumerable<AiPlayers> AiPlayersValues
        {
            get
            {
                return Enum.GetValues(typeof(AiPlayers)).Cast<AiPlayers>();
            }
        }

        #region Start Action
        private void StartAction(object commandArg)
        {
            var gameService = ServiceLocator.Current.GetInstance<IGameService>();
            if (IsAiPlayerGame)
                gameService.Start(Player1, userInfo.GetUserProfileImagePath(), SelectedAiPlayer);
            else
                gameService.Start(Player1, Player2);
            ResetStates();
            GoToStart = true;
        }

        private bool _goToStart;

        public bool GoToStart
        {
            get { return _goToStart; }
            set { SetProperty(ref _goToStart, value); }
        }

        #endregion

        #region Navigation states
        private bool _goToPvpSetup;
        public bool GoToPvpSetup
        {
            get { return _goToPvpSetup; }
            set { SetProperty(ref _goToPvpSetup, value); }
        }

        private bool _isAiPlayerGame;
        public bool IsAiPlayerGame
        {
            get { return _isAiPlayerGame; }
            set
            {
                SetProperty(ref _isAiPlayerGame, value);
                StartCommand.RaiseCanExecuteChanged();
            }
        }

        private bool _gameToStart;
        public bool GameToStart
        {
            get { return _gameToStart; }
            set { SetProperty(ref _gameToStart, value); }
        }

        private bool _pvpToMain;
        public bool PvpToMain
        {
            get { return _pvpToMain; }
            set { SetProperty(ref _pvpToMain, value); }
        }
        #endregion
    }
}
