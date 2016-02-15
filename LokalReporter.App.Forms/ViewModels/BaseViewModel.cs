using System.Threading;
using MvvmCross.Core.ViewModels;

using PropertyChanged;

namespace LokalReporter.App.FormsApp.ViewModels {

    [ImplementPropertyChanged]
    public class BaseViewModel : MvxViewModel {

        
        protected CancellationToken CloseCancellationToken { get; } = CancellationToken.None;
    }

}