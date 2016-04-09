using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

using LokalReporter.App.FormsApp.Helpers;
using LokalReporter.Requests;
using LokalReporter.Responses;

using PropertyChanged;

using XLabs;

namespace LokalReporter.App.FormsApp.ViewModels
{
    [ImplementPropertyChanged]
    public class FilteredArticlesViewModel : BaseViewModel
    {

        private readonly IArticlesService articlesService;
        private Filter filter;

        public FilteredArticlesViewModel(IArticlesService articlesService)
        {
            this.articlesService = articlesService;
            this.ShowDetails = new RelayCommand<Article>(a => this.ShowViewModel<DetailsViewModel>(new Identifier(a.Id.ToString())));
            this.Restock = new RelayCommand(this.RestockAction);
        }

        public ObservableCollection<Article> Items { get; private set; } = new ObservableCollection<Article>();
        public ICommand ShowDetails { get; }
        public ICommand Restock { get; }
        public bool IsRestocking { get; set; }

        public async Task<FilteredArticlesViewModel> Setup(Filter filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            this.filter = filter;

            var result = await this.articlesService.GetArticlesAsync(filter, this.CloseCancellationToken);
            this.Items = new ObservableCollection<Article>(result.Articles);
            this.HasItems = this.Items.Count > 0;
            return this;
        }

        public bool HasItems { get; set; } = true;

        public async void RestockAction()
        {
            if (this.IsRestocking)
            {
                // don't trigger restock again
                return;
            }

            this.IsRestocking = true;
            try
            {
                var restockFilter = this.filter.Clone();
                restockFilter.Paging = new Paging {Offset = this.Items.Count, Limit = 20};
                var articlesResult = await this.articlesService.GetArticlesAsync(restockFilter, this.CloseCancellationToken);
                foreach (var article in articlesResult.Articles)
                {
                    this.Items.Add(article);
                }
            }
            finally
            {
                this.IsRestocking = false;
            }
        }

    }
}