using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using LokalReporter.App.FormsApp.Pages;
using LokalReporter.App.FormsApp.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Forms.Presenter.Core;
using MvvmCross.Platform;
using Xamarin.Forms;
using MenuItem = LokalReporter.App.FormsApp.ViewModels.MenuItem;

namespace LokalReporter.App.FormsApp {

    public class MasterDetailPresenter {
        private readonly MasterDetailPage masterDetailPage;
        private readonly MenuPage menuPage;

        public MasterDetailPresenter()
        {
            this.masterDetailPage = new MasterDetailPage();
            this.menuPage = new MenuPage {
                Title = "test",
                BindingContext = MvxPresenterHelpers.LoadViewModel(new MvxViewModelRequest(typeof (MenuViewModel), null, null, null))
            };
            masterDetailPage.Master = menuPage;

            var navigationPage = new NavigationPage();
            masterDetailPage.Detail = navigationPage;
            Application.Current.MainPage = masterDetailPage;

            navigationPage.Popped += NavigationPageOnNavigated;
            navigationPage.Pushed += NavigationPageOnNavigated;
        }

        private void NavigationPageOnNavigated(object sender, NavigationEventArgs navigationEventArgs)
        {
            this.masterDetailPage.IsPresented = false;
            this.menuPage.OnNavigated(((NavigationPage)sender).CurrentPage);
        }

        public async Task TryShow(MvxViewModelRequest request, INavigation navigation)
        {
            Page page;
            if (request.ViewModelType.GetTypeInfo().ImplementedInterfaces.Contains(typeof (IFeedsViewModel))) {
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
        }

        public Task TryShow(MvxViewModelRequest request)
        {
            return this.TryShow(request, this.masterDetailPage.Detail.Navigation);
        }
    }

}