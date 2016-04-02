using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

using LokalReporter.App.FormsApp.Helpers;
using LokalReporter.Common;
using LokalReporter.Requests;

using PropertyChanged;

using XLabs;

namespace LokalReporter.App.FormsApp.ViewModels
{
    [ImplementPropertyChanged]
    public class MenuViewModel : BaseViewModel
    {

        private readonly IArticlesService service;

        public MenuViewModel(IArticlesService service)
        {
            this.service = service;
            this.Navigate = new RelayCommand<MenuItem>(this.NavigateAction);
        }

        public List<MenuSection> Items { get; set; }

        public ICommand Navigate { get; }

        private void NavigateAction(MenuItem i)
        {
            this.ShowViewModelWithComplexParameter(i.TargetViewModelType, i.Parameter);
        }

        public override async void Start()
        {
            base.Start();

            var categories = await this.service.GetCategoriesAsync(this.CloseCancellationToken);
            var districs = await this.service.GetDistrictsAsync(this.CloseCancellationToken);

            var filterMenuItems =
                categories.Select(
                    r => new MenuItem(r.Name, new FilterPreset(r.Name, new Filter {Category = r}), typeof (MultiFilteredArticlesViewModel)));

            var distMenuItems =
                districs.Select(
                    d => new MenuItem(d.Name, new FilterPreset(d.Name, new Filter {District = d}), typeof (MultiFilteredArticlesViewModel)));

            var startMenuItem = new MenuItem("Startseite", typeof (PersonalFeedsViewModel));

            this.Items = new List<MenuSection> {new MenuSection(null, startMenuItem), new MenuSection("Resorts", filterMenuItems), new MenuSection("Bezirke", distMenuItems)};
        }

    }
}