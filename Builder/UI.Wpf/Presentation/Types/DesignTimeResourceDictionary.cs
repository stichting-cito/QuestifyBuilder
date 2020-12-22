using System;
using System.Windows;
using MEFedMVVM.Common;

namespace Questify.Builder.UI.Wpf.Presentation.Types
{
    internal class DesignTimeResourceDictionary : ResourceDictionary
    {
        public new Uri Source
        {
            get { return base.Source; }
            set
            {
                if (!Designer.IsInDesignMode)
                    return;
                base.Source = value;
            }
        }
    }
}
