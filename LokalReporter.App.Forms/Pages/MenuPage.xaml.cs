using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using MenuItem = LokalReporter.App.FormsApp.ViewModels.MenuItem;

namespace LokalReporter.App.FormsApp.Pages {

    public partial class MenuPage : ContentPage {
        public MenuPage()
        {
            InitializeComponent();
        }

        public async void OnNavigated(Page page)
        {
            await Task.Yield();
            IEnumerable<MenuItem> menuItems = this.MenuListView.ItemsSource.OfType<IEnumerable<MenuItem>>().SelectMany(ien => ien);
            var newSelected = menuItems.FirstOrDefault(mi => mi.TargetViewModelType == page?.BindingContext?.GetType());
            newSelected = newSelected ?? menuItems.FirstOrDefault(mi => mi.Title == page?.Title);
            this.MenuListView.SelectedItem = newSelected;
        }
    }

}