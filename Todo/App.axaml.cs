using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Live.Avalonia;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Reactive;
using Todo.Services;
using Todo.ViewModels;
using Todo.Views;

namespace Todo
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
                // Debugging requires pdb loading etc, so we disable live reloading
                // during a test run with an attached debugger.
                var window = new Window();
                window.Content = CreateView(window);
                window.Show();
            }
            else
            {
                // Here, we create a new LiveViewHost, located in the 'Live.Avalonia'
                // namespace, and pass an ILiveView implementation to it. The ILiveView
                // implementation should have a parameterless constructor! Next, we
                // start listening for any changes in the source files. And then, we
                // show the LiveViewHost window. Simple enough, huh?
                var window = new LiveViewHost(this, Console.WriteLine);
                window.StartWatchingSourceFilesForHotReloading();
                window.Show();
            }

            // Here we subscribe to ReactiveUI default exception handler to avoid app
            // termination in case if we do something wrong in our view models. See:
            // https://www.reactiveui.net/docs/handbook/default-exception-handler/
            //
            // In case if you are using another MV* framework, please refer to its 
            // documentation explaining global exception handling.
            RxApp.DefaultExceptionHandler = Observer.Create<Exception>(Console.WriteLine);
            base.OnFrameworkInitializationCompleted();
        }

        public object CreateView(Window window)
        {
            var db = new Database();
            window.DataContext ??= new MainWindowViewModel(db);
            return new MainWindow();

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
