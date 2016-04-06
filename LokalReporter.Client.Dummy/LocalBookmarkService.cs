using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using LokalReporter.Common;
using LokalReporter.Requests;
using LokalReporter.Responses;

namespace LokalReporter.Client.Dummy
{
    public class LocalBookmarkService : IBookmarkService
    {

        private readonly IArticlesService articlesService;

        private readonly ICollectionAsyncSetting<int> bookmarksSetting;


        public LocalBookmarkService(IUserSettings userSettings, IArticlesService articlesService)
        {
            this.articlesService = articlesService;
            this.bookmarksSetting = userSettings.BookmarksSetting;
        }

        public async Task<bool> ToggleBookmarkAsync(int articleId)
        {
            IReadOnlyCollection<int> bookmarks = await this.bookmarksSetting.GetValueAsync();
            var bms = bookmarks as List<int> ?? bookmarks.ToList();

            if (bookmarks.Contains(articleId))
            {
                bms.Remove(articleId);
                await this.bookmarksSetting.SetValueAsync(bms);
                return false;
            }
            else
            {
                await this.bookmarksSetting.AddItemAndSave(articleId);
                return true;
            }
        }

        public async Task<IEnumerable<Article>> GetBookmarkedArticlesAsync()
        {
            var bs = await this.bookmarksSetting.GetValueAsync();
            var articlesResult = await this.articlesService.GetArticlesAsync(new Filter {Ids = bs}, CancellationToken.None);
            return articlesResult.Articles;
        }

    }
}