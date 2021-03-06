using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LokalReporter.Common;
using LokalReporter.Requests;
using LokalReporter.Responses;

using Plugin.Toasts;

using ReactiveUI;

namespace LokalReporter.App.FormsApp.ViewModels
{
    public class SetupFeedFilterViewModel : BaseViewModel
    {

        private readonly IArticlesService articlesService;
        private readonly IToastNotificator notificator;
        private readonly IUserSettings userSettings;
        private readonly List<object> selectViewModels = new List<object>();

        public SetupFeedFilterViewModel(IArticlesService articlesService, IToastNotificator notificator, IUserSettings userSettings)
        {
            this.articlesService = articlesService;
            this.notificator = notificator;
            this.userSettings = userSettings;
            this.Title = "Filter erstellen";
            var canExecute = this.WhenAny(vm => vm.CategorySelect.SelectedItem, vm => vm.DistrictSelect.SelectedItem,
                (ci, di) => ci.Value != null || di.Value != null);
            this.Create = ReactiveCommand.Create(canExecute);
            this.Create.Subscribe(this.CreateFilterAction);

            canExecute.Subscribe(b =>
            {
                var titleParts =
                    this.selectViewModels.OfType<SelectViewModel>()
                        .Select(svm => (svm.SelectedItem as IdEntity)?.Name)
                        .Where(p => p != null);
                this.FilterName = string.Join(" - ", titleParts);
            });
        }

        public ReactiveCommand<object> Create { get; set; }

        public string FilterName { get; set; }

        public SelectViewModel<Category> CategorySelect { get; set; }

        public SelectViewModel<District> DistrictSelect { get; set; }

        private async void CreateFilterAction(object o)
        {
            var preset = new FilterPreset();
            preset.Filter = new Filter();

            if (!string.IsNullOrWhiteSpace(this.FilterName))
            {
                preset.Title = this.FilterName;
            }
            else
            {
                var titleParts =
                    this.selectViewModels.OfType<SelectViewModel>()
                        .Select(svm => (svm.SelectedItem as IdEntity)?.Name)
                        .Where(p => p != null);
                preset.Title = string.Join(" - ", titleParts);
            }

            if (this.CategorySelect.SelectedItem != null)
            {
                preset.Filter.Category = this.CategorySelect.SelectedItem;
            }

            if (this.DistrictSelect.SelectedItem != null)
            {
                preset.Filter.District = this.DistrictSelect.SelectedItem;
            }

            this.IsLoading = true;
            try
            {
                await this.userSettings.UserFiltersSetting.AddItemAndSave(preset);
                this.Close(this);
                await this.notificator.Notify(ToastNotificationType.Success, "Filter erstellt",
                    $"Neuer Filter {preset.Title} erfolgreich erstellt.", TimeSpan.FromSeconds(3));
            }
            finally
            {
                this.IsLoading = false;
            }
        }

        public override async void Start()
        {
            base.Start();

            this.DistrictSelect = new SelectViewModel<District>
            {
                Items = (await this.articlesService.GetDistrictsAsync(this.CloseCancellationToken)).ToList(),
                Title = "Bezirk",
                Placeholder = "W�hlen Sie einen Bezirk"
            };

            this.CategorySelect = new SelectViewModel<Category>
            {
                Items = (await this.articlesService.GetCategoriesAsync(this.CloseCancellationToken)).ToList(),
                Placeholder = "W�hlen Sie ein Resort",
                Title = "Resort"
            };

            this.selectViewModels.Add(this.DistrictSelect);
            this.selectViewModels.Add(this.CategorySelect);
        }

    }
}