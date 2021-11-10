using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace CutCode.CrossPlatform.Views
{
    public class HomeView : Window
    {
        public HomeView()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}