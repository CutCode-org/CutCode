using Autofac;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CutCode.CrossPlatform.Interfaces;
using CutCode.CrossPlatform.ViewModels;
using CutCode.CrossPlatform.Views;
using Splat;

namespace CutCode.CrossPlatform
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var databaseService = new DataBase();
                var themeService = new ThemeService(databaseService);
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(themeService),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}