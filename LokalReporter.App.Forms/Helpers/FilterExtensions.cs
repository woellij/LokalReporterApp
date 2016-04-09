using Newtonsoft.Json;

namespace LokalReporter.App.FormsApp.Helpers {

    public static class FilterExtensions {
        public static T Clone<T>(this T data)
        {
            var s = JsonConvert.SerializeObject(data);
            return JsonConvert.DeserializeObject<T>(s);
        }
    }
}