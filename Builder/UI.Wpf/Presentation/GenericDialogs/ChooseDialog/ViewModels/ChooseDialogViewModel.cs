using System.Collections.Generic;
using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.GenericDialogs.ChooseDialog.ViewModels
{
    public class ChooseDialogViewModel : ViewModelBase
    {
        public static readonly System.ComponentModel.PropertyChangedEventArgs ChoosableObjectsChangedEventArgs = ObservableHelper.CreateArgs<ChooseDialogViewModel>(x => x.ChoosableObjects);
        public static readonly System.ComponentModel.PropertyChangedEventArgs SelectedObjectChangedEventArgs = ObservableHelper.CreateArgs<ChooseDialogViewModel>(x => x.SelectedObject);

        public DataWrapper<IList<object>> ChoosableObjects { get; private set; }
        public DataWrapper<object> SelectedObject { get; private set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public ChooseDialogViewModel(string title, string description)
        {
            ChoosableObjects = new DataWrapper<IList<object>>(this, ChoosableObjectsChangedEventArgs);
            SelectedObject = new DataWrapper<object>(this, SelectedObjectChangedEventArgs);
            InitCommands();
            Title = title;
            Description = description;
        }

        private void InitCommands()
        {
            OkCommand = new SimpleCommand<object, object>(o => CanOk(), o => DoOk());
            CancelCommand = new SimpleCommand<object, object>(o => DoCancel());
        }

        private bool CanOk()
        {
            return true;
        }

        private void DoOk()
        {
            RaiseCloseRequest(true);
        }

        private void DoCancel()
        {
            RaiseCloseRequest(false);
        }


        public SimpleCommand<object, object> OkCommand { get; private set; }
        public SimpleCommand<object, object> CancelCommand { get; private set; }

    }
}
