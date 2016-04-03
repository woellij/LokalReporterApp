using System;

using Xamarin.Forms;

namespace LokalReporter.App.FormsApp.Views
{
    public class ToggleImageView : Image
    {

        public static readonly BindableProperty IsCheckedImageSourceProperty = BindableProperty.Create("IsCheckedImageSource", typeof (ImageSource), typeof (ToggleImageView), default(ImageSource));

        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create("IsChecked", typeof (bool), typeof (ToggleImageView), default(bool), BindingMode.TwoWay);
        private ImageSource defaultSource;

        public ToggleImageView()
        {
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += this.OnTapped;
            this.GestureRecognizers.Add(tapGestureRecognizer);
        }

        public ImageSource IsCheckedImageSource
        {
            get { return (ImageSource) this.GetValue(IsCheckedImageSourceProperty); }
            set { this.SetValue(IsCheckedImageSourceProperty, value); }
        }

        public bool IsChecked
        {
            get { return (bool) this.GetValue(IsCheckedProperty); }
            set { this.SetValue(IsCheckedProperty, value); }
        }


        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == SourceProperty.PropertyName)
            {
                if (this.defaultSource == null)
                    this.defaultSource = this.Source;
            }

            if (propertyName == IsCheckedProperty.PropertyName)
            {
                this.Source = this.IsChecked ? this.IsCheckedImageSource : this.defaultSource;
            }
        }


        private void OnTapped(object sender, EventArgs eventArgs)
        {
            this.IsChecked = !this.IsChecked;
        }

    }
}