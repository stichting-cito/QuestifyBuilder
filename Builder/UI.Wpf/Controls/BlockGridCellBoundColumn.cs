using System.Windows;
using System.Windows.Data;

namespace Questify.Builder.UI.Wpf.Controls
{
    public abstract class BlockGridCellBoundColumn : BlockGridCellColumn
    {
        private BindingBase _binding;

        public virtual BindingBase Binding
        {
            get
            {
                return _binding;
            }

            set
            {
                if (_binding != value)
                {
                    BindingBase oldBinding = _binding;
                    _binding = value;
                    CoerceValue(IsReadOnlyProperty);
                    OnBindingChanged(oldBinding, _binding);
                }
            }
        }

        protected override bool OnCoerceIsReadOnly(bool baseValue)
        {
            if (BlockGridHelper.IsOneWay(_binding))
            {
                return true;
            }

            return base.OnCoerceIsReadOnly(baseValue);
        }


        internal void ApplyBinding(DependencyObject target, DependencyProperty property)
        {
            BindingBase binding = Binding;
            if (binding != null)
            {
                BindingOperations.SetBinding(target, property, binding);
            }
            else
            {
                BindingOperations.ClearBinding(target, property);
            }
        }


        protected virtual void OnBindingChanged(BindingBase oldBinding, BindingBase newBinding)
        {
            NotifyPropertyChanged("Binding");
        }



        public Style ElementStyle
        {
            get { return (Style)GetValue(ElementStyleProperty); }
            set { SetValue(ElementStyleProperty, value); }
        }

        public static readonly DependencyProperty ElementStyleProperty =
    DependencyProperty.Register(
        "ElementStyle",
        typeof(Style),
        typeof(BlockGridCellBoundColumn),
        new FrameworkPropertyMetadata(null, new PropertyChangedCallback(BlockGridCellColumn.NotifyPropertyChangeForRefreshContent)));

        public Style EditingElementStyle
        {
            get { return (Style)GetValue(EditingElementStyleProperty); }
            set { SetValue(EditingElementStyleProperty, value); }
        }

        public static readonly DependencyProperty EditingElementStyleProperty =
    DependencyProperty.Register(
        "EditingElementStyle",
        typeof(Style),
        typeof(BlockGridCellBoundColumn),
        new FrameworkPropertyMetadata(null, new PropertyChangedCallback(BlockGridCellColumn.NotifyPropertyChangeForRefreshContent)));

        internal void ApplyStyle(bool isEditing, bool defaultToElementStyle, FrameworkElement element)
        {
            Style style = PickStyle(isEditing, defaultToElementStyle);
            if (style != null)
            {
                element.Style = style;
            }
        }

        private Style PickStyle(bool isEditing, bool defaultToElementStyle)
        {
            Style style = isEditing ? EditingElementStyle : ElementStyle;
            if (isEditing && defaultToElementStyle && (style == null))
            {
                style = ElementStyle;
            }

            return style;
        }

    }
}
