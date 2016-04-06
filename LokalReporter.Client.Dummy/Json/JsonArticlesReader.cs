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
        private Task<List<Article>> readTask;

        public Task<List<Article>> ReadAsync()
        {
            lock (this.lo)
            {
                return this.readTask ?? (this.readTask = this.ReadTask());
            }
        }

        private async Task<List<Article>> ReadTask()
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
            }).ToList();
        }

    }
}