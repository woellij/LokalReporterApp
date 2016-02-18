using System.Threading.Tasks;
using LokalReporter.App.FormsApp.Helpers;
using MvvmCross.Platform;
using PropertyChanged;

namespace LokalReporter.App.FormsApp.ViewModels {

    [ImplementPropertyChanged]
    public class FeedViewModel : BaseViewModel {
        public FilteredArticlesViewModel TopArticles { get; set; }
        public FilteredArticlesViewModel Articles { get; set; }

        public string Title { get; set; }

        public async void Setup(FilterPreset preset)
        {
            this.Title = preset.Title;

            var filter = preset.Filter.Clone();
            filter.Paging = new Paging {Limit = 20};
            this.Articles = Mvx.IocConstruct<FilteredArticlesViewModel>();

            var topFilter = filter.Clone();
            topFilter.IsTopStory = true;

            this.TopArticles = Mvx.IocConstruct<FilteredArticlesViewModel>();

            this.IsLoading = true;
            try {
                await Task.WhenAll(this.TopArticles.Setup(topFilter), this.Articles.Setup(filter));
            }
            finally {
                this.IsLoading = false;
            }
        }
    }

}