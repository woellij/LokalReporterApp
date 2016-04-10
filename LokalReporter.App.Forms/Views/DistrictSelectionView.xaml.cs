using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using Xamarin.Forms;

using XLabs;

namespace LokalReporter.App.FormsApp.Views
{
    public partial class DistrictSelectionView
    {

        public static readonly BindableProperty SelectedDistrictProperty = BindableProperty.Create("SelectedDistrict", typeof (object), typeof (DistrictSelectionView), default(object), BindingMode.TwoWay);

        public DistrictSelectionView()
        {
            this.InitializeComponent();
            this.Init(this.Map.Children.OfType<ToggleImageView>());
        }

        public object SelectedDistrict
        {
            get { return this.GetValue(SelectedDistrictProperty); }
            set { this.SetValue(SelectedDistrictProperty, value); }
        }

        private void Init(IEnumerable<ToggleImageView> views)
        {
            foreach (var toggleImageView in views)
            {
                toggleImageView.PropertyChanged += this.ToggleImageViewOnPropertyChanged;
            }
        }

        private void ToggleImageViewOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName != ToggleImageView.IsCheckedProperty.PropertyName)
            {
                return;
            }

            var changedView = sender as ToggleImageView;
            if (changedView != null)
            {
                if (changedView.IsChecked)
                {
                    // only untoggle others when new one is toggled
                    var toggleImageViews = this.Map.Children.OfType<ToggleImageView>().ToList();
                    var imageViews = toggleImageViews.Where(tiv => tiv != sender);
                    foreach (var tiv in imageViews)
                    {
                        tiv.IsChecked = false;
                    }
                }


                this.SelectedDistrict = changedView.IsChecked ? changedView.BindingContext : null;
            }
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == SelectedDistrictProperty.PropertyName)
            {
                var toggleImageViews = this.Map.Children.OfType<ToggleImageView>().ToList();
                if (this.SelectedDistrict == null)
                {
                    foreach (var tiv in toggleImageViews)
                    {
                        tiv.IsChecked = false;
                    }
                }
                else
                    toggleImageViews.Where(i => i.BindingContext.Equals(this.SelectedDistrict)).ForEach(i => i.IsChecked = true).ToList();
            }
        }

    }
}