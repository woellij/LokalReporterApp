using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using LokalReporter.App.FormsApp.Helpers;
using LokalReporter.App.FormsApp.ViewModels.Messages;
using LokalReporter.Common;

using ReactiveUI;

using XLabs;

namespace LokalReporter.App.FormsApp.ViewModels {

    public class FilterSettingsViewModel : BaseViewModel {

        private IAsyncSetting<IReadOnlyCollection<FilterPreset>> setting;

        public FilterSettingsViewModel(IUserSettings userSettings)
        {
            this.setting = userSettings.UserFiltersSetting;
            this.Title = "Ihre Filter";
        }

        public override async void Start()
        {
            base.Start();
            this.Filters = new ObservableCollection<FilterPreset>(await this.setting.GetValueAsync());
            this.DeleteFilter = new RelayCommand<FilterPreset>(this.DeleteFilterAction);
            this.AddNewFeedFilter = new RelayCommand(() => this.ShowViewModel<SetupFeedFilterViewModel>());
        }

        public ICommand AddNewFeedFilter { get; set; }

        public ICommand DeleteFilter { get; set; }

        private async void DeleteFilterAction(FilterPreset obj)
        {
            if (!this.Filters.Contains(obj)) {
                return;
            }
            this.Filters.Remove(obj);
            await this.setting.SetValueAsync(this.Filters.ToList());
            MessageBus.Current.SendMessage(new FeedSubscribedChangedMessage(false, obj));
        }

        public ICollection<FilterPreset> Filters { get; set; }
    }

}