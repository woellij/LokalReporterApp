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

namespace LokalReporter.App.FormsApp.Presenter
{
    public class MasterDetailPresenter : MvxFormsPagePresenter
    {

        private readonly MasterDetailPage masterDetailPage;
        private readonly MenuPage menuPage;

        public MasterDetailPresenter(MvxViewModelRequest request)
        {
            this.masterDetailPage = new LokalReporterMasterDetailPage();
            this.menuPage = new MenuPage
            {
                Title = "Menu",
                BindingContext =
                    MvxPresenterHelpers.LoadViewModel(new MvxViewModelRequest(typeof (MenuViewModel), null, null, null))
            };
            this.masterDetailPage.Master = this.menuPage;

            var navigationPage = new MainPage();
            this.masterDetailPage.Detail = navigationPage;
            
            this.TryShow(request);

            Application.Current.MainPage = this.masterDetailPage;

            navigationPage.Popped +=
                (sender, args) => this.NavigationPageOnNavigated(sender, args, NavigationEventType.Popped);
            navigationPage.Pushed +=
                (sender, args) => this.NavigationPageOnNavigated(sender, args, NavigationEventType.Pushed);
        }

        private void NavigationPageOnNavigated(object sender, NavigationEventArgs navigationEventArgs,
            NavigationEventType type)
        {
            this.masterDetailPage.IsPresented = false;
            var currentPage = ((NavigationPage) sender).CurrentPage;
            ((MenuViewModel)this.menuPage.BindingContext).OnNavigated(currentPage);

            (currentPage.BindingContext as INavigatedToAware)?.OnNavigatedTo(type);
        }

        public async Task TryShow(MvxViewModelRequest request, INavigation navigation)
        {
            Page page = null;
            if (request.ViewModelType.GetTypeInfo().ImplementedInterfaces.Contains(typeof (IFeedsViewModel)))
            {
                page = new FeedsPage();
            }
            else
            {
                try
                {
                    page = MvxPresenterHelpers.CreatePage(request);
                }
                catch (Exception e)
                {
                    
                }
            }

            bool flag;
            if (page == null)
            {
                flag = false;
            }
            else
            {
                IMvxViewModel mvxViewModel = MvxPresenterHelpers.LoadViewModel(request);
                page.BindingContext = mvxViewModel;

                var busy = Binding.Create<BaseViewModel>(vm => vm.IsLoading);
                page.SetBinding(Page.IsBusyProperty, busy);

                var titleBinding = Binding.Create<BaseViewModel>(vm => vm.Title);
                page.SetBinding(Page.TitleProperty, titleBinding);

                try
                {
                    await navigation.PushAsync(page);
                }
                catch (Exception ex)
                {
                    Mvx.Error("Exception pushing {0}: {1}\n{2}", (object) page.GetType(), (object) ex.Message,
                        (object) ex.StackTrace);
                }
            }

            flag = true;
        }

        public Task TryShow(MvxViewModelRequest request)
        {
            return this.TryShow(request, this.masterDetailPage.Detail.Navigation);
        }

        public async void ChangePresentation(MvxPresentationHint hint)
        {
            if (hint is MvxClosePresentationHint)
            {
                Page page = await ((NavigationPage) this.masterDetailPage.Detail).PopAsync();
            }
        }

    }
}