using System.Windows;
using Game.Lastoneout.Events;
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
        public DelegateCommand ExitCommand { get; private set; }
        public DelegateCommand<object> StartCommand { get; private set; }

        public ShellViewModel(IUserInfoService userInfoService, IEventAggregator eventAggregator)
        {
            // Initialize this ViewModel's commands.
            ExitCommand = new DelegateCommand(Application.Current.Shutdown);
            PvpToMainCommand = new DelegateCommand(() => { ResetStates(); PvpToMain = true; });
            GoToPvpSetupCommand = new DelegateCommand(() => { ResetStates(); GoToPvpSetup = true; });
            eventAggregator.GetEvent<ReturnEvent>().Subscribe(x => { ResetStates(); GameToStart = true; });
            StartCommand = new DelegateCommand<object>(StartAction, x => !string.IsNullOrEmpty(Player1) && !string.IsNullOrEmpty(Player2));

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

        #region Start Action
        private void StartAction(object commandArg)
        {
            var gameService = ServiceLocator.Current.GetInstance<IGameService>();
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
