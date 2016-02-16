using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LokalReporter.Requests;
using LokalReporter.Responses;

namespace LokalReporter.Client.Dummy {

    public class JsonArticlesService : IArticlesService {
        private Dictionary<string, Article> articles;

        public async Task<ArticlesResult> GetArticlesAsync(Filter filter, CancellationToken cancellationToken)
        {
            await this.EnsureLoadedAsync();
            return await Task.Run(() => new FilterHandler(this.articles.Values.AsQueryable()).FilterBy(filter));
        }

        public async Task<Article> GetArticleAsync(string id, CancellationToken cancellationToken)
        {
            await this.EnsureLoadedAsync();
            return this.articles[id];
        }

        private async Task EnsureLoadedAsync()
        {
            if (this.articles == null) {
                var articles = await new JsonArticlesReader().ReadAsync();
                this.articles = articles.Distinct().Select(a => {
                    if (a.Images.Count == 0) {
                        a.Images.Add(new Image("", 0, 0));
                    }
                    return a;
                }).ToDictionary(a => a.Id, a => a);
            }
        }
    }

}