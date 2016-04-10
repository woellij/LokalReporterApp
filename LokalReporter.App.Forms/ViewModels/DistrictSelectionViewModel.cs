using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LokalReporter.Responses;

using PropertyChanged;

namespace LokalReporter.App.FormsApp.ViewModels
{
    [ImplementPropertyChanged]
    public class DistrictSelectionViewModel : BaseViewModel
    {

        private readonly IArticlesService articlesService;

        private District selectedDistrict;
        private int selectedDistrictIndex;

        public DistrictSelectionViewModel(IArticlesService articlesService)
        {
            this.articlesService = articlesService;
        }

        public int SelectedDistrictIndex
        {
            get
            {
                return this.selectedDistrictIndex < 0
                    ? this.SelectedDistrict != null ? this.Districts.IndexOf(this.SelectedDistrict) : -1
                    : this.selectedDistrictIndex;
            }
            set
            {
                this.selectedDistrictIndex = value;
                this.SelectedDistrict = this.Districts.ElementAtOrDefault(value);
            }
        }

        public District SelectedDistrict
        {
            get { return this.selectedDistrict; }
            set
            {
                this.selectedDistrict = value;
                this.OnSelectedDistrictChanged(value);
            }
        }

        public IList<District> Districts { get; set; }

        protected virtual void OnSelectedDistrictChanged(District value)
        {
        }

        public override async void Start()
        {
            base.Start();
            await this.StartAsync();
        }

        protected virtual async Task StartAsync()
        {
            this.Districts = (await this.articlesService.GetDistrictsAsync(this.CloseCancellationToken)).ToList();
        }



    }
}