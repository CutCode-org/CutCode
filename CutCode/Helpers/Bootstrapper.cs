using Stylet;
using StyletIoC;

namespace CutCode
{
    public class Bootstrapper : Bootstrapper<MainViewModel>
    {
        protected override void ConfigureIoC(IStyletIoCBuilder builder)
        {
            builder.Bind<IThemeService>().To<ThemeService>();
            // Configure the IoC container in here
        }

        protected override void Configure()
        {
            // Perform any other configuration before the application starts
        }
    }
}
