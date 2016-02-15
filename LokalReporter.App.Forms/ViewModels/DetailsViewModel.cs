using LokalReporter.Responses;
using PropertyChanged;

namespace LokalReporter.App.FormsApp.ViewModels {

    [ImplementPropertyChanged]
    public class DetailsViewModel : BaseViewModel {
        private readonly IArticlesService articles;
        public DetailsViewModel(IArticlesService articles)
        {
            this.articles = articles;
        }

        public async void Init(Identifier identifier)
        {
            this.Article = await this.articles.GetArticleAsync(identifier.Id, this.CloseCancellationToken);
        }

        public Article Article { get; set; }
    }

}