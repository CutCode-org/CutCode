using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CutCode.CrossPlatform.ViewModels;

namespace CutCode.CrossPlatform.Views
{
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            DataContext = new HomeViewModel();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}