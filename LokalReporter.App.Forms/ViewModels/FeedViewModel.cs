using System.Threading.Tasks;

using LokalReporter.App.FormsApp.Helpers;
using LokalReporter.Common;
using LokalReporter.Requests;

using MvvmCross.Platform;

using PropertyChanged;

namespace LokalReporter.App.FormsApp.ViewModels
{
    [ImplementPropertyChanged]
    public class FeedViewModel : BaseViewModel
    {

        public FilteredArticlesViewModel TopArticles { get; set; }
        public FilteredArticlesViewModel Articles { get; set; }

        public async void Setup(FilterPreset preset)
        {
            this.Title = preset.Title;

            Filter filter = preset.Filter.Clone();
            filter.Paging = new Paging {Limit = 20};
            this.Articles = Mvx.IocConstruct<FilteredArticlesViewModel>();

            Filter topFilter = filter.Clone();
            topFilter.IsTopStory = true;

            this.TopArticles = Mvx.IocConstruct<FilteredArticlesViewModel>();

            this.IsLoading = true;
            try
            {
                await this.TopArticles.Setup(topFilter);
                this.IsLoading = false;
                await Task.Delay(200);
                await this.Articles.Setup(filter);
            }
            finally
            {
                this.IsLoading = false;
            }
        }

    }
}