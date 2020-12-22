using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using Cinch;
using MEFedMVVM.ViewModelLocator;

namespace Questify.Builder.UI.Wpf.Presentation.GenericDialogs.GenericWizard.ViewModels
{
    [ExportViewModel("WizardViewModel")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class WizardViewModel : ViewModelBase, IWizardViewModel
    {
        private int _pageIndex = 0;

        private bool ViewAvailable { get { return Views.Count > _pageIndex; } }

        private bool IsLastPage { get { return _pageIndex == Views.Count - 1; } }

        private IWizardPage CurrentPageViewModel { get { return ViewAvailable ? (IWizardPage)Views[_pageIndex].ViewModelInstance : null; } }

        private string _title;


        public string Title { get { return _title; } set { _title = value; NotifyPropertyChanged(TitleArgs); } }

        public WorkspaceData CurrentPageWorkspace { get { return ViewAvailable ? Views[_pageIndex] : null; } }

        public bool CanExecuteNext
        {
            get
            {
                if (CurrentPageViewModel == null) return false;
                if (CurrentPageViewModel.CanExecuteNext.HasValue) return CurrentPageViewModel.CanExecuteNext.Value;
                return CurrentPageViewModel.IsValid && !IsLastPage;
            }
        }

        public bool CanExecutePrevious
        {
            get
            {
                if (CurrentPageViewModel == null) return false;
                if (CurrentPageViewModel.CanExecutePrevious.HasValue) return CurrentPageViewModel.CanExecutePrevious.Value;
                return _pageIndex > 0;
            }
        }

        public bool CanExecuteFinish
        {
            get
            {
                if (CurrentPageViewModel == null) return false;
                if (CurrentPageViewModel.CanExecuteFinish.HasValue) return CurrentPageViewModel.CanExecuteFinish.Value;
                return CurrentPageViewModel.IsValid && IsLastPage;
            }
        }

        public bool CanExecuteCancel
        {
            get
            {
                if (CurrentPageViewModel == null) return false;
                if (CurrentPageViewModel.CanExecuteCancel.HasValue) return CurrentPageViewModel.CanExecuteCancel.Value;
                return true;
            }
        }

        public bool CanExecuteClose
        {
            get
            {
                if (CurrentPageViewModel == null) return false;
                if (CurrentPageViewModel.CanExecuteClose.HasValue) return CurrentPageViewModel.CanExecuteClose.Value;
                return true;
            }
        }

        public CloseMethod CloseMethod
        {
            get
            {
                return CurrentPageViewModel != null ? CurrentPageViewModel.CloseMethod : CloseMethod.CANCEL;
            }
        }

        private FinishAction FinishAction
        {
            get
            {
                return CurrentPageViewModel != null ? CurrentPageViewModel.FinishAction : FinishAction.NOTHING;
            }
        }

        public object Tag { get; set; }

        public SimpleCommand<object, object> Next { get; private set; }

        public SimpleCommand<object, object> Previous { get; private set; }

        public SimpleCommand<object, object> Cancel { get; private set; }


        public SimpleCommand<object, object> Finish { get; private set; }

        public SimpleCommand<object, object> Close { get; private set; }

        private static PropertyChangedEventArgs CurrentPageWorkspaceArgs = ObservableHelper.CreateArgs<WizardViewModel>(x => x.CurrentPageWorkspace); private static PropertyChangedEventArgs CurrentPageViewModelArgs = ObservableHelper.CreateArgs<WizardViewModel>(x => x.CurrentPageViewModel); private static PropertyChangedEventArgs CanExecuteNextArgs = ObservableHelper.CreateArgs<WizardViewModel>(x => x.CanExecuteNext); private static PropertyChangedEventArgs CanExecutePreviousArgs = ObservableHelper.CreateArgs<WizardViewModel>(x => x.CanExecutePrevious); private static PropertyChangedEventArgs CanExecuteFinishArgs = ObservableHelper.CreateArgs<WizardViewModel>(x => x.CanExecuteFinish); private static PropertyChangedEventArgs CanExecuteCancelArgs = ObservableHelper.CreateArgs<WizardViewModel>(x => x.CanExecuteCancel); private static PropertyChangedEventArgs CanExecuteCloseArgs = ObservableHelper.CreateArgs<WizardViewModel>(x => x.CanExecuteClose); private static PropertyChangedEventArgs CloseMethodArgs = ObservableHelper.CreateArgs<WizardViewModel>(x => x.CloseMethod); private static PropertyChangedEventArgs FinishActionArgs = ObservableHelper.CreateArgs<WizardViewModel>(x => x.FinishAction); private static PropertyChangedEventArgs TitleArgs = ObservableHelper.CreateArgs<WizardViewModel>(x => x.Title);
        public WizardViewModel()
        {
            Title = "Wizard";
            Next = new SimpleCommand<object, object>((o) => ExecuteNext());
            Previous = new SimpleCommand<object, object>((o) => ExecutePrevious());
            Finish = new SimpleCommand<object, object>((o) => ExecuteFinish());
            Cancel = new SimpleCommand<object, object>((o) => ExecuteCancel());
            Close = new SimpleCommand<object, object>((o) => ExecuteClose());
        }


        public void SetPages(IEnumerable<string> viewLookupKeys)
        {
            Views.Clear();
            _pageIndex = 0;
            foreach (var pageLookupKey in viewLookupKeys)
            {
                var ws = new WizardPageWorkspaceData(pageLookupKey) { WizardViewModel = this };
                Views.Add(ws);
            }
            NotifyPropertyChanged(CurrentPageWorkspaceArgs);
        }

        public void SetPages(IEnumerable<WizardPageWorkspaceData> workspaces)
        {
            Views.Clear();
            _pageIndex = 0;
            foreach (var workspace in workspaces)
            {
                workspace.WizardViewModel = this;
                Views.Add(workspace);
            }
            NotifyPropertyChanged(CurrentPageWorkspaceArgs);
        }

        public void NotifyPageViewLoaded()
        {
            CurrentPageViewModel.PropertyChanged += (s, e) =>
            {
                UpdateButtons();
            };
            NotifyPropertyChanged(CurrentPageViewModelArgs);
            UpdateButtons();
        }

        public void ExecuteNext()
        {
            CurrentPageViewModel.OnNext();
            _pageIndex = (_pageIndex + 1) % Views.Count;
            NotifyPropertyChanged(CurrentPageWorkspaceArgs);
        }

        public void ExecutePrevious()
        {
            CurrentPageViewModel.OnPrevious();
            _pageIndex = (_pageIndex - 1) % Views.Count;
            NotifyPropertyChanged(CurrentPageWorkspaceArgs);
        }

        public void ExecuteCancel()
        {
            CurrentPageViewModel.OnCancel();
            RaiseCloseRequest(false);
        }

        public void ExecuteClose()
        {
            var result = CurrentPageViewModel.OnClose();
            RaiseCloseRequest(result);
        }

        public void ExecuteFinish()
        {
            var result = CurrentPageViewModel.OnFinish();
            switch (FinishAction)
            {
                case FinishAction.CLOSE:
                    RaiseCloseRequest(result);
                    break;
                case FinishAction.NEXT:
                    ExecuteNext();
                    break;
                case FinishAction.NOTHING:
                default:
                    break;
            }
        }

        private void UpdateButtons()
        {
            NotifyPropertyChanged(CanExecuteNextArgs);
            NotifyPropertyChanged(CanExecutePreviousArgs);
            NotifyPropertyChanged(CanExecuteFinishArgs);
            NotifyPropertyChanged(CanExecuteCancelArgs);
            NotifyPropertyChanged(CanExecuteCloseArgs);
            NotifyPropertyChanged(CloseMethodArgs);
            NotifyPropertyChanged(FinishActionArgs);
        }
    }
}
