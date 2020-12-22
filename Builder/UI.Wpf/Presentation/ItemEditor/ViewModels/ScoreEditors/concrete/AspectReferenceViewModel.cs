using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete
{
    public class AspectReferenceViewModel : INotifyPropertyChanged
    {
        private int _maxScore;

        public string name { get; set; }
        public string title { get; set; }
        public int maxScore
        {
            get
            {
                return _maxScore;
            }
            set
            {
                _maxScore = value;
                OnPropertyChanged();
            }
        }
        public bool isSelected { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
