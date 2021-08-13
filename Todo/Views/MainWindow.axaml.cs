using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Todo.ViewModels;

namespace Todo.Views
{
    public sealed class MainWindow : ReactiveUserControl<MainWindowViewModel>
    {
        public MainWindow()
        {
            AvaloniaXamlLoader.Load(this);
            this.WhenActivated(disposables => { });
        }
    }
}
