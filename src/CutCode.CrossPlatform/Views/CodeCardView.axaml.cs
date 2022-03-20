using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace CutCode.CrossPlatform.Views
{
    public class CodeCardView : UserControl
    {
        public CodeCardView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}