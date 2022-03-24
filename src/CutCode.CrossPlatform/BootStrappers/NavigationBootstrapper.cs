// ---------------------------------------------
//      --- CutCode.CrossPlatform by Scarementus ---
//      ---        Licence MIT       ---
// ---------------------------------------------

using CutCode.CrossPlatform.ViewModels;
using CutCode.CrossPlatform.Views;
using ReactiveUI;
using Splat;

namespace CutCode.CrossPlatform.BootStrappers;

public static class NavigationBootstrapper
{
    public static void RegisterViewModels(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
    {
        services.Register(() => new HomeView(), typeof(IViewFor<HomeViewModel>));
        services.Register(() => new AddView(), typeof(IViewFor<AddViewModel>));
        services.Register(() => new FavoritesView(), typeof(IViewFor<FavoritesViewModel>));
        services.Register(() => new SettingsView(), typeof(IViewFor<SettingsViewModel>));
    }
}