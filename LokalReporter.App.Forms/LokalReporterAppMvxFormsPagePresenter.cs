using LokalReporter.App.FormsApp.Pages;
using LokalReporter.App.FormsApp.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using MvvmCross.Forms.Presenter.Core;
using Xamarin.Forms;
using XLabs.Forms.Pages;

namespace LokalReporter.App.FormsApp {

    public class LokalReporterAppMvxFormsPagePresenter : MvxFormsPagePresenter {



        public override async void Show(MvxViewModelRequest request)
        {
            
            if (request.ViewModelType == typeof (PersonalFeedsViewModel)) {

                var masterDetailPage = new MasterDetailPage();
                masterDetailPage.Master = new MenuPage {
                    Title = "test",
                    BindingContext = MvxPresenterHelpers.LoadViewModel(new MvxViewModelRequest(typeof (MenuViewModel), null, null, null))
                };

                var navigationPage = new NavigationPage();
                masterDetailPage.Detail = navigationPage;
                Application.Current.MainPage = masterDetailPage;
                this.MasterDetailPresenter = new MasterDetailPresenter(masterDetailPage);
                }

            if (this.MasterDetailPresenter != null) {
                await this.MasterDetailPresenter.TryShow(request);
            }
            else {
                base.Show(request);
            }
        }

        public MasterDetailPresenter MasterDetailPresenter { get; set; }

    }

}