using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using Questify.Builder.UI.Wpf.Controls;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views.ScoreEditors
{
    public class BlockGridSelectedItemsBehaviour : Behavior<BlockGrid>
    {
        public static readonly DependencyProperty SelectedItemsProperty =
    DependencyProperty.Register("SelectedItems", typeof(object),
    typeof(BlockGridSelectedItemsBehaviour),
    new FrameworkPropertyMetadata(null) { BindsTwoWayByDefault = true });

        public IList<BlockRow> SelectedItems
        {
            get { return (IList<BlockRow>)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.SelectionChanged += OnBlockGridSelectionChanged;
        }

        private void OnBlockGridSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            foreach (var item in e.AddedItems)
            {
                if (item is BlockRow)
                {
                    SelectedItems.Add((BlockRow)item);
                }
            }

            foreach (var item in e.RemovedItems)
            {
                if (item is BlockRow)
                {
                    SelectedItems.Remove((BlockRow)item);
                }
            }

        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (this.AssociatedObject != null)
                this.AssociatedObject.SelectionChanged -= OnBlockGridSelectionChanged;
        }

    }
}
