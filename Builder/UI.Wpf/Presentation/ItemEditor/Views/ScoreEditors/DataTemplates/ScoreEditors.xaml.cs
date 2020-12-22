using System;
using System.Windows;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views.ScoreEditors.DataTemplates
{
    partial class ScoreEditors : ResourceDictionary
    {
        private void ExplicitBindingUpdateOnClosed(object sender, EventArgs e)
        {
            var binding = ((System.Windows.Controls.Primitives.Popup)sender).GetBindingExpression(System.Windows.Controls.Primitives.Popup.DataContextProperty);
            binding.UpdateSource();
        }
    }
}
