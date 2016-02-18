using LokalReporter.App.FormsApp.ViewModels;
using Xamarin.Forms;

namespace LokalReporter.App.FormsApp.Pages {

    public partial class FeedsPage : ContentPage {
        public FeedsPage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (this.BindingContext is PersonalFeedsViewModel) {
                AddFilterButton.IsVisible = true;
            }
            else {
                AddFilterButton.IsVisible = false;
            }
        }
    }

}