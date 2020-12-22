using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views.ScoreEditors
{
    public class AdornedTextBlock : TextBlock
    {
        public AdornedTextBlock()
        {
            this.Initialized += AdornedTextBlock_Initialized;
        }

        void AdornedTextBlock_Initialized(object sender, EventArgs e)
        {
            if (ShowAdorner)
            {
                AdornerLayer.GetAdornerLayer(this).Add(new TextBlockAdorner(this));
            }
        }

        public bool ShowAdorner
        {
            get { return (bool)GetValue(ShowAdornerProperty); }
            set { SetValue(ShowAdornerProperty, value); }
        }

        public static readonly DependencyProperty ShowAdornerProperty =
    DependencyProperty.Register("ShowAdorner", typeof(bool), typeof(AdornedTextBlock), new PropertyMetadata());




    }
}
