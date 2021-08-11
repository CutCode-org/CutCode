using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CutCode.MultiPlatform.ViewModels;
using CutCode.MultiPlatform.Views;
using Live.Avalonia;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Reactive;

namespace CutCode.MultiPlatform
{
    public class App : Application, ILiveView
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (Debugger.IsAttached || IsProduction())
            {
                var window = new Window();
                window.Content = CreateView(window);
                window.Show();
            }
            else
            {
                var window = new LiveViewHost(this, Console.WriteLine);
                window.StartWatchingSourceFilesForHotReloading();
                window.Show();
            }
            RxApp.DefaultExceptionHandler = Observer.Create<Exception>(Console.WriteLine);
            base.OnFrameworkInitializationCompleted();
        }
        public object CreateView(Window window)
        {
            window.DataContext ??= new ViewModelBase();
            return new 
        }

        private static bool IsProduction()
        {
#if DEBUG
            return false;
#else
        return true;
#endif
        }
    }

}
