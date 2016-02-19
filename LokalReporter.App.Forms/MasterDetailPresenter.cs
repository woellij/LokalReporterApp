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

namespace LokalReporter.App.FormsApp {

    public class MasterDetailPresenter  {
        private readonly MasterDetailPage masterDetailPage;

        public MasterDetailPresenter(MasterDetailPage masterDetailPage)
        {
            this.masterDetailPage = masterDetailPage;
            ((NavigationPage)masterDetailPage.Detail).Popped += NavigationPageOnNavigated;
            ((NavigationPage)masterDetailPage.Detail).Pushed += NavigationPageOnNavigated;
        }

        private void NavigationPageOnNavigated(object sender, NavigationEventArgs navigationEventArgs)
        {
            this.masterDetailPage.IsPresented = false;
        }

        public async Task TryShow(MvxViewModelRequest request, INavigation navigation)
        {
            Page page;
            if (request.ViewModelType.GetTypeInfo().ImplementedInterfaces.Contains(typeof(IFeedsViewModel)))
            {
                page = new FeedsPage();
            }
            else {

                page = MvxPresenterHelpers.CreatePage(request);
            }

            bool flag;
            if (page == null)
            {
                flag = false;
            }
            else {
                IMvxViewModel mvxViewModel = MvxPresenterHelpers.LoadViewModel(request);
                page.BindingContext = mvxViewModel;

                try
                {
                    await navigation.PushAsync(page);
                }
                catch (Exception ex)
                {
                    Mvx.Error("Exception pushing {0}: {1}\n{2}", (object)page.GetType(), (object)ex.Message, (object)ex.StackTrace);
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