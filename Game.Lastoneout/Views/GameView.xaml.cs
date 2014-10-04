using System.Windows.Controls;
using Game.Lastoneout.ViewModels;

namespace Game.Lastoneout.Views
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : UserControl
    {
        public GameView(GameViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
