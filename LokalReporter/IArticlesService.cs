using System.Threading;
using System.Threading.Tasks;

using LokalReporter.Responses;

namespace LokalReporter {

    public interface IArticlesService {

        Task<ArticlesResult> GetArticlesAsync(Filter filter, CancellationToken cancellationToken);

    }

}