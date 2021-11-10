using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace CutCode.CrossPlatform.Views
{
    public class FavoritesView : UserControl
    {
        public FavoritesView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}