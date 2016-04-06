using System.Linq;
using System.Windows.Input;

using LokalReporter.Responses;

using PropertyChanged;

using XLabs;

namespace LokalReporter.App.FormsApp.ViewModels
{
    [ImplementPropertyChanged]
    public class DetailsViewModel : BaseViewModel
    {

        private readonly IArticlesService articles;
        private readonly IBookmarkService bookmarksService;

        public DetailsViewModel(IArticlesService articles, IBookmarkService bookmarksService)
        {
            this.articles = articles;
            this.bookmarksService = bookmarksService;
            this.Bookmark = new RelayCommand(async () =>
            {
                this.IsLoading = true;
                this.IsBookmarked = await this.bookmarksService.ToggleBookmarkAsync(this.Article.Id);
                this.IsLoading = false;
            });
        }


        public ICommand Bookmark { get; set; }
        public Article Article { get; set; }

        public bool IsBookmarked { get; set; }

        public async void Init(Identifier identifier)
        {
            this.Article = await this.articles.GetArticleAsync(identifier.Id, this.CloseCancellationToken);
            this.Title = Article.Title;
            var bookmarks = await this.bookmarksService.GetBookmarkedArticlesAsync();
            this.IsBookmarked = bookmarks.Any(article => article.Id == this.Article.Id);
        }

    }
}