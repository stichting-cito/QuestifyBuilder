using System.Collections.Generic;
using System.ComponentModel;
using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete
{
    class GapsAndPreprocessing : INotifyPropertyChanged
    {
        static PropertyChangedEventArgs GapArgs = ObservableHelper.CreateArgs<GapsAndPreprocessing>(x => x.Gaps);
        private List<GapValueViewModel<string>> _gaps;

        public List<GapValueViewModel<string>> Gaps
        {
            get { return _gaps; }
            set
            {
                _gaps = value;
                PropertyChanged(this, GapArgs);
            }
        }
        public List<CheckableValueViewModel> Preprocessing { get; set; }


        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
