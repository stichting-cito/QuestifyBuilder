using System.Windows;
using System.Windows.Controls;

namespace Questify.Builder.UI.Wpf.Presentation.MarkupExtensions
{
    public static class AutoResizeParentGridView
    {
        public static bool GetResizeUponResize(FrameworkElement frameworkElement)
        {
            return (bool)frameworkElement.GetValue(ResizeUponResizeProperty);
        }

        public static void SetResizeUponResize(FrameworkElement frameworkElement, bool value)
        {
            frameworkElement.SetValue(ResizeUponResizeProperty, value);
        }

        public static readonly DependencyProperty ResizeUponResizeProperty = DependencyProperty.RegisterAttached(
    "ResizeUponResize", typeof(bool), typeof(AutoResizeParentGridView), new UIPropertyMetadata(false, OnResizeUponResizeChanged));

        private static void OnResizeUponResizeChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        {
            var item = depObj as FrameworkElement;
            if (item == null || e.NewValue is bool == false)
                return;

            if ((bool)e.NewValue)
                item.SizeChanged += item_SizeChanged;
            else
                item.SizeChanged -= item_SizeChanged;
        }

        private static void item_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var row = GetParentRowPresenter(sender as DependencyObject);
            if (row != null) ResizeGridView(row);
        }

        public static bool GetResizeUponComboSelection(ComboBox comboBox)
        {
            return (bool)comboBox.GetValue(ResizeUponSelectionProperty);
        }

        public static void SetResizeUponComboSelection(ComboBox combobox, bool value)
        {
            combobox.SetValue(ResizeUponSelectionProperty, value);
        }

        public static readonly DependencyProperty ResizeUponSelectionProperty = DependencyProperty.RegisterAttached(
    "ResizeUponComboSelection", typeof(bool), typeof(AutoResizeParentGridView), new UIPropertyMetadata(false, OnResizeUponComboSelection));

        private static void OnResizeUponComboSelection(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        {
            var item = depObj as ComboBox;
            if (item == null)
                return;

            if (e.NewValue is bool == false)
                return;

            if ((bool)e.NewValue)
                item.SelectionChanged += item_SelectionChanged;
            else
                item.SelectionChanged -= item_SelectionChanged;
        }

        static void item_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var row = GetParentRowPresenter(sender as DependencyObject);
            if (row != null) ResizeGridView(row);
        }

        public static bool GetResizeUponVisbility(FrameworkElement frameworkElement)
        {
            return (bool)frameworkElement.GetValue(ResizeUponVisibilityProperty);
        }

        public static void SetResizeUponVisbility(FrameworkElement frameworkElement, bool value)
        {
            frameworkElement.SetValue(ResizeUponVisibilityProperty, value);
        }

        public static readonly DependencyProperty ResizeUponVisibilityProperty = DependencyProperty.RegisterAttached(
    "ResizeUponVisibilityChanged", typeof(bool), typeof(AutoResizeParentGridView), new UIPropertyMetadata(false, OnResizeUponVisbility));

        private static void OnResizeUponVisbility(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        {
            var item = depObj as FrameworkElement;
            if (item == null || e.NewValue is bool == false)
                return;

            if ((bool)e.NewValue)
                item.IsVisibleChanged += item_IsVisibleChanged;
            else
                item.IsVisibleChanged -= item_IsVisibleChanged;
        }

        static void item_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var row = GetParentRowPresenter(sender as DependencyObject);
            if (row != null) ResizeGridView(row);
        }



        private static GridViewRowPresenter GetParentRowPresenter(DependencyObject child)
        {
            if (child == null) return null;
            var parent = System.Windows.Media.VisualTreeHelper.GetParent(child);
            while (parent != null && !(parent is GridViewRowPresenter))
            {
                parent = System.Windows.Media.VisualTreeHelper.GetParent(parent);
            }
            return parent != null ? (GridViewRowPresenter)parent : null;
        }

        private static void ResizeGridView(GridViewRowPresenter rowPresenter)
        {
            foreach (var column in rowPresenter.Columns)
            {
                if (double.IsNaN(column.Width))
                {
                    column.Width = column.ActualWidth;
                }
                else continue;

                column.Width = double.NaN;
            }
        }

    }
}
