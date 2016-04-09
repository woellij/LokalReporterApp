using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LokalReporter.App.FormsApp.Helpers;
using LokalReporter.Common;
using LokalReporter.Requests;
using LokalReporter.Responses;

using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

using PropertyChanged;

namespace LokalReporter.App.FormsApp.ViewModels
{
    [ImplementPropertyChanged]
    public class MultiFilteredArticlesViewModel : BaseViewModel, IFeedsViewModel
    {

        private readonly IArticlesService service;
        private readonly IUserSettings userSettings;

        public MultiFilteredArticlesViewModel(IArticlesService service, IUserSettings userSettings)
        {
            this.service = service;
            this.userSettings = userSettings;
        }

        public ICollection<FeedViewModel> Feeds { get; set; }

        protected override async void InitFromBundle(IMvxBundle parameters)
        {
            var filterPreset = parameters.GetComplexParameter<FilterPreset>();
            this.Title = filterPreset.Title;

            var filter = filterPreset.Filter;

            if (filter.Category == null && filter.District != null)
            {
                var categories = await this.service.GetCategoriesAsync(this.CloseCancellationToken);
                this.SetViewModels(categories, filter, (f, entity) => f.Category = entity);
            }
            else if (filter.District == null && filter.Category != null)
            {
                var setDistrict = await this.userSettings.DistrictSetting.GetValueAsync();
                var districts = await this.service.GetDistrictsAsync(this.CloseCancellationToken);
                this.SetViewModels(
                    districts.OrderByDescending(
                        district => district.Equals(setDistrict)),
                    filter,
                    (f, entity) => f.District = entity);
            }
        }

        private void SetViewModels<TEntitiy>(IEnumerable<TEntitiy> byEntity, Filter filter,
            Action<Filter, TEntitiy> setter)
            where TEntitiy : IdEntity
        {
            this.Feeds = byEntity.Select((entity, i) =>
            {
                var filterClone = filter.Clone();
                setter(filter, entity);
                var viewModel = Mvx.IocConstruct<FeedViewModel>();
                Task.Delay(500*i)
                    .ContinueWith(t =>
                    {
                        var filterPreset = new FilterPreset {Title = entity.Name, ExtendedTitle = $"{this.Title} - {entity.Name}", Filter = filterClone};
                        viewModel.Setup(filterPreset, true);
                    },
                        TaskScheduler.FromCurrentSynchronizationContext());
                return viewModel;
            }).ToList();
        }

    }
}