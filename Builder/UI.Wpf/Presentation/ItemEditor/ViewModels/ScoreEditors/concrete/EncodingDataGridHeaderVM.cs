using System.Windows.Media.Imaging;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete
{
    internal class EncodingDataGridHeaderVm
    {

        private readonly string _headerText;
        private readonly string _toolTipText;



        public EncodingDataGridHeaderVm(string displayValue)
        {
            _headerText = (displayValue.Length > 30) ? displayValue.Substring(0, 30) + "..." : displayValue;
            _toolTipText = displayValue;
        }



        public string ToolTipText
        {
            get { return _toolTipText; }
        }

        public string HeaderText
        {
            get { return _headerText; }
        }

        public bool CanDelete { get; set; }

        public string ConceptId { get; set; }

        public bool HasPreProcessingRules { get; set; }

        public bool HasHeaderImage
        {
            get { return (HeaderImage != default(BitmapImage)); }
        }

        public BitmapImage HeaderImage { get; set; }

        public override string ToString()
        {
            return _headerText;
        }



    }
}