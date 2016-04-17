using LokalReporter.App.FormsApp;
using LokalReporter.Client.Dummy;

using MvvmCross.Platform;
using MvvmCross.Platform.IoC;

using Plugin.Toasts;

namespace LokalReporter.App.Droid {

    public class Dependencies {

        static bool _isInitialized;

        public static void Initialize(MvxFormsApplicationActivity current)
        {
            if (Dependencies._isInitialized) {
                return;
            }

            Mvx.LazyConstructAndRegisterSingleton<IToastNotificator, ToastNotificatorImplementation>();
            ToastNotificatorImplementation.Init(current);
            // initialize each module

            new DummyClientDependencyModule().Initialize();
            new AppDepencencyModule().Initialize();
        }

    }

}