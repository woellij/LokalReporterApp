using System.Threading.Tasks;

using LokalReporter.App.FormsApp.Helpers;
using LokalReporter.Client.Dummy.Json;
using LokalReporter.Common;

using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace LokalReporter.Client.Dummy.Settings
{
    internal class SettingsAsyncSetting<T> : IAsyncSetting<T>
    {

        private readonly T defaultValue;

        private readonly string key;

        public SettingsAsyncSetting(string key, T defaultValue)
        {
            this.key = key;
            this.defaultValue = defaultValue;
        }

        private ISettings Settings => CrossSettings.Current;

        public Task<T> GetValueAsync()
        {
            return Task.Run(() => this.Settings.GetComplexValueOrDefault(this.key, this.defaultValue));
        }

        public Task SetValueAsync(T value)
        {
            return Task.Run(() => this.Settings.AddOrUpdateComplexValue(this.key, value));
        }

    }
}