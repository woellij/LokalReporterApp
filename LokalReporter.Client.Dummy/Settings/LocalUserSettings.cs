using System.Collections.Generic;

using LokalReporter.Common;
using LokalReporter.Responses;

namespace LokalReporter.Client.Dummy.Settings
{
    public class LocalUserSettings : IUserSettings
    {
        
        public IAsyncSetting<District> DistrictSetting
        {
            get { return new SettingsAsyncSetting<District>("district", null); }
        }

        public ICollectionAsyncSetting<FilterPreset> UserFiltersSetting =>
            new CollectionAsyncSetting<FilterPreset>("filter_presets",
                new List<FilterPreset>());

        public ICollectionAsyncSetting<int> BookmarksSetting { get; } = new CollectionAsyncSetting<int>("bookmarks", new List<int>());

    }
}