using System.Collections.Generic;
using System.Linq;
using LokalReporter.App.FormsApp.ViewModels;
using LokalReporter.Client.Dummy;
using LokalReporter.Requests;
using LokalReporter.Responses;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace LokalReporter.App.FormsApp.Helpers {

    /// <summary>
    ///     This is the Settings static class that can be used in your Core solution or in any
    ///     of your client applications. All settings are laid out the same exact way with getters
    ///     and setters.
    /// </summary>
    public static class Settings {
        private static readonly string districtKey = "district";

        private static ISettings AppSettings
        {
            get { return CrossSettings.Current; }
        }

        public static string GeneralSettings
        {
            get { return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault); }
            set { AppSettings.AddOrUpdateValue(SettingsKey, value); }
        }

        public static District SelectedDistrict
        {
            get { return AppSettings.GetComplexValueOrDefault<District>(districtKey); }
            set { AppSettings.AddOrUpdateComplexValue(districtKey, value); }
        }

        public static IReadOnlyCollection<FilterPreset> UserFeedFilters
        {
            get
            {
                var complexValueOrDefault = AppSettings.GetComplexValueOrDefault<List<FilterPreset>>("feedFilters") ?? new List<FilterPreset>();
                return complexValueOrDefault;
            }
            set { AppSettings.AddOrUpdateComplexValue("feedFilters", value); }
        }

        public static void AddFeedFilter(FilterPreset filter)
        {
            var filters = UserFeedFilters;
            var current = filters as List<FilterPreset> ?? filters.ToList();
            current.Add(filter);
            UserFeedFilters = current;
        }

        #region Setting Constants

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;

        #endregion
    }

}