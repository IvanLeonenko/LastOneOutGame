using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Game.Lastoneout.ViewModels
{
    public class DeskViewModel : BindableBase
    {
        private ObservableCollection<CoinViewModel> _coins;
        public ObservableCollection<CoinViewModel> Coins
        {
            get { return _coins; }
            set { SetProperty(ref _coins, value); }
        }

        public DeskViewModel(IEventAggregator eventAggregator)
        {
            Coins = new ObservableCollection<CoinViewModel>();
            for (var i = 0; i < 20; i++)
            {
                Coins.Add(new CoinViewModel());
            }

            //eventAggregator
        }
    }
}
