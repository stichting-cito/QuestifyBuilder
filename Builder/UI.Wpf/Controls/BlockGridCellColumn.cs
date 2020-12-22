using System.Windows;
using System.Windows.Input;

namespace Questify.Builder.UI.Wpf.Controls
{
    public abstract class BlockGridCellColumn : BlockGridContentColumn<BlockGridCell>
    {

        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }


        public static readonly DependencyProperty IsReadOnlyProperty =
    DependencyProperty.Register(
                            "IsReadOnly",
                            typeof(bool),
                            typeof(BlockGridColumn),
                            new FrameworkPropertyMetadata(false,
                                OnNotifyCellPropertyChanged,
                                (s, e) => ((BlockGridCellColumn)s).OnCoerceIsReadOnly((bool)e)));


        protected virtual bool OnCoerceIsReadOnly(bool baseValue)
        {
            return (bool)BlockGridHelper.GetCoercedTransferPropertyValue(
                this,
                baseValue,
                IsReadOnlyProperty,
                BlockGridOwner,
                BlockGrid.IsReadOnlyProperty);
        }





        internal virtual void OnInput(InputEventArgs e)
        {
        }

        internal void TryBeginEdit(InputEventArgs e, bool handled)
        {
            var owner = BlockGridOwner;
            if (owner != null)
            {
                if (owner.TryBeginEdit(e))
                {
                    e.Handled |= handled;
                }
            }
        }

    }
}
