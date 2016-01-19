using LokalReporter.App.FormsApp.ViewModels;

using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.IoC;

namespace LokalReporter.App.FormsApp {

    public class App : MvxApplication {

        public override void Initialize()
        {
            this.CreatableTypes().EndingWith("Service").AsInterfaces().RegisterAsLazySingleton();

            this.RegisterAppStart<FirstViewModel>();
        }

    }

}