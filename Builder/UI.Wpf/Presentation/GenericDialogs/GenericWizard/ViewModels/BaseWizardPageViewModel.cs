using System.ComponentModel;
using System.ComponentModel.Composition;
using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.GenericDialogs.GenericWizard.ViewModels
{
    public abstract class BaseWizardPageViewModel : ViewModelBase, IWizardPage
    {
        protected IViewAwareStatus _viewAwareStatus;

        protected IWizardViewModel _wizardViewModel;

        private bool? _canExecuteCancel;
        private bool? _canExecutePrevious;
        private bool? _canExecuteNext;
        private bool? _canExecuteFinish;
        private bool? _canExecuteClose;
        private FinishAction _finishAction = FinishAction.NEXT;
        private CloseMethod _closeMethod = CloseMethod.CANCEL;

        protected object Tag { get { return _wizardViewModel.Tag; } set { _wizardViewModel.Tag = value; } }

        public virtual string NextPageOverride { get; set; }

        public virtual int? SkipPages { get; set; }

        public abstract bool IsValid { get; set; }

        public virtual bool? CanExecuteCancel
        {
            get { return _canExecuteCancel; }
            set
            {
                _canExecuteCancel = value;
                NotifyPropertyChanged(CanExecuteCancelArgs);
            }
        }

        public virtual bool? CanExecuteNext
        {
            get { return _canExecuteNext; }
            set
            {
                _canExecuteNext = value;
                NotifyPropertyChanged(CanExecuteNextArgs);
            }
        }

        public virtual bool? CanExecutePrevious
        {
            get { return _canExecutePrevious; }
            set
            {
                _canExecutePrevious = value;
                NotifyPropertyChanged(CanExecutePreviousArgs);
            }
        }

        public virtual bool? CanExecuteFinish
        {
            get { return _canExecuteFinish; }
            set
            {
                _canExecuteFinish = value;
                NotifyPropertyChanged(CanExecuteFinishArgs);
            }
        }

        public virtual bool? CanExecuteClose
        {
            get { return _canExecuteClose; }
            set
            {
                _canExecuteClose = value;
                NotifyPropertyChanged(CanExecuteCloseArgs);
            }
        }

        public virtual FinishAction FinishAction
        {
            get { return _finishAction; }
            set
            {
                _finishAction = value;
                NotifyPropertyChanged(FinishActionArgs);
            }
        }

        public virtual CloseMethod CloseMethod
        {
            get { return _closeMethod; }
            set
            {
                _closeMethod = value;
                NotifyPropertyChanged(CloseMethodArgs);
            }
        }

        protected static PropertyChangedEventArgs IsValidArgs = ObservableHelper.CreateArgs<BaseWizardPageViewModel>(x => x.IsValid); protected static PropertyChangedEventArgs NextPageOverrideArgs = ObservableHelper.CreateArgs<BaseWizardPageViewModel>(x => x.NextPageOverride); protected static PropertyChangedEventArgs SkipPagesArgs = ObservableHelper.CreateArgs<BaseWizardPageViewModel>(x => x.SkipPages); protected static PropertyChangedEventArgs CanExecuteCancelArgs = ObservableHelper.CreateArgs<BaseWizardPageViewModel>(x => x.CanExecuteCancel); protected static PropertyChangedEventArgs CanExecutePreviousArgs = ObservableHelper.CreateArgs<BaseWizardPageViewModel>(x => x.CanExecutePrevious); protected static PropertyChangedEventArgs CanExecuteNextArgs = ObservableHelper.CreateArgs<BaseWizardPageViewModel>(x => x.CanExecuteNext); protected static PropertyChangedEventArgs CanExecuteFinishArgs = ObservableHelper.CreateArgs<BaseWizardPageViewModel>(x => x.CanExecuteFinish); protected static PropertyChangedEventArgs CanExecuteCloseArgs = ObservableHelper.CreateArgs<BaseWizardPageViewModel>(x => x.CanExecuteClose); protected static PropertyChangedEventArgs FinishActionArgs = ObservableHelper.CreateArgs<BaseWizardPageViewModel>(x => x.FinishAction); protected static PropertyChangedEventArgs CloseMethodArgs = ObservableHelper.CreateArgs<BaseWizardPageViewModel>(x => x.CloseMethod);
        [ImportingConstructor]
        public BaseWizardPageViewModel(IViewAwareStatus viewAwareStatus)
        {
            _viewAwareStatus = viewAwareStatus;
            _viewAwareStatus.ViewLoaded += baseWizardPage_viewAwareStatus_ViewLoaded;
        }

        private void baseWizardPage_viewAwareStatus_ViewLoaded()
        {
            var view = (IWorkSpaceAware)_viewAwareStatus.View;
            _wizardViewModel = ((WizardPageWorkspaceData)view.WorkSpaceContextualData).WizardViewModel;
            _wizardViewModel.NotifyPageViewLoaded();
        }

        public virtual void OnNext()
        {
        }

        public virtual void OnPrevious()
        {
        }

        public virtual bool OnFinish()
        {
            return true;
        }

        public virtual void OnCancel()
        {
        }

        public virtual bool OnClose()
        {
            return true;
        }
    }
}
