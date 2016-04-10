using System.Collections.Generic;
using System.Threading.Tasks;

using LokalReporter.Common;
using LokalReporter.Responses;

using PropertyChanged;

namespace LokalReporter.App.FormsApp.ViewModels
{
    public class DistrictSettingViewModel : DistrictSelectionViewModel
    {

        private readonly IAsyncSetting<District> setting;

        public DistrictSettingViewModel(IArticlesService articlesService, IUserSettings userSettings)
            : base(articlesService)
        {
            this.setting = userSettings.DistrictSetting;
        }

        public IReadOnlyCollection<string> DistrictNames { get; set; }

        protected override void OnSelectedDistrictChanged(District value)
        {
            this.setting.SetValueAsync(value);
        }

        protected override async Task StartAsync()
        {
            await base.StartAsync();
            this.SelectedDistrict = await this.setting.GetValueAsync();
        }

    }
}