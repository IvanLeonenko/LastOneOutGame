using Microsoft.Practices.Prism.Mvvm;

namespace Game.Lastoneout.ViewModels
{
    public class CoinViewModel : BindableBase
    {
        private bool _visible;
        public bool Visible
        {
            get { return _visible; }
            set { SetProperty(ref _visible, value); }
        }

        private bool _blink;
        public bool Blink
        {
            get { return _blink; }
            set { SetProperty(ref _blink, value); }
        }
    }
}
