using LokalReporter.App.FormsApp.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Forms.Presenter.Core;

namespace LokalReporter.App.FormsApp {

    public class LokalReporterAppMvxFormsPagePresenter : MvxFormsPagePresenter {
        public MasterDetailPresenter MasterDetailPresenter { get; set; }

        public override async void Show(MvxViewModelRequest request)
        {
            if (request.ViewModelType == typeof (PersonalFeedsViewModel)) {
                if (this.MasterDetailPresenter == null) {
                    this.MasterDetailPresenter = new MasterDetailPresenter();
                }
            }

            if (this.MasterDetailPresenter != null) {
                await this.MasterDetailPresenter.TryShow(request);
            }
            else {
                base.Show(request);
            }
        }
    }

}