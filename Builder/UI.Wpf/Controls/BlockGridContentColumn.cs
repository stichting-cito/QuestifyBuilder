using System.Windows;
using System.Windows.Controls;

namespace Questify.Builder.UI.Wpf.Controls
{
    public abstract class BlockGridContentColumn<T> : BlockGridColumn
    where T : ContentControl
    {


        internal FrameworkElement BuildVisualTree(bool isEditing, object dataItem, T contentControl)
        {
            if (isEditing)
            {
                return GenerateEditingElement(contentControl, dataItem);
            }
            else
            {
                return GenerateElement(contentControl, dataItem);
            }
        }

        protected abstract FrameworkElement GenerateElement(ContentControl contentControl, object dataItem);

        protected abstract FrameworkElement GenerateEditingElement(ContentControl contentControl, object dataItem);




        protected virtual object PrepareCellForEdit(FrameworkElement editingElement, RoutedEventArgs editingEventArgs)
        {
            return null;
        }

        protected virtual void CancelCellEdit(FrameworkElement editingElement, object uneditedValue)
        {
            BlockGridHelper.UpdateTarget(editingElement);
        }

        protected virtual bool CommitCellEdit(FrameworkElement editingElement)
        {
            return BlockGridHelper.ValidateWithoutUpdate(editingElement);
        }

        internal void BeginEdit(FrameworkElement editingElement, RoutedEventArgs e)
        {
            if (editingElement != null)
            {
                editingElement.UpdateLayout();

                object originalValue = PrepareCellForEdit(editingElement, e);
                SetOriginalValue(editingElement, originalValue);
            }
        }

        internal void CancelEdit(FrameworkElement editingElement)
        {
            if (editingElement != null)
            {
                CancelCellEdit(editingElement, GetOriginalValue(editingElement));
                ClearOriginalValue(editingElement);
            }
        }

        internal bool CommitEdit(FrameworkElement editingElement)
        {
            if (editingElement != null)
            {
                if (CommitCellEdit(editingElement))
                {
                    ClearOriginalValue(editingElement);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        private static object GetOriginalValue(DependencyObject obj)
        {
            return (object)obj.GetValue(OriginalValueProperty);
        }

        private static void SetOriginalValue(DependencyObject obj, object value)
        {
            obj.SetValue(OriginalValueProperty, value);
        }

        private static void ClearOriginalValue(DependencyObject obj)
        {
            obj.ClearValue(OriginalValueProperty);
        }

        private static readonly DependencyProperty OriginalValueProperty =
            DependencyProperty.RegisterAttached("OriginalValue", typeof(object), typeof(BlockGridContentColumn<T>), new FrameworkPropertyMetadata(null));


    }
}
