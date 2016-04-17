using LokalReporter.App.FormsApp;
using LokalReporter.Client.Dummy;

using MvvmCross.Platform;

using Plugin.Toasts;

namespace LokalReporter.App.iOS {

    public class Dependencies {

        public static bool IsInitialized;

        public static void Initialize()
        {
            if (Dependencies.IsInitialized) {
                return;
            }

            Mvx.LazyConstructAndRegisterSingleton<IToastNotificator, ToastNotificatorImplementation>();
            ToastNotificatorImplementation.Init();
            
            // initialize each module
            new DummyClientDependencyModule().Initialize();
            new AppDepencencyModule().Initialize();
        }

    }

}