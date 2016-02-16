using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LokalReporter.Requests;
using LokalReporter.Responses;
using Newtonsoft.Json;

namespace LokalReporter.Client.Dummy {

    public class XmlArticlesService : IArticlesService {
        private readonly IDictionary<string, string> districts = new Dictionary<string, string> {
            {"unterfranken", "Unterfranken"},
            {"mittelfranken", "Mittelfranken"},
            {"oberfranken", "Oberfranken"},
            {"oberpfalz", "Oberpfalz"},
            {"niederbayern", "Niederbayern"},
            {"oberbayern", "Oberbayern"},
            {"schwaben", "Schwaben"}
        };

        private readonly Random random = new Random();

        private readonly IDictionary<string, string> resorts = new Dictionary<string, string> {
            {"wetter", "Wetter"},
            {"sport", "Sport"},
            {"verkehr", "Verkehr"},
            {"polizei", "Polizei"},
            {"ratgeber", "Ratgeber"},
            {"kultur", "Kultur"},
            {"religion", "Religion"}
        };

        private IDictionary<string, Article> articles;

        public async Task<ArticlesResult> GetArticlesAsync(Filter filter, CancellationToken cancellationToken)
        {
            await this.EnsureXmlLoaded();
            return new ArticlesResult {Articles = this.articles.Values.ToList()};
        }

        public Task<Article> GetArticleAsync(string id, CancellationToken cancellationToken)
        {
            Article a = null;
            this.articles.TryGetValue(id, out a);
            return Task.FromResult(a);
        }

        private async Task EnsureXmlLoaded()
        {
            if (this.articles == null) {
                List<Article> list = await new XmlArticlesReader().ReadArticlesAsync();
                list = list.Distinct().ToList();
                //var articles = list.Where(a => a.Tags.Count > 0).ToList();
                //var orderedTags = articles.SelectMany(a => a.Tags).GroupBy(t => t.Id).OrderByDescending(g => g.Count()).Select(g => $"{g.Key}: {g.Count()}").ToList();
                
                foreach (var article in list) {
                    var bezirk = this.GetBezirk(article);
                    if (bezirk != null) {
                        article.District = bezirk;
                    }

                    IEnumerable<Category> resorts = this.GetResort(article);
                    if (resorts != null) {
                        article.Categories.AddRange(resorts);
                    }
                }
                //var batchSize = 500;
                //for (int i = 0;; i++) {
                //    try {

                //        var l = list.Skip(batchSize*i).Take(batchSize).ToList();
                //        var jsonArticles = JsonConvert.SerializeObject(l);
                //        Debug.WriteLine(jsonArticles);
                //    }
                //    catch {
                //        break;
                //    }
                //}
                var jsonArticles = JsonConvert.SerializeObject(list);
                Debug.WriteLine(jsonArticles);
                this.articles = list.Distinct().ToDictionary(a => a.Id);
            }
        }

        private IEnumerable<Category> GetResort(Article article)
        {
            return this.Categories<Category>(article, this.resorts);
        }

        private IEnumerable<T> Categories<T>(Article article, IDictionary<string, string> dictionary) where T : IdEntity, new()
        {
            var tags = article.Tags;

            var keys = dictionary.Keys;
            var resortIds =
                keys.Where(key => tags.Select(t => t.Id).Any(key.Contains))
                    .Concat(keys.Where(key => article.HtmlContent.ToLowerInvariant().Contains(key)));
            try {
                return resortIds.Select(resortId => Entities.From<T>(resortId, dictionary[resortId]));
            }
            catch {
                return null;
            }
        }

        private District GetBezirk(Article article)
        {
            return this.Categories<District>(article, this.districts).FirstOrDefault() ?? this.RandomDistrict();
        }

        private District RandomDistrict()
        {
            var rndm = this.random.Next(this.districts.Count);
            var pair = this.districts.ElementAt(rndm);
            return Entities.From<District>(pair.Key, pair.Value);
        }
    }

}