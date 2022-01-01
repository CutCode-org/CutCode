using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace CutCode.CrossPlatform.Views
{
    public class NotificationView : UserControl
    {
        public NotificationView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}