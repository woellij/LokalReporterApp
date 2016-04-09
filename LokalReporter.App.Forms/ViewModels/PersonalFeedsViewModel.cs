using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using LokalReporter.App.FormsApp.Presenter;
using LokalReporter.Common;
using LokalReporter.Requests;
using LokalReporter.Responses;

using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

using PropertyChanged;

using XLabs;

namespace LokalReporter.App.FormsApp.ViewModels
{
    [ImplementPropertyChanged]
    public class PersonalFeedsViewModel : BaseViewModel, INavigatedToAware
    {

        private readonly IUserSettings userSettings;
        private IReadOnlyCollection<FilterPreset> filters;

        public PersonalFeedsViewModel(IUserSettings userSettings)
        {
            this.userSettings = userSettings;
            this.ShowDetails = new RelayCommand<Article>(a => this.ShowViewModel<DetailsViewModel>(new Identifier(a.Id.ToString())));
            this.AddNewFeedFilter = new RelayCommand(() => this.ShowViewModel<SetupFeedFilterViewModel>());

            this.BookmarksViewModel = Mvx.IocConstruct<BookmarksViewModel>();
            
            this.Title = "Startseite";
        }

        public BookmarksViewModel BookmarksViewModel { get; set; }

        public ICommand AddNewFeedFilter { get; }

        public ICommand ShowDetails { get; }

        public ICollection<FeedViewModel> Feeds { get; set; }

        public void OnNavigatedTo(NavigationEventType type)
        {
            if (type == NavigationEventType.Popped)
            {
                this.Start();
            }
            this.BookmarksViewModel.Start();
        }

        public override async void Start()
        {
            this.BookmarksViewModel.Start();

            var startFilters = await this.GetUserFeedFilters();
            if (startFilters == null)
            {
                return;
            }

            if (this.filters != null)
            {
                var additional = startFilters.Except(this.filters).ToList();
                if (additional.Any())
                {
                    var additionalFeeds = this.CreateFeeds(additional);
                    foreach (var additionalFeed in additionalFeeds)
                    {
                        this.Feeds.Add(additionalFeed);
                    }
                }
                
                var deletedFeeds = this.Feeds.Where(f => this.filters.Except(startFilters).Any(preset => preset.Title == f.Title)).ToList();
                foreach (var feedViewModel in deletedFeeds)
                {
                    this.Feeds.Remove(feedViewModel);
                }
            }
            else
            {
                this.Feeds = new ObservableCollection<FeedViewModel>(this.CreateFeeds(startFilters));
            }

            this.filters = startFilters;
        }

        private List<FeedViewModel> CreateFeeds(IReadOnlyCollection<FilterPreset> filters)
        {
            return filters.Select((f, i) =>
            {
                var feedViewModel = Mvx.IocConstruct<FeedViewModel>();

                Task.Delay(500*i)
                    .ContinueWith(t => { feedViewModel.Setup(f); }, TaskScheduler.FromCurrentSynchronizationContext());
                return feedViewModel;
            }).ToList();
        }

        private async Task<IReadOnlyCollection<FilterPreset>> GetUserFeedFilters()
        {
            var filters = await this.userSettings.UserFiltersSetting.GetValueAsync();
            var selectedDistrict = await this.userSettings.DistrictSetting.GetValueAsync();
            var districtFilter = new FilterPreset(selectedDistrict.Name, new Filter {District = selectedDistrict});

            return new[] {districtFilter}.Concat(filters).ToList();
        }

    }
}