using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

using LokalReporter.App.FormsApp.Helpers;
using LokalReporter.Common;
using LokalReporter.Requests;
using LokalReporter.Responses;

using PropertyChanged;

using Xamarin.Forms;

using XLabs;

namespace LokalReporter.App.FormsApp.ViewModels
{
    [ImplementPropertyChanged]
    public class MenuViewModel : DistrictSelectionViewModel
    {

        private readonly IArticlesService service;
        private MenuItem selectedItem;
        private bool inOnNavitated;

        public MenuViewModel(IArticlesService service) : base(service)
        {
            this.service = service;
            this.Navigate = new RelayCommand<MenuItem>(this.NavigateAction);
        }

        public MenuItem SelectedItem
        {
            get { return this.selectedItem; }
            set
            {
                if (this.selectedItem != null)
                {
                    this.Dispatcher.RequestMainThreadAction(() => this.selectedItem.ForegroundColor = MenuItem.DefaultColor);
                }
                this.selectedItem = value;

                if (value != null)
                {
                    this.Dispatcher.RequestMainThreadAction(() => value.ForegroundColor = Color.White);
                }
            }
        }

        protected override void OnSelectedDistrictChanged(District d)
        {
            if (this.inOnNavitated)
            {
                return;
            }

            base.OnSelectedDistrictChanged(d);
            if (d == null)
            {
                return;
            }
            var mi = new MenuItem(d.Name, new FilterPreset(d.Name, new Filter {District = d}), typeof (MultiFilteredArticlesViewModel));
            this.NavigateAction(mi);

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
            var bookmarks = new MenuItem("Ihre Leseliste", typeof (BookmarksViewModel));

            this.Items = new List<MenuSection> {new MenuSection("Allgemeines", new List<MenuItem> {startMenuItem, bookmarks}), new MenuSection("Resorts", filterMenuItems) 

                };
            //new MenuSection("Bezirke", distMenuItems),
        }

        public void OnNavigated(Page page)
        {
            try
            {
                this.inOnNavitated = true;

                IEnumerable<MenuItem> menuItems = this.Items.SelectMany(ien => ien).ToList();

                var filtered = page.BindingContext as IFiltered;
                this.SelectedDistrict = filtered?.Filter?.District;

                var newSelected = menuItems.FirstOrDefault(mi => mi.TargetViewModelType == page?.BindingContext?.GetType() && mi.Title.Equals((page.BindingContext as BaseViewModel).Title));
                newSelected = newSelected ?? menuItems.FirstOrDefault(mi => mi.Title == page?.Title);
                this.SelectedItem = newSelected;
            }
            finally
            {
                this.inOnNavitated = false;
            }
        }

    }
}