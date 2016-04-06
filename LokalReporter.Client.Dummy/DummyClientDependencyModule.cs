using LokalReporter.Client.Dummy.Json;
using LokalReporter.Client.Dummy.Settings;

using MvvmCross.Platform;

namespace LokalReporter.Client.Dummy
{
    public class DummyClientDependencyModule : IDependencyModule
    {

        public void Initialize()
        {
            Mvx.LazyConstructAndRegisterSingleton<IArticlesService>(Mvx.IocConstruct<JsonArticlesService>);
            Mvx.LazyConstructAndRegisterSingleton<IUserSettings>(Mvx.IocConstruct<LocalUserSettings>);
            Mvx.LazyConstructAndRegisterSingleton<ILocations>(Mvx.IocConstruct<FileLocations>);
            Mvx.LazyConstructAndRegisterSingleton<IBookmarkService>(Mvx.IocConstruct<LocalBookmarkService>);
        }

    }
}