using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using LokalReporter.Client.Dummy.Data.Reading;
using LokalReporter.Responses;

namespace LokalReporter.Client.Dummy.Json
{
    internal class JsonArticlesReader
    {

        private readonly object lo = new object();
        private static Task<IEnumerable<Article>> _readTask;

        public Task<IEnumerable<Article>> ReadAsync()
        {
            lock (this.lo)
            {
                return _readTask ?? (_readTask = this.ReadTask());
            }
        }

        private async Task<IEnumerable<Article>> ReadTask()
        {
            var articles = await DataReader.ReadFromEmbeddedResourceAsync<List<Article>>(this.GetType().GetTypeInfo().Assembly, "LokalReporter.Client.Dummy.articles.json");

            return articles.Distinct().Select(a =>
            {
                if (a.Images.Count == 0)
                {
                    a.Images.Add(new Image("", 0, 0));
                }
                else
                {
                    a.Images = a.Images.OrderByDescending(i => i.Height).ToList();
                }
                return a;
            });
        }

    }
}