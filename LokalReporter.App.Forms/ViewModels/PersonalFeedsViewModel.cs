using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using LokalReporter.App.FormsApp.Presenter;
using LokalReporter.App.FormsApp.ViewModels.Messages;
using LokalReporter.Common;
using LokalReporter.Requests;
using LokalReporter.Responses;

using MvvmCross.Platform;

using PropertyChanged;

using ReactiveUI;

using XLabs;

namespace LokalReporter.App.FormsApp.ViewModels
{
    [ImplementPropertyChanged]
    public class PersonalFeedsViewModel : BaseViewModel, INavigatedToAware
    {

        private readonly IUserSettings userSettings;
        private IReadOnlyCollection<Tuple<bool, FilterPreset>> filters;

        public PersonalFeedsViewModel(IUserSettings userSettings)
        {
            this.userSettings = userSettings;
            this.ShowDetails = new RelayCommand<Article>(a => this.ShowViewModel<DetailsViewModel>(new Identifier(a.Id.ToString())));
            this.AddNewFeedFilter = new RelayCommand(() => this.ShowViewModel<SetupFeedFilterViewModel>());
            
            MessageBus.Current.Listen<FeedSubscribedChangedMessage>().Subscribe(this.OnFeedSubscribedChanged);

            this.Title = "Startseite";
        }
        
        public ICommand AddNewFeedFilter { get; }

        public ICommand ShowDetails { get; }

        public ICollection<FeedViewModel> Feeds { get; set; }

        public void OnNavigatedTo(NavigationEventType type)
        {
            if (type == NavigationEventType.Popped)
            {
                this.Start();
            }
        }

        public void OnFeedSubscribedChanged(FeedSubscribedChangedMessage message)
        {
            var match = this.Feeds?.FirstOrDefault(f => f.Filter.Equals(message.Preset.Filter));
            if (message.IsSubscribed == false)
            {
                if (match != null)
                {
                    this.userSettings.UserFiltersSetting.RemoveAndSave(match.FilterPreset);
                    this.Feeds.Remove(match);
                }
            }
            else if (message.IsSubscribed)
            {
                if (match != null)
                {
                    return;
                }
                this.userSettings.UserFiltersSetting.AddItemAndSave(message.Preset);
                this.Feeds.Add(this.CreateFeedViewModel(0, message.Preset, true));
            }
        }


        public override async void Start()
        {
            var startFilters = await GetUserFeedFilters();
            if (startFilters == null)
            {
                return;
            }

            if (this.filters != null)
            {
                var additional = startFilters.Where(startFilter => !this.filters.Any(t => t.Item2.Equals(startFilter.Item2))).ToList();

                if (additional.Any())
                {
                    var additionalFeeds = this.CreateFeedViewmodels(additional);
                    foreach (var additionalFeed in additionalFeeds)
                    {
                        this.Feeds.Add(additionalFeed);
                    }
                }

                var deletedFeeds = this.Feeds.Where(f => this.filters.Any(tuple => tuple.Item2.Equals(f))).ToList();
                foreach (var feedViewModel in deletedFeeds)
                {
                    this.Feeds.Remove(feedViewModel);
                }
            }
            else
            {
                this.Feeds = new ObservableCollection<FeedViewModel>(this.CreateFeedViewmodels(startFilters));
            }

            this.filters = startFilters;
        }

        private List<FeedViewModel> CreateFeedViewmodels(IReadOnlyCollection<Tuple<bool, FilterPreset>> filters)
        {
            return filters.Select((tuple, i) => this.CreateFeedViewModel(i, tuple.Item2, tuple.Item1)).ToList();
        }

        private FeedViewModel CreateFeedViewModel(int i, FilterPreset preset, bool canUserChangeSubscription)
        {
            var feedViewModel = Mvx.IocConstruct<FeedViewModel>();

            Task.Delay(100*i)
                .ContinueWith(t =>
                {
                    feedViewModel.Setup(preset, true);
                    feedViewModel.CanChangeSubscription = canUserChangeSubscription;
                }, TaskScheduler.FromCurrentSynchronizationContext());
            return feedViewModel;
        }

        private async Task<IReadOnlyCollection<Tuple<bool, FilterPreset>>> GetUserFeedFilters()
        {
            var filters = await this.userSettings.UserFiltersSetting.GetValueAsync();
            var selectedDistrict = await this.userSettings.DistrictSetting.GetValueAsync();
            var districtFilter = new FilterPreset(selectedDistrict.Name, new Filter {District = selectedDistrict});

            return new[] {new Tuple<bool, FilterPreset>(false, districtFilter)}.Concat(filters.Select(f => new Tuple<bool, FilterPreset>(true, f))).ToList();
        }

    }
}