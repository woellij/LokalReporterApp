using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LokalReporter.Requests;
using LokalReporter.Responses;

namespace LokalReporter {

    public interface IArticlesService {
        Task<ArticlesResult> GetArticlesAsync(Filter filter, CancellationToken cancellationToken);

        Task<Article> GetArticleAsync(string id, CancellationToken cancellationToken);

        Task<IReadOnlyCollection<Category>> GetCategoriesAsync(CancellationToken cancellation);

        Task<IReadOnlyCollection<District>> GetDistrictsAsync(CancellationToken cancellation);
    }

}