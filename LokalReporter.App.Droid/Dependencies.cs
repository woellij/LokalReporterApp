using LokalReporter.App.FormsApp;
using LokalReporter.Client.Dummy;

using MvvmCross.Platform.IoC;

namespace LokalReporter.App.Droid {

    public class Dependencies {

        static bool isInitialized;

        public static void Initialize()
        {
            if (Dependencies.isInitialized) {
                return;
            }

            // initialize each module

            new DummyClientDependencyModule().Initialize();
            new AppDepencencyModule().Initialize();
        }

    }

}