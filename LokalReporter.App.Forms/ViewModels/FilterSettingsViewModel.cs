using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using LokalReporter.App.FormsApp.Helpers;
using XLabs;

namespace LokalReporter.App.FormsApp.ViewModels {

    public class FilterSettingsViewModel : BaseViewModel {
        public FilterSettingsViewModel()
        {
            this.Title = "Ihre Filter";
        }

        public override async void Start()
        {
            base.Start();
            this.Filters = new ObservableCollection<FilterPreset>(await Task.Run(() => Settings.UserFeedFilters));
            this.DeleteFilter = new RelayCommand<FilterPreset>(this.DeleteFilterAction);
            this.AddNewFeedFilter = new RelayCommand(() => this.ShowViewModel<SetupFeedFilterViewModel>());
        }

        public ICommand AddNewFeedFilter { get; set; }

        public ICommand DeleteFilter { get; set; }

        private void DeleteFilterAction(FilterPreset obj)
        {
            if (!this.Filters.Contains(obj)) {
                return;
            }
            this.Filters.Remove(obj);
            Task.Run(() => Settings.UserFeedFilters = this.Filters.ToList());
        }

        public ICollection<FilterPreset> Filters { get; set; }
    }

}