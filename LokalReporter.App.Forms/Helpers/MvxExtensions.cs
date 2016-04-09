using System;

using MvvmCross.Core.ViewModels;

namespace LokalReporter.App.FormsApp.Helpers
{
    public static class MvxExtensions {
        public static TViewModel LoadViewModel<TViewModel>(this IMvxViewModelLocator locator) where TViewModel : IMvxViewModel
        {
            return (TViewModel) locator.LoadViewModel(typeof (TViewModel));
        }

        private static object LoadViewModel(this IMvxViewModelLocator locator, Type type)
        {
            return locator.Load(type, null, null);
        }
    }
}