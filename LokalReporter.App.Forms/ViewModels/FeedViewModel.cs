using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using LokalReporter.App.FormsApp.Helpers;
using LokalReporter.App.FormsApp.ViewModels.Messages;
using LokalReporter.Common;
using LokalReporter.Requests;
using LokalReporter.Responses;

using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

using Newtonsoft.Json;

using PropertyChanged;

using ReactiveUI;

using XLabs;

namespace LokalReporter.App.FormsApp.ViewModels
{
    [ImplementPropertyChanged]
    public class FeedViewModel : BaseViewModel, IFiltered
    {
        
        private readonly ICollectionAsyncSetting<FilterPreset> userFilterSetting;
        private bool isSubscribed;
        private bool settingUp;

        public FeedViewModel(IUserSettings settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            this.userFilterSetting = settings.UserFiltersSetting;
            this.ShowDetails = new RelayCommand<Article>(a => this.ShowViewModel<DetailsViewModel>(new Identifier(a.Id.ToString())));
            this.ShowMore = new RelayCommand(() =>
            {
                var parameter = new MvxBundle();
                if (parameter.Data != null) parameter.Data["preset"] = JsonConvert.SerializeObject(this.FilterPreset);
                this.ShowViewModel<FeedViewModel>(parameter);
            });
        }

        public bool IsSubscribed
        {
            get { return this.isSubscribed; }
            set
            {
                this.isSubscribed = value;
                if (this.settingUp || this.FilterPreset == null)
                {
                    return;
                }
                MessageBus.Current.SendMessage(new FeedSubscribedChangedMessage(value, this.FilterPreset));
            }
        }

        public bool CanChangeSubscription { get; set; } = true;

        public FilteredArticlesViewModel TopArticles { get; set; }
        public FilteredArticlesViewModel Articles { get; set; }

        public ICommand ShowDetails { get; }

        public ICommand ShowMore { get; }

        public Filter Filter { get; set; }
        public FilterPreset FilterPreset { get; set; }

        protected override void InitFromBundle(IMvxBundle parameters)
        {
            base.InitFromBundle(parameters);
            this.FilterPreset = JsonConvert.DeserializeObject<FilterPreset>(parameters.Data["preset"]);
            this.Setup(this.FilterPreset, false);
        }

        public async void Setup(FilterPreset preset, bool isSubViewModel)
        {
            this.settingUp = true;
            this.Title = isSubViewModel ? preset.Title : preset.ExtendedTitle ?? preset.Title;
            this.FilterPreset = preset;
            this.Filter = preset.Filter;
            
            await this.SetIsSubscribed(preset);

            var filter = preset.Filter.Clone();
            filter.Paging = new Paging {Limit = 5};
            this.Articles = Mvx.IocConstruct<FilteredArticlesViewModel>();

            var topFilter = filter.Clone();
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
                this.settingUp = false;
            }
        }

        private async Task SetIsSubscribed(FilterPreset preset)
        {
            var userFilters = await this.userFilterSetting.GetValueAsync();
            this.IsSubscribed = userFilters.Any(f => f.Filter.Equals(preset.Filter));
        }

    }
}