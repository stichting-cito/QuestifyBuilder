using System;
using System.Windows.Controls;
using Questify.Builder.UI.Wpf.Controls;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Controls.HelperClasses
{
    class BlockGridTestColumn : BlockGridCellColumn
    {
        protected override System.Windows.FrameworkElement GenerateElement(System.Windows.Controls.ContentControl cell, object dataItem)
        {
            return new TextBlock();
        }

        protected override System.Windows.FrameworkElement GenerateEditingElement(System.Windows.Controls.ContentControl cell, object dataItem)
        {
            throw new NotImplementedException();
        }
    }
}
