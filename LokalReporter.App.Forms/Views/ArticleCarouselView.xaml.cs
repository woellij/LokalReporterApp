using System;
using System.Collections;
using System.Collections.Generic;

using LokalReporter.App.FormsApp.ViewModels;

using NControl.Controls;

using Xamarin.Forms;

using Point = NGraphics.Point;

namespace LokalReporter.App.FormsApp.Views
{
    public partial class ArticleCarouselView : GalleryView
    {

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create<ArticleCarouselView, object>(b => b.ItemsSource, default(object),
                propertyChanged: ItemsSourcePropertyChanged);

        public ArticleCarouselView()
        {
            this.InitializeComponent();
            this.Clicked += OnClicked;
        }

        private void OnClicked(object sender, EventArgs eventArgs)
        {
            var article = ((GalleryView)sender)?.Page?.BindingContext;
            if (article != null)
            {
                ((FilteredArticlesViewModel)this.BindingContext).ShowDetails.Execute(article);
            }
        }

        public object ItemsSource
        {
            get { return this.GetValue(ItemsSourceProperty); }
            set { this.SetValue(ItemsSourceProperty, value); }
        }

        private static void ItemsSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var articles = newValue as IEnumerable;
            var galery = bindable as ArticleCarouselView;
            if (galery != null)
            {
                if (articles != null)
                {
                    foreach (var article in articles)
                    {
                        try
                        {
                            galery.BatchBegin();
                            galery.Children.Add(new BigArticleView {BindingContext = article});
                            galery.BatchCommit();
                        }
                        catch
                        {
                        }
                    }
                }
                else
                {
                    try
                    {
                        galery.Children.Clear();
                    }
                    catch
                    {
                    }
                }
            }
        }

    }
}