using System.Collections.Generic;
using System.Linq;

using LokalReporter.App.FormsApp.Presenter;
using LokalReporter.Responses;

using PropertyChanged;

namespace LokalReporter.App.FormsApp.ViewModels
{
    [ImplementPropertyChanged]
    public class BookmarksViewModel : FilteredArticlesViewModel, INavigatedToAware
    {

        private readonly IBookmarkService bookmarkService;
        private List<Article> bookmarkedArticles;

        public List<Article> BookmarkedArticles
        {
            get { return this.bookmarkedArticles; }
            set
            {
                this.bookmarkedArticles = value;
                this.HasItems = this.BookmarkedArticles.Count > 0;
                this.HasNoItems = !this.HasItems;
            }
        }

        public BookmarksViewModel(IBookmarkService bookmarkService, IArticlesService service)
            : base(service)
        {
            this.bookmarkService = bookmarkService;
            this.Title = "Ihre Leseliste";
        }


        public override async void Start()
        {
            base.Start();
            this.IsLoading = true;
            this.BookmarkedArticles = (await this.bookmarkService.GetBookmarkedArticlesAsync()).ToList();
            this.IsLoading = false;
        }

        public bool HasNoItems { get; set; }

        public bool HasItems { get; set; }

        public async void OnNavigatedTo(NavigationEventType type)
        {
            if (type == NavigationEventType.Popped)
            {
                this.BookmarkedArticles = (await this.bookmarkService.GetBookmarkedArticlesAsync()).ToList();
            }
        }

    }
}