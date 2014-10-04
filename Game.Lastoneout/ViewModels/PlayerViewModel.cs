using System;
using Game.Lastoneout.Events;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Game.Lastoneout.ViewModels
{
    public class PlayerViewModel : BindableBase
    {
        public DelegateCommand<object> ButtonListCommand { get; private set; }
        public DelegateCommand<object> EndTurnCommand { get; private set; }

        public PlayerViewModel(IEventAggregator eventAggregator)
        {
            SelectedInd = -1;

            Show2Toggle = Show3Toggle = true;
            
            ButtonListCommand = new DelegateCommand<object>((step) => SetStep(Convert.ToInt32(step)));

            EndTurnCommand = new DelegateCommand<object>((x) =>
            {
                eventAggregator.GetEvent<EndTurnEvent>().Publish(_step);
                SelectedInd = -1;
                SetStep(0);
            }, (x) => _step > 0 && _step <= 3);
        }

        private bool _isAiPlayer;
        public bool IsAiPlayer
        {
            get { return _isAiPlayer; }
            set { SetProperty(ref _isAiPlayer, value); }
        }

        private int _step;
        private void SetStep(int step)
        {
            _step = step;
            EndTurnCommand.RaiseCanExecuteChanged();
        }
        
        private string _playerName;
        public string PlayerName
        {
            get { return _playerName; }
            set { SetProperty(ref _playerName, value); }
        }

        private string _imageSource;
        public string ImageSource
        {
            get { return _imageSource; }
            set { SetProperty(ref _imageSource, value); }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private int _selectedInd;
        public int SelectedInd
        {
            get { return _selectedInd; }
            set { SetProperty(ref _selectedInd, value); }
        }

        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { SetProperty(ref _isActive, value); }
        }

        private bool _show2Toggle;
        public bool Show2Toggle
        {
            get { return _show2Toggle; }
            set { SetProperty(ref _show2Toggle, value); }
        }

        private bool _show3Toggle;
        public bool Show3Toggle
        {
            get { return _show3Toggle; }
            set { SetProperty(ref _show3Toggle, value); }
        }
    }
}
