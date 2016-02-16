using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using LokalReporter.Responses;
using Newtonsoft.Json;

namespace LokalReporter.Client.Dummy {

    internal class JsonArticlesReader {
        public Task<List<Article>> ReadAsync()
        {
            return Task.Run(() => {

                using (
                    Stream stream =
                        this.GetType().GetTypeInfo().Assembly.GetManifestResourceStream("LokalReporter.Client.Dummy.articles.json")) {
                    using (TextReader reader = new StreamReader(stream)) {
                        return JsonSerializer.Create().Deserialize<List<Article>>(new JsonTextReader(reader));
                    }
                }
            });
        }
    }

}