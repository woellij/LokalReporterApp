using LokalReporter.Client.Dummy.Json;
using MvvmCross.Platform;

namespace LokalReporter.Client.Dummy {

    public class DummyClientDependencyModule : IDependencyModule {

        public void Initialize()
        {
            Mvx.LazyConstructAndRegisterSingleton<IArticlesService>(Mvx.IocConstruct<JsonArticlesService>);
        }

    }

}