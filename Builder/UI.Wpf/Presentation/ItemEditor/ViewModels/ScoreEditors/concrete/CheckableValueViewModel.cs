using System;
using System.ComponentModel;
using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete
{
    class CheckableValueViewModel : ViewModelBase
    {

        static PropertyChangedEventArgs CheckChangedArgs = ObservableHelper.CreateArgs<CheckableValueViewModel>(x => x.Checked);
        private bool _checked;

        public bool Checked
        {
            get { return _checked; }
            set
            {
                _checked = value;
                if (DoCheckedChanged != null)
                {
                    DoCheckedChanged(this);
                }
            }
        }

        public string DisplayValue { get; set; }

        public object Tag { get; set; }



        public Action<CheckableValueViewModel> DoCheckedChanged { get; set; }
    }
}
