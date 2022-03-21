using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using CutCode.CrossPlatform.ViewModels;

namespace CutCode.CrossPlatform.Views
{
    public class AddView : ReactiveUserControl<AddViewModel>
    {
        public AddView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}