using System.Threading.Tasks;
using System.Windows.Input;

using MvvmCross.Core.ViewModels;

namespace LokalReporter.App.FormsApp.ViewModels {

    public class FirstViewModel : BaseViewModel {

        public FirstViewModel()
        {
            this.NavigateToSecond = new MvxCommand(() => this.ShowViewModel<SecondViewModel>());
        }

        public ICommand NavigateToSecond { get; set; }

        public string TestText { get; set; }

        public override async void Start()
        {
            base.Start();
            this.Update(1);
        }

        private async void Update(int i)
        {
            this.TestText = "TestText! " + i;
            await Task.Delay(3000);
            this.Update(++i);
        }

    }

}