using System;

using JetBrains.Annotations;

using Newtonsoft.Json;

namespace LokalReporter.App.FormsApp.Helpers
{
    public static class FilterExtensions
    {

        [NotNull]
        public static T Clone<T>(this T data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            var s = JsonConvert.SerializeObject(data);
            return JsonConvert.DeserializeObject<T>(s);
        }

    }
}