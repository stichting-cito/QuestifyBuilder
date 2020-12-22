using System.Windows;
using System.Windows.Media;
using Questify.Builder.Logic.Service.Interfaces.UI;

namespace Questify.Builder.UI.Wpf.Presentation.Helpers
{
    public class WpfHelper
    {

        public static T GetAncestor<T>(DependencyObject reference) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(reference);
            while (parent != null && !(parent is T))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            if (parent != null)
            {
                return (T)parent;
            }
            return null;
        }


        public static T GetByDependencyObjectByInterface<T>(DependencyObject reference) where T : class
        {
            if (reference == null)
            {
                return null;
            }

            var service = reference as T;
            if (service == null)
            {
                return GetByDependencyObjectByInterface<T>(VisualTreeHelper.GetParent(reference));
            }

            return service;
        }

        public static DependencyObject GetParent(DependencyObject reference)
        {
            var parent = VisualTreeHelper.GetParent(reference);
            var keepSearching = true;
            while (keepSearching)
            {
                var parentControl = VisualTreeHelper.GetParent(parent);
                if (parentControl != null)
                {
                    parent = parentControl;
                }
                else
                {
                    keepSearching = false;
                }
            }
            return parent;
        }

        public static T FindChild<T>(DependencyObject parent, string childName)
   where T : DependencyObject
        {
            if (parent == null)
            {
                return null;
            }

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                T childType = child as T;
                if (childType == null)
                {
                    foundChild = FindChild<T>(child, childName);

                    if (foundChild != null)
                    {
                        break;
                    }
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }

        private static System.Windows.Forms.Control FindFocusedControl2(System.Windows.Forms.Control control, int maxDepth)
        {
            if (maxDepth <= 0 || control == null)
            {
                return null;
            }
            if (control is IViewCommands)
            {
                return control;
            }
            if (control.Parent != null)
            {
                return FindFocusedControl2(control.Parent, maxDepth - 1);
            }
            return null;
        }
    }
}
