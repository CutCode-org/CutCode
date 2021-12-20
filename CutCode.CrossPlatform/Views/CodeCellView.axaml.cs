using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace a
{
    public class CodeCellView_axaml : UserControl
    {
        public CodeCellView_axaml()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}