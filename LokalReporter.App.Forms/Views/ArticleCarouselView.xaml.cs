using System;
using System.Collections;
using LokalReporter.App.FormsApp.ViewModels;
using NControl.Controls;
using Xamarin.Forms;

namespace LokalReporter.App.FormsApp.Views {

    public partial class ArticleCarouselView {
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create<ArticleCarouselView, object>(b => b.ItemsSource, default(object),
                propertyChanged: ItemsSourcePropertyChanged);

        public ArticleCarouselView()
        {
            InitializeComponent();
        }

        public object ItemsSource
        {
            get { return this.GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        private static void ItemsSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var articles = newValue as IEnumerable;
            var view = bindable as ArticleCarouselView;
            if (view != null) {
                if (articles != null) {
                    foreach (var article in articles) {
                        try {
                            view.BatchBegin();
                            view.Children.Add(new BigArticleView {BindingContext = article});
                            view.BatchCommit();
                        }
                        catch {}
                    }
                }
                else {
                    view.Children.Clear();
                }
            }
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            var article = ((GalleryView)sender)?.Page?.BindingContext;
            if (article != null)
            {
                ((FilteredArticlesViewModel)this.BindingContext).ShowDetails.Execute(article);
            }
        }
    }

}