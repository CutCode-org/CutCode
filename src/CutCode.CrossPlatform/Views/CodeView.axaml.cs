using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using CutCode.CrossPlatform.ViewModels;

namespace CutCode.CrossPlatform.Views
{
    public class CodeView : ReactiveUserControl<CodeViewModel>
    {
        public CodeView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}