using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Questify.Builder.UI.Wpf.Controls
{
    internal static class BlockGridHelper
    {
        public static T FindVisualParent<T>(UIElement element) where T : UIElement
        {
            var parent = element;
            while (parent != null)
            {
                var correctlyTyped = parent as T;
                if (correctlyTyped != null)
                {
                    return correctlyTyped;
                }

                parent = VisualTreeHelper.GetParent(parent) as UIElement;
            }

            return null;
        }

        public static T FindParent<T>(FrameworkElement element) where T : FrameworkElement
        {
            var parent = element.TemplatedParent as FrameworkElement;

            while (parent != null)
            {
                var correctlyTyped = parent as T;
                if (correctlyTyped != null)
                {
                    return correctlyTyped;
                }

                parent = parent.TemplatedParent as FrameworkElement;
            }

            return null;
        }


        public static T FindFirstVisualChild<T>(UIElement element) where T : UIElement
        {
            var parent = element;
            if (parent != null)
            {
                for (var i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
                {
                    var child = VisualTreeHelper.GetChild(parent, i) as UIElement;
                    if (child != null)
                    {
                        var childCorrectlyTyped = child as T;

                        if (childCorrectlyTyped == null)
                        {
                            childCorrectlyTyped = FindFirstVisualChild<T>(child);
                        }
                        if (childCorrectlyTyped != null)
                        {
                            return childCorrectlyTyped;
                        }
                    }
                }
            }

            return null;
        }




        public static bool IsDefaultValue(DependencyObject d, DependencyProperty dp)
        {
            return DependencyPropertyHelper.GetValueSource(d, dp).BaseValueSource == BaseValueSource.Default;
        }

        public static object GetCoercedTransferPropertyValue(
            DependencyObject baseObject,
            object baseValue,
            DependencyProperty baseProperty,
            DependencyObject parentObject,
            DependencyProperty parentProperty)
        {
            return GetCoercedTransferPropertyValue(
                baseObject,
                baseValue,
                baseProperty,
                parentObject,
                parentProperty,
                null,
                null);
        }

        public static object GetCoercedTransferPropertyValue(
    DependencyObject baseObject,
    object baseValue,
    DependencyProperty baseProperty,
    DependencyObject parentObject,
    DependencyProperty parentProperty,
    DependencyObject grandParentObject,
    DependencyProperty grandParentProperty)
        {
            var coercedValue = baseValue;

            if (IsPropertyTransferEnabled(baseObject, baseProperty))
            {
                var propertySource = DependencyPropertyHelper.GetValueSource(baseObject, baseProperty);
                var maxBaseValueSource = propertySource.BaseValueSource;

                if (parentObject != null)
                {
                    var parentPropertySource = DependencyPropertyHelper.GetValueSource(parentObject, parentProperty);

                    if (parentPropertySource.BaseValueSource > maxBaseValueSource)
                    {
                        coercedValue = parentObject.GetValue(parentProperty);
                        maxBaseValueSource = parentPropertySource.BaseValueSource;
                    }
                }

                if (grandParentObject != null)
                {
                    var grandParentPropertySource = DependencyPropertyHelper.GetValueSource(grandParentObject, grandParentProperty);

                    if (grandParentPropertySource.BaseValueSource > maxBaseValueSource)
                    {
                        coercedValue = grandParentObject.GetValue(grandParentProperty);
                    }
                }
            }

            return coercedValue;
        }

        public static void TransferProperty(DependencyObject d, DependencyProperty p)
        {
            var transferEnabledMap = GetPropertyTransferEnabledMapForObject(d);
            transferEnabledMap[p] = true;
            d.CoerceValue(p);
            transferEnabledMap[p] = false;
        }

        private static Dictionary<DependencyProperty, bool> GetPropertyTransferEnabledMapForObject(DependencyObject d)
        {
            Dictionary<DependencyProperty, bool> propertyTransferEnabledForObject;

            if (!_propertyTransferEnabledMap.TryGetValue(d, out propertyTransferEnabledForObject))
            {
                propertyTransferEnabledForObject = new Dictionary<DependencyProperty, bool>();
                _propertyTransferEnabledMap.Add(d, propertyTransferEnabledForObject);
            }

            return propertyTransferEnabledForObject;
        }

        internal static bool IsPropertyTransferEnabled(DependencyObject d, DependencyProperty p)
        {
            Dictionary<DependencyProperty, bool> propertyTransferEnabledForObject;

            if (_propertyTransferEnabledMap.TryGetValue(d, out propertyTransferEnabledForObject))
            {
                bool isPropertyTransferEnabled;
                if (propertyTransferEnabledForObject.TryGetValue(p, out isPropertyTransferEnabled))
                {
                    return isPropertyTransferEnabled;
                }
            }

            return false;
        }

        private static ConditionalWeakTable<DependencyObject, Dictionary<DependencyProperty, bool>> _propertyTransferEnabledMap = new ConditionalWeakTable<DependencyObject, Dictionary<DependencyProperty, bool>>();



        private static readonly DependencyProperty FlowDirectionCacheProperty = DependencyProperty.Register("FlowDirectionCache", typeof(FlowDirection), typeof(BlockGridHelper));

        internal static bool ValidateWithoutUpdate(FrameworkElement element)
        {
            var result = true;
            var bindingGroup = element?.BindingGroup;
            var cell = element?.Parent as BlockGridCell;

            if (bindingGroup != null && cell != null)
            {
                var expressions = bindingGroup.BindingExpressions;
                var bindingExpressionsCopy = new BindingExpressionBase[expressions.Count];
                expressions.CopyTo(bindingExpressionsCopy, 0);

                for (var i = 0; i < bindingExpressionsCopy.Length; i++)
                {
                    var beb = bindingExpressionsCopy[i];

                    var targetElement = beb.Target;
                    if (targetElement != null)
                    {
                        result = beb.ValidateWithoutUpdate() && result;
                    }
                }
            }

            return result;
        }

        internal static void UpdateTarget(FrameworkElement element)
        {
            var bindingGroup = element?.BindingGroup;
            var cell = element?.Parent as BlockGridCell;

            if (bindingGroup != null && cell != null)
            {
                var expressions = bindingGroup.BindingExpressions;
                var bindingExpressionsCopy = new BindingExpressionBase[expressions.Count];
                expressions.CopyTo(bindingExpressionsCopy, 0);

                foreach (var beb in bindingExpressionsCopy)
                {
                    var targetElement = beb.Target;
                    if (targetElement != null)
                    {
                        beb.UpdateTarget();
                    }
                }
            }
        }

        internal static bool IsOneWay(BindingBase bindingBase)
        {
            if (bindingBase == null)
            {
                return false;
            }

            var binding = bindingBase as Binding;
            if (binding != null)
            {
                return binding.Mode == BindingMode.OneWay;
            }

            var multiBinding = bindingBase as MultiBinding;
            if (multiBinding != null)
            {
                return multiBinding.Mode == BindingMode.OneWay;
            }

            var priBinding = bindingBase as PriorityBinding;
            if (priBinding != null)
            {
                var subBindings = priBinding.Bindings;
                var count = subBindings.Count;
                for (var i = 0; i < count; i++)
                {
                    if (IsOneWay(subBindings[i]))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        internal static void SyncColumnProperty(DependencyObject column, DependencyObject content, DependencyProperty contentProperty, DependencyProperty columnProperty)
        {
            if (IsDefaultValue(column, columnProperty))
            {
                content.ClearValue(contentProperty);
            }
            else
            {
                content.SetValue(contentProperty, column.GetValue(columnProperty));
            }
        }

        internal static void RestoreFlowDirection(FrameworkElement element, BlockGridCell cell)
        {
            if (element != null && cell != null)
            {
                var flowDirectionObj = cell.ReadLocalValue(FlowDirectionCacheProperty);
                if (flowDirectionObj != DependencyProperty.UnsetValue)
                {
                    element.SetValue(FrameworkElement.FlowDirectionProperty, flowDirectionObj);
                }
            }
        }



        public static bool ShouldNotifyCells(BlockGridNotificationTarget target)
        {
            return TestTarget(target, BlockGridNotificationTarget.Cells);
        }

        public static bool ShouldNotifyCellsPresenter(BlockGridNotificationTarget target)
        {
            return TestTarget(target, BlockGridNotificationTarget.CellsPresenter);
        }

        public static bool ShouldNotifyColumns(BlockGridNotificationTarget target)
        {
            return TestTarget(target, BlockGridNotificationTarget.Columns);
        }

        public static bool ShouldNotifyColumnHeaders(BlockGridNotificationTarget target)
        {
            return TestTarget(target, BlockGridNotificationTarget.ColumnHeaders);
        }

        public static bool ShouldNotifyColumnHeadersPresenter(BlockGridNotificationTarget target)
        {
            return TestTarget(target, BlockGridNotificationTarget.ColumnHeadersPresenter);
        }

        public static bool ShouldNotifyColumnCollection(BlockGridNotificationTarget target)
        {
            return TestTarget(target, BlockGridNotificationTarget.ColumnCollection);
        }

        public static bool ShouldNotifyDataGrid(BlockGridNotificationTarget target)
        {
            return TestTarget(target, BlockGridNotificationTarget.DataGrid);
        }

        public static bool ShouldNotifyDetailsPresenter(BlockGridNotificationTarget target)
        {
            return TestTarget(target, BlockGridNotificationTarget.DetailsPresenter);
        }

        public static bool ShouldRefreshCellContent(BlockGridNotificationTarget target)
        {
            return TestTarget(target, BlockGridNotificationTarget.RefreshCellContent);
        }

        public static bool ShouldNotifyRowHeaders(BlockGridNotificationTarget target)
        {
            return TestTarget(target, BlockGridNotificationTarget.RowHeaders);
        }

        public static bool ShouldNotifyRows(BlockGridNotificationTarget target)
        {
            return TestTarget(target, BlockGridNotificationTarget.Rows);
        }

        public static bool ShouldNotifyRowSubtree(BlockGridNotificationTarget target)
        {
            var value =
                BlockGridNotificationTarget.Rows |
                BlockGridNotificationTarget.RowHeaders |
                BlockGridNotificationTarget.CellsPresenter |
                BlockGridNotificationTarget.Cells |
                BlockGridNotificationTarget.RefreshCellContent |
                BlockGridNotificationTarget.DetailsPresenter;

            return TestTarget(target, value);
        }

        private static bool TestTarget(BlockGridNotificationTarget target, BlockGridNotificationTarget value)
        {
            return (target & value) != 0;
        }




        public static bool HasNonEscapeCharacters(TextCompositionEventArgs textArgs)
        {
            if (textArgs != null)
            {
                var text = textArgs.Text;
                for (int i = 0, count = text.Length; i < count; i++)
                {
                    if (text[i] != EscapeChar)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool IsImeProcessed(KeyEventArgs keyArgs)
        {
            return keyArgs?.Key == Key.ImeProcessed;
        }

        private const char EscapeChar = '\u001b';


    }
}
