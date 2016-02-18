using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using LokalReporter.App.FormsApp.Helpers;
using LokalReporter.Requests;
using LokalReporter.Responses;
using MvvmCross.Core.ViewModels;
using PropertyChanged;
using XLabs;

namespace LokalReporter.App.FormsApp.ViewModels {

    [ImplementPropertyChanged]
    public class FilteredArticlesViewModel : BaseViewModel {
        private readonly IArticlesService articlesService;
        private Filter filter;

        public FilteredArticlesViewModel(IArticlesService articlesService)
        {
            this.articlesService = articlesService;
            this.ShowDetails = new RelayCommand<Article>(a => this.ShowViewModel<DetailsViewModel>(new Identifier(a.Id)));
        }

        public IReadOnlyCollection<Article> Items { get; private set; } = new List<Article>();
        public ICommand ShowDetails { get; }

        public async Task<FilteredArticlesViewModel> Setup(Filter filter)
        {
            if (filter == null) {
                throw new ArgumentNullException(nameof(filter));
            }

            this.filter = filter;

            ArticlesResult result = await this.articlesService.GetArticlesAsync(filter, this.CloseCancellationToken);
            this.Items = result.Articles;

            return this;
        }

        protected override async void InitFromBundle(IMvxBundle parameters)
        {
            var filterPreset = parameters.GetParameter<FilterPreset>();
            this.Title = filterPreset.Title;
            await this.Setup(filterPreset.Filter);
        }
        

        public string Title { get; set; }
    }

}