using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Game.Common.Helpers;
using Game.Lastoneout.GameInfrastructure;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Prism.Mvvm;

namespace Game.Lastoneout.ViewModels
{
    public class DeskViewModel : BindableBase
    {
        private int _state;

        private ObservableCollection<CoinViewModel> _coins;
        public ObservableCollection<CoinViewModel> Coins
        {
            get { return _coins; }
            set { SetProperty(ref _coins, value); }
        }

        public DeskViewModel(IGameService gameService)
        {
            Coins = new ObservableCollection<CoinViewModel>();

            gameService.Started += (sender, args) =>
            {
                _state = gameService.GetCount();
                if (!Coins.Any())
                    for (var i = 0; i < _state; i++)
                        Coins.Add(new CoinViewModel());

                _coins.ForEach(x =>
                {
                    x.Visible = true;
                    x.Blink = false;
                });
            };
            gameService.Updated += (sender, args) =>
            {
                var toHide = _state - gameService.GetCount();
                _state = gameService.GetCount();
                while (toHide > 0)
                {
                    var visible = _coins.Where(x => x.Visible).ToArray();
                    if (visible.Length > 0)
                    {
                        visible[RandomHelper.RandomNumber(0, visible.Length)].Visible = false;
                    }
                    toHide--;
                }
            };
            gameService.GameFinished += (sender, args) => _coins.ForEach(async x =>
            {
                await Task.Delay(RandomHelper.RandomNumber(50, 300));
                x.Blink = true;
            });
        }
    }
}
