using System.Threading;
using MvvmCross.Core.ViewModels;

using PropertyChanged;

namespace LokalReporter.App.FormsApp.ViewModels {

    [ImplementPropertyChanged]
    public class BaseViewModel : MvxViewModel {

        public bool IsLoading { get; set; }
        protected CancellationToken CloseCancellationToken { get; } = CancellationToken.None;

        public string Title { get; set; }
    }

}