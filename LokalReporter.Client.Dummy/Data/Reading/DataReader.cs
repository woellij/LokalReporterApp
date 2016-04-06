using System.IO;
using System.Reflection;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace LokalReporter.Client.Dummy.Data.Reading
{
    public class DataReader
    {

        public static Task<TData> ReadFromEmbeddedResourceAsync<TData>(Assembly assembly, params string[] nameSpaceAndFile)
        {
            return Task.Run(() =>
            {
                var file = string.Join(".", nameSpaceAndFile);
                using (var stream = assembly.GetManifestResourceStream(file))
                {
                    using (var jsonReader = new JsonTextReader(new StreamReader(stream)))
                    {
                        var result = JsonSerializer.Create().Deserialize<TData>(jsonReader);
                        return result;
                    }
                }
            });
        }

    }
}