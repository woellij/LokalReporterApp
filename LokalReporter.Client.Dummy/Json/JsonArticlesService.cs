using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using LokalReporter.Client.Dummy.Locations;
using LokalReporter.Requests;
using LokalReporter.Responses;

namespace LokalReporter.Client.Dummy.Json
{
    public class JsonArticlesService : IArticlesService
    {

        private readonly ILocations locations;
        private readonly object lockObject = new object();
        private Dictionary<int, Article> articles;
        private Task loadTask;

        public JsonArticlesService(ILocations locations)
        {
            this.locations = locations;
        }

        public async Task<ArticlesResult> GetArticlesAsync(Filter filter, CancellationToken cancellationToken)
        {
            await this.EnsureLoadedAsync();
            return await Task.Run(() => this.GetArticles(filter), cancellationToken);
        }

        public async Task<Article> GetArticleAsync(string id, CancellationToken cancellationToken)
        {
            await this.EnsureLoadedAsync();
            return this.articles[int.Parse(id)];
        }

        public async Task<IReadOnlyCollection<Category>> GetCategoriesAsync(CancellationToken cancellation)
        {
            await Task.Yield();
            return Entities.Categories;
        }

        public async Task<IReadOnlyCollection<District>> GetDistrictsAsync(CancellationToken cancellation)
        {
            await Task.Yield();
            return Entities.Districts;
        }

        private async Task<ArticlesResult> GetArticles(Filter filter)
        {
            List<DistrictRelation> districtRel = null;
            if (filter.District != null)
            {
                districtRel = await this.locations.GetDistrictRelationsAsync();
            }
            return await Task.Run(() => this.GetArticles(filter, districtRel));
        }

        private ArticlesResult GetArticles(Filter filter, List<DistrictRelation> districtRel)
        {
            lock (this.lockObject)
            {
                var result = this.articles.Values.AsQueryable();

                if (filter.Ids != null && filter.Ids.Any())
                {
                    result = result.Where(a => filter.Ids.Contains(a.Id));
                }

                if (filter.IsTopStory)
                {
                    result = result.Where(a => a.Images.Any(i => !string.IsNullOrWhiteSpace(i.Source)));
                }

                if (filter.District != null)
                {
                    var articleIds = districtRel.FirstOrDefault(relation => relation.DistrictId == filter.District.Id)?.ArticleIds;
                    if (articleIds != null)
                    {
                        result = result.Where(a => articleIds.Contains(a.Id));
                    }
                }

                if (filter.Category != null)
                {
                    result = result.Where(a => a.Categories != null && a.Categories.Any(c => c.Equals(filter.Category)));
                }
                if (filter.Tag != null)
                {
                    result = result.Where(a => a.Tags != null && a.Tags.Any(t => t.Equals(filter.District)));
                }

                //var filtered = result.ToList();
                if (filter.Paging != null)
                {
                    result = result.Skip(filter.Paging.Offset).Take(filter.Paging.Limit);
                }

                return new ArticlesResult {Articles = result.ToList()};
            }
        }

        private async Task EnsureLoadedAsync()
        {
            if (this.articles == null)
            {
                if (this.loadTask == null)
                    this.loadTask = Task.Run(async () => await this.LoadArticlesAsync());
                await this.loadTask;
            }
        }

        private async Task LoadArticlesAsync()
        {
            var articles = await new JsonArticlesReader().ReadAsync();
            this.articles = articles.OrderByDescending(a => a.Date).ToDictionary(a => a.Id, a => a);
        }

    }
}