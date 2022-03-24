// ---------------------------------------------
//      --- CutCode.CrossPlatform by Scarementus ---
//      ---        Licence MIT       ---
// ---------------------------------------------

using Splat;

namespace CutCode.CrossPlatform.BootStrappers;

public static class EntryBootstrapper
{
    public static void Register(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
    {
        ServicesBootstrapper.RegisterServices(services, resolver);
        NavigationBootstrapper.RegisterViewModels(services, resolver);
    }
}