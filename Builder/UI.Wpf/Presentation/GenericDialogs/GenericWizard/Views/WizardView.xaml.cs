using System.Windows;
using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.GenericDialogs.GenericWizard.Views
{
    [PopupNameToViewLookupKeyMetadata(Constants.WizardWorkSpace, typeof(WizardView))]
    public partial class WizardView : Window
    {
        public WizardView()
        {
            InitializeComponent();
        }
    }
}
