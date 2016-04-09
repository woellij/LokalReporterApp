using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using LokalReporter.App.FormsApp.ViewModels;

using MvvmCross.Binding.ExtensionMethods;

using Xamarin.Forms;

namespace LokalReporter.App.FormsApp.Pages {

    public partial class FeedPage : ContentPage {
        public FeedPage()
        {
            InitializeComponent();
            this.ArticlesListView.ItemAppearing += ArticlesListViewOnItemAppearing;
        }

        private void ArticlesListViewOnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var index = this.GetIndexOf(e.Item);;
            if (index > this.ArticlesListView.ItemsSource.Count() - 5)
            {
                ((FeedViewModel) this.BindingContext).Articles.RestockAction();
            }
        }

        private int GetIndexOf(object itemInView)
        {
            return this.ArticlesListView.ItemsSource.Cast<object>().TakeWhile(item => !item.Equals(itemInView)).Count();
        }

    }

}