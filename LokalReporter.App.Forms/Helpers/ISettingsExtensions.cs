using System.Collections.Generic;
using LokalReporter.Requests;
using Newtonsoft.Json;
using Plugin.Settings.Abstractions;

namespace LokalReporter.App.FormsApp.Helpers {

    public static class SettingsExtensions {
        public static T GetComplexValueOrDefault<T>(this ISettings settings, string key, T defaul = default(T))
        {
            string value = settings.GetValueOrDefault(key, string.Empty);
            return value == string.Empty ? defaul : JsonConvert.DeserializeObject<T>(value);
        }

        public static void AddOrUpdateComplexValue<T>(this ISettings settings, string key, T value)
        {
            string valueString = EqualityComparer<T>.Default.Equals(default(T), value) ? string.Empty : JsonConvert.SerializeObject(value);
            settings.AddOrUpdateValue(key, valueString);
        }

    }

}