using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using LokalReporter.Responses;

namespace LokalReporter
{
    public interface IBookmarkService
    {

        Task<bool> ToggleBookmarkAsync(int articleId);

        Task<IEnumerable<Article>> GetBookmarkedArticlesAsync();

    }
}