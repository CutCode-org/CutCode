using System;
using System.Diagnostics;
using Avalonia;
using Avalonia.Logging;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ReactiveUI;
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
            var isRunningInAnotherInstance = Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Length > 1;
            if(!isRunningInAnotherInstance)
                BuildAvaloniaApp()
                    .StartWithClassicDesktopLifetime(args);
        }
        
        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI();
    }
}
