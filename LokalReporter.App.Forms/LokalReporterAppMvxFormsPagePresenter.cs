using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using LokalReporter.App.FormsApp.Pages;
using LokalReporter.App.FormsApp.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using MvvmCross.Forms.Presenter.Core;
using MvvmCross.Platform;
using Xamarin.Forms;

namespace LokalReporter.App.FormsApp {

    public class LokalReporterAppMvxFormsPagePresenter : MvxFormsPagePresenter {
        private bool isOnMasterDetail;



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
                this.isOnMasterDetail = true;
                
            }

            if (this.isOnMasterDetail) {
                Page mainPage = (NavigationPage) ((MasterDetailPage) Application.Current.MainPage).Detail;
                await this.TryShow(request, mainPage.Navigation);
            }
            else {
                base.Show(request);
            }
        }

        private async Task<bool> TryShow(MvxViewModelRequest request, INavigation navigation)
        {
            Page page;
            if (request.ViewModelType.GetTypeInfo().ImplementedInterfaces.Contains(typeof(IFeedsViewModel))) {
                page = new FeedsPage();
            }
            else {

                page = MvxPresenterHelpers.CreatePage(request);
            }

            bool flag;
            if (page == null) {
                flag = false;
            }
            else {
                IMvxViewModel mvxViewModel = MvxPresenterHelpers.LoadViewModel(request);
                page.BindingContext = mvxViewModel;

                try {
                    await navigation.PushAsync(page);
                }
                catch (Exception ex) {
                    Mvx.Error("Exception pushing {0}: {1}\n{2}", (object) page.GetType(), (object) ex.Message, (object) ex.StackTrace);
                }
            }

            flag = true;
            return flag;
        }
    }


}