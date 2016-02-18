using System;
using Xamarin.Forms;

namespace LokalReporter.App.FormsApp.Helpers.Behaviors {

    public class BindableBehavior<T> : Behavior<T> where T : BindableObject
    {
        public T AssociatedObject { get; private set; }

        protected override void OnAttachedTo(T visualElement)
        {
            base.OnAttachedTo(visualElement);

            this.AssociatedObject = visualElement;

            if (visualElement.BindingContext != null)
                this.BindingContext = visualElement.BindingContext;

            visualElement.BindingContextChanged += this.OnBindingContextChanged;
        }

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            this.OnBindingContextChanged();
        }

        protected override void OnDetachingFrom(T view)
        {
            view.BindingContextChanged -= this.OnBindingContextChanged;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            this.BindingContext = this.AssociatedObject.BindingContext;
        }
    }

}