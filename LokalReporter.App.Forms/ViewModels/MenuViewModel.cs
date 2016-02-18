using System;
using System.Collections.Generic;
using System.Windows.Input;
using LokalReporter.App.FormsApp.Helpers;
using PropertyChanged;
using XLabs;
using System.Linq;
using LokalReporter.Requests;

namespace LokalReporter.App.FormsApp.ViewModels {

    public class MenuSections : List<MenuSection> {
        public MenuSections(params MenuSection[] items) : base(items) {}
    }

    public class MenuSection : List<MenuItem> {
        public MenuSection(string title, IEnumerable<MenuItem> items) : base(items)
        {
            this.Title = title;
        }

        public string Title { get; set; }
    }

    public class MenuItem {
        public MenuItem(string title)
        {
            this.Title = title;
        }

        public string Title { get; set; }
    }

    internal class FilterMenuItem : MenuItem {
        public FilterMenuItem(string title, Filter filter) : base(title)
        {
            this.Filter = filter;
        }

        public Filter Filter { get; }
    }

    [ImplementPropertyChanged]
    public class MenuViewModel : BaseViewModel {
        private readonly IArticlesService service;

        public MenuViewModel(IArticlesService service)
        {
            this.service = service;
            this.Navigate = new RelayCommand<MenuItem>(NavigateAction);
        }

        public MenuSections Items { get; set; }

        public ICommand Navigate { get; }

        private void NavigateAction(MenuItem i)
        {
            if (i is FilterMenuItem) {
                var filter = ((FilterMenuItem) i).Filter;
                filter.Paging.Limit = 500;
                var parameter = new FilterPreset {Filter = filter};
                this.ShowViewModel<MultiFilteredArticlesViewModel, FilterPreset>(parameter);
            }
            else {
                throw new NotSupportedException();
            }
        }

        public override async void Start()
        {
            base.Start();

            var categories = await this.service.GetCategoriesAsync(this.CloseCancellationToken);
            var districs = await this.service.GetDistrictsAsync(this.CloseCancellationToken);

            var filterMenuItems = categories.Select(r => new FilterMenuItem(r.Name, new Filter {Category = r}));

            var distMenuItems = districs.Select(d => new FilterMenuItem(d.Name, new Filter {District = d}));

            this.Items = new MenuSections(new MenuSection("Resorts", filterMenuItems), new MenuSection("Bezirke", distMenuItems));
        }
    }

}