using System;
using System.Diagnostics;
using Avalonia;
using Avalonia.Logging;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ReactiveUI;
using CutCode.CrossPlatform.ViewModels;
using CutCode.CrossPlatform.Views;
using ReactiveUI;
using Splat;

namespace CutCode.CrossPlatform
{
    class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            var isRunningInAnotherInstance =
                Process.GetProcessesByName(
                        System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly()
                            .Location))
                    .Length > 1;
            if (!isRunningInAnotherInstance)
                BuildAvaloniaApp()
                    .StartWithClassicDesktopLifetime(args);
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
        {
            Locator.CurrentMutable.Register(() => new HomeView(), typeof(IViewFor<HomeViewModel>));
            Locator.CurrentMutable.Register(() => new AddView(), typeof(IViewFor<AddViewModel>));
            return AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI();
        }
    }
}