using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LokalReporter.App.FormsApp.Helpers;
using LokalReporter.Requests;
using LokalReporter.Responses;
using Plugin.Toasts;
using ReactiveUI;

namespace LokalReporter.App.FormsApp.ViewModels {

    public class SetupFeedFilterViewModel : BaseViewModel {
        private readonly IArticlesService articlesService;
        private readonly IToastNotificator notificator;
        private readonly List<object> selectViewModels = new List<object>();

        public SetupFeedFilterViewModel(IArticlesService articlesService, IToastNotificator notificator)
        {
            this.articlesService = articlesService;
            this.notificator = notificator;
            this.Title = "Filter erstellen";
            IObservable<bool> canExecute = this.WhenAny(vm => vm.CategorySelect.SelectedItem, vm => vm.DistrictSelect.SelectedItem,
                (ci, di) => ci.Value != null || di.Value != null);
            this.Create = ReactiveCommand.Create(canExecute);
            this.Create.Subscribe(CreateAction);

            canExecute.Subscribe(b => {
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

        private async void CreateAction(object o)
        {
            var preset = new FilterPreset();
            preset.Filter = new Filter();

            if (!string.IsNullOrWhiteSpace(this.FilterName)) {
                preset.Title = this.FilterName;
            }
            else {
                var titleParts =
                    this.selectViewModels.OfType<SelectViewModel>()
                        .Select(svm => (svm.SelectedItem as IdEntity)?.Name)
                        .Where(p => p != null);
                preset.Title = string.Join(" - ", titleParts);
            }

            if (this.CategorySelect.SelectedItem != null) {
                preset.Filter.Category = this.CategorySelect.SelectedItem;
            }

            if (this.DistrictSelect.SelectedItem != null) {
                preset.Filter.District = this.DistrictSelect.SelectedItem;
            }

            this.IsLoading = true;
            try {
                await Task.Delay(2000);
                await Task.Run(() => Settings.AddFeedFilter(preset));
                await this.notificator.Notify(ToastNotificationType.Success, "Filter erstellt",
                    $"Neuer Filter {preset.Title} erfolgreich erstellt.", TimeSpan.FromSeconds(3));
                this.Close(this);
            }
            finally {
                this.IsLoading = false;
            }

        }

        public override async void Start()
        {
            base.Start();

            this.DistrictSelect = new SelectViewModel<District> {
                Items = (await this.articlesService.GetDistrictsAsync(this.CloseCancellationToken)).ToList(),
                Title = "Bezirk",
                Placeholder = "Wählen Sie einen Bezirk"
            };

            this.CategorySelect = new SelectViewModel<Category> {
                Items = (await this.articlesService.GetCategoriesAsync(this.CloseCancellationToken)).ToList(),
                Placeholder = "Wählen Sie ein Resort",
                Title = "Resort"
            };

            this.selectViewModels.Add(this.DistrictSelect);
            this.selectViewModels.Add(this.CategorySelect);
        }
    }

}