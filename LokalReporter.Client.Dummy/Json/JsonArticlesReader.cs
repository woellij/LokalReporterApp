using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using LokalReporter.Responses;
using Newtonsoft.Json;

namespace LokalReporter.Client.Dummy.Json {

    internal class JsonArticlesReader {
        private readonly object lo = new object();
        private Task<List<Article>> readTask;

        public Task<List<Article>> ReadAsync()
        {
            lock (lo) {
                return this.readTask ?? (this.readTask = this.ReadTask());
            }
        }

        private Task<List<Article>> ReadTask()
        {
            return Task.Run(() => {
                using (
                    Stream stream =
                        this.GetType().GetTypeInfo().Assembly.GetManifestResourceStream("LokalReporter.Client.Dummy.articles.json")) {
                    using (TextReader reader = new StreamReader(stream)) {
                        var articles = JsonSerializer.Create().Deserialize<List<Article>>(new JsonTextReader(reader));
                        //articles.Shuffle();
                        return articles;
                    }
                }
            });
        }
    }

}