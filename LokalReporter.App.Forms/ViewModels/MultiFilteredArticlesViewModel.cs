using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LokalReporter.App.FormsApp.Helpers;
using LokalReporter.Requests;
using LokalReporter.Responses;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using PropertyChanged;

namespace LokalReporter.App.FormsApp.ViewModels {

    [ImplementPropertyChanged]
    public class MultiFilteredArticlesViewModel : BaseViewModel, IFeedsViewModel {
        private readonly IArticlesService service;

        public MultiFilteredArticlesViewModel(IArticlesService service)
        {
            this.service = service;
        }

        public string Title { get; set; }

        public ICollection<FeedViewModel> Feeds { get; set; }

        protected override async void InitFromBundle(IMvxBundle parameters)
        {
            var filterPreset = parameters.GetComplexParameter<FilterPreset>();
            this.Title = filterPreset.Title;

            var filter = filterPreset.Filter;

            if (filter.Category == null && filter.District != null) {
                var categories = await this.service.GetCategoriesAsync(this.CloseCancellationToken);
                this.SetViewModels(categories, filter, (f, entity) => f.Category = entity);
            }
            else if (filter.District == null && filter.Category != null) {
                var districts = await this.service.GetDistrictsAsync(this.CloseCancellationToken);
                this.SetViewModels(districts.OrderByDescending(district => district.Equals(Settings.SelectedDistrict)), filter,
                    (f, entity) => f.District = entity);
            }
        }

        private void SetViewModels<TEntitiy>(IEnumerable<TEntitiy> byEntity, Filter filter, Action<Filter, TEntitiy> setter)
            where TEntitiy : IdEntity
        {
            this.Feeds = byEntity.Select((entity, i) => {
                var filterClone = filter.Clone();
                setter(filter, entity);
                var viewModel = Mvx.IocConstruct<FeedViewModel>();
                Task.Delay(500*i)
                    .ContinueWith(t => {
                        var filterPreset = new FilterPreset {Title = entity.Name, Filter = filterClone};
                        viewModel.Setup(filterPreset);
                    },
                        TaskScheduler.FromCurrentSynchronizationContext());
                return viewModel;
            }).ToList();
        }
    }

}