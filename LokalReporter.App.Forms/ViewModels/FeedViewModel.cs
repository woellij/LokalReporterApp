using System.Threading.Tasks;
using System.Windows.Input;

using LokalReporter.App.FormsApp.Helpers;
using LokalReporter.Common;
using LokalReporter.Requests;
using LokalReporter.Responses;

using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

using Newtonsoft.Json;

using PropertyChanged;

using XLabs;

namespace LokalReporter.App.FormsApp.ViewModels
{
    [ImplementPropertyChanged]
    public class FeedViewModel : BaseViewModel
    {

        private FilterPreset preset;

        public FilteredArticlesViewModel TopArticles { get; set; }
        public FilteredArticlesViewModel Articles { get; set; }

        public FeedViewModel()
        {
            this.ShowDetails = new RelayCommand<Article>(a => this.ShowViewModel<DetailsViewModel>(new Identifier(a.Id.ToString())));
            this.ShowMore = new RelayCommand(() =>
            {

                var parameter = new MvxBundle();
                parameter.Data["preset"] = JsonConvert.SerializeObject((object)this.preset);
                this.ShowViewModel<FeedViewModel>(parameter);
            });
        }

        public ICommand ShowDetails { get; }

        public ICommand ShowMore { get; }

        protected override void InitFromBundle(IMvxBundle parameters)
        {
            base.InitFromBundle(parameters);
            this.preset = JsonConvert.DeserializeObject<FilterPreset>(parameters.Data["preset"]);
            this.Setup(preset);
        }

        public async void Setup(FilterPreset preset)
        {
            this.Title = preset.Title;
            this.preset = preset;

            Filter filter = preset.Filter.Clone();
            filter.Paging = new Paging {Limit = 5 };
            this.Articles = Mvx.IocConstruct<FilteredArticlesViewModel>();

            Filter topFilter = filter.Clone();
            topFilter.IsTopStory = true;
            topFilter.Paging = new Paging {Limit = 5};

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