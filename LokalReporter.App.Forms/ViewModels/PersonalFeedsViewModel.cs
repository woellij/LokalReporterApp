using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using LokalReporter.App.FormsApp.Helpers;
using LokalReporter.Requests;
using LokalReporter.Responses;
using MvvmCross.Platform;
using PropertyChanged;
using XLabs;

namespace LokalReporter.App.FormsApp.ViewModels {

    [ImplementPropertyChanged]
    public class PersonalFeedsViewModel : BaseViewModel, IFeedsViewModel, INavigatedToAware {
        private IReadOnlyCollection<FilterPreset> filters;

        public PersonalFeedsViewModel()
        {
            this.ShowDetails = new RelayCommand<Article>(a => this.ShowViewModel<DetailsViewModel>(new Identifier(a.Id)));
            this.AddNewFeedFilter = new RelayCommand(() => this.ShowViewModel<SetupFeedFilterViewModel>());

            this.Title = "Startseite";
        }

        public ICommand AddNewFeedFilter { get; }

        public ICommand ShowDetails { get; }

        public ICollection<FeedViewModel> Feeds { get; set; }

        public void OnNavigatedTo(NavigationEventType type)
        {
            if (type == NavigationEventType.Popped) {
                this.Start();
            }
        }

        public override async void Start()
        {
            var startFilters = await Task.Run(() => GetUserFeedFilters());
            if (startFilters == null) {
                return;
            }

            if (this.filters != null) {
                var additional = startFilters.Except(this.filters).ToList();
                if (additional.Any()) {
                    var additionalFeeds = this.CreateFeeds(additional);
                    foreach (var additionalFeed in additionalFeeds) {
                        this.Feeds.Add(additionalFeed);
                    }
                }
            }
            else {
                this.Feeds = new ObservableCollection<FeedViewModel>(this.CreateFeeds(startFilters));
            }

            this.filters = startFilters;
        }

        private List<FeedViewModel> CreateFeeds(IReadOnlyCollection<FilterPreset> filters)
        {
            return filters.Select((f, i) => {
                var feedViewModel = Mvx.IocConstruct<FeedViewModel>();

                Task.Delay(500*i).ContinueWith(t => { feedViewModel.Setup(f); }, TaskScheduler.FromCurrentSynchronizationContext());
                return feedViewModel;
            }).ToList();
        }

        private static IReadOnlyCollection<FilterPreset> GetUserFeedFilters()
        {
            IReadOnlyCollection<FilterPreset> filters = Settings.UserFeedFilters;
            District selectedDistrict = Settings.SelectedDistrict;
            var districtFilter = new FilterPreset(selectedDistrict.Name, new Filter {District = selectedDistrict});

            return new[] {districtFilter}.Concat(filters).ToList();
        }
    }

}