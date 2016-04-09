using LokalReporter.App.FormsApp.ViewModels;

using MvvmCross.Core.ViewModels;
using MvvmCross.Forms.Presenter.Core;

namespace LokalReporter.App.FormsApp.Presenter
{
    public class LokalReporterAppMvxFormsPagePresenter : MvxFormsPagePresenter
    {

        public MasterDetailPresenter MasterDetailPresenter { get; set; }

        public override async void Show(MvxViewModelRequest request)
        {
            if (request.ViewModelType == typeof (PersonalFeedsViewModel))
            {
                if (this.MasterDetailPresenter == null)
                {
                    this.MasterDetailPresenter = new MasterDetailPresenter(request);
                    return;
                }
            }

            if (this.MasterDetailPresenter != null)
            {
                await this.MasterDetailPresenter.TryShow(request);
            }
            else
            {
                base.Show(request);
            }
        }

        public override void ChangePresentation(MvxPresentationHint hint)
        {
            if (this.MasterDetailPresenter != null)
            {
                this.MasterDetailPresenter.ChangePresentation(hint);
            }
            else
            {
                base.ChangePresentation(hint);
            }
        }

    }
}