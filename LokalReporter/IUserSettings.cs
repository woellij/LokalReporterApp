using LokalReporter.Common;
using LokalReporter.Responses;

namespace LokalReporter
{
    public interface IUserSettings
    {

        IAsyncSetting<District> DistrictSetting { get; }

        ICollectionAsyncSetting<FilterPreset> UserFiltersSetting { get; }

        ICollectionAsyncSetting<int> BookmarksSetting { get; } 

    }
}