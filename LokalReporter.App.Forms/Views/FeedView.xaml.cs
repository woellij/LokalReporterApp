using System;
using LokalReporter.App.FormsApp.ViewModels;
using NControl.Controls;

namespace LokalReporter.App.FormsApp.Views {

    public partial class FeedView {
        public FeedView()
        {
            InitializeComponent();
        }

        private void GalleryView_OnClicked(object sender, EventArgs e)
        {
            if (sender is GalleryView) {
                var article = ((GalleryView) sender)?.Page?.BindingContext;
                if (article != null) {
                    ((FeedViewModel) this.BindingContext).Articles.ShowDetails.Execute(article);
                }
            }
        }
    }

}