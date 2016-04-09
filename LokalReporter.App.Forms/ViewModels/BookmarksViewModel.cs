using System.Collections.Generic;
using System.Linq;

using LokalReporter.Responses;

using PropertyChanged;

namespace LokalReporter.App.FormsApp.ViewModels
{
    [ImplementPropertyChanged]
    public class BookmarksViewModel : BaseViewModel
    {

        private readonly IBookmarkService bookmarkService;

        public List<Article> BookmarkedArticles { get; set; }

        public BookmarksViewModel(IBookmarkService bookmarkService)
        {
            this.bookmarkService = bookmarkService;
        }


        public override async void Start()
        {
            base.Start();
            this.IsLoading = true;
            this.BookmarkedArticles = (await this.bookmarkService.GetBookmarkedArticlesAsync()).ToList();
            this.HasItems = BookmarkedArticles.Count > 0;
            this.IsLoading = false;
        }

        public bool HasItems { get; set; }

    }
}