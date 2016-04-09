using System;
using System.Collections.Generic;

using MvvmCross.Core.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;

using Newtonsoft.Json;

namespace LokalReporter.App.FormsApp.Helpers
{
    public static class NavigationExtensions {
        public static void ShowViewModelWithComplexParameter<TViewmodel, TParameter>(this IMvxViewModel viewmodel, TParameter parameter)
        {
            viewmodel.ShowViewModelWithComplexParameter(typeof (TViewmodel), parameter);
        }

        public static void ShowViewModel(this IMvxViewModel viewModel, Type targetViewModelType)
        {
            viewModel.ShowViewModelWithComplexParameter(targetViewModelType, null);
        }

        public static void ShowViewModel<TTargetViewModel>(this IMvxViewModel viewModel)
        {
            viewModel.ShowViewModel(typeof (TTargetViewModel));
        }

        public static void ShowViewModelWithComplexParameter(this IMvxViewModel viewModel, Type targetViewModelType, object parameter)
        {
            MvxTrace.Trace("Showing ViewModel {0}", targetViewModelType.Name);
            IMvxViewDispatcher viewDispatcher = Mvx.Resolve<IMvxViewDispatcher>();

            if (parameter != null) {
                var paramString = JsonConvert.SerializeObject(parameter);
                IDictionary<string, string> dict = new Dictionary<string, string> {{"param", paramString}};
                viewDispatcher.ShowViewModel(new MvxViewModelRequest(targetViewModelType, new MvxBundle(dict), null, null));
            }
            else {
                viewDispatcher.ShowViewModel(new MvxViewModelRequest(targetViewModelType, null, null, null));
            }
        }

        public static TParameter GetComplexParameter<TParameter>(this IMvxBundle bundle)
        {
            var paramString = bundle.SafeGetData()["param"];
            return JsonConvert.DeserializeObject<TParameter>(paramString);
        }
    }
}