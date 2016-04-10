using System.Collections.Generic;

namespace LokalReporter.App.FormsApp.ViewModels {
    internal interface IFeedsViewModel {
        ICollection<FeedViewModel> Feeds { get; set; }
    }

}