using System;
using Game.Lastoneout.Events;
using Game.Lastoneout.Services;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Game.Lastoneout.ViewModels
{
    public class PlayerViewModel : BindableBase
    {
        public DelegateCommand<object> ButtonListCommand { get; private set; }
        public DelegateCommand<object> EndTurnCommand { get; private set; }

        public PlayerViewModel(IGameService gameService, IEventAggregator eventAggregator)
        {
            SelectedInd = -1;

            ButtonListCommand = new DelegateCommand<object>((step) =>
            {
                SetStep(Convert.ToInt32(step));
            });

            EndTurnCommand = new DelegateCommand<object>((x) =>
            {
                if (!gameService.EndTurn(_step)) 
                    return;
                SelectedInd = -1;
                SetStep(0);
                eventAggregator.GetEvent<EndTurnEvent>().Publish(this.GetHashCode());
            }, (x) => _step > 0 && _step <= 3);

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
    }
}
