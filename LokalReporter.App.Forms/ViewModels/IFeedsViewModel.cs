using System.Collections.Generic;

namespace LokalReporter.App.FormsApp.ViewModels {

    internal interface IFeedsViewModel {
        IReadOnlyCollection<FeedViewModel> Feeds { get; set; }
    }

}