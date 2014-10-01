using System;
using System.Windows;
using Game.Lastoneout.Services;
using Game.Shell.Services;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.ServiceLocation;

namespace Game.Shell.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        public DelegateCommand GoToPvpSetupCommand { get; private set; }
        public DelegateCommand ExitCommand { get; private set; }
        public DelegateCommand<object> StartCommand { get; private set; }

        public ShellViewModel(IUserInfoService userInfoService)
        {
            // Initialize this ViewModel's commands.
            ExitCommand = new DelegateCommand(Application.Current.Shutdown);
            GoToPvpSetupCommand = new DelegateCommand(() => GoToPvpSetup = true);
            StartCommand = new DelegateCommand<object>(StartAction, x => !string.IsNullOrEmpty(Player1) && !string.IsNullOrEmpty(Player2));

            Player1 = userInfoService.GetUserName();
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
            GoToStart = true;
        }

        private bool _goToStart;

        public bool GoToStart
        {
            get { return _goToStart; }
            set { SetProperty(ref _goToStart, value); }
        }

        #endregion

        #region Pvp click
        private bool _goToPvpSetup;

        public bool GoToPvpSetup
        {
            get { return _goToPvpSetup; }
            set { SetProperty(ref _goToPvpSetup, value); }
        }
        #endregion
    }
}
