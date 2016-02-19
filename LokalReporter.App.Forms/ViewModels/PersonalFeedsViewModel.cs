using System.Collections.Generic;
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
    public class PersonalFeedsViewModel : BaseViewModel, IFeedsViewModel {
        public PersonalFeedsViewModel()
        {
            this.ShowDetails = new RelayCommand<Article>(a => this.ShowViewModel<DetailsViewModel>(new Identifier(a.Id)));
            this.AddNewFeedFilter = new RelayCommand(() => this.ShowViewModel<SetupFeedFilterViewModel>());

            this.Title = "Startseite";
        }

        public ICommand AddNewFeedFilter { get; }

        public ICommand ShowDetails { get; }

        public IReadOnlyCollection<FeedViewModel> Feeds { get; set; }

        public override async void Start()
        {
            var filters = await Task.Run(() => GetUserFeedFilters());
            if (filters == null) {
                return;
            }

            this.Feeds = filters.Select((f, i) => {
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