using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using LokalReporter.Responses;

namespace LokalReporter.Client.Dummy {

    public class DummyArticlesService : IArticlesService {

        public Task<ArticlesResult> GetArticlesAsync(Filter filter, CancellationToken cancellationToken)
        {
            ArticlesResult result = new ArticlesResult();
            var articles = new List<Article>();
            result.Articles = articles;

            for (int i = filter.Paging.Offset; i < filter.Paging.Limit; i++) {
                Article article = DummyArticlesService.CreateDummyArticle(i);
                articles.Add(article);
            }

            return Task.FromResult(result);
        }

        private static Article CreateDummyArticle(int i)
        {
            return new Article {
                Title = "Dummy Article " + i,
                Extract = "Pretty short extract for the dummy Article. Like short content or whatever."
            };
        }

    }

}