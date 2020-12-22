using System.Windows;
using System.Windows.Controls;

namespace Questify.Builder.UI.Wpf.Controls
{
    public class BlockGridBlockRowsPanel : StackPanel
    {
        static BlockGridBlockRowsPanel()
        {
            var ownerType = typeof(BlockGridBlockRowsPanel);
        }


        public BlockGridBlockRowsPanel()
        {
            SetValue(HorizontalAlignmentProperty, HorizontalAlignment.Left);
        }

    }
}
